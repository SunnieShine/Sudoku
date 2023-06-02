namespace Sudoku.Analytics;

/// <summary>
/// Represents a searcher that can creates <see cref="Step"/> instances for the specified technique.
/// </summary>
/// <param name="priority">
/// <para>
/// Indicates the priority value of the current step searcher.
/// This property is used for sorting multiple <see cref="StepSearcher"/> instances.
/// </para>
/// <para>
/// Please note that the set value cannot be same for different <see cref="StepSearcher"/> types;
/// otherwise, <see cref="InvalidOperationException"/> will be thrown while comparing with two <see cref="StepSearcher"/>s.
/// </para>
/// <para>
/// This property may be automatically generated by source generator. Therefore, you may not care about implementation of this property.
/// </para>
/// </param>
/// <param name="level">
/// <para>Indicates the level that the current step searcher belongs to.</para>
/// <para>
/// This property indicates how difficult the step searcher can be enabled.
/// </para>
/// </param>
/// <param name="runningArea">
/// <para>Indicates the running area which describes a function where the current step searcher can be invoked.</para>
/// <para>
/// By default, the step searcher will support
/// both <see cref="StepSearcherRunningArea.Searching"/> and <see cref="StepSearcherRunningArea.Gathering"/>.
/// </para>
/// </param>
/// <seealso cref="Step"/>
public abstract partial class StepSearcher(
	[PrimaryConstructorParameter] int priority,
	[PrimaryConstructorParameter] int level,
	[PrimaryConstructorParameter] StepSearcherRunningArea runningArea = StepSearcherRunningArea.Searching | StepSearcherRunningArea.Gathering
) : IComparable<StepSearcher>, IEquatable<StepSearcher>
{
	/// <summary>
	/// Indicates the backing field of property <see cref="SeparatedPriority"/>.
	/// </summary>
	/// <seealso cref="SeparatedPriority"/>
	private int _separatedPriority;


	/// <summary>
	/// Determines whether the current step searcher is separated one, which mean it can be created
	/// as many possible instances in a same step searchers pool.
	/// </summary>
	public bool IsSeparated => EqualityContract.GetCustomAttribute<SeparatedAttribute>() is not null;

	/// <summary>
	/// Determines whether the current step searcher is a direct one.
	/// </summary>
	/// <remarks>
	/// If you don't know what is a direct step searcher, please visit the property
	/// <see cref="DirectAttribute"/> to learn more information.
	/// </remarks>
	/// <seealso cref="DirectAttribute"/>
	public bool IsDirect => EqualityContract.IsDefined(typeof(DirectAttribute));

	/// <summary>
	/// Determines whether we can adjust the ordering of the current step searcher
	/// as a customized configuration option before solving a puzzle.
	/// </summary>
	/// <remarks>
	/// If you don't know what is a direct step searcher, please visit the property <see cref="FixedAttribute"/> to learn more information.
	/// </remarks>
	/// <seealso cref="FixedAttribute"/>
	public bool IsFixed => EqualityContract.IsDefined(typeof(FixedAttribute));

	/// <summary>
	/// Determines whether the current step searcher is not supported for sukaku solving mode.
	/// </summary>
	public bool IsNotSupportedForSukaku
		=> EqualityContract.GetCustomAttribute<ConditionalCasesAttribute>() is { Cases: var cases } && cases.Flags(ConditionalCase.Standard);

	/// <summary>
	/// Determines whether the current step searcher is disabled
	/// by option <see cref="ConditionalCase.UnlimitedTimeComplexity"/> being configured.
	/// </summary>
	/// <seealso cref="ConditionalCase.UnlimitedTimeComplexity"/>
	public bool IsConfiguredSlow
		=> EqualityContract.GetCustomAttribute<ConditionalCasesAttribute>() is { Cases: var cases }
		&& cases.Flags(ConditionalCase.UnlimitedTimeComplexity);

	/// <summary>
	/// Determines whether the current step searcher is disabled
	/// by option <see cref="ConditionalCase.UnlimitedSpaceComplexity"/> being configured.
	/// </summary>
	/// <seealso cref="ConditionalCase.UnlimitedSpaceComplexity"/>
	public bool IsConfiguredHighAllocation
		=> EqualityContract.GetCustomAttribute<ConditionalCasesAttribute>() is { Cases: var cases }
		&& cases.Flags(ConditionalCase.UnlimitedSpaceComplexity);

	/// <summary>
	/// Indicates the separated priority. This value cannot be greater than 16 due to design of <see cref="SeparatedAttribute"/>.
	/// </summary>
	/// <value>The value to be set. The value must be between 0 and 16 (i.e. <![CDATA[>= 0 and < 16]]>).</value>
	/// <exception cref="ArgumentOutOfRangeException">
	/// Throws when <see langword="value"/> is below 0, greater than 16 or equal to 16.
	/// </exception>
	/// <seealso cref="SeparatedAttribute"/>
	public int SeparatedPriority
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		get => _separatedPriority;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[RequiresUnreferencedCode("This setter can only be invoked by reflection.")]
		internal init => _separatedPriority = value is >= 0 and < 16 ? value : throw new ArgumentOutOfRangeException(nameof(value));
	}

	/// <summary>
	/// Indicates the final priority value ID of the step searcher. This property is used as comparison.
	/// </summary>
	internal int PriorityId => Priority << 4 | SeparatedPriority;

	/// <summary>
	/// The qualified type name of this instance.
	/// </summary>
	protected string TypeName => EqualityContract.Name;

	/// <summary>
	/// Indicates the <see cref="Type"/> instance that represents the reflection data for the current instance.
	/// This property is used as type checking to distinct with multiple <see cref="StepSearcher"/>s.
	/// </summary>
	protected Type EqualityContract => GetType();


	[GeneratedOverridingMember(GeneratedEqualsBehavior.AsCastAndCallingOverloading)]
	public override partial bool Equals(object? obj);

	/// <inheritdoc/>
	public bool Equals([NotNullWhen(true)] StepSearcher? other) => other is not null && CompareTo(other) == 0;

	[GeneratedOverridingMember(GeneratedGetHashCodeBehavior.SimpleField, nameof(PriorityId))]
	public override partial int GetHashCode();

	/// <inheritdoc/>
	public int CompareTo(StepSearcher? other)
	{
		ArgumentNullException.ThrowIfNull(other);

		return Sign(PriorityId - other.PriorityId);
	}

	/// <summary>
	/// Returns the real name of this instance.
	/// </summary>
	/// <returns>Real name of this instance.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public sealed override string ToString() => R[$"StepSearcherName_{TypeName}"] ?? TypeName;

	/// <summary>
	/// Try to collect all available <see cref="Step"/>s using the current technique rule.
	/// </summary>
	/// <param name="context">
	/// <para>
	/// The analysis context. This argument offers you some elementary data configured or assigned, for the current loop of step searching.
	/// </para>
	/// <para>
	/// All available <see cref="Step"/> results will be stored in property <see cref="AnalysisContext.Accumulator"/>
	/// of this argument, if property <see cref="AnalysisContext.OnlyFindOne"/> returns <see langword="false"/>;
	/// otherwise, the property won't be used, and this method will return the first found step.
	/// </para>
	/// </param>
	/// <returns>
	/// Returns the first found step. The nullability of the return value is described as follow:
	/// <list type="bullet">
	/// <item>
	/// <see langword="null"/>:
	/// <list type="bullet">
	/// <item><c><paramref name="context"/>.OnlyFindOne == <see langword="false"/></c>.</item>
	/// <item><c><paramref name="context"/>.OnlyFindOne == <see langword="true"/></c>, but nothing found.</item>
	/// </list>
	/// </item>
	/// <item>
	/// Not <see langword="null"/>:
	/// <list type="bullet">
	/// <item>
	/// <c><paramref name="context"/>.OnlyFindOne == <see langword="true"/></c>,
	/// and found <b>at least one step</b>. In this case the return value is the first found step.
	/// </item>
	/// </list>
	/// </item>
	/// </list>
	/// </returns>
	/// <seealso cref="Step"/>
	/// <seealso cref="AnalysisContext"/>
	protected internal abstract Step? Collect(scoped ref AnalysisContext context);
}
