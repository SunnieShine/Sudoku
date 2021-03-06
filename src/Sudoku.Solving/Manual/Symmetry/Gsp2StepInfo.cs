﻿using System.Collections.Generic;
using System.Text;
using Sudoku.Data;
using Sudoku.Data.Collections;
using Sudoku.Data.Extensions;
using Sudoku.Drawing;
using Sudoku.Techniques;

namespace Sudoku.Solving.Manual.Symmetry
{
	/// <summary>
	/// Provides a usage of <b>Gurth's symmetrical placement 2</b> (GSP2) technique.
	/// </summary>
	/// <param name="Conclusions">All conclusions.</param>
	/// <param name="Views">All views.</param>
	/// <param name="SymmetryType">The symmetry type used.</param>
	/// <param name="SwappingTable">Indicates the swapping table.</param>
	/// <param name="MappingTable">
	/// The mapping table. The value is always not <see langword="null"/> unless the current instance
	/// contains multiple different symmetry types.
	/// </param>
	public sealed record Gsp2StepInfo(
		IReadOnlyList<Conclusion> Conclusions, IReadOnlyList<View> Views, SymmetryType SymmetryType,
		int[]?[]? SwappingTable, int?[]? MappingTable) : SymmetryStepInfo(Conclusions, Views)
	{
		/// <inheritdoc/>
		public override decimal Difficulty => 8.5M;

		/// <inheritdoc/>
		public override DifficultyLevel DifficultyLevel => DifficultyLevel.Nightmare;

		/// <inheritdoc/>
		public override Technique TechniqueCode => Technique.Gsp2;


		/// <inheritdoc/>
		public override string ToString()
		{
			string elimStr = new ConclusionCollection(Conclusions).ToString();
			return $"{Name}: {getSwappedString()}{getStringThatDescribesGspInfo()}) => {elimStr}";

			string getSwappedString()
			{
				if (SwappingTable is { Length: not 0 })
				{
					const string separator = ", ";
					var sb = new ValueStringBuilder(stackalloc char[100]);
					sb.Append("We should swap region(s) ");

					foreach (int[]? swappingRegionPair in SwappingTable)
					{
						if (swappingRegionPair is not null)
						{
							sb.Append(new RegionCollection(swappingRegionPair[0]).ToString());
							sb.Append(" with ");
							sb.Append(new RegionCollection(swappingRegionPair[1]).ToString());
							sb.Append(separator);
						}
					}

					sb.Append("then we'll get the symmetrical placement (");

					return sb.ToString();
				}
				else
				{
					return string.Empty;
				}
			}

			string getStringThatDescribesGspInfo()
			{
				const string separator = ", ";
				string customName = SymmetryType.GetName().ToLower();
				if (MappingTable is not null)
				{
					var sb = new ValueStringBuilder(stackalloc char[50]);
					for (int i = 0; i < 9; i++)
					{
						int? value = MappingTable[i];

						sb.Append(i + 1);
						sb.Append(
							value.HasValue && value != i ? $" -> {(value.Value + 1).ToString()}" : string.Empty
						);
						sb.Append(separator);
					}

					sb.RemoveFromEnd(separator.Length);
					string mapping = sb.ToString();
					return $"Symmetry type: {customName}, mapping relations: {mapping}";
				}
				else
				{
					return $"Symmetry type: {customName}";
				}
			}
		}
	}
}
