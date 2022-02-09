﻿using Microsoft.UI.Xaml.Shapes;
using Windows.Foundation;

namespace Sudoku.UI.Views.Controls;

/// <summary>
/// Defines a user control that interacts with a sudoku grid.
/// </summary>
public sealed partial class SudokuPane : UserControl
{
	private double _size;
	private double _outsideOffset;
	private Grid _grid;


	/// <summary>
	/// Initializes a <see cref="SudokuPane"/> instance.
	/// </summary>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public SudokuPane() => InitializeComponent();


	/// <summary>
	/// Gets or sets the size of the pane to the view model.
	/// </summary>
	public double Size
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		get => _size;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		set
		{
			if (!_size.NearlyEquals(value))
			{
				_size = value;

				UpdateBorderLines();
			}
		}
	}

	/// <summary>
	/// Gets or sets the outside offset to the view model.
	/// </summary>
	public double OutsideOffset
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		get => _outsideOffset;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		set
		{
			if (!_outsideOffset.NearlyEquals(value))
			{
				_outsideOffset = value;

				UpdateBorderLines();
			}
		}
	}

	/// <summary>
	/// Gets or sets the current grid used.
	/// </summary>
	public Grid Grid
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		get => _grid;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		set
		{
			if (_grid != value)
			{
				_grid = value;

				UpdateGrid();
			}
		}
	}


	/// <summary>
	/// Update the grid info.
	/// </summary>
	private void UpdateGrid()
	{
	}

	/// <summary>
	/// Update the border lines.
	/// </summary>
	private void UpdateBorderLines()
	{
		foreach (var control in
			from control in _cCanvasMain.Children.OfType<Line>()
			where control.Tag is string s && s.Contains(SudokuCanvasTags.BorderLines)
			select control)
		{
			string tag = (string)control.Tag!;
			int i = int.Parse(tag.Split('|')[^1]);
			int weight = tag.Contains(SudokuCanvasTags.BlockBorderLines) ? 3 : 9;
			if (tag.Contains(SudokuCanvasTags.HorizontalBorderLines))
			{
				var (x1, y1) = HorizontalBorderLinePoint1(i, weight);
				control.X1 = x1;
				control.Y1 = y1;

				var (x2, y2) = HorizontalBorderLinePoint2(i, weight);
				control.X2 = x2;
				control.Y2 = y2;
			}
			else if (tag.Contains(SudokuCanvasTags.VerticalBorderLines))
			{
				var (x1, y1) = VerticalBorderLinePoint1(i, weight);
				control.X1 = x1;
				control.Y1 = y1;

				var (x2, y2) = VerticalBorderLinePoint2(i, weight);
				control.X2 = x2;
				control.Y2 = y2;
			}
		}
	}

	/// <summary>
	/// Gets the first point value of the horizontal border line.
	/// </summary>
	/// <param name="i">The index of the line.</param>
	/// <param name="weight">The weight of the division operation. The value can only be 3, 9 or 27.</param>
	/// <returns>The first point value of the horizontal border line.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private Point HorizontalBorderLinePoint1(int i, int weight) =>
		new(OutsideOffset + i * (Size - 2 * OutsideOffset) / weight, OutsideOffset);

	/// <summary>
	/// Gets the second point value of the horizontal border line.
	/// </summary>
	/// <param name="i">The index of the line.</param>
	/// <param name="weight">The weight of the division operation. The value can only be 3, 9 or 27.</param>
	/// <returns>The second point value of the horizontal border line.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private Point HorizontalBorderLinePoint2(int i, int weight) =>
		new(OutsideOffset + i * (Size - 2 * OutsideOffset) / weight, Size - OutsideOffset);

	/// <summary>
	/// Gets the first point value of the vertical border line.
	/// </summary>
	/// <param name="i">The index of the line.</param>
	/// <param name="weight">The weight of the division operation. The value can only be 3, 9 or 27.</param>
	/// <returns>The first point value of the horizontal border line.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private Point VerticalBorderLinePoint1(int i, int weight) =>
		new(OutsideOffset, OutsideOffset + i * (Size - 2 * OutsideOffset) / weight);

	/// <summary>
	/// Gets the second point value of the vertical border line.
	/// </summary>
	/// <param name="i">The index of the line.</param>
	/// <param name="weight">The weight of the division operation. The value can only be 3, 9 or 27.</param>
	/// <returns>The second point value of the horizontal border line.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private Point VerticalBorderLinePoint2(int i, int weight) =>
		new(Size - OutsideOffset, OutsideOffset + i * (Size - 2 * OutsideOffset) / weight);
}
