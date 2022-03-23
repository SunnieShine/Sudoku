﻿using System.Buffers;
using Sudoku.Collections;
using Xunit.Abstractions;

namespace Sudoku.Test;

internal sealed partial class AicSearcher
{
	/// <summary>
	/// Indicates the field that stores the temporary strong inferences during the searching.
	/// </summary>
	/// <remarks>
	/// The value uses a <see cref="Dictionary{TKey, TValue}"/> to store the table of strong inferences, where:
	/// <list type="table">
	/// <listheader>
	/// <term>Item</term>
	/// <description>Meaning</description>
	/// </listheader>
	/// <item>
	/// <term>Key</term>
	/// <description>The ID of a node.</description>
	/// </item>
	/// <item>
	/// <term>Value</term>
	/// <description>
	/// All possible IDs that corresponds to their own node respectively,
	/// one of which can form a strong inference with the <b>Key</b> node.
	/// </description>
	/// </item>
	/// </list>
	/// </remarks>
	/// <seealso cref="Dictionary{TKey, TValue}"/>
	private readonly Dictionary<int, HashSet<int>?> _strongInferences = new();

	/// <summary>
	/// Indicates the field that stores the temporary weak inferences during the searching.
	/// </summary>
	/// <remarks>
	/// The value uses a <see cref="Dictionary{TKey, TValue}"/> to store the table of weak inferences, where:
	/// <list type="table">
	/// <listheader>
	/// <term>Item</term>
	/// <description>Meaning</description>
	/// </listheader>
	/// <item>
	/// <term>Key</term>
	/// <description>The ID of a node.</description>
	/// </item>
	/// <item>
	/// <term>Value</term>
	/// <description>
	/// All possible IDs that corresponds to their own node respectively,
	/// one of which can form a weak inference with the <b>Key</b> node.
	/// </description>
	/// </item>
	/// </list>
	/// </remarks>
	/// <seealso cref="Dictionary{TKey, TValue}"/>
	private readonly Dictionary<int, HashSet<int>?> _weakInferences = new();

	/// <summary>
	/// Indicates the lookup table that can get the target ID value
	/// via the corresponding <see cref="Node"/> instance.
	/// </summary>
	/// <seealso cref="Node"/>
	private readonly Dictionary<Node, int> _idLookup = new();

	/// <summary>
	/// Indicates all possible found chains, that stores the IDs of the each node.
	/// </summary>
	private readonly List<(int[] ChainIds, bool StartsWithWeakInference)> _foundChains = new();

	/// <summary>
	/// Indicates the output instance that can allow displaying the customized items onto the test explorer.
	/// </summary>
	private readonly ITestOutputHelper _output;

	/// <summary>
	/// Indicates the global ID value.
	/// </summary>
	private int _globalId;

	/// <summary>
	/// Indicates the lookup table that can get the target <see cref="Node"/> instance
	/// via the corresponding ID value specified as the index.
	/// </summary>
	private Node?[] _nodeLookup = null!;


	/// <summary>
	/// Initializes a <see cref="AicSearcher"/> instance via the <see cref="ITestOutputHelper"/> instance
	/// to allow displaying the customized items onto the test explorer.
	/// </summary>
	/// <param name="output">
	/// The <see cref="ITestOutputHelper"/> instance
	/// that allows displaying the customized items onto the test explorer.
	/// </param>
	/// <seealso cref="ITestOutputHelper"/>
	public AicSearcher(ITestOutputHelper output) => _output = output;


	/// <summary>
	/// Indicates the maximum capacity used for the allocation on shared memory.
	/// </summary>
	public int MaxCapacity { get; set; } = 3000;

	/// <summary>
	/// Indicates the extended nodes to be searched for. Please note that the type of the property
	/// is an enumeration type with bit-fields attribute, which means you can add multiple choices
	/// into the value.
	/// </summary>
	public SearcherNodeTypes NodeTypes { get; set; } =
		SearcherNodeTypes.SoleDigit | SearcherNodeTypes.SoleCell
			| SearcherNodeTypes.LockedCandidates
			| SearcherNodeTypes.LockedSet | SearcherNodeTypes.HiddenSet;


	/// <summary>
	/// Get all possible chain steps.
	/// </summary>
	/// <param name="grid">The grid used.</param>
	public void GetAll(in Grid grid)
	{
		// Clear all possible lists.
		_strongInferences.Clear();
		_weakInferences.Clear();
		_nodeLookup = ArrayPool<Node?>.Shared.Rent(MaxCapacity);
		_idLookup.Clear();
		_foundChains.Clear();

		// Gather strong and weak links.
		GatherStrongAndWeak_Sole(grid);
		GatherStrongAndWeak_LockedCandidates(grid);
		GatherStrongAndWeak_AlmostLockedSet(grid);

		// Remove IDs if they don't appear in the lookup table.
		RemoveIdsNotAppearingInLookupDictionary(_weakInferences);
		RemoveIdsNotAppearingInLookupDictionary(_strongInferences);

#if true
		// Display the inferences found.
		PrintInferences(_strongInferences);
		PrintInferences(_weakInferences);
#else
		// Construct chains.
		Dfs_StartWithWeak();
		Dfs_StartWithStrong();

		// Output the result.
		var tempList = new Dictionary<AlternatingInferenceChain, Conclusion[]>();
		foreach (var (nids, startsWithWeak) in _foundChains)
		{
			var chain = new AlternatingInferenceChain(from nid in nids select _id2NodeLookup[nid], startsWithWeak);
			if (chain.GetConclusions(grid) is { Length: not 0 } conclusions && !tempList.ContainsKey(chain))
			{
				tempList.Add(chain, conclusions);
			}
		}

		foreach (var (chain, conclusions) in tempList)
		{
			_output.WriteLine($"{chain} => {new ConclusionCollection(conclusions).ToString()}");
		}
#endif

		// Clears the memory.
		ArrayPool<Node?>.Shared.Return(_nodeLookup);
	}

	/// <summary>
	/// Print the inferences.
	/// </summary>
	/// <param name="inferences">The table of inferences.</param>
	private void PrintInferences(Dictionary<int, HashSet<int>?> inferences)
	{
		const string separator = ", ";

		var sb = new StringHandler();
		foreach (var (id, nextIds) in inferences)
		{
			if (_nodeLookup[id] is not { } node)
			{
				continue;
			}

			sb.Append("Node ");
			sb.Append(node.ToSimpleString());
			sb.Append(": ");

			if (nextIds is not null)
			{
				foreach (int nextId in nextIds)
				{
					sb.Append(_nodeLookup[nextId]!.ToSimpleString());
					sb.Append(separator);
				}

				sb.RemoveFromEnd(separator.Length);
			}
			else
			{
				sb.Append("<null>");
			}

			sb.AppendLine();
		}

		_output.WriteLine(sb.ToStringAndClear());
	}

	/// <summary>
	/// To print the whole chain via the ID. The method is only used for calling by the debugger.
	/// </summary>
	/// <param name="chainIds">The IDs.</param>
	private string PrintChainData(int[] chainIds) =>
		string.Join(" -> ", from id in chainIds select _nodeLookup[id]!.ToString());
}
