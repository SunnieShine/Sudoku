namespace Sudoku.Analytics.Configuration;

/// <summary>
/// Represents a type that encapsulates a list of options adjusted by users and used by <see cref="StepSearcher"/> instances.
/// Some options may not relate to a real <see cref="StepSearcher"/> instance directly, but relate to a <see cref="Step"/>
/// that a <see cref="StepSearcher"/> instance can create.
/// For example, setting notation to the coordinates.
/// </summary>
/// <seealso cref="StepSearcher"/>
/// <seealso cref="Analyzer"/>
public sealed record StepSearcherOptions : IStepSearcherOptions<StepSearcherOptions>
{
	/// <summary>
	/// Indicates whether the step searchers will adjust the searching order to distinct two modes on displaying candidates,
	/// making the experience better.
	/// </summary>
	public bool DistinctDirectMode { get; init; } = false;

	/// <summary>
	/// Indicates whether the current solver uses direct mode to solve a puzzle, which means the UI will display the grid without any candidates.
	/// </summary>
	public bool IsDirectMode { get; init; } = false;

	/// <summary>
	/// Indicates the default link option.
	/// </summary>
	public LinkOption DefaultLinkOption { get; init; } = LinkOption.House;

	/// <inheritdoc cref="CoordinateConverter"/>
	public CoordinateConverter Converter { get; init; } = CoordinateConverter.InvariantCultureInstance;

	/// <summary>
	/// Indicates the current culture used.
	/// </summary>
	public CultureInfo CurrentCulture => Converter.CurrentCulture ?? CultureInfo.CurrentUICulture;

	/// <summary>
	/// Indicates the link options overridden.
	/// </summary>
	public IDictionary<LinkType, LinkOption> OverriddenLinkOptions { get; } = new Dictionary<LinkType, LinkOption>();


	/// <inheritdoc/>
	/// <remarks>
	/// This default option makes the internal members be:
	/// <list type="bullet">
	/// <item><see cref="Converter"/>: <see cref="RxCyConverter"/></item>
	/// <item><see cref="DistinctDirectMode"/>: <see langword="false"/></item>
	/// <item><see cref="IsDirectMode"/>: <see langword="false"/></item>
	/// <item><see cref="DefaultLinkOption"/>: <see cref="LinkOption.House"/></item>
	/// <item><see cref="OverriddenLinkOptions"/>: <c>[]</c></item>
	/// </list>
	/// </remarks>
	public static StepSearcherOptions Default => new();
}
