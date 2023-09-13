using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Documents;
using Sudoku.Algorithm.Generating;
using Sudoku.Analytics;
using Sudoku.Analytics.Categorization;
using Sudoku.Concepts;
using SudokuStudio.ComponentModel;
using SudokuStudio.Interaction.Conversions;
using SudokuStudio.Views.Attached;

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
		var uiPref = ((App)Application.Current).Preference.UIPreferences;
		var comma = GetString("_Token_Comma2");
		var openBrace = GetString("_Token_OpenBrace");
		var closedBrace = GetString("_Token_ClosedBrace");
		TextBlockBindable.SetInlines(
			GeneratingStrategyDisplayer,
			[
				new Run { Text = GetString("AnalyzePage_GeneratingStrategySelected") },
				new Run { Text = openBrace },
				new Run { Text = GetString("AnalyzePage_PleaseSelectDifficultyLevel") },
				new Run { Text = DifficultyLevelConversion.GetName(uiPref.GeneratorDifficultyLevel) }.SingletonSpan<Bold>(),
				new Run { Text = comma },
				new Run { Text = GetString("AnalyzePage_PleaseSelectSymmetricPattern") },
				new Run
				{
					Text = GetString($"SymmetricType_{((App)Application.Current).Preference.UIPreferences.GeneratorSymmetricPattern}")
				}.SingletonSpan<Bold>(),
				new Run { Text = comma },
				new Run { Text = GetString("AnalyzePage_TechniqueMustAppear") },
				new Run
				{
					Text = uiPref.SelectedTechnique switch
					{
						Technique.None => GetString("TechniqueSelector_NoTechniqueSelected"),
						var t => $"{t.GetName()}{openBrace}{t.GetEnglishName()}{closedBrace}"
					}
				}.SingletonSpan<Bold>(),
				new Run { Text = comma },
				new Run { Text = GetString("AnalyzePage_GenerateForMinimalPuzzle") },
				new Run { Text = uiPref.GeneratedPuzzleShouldBeMinimal ? GetString("Yes") : GetString("No") }.SingletonSpan<Bold>(),
				new Run { Text = comma },
				new Run { Text = GetString("TechniqueSelector_ShouleBePearlPuzzle") },
				new Run
				{
					Text = uiPref.GeneratedPuzzleShouldBePearl switch
					{
						true => GetString("GeneratingStrategyPage_PearlPuzzle"),
						false => GetString("GeneratingStrategyPage_NormalPuzzle"),
						//_ => GetString("GeneratingStrategyPage_DiamondPuzzle")
					}
				}.SingletonSpan<Bold>(),
				new Run { Text = closedBrace }
			]
		);
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
		var pearl = preferences.GeneratedPuzzleShouldBePearl;
		var technique = preferences.SelectedTechnique;
		var analyzer = ((App)Application.Current).Analyzer
			.WithStepSearchers(((App)Application.Current).GetStepSearchers(), difficultyLevel)
			.WithRuntimeIdentifierSetters(BasePage.SudokuPane);
		using var cts = new CancellationTokenSource();
		BasePage._ctsForAnalyzingRelatedOperations = cts;

		try
		{
			var grid = await Task.Run(() => gridCreator(analyzer, new(difficultyLevel, symmetry, minimal, pearl, technique)));
			if (grid.IsUndefined)
			{
				return;
			}

			if (((App)Application.Current).Preference.UIPreferences.SavePuzzleGeneratingHistory)
			{
				((App)Application.Current).PuzzleGeneratingHistory.Puzzles.Add(new() { BaseGrid = grid });
			}

			BasePage.SudokuPane.Puzzle = grid;
		}
		catch (TaskCanceledException)
		{
		}
		finally
		{
			BasePage._ctsForAnalyzingRelatedOperations = null;
			BasePage.IsGeneratorLaunched = false;
		}


		Grid gridCreator(Analyzer analyzer, GeneratingDetails details)
		{
			try
			{
				return generatePuzzleCore(progress => DispatcherQueue.TryEnqueue(() =>
				{
					var count = progress.Count;
					BasePage.AnalyzeProgressLabel.Text = processingText;
					BasePage.AnalyzeStepSearcherNameLabel.Text = count.ToString();
				}), details, cts.Token, analyzer) ?? Grid.Undefined;
			}
			catch (TaskCanceledException)
			{
				return Grid.Undefined;
			}
		}

		static Grid? generatePuzzleCore(
			Action<GeneratorProgress> reportingAction,
			GeneratingDetails details,
			CancellationToken cancellationToken,
			Analyzer analyzer
		)
		{
			try
			{
				for (
					var (count, progress, (difficultyLevel, symmetry, minimal, pearl, technique)) = (
						0,
						new SelfReportingProgress<GeneratorProgress>(reportingAction),
						details
					); ; count++, progress.Report(new(count)), cancellationToken.ThrowIfCancellationRequested()
				)
				{
					if (HodokuPuzzleGenerator.Generate(HodokuPuzzleGenerator.AutoClues, symmetry, cancellationToken) is var grid
						&& analyzer.Analyze(grid) is { IsSolved: true, IsPearl: var isPearl, DifficultyLevel: var puzzleDifficultyLevel } analyzerResult
						&& (difficultyLevel == 0 || puzzleDifficultyLevel == difficultyLevel)
						&& (minimal && grid.IsMinimal || !minimal)
						&& (pearl && isPearl is true || !pearl)
						&& (technique != 0 && analyzerResult.HasTechnique(technique) || technique == 0))
					{
						return grid;
					}
				}
			}
			catch (OperationCanceledException)
			{
				return null;
			}
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
/// <param name="ShouldBePearl">Indicates whether generated puzzles should be pearl.</param>
/// <param name="SelectedTechnique">Indicates the selected technique that you want it to be appeared in generated puzzles.</param>
file readonly record struct GeneratingDetails(
	DifficultyLevel DifficultyLevel,
	SymmetricType SymmetricPattern,
	bool ShouldBeMinimal,
	bool ShouldBePearl,
	Technique SelectedTechnique
);
