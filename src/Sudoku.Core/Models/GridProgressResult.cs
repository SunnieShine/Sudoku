﻿using System;
using System.Text;
using Sudoku.DocComments;
using Sudoku.Globalization;
using Sudoku.Resources;

namespace Sudoku.Models
{
	/// <summary>
	/// Encapsulates a progress result used for report the current state.
	/// </summary>
	public struct GridProgressResult : IValueEquatable<GridProgressResult>, IProgressResult
	{
		/// <summary>
		/// Initializes an instance with the specified current point and the total point.
		/// </summary>
		/// <param name="currentCandidatesCount">The current point.</param>
		/// <param name="currentCellsCount">The number of unsolved cells.</param>
		/// <param name="initialCandidatesCount">The number of unsolved candidates in the initial grid.</param>
		/// <param name="countryCode">The country code.</param>
		public GridProgressResult(
			int currentCandidatesCount, int currentCellsCount, int initialCandidatesCount,
			CountryCode countryCode)
		{
			CurrentCandidatesCount = currentCandidatesCount;
			CurrentCellsCount = currentCellsCount;
			InitialCandidatesCount = initialCandidatesCount;
			CountryCode = countryCode == CountryCode.Default ? CountryCode.EnUs : countryCode;
		}


		/// <summary>
		/// Indicates the number of unsolved cells.
		/// </summary>
		public int CurrentCellsCount { get; set; }

		/// <summary>
		/// Indicates the number of unsolved candidates.
		/// </summary>
		public int CurrentCandidatesCount { get; set; }

		/// <summary>
		/// Indicates the number of unsolved candidates in the initial grid.
		/// </summary>
		public int InitialCandidatesCount { get; }

		/// <summary>
		/// The country code.
		/// </summary>
		public CountryCode CountryCode { get; }

		/// <summary>
		/// Indicates the current percentage.
		/// </summary>
		public readonly double Percentage =>
			(double)(InitialCandidatesCount - CurrentCandidatesCount) / InitialCandidatesCount * 100;


		/// <inheritdoc cref="DeconstructMethod"/>
		/// <param name="current">The number of unsolved candidates.</param>
		/// <param name="unsolvedCells">The number of unsolved cells.</param>
		public readonly void Deconstruct(out int current, out int unsolvedCells)
		{
			current = CurrentCandidatesCount;
			unsolvedCells = CurrentCellsCount;
		}

		/// <inheritdoc cref="DeconstructMethod"/>
		/// <param name="currentCandidatesCount">
		/// The number of unsolved candidates.
		/// </param>
		/// <param name="currentCellsCount">
		/// The number of unsolved cells.
		/// </param>
		/// <param name="initialCandidatesCount">
		/// The number of unsolved candidates in the initial grid.
		/// </param>
		public readonly void Deconstruct(
			out int currentCandidatesCount, out int currentCellsCount, out int initialCandidatesCount)
		{
			currentCandidatesCount = CurrentCandidatesCount;
			currentCellsCount = CurrentCellsCount;
			initialCandidatesCount = InitialCandidatesCount;
		}

		/// <inheritdoc cref="object.ToString"/>
		public override readonly string ToString()
		{
			var sb = new ValueStringBuilder(stackalloc char[50]);
			sb.Append((string)TextResources.Current.UnsolvedCells);
			sb.Append(CurrentCellsCount.ToString());
			sb.Append((string)TextResources.Current.UnsolvedCandidates);
			sb.Append(CurrentCandidatesCount.ToString());

			return sb.ToString();
		}

		/// <inheritdoc cref="object.Equals(object?)"/>
		public override readonly bool Equals(object? obj) =>
			obj is GridProgressResult comparer && Equals(comparer);

		/// <inheritdoc/>
		[CLSCompliant(false)]
		public readonly bool Equals(in GridProgressResult other) =>
			CurrentCellsCount == other.CurrentCellsCount
			&& CurrentCandidatesCount == other.CurrentCandidatesCount
			&& InitialCandidatesCount == other.InitialCandidatesCount
			&& CountryCode == other.CountryCode;

		/// <inheritdoc cref="object.GetHashCode"/>
		public override readonly int GetHashCode() =>
			CurrentCellsCount * 729 + CurrentCandidatesCount ^ InitialCandidatesCount
			^ CountryCode.GetHashCode();


		/// <inheritdoc cref="Operators.operator =="/>
		public static bool operator ==(in GridProgressResult left, in GridProgressResult right) =>
			left.Equals(right);

		/// <inheritdoc cref="Operators.operator !="/>
		public static bool operator !=(in GridProgressResult left, in GridProgressResult right) =>
			!(left == right);
	}
}
