﻿using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Sudoku.DocComments;
using Sudoku.Techniques;

namespace Sudoku.Windows.CustomControls
{
	/// <summary>
	/// Interaction logic for <c>TechniqueBox.xaml</c>.
	/// </summary>
	public partial class TechniqueBox : UserControl
	{
		/// <inheritdoc cref="DefaultConstructor"/>
		public TechniqueBox() => InitializeComponent();


		/// <summary>
		/// Indicates the category.
		/// </summary>
		public string Category { get; set; } = string.Empty;

		/// <summary>
		/// Indicates the technique.
		/// </summary>
		public KeyedTuple<string, Technique>? Technique { get; set; }


		/// <summary>
		/// Indicates whether the check box changed the status.
		/// </summary>
		public event EventHandler? CheckingChanged;


		/// <inheritdoc cref="Events.Click(object?, EventArgs)"/>
		private void CheckBox_Click(object sender, RoutedEventArgs e) =>
			CheckingChanged?.Invoke(sender, EventArgs.Empty);
	}
}
