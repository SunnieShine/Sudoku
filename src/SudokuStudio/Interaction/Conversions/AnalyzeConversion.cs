namespace SudokuStudio.Interaction.Conversions;

/// <summary>
/// Provides with conversion methods used by XAML designer, about analyze tab pages.
/// </summary>
internal static class AnalyzeConversion
{
	public static bool GetIsEnabled(Grid grid) => grid is { IsSolved: false, SolutionGrid.IsUndefined: false };

	public static bool GetAnalyzerButtonIsEnabled(bool isGeneratorLaunched) => !isGeneratorLaunched;

	public static bool GetProgressRingIsIntermediate(bool isAnalyzerLaunched, bool isGathererLaunched, bool isGeneratorLaunched)
		=> (isAnalyzerLaunched, isGathererLaunched, isGeneratorLaunched) switch { (_, _, true) => true, _ => false };

	public static bool GetProgressRingIsActive(bool isAnalyzerLaunched, bool isGathererLaunched, bool isGeneratorLaunched)
		=> (isAnalyzerLaunched, isGathererLaunched, isGeneratorLaunched) switch { (_, _, true) => false, _ => true };

	public static int GetViewPipsPagerPageCount(IRenderable? renderable) => renderable?.Views?.Length ?? 0;

	public static int GetCurrentViewIndexForViewPipsPager(int currentIndex) => currentIndex;

	public static double GetWidth_HodokuRatingText(bool showing) => showing ? 40 : 0;

	public static double GetWidth_SudokuExplainerText(bool showing) => showing ? 60 : 0;

	public static string GetEliminationString(Step step) => step.Options.Converter.ConclusionConverter(step.Conclusions);

	public static string GetDifficultyRatingText(Step step)
	{
		var pref = ((App)Application.Current).Preference.TechniqueInfoPreferences;
		var resultDifficulty = pref.GetRating(step.Code) switch
		{
			{ } integerValue => integerValue / pref.RatingScale,
			_ => step.Difficulty * TechniqueInfoPreferenceGroup.RatingScaleDefaultValue / pref.RatingScale
		};
		return resultDifficulty.ToString(GetFormatOfDifficulty(resultDifficulty));
	}

	public static string GetDifficultyRatingText_Hodoku(Step step)
		=> HodokuCompatibility.GetDifficultyScore(step.Code, out _) is { } r ? r.ToString() : string.Empty;

	public static string GetDifficultyRatingText_SudokuExplainer(Step step)
		=> SudokuExplainerCompatibility.GetDifficultyRatingRange(step.Code) switch
		{
			({ IsRange: false, Min: var d }, _) => $"{d:0.0}",
			({ IsRange: true, Min: var d1, Max: var d2 }, _) => $"{d1:0.0}-{d2:0.0}",
			(_, { IsRange: false, Min: var d }) => $"{d:0.0}",
			(_, { IsRange: true, Min: var d1, Max: var d2 }) => $"{d1:0.0}-{d2:0.0}",
			_ => string.Empty
		};

	public static string GetIndexText(SolvingPathStepBindableSource step) => (step.Index + 1).ToString();

	public static string GetViewIndexDisplayerString(IRenderable? visualUnit, int currentIndex)
		=> visualUnit?.Views?.Length is { } length ? $"{currentIndex + 1}/{length}" : "0/0";

	public static string GetName(Step step) => step.GetName(App.CurrentCulture);

	public static string GetSimpleString(Step step) => step.ToSimpleString(App.CurrentCulture);

	public static Thickness GetMargin_HodokuRating(bool showing) => showing ? new(12, 0, 0, 0) : new();

	public static Thickness GetMargin_SudokuExplainerRating(bool showing) => showing ? new(12, 0, 0, 0) : new();

	public static Visibility GetProgressRingVisibility(bool isAnalyzerLaunched, bool isGathererLaunched, bool isGeneratorLaunched)
		=> isAnalyzerLaunched || isGathererLaunched || isGeneratorLaunched ? Visibility.Visible : Visibility.Collapsed;

	public static Visibility GetAnalyzeTabsVisibility(bool isAnalyzerLaunched, bool isGathererLaunched, bool isGeneratorLaunched)
		=> isAnalyzerLaunched || isGathererLaunched || isGeneratorLaunched ? Visibility.Collapsed : Visibility.Visible;

	public static Visibility GetDifficultyRatingVisibility(bool showDifficultyRating)
		=> showDifficultyRating ? Visibility.Visible : Visibility.Collapsed;

	public static Visibility GetSummaryTableVisibility(IEnumerable itemsSource)
		=> itemsSource is null || itemsSource.None() ? Visibility.Collapsed : Visibility.Visible;

	public static Visibility GetSolvingPathListVisibility(object itemsSource)
		=> itemsSource switch { SolvingPathStepCollection and not [] => Visibility.Visible, _ => Visibility.Collapsed };

	public static Visibility GetViewPipsPagerVisibility(IRenderable? renderable)
		=> renderable switch { { Views.Length: >= 2 } => Visibility.Visible, _ => Visibility.Collapsed };

	public static Visibility GetEnglishNameTextBlockVisibility()
		=> ((App)Application.Current).Preference.AnalysisPreferences.AlsoDisplayEnglishNameOfStep ? Visibility.Visible : Visibility.Collapsed;

	public static IEnumerable<Inline> GetInlinesOfTooltip(SolvingPathStepBindableSource s)
	{
		if (s is not
			{
				Index: var index,
				DisplayItems: var displayKind,
				Step:
				{
					Code: var technique,
					BaseDifficulty: var baseDifficulty,
					Difficulty: var difficulty,
					ExtraDifficultyFactors: var cases
				} step
			})
		{
			throw new ArgumentException($"The argument '{nameof(s)}' is invalid.", nameof(s));
		}

		var pref = ((App)Application.Current).Preference.TechniqueInfoPreferences;
		var result = new List<Inline>();

		if (displayKind.HasFlag(StepTooltipDisplayItems.TechniqueName))
		{
			result.Add(new Run { Text = ResourceDictionary.Get("AnalyzePage_TechniqueName", App.CurrentCulture) }.SingletonSpan<Bold>());
			result.Add(new LineBreak());
			result.Add(new Run { Text = step.GetName(App.CurrentCulture) });
		}

		if (displayKind.HasFlag(StepTooltipDisplayItems.TechniqueIndex))
		{
			appendEmptyLinesIfNeed();

			result.Add(new Run { Text = ResourceDictionary.Get("AnalyzePage_TechniqueIndex", App.CurrentCulture) }.SingletonSpan<Bold>());
			result.Add(new LineBreak());
			result.Add(new Run { Text = (index + 1).ToString() });
		}

		if (displayKind.HasFlag(StepTooltipDisplayItems.Abbreviation))
		{
			appendEmptyLinesIfNeed();

			result.Add(new Run { Text = ResourceDictionary.Get("AnalyzePage_Abbreviation", App.CurrentCulture) }.SingletonSpan<Bold>());
			result.Add(new LineBreak());
			result.Add(new Run { Text = technique.GetAbbreviation() ?? ResourceDictionary.Get("AnalyzePage_None", App.CurrentCulture) });
		}

		if (displayKind.HasFlag(StepTooltipDisplayItems.Aliases))
		{
			appendEmptyLinesIfNeed();

			result.Add(new Run { Text = ResourceDictionary.Get("AnalyzePage_Aliases", App.CurrentCulture) }.SingletonSpan<Bold>());
			result.Add(new LineBreak());
			result.Add(
				new Run
				{
					Text = technique.GetAliasedNames(App.CurrentCulture) is { } aliases and not []
						? string.Join(ResourceDictionary.Get("_Token_Comma", App.CurrentCulture), aliases)
						: ResourceDictionary.Get("AnalyzePage_None", App.CurrentCulture)
				}
			);
		}

		if (displayKind.HasFlag(StepTooltipDisplayItems.DifficultyRating))
		{
			appendEmptyLinesIfNeed();

			var difficultyValue = pref.GetRating(technique) switch
			{
				{ } integerValue => integerValue,
				_ => difficulty * TechniqueInfoPreferenceGroup.RatingScaleDefaultValue
			} / pref.RatingScale;

			result.Add(new Run { Text = ResourceDictionary.Get("AnalyzePage_TechniqueDifficultyRating", App.CurrentCulture) }.SingletonSpan<Bold>());
			result.Add(new LineBreak());
			result.Add(new Run { Text = difficultyValue.ToString(GetFormatOfDifficulty(difficultyValue)) });
		}

		if (displayKind.HasFlag(StepTooltipDisplayItems.ExtraDifficultyCases))
		{
			appendEmptyLinesIfNeed();

			result.Add(new Run { Text = ResourceDictionary.Get("AnalyzePage_ExtraDifficultyCase", App.CurrentCulture) }.SingletonSpan<Bold>());
			result.Add(new LineBreak());

			switch (cases)
			{
				case { Length: not 0 }:
				{
					var baseDifficultyValue = pref.GetRating(technique) switch
					{
						{ } integerValue => integerValue,
						_ => baseDifficulty * TechniqueInfoPreferenceGroup.RatingScaleDefaultValue
					} / pref.RatingScale;
					var baseDifficultyString = baseDifficultyValue.ToString(GetFormatOfDifficulty(baseDifficultyValue));

					result.Add(new Run { Text = $"{ResourceDictionary.Get("AnalyzePage_BaseDifficulty", App.CurrentCulture)}{baseDifficultyString}" });
					result.Add(new LineBreak());
					result.AddRange(appendExtraDifficultyFactors(cases, pref.RatingScale));
					break;
				}
				default:
				{
					result.Add(new Run { Text = ResourceDictionary.Get("AnalyzePage_None", App.CurrentCulture) });
					break;
				}
			}
		}

		if (displayKind.HasFlag(StepTooltipDisplayItems.SimpleDescription))
		{
			appendEmptyLinesIfNeed();

			result.Add(new Run { Text = ResourceDictionary.Get("AnalyzePage_SimpleDescription", App.CurrentCulture) }.SingletonSpan<Bold>());
			result.Add(new LineBreak());
			result.Add(new Run { Text = step.ToString(App.CurrentCulture) });
		}

		return result;


		static IEnumerable<Inline> appendExtraDifficultyFactors(ExtraDifficultyFactor[] factors, decimal ratingScale)
		{
			var colon = ResourceDictionary.Get("_Token_Colon", App.CurrentCulture);
			for (var i = 0; i < factors.Length; i++)
			{
				var factor = factors[i];
				var extraDifficultyName = factor.ToString(App.CurrentCulture);
				var difficultyValue = factor.Value * TechniqueInfoPreferenceGroup.RatingScaleDefaultValue / ratingScale;
				var difficultyValueString = difficultyValue.ToString(GetFormatOfDifficulty(difficultyValue));
				yield return new Run { Text = $"{extraDifficultyName}{colon}+{difficultyValueString}" };

				if (i != factors.Length - 1)
				{
					yield return new LineBreak();
				}
			}
		}

		void appendEmptyLinesIfNeed()
		{
			if (result.Count != 0)
			{
				result.Add(new LineBreak());
				result.Add(new LineBreak());
			}
		}
	}

	/// <summary>
	/// Try to get the format string via the decimal value.
	/// </summary>
	/// <param name="scaling">The scaling value.</param>
	/// <returns>
	/// The format string. The value will be:
	/// <list type="table">
	/// <listheader>
	/// <term>The number of digits after period token '<c>.</c>'</term>
	/// <description>Result format string</description>
	/// </listheader>
	/// <item>
	/// <term>2</term>
	/// <description>"<c>0.00</c>"</description>
	/// </item>
	/// <item>
	/// <term>1</term>
	/// <description>"<c>0.0</c>"</description>
	/// </item>
	/// <item>
	/// <term>0 or others</term>
	/// <description>"<c>0</c>"</description>
	/// </item>
	/// </list>
	/// </returns>
	private static string GetFormatOfDifficulty(decimal scaling)
	{
		// A little trick is to get the length of the string, and remove the digits before period.
		// E.g.
		//    4.321 is of length 5 ('4', '.', '3', '2' and '1')
		//             ↓
		//      Index of '.' = 1
		//             ↓
		//   Result = 5 - 1 - 1 = 3

		var s = scaling.ToString();
		var length = s.Length;
		var pos = s.IndexOf('.'); // 'pos' can be -1.
		return pos == -1 ? "0" : (length - pos - 1) switch { 0 => "0", 1 => "0.0", 2 => "0.00", _ => "0.00" };
	}
}
