using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using SudokuStudio.ComponentModel;
using Path = Microsoft.UI.Xaml.Shapes.Path;

namespace SudokuStudio.Views.Controls.Shapes;

/// <summary>
/// Represents a star mark.
/// </summary>
[DependencyProperty<double>("StrokeThickness", DocSummary = "Indicates the stroke thickness for the star.")]
public sealed partial class Star : UserControl
{
	[Default]
	private static readonly double StrokeThicknessDefaultValue = 6;


	/// <summary>
	/// Initializes a <see cref="Star"/> instance.
	/// </summary>
	public Star() => InitializeComponent();


	private void ParentViewBox_SizeChanged(object sender, SizeChangedEventArgs e)
		=> PathPresenter.StrokeThickness = StrokeThicknessDefaultValue * 250 / ParentViewBox.ActualWidth;


	[Callback]
	private static void StrokeThicknessPropertyCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is not Path { Parent: Viewbox { ActualWidth: var aw } } pathControl)
		{
			return;
		}

		pathControl.StrokeThickness = (double)e.NewValue * 250 / aw;
	}
}
