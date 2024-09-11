namespace Sudoku.Concepts.Graphs;

using ConflictedInfo = ((Cell Left, Cell Right), CellMap InfluencedRange);

/// <summary>
/// <para>Represents a cluster. A cluster is a group of candidates which are all connected with strong links.</para>
/// <para>
/// This data structure will simplify the definition, only reserving for single-digit strong links (i.e. conjugate pairs),
/// in order to make the implementation behave well and easily on representing data.
/// </para>
/// <para>
/// Please visit <see href="http://sudopedia.enjoysudoku.com/Cluster.html">this link</see> to learn more information.
/// </para>
/// </summary>
/// <param name="grid">Indicates the grid used.</param>
/// <param name="digit">Indicates the digit used.</param>
/// <param name="map">Indicates the cells used.</param>
/// <seealso href="http://sudopedia.enjoysudoku.com/Cluster.html">Cluster</seealso>
[StructLayout(LayoutKind.Auto)]
[TypeImpl(TypeImplFlag.AllObjectMethods | TypeImplFlag.EqualityOperators, ToStringBehavior = ToStringBehavior.RecordLike)]
public readonly ref partial struct Cluster(
	[PrimaryConstructorParameter(MemberKinds.Field), HashCodeMember, StringMember] ref readonly Grid grid,
	[PrimaryConstructorParameter, HashCodeMember, StringMember] Digit digit,
	[PrimaryConstructorParameter(MemberKinds.Field), HashCodeMember, StringMember] scoped ref readonly CellMap map
) : IEquatable<Cluster>
{
	/// <summary>
	/// Indicates the internal map.
	/// </summary>
	[UnscopedRef]
	public ref readonly CellMap Map => ref _map;

	/// <summary>
	/// Represents a list of cells that will form wrap contradictions in the cluster.
	/// </summary>
	public CellMap WrapContradictions
	{
		get
		{
			var result = CellMap.Empty;
			var graph = CellGraph.CreateFromConjugatePair(in _grid, Digit, in Map);
			var components = graph.Components;
			var lastCells = Map;
			while (lastCells)
			{
				foreach (var componentStartCell in lastCells.ToArray())
				{
					var component = components.First(graph => graph.Contains(componentStartCell));
					foreach (var cellDegree1 in graph[1])
					{
						graph.GetComponentOf(cellDegree1, out var depths);
						var nodeGroups =
							from depth in depths
							let depthKey = (depth.Depth & 1) == 1
							group depth by depthKey into depthGroup
							let depthCells = (from depth in depthGroup select depth.Cell).AsCellMap()
							select (DepthValueIsOdd: depthGroup.Key, Cells: depthCells);

						for (var i = 0; i < 2; i++)
						{
							ref readonly var groupCells = ref nodeGroups[i].Cells;

							// Check whether there're two or more cells lying in a same house.
							foreach (var house in groupCells.Houses)
							{
								if ((HousesMap[house] & groupCells).Count >= 2)
								{
									// All of this group is wrong.
									result |= groupCells;
									goto NextComponent;
								}
							}
						}
					}

				NextComponent:
					lastCells &= ~component.Map;
					break;
				}
			}
			return result;
		}
	}

	/// <summary>
	/// Represents a list of cells that will form trap contradictions in the cluster.
	/// </summary>
	public CellMap TrapContradictions
	{
		get
		{
			var candsMap = _grid.CandidatesMap[Digit];
			var result = CellMap.Empty;
			var graph = CellGraph.CreateFromConjugatePair(in _grid, Digit, in Map);
			var components = graph.Components;
			var lastCells = Map;
			while (lastCells)
			{
				foreach (var componentStartCell in lastCells.ToArray())
				{
					var component = components.First(graph => graph.Contains(componentStartCell));
					foreach (var cellDegree1 in graph[1])
					{
						graph.GetComponentOf(cellDegree1, out var depths);
						var nodeGroups =
							from depth in depths
							let depthKey = (depth.Depth & 1) == 1
							group depth by depthKey into depthGroup
							let depthCells = (from depth in depthGroup select depth.Cell).AsCellMap()
							select (DepthValueIsOdd: depthGroup.Key, Cells: depthCells);

						// There must be 2 cases. Now we should iterate two collections to get contradiction.
						var conflictedCells = CellMap.Empty;
						var conflictedPair = new HashSet<ConflictedInfo>();
						foreach (var cell1 in nodeGroups[0].Cells)
						{
							foreach (var cell2 in nodeGroups[1].Cells)
							{
								var intersection = (cell1.AsCellMap() + cell2).PeerIntersection;
								var currentConflictCells = intersection & candsMap;
								if (!!currentConflictCells
									&& !conflictedPair.Any(p => (p.InfluencedRange & currentConflictCells) == currentConflictCells))
								{
									conflictedPair.Add(((cell1, cell2), currentConflictCells));
									conflictedCells |= currentConflictCells;
								}
							}
						}
						result |= conflictedCells;
					}

					lastCells &= ~component.Map;
					break;
				}
			}
			return result;
		}
	}


	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool Equals(Cluster other) => Map == other.Map;


	/// <summary>
	/// Creates a <see cref="Cluster"/> instance via the specified grid.
	/// </summary>
	/// <param name="grid">The grid to be used.</param>
	/// <param name="digit">Indicates the digits used.</param>
	/// <returns>A <see cref="Cluster"/> instance.</returns>
	public static Cluster Create(ref readonly Grid grid, Digit digit)
	{
		var result = CellMap.Empty;
		foreach (var cp in grid.ConjugatePairs)
		{
			if (cp.Digit == digit)
			{
				result |= cp.Map;
			}
		}
		return new(in grid, digit, in result);
	}
}
