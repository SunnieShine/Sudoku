﻿using System.Collections.Generic;
using System.Text;
using Sudoku.Data;
using Sudoku.Data.Collections;
using Sudoku.Drawing;
using Sudoku.Techniques;

namespace Sudoku.Solving.Manual.Exocets
{
	/// <summary>
	/// Provides a usage of <b>exocet</b> technique.
	/// </summary>
	/// <param name="Views">All views.</param>
	/// <param name="Exocet">The exocet.</param>
	/// <param name="Digits">All digits.</param>
	/// <param name="LockedMemberQ">The locked member Q.</param>
	/// <param name="LockedMemberR">The locked member R.</param>
	/// <param name="Eliminations">The eliminations.</param>
	public abstract record ExocetStepInfo(
		IReadOnlyList<View> Views, in Pattern Exocet, IEnumerable<int> Digits,
		IEnumerable<int>? LockedMemberQ, IEnumerable<int>? LockedMemberR,
		IReadOnlyList<Elimination> Eliminations) : StepInfo(GatherConclusions(Eliminations), Views)
	{
		/// <inheritdoc/>
		public sealed override string Name => base.Name;

		/// <inheritdoc/>
		public sealed override bool ShowDifficulty => base.ShowDifficulty;

		/// <inheritdoc/>
		public abstract override decimal Difficulty { get; }

		/// <inheritdoc/>
		public sealed override TechniqueTags TechniqueTags => TechniqueTags.Exocet;

		/// <inheritdoc/>
		public sealed override DifficultyLevel DifficultyLevel => DifficultyLevel.Nightmare;


		/// <inheritdoc/>
		public override string ToString() => ToStringInternal();

		/// <inheritdoc/>
		public sealed override string ToFullString()
		{
			var sb = new ValueStringBuilder(stackalloc char[100]);
			sb.AppendLine(ToString());
			sb.AppendLineRange(Eliminations);

			return sb.ToString();
		}

		/// <summary>
		/// Get the additional message.
		/// </summary>
		/// <returns>The additional message.</returns>
		protected abstract string? GetAdditional();

		/// <summary>
		/// Same as <see cref="ToString"/> but the implementation part.
		/// </summary>
		/// <returns>The result.</returns>
		protected string ToStringInternal()
		{
			var (baseMap, targetMap, _) = Exocet;
			string? addtional = GetAdditional();

			var sb = new ValueStringBuilder(stackalloc char[150]);
			sb.Append(Name);
			sb.Append(": Digits ");
			sb.Append(new DigitCollection(Digits).ToString());
			sb.Append(" in base cells ");
			sb.Append(baseMap.ToString());
			sb.Append(", target cells ");
			sb.Append(targetMap.ToString());
			sb.Append(
				LockedMemberQ is null
				? null
				: $", locked member 1: {new DigitCollection(LockedMemberQ).ToString()}"
			);
			sb.Append(
				LockedMemberR is null
				? null
				: $", locked member 2: {new DigitCollection(LockedMemberR).ToString()}"
			);
			sb.Append(addtional is null ? string.Empty : $" with {addtional}");
			sb.Append(" => ");
			sb.Append(new ConclusionCollection(Conclusions).ToString());

			return sb.ToString();
		}


		/// <summary>
		/// Gather conclusions.
		/// </summary>
		/// <returns>The gathered result.</returns>
		private static IReadOnlyList<Conclusion> GatherConclusions(IReadOnlyList<Elimination> eliminations)
		{
			var result = new List<Conclusion>();
			foreach (var eliminationInstance in eliminations)
			{
				result.AddRange(eliminationInstance.AsSpan().ToArray());
			}

			return result;
		}
	}
}
