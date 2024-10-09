namespace Sudoku.Analytics.Construction;

/// <summary>
/// Represents a pattern that describes a technique, describing cells and digits used in a puzzle.
/// </summary>
[TypeImpl(
	TypeImplFlag.Object_Equals | TypeImplFlag.Object_GetHashCode | TypeImplFlag.EqualityOperators,
	OtherModifiersOnEquals = "sealed",
	GetHashCodeBehavior = GetHashCodeBehavior.MakeAbstract)]
public abstract partial class Pattern : ICloneable, IEquatable<Pattern>, IEqualityOperators<Pattern, Pattern, bool>
{
	/// <summary>
	/// Indicates whether the current pattern can be used as a node inside a chain pattern <see cref="ChainOrLoop"/>,
	/// represented as a relation in a link, stored in <see cref="Link.GroupedLinkPattern"/>.
	/// </summary>
	/// <seealso cref="ChainOrLoop"/>
	/// <seealso cref="Link.GroupedLinkPattern"/>
	public abstract bool IsChainingCompatible { get; }


	/// <inheritdoc/>
	public abstract bool Equals([NotNullWhen(true)] Pattern? other);

	/// <inheritdoc cref="ICloneable.Clone"/>
	public abstract Pattern Clone();

	/// <inheritdoc/>
	object ICloneable.Clone() => Clone();
}
