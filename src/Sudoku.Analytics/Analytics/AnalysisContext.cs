namespace Sudoku.Analytics;

/// <summary>
/// Defines a context that is used by step searchers to check the details of the solving and analysis information.
/// </summary>
/// <param name="accumulator">
/// <para>The accumulator to store each step.</para>
/// <para>
/// If <see cref="OnlyFindOne"/> is set to <see langword="true"/>,
/// this argument will become useless because we only finding one step is okay,
/// so we may not use the accumulator to store all possible steps, in order to optimize the performance.
/// Therefore, this argument can be <see langword="null"/> in this case.
/// </para>
/// </param>
/// <param name="grid">Indicates the puzzle to be solved and analyzed.</param>
/// <param name="initialGrid">Indicates the initial grid.</param>
/// <param name="onlyFindOne">Indicates whether the solver only find one possible step and exit the searcher.</param>
/// <param name="predefinedOptions">
/// Indicates the pre-defined options set by user in type <see cref="Analyzer"/>.
/// The value can be <see langword="null"/> if the target step searcher doesn't use this property.
/// </param>
/// <seealso cref="Analyzer"/>
[StructLayout(LayoutKind.Auto)]
[DebuggerStepThrough]
[LargeStructure]
[method: EditorBrowsable(EditorBrowsableState.Never)]
[method: MethodImpl(MethodImplOptions.AggressiveInlining)]
public ref partial struct AnalysisContext(
	[PrimaryConstructorParameter(SetterExpression = "internal set")] List<Step>? accumulator,
	[PrimaryConstructorParameter(MemberKinds.Field, Accessibility = "public", GeneratedMemberName = "Grid")] ref readonly Grid grid,
	[PrimaryConstructorParameter(MemberKinds.Field, Accessibility = "public", GeneratedMemberName = "InitialGrid")] ref readonly Grid initialGrid,
	[PrimaryConstructorParameter(MembersNotNull = "false: Accumulator")] bool onlyFindOne,
	[PrimaryConstructorParameter] StepSearcherOptions predefinedOptions
)
{
	/// <summary>
	/// Indicates whether a puzzle satisfies a Gurth's Symmetrical Placement (GSP) pattern.
	/// If satisfying, what kind of symmetry the pattern will be.
	/// </summary>
	/// <remarks>
	/// This value will only be set in <see cref="Analyzer"/> with a not-<see langword="null"/> value.
	/// </remarks>
	/// <seealso cref="Analyzer"/>
	[DisallowNull]
	public SymmetricType? GspPatternInferred { get; internal set; }

	/// <summary>
	/// Indicates the previously set digit.
	/// </summary>
	/// <remarks>
	/// This value will only be set in <see cref="SingleStepSearcher"/>.
	/// </remarks>
	/// <seealso cref="SingleStepSearcher"/>
	public Digit PreviousSetDigit { get; internal set; }

	/// <summary>
	/// Indicates the mapping relations. The index of the array means the base digit,
	/// and the target value at the specified index means the other mapping digit. If a value does not contain a mapping digit,
	/// the indexed value (target value) will be an empty array.
	/// </summary>
	/// <remarks><b>
	/// <para>
	/// By default, the value is an empty array or trash memory value (stack-only structures can be used as locals, uninitialized).
	/// Due to being uninitialized, we should use this property very carefully.
	/// </para>
	/// <para>
	/// Please firstly check for property <see cref="GspPatternInferred"/>.
	/// If that property returns an array not empty, you can use this property with safety.
	/// </para>
	/// </b></remarks>
	/// <seealso cref="GspPatternInferred"/>
	public ReadOnlySpan<Digit?> MappingRelations { get; internal set; }
}
