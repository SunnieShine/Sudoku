﻿using System.Collections.Generic;
using Sudoku.Data;
using Sudoku.Drawing;
using Sudoku.Extensions;
using Sudoku.Solving.Annotations;
using static Sudoku.Constants.Processings;
using static Sudoku.Constants.RegionLabel;
using static Sudoku.Data.ConclusionType;
using static Sudoku.Solving.Constants.Processings;

namespace Sudoku.Solving.Manual.Chaining
{
	/// <summary>
	/// Encapsulates an <b>forcing chains</b> (<b>FCs</b>) technique searcher.
	/// </summary>
	[TechniqueDisplay(nameof(TechniqueCode.MultipleFc))]
	public sealed class FcTechniqueSearcher : ChainingTechniqueSearcher
	{
		/// <summary>
		/// Indicates the information.
		/// </summary>
		private readonly bool _nishio, _multiple/*, _dynamic*/;

		///// <summary>
		///// Indicates the level of the dynamic searching.
		///// </summary>
		//private readonly int _level;


		///// <summary>
		///// The temporary grid for handling dynamic and multiple forcing chains.
		///// </summary>
		//private Grid _tempGrid = null!;


		/// <summary>
		/// Initializes an instance with the specified information.
		/// </summary>
		/// <param name="nishio">Indicates whether the current searcher searches nishio forcing chains.</param>
		/// <param name="multiple">Indicates whether the current searcher searches multiple forcing chains.</param>
		///// <param name="dynamic">Indicates whether the current searcher searches dynamic forcing chains.</param>
		///// <param name="level">Indicates the level of the dynamic searching.</param>
		public FcTechniqueSearcher(bool nishio, bool multiple/*, bool dynamic, int level*/) =>
			(_nishio, _multiple/*, _dynamic, _level*/) = (nishio, multiple/*, dynamic, level*/);


		/// <summary>
		/// Indicates the priority of this technique.
		/// </summary>
		public static int Priority { get; set; } = 80;

		/// <summary>
		/// Indicates whether the technique is enabled.
		/// </summary>
		public static bool IsEnabled { get; set; } = true;


		/// <inheritdoc/>
		public override void GetAll(IBag<TechniqueInfo> accumulator, IReadOnlyGrid grid)
		{
			var tempAccumulator = new Bag<ChainingTechniqueInfo>();
			GetAll(tempAccumulator, grid);

			if (tempAccumulator.Count == 0)
			{
				return;
			}

			var set = new Set<ChainingTechniqueInfo>(tempAccumulator);
			set.Sort((i1, i2) =>
			{
				decimal d1 = i1.Difficulty, d2 = i2.Difficulty;
				if (d1 < d2)
				{
					return -1;
				}
				else if (d1 > d2)
				{
					return 1;
				}
				else
				{
					int l1 = i1.Complexity;
					int l2 = i2.Complexity;
					return l1 == l2 ? i1.SortKey - i2.SortKey : l1 - l2;
				}
			});

			accumulator.AddRange(set);
		}

		/// <summary>
		/// Search for chains of each type.
		/// </summary>
		/// <param name="accumulator">The accumulator.</param>
		/// <param name="grid">Thr grid.</param>
		private void GetAll(IBag<ChainingTechniqueInfo> accumulator, IReadOnlyGrid grid)
		{
			// Iterate on all empty cells.
			foreach (int cell in EmptyMap)
			{
				short mask = grid.GetCandidateMask(cell);
				int count = mask.CountSet();
				if (count > 2)
				{
					// Prepare storage and accumulator for cell eliminations.
					var valueToOn = new Dictionary<int, Set<Node>>();
					var valueToOff = new Dictionary<int, Set<Node>>();
					Set<Node>? cellToOn = null, cellToOff = null;

					// Iterate on all candidates that aren't alone.
					foreach (int digit in mask.GetAllSets())
					{
						// Do binary chaining (same candidate either on or off).
						var pOn = new Node(cell, digit, true);
						var pOff = new Node(cell, digit, false);
						var onToOn = new Set<Node>();
						var onToOff = new Set<Node>();
						//bool doDouble = count >= 3 && !_nishio && _dynamic, doContradiction = _dynamic || _nishio;
						//DoBinaryChaining(accumulator, grid, pOn, pOff, onToOn, onToOff, doDouble, doContradiction);

						if (!_nishio)
						{
							// Do region chaining.
							onToOn.Add(pOn);
							DoRegionChaining(accumulator, grid, cell, digit, onToOn, onToOff);
						}

						// Collect results for cell chaining.
						valueToOn.Add(digit, onToOn);
						valueToOff.Add(digit, onToOff);
						if (cellToOn is null/* || cellToOff is null*/)
						{
							cellToOn = new Set<Node>(onToOn);
							cellToOff = new Set<Node>(onToOff);
						}
						else
						{
							cellToOn &= onToOn;
							cellToOff = cellToOff! & onToOff;
						}
					}

					if (!_nishio)
					{
						// Do cell eliminations.
						if (count == 2 || _multiple)
						{
							if (!(cellToOn is null))
							{
								foreach (var p in cellToOn)
								{
									var hint = CreateCellEliminationHint(grid, cell, p, valueToOn);
									if (!(hint is null))
									{
										accumulator.Add(hint);
									}
								}
							}
							if (!(cellToOff is null))
							{
								foreach (var p in cellToOff)
								{
									var hint = CreateCellEliminationHint(grid, cell, p, valueToOff);
									if (!(hint is null))
									{
										accumulator.Add(hint);
									}
								}
							}
						}
					}
				}
			}
		}

		//private void DoBinaryChaining(
		//	IBag<ChainingTechniqueInfo> accumulator, IReadOnlyGrid grid, Node pOn, Node pOff,
		//	ISet<Node> onToOn, ISet<Node> onToOff, bool doDouble, bool doContradiction)
		//{
		//	Node[]? absurdNodes;
		//	Set<Node> offToOn = new Set<Node>(), offToOff = new Set<Node>();
		//
		//	// Circular forcing chains (hypothesis implying its negation)
		//	// are already covered by cell forcing chains, and are therefore not checked for.
		//
		//	// Test o is currectly on.
		//	onToOn.Add(pOn);
		//	absurdNodes = DoChaining(grid, onToOn, onToOff);
		//	if (doContradiction && !(absurdNodes is null))
		//	{
		//		// 'p' cannot hold its value, otherwise it would lead to a contradiction.
		//		var hint = CreateChainingOffHint(absurdNodes[0], absurdNodes[1], pOn, pOn, true);
		//		if (!(hint is null))
		//		{
		//			accumulator.Add(hint);
		//		}
		//	}
		//
		//	// Test p is currently off.
		//	offToOff.Add(pOff);
		//	if (doContradiction && !(absurdNodes is null))
		//	{
		//		// 'p' must hold its value otherwise it would lead to a contradiction.
		//		var hint = CreateChainingOnHint(absurdNodes[0], absurdNodes[1], pOff, pOff, true);
		//		if (!(hint is null))
		//		{
		//			accumulator.Add(hint);
		//		}
		//	}
		//
		//	if (doDouble)
		//	{
		//		// Check nodes that must be on in both case.
		//		foreach (var pFromOn in onToOn)
		//		{
		//			if (offToOn.Contains(pFromOn))
		//			{
		//				var hint = CreateChainingOnHint(pFromOn, pOn, pFromOn, false);
		//				if (!(hint is null))
		//				{
		//					accumulator.Add(hint);
		//				}
		//			}
		//		}
		//
		//		// Check nodes that must be off in both case.
		//		foreach (var pFromOn in onToOff)
		//		{
		//			if (offToOff.Contains(pFromOn))
		//			{
		//				var hint = CreateChainingOffHint(pFromOn, pFromOn, pOff, pFromOn, false);
		//				if (!(hint is null))
		//				{
		//					accumulator.Add(hint);
		//				}
		//			}
		//		}
		//	}
		//}

		private void DoRegionChaining(
			IBag<ChainingTechniqueInfo> accumulator, IReadOnlyGrid grid, int cell, int digit,
			Set<Node> onToOn, Set<Node> onToOff)
		{
			for (var label = Block; label <= Column; label++)
			{
				int region = GetRegion(cell, label);
				var worthMap = CandMaps[digit] & RegionMaps[region];
				if (worthMap.Count == 2 || _multiple && worthMap.Count > 2)
				{
					// Determine whether we meet this region for the first time.
					if (worthMap.SetAt(0) == cell)
					{
						var posToOn = new Dictionary<int, Set<Node>>();
						var posToOff = new Dictionary<int, Set<Node>>();
						Set<Node> regionToOn = new Set<Node>(), regionToOff = new Set<Node>();

						// Iterate on node positions within the region.
						foreach (int otherCell in worthMap)
						{
							if (otherCell == cell)
							{
								posToOn.Add(otherCell, onToOn);
								posToOff.Add(otherCell, onToOff);
								regionToOn |= onToOn;
								regionToOff |= onToOff;
							}
							else
							{
								var other = new Node(otherCell, digit, true);
								Set<Node> otherToOn = new Set<Node> { other }, otherToOff = new Set<Node>();

								DoChaining(grid, otherToOn, otherToOff);

								posToOn.Add(otherCell, otherToOn);
								posToOff.Add(otherCell, otherToOff);
								regionToOn &= otherToOn;
								regionToOff &= otherToOff;
							}
						}

						// Gather results.
						foreach (var p in regionToOn)
						{
							var hint = CreateRegionEliminationHint(region, digit, p, posToOn);
							if (!(hint is null))
							{
								accumulator.Add(hint);
							}
						}
						foreach (var p in regionToOff)
						{
							var hint = CreateRegionEliminationHint(region, digit, p, posToOff);
							if (!(hint is null))
							{
								accumulator.Add(hint);
							}
						}
					}
				}
			}
		}

		private Node[]? DoChaining(IReadOnlyGrid grid, ISet<Node> toOn, ISet<Node> toOff)
		{
			//_tempGrid = grid.Clone();

			var pendingOn = new Set<Node>(toOn);
			var pendingOff = new Set<Node>(toOff);
			while (pendingOn.Count != 0 || pendingOff.Count != 0)
			{
				if (pendingOn.Count != 0)
				{
					var p = pendingOn.Remove();

					var makeOff = GetOnToOff(grid, p, !_nishio);
					foreach (var pOff in makeOff)
					{
						var pOn = new Node(pOff._cell, pOff.Digit, true); // Conjugate
						if (toOn.Contains(pOn))
						{
							// Contradiction found.
							return new[] { pOn, pOff }; // Cannot be both on and off at the same time.
						}
						else if (!toOff.Contains(pOff))
						{
							// Not processed yet.
							toOff.Add(pOff);
							pendingOff.Add(pOff);
						}
					}
				}
				else
				{
					var p = pendingOff.Remove();

					var makeOn = GetOffToOn(grid, p/*, _tempGrid, toOff*/, !_nishio, true);
					//if (_dynamic)
					//{
					//	p.Off(grid);
					//}

					foreach (var pOn in makeOn)
					{
						var pOff = new Node(pOn._cell, pOn.Digit, false); // Conjugate.
						if (toOff.Contains(pOff))
						{
							// Contradiction found.
							return new[] { pOn, pOff }; // Cannot be both on and off at the same time.
						}
						else if (!toOn.Contains(pOn))
						{
							// Not processed yet.
							toOn.Add(pOn);
							pendingOn.Add(pOn);
						}
					}
				}

				//if (pendingOn.Count != 0 && pendingOff.Count != 0 && _level > 0)
				//{
				//	foreach (var pOff in GetAdvanceedNodes(grid, _tempGrid, toOff))
				//	{
				//		if (!toOff.Contains(pOff))
				//		{
				//			// Not processed yet.
				//			toOff.Add(pOff);
				//			pendingOff.Add(pOff);
				//		}
				//	}
				//}
			}

			// Recover.
			//grid = _tempGrid;

			return null;
		}

		//private ChainingTechniqueInfo? CreateChainingOnHint(
		//	Node destOn, Node destOff, Node source, Node target, bool isAbsurd) =>
		//	new BinaryChainingTechniqueInfo(
		//		conclusions: new List<Conclusion> { new Conclusion(Assignment, target._cell, target.Digit) },
		//		views: new[]
		//		{
		//			new View(
		//				cellOffsets: null,
		//				candidateOffsets: null,
		//				regionOffsets: null,
		//				links: null)
		//		},
		//		source,
		//		destOn,
		//		destOff,
		//		isAbsurd,
		//		isNishio: _nishio);

		//private ChainingTechniqueInfo? CreateChainingOffHint(
		//	Node destOn, Node destOff, Node source, Node target, bool isAbsurd) =>
		//	new BinaryChainingTechniqueInfo(
		//		conclusions: new List<Conclusion> { new Conclusion(Elimination, target._cell, target.Digit) },
		//		views: new[]
		//		{
		//			new View(
		//				cellOffsets: null,
		//				candidateOffsets: null,
		//				regionOffsets: null,
		//				links: null)
		//		},
		//		source,
		//		destOn,
		//		destOff,
		//		isAbsurd,
		//		isNishio: _nishio);

		private ChainingTechniqueInfo? CreateCellEliminationHint(
			IReadOnlyGrid grid, int sourceCell, Node target, IReadOnlyDictionary<int, Set<Node>> outcomes)
		{
			// Build removable nodes.
			var conclusions = new List<Conclusion>
			{
				new Conclusion(target.IsOn ? Assignment : Elimination, target._cell, target.Digit)
			};

			// Build chains.
			var chains = new Dictionary<int, Node>();
			foreach (int digit in grid.GetCandidates(sourceCell))
			{
				//var valueTarget = outcomes[digit][target];
				//chains.Add(digit, valueTarget);
				chains.Add(digit, target);
			}

			var candidateOffsets = new List<(int, int)>();
			foreach (var node in chains.Values)
			{
				candidateOffsets.AddRange(GetCandidateOffsets(node));
			}

			var links = new List<Link>();
			foreach (var node in chains.Values)
			{
				links.AddRange(GetLinks(node));
			}

			return new CellChainingTechniqueInfo(
				conclusions,
				views: new[]
				{
					new View(
						cellOffsets: new[] { (0, sourceCell) },
						candidateOffsets,
						regionOffsets: null,
						links)
				},
				sourceCell,
				chains);
		}

		private ChainingTechniqueInfo? CreateRegionEliminationHint(
			int region, int digit, Node target, IDictionary<int, Set<Node>> outcomes)
		{
			// Build removable nodes.
			var conclusions = new List<Conclusion>
			{
				new Conclusion(target.IsOn ? Assignment : Elimination, target._cell, target.Digit)
			};

			// Build chains.
			var chains = new Dictionary<int, Node>();
			var map = RegionMaps[region] & CandMaps[digit];
			foreach (int cell in map)
			{
				//var posTarget = outcomes[cell][target];
				//chains.Add(cell, posTarget);
				chains.Add(cell, target);
			}

			var candidateOffsets = new List<(int, int)>();
			foreach (var node in chains.Values)
			{
				candidateOffsets.AddRange(GetCandidateOffsets(node));
			}

			var links = new List<Link>();
			foreach (var node in chains.Values)
			{
				links.AddRange(GetLinks(node));
			}

			return new RegionChainingTechniqueInfo(
				conclusions,
				views: new[]
				{
					new View(
						cellOffsets: null,
						candidateOffsets,
						regionOffsets: new[] { (0, region) },
						links)
				},
				region,
				digit,
				chains);
		}
	}
}
