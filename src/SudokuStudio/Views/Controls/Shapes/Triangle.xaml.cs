namespace SudokuStudio.Views.Controls.Shapes;

/// <summary>
/// Represents a triangle control.
/// </summary>
public sealed partial class Triangle : UserControl
{
	[Default]
	private static readonly double StrokeThicknessDefaultValue = 6;


	/// <summary>
	/// Initializes a <see cref="Triangle"/> instance.
	/// </summary>
	public Triangle() => InitializeComponent();


	/// <summary>
	/// Indicates the stroke thickness for the star.
	/// </summary>
	[DependencyProperty]
	public partial double StrokeThickness { get; set; }


	private void ParentViewBox_SizeChanged(object sender, SizeChangedEventArgs e)
		=> PathPresenter.StrokeThickness = StrokeThicknessDefaultValue * 20 / ParentViewBox.ActualWidth;


	[Callback]
	private static void StrokeThicknessPropertyCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is not Path { Parent: Viewbox { ActualWidth: var aw } } pathControl)
		{
			return;
		}

		pathControl.StrokeThickness = (double)e.NewValue * 20 / aw;
	}
}
