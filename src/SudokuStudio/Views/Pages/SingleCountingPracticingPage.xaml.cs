namespace SudokuStudio.Views.Pages;

/// <summary>
/// Represents a page that provides with practice tool to allow you practicing counting logic for technique Naked Single and Full House.
/// </summary>
public sealed partial class SingleCountingPracticingPage : Page
{
	/// <summary>
	/// Indicates the maximum possible supported number of puzzles.
	/// </summary>
	private const int MaxPuzzlesCountSupported = 100;


	/// <summary>
	/// The internal sync root.
	/// </summary>
	private static readonly Lock SyncRootOnChangingPuzzles = new();


	/// <summary>
	/// Defines a timer instance.
	/// </summary>
	private readonly Stopwatch _stopwatch = new();

	/// <summary>
	/// Indicates the puzzles last.
	/// </summary>
	private volatile int _currentPuzzleIndex = -1;

	/// <summary>
	/// Indicates the target result data.
	/// </summary>
	private List<Candidate> _targetResultData;

	/// <summary>
	/// Indicates the answered data.
	/// </summary>
	private List<(Candidate Candidate, TimeSpan TimeSpan)> _answeredData;


	/// <summary>
	/// Initializes a <see cref="SingleCountingPracticingPage"/> instance.
	/// </summary>
	public SingleCountingPracticingPage()
	{
		InitializeComponent();
		InitializeEvents();
		InitializeFields();
	}


	/// <summary>
	/// Indicates the selection mode. The value is as follows:
	/// <list type="table">
	/// <listheader>
	/// <term>Value</term>
	/// <description>Meaning</description>
	/// </listheader>
	/// <item>
	/// <term>0</term>
	/// <description>Indicates the generator generates the puzzle that only uses row/column/block 5.</description>
	/// </item>
	/// <item>
	/// <term>1</term>
	/// <description>Indicates the generator generates the puzzle that relies on full houses.</description>
	/// </item>
	/// <item>
	/// <term>2</term>
	/// <description>Indicates the generator generates the puzzle that relies on naked singles.</description>
	/// </item>
	/// <item>
	/// <term>3</term>
	/// <description>Indicates both selection 1 and 2.</description>
	/// </item>
	/// </list>
	/// </summary>
	[DependencyProperty(DefaultValue = -1)]
	public partial int SelectedMode { get; set; }

	/// <summary>
	/// Indicates the tested puzzles count.
	/// </summary>
	[DependencyProperty(DefaultValue = 10)]
	public partial int TestedPuzzlesCount { get; set; }

	/// <summary>
	/// Indicates whether the game is running.
	/// </summary>
	[DependencyProperty]
	internal partial bool IsRunning { get; set; }


	/// <summary>
	/// Initializes for fields.
	/// </summary>
	[MemberNotNull(nameof(_targetResultData), nameof(_answeredData))]
	private void InitializeFields()
	{
		_targetResultData = new(MaxPuzzlesCountSupported);
		_answeredData = new(MaxPuzzlesCountSupported);
		_targetResultData.Refresh(MaxPuzzlesCountSupported);
		_answeredData.Refresh(MaxPuzzlesCountSupported);
	}

	/// <summary>
	/// Initializes for events.
	/// </summary>
	private void InitializeEvents() => SudokuPane.DigitInput += SudokuPane_DigitInput;

	/// <summary>
	/// Try to generate a new puzzle.
	/// </summary>
	private void GeneratePuzzle()
	{
		var final = g((GeneratingMode)SelectedMode, out var targetCandidate);
		SudokuPane.Puzzle = final;
		_targetResultData[_currentPuzzleIndex] = targetCandidate;


		static Grid g(GeneratingMode mode, out Candidate targetCandidate)
		{
			if (mode == GeneratingMode.Both)
			{
				mode = Random.Shared.Next(0, 1000) < 500 ? GeneratingMode.FullHouse : GeneratingMode.NakedSingle;
			}

			switch (mode)
			{
				case GeneratingMode.House5 or GeneratingMode.FullHouse when mode switch
				{
					GeneratingMode.House5 => ConclusionCellAlignment.CenterHouse,
					_ => ConclusionCellAlignment.NotLimited
				} is var targetAlignment && g<FullHouseGenerator>(targetAlignment, out var puzzle, out targetCandidate):
				{
					return puzzle;
				}
				case GeneratingMode.NakedSingle when g<NakedSingleGenerator>(default, out var puzzle, out targetCandidate):
				{
					return puzzle;
				}
				default:
				{
					throw new ArgumentException(SR.ExceptionMessage("ModeInvalidOrUndefined"), nameof(mode));
				}
			}


			static bool g<TPrimaryGenerator>(ConclusionCellAlignment alignment, out Grid result, out Candidate targetCandidate)
				where TPrimaryGenerator : PrimaryGenerator, new()
			{
				var generator = new TPrimaryGenerator { Alignment = alignment };
				if (generator.TryGenerateJustOneCell(out var p, out var step) && step is SingleStep { Cell: var c, Digit: var d })
				{
					(targetCandidate, result) = (c * 9 + d, p);
					return true;
				}

				(targetCandidate, result) = (-1, Grid.Undefined);
				return false;
			}
		}
	}


	[Callback]
	private static void IsRunningPropertyCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if ((d, e) is not (SingleCountingPracticingPage { TestedPuzzlesCount: var testedCount } page, { NewValue: bool value }))
		{
			return;
		}

		lock (SyncRootOnChangingPuzzles)
		{
			if (value)
			{
				page._stopwatch.Start();
				page._currentPuzzleIndex = 0;
				page.ResultDataDisplayer.Text = string.Empty;
			}
			else
			{
				page._stopwatch.Stop();
				page._currentPuzzleIndex = -1;

				var correctCount = page._answeredData.CountWithSameIndex(page._targetResultData, static (a, b) => a.Candidate == b, testedCount);
				var totalTimeSpan = page._answeredData[testedCount - 1].TimeSpan;
				page.ResultDataDisplayer.Text = string.Format(
					SR.Get("SingleCountingPracticingPage_ResultDisplayLabel", App.CurrentCulture),
					totalTimeSpan,
					testedCount,
					totalTimeSpan / testedCount,
					(double)correctCount / testedCount,
					correctCount,
					testedCount
				);
			}

			page._targetResultData.Refresh(MaxPuzzlesCountSupported);
			page._answeredData.Refresh(MaxPuzzlesCountSupported);
		}
	}


	private void SudokuPane_DigitInput(SudokuPane sender, DigitInputEventArgs e)
	{
		lock (SyncRootOnChangingPuzzles)
		{
			if (!IsRunning || _currentPuzzleIndex >= TestedPuzzlesCount)
			{
				return;
			}

			if (e is not { Candidate: var candidate and not -1 })
			{
				return;
			}

			var elapsed = _stopwatch.Elapsed;
			_answeredData[_currentPuzzleIndex] = (
				candidate,
				_currentPuzzleIndex == 0 ? elapsed : elapsed - _answeredData[_currentPuzzleIndex - 1].TimeSpan
			);

			if (++_currentPuzzleIndex >= TestedPuzzlesCount)
			{
				IsRunning = false;
			}
			else
			{
				GeneratePuzzle();

				// By setting 'e.Cancel' to true, to tell the event handler don't re-trigger this event,
				// preventing the old input value filling into the grid that has already updated currently.
				e.Cancel = true;
			}
		}
	}

	private void StartButton_Click(object sender, RoutedEventArgs e)
	{
		IsRunning = true;
		GeneratePuzzle();
	}

	private void Page_Loaded(object sender, RoutedEventArgs e) => SelectModeComboxBox.SelectedIndex = 0;

	private void SudokuPane_Loaded(object sender, RoutedEventArgs e)
	{
		var app = Application.Current.AsApp();
		app.CoverSettingsToSudokuPaneViaApplicationTheme(SudokuPane);
		app.MainSudokuPane = SudokuPane;
	}

	private void SudokuPane_ActualThemeChanged(FrameworkElement sender, object args)
		=> Application.Current.AsApp().CoverSettingsToSudokuPaneViaApplicationTheme(SudokuPane);
}

/// <include file='../../global-doc-comments.xml' path='g/csharp11/feature[@name="file-local"]/target[@name="class" and @when="extension"]'/>
file static class Extensions
{
	/// <summary>
	/// Counts up the number of instances that satisfies the specified condition, with the specified instance as the reference,
	/// using index from another collection.
	/// </summary>
	/// <typeparam name="T1">The type of elements from the current collection.</typeparam>
	/// <typeparam name="T2">The type of elements from the other collection.</typeparam>
	/// <param name="this">The collection.</param>
	/// <param name="other">The other collection.</param>
	/// <param name="predicate">The condition.</param>
	/// <param name="count">The number of elements.</param>
	/// <returns>An <see cref="int"/> result.</returns>
	public static int CountWithSameIndex<T1, T2>(this List<T1?> @this, List<T2?> other, Func<T1?, T2?, bool> predicate, int count)
		where T1 : notnull
		where T2 : notnull
	{
		Debug.Assert(@this.Count == other.Count);

		var result = 0;
		for (var i = 0; i < count; i++)
		{
			if (predicate(@this[i], other[i]))
			{
				result++;
			}
		}
		return result;
	}

	/// <summary>
	/// Refresh the collection.
	/// </summary>
	/// <typeparam name="T">The type of each element.</typeparam>
	/// <param name="this">The collection.</param>
	/// <param name="count">The number of elements to be created.</param>
	public static void Refresh<T>(this List<T?> @this, int count) where T : notnull
	{
		@this.Clear();
		for (var i = 0; i < count; i++)
		{
			@this.Add(default);
		}
	}
}

/// <summary>
/// Indicates the generating mode.
/// </summary>
file enum GeneratingMode
{
	/// <summary>
	/// Indicates the generator generates the puzzle that only uses row/column/block 5.
	/// </summary>
	House5 = 0,

	/// <summary>
	/// Indicates the generator generates the puzzle that relies on full houses.
	/// </summary>
	FullHouse = 1,

	/// <summary>
	/// Indicates the generator generates the puzzle that relies on naked singles.
	/// </summary>
	NakedSingle = 2,

	/// <summary>
	/// Indicates both <see cref="FullHouse"/> and <see cref="NakedSingle"/>.
	/// </summary>
	/// <seealso cref="FullHouse"/>
	/// <seealso cref="NakedSingle"/>
	Both = 3
}
