namespace SudokuStudio.Views.Windows;

/// <summary>
/// Provides with a <see cref="Window"/> instance that is running as main instance of the program.
/// </summary>
/// <seealso cref="Window"/>
public sealed partial class MainWindow : Window
{
	/// <summary>
	/// The default navigation transition instance that will create animation fallback while switching pages.
	/// </summary>
	private static readonly NavigationTransitionInfo NavigationTransitionInfo = new EntranceNavigationTransitionInfo();


	/// <summary>
	/// Initializes a <see cref="MainWindow"/> instance.
	/// </summary>
	public MainWindow()
	{
		InitializeComponent();

#if MICA_BACKDROP
		TrySetMicaBackdrop();
#endif
#if CUSTOMIZED_TITLE_BAR
		InitializeAppWindow();
#endif
		SetAppIcon();
		SetAppTitle();
	}


	/// <summary>
	/// Try to navigate to the target page.
	/// </summary>
	/// <param name="pageType">The target page type.</param>
	private void NavigateToPage(Type? pageType)
	{
		if (pageType is not null && NavigationViewFrame.SourcePageType != pageType)
		{
			NavigationViewFrame.Navigate(pageType, null, NavigationTransitionInfo);
			MainNavigationView.Header = GetStringNullable($"{nameof(MainWindow)}_{pageType.Name}{nameof(Title)}") ?? string.Empty;
		}
	}

	/// <summary>
	/// Try to set icon of the program.
	/// </summary>
	private void SetAppIcon() => _appWindow.SetIcon(@"Resources\images\Logo.ico");

	/// <summary>
	/// Try to set program name onto the title.
	/// </summary>
	private void SetAppTitle() => _appWindow.Title = GetString("_ProgramName");


	private void NavigationView_Loaded(object sender, RoutedEventArgs e) => NavigateToPage(typeof(AnalyzePage));

	private void NavigationView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
		=> NavigateToPage(
			args switch
			{
				{ IsSettingsInvoked: true } => typeof(SettingsPage),
				{ InvokedItemContainer: var container } when container == AnalyzePageItem => typeof(AnalyzePage),
				_ => null
			}
		);
}
