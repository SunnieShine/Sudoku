namespace SudokuStudio.Configuration;

/// <summary>
/// Represents with preference items that is used by <see cref="Analyzer"/>, for the ordering of <see cref="StepSearcher"/>s.
/// </summary>
/// <seealso cref="Analyzer"/>
/// <seealso cref="StepSearcher"/>
public sealed partial class StepSearcherOrderingPreferenceGroup : PreferenceGroup
{
	[Default]
	private static readonly ObservableCollection<StepSearcherInfo> StepSearchersOrderDefaultValue = new(
		(
			from searcher in StepSearcherFactory.StepSearchers
			select new StepSearcherInfo
			{
				IsEnabled = searcher.RunningArea.HasFlag(StepSearcherRunningArea.Searching),
				TypeName = searcher.GetType().Name
			}
		).ToArray()
	);


	/// <summary>
	/// Indicates the order of step searchers.
	/// </summary>
	[DependencyProperty]
	public partial ObservableCollection<StepSearcherInfo> StepSearchersOrder { get; set; }


	[Callback]
	private static void StepSearchersOrderPropertyCallback(DependencyObject obj, DependencyPropertyChangedEventArgs e)
	{
		if (e is not { NewValue: ObservableCollection<StepSearcherInfo> stepSearchers })
		{
			return;
		}

		if (Application.Current.AsApp().Analyzer is not { } analyzer)
		{
			return;
		}

		analyzer.WithStepSearchers([.. from s in stepSearchers select s.CreateStepSearcher()]);
	}
}
