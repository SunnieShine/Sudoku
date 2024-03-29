namespace SudokuStudio.Views.Pages.Settings;

/// <summary>
/// Represents a settings page that displays for basic preferences.
/// </summary>
public sealed partial class BasicPreferenceItemsPage : Page
{
	/// <summary>
	/// Initializes a <see cref="BasicPreferenceItemsPage"/> instance.
	/// </summary>
	public BasicPreferenceItemsPage()
	{
		InitializeComponent();
		InitializeControls();
	}


	/// <summary>
	/// Initializes for control properties.
	/// </summary>
	private void InitializeControls()
	{
		var uiPref = ((App)Application.Current).Preference.UIPreferences;
		var isChinese = CultureInfo.CurrentUICulture.Name.Contains("zh");
		LanguageComboBox.SelectedIndex = uiPref.Language switch { 0 => 0, 1033 => 1, 2052 => 2 };
		Comma2ComboBoxItem_DefaultSeparator.Visibility = isChinese ? Visibility.Visible : Visibility.Collapsed;
		Comma2ComboBoxItem_DigitSeparator.Visibility = isChinese ? Visibility.Visible : Visibility.Collapsed;
		ThemeComboBox.SelectedIndex = (int)uiPref.CurrentTheme;
	}

	private void BackdropSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
	{
		if (sender is Segmented { SelectedItem: SegmentedItem { Tag: string s } } && Enum.TryParse<BackdropKind>(s, out var value))
		{
			((App)Application.Current).Preference.UIPreferences.Backdrop = value;
		}
	}

	private void HouseCompletedFeedbackColorSelector_ColorChanged(object sender, Color e)
		=> ((App)Application.Current).Preference.UIPreferences.HouseCompletedFeedbackColor = e;

	private void ConceptNotationModeSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
	{
		if (sender is Segmented { SelectedItem: SegmentedItem { Tag: int rawValue } })
		{
			((App)Application.Current).Preference.UIPreferences.ConceptNotationBasedKind = (CoordinateType)rawValue;
		}
	}

	private void NotationDefaultSeparatorSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
	{
		if (sender is Segmented { SelectedItem: SegmentedItem { Tag: string s } })
		{
			((App)Application.Current).Preference.UIPreferences.DefaultSeparatorInNotation = s;
		}
	}

	private void NotationDigitSeparatorSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
	{
		if (sender is Segmented { SelectedItem: SegmentedItem { Tag: string s } })
		{
			((App)Application.Current).Preference.UIPreferences.DefaultSeparatorInNotation = s;
		}
	}

	private void FinalRowLetterInK9NotationSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
	{
		if (sender is Segmented { SelectedItem: SegmentedItem { Tag: string and [var ch] } })
		{
			((App)Application.Current).Preference.UIPreferences.FinalRowLetterInK9Notation = ch;
		}
	}

	private void LanguageComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		=> ((App)Application.Current).Preference.UIPreferences.Language = (int)((SegmentedItem)LanguageComboBox.SelectedItem).Tag!;

	private void ThemeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
	{
		var theme = (Theme)((SegmentedItem)ThemeComboBox.SelectedItem).Tag!;
		((App)Application.Current).Preference.UIPreferences.CurrentTheme = theme;

		// Manually set theme.
		foreach (var window in ((App)Application.Current).WindowManager.ActiveWindows)
		{
			if (window is MainWindow instance)
			{
				instance.ManuallySetTitleBarButtonsColor(theme);
			}

			if (window.Content is FrameworkElement control)
			{
				control.RequestedTheme = theme switch
				{
					Theme.Default => ElementTheme.Default,
					Theme.Light => ElementTheme.Light,
					_ => ElementTheme.Dark
				};
			}
		}
	}

	private void PlaceholderTextSegmented_SelectionChanged(object sender, SelectionChangedEventArgs e)
	{
		if (sender is Segmented { SelectedItem: SegmentedItem { Tag: string and [var ch] } })
		{
			((App)Application.Current).Preference.UIPreferences.EmptyCellCharacter = ch;
		}
	}
}
