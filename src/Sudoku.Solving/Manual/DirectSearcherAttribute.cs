﻿using System;
using System.Collections.Generic;
using Sudoku.Data;

namespace Sudoku.Solving.Manual
{
	/// <summary>
	/// <para>
	/// Indicates the technique searcher can or can't be used in some scenarios
	/// where they aren't in traversing mode to call
	/// <see cref="StepSearcher.GetAll(IList{StepInfo}, in SudokuGrid)"/>
	/// in <see cref="StepSearcher"/>s one by one.
	/// </para>
	/// <para>
	/// If <see langword="true"/>, the searcher can't use those <see langword="static"/>
	/// properties such as <see cref="FastProperties.CandMaps"/> in its method
	/// <see cref="StepSearcher.GetAll(IList{StepInfo}, in SudokuGrid)"/>.
	/// </para>
	/// </summary>
	/// <remarks>
	/// <para>
	/// All disallowed properties are:
	/// <list type="bullet">
	/// <item><see cref="FastProperties.DigitMaps"/></item>
	/// <item><see cref="FastProperties.ValueMaps"/></item>
	/// <item><see cref="FastProperties.CandMaps"/></item>
	/// <item><see cref="FastProperties.BivalueMap"/></item>
	/// <item><see cref="FastProperties.EmptyMap"/></item>
	/// </list>
	/// The disallowed method is:
	/// <list type="bullet">
	/// <item><see cref="FastProperties.InitializeMaps(in SudokuGrid)"/></item>
	/// </list>
	/// </para>
	/// <para>
	/// Those properties or methods can optimize the performance to analyze a sudoku grid, but
	/// sometimes they may cause a potential bug that is hard to find and fix. The attribute
	/// is created and used for solving the problem.
	/// </para>
	/// </remarks>
	/// <seealso cref="StepSearcher"/>
	/// <seealso cref="FastProperties"/>
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class DirectSearcherAttribute : Attribute
	{
		/// <summary>
		/// Indicates the ability that can or can't be used in non-traversing scenarios.
		/// The default value is <see langword="true"/>.
		/// </summary>
		public bool IsAllow { get; init; } = true;
	}
}
