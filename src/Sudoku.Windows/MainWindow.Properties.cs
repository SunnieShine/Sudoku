﻿using System;
using System.Windows;
using Sudoku.Data.Stepping;

namespace Sudoku.Windows
{
	partial class MainWindow
	{
		/// <summary>
		/// Indicates the settings used.
		/// </summary>
		public WindowsSettings Settings { get; private set; } = null!;

		/// <summary>
		/// Indicates the puzzle, which is equivalent to <see cref="_puzzle"/>,
		/// but add the auto-update value layer.
		/// </summary>
		/// <remarks>This property is an <see langword="set"/>-only method in fact.</remarks>
		/// <value>The new grid.</value>
		/// <seealso cref="_puzzle"/>
		private UndoableGrid Puzzle
		{
			set
			{
				_puzzle = value;
				_currentPainter = new(_pointConverter, Settings, value);
				_initialPuzzle = value.InnerGrid;

				GC.Collect();
			}
		}


		/// <summary>
		/// The language source dictionary.
		/// </summary>
		internal static ResourceDictionary LangSource => Application.Current.Resources;
	}
}
