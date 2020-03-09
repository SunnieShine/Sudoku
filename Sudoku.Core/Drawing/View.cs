﻿using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Sudoku.Data;
using Sudoku.Data.Extensions;

namespace Sudoku.Drawing
{
	/// <summary>
	/// Encapsulates a view when displaying the information on forms.
	/// </summary>
	[DebuggerStepThrough]
	public sealed class View
	{
		/// <summary>
		/// Initializes an instance with information.
		/// </summary>
		/// <param name="cellOffsets">
		/// The list of pairs of identifier and cell offset.
		/// </param>
		/// <param name="candidateOffsets">
		/// The list of pairs of identifier and candidate offset.
		/// </param>
		/// <param name="regionOffsets">
		/// The list of pairs of identifier and region offset.
		/// </param>
		/// <param name="linkMasks">The list of link masks.</param>
		public View(
			IReadOnlyList<(int, int)>? cellOffsets, IReadOnlyList<(int, int)>? candidateOffsets,
			IReadOnlyList<(int, int)>? regionOffsets, IReadOnlyList<Inference>? linkMasks) =>
			(CellOffsets, CandidateOffsets, RegionOffsets, LinkMasks) = (cellOffsets, candidateOffsets, regionOffsets, linkMasks);


		/// <summary>
		/// All cell offsets.
		/// </summary>
		/// <remarks>
		/// This property is a list of pairs of identifier and cell offsets,
		/// where the identifier is an <see cref="int"/> value that can tell
		/// all cell offsets' colors.
		/// </remarks>
		public IReadOnlyList<(int _id, int _cellOffset)>? CellOffsets { get; }

		/// <summary>
		/// All candidate offsets.
		/// </summary>
		/// <remarks>
		/// This property is a list of pairs of identifier and candidate offsets,
		/// where the identifier is an <see cref="int"/> value that can tell
		/// all cell offsets' colors.
		/// </remarks>
		public IReadOnlyList<(int _id, int _candidateOffset)>? CandidateOffsets { get; }

		/// <summary>
		/// All region offsets.
		/// </summary>
		/// <remarks>
		/// This property is a list of pairs of identifier and region offsets,
		/// where the identifier is an <see cref="int"/> value that can tell
		/// all cell offsets' colors.
		/// </remarks>
		public IReadOnlyList<(int _id, int _regionOffset)>? RegionOffsets { get; }

		/// <summary>
		/// All link masks.
		/// </summary>
		public IReadOnlyList<Inference>? LinkMasks { get; }

		/// <inheritdoc/>
		public override string ToString()
		{
			const string separator = ", ";
			var sb = new StringBuilder();

			if (!(CellOffsets is null))
			{
				sb.AppendLine("Cells:");
				sb.Append("    ");
				foreach (var (_, cellOffset) in CellOffsets)
				{
					sb.Append($"r{cellOffset / 9 + 1}c{cellOffset % 9 + 1}{separator}");
				}
				sb.RemoveFromEnd(separator.Length);
				sb.AppendLine();
			}
			if (!(CandidateOffsets is null))
			{
				sb.AppendLine("Candidates:");
				sb.Append("    ");
				foreach (var (_, candidateOffset) in CandidateOffsets)
				{
					sb.Append($"r{candidateOffset / 81 + 1}c{candidateOffset % 81 / 9 + 1}({candidateOffset % 9 + 1}){separator}");
				}
				sb.RemoveFromEnd(separator.Length);
				sb.AppendLine();
			}
			if (!(RegionOffsets is null))
			{
				sb.AppendLine("Regions:");
				sb.Append("    ");
				foreach (var (_, regionOffset) in RegionOffsets)
				{
					sb.Append($@"{(regionOffset / 9) switch
					{
						0 => "b",
						1 => "r",
						2 => "c",
						_ => throw Throwing.ImpossibleCase
					}}{regionOffset % 9}{separator}");
				}
				sb.RemoveFromEnd(separator.Length);
				sb.AppendLine();
			}
			if (!(LinkMasks is null))
			{
				sb.AppendLine("Links:");
				sb.Append("    ");
				foreach (var (startCell, startDigit, startIsOn, endCell, endDigit, endIsOn) in LinkMasks)
				{
					string startCondition = startIsOn ? string.Empty : "!";
					string endCondition = endIsOn ? string.Empty : "!";
					int startOutputRow = startCell / 9 + 1;
					int startOutputColumn = startCell % 9 + 1;
					int startOutputDigit = startDigit + 1;
					int endOutputRow = endCell / 9 + 1;
					int endOutputColumn = endCell % 9 + 1;
					int endOutputDigit = endDigit + 1;
					sb.Append($"{startCondition}r{startOutputRow}{startOutputColumn}({startOutputDigit}) -> ");
					sb.Append($"{endCondition}r{endOutputRow}{endOutputColumn}({endOutputDigit}){separator}");
				}
				sb.RemoveFromEnd(separator.Length);
				sb.AppendLine();
			}

			return sb.ToString();
		}
	}
}
