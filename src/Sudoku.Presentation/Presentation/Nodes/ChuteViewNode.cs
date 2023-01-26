﻿namespace Sudoku.Presentation.Nodes;

/// <summary>
/// Defines a view node that highlights for a chute (i.e. 3 houses that is in a three blocks in a line).
/// </summary>
public sealed partial class ChuteViewNode : BasicViewNode
{
	/// <summary>
	/// Initializes a <see cref="ChuteViewNode"/> instance via the identifier and the highlight chute.
	/// </summary>
	/// <param name="identifier">The identifier.</param>
	/// <param name="chuteIndex">The chute index.</param>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public ChuteViewNode(Identifier identifier, int chuteIndex) : base(identifier) => ChuteIndex = chuteIndex;


	/// <summary>
	/// Indicates whether the chute is in a row.
	/// </summary>
	public bool IsRow => ChuteIndex < 3;

	/// <summary>
	/// Indicates the chute index. The value can be between 0 and 5.
	/// </summary>
	public int ChuteIndex { get; }

	/// <summary>
	/// <para>
	/// Indicates a <see cref="short"/> mask that represents for the houses used.
	/// The result mask is a 27-bit digit that represents every possible houses using cases.
	/// </para>
	/// <para>
	/// Please note that the first 9-bit always keep the zero value because they is reserved bits
	/// for block houses, but all chutes don't use them.
	/// </para>
	/// </summary>
	public int HousesMask => Chutes[ChuteIndex] switch { (_, var isRow, var rawMask) => rawMask << (isRow ? 9 : 18) };


	[GeneratedDeconstruction]
	public partial void Deconstruct(out Identifier identifier, [GeneratedDeconstructionArgument(nameof(ChuteIndex))] out int chute);

	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public override bool Equals([NotNullWhen(true)] ViewNode? other)
		=> other is ChuteViewNode comparer && Identifier == comparer.Identifier && ChuteIndex == comparer.ChuteIndex;

	[GeneratedOverriddingMember(GeneratedGetHashCodeBehavior.CallingHashCodeCombine, nameof(TypeIdentifier), nameof(Identifier), nameof(ChuteIndex))]
	public override partial int GetHashCode();

	[GeneratedOverriddingMember(GeneratedToStringBehavior.RecordLike, nameof(Identifier), nameof(ChuteIndex))]
	public override partial string ToString();

	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public override ChuteViewNode Clone() => new(Identifier, ChuteIndex);
}
