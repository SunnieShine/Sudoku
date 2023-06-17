namespace SudokuStudio.Views.Pages.Operation;

/// <summary>
/// Indicates the generating operation command bar.
/// </summary>
public sealed partial class GeneratingOperation : Page, IOperationProviderPage
{
	/// <summary>
	/// Initializes a <see cref="GeneratingOperation"/> instance.
	/// </summary>
	public GeneratingOperation()
	{
		InitializeComponent();
		SetMemoryOptions();
	}


	/// <inheritdoc/>
	public AnalyzePage BasePage { get; set; } = null!;


	/// <summary>
	/// Update control selection via user configuration.
	/// </summary>
	private void SetMemoryOptions()
	{
		//
		// DifficultyLevelSelector
		//
		var uiPref = ((App)Application.Current).Preference.UIPreferences;
		var flag = false;
		for (var (i, items, target) = (0, DifficultyLevelSelector.Items, uiPref.GeneratorDifficultyLevel);
			i < items.Count;
			i++)
		{
			if (items[i] is ComboBoxItem { Tag: int rawValue } && (DifficultyLevel)rawValue == target)
			{
				(DifficultyLevelSelector.SelectedIndex, flag) = (i, true);
				break;
			}
		}
		if (!flag)
		{
			DifficultyLevelSelector.SelectedIndex = 0;
		}

		//
		// PuzzleSymmetricPatternSelector
		//
		for ((var i, var items, var target, flag) = (0, PuzzleSymmetricPatternSelector.Items, uiPref.GeneratorSymmetricPattern, false);
			i < items.Count;
			i++)
		{
			if (items[i] is ComboBoxItem { Tag: int rawValue } && (SymmetricType)rawValue == target)
			{
				(PuzzleSymmetricPatternSelector.SelectedIndex, flag) = (i, true);
				break;
			}
		}
		if (!flag)
		{
			PuzzleSymmetricPatternSelector.SelectedIndex = 0;
		}

		//
		// GenerateForMinimalPuzzleToggleSwitch
		//
		GenerateForMinimalPuzzleToggleSwitch.IsOn = uiPref.GeneratedPuzzleShouldBeMinimal;

		//
		// PuzzleTechniqueSelector
		//
		PuzzleTechniqueSelector.SelectedValue = uiPref.SelectedTechnique;
	}


	private async void NewPuzzleButton_ClickAsync(object sender, RoutedEventArgs e)
	{
		BasePage.IsGeneratorLaunched = true;

		BasePage.ClearAnalyzeTabsData();

		var processingText = GetString("AnalyzePage_GeneratorIsProcessing")!;
		var preferences = ((App)Application.Current).Preference.UIPreferences;
		var difficultyLevel = preferences.GeneratorDifficultyLevel;
		var symmetry = preferences.GeneratorSymmetricPattern;
		var minimal = preferences.GeneratedPuzzleShouldBeMinimal;
		var technique = preferences.SelectedTechnique;
		var grid = await Task.Run(() => gridCreator(new(difficultyLevel, symmetry, minimal, technique)));

		BasePage.IsGeneratorLaunched = false;

		if (((App)Application.Current).Preference.UIPreferences.SavePuzzleGeneratingHistory)
		{
			((App)Application.Current).PuzzleGeneratingHistory.Puzzles.Add(new() { BaseGrid = grid });
		}

		BasePage.SudokuPane.Puzzle = grid;


		Grid gridCreator(GeneratingDetails details)
		{
			var (progress, (difficultyLevel, symmetry, minimal, technique)) = (new SelfReportingProgress<GeneratorProgress>(reportingAction), details);
			for (var count = 0; ; count++)
			{
				if (HodokuPuzzleGenerator.Generate(symmetry) is var grid
					&& ((App)Application.Current).Analyzer.Analyze(grid) is { DifficultyLevel: var puzzleDifficultyLevel } analyzerResult
					&& (difficultyLevel == 0 || puzzleDifficultyLevel == difficultyLevel)
					&& (minimal && grid.IsMinimal || !minimal)
					&& (technique != 0 && analyzerResult.HasTechnique(technique) || technique == 0))
				{
					return grid;
				}

				progress.Report(new(count));
			}
		}

		void reportingAction(GeneratorProgress progress)
		{
			DispatcherQueue.TryEnqueue(progressCallback);


			void progressCallback()
			{
				var count = progress.Count;
				BasePage.AnalyzeProgressLabel.Text = processingText;
				BasePage.AnalyzeStepSearcherNameLabel.Text = count.ToString();
			}
		}
	}

	private void DifficultyLevelSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
	{
		if (sender is ComboBox { SelectedItem: ComboBoxItem { Tag: int value } })
		{
			((App)Application.Current).Preference.UIPreferences.GeneratorDifficultyLevel = (DifficultyLevel)value;
		}
	}

	private void PuzzleSymmetricPatternSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
	{
		if (sender is ComboBox { SelectedItem: ComboBoxItem { Tag: int value } })
		{
			((App)Application.Current).Preference.UIPreferences.GeneratorSymmetricPattern = (SymmetricType)value;
		}
	}

	private void GenerateForMinimalPuzzleToggleSwitch_Toggled(object sender, RoutedEventArgs e)
	{
		if (sender is ToggleSwitch { IsOn: var value })
		{
			((App)Application.Current).Preference.UIPreferences.GeneratedPuzzleShouldBeMinimal = value;
		}
	}

	private void PuzzleTechniqueSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
	{
		if (sender is TechniqueSelector { SelectedValue: Technique value })
		{
			((App)Application.Current).Preference.UIPreferences.SelectedTechnique = value;
		}
	}
}

/// <summary>
/// Defines a self-reporting progress type.
/// </summary>
/// <param name="handler"><inheritdoc cref="Progress{T}.Progress(Action{T})" path="/param[@name='handler']"/></param>
/// <typeparam name="T"><inheritdoc cref="Progress{T}" path="/typeparam[@name='T']"/></typeparam>
file sealed class SelfReportingProgress<T>(Action<T> handler) : Progress<T>(handler)
{
	/// <inheritdoc cref="Progress{T}.OnReport(T)"/>
	public void Report(T value) => OnReport(value);
}

/// <summary>
/// The encapsulated type to describe the details for generating puzzles.
/// </summary>
/// <param name="DifficultyLevel">Indicates the difficulty level selected.</param>
/// <param name="SymmetricPattern">Indicates the symmetric pattern selected.</param>
/// <param name="ShouldBeMinimal">Indicates whether generated puzzles should be minimal.</param>
/// <param name="SelectedTechnique">Indicates the selected technique that you want it to be appeared in generated puzzles.</param>
file readonly record struct GeneratingDetails(
	DifficultyLevel DifficultyLevel,
	SymmetricType SymmetricPattern,
	bool ShouldBeMinimal,
	Technique SelectedTechnique
);
