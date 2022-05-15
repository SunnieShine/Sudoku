﻿namespace Sudoku.Concepts.Solving;

/// <summary>
/// <para>
/// Indicates an exocet pattern. The pattern will be like:
/// <code><![CDATA[
/// .-------.-------.-------.
/// | B B E | E . . | E . . |
/// | . . E | Q . . | R . . |
/// | . . E | Q . . | R . . |
/// :-------+-------+-------:
/// | . . S | S . . | S . . |
/// | . . S | S . . | S . . |
/// | . . S | S . . | S . . |
/// :-------+-------+-------:
/// | . . S | S . . | S . . |
/// | . . S | S . . | S . . |
/// | . . S | S . . | S . . |
/// '-------'-------'-------'
/// ]]></code>
/// Where:
/// <list type="table">
/// <item><term>B</term><description>Base Cells.</description></item>
/// <item><term>Q</term><description>1st Object Pair (Target cells pair 1).</description></item>
/// <item><term>R</term><description>2nd Object Pair (Target cells pair 2).</description></item>
/// <item><term>S</term><description>Cross-line Cells.</description></item>
/// <item><term>E</term><description>Escape Cells.</description></item>
/// </list>
/// </para>
/// <para>
/// In the data structure, all letters will be used as the same one in this exemplar.
/// In addition, if senior exocet, one of two target cells will lie in cross-line cells,
/// and the lines of two target cells lying on can't contain any base digits.
/// </para>
/// </summary>
/// <param name="Base1">Indicates the first base cell.</param>
/// <param name="Base2">Indicates the second base cell.</param>
/// <param name="TargetQ1">Indicates the first target cell in the Q part.</param>
/// <param name="TargetQ2">Indicates the second target cell in the Q part.</param>
/// <param name="TargetR1">Indicates the first target cell in the R part.</param>
/// <param name="TargetR2">Indicates the second target cell in the R part.</param>
/// <param name="MirrorQ1">Indicates the first mirror cell in the Q part.</param>
/// <param name="MirrorQ2">Indicates the second mirror cell in the Q part.</param>
/// <param name="MirrorR1">Indicates the first mirror cell in the R part.</param>
/// <param name="MirrorR2">Indicates the second mirror cell in the R part.</param>
/// <param name="CrossLine">Indicates the cross-line cells.</param>
[AutoOverridesGetHashCode(nameof(Base1), nameof(Base2), nameof(TargetQ1), nameof(TargetQ2), nameof(TargetR1), nameof(TargetR2), nameof(MirrorQ1), nameof(MirrorQ2), nameof(MirrorR1), nameof(MirrorR2), nameof(BaseCellsMap), nameof(TargetCellsMap))]
[AutoOverridesEquals(nameof(Base1), nameof(Base2), nameof(TargetQ1), nameof(TargetQ2), nameof(TargetR1), nameof(TargetR2), nameof(MirrorQ1), nameof(MirrorQ2), nameof(MirrorR1), nameof(MirrorR2), nameof(BaseCellsMap), nameof(TargetCellsMap), nameof(CrossLine), EmitsInKeyword = true)]
public readonly partial record struct ExocetPattern(
	int Base1, int Base2, int TargetQ1, int TargetQ2, int TargetR1, int TargetR2, in Cells CrossLine,
	in Cells MirrorQ1, in Cells MirrorQ2, in Cells MirrorR1, in Cells MirrorR2) :
	ITechniquePattern<ExocetPattern>
{
	/// <inheritdoc/>
	public Cells Map => CrossLine + TargetQ1 + TargetQ2 + TargetR1 + TargetR2 + Base1 + Base2;

	/// <summary>
	/// Indicates the full map, with mirror cells.
	/// </summary>
	public Cells MapWithMirrors => Map | MirrorQ1 | MirrorQ2 | MirrorR1 | MirrorR2;

	/// <summary>
	/// Indicates the base cells.
	/// </summary>
	public Cells BaseCellsMap
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		get => Cells.Empty + Base1 + Base2;
	}

	/// <summary>
	/// Indicates the target cells.
	/// </summary>
	public Cells TargetCellsMap
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		get => Cells.Empty + TargetQ1 + TargetQ2 + TargetR1 + TargetR2;
	}


	/// <inheritdoc cref="object.ToString"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public override string ToString()
	{
		string baseCellsStr = (Cells.Empty + Base1 + Base2).ToString();
		string targetCellsStr = (Cells.Empty + TargetQ1 + TargetQ2 + TargetR1 + TargetR2).ToString();
		return $"Exocet: base {baseCellsStr}, target {targetCellsStr}";
	}
}
