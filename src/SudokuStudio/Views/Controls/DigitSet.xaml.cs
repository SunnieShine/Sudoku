namespace SudokuStudio.Views.Controls;

/// <summary>
/// Represents a digit set control.
/// </summary>
[DependencyProperty<Digit>("SelectedDigit", DefaultValue = -1, DocSummary = "Indicates the selected digit.")]
public sealed partial class DigitSet : UserControl
{
	/// <summary>
	/// Indicates the items source.
	/// </summary>
	private readonly Digit[] _itemsSource = [0, 1, 2, 3, 4, 5, 6, 7, 8];


	/// <summary>
	/// Initializes a <see cref="DigitSet"/> instance.
	/// </summary>
	public DigitSet() => InitializeComponent();


	/// <summary>
	/// Indicates the event triggered when the selected digit is changed.
	/// </summary>
	public event DigitSetSelectedDigitChangedEventHandler? SelectedDigitChanged;


	private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
	{
		SelectedDigit = (Digit)((ListView)sender).SelectedItem;
		SelectedDigitChanged?.Invoke(this, new(SelectedDigit));
	}
}
