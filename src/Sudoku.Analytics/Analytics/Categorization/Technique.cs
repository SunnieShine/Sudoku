namespace Sudoku.Analytics.Categorization;

using static ExtraDifficultyFactorNames;

/// <summary>
/// Represents a technique instance, which is used for comparison.
/// </summary>
/// <remarks><b><i>
/// Please note that some fields declared in this type may not be used by neither direct reference nor reflection,
/// but they are reserved ones. In the future I'll implement the searching logic on those fields. Maybe or maybe not :D
/// </i></b></remarks>
public enum Technique
{
	/// <summary>
	/// The placeholder of this enumeration type.
	/// </summary>
	None,

	//
	// Singles
	//
	#region Singles
	/// <summary>
	/// Indicates full house. This technique is the most elementary technique to be used in the candidate view mode.
	/// </summary>
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Full_House.html")]
	[HodokuTechniquePrefix("0000")]
	[HodokuDifficultyRating(4, HodokuDifficultyLevel.Easy)]
	[SudokuExplainerTechnique(SudokuExplainerTechnique.Single)]
	[SudokuExplainerDifficultyRating(1.0)]
	[SudokuExplainerNames("Single")]
	[TechniqueGroup(TechniqueGroup.Single)]
	[StaticDifficultyLevel(DifficultyLevel.Easy)]
	[BoundStepTypes(typeof(FullHouseStep))]
	[StaticDifficulty(1.0)]
	FullHouse,

	/// <summary>
	/// Indicates last digit.
	/// </summary>
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Last_Digit.html")]
	[HodokuTechniquePrefix("0001")]
	[TechniqueGroup(TechniqueGroup.Single)]
	[StaticDifficultyLevel(DifficultyLevel.Easy)]
	[BoundStepTypes(typeof(LastDigitStep), SecondaryTypes = [typeof(HiddenSingleStep)])]
	[StaticDifficulty(1.1)]
	LastDigit,

	/// <summary>
	/// Indicates hidden single (in block).
	/// </summary>
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Hidden_Single.html")]
	[HodokuTechniquePrefix("0002")]
	[HodokuDifficultyRating(14, HodokuDifficultyLevel.Easy)]
	[SudokuExplainerDifficultyRating(1.2)]
	[TechniqueGroup(TechniqueGroup.Single)]
	[StaticDifficultyLevel(DifficultyLevel.Easy)]
	[BoundStepTypes(typeof(HiddenSingleStep))]
	[StaticDifficulty(1.2)]
	HiddenSingleBlock,

	/// <summary>
	/// Indicates hidden single (in row).
	/// </summary>
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Hidden_Single.html")]
	[HodokuTechniquePrefix("0002")]
	[HodokuDifficultyRating(14, HodokuDifficultyLevel.Easy)]
	[SudokuExplainerTechnique(SudokuExplainerTechnique.HiddenSingle)]
	[SudokuExplainerDifficultyRating(1.5)]
	[TechniqueGroup(TechniqueGroup.Single)]
	[StaticDifficultyLevel(DifficultyLevel.Easy)]
	[BoundStepTypes(typeof(HiddenSingleStep))]
	[StaticDifficulty(1.5)]
	HiddenSingleRow,

	/// <summary>
	/// Indicates hidden single (in column).
	/// </summary>
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Hidden_Single.html")]
	[HodokuTechniquePrefix("0002")]
	[HodokuDifficultyRating(14, HodokuDifficultyLevel.Easy)]
	[SudokuExplainerTechnique(SudokuExplainerTechnique.HiddenSingle)]
	[SudokuExplainerDifficultyRating(1.5)]
	[TechniqueGroup(TechniqueGroup.Single)]
	[StaticDifficultyLevel(DifficultyLevel.Easy)]
	[BoundStepTypes(typeof(HiddenSingleStep))]
	[StaticDifficulty(1.5)]
	HiddenSingleColumn,

	/// <summary>
	/// Indicates naked single.
	/// </summary>
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Naked_Single.html")]
	[HodokuTechniquePrefix("0003")]
	[HodokuDifficultyRating(4, HodokuDifficultyLevel.Easy)]
	[SudokuExplainerTechnique(SudokuExplainerTechnique.NakedSingle)]
	[SudokuExplainerDifficultyRating(2.3)]
	[TechniqueGroup(TechniqueGroup.Single)]
	[StaticDifficultyLevel(DifficultyLevel.Easy)]
	[BoundStepTypes(typeof(NakedSingleStep))]
	[StaticDifficulty(2.3, ValueInDirectMode = 1.0)]
	NakedSingle,
	#endregion

	//
	// Direct Singles
	//
	#region Direct Singles
	/// <summary>
	/// Indicates crosshatching in block, equivalent to hidden single in block, but used in direct views.
	/// </summary>
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Hidden_Single.html")]
	[TechniqueGroup(TechniqueGroup.Single)]
	[TechniqueFeature(TechniqueFeature.DirectTechniques)]
	[StaticDifficultyLevel(DifficultyLevel.Easy)]
	[BoundStepTypes(typeof(HiddenSingleStep))]
	[StaticDifficulty(1.9)]
	CrosshatchingBlock,

	/// <summary>
	/// Indicates crosshatching in row, equivalent to hidden single in row, but used in direct views.
	/// </summary>
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Hidden_Single.html")]
	[TechniqueGroup(TechniqueGroup.Single)]
	[TechniqueFeature(TechniqueFeature.DirectTechniques)]
	[StaticDifficultyLevel(DifficultyLevel.Easy)]
	[BoundStepTypes(typeof(HiddenSingleStep))]
	[StaticDifficulty(2.3)]
	CrosshatchingRow,

	/// <summary>
	/// Indicates crosshatching in column, equivalent to hidden single in column, but used in direct views.
	/// </summary>
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Hidden_Single.html")]
	[TechniqueGroup(TechniqueGroup.Single)]
	[TechniqueFeature(TechniqueFeature.DirectTechniques)]
	[StaticDifficultyLevel(DifficultyLevel.Easy)]
	[BoundStepTypes(typeof(HiddenSingleStep))]
	[StaticDifficulty(2.3)]
	CrosshatchingColumn,
	#endregion

	//
	// Complex Singles
	//
	#region Complex Singles
	/// <summary>
	/// Indicates full house, with pointing.
	/// </summary>
	[TechniqueGroup(TechniqueGroup.ComplexSingle)]
	[TechniqueFeature(TechniqueFeature.DirectTechniques)]
	[StaticDifficultyLevel(DifficultyLevel.Moderate)]
	[BoundStepTypes(typeof(DirectIntersectionStep))]
	[StaticDifficulty(3.0)]
	[ExtraDifficultyFactors(Size, Locked)]
	PointingFullHouse,

	/// <summary>
	/// Indicates full house, with claiming.
	/// </summary>
	[TechniqueGroup(TechniqueGroup.ComplexSingle)]
	[TechniqueFeature(TechniqueFeature.DirectTechniques)]
	[StaticDifficultyLevel(DifficultyLevel.Moderate)]
	[BoundStepTypes(typeof(DirectIntersectionStep))]
	[StaticDifficulty(3.0)]
	[ExtraDifficultyFactors(Size, Locked)]
	ClaimingFullHouse,

	/// <summary>
	/// Indicates full house, with naked pair.
	/// </summary>
	[TechniqueGroup(TechniqueGroup.ComplexSingle)]
	[TechniqueFeature(TechniqueFeature.DirectTechniques)]
	[StaticDifficultyLevel(DifficultyLevel.Moderate)]
	[BoundStepTypes(typeof(DirectSubsetStep))]
	[StaticDifficulty(3.3)]
	[ExtraDifficultyFactors(Size, Locked)]
	NakedPairFullHouse,

	/// <summary>
	/// Indicates full house, with naked pair (+).
	/// </summary>
	[TechniqueGroup(TechniqueGroup.ComplexSingle)]
	[TechniqueFeature(TechniqueFeature.DirectTechniques)]
	[StaticDifficultyLevel(DifficultyLevel.Moderate)]
	[BoundStepTypes(typeof(DirectSubsetStep))]
	[StaticDifficulty(3.3)]
	[ExtraDifficultyFactors(Size, Locked)]
	NakedPairPlusFullHouse,

	/// <summary>
	/// Indicates full house, with hidden pair.
	/// </summary>
	[TechniqueGroup(TechniqueGroup.ComplexSingle)]
	[TechniqueFeature(TechniqueFeature.DirectTechniques)]
	[StaticDifficultyLevel(DifficultyLevel.Moderate)]
	[BoundStepTypes(typeof(DirectSubsetStep))]
	[StaticDifficulty(3.7)]
	[ExtraDifficultyFactors(Size, Locked)]
	HiddenPairFullHouse,

	/// <summary>
	/// Indicates full house, with locked pair.
	/// </summary>
	[TechniqueGroup(TechniqueGroup.ComplexSingle)]
	[TechniqueFeature(TechniqueFeature.DirectTechniques)]
	[StaticDifficultyLevel(DifficultyLevel.Moderate)]
	[BoundStepTypes(typeof(DirectSubsetStep))]
	[StaticDifficulty(3.3)]
	[ExtraDifficultyFactors(Size, Locked)]
	LockedPairFullHouse,

	/// <summary>
	/// Indicates full house, with locked hidden pair.
	/// </summary>
	[TechniqueGroup(TechniqueGroup.ComplexSingle)]
	[TechniqueFeature(TechniqueFeature.DirectTechniques)]
	[StaticDifficultyLevel(DifficultyLevel.Moderate)]
	[BoundStepTypes(typeof(DirectSubsetStep))]
	[StaticDifficulty(3.7)]
	[ExtraDifficultyFactors(Size, Locked)]
	LockedHiddenPairFullHouse,

	/// <summary>
	/// Indicates full house, with naked triple.
	/// </summary>
	[TechniqueGroup(TechniqueGroup.ComplexSingle)]
	[TechniqueFeature(TechniqueFeature.DirectTechniques)]
	[StaticDifficultyLevel(DifficultyLevel.Moderate)]
	[BoundStepTypes(typeof(DirectSubsetStep))]
	[StaticDifficulty(3.3)]
	[ExtraDifficultyFactors(Size, Locked)]
	NakedTripleFullHouse,

	/// <summary>
	/// Indicates full house, with naked triple (+).
	/// </summary>
	[TechniqueGroup(TechniqueGroup.ComplexSingle)]
	[TechniqueFeature(TechniqueFeature.DirectTechniques)]
	[StaticDifficultyLevel(DifficultyLevel.Moderate)]
	[BoundStepTypes(typeof(DirectSubsetStep))]
	[StaticDifficulty(3.3)]
	[ExtraDifficultyFactors(Size, Locked)]
	NakedTriplePlusFullHouse,

	/// <summary>
	/// Indicates full house, with hidden triple.
	/// </summary>
	[TechniqueGroup(TechniqueGroup.ComplexSingle)]
	[TechniqueFeature(TechniqueFeature.DirectTechniques)]
	[StaticDifficultyLevel(DifficultyLevel.Moderate)]
	[BoundStepTypes(typeof(DirectSubsetStep))]
	[StaticDifficulty(3.7)]
	[ExtraDifficultyFactors(Size, Locked)]
	HiddenTripleFullHouse,

	/// <summary>
	/// Indicates full house, with locked triple.
	/// </summary>
	[TechniqueGroup(TechniqueGroup.ComplexSingle)]
	[TechniqueFeature(TechniqueFeature.DirectTechniques)]
	[StaticDifficultyLevel(DifficultyLevel.Moderate)]
	[BoundStepTypes(typeof(DirectSubsetStep))]
	[StaticDifficulty(3.3)]
	[ExtraDifficultyFactors(Size, Locked)]
	LockedTripleFullHouse,

	/// <summary>
	/// Indicates full house, with locked hidden triple.
	/// </summary>
	[TechniqueGroup(TechniqueGroup.ComplexSingle)]
	[TechniqueFeature(TechniqueFeature.DirectTechniques)]
	[StaticDifficultyLevel(DifficultyLevel.Moderate)]
	[BoundStepTypes(typeof(DirectSubsetStep))]
	[StaticDifficulty(3.7)]
	[ExtraDifficultyFactors(Size, Locked)]
	LockedHiddenTripleFullHouse,

	/// <summary>
	/// Indicates full house, with naked quadruple.
	/// </summary>
	[TechniqueGroup(TechniqueGroup.ComplexSingle)]
	[TechniqueFeature(TechniqueFeature.DirectTechniques)]
	[StaticDifficultyLevel(DifficultyLevel.Moderate)]
	[BoundStepTypes(typeof(DirectSubsetStep))]
	[StaticDifficulty(3.3)]
	[ExtraDifficultyFactors(Size, Locked)]
	NakedQuadrupleFullHouse,

	/// <summary>
	/// Indicates full house, with naked quadruple (+).
	/// </summary>
	[TechniqueGroup(TechniqueGroup.ComplexSingle)]
	[TechniqueFeature(TechniqueFeature.DirectTechniques)]
	[StaticDifficultyLevel(DifficultyLevel.Moderate)]
	[BoundStepTypes(typeof(DirectSubsetStep))]
	[StaticDifficulty(3.3)]
	[ExtraDifficultyFactors(Size, Locked)]
	NakedQuadruplePlusFullHouse,

	/// <summary>
	/// Indicates full house, with hidden quadruple.
	/// </summary>
	[TechniqueGroup(TechniqueGroup.ComplexSingle)]
	[TechniqueFeature(TechniqueFeature.DirectTechniques)]
	[StaticDifficultyLevel(DifficultyLevel.Moderate)]
	[BoundStepTypes(typeof(DirectSubsetStep))]
	[StaticDifficulty(3.7)]
	[ExtraDifficultyFactors(Size, Locked)]
	HiddenQuadrupleFullHouse,

	/// <summary>
	/// Indicates crosshatching in block, with pointing.
	/// </summary>
	[TechniqueGroup(TechniqueGroup.ComplexSingle)]
	[TechniqueFeature(TechniqueFeature.DirectTechniques)]
	[StaticDifficultyLevel(DifficultyLevel.Moderate)]
	[BoundStepTypes(typeof(DirectIntersectionStep))]
	[StaticDifficulty(3.2)]
	[ExtraDifficultyFactors(Size, Locked)]
	PointingCrosshatchingBlock,

	/// <summary>
	/// Indicates crosshatching in block, with claiming.
	/// </summary>
	[TechniqueGroup(TechniqueGroup.ComplexSingle)]
	[TechniqueFeature(TechniqueFeature.DirectTechniques)]
	[StaticDifficultyLevel(DifficultyLevel.Moderate)]
	[BoundStepTypes(typeof(DirectIntersectionStep))]
	[StaticDifficulty(3.2)]
	[ExtraDifficultyFactors(Size, Locked)]
	ClaimingCrosshatchingBlock,

	/// <summary>
	/// Indicates crosshatching in block, with naked pair.
	/// </summary>
	[TechniqueGroup(TechniqueGroup.ComplexSingle)]
	[TechniqueFeature(TechniqueFeature.DirectTechniques)]
	[StaticDifficultyLevel(DifficultyLevel.Moderate)]
	[BoundStepTypes(typeof(DirectSubsetStep))]
	[StaticDifficulty(3.3)]
	[ExtraDifficultyFactors(Size, Locked)]
	NakedPairCrosshatchingBlock,

	/// <summary>
	/// Indicates crosshatching in block, with naked pair (+).
	/// </summary>
	[TechniqueGroup(TechniqueGroup.ComplexSingle)]
	[TechniqueFeature(TechniqueFeature.DirectTechniques)]
	[StaticDifficultyLevel(DifficultyLevel.Moderate)]
	[BoundStepTypes(typeof(DirectSubsetStep))]
	[StaticDifficulty(3.3)]
	[ExtraDifficultyFactors(Size, Locked)]
	NakedPairPlusCrosshatchingBlock,

	/// <summary>
	/// Indicates crosshatching in block, with hidden pair.
	/// </summary>
	[TechniqueGroup(TechniqueGroup.ComplexSingle)]
	[TechniqueFeature(TechniqueFeature.DirectTechniques)]
	[StaticDifficultyLevel(DifficultyLevel.Moderate)]
	[BoundStepTypes(typeof(DirectSubsetStep))]
	[StaticDifficulty(3.7)]
	[ExtraDifficultyFactors(Size, Locked)]
	HiddenPairCrosshatchingBlock,

	/// <summary>
	/// Indicates crosshatching in block, with locked pair.
	/// </summary>
	[TechniqueGroup(TechniqueGroup.ComplexSingle)]
	[TechniqueFeature(TechniqueFeature.DirectTechniques)]
	[StaticDifficultyLevel(DifficultyLevel.Moderate)]
	[BoundStepTypes(typeof(DirectSubsetStep))]
	[StaticDifficulty(3.3)]
	[ExtraDifficultyFactors(Size, Locked)]
	LockedPairCrosshatchingBlock,

	/// <summary>
	/// Indicates crosshatching in block, with locked hidden pair.
	/// </summary>
	[TechniqueGroup(TechniqueGroup.ComplexSingle)]
	[TechniqueFeature(TechniqueFeature.DirectTechniques)]
	[StaticDifficultyLevel(DifficultyLevel.Moderate)]
	[BoundStepTypes(typeof(DirectSubsetStep))]
	[StaticDifficulty(3.7)]
	[ExtraDifficultyFactors(Size, Locked)]
	LockedHiddenPairCrosshatchingBlock,

	/// <summary>
	/// Indicates crosshatching in block, with naked triple.
	/// </summary>
	[TechniqueGroup(TechniqueGroup.ComplexSingle)]
	[TechniqueFeature(TechniqueFeature.DirectTechniques)]
	[StaticDifficultyLevel(DifficultyLevel.Moderate)]
	[BoundStepTypes(typeof(DirectSubsetStep))]
	[StaticDifficulty(3.3)]
	[ExtraDifficultyFactors(Size, Locked)]
	NakedTripleCrosshatchingBlock,

	/// <summary>
	/// Indicates crosshatching in block, with naked triple (+).
	/// </summary>
	[TechniqueGroup(TechniqueGroup.ComplexSingle)]
	[TechniqueFeature(TechniqueFeature.DirectTechniques)]
	[StaticDifficultyLevel(DifficultyLevel.Moderate)]
	[BoundStepTypes(typeof(DirectSubsetStep))]
	[StaticDifficulty(3.3)]
	[ExtraDifficultyFactors(Size, Locked)]
	NakedTriplePlusCrosshatchingBlock,

	/// <summary>
	/// Indicates crosshatching in block, with hidden triple.
	/// </summary>
	[TechniqueGroup(TechniqueGroup.ComplexSingle)]
	[TechniqueFeature(TechniqueFeature.DirectTechniques)]
	[StaticDifficultyLevel(DifficultyLevel.Moderate)]
	[BoundStepTypes(typeof(DirectSubsetStep))]
	[StaticDifficulty(3.7)]
	[ExtraDifficultyFactors(Size, Locked)]
	HiddenTripleCrosshatchingBlock,

	/// <summary>
	/// Indicates crosshatching in block, with locked triple.
	/// </summary>
	[TechniqueGroup(TechniqueGroup.ComplexSingle)]
	[TechniqueFeature(TechniqueFeature.DirectTechniques)]
	[StaticDifficultyLevel(DifficultyLevel.Moderate)]
	[BoundStepTypes(typeof(DirectSubsetStep))]
	[StaticDifficulty(3.3)]
	[ExtraDifficultyFactors(Size, Locked)]
	LockedTripleCrosshatchingBlock,

	/// <summary>
	/// Indicates crosshatching in block, with locked hidden triple.
	/// </summary>
	[TechniqueGroup(TechniqueGroup.ComplexSingle)]
	[TechniqueFeature(TechniqueFeature.DirectTechniques)]
	[StaticDifficultyLevel(DifficultyLevel.Moderate)]
	[BoundStepTypes(typeof(DirectSubsetStep))]
	[StaticDifficulty(3.7)]
	[ExtraDifficultyFactors(Size, Locked)]
	LockedHiddenTripleCrosshatchingBlock,

	/// <summary>
	/// Indicates crosshatching in block, with naked quadruple.
	/// </summary>
	[TechniqueGroup(TechniqueGroup.ComplexSingle)]
	[TechniqueFeature(TechniqueFeature.DirectTechniques)]
	[StaticDifficultyLevel(DifficultyLevel.Moderate)]
	[BoundStepTypes(typeof(DirectSubsetStep))]
	[StaticDifficulty(3.3)]
	[ExtraDifficultyFactors(Size, Locked)]
	NakedQuadrupleCrosshatchingBlock,

	/// <summary>
	/// Indicates crosshatching in block, with naked quadruple (+).
	/// </summary>
	[TechniqueGroup(TechniqueGroup.ComplexSingle)]
	[TechniqueFeature(TechniqueFeature.DirectTechniques)]
	[StaticDifficultyLevel(DifficultyLevel.Moderate)]
	[BoundStepTypes(typeof(DirectSubsetStep))]
	[StaticDifficulty(3.3)]
	[ExtraDifficultyFactors(Size, Locked)]
	NakedQuadruplePlusCrosshatchingBlock,

	/// <summary>
	/// Indicates full house, with hidden quadruple.
	/// </summary>
	[TechniqueGroup(TechniqueGroup.ComplexSingle)]
	[TechniqueFeature(TechniqueFeature.DirectTechniques)]
	[StaticDifficultyLevel(DifficultyLevel.Moderate)]
	[BoundStepTypes(typeof(DirectSubsetStep))]
	[StaticDifficulty(3.7)]
	[ExtraDifficultyFactors(Size, Locked)]
	HiddenQuadrupleCrosshatchingBlock,

	/// <summary>
	/// Indicates crosshatching in row, with pointing.
	/// </summary>
	[TechniqueGroup(TechniqueGroup.ComplexSingle)]
	[TechniqueFeature(TechniqueFeature.DirectTechniques)]
	[StaticDifficultyLevel(DifficultyLevel.Moderate)]
	[BoundStepTypes(typeof(DirectIntersectionStep))]
	[StaticDifficulty(4.5)]
	[ExtraDifficultyFactors(Size, Locked)]
	PointingCrosshatchingRow,

	/// <summary>
	/// Indicates crosshatching in row, with claiming.
	/// </summary>
	[TechniqueGroup(TechniqueGroup.ComplexSingle)]
	[TechniqueFeature(TechniqueFeature.DirectTechniques)]
	[StaticDifficultyLevel(DifficultyLevel.Moderate)]
	[BoundStepTypes(typeof(DirectIntersectionStep))]
	[StaticDifficulty(4.5)]
	[ExtraDifficultyFactors(Size, Locked)]
	ClaimingCrosshatchingRow,

	/// <summary>
	/// Indicates crosshatching in row, with naked pair.
	/// </summary>
	[TechniqueGroup(TechniqueGroup.ComplexSingle)]
	[TechniqueFeature(TechniqueFeature.DirectTechniques)]
	[StaticDifficultyLevel(DifficultyLevel.Moderate)]
	[BoundStepTypes(typeof(DirectSubsetStep))]
	[StaticDifficulty(3.3)]
	[ExtraDifficultyFactors(Size, Locked)]
	NakedPairCrosshatchingRow,

	/// <summary>
	/// Indicates crosshatching in row, with naked pair (+).
	/// </summary>
	[TechniqueGroup(TechniqueGroup.ComplexSingle)]
	[TechniqueFeature(TechniqueFeature.DirectTechniques)]
	[StaticDifficultyLevel(DifficultyLevel.Moderate)]
	[BoundStepTypes(typeof(DirectSubsetStep))]
	[StaticDifficulty(3.3)]
	[ExtraDifficultyFactors(Size, Locked)]
	NakedPairPlusCrosshatchingRow,

	/// <summary>
	/// Indicates crosshatching in row, with hidden pair.
	/// </summary>
	[TechniqueGroup(TechniqueGroup.ComplexSingle)]
	[TechniqueFeature(TechniqueFeature.DirectTechniques)]
	[StaticDifficultyLevel(DifficultyLevel.Moderate)]
	[BoundStepTypes(typeof(DirectSubsetStep))]
	[StaticDifficulty(3.7)]
	[ExtraDifficultyFactors(Size, Locked)]
	HiddenPairCrosshatchingRow,

	/// <summary>
	/// Indicates crosshatching in row, with locked pair.
	/// </summary>
	[TechniqueGroup(TechniqueGroup.ComplexSingle)]
	[TechniqueFeature(TechniqueFeature.DirectTechniques)]
	[StaticDifficultyLevel(DifficultyLevel.Moderate)]
	[BoundStepTypes(typeof(DirectSubsetStep))]
	[StaticDifficulty(3.3)]
	[ExtraDifficultyFactors(Size, Locked)]
	LockedPairCrosshatchingRow,

	/// <summary>
	/// Indicates crosshatching in row, with locked hidden pair.
	/// </summary>
	[TechniqueGroup(TechniqueGroup.ComplexSingle)]
	[TechniqueFeature(TechniqueFeature.DirectTechniques)]
	[StaticDifficultyLevel(DifficultyLevel.Moderate)]
	[BoundStepTypes(typeof(DirectSubsetStep))]
	[StaticDifficulty(3.7)]
	[ExtraDifficultyFactors(Size, Locked)]
	LockedHiddenPairCrosshatchingRow,

	/// <summary>
	/// Indicates crosshatching in row, with naked triple.
	/// </summary>
	[TechniqueGroup(TechniqueGroup.ComplexSingle)]
	[TechniqueFeature(TechniqueFeature.DirectTechniques)]
	[StaticDifficultyLevel(DifficultyLevel.Moderate)]
	[BoundStepTypes(typeof(DirectSubsetStep))]
	[StaticDifficulty(3.3)]
	[ExtraDifficultyFactors(Size, Locked)]
	NakedTripleCrosshatchingRow,

	/// <summary>
	/// Indicates crosshatching in row, with naked triple (+).
	/// </summary>
	[TechniqueGroup(TechniqueGroup.ComplexSingle)]
	[TechniqueFeature(TechniqueFeature.DirectTechniques)]
	[StaticDifficultyLevel(DifficultyLevel.Moderate)]
	[BoundStepTypes(typeof(DirectSubsetStep))]
	[StaticDifficulty(3.3)]
	[ExtraDifficultyFactors(Size, Locked)]
	NakedTriplePlusCrosshatchingRow,

	/// <summary>
	/// Indicates crosshatching in row, with hidden triple.
	/// </summary>
	[TechniqueGroup(TechniqueGroup.ComplexSingle)]
	[TechniqueFeature(TechniqueFeature.DirectTechniques)]
	[StaticDifficultyLevel(DifficultyLevel.Moderate)]
	[BoundStepTypes(typeof(DirectSubsetStep))]
	[StaticDifficulty(3.7)]
	[ExtraDifficultyFactors(Size, Locked)]
	HiddenTripleCrosshatchingRow,

	/// <summary>
	/// Indicates crosshatching in row, with locked triple.
	/// </summary>
	[TechniqueGroup(TechniqueGroup.ComplexSingle)]
	[TechniqueFeature(TechniqueFeature.DirectTechniques)]
	[StaticDifficultyLevel(DifficultyLevel.Moderate)]
	[BoundStepTypes(typeof(DirectSubsetStep))]
	[StaticDifficulty(3.3)]
	[ExtraDifficultyFactors(Size, Locked)]
	LockedTripleCrosshatchingRow,

	/// <summary>
	/// Indicates crosshatching in row, with locked hidden triple.
	/// </summary>
	[TechniqueGroup(TechniqueGroup.ComplexSingle)]
	[TechniqueFeature(TechniqueFeature.DirectTechniques)]
	[StaticDifficultyLevel(DifficultyLevel.Moderate)]
	[BoundStepTypes(typeof(DirectSubsetStep))]
	[StaticDifficulty(3.7)]
	[ExtraDifficultyFactors(Size, Locked)]
	LockedHiddenTripleCrosshatchingRow,

	/// <summary>
	/// Indicates crosshatching in row, with naked quadruple.
	/// </summary>
	[TechniqueGroup(TechniqueGroup.ComplexSingle)]
	[TechniqueFeature(TechniqueFeature.DirectTechniques)]
	[StaticDifficultyLevel(DifficultyLevel.Moderate)]
	[BoundStepTypes(typeof(DirectSubsetStep))]
	[StaticDifficulty(3.3)]
	[ExtraDifficultyFactors(Size, Locked)]
	NakedQuadrupleCrosshatchingRow,

	/// <summary>
	/// Indicates crosshatching in row, with naked quadruple (+).
	/// </summary>
	[TechniqueGroup(TechniqueGroup.ComplexSingle)]
	[TechniqueFeature(TechniqueFeature.DirectTechniques)]
	[StaticDifficultyLevel(DifficultyLevel.Moderate)]
	[BoundStepTypes(typeof(DirectSubsetStep))]
	[StaticDifficulty(3.3)]
	[ExtraDifficultyFactors(Size, Locked)]
	NakedQuadruplePlusCrosshatchingRow,

	/// <summary>
	/// Indicates full house, with hidden quadruple.
	/// </summary>
	[TechniqueGroup(TechniqueGroup.ComplexSingle)]
	[TechniqueFeature(TechniqueFeature.DirectTechniques)]
	[StaticDifficultyLevel(DifficultyLevel.Moderate)]
	[BoundStepTypes(typeof(DirectSubsetStep))]
	[StaticDifficulty(3.7)]
	[ExtraDifficultyFactors(Size, Locked)]
	HiddenQuadrupleCrosshatchingRow,

	/// <summary>
	/// Indicates crosshatching in column, with pointing.
	/// </summary>
	[TechniqueGroup(TechniqueGroup.ComplexSingle)]
	[TechniqueFeature(TechniqueFeature.DirectTechniques)]
	[StaticDifficultyLevel(DifficultyLevel.Moderate)]
	[BoundStepTypes(typeof(DirectIntersectionStep))]
	[StaticDifficulty(4.5)]
	[ExtraDifficultyFactors(Size, Locked)]
	PointingCrosshatchingColumn,

	/// <summary>
	/// Indicates crosshatching in column, with claiming.
	/// </summary>
	[TechniqueGroup(TechniqueGroup.ComplexSingle)]
	[TechniqueFeature(TechniqueFeature.DirectTechniques)]
	[StaticDifficultyLevel(DifficultyLevel.Moderate)]
	[BoundStepTypes(typeof(DirectIntersectionStep))]
	[StaticDifficulty(4.5)]
	[ExtraDifficultyFactors(Size, Locked)]
	ClaimingCrosshatchingColumn,

	/// <summary>
	/// Indicates crosshatching in column, with naked pair.
	/// </summary>
	[TechniqueGroup(TechniqueGroup.ComplexSingle)]
	[TechniqueFeature(TechniqueFeature.DirectTechniques)]
	[StaticDifficultyLevel(DifficultyLevel.Moderate)]
	[BoundStepTypes(typeof(DirectSubsetStep))]
	[StaticDifficulty(3.3)]
	[ExtraDifficultyFactors(Size, Locked)]
	NakedPairCrosshatchingColumn,

	/// <summary>
	/// Indicates crosshatching in column, with naked pair (+).
	/// </summary>
	[TechniqueGroup(TechniqueGroup.ComplexSingle)]
	[TechniqueFeature(TechniqueFeature.DirectTechniques)]
	[StaticDifficultyLevel(DifficultyLevel.Moderate)]
	[BoundStepTypes(typeof(DirectSubsetStep))]
	[StaticDifficulty(3.3)]
	[ExtraDifficultyFactors(Size, Locked)]
	NakedPairPlusCrosshatchingColumn,

	/// <summary>
	/// Indicates crosshatching in column, with hidden pair.
	/// </summary>
	[TechniqueGroup(TechniqueGroup.ComplexSingle)]
	[TechniqueFeature(TechniqueFeature.DirectTechniques)]
	[StaticDifficultyLevel(DifficultyLevel.Moderate)]
	[BoundStepTypes(typeof(DirectSubsetStep))]
	[StaticDifficulty(3.7)]
	[ExtraDifficultyFactors(Size, Locked)]
	HiddenPairCrosshatchingColumn,

	/// <summary>
	/// Indicates crosshatching in column, with locked pair.
	/// </summary>
	[TechniqueGroup(TechniqueGroup.ComplexSingle)]
	[TechniqueFeature(TechniqueFeature.DirectTechniques)]
	[StaticDifficultyLevel(DifficultyLevel.Moderate)]
	[BoundStepTypes(typeof(DirectSubsetStep))]
	[StaticDifficulty(3.3)]
	[ExtraDifficultyFactors(Size, Locked)]
	LockedPairCrosshatchingColumn,

	/// <summary>
	/// Indicates crosshatching in column, with locked hidden pair.
	/// </summary>
	[TechniqueGroup(TechniqueGroup.ComplexSingle)]
	[TechniqueFeature(TechniqueFeature.DirectTechniques)]
	[StaticDifficultyLevel(DifficultyLevel.Moderate)]
	[BoundStepTypes(typeof(DirectSubsetStep))]
	[StaticDifficulty(3.7)]
	[ExtraDifficultyFactors(Size, Locked)]
	LockedHiddenPairCrosshatchingColumn,

	/// <summary>
	/// Indicates crosshatching in column, with naked triple.
	/// </summary>
	[TechniqueGroup(TechniqueGroup.ComplexSingle)]
	[TechniqueFeature(TechniqueFeature.DirectTechniques)]
	[StaticDifficultyLevel(DifficultyLevel.Moderate)]
	[BoundStepTypes(typeof(DirectSubsetStep))]
	[StaticDifficulty(3.3)]
	[ExtraDifficultyFactors(Size, Locked)]
	NakedTripleCrosshatchingColumn,

	/// <summary>
	/// Indicates crosshatching in column, with naked triple (+).
	/// </summary>
	[TechniqueGroup(TechniqueGroup.ComplexSingle)]
	[TechniqueFeature(TechniqueFeature.DirectTechniques)]
	[StaticDifficultyLevel(DifficultyLevel.Moderate)]
	[BoundStepTypes(typeof(DirectSubsetStep))]
	[StaticDifficulty(3.3)]
	[ExtraDifficultyFactors(Size, Locked)]
	NakedTriplePlusCrosshatchingColumn,

	/// <summary>
	/// Indicates crosshatching in column, with hidden triple.
	/// </summary>
	[TechniqueGroup(TechniqueGroup.ComplexSingle)]
	[TechniqueFeature(TechniqueFeature.DirectTechniques)]
	[StaticDifficultyLevel(DifficultyLevel.Moderate)]
	[BoundStepTypes(typeof(DirectSubsetStep))]
	[StaticDifficulty(3.7)]
	[ExtraDifficultyFactors(Size, Locked)]
	HiddenTripleCrosshatchingColumn,

	/// <summary>
	/// Indicates crosshatching in column, with locked triple.
	/// </summary>
	[TechniqueGroup(TechniqueGroup.ComplexSingle)]
	[TechniqueFeature(TechniqueFeature.DirectTechniques)]
	[StaticDifficultyLevel(DifficultyLevel.Moderate)]
	[BoundStepTypes(typeof(DirectSubsetStep))]
	[StaticDifficulty(3.3)]
	[ExtraDifficultyFactors(Size, Locked)]
	LockedTripleCrosshatchingColumn,

	/// <summary>
	/// Indicates crosshatching in column, with locked hidden triple.
	/// </summary>
	[TechniqueGroup(TechniqueGroup.ComplexSingle)]
	[TechniqueFeature(TechniqueFeature.DirectTechniques)]
	[StaticDifficultyLevel(DifficultyLevel.Moderate)]
	[BoundStepTypes(typeof(DirectSubsetStep))]
	[StaticDifficulty(3.7)]
	[ExtraDifficultyFactors(Size, Locked)]
	LockedHiddenTripleCrosshatchingColumn,

	/// <summary>
	/// Indicates crosshatching in column, with naked quadruple.
	/// </summary>
	[TechniqueGroup(TechniqueGroup.ComplexSingle)]
	[TechniqueFeature(TechniqueFeature.DirectTechniques)]
	[StaticDifficultyLevel(DifficultyLevel.Moderate)]
	[BoundStepTypes(typeof(DirectSubsetStep))]
	[StaticDifficulty(3.3)]
	[ExtraDifficultyFactors(Size, Locked)]
	NakedQuadrupleCrosshatchingColumn,

	/// <summary>
	/// Indicates crosshatching in column, with naked quadruple (+).
	/// </summary>
	[TechniqueGroup(TechniqueGroup.ComplexSingle)]
	[TechniqueFeature(TechniqueFeature.DirectTechniques)]
	[StaticDifficultyLevel(DifficultyLevel.Moderate)]
	[BoundStepTypes(typeof(DirectSubsetStep))]
	[StaticDifficulty(3.3)]
	[ExtraDifficultyFactors(Size, Locked)]
	NakedQuadruplePlusCrosshatchingColumn,

	/// <summary>
	/// Indicates full house, with hidden quadruple.
	/// </summary>
	[TechniqueGroup(TechniqueGroup.ComplexSingle)]
	[TechniqueFeature(TechniqueFeature.DirectTechniques)]
	[StaticDifficultyLevel(DifficultyLevel.Moderate)]
	[BoundStepTypes(typeof(DirectSubsetStep))]
	[StaticDifficulty(3.7)]
	[ExtraDifficultyFactors(Size, Locked)]
	HiddenQuadrupleCrosshatchingColumn,

	/// <summary>
	/// Indicates naked single, with pointing.
	/// </summary>
	[TechniqueGroup(TechniqueGroup.ComplexSingle)]
	[TechniqueFeature(TechniqueFeature.DirectTechniques)]
	[StaticDifficultyLevel(DifficultyLevel.Moderate)]
	[BoundStepTypes(typeof(DirectIntersectionStep))]
	[StaticDifficulty(5.3)]
	[ExtraDifficultyFactors(Size, Locked)]
	PointingNakedSingle,

	/// <summary>
	/// Indicates naked single, with claiming.
	/// </summary>
	[TechniqueGroup(TechniqueGroup.ComplexSingle)]
	[TechniqueFeature(TechniqueFeature.DirectTechniques)]
	[StaticDifficultyLevel(DifficultyLevel.Moderate)]
	[BoundStepTypes(typeof(DirectIntersectionStep))]
	[StaticDifficulty(5.3)]
	[ExtraDifficultyFactors(Size, Locked)]
	ClaimingNakedSingle,

	/// <summary>
	/// Indicates naked single, with naked pair.
	/// </summary>
	[TechniqueGroup(TechniqueGroup.ComplexSingle)]
	[TechniqueFeature(TechniqueFeature.DirectTechniques)]
	[StaticDifficultyLevel(DifficultyLevel.Moderate)]
	[BoundStepTypes(typeof(DirectSubsetStep))]
	[StaticDifficulty(3.3)]
	[ExtraDifficultyFactors(Size, Locked)]
	NakedPairNakedSingle,

	/// <summary>
	/// Indicates naked single, with naked pair (+).
	/// </summary>
	[TechniqueGroup(TechniqueGroup.ComplexSingle)]
	[TechniqueFeature(TechniqueFeature.DirectTechniques)]
	[StaticDifficultyLevel(DifficultyLevel.Moderate)]
	[BoundStepTypes(typeof(DirectSubsetStep))]
	[StaticDifficulty(3.3)]
	[ExtraDifficultyFactors(Size, Locked)]
	NakedPairPlusNakedSingle,

	/// <summary>
	/// Indicates naked single, with hidden pair.
	/// </summary>
	[TechniqueGroup(TechniqueGroup.ComplexSingle)]
	[TechniqueFeature(TechniqueFeature.DirectTechniques)]
	[StaticDifficultyLevel(DifficultyLevel.Moderate)]
	[BoundStepTypes(typeof(DirectSubsetStep))]
	[StaticDifficulty(3.7)]
	[ExtraDifficultyFactors(Size, Locked)]
	HiddenPairNakedSingle,

	/// <summary>
	/// Indicates naked single, with locked pair.
	/// </summary>
	[TechniqueGroup(TechniqueGroup.ComplexSingle)]
	[TechniqueFeature(TechniqueFeature.DirectTechniques)]
	[StaticDifficultyLevel(DifficultyLevel.Moderate)]
	[BoundStepTypes(typeof(DirectSubsetStep))]
	[StaticDifficulty(3.3)]
	[ExtraDifficultyFactors(Size, Locked)]
	LockedPairNakedSingle,

	/// <summary>
	/// Indicates naked single, with locked hidden pair.
	/// </summary>
	[TechniqueGroup(TechniqueGroup.ComplexSingle)]
	[TechniqueFeature(TechniqueFeature.DirectTechniques)]
	[StaticDifficultyLevel(DifficultyLevel.Moderate)]
	[BoundStepTypes(typeof(DirectSubsetStep))]
	[StaticDifficulty(3.7)]
	[ExtraDifficultyFactors(Size, Locked)]
	LockedHiddenPairNakedSingle,

	/// <summary>
	/// Indicates naked single, with naked triple.
	/// </summary>
	[TechniqueGroup(TechniqueGroup.ComplexSingle)]
	[TechniqueFeature(TechniqueFeature.DirectTechniques)]
	[StaticDifficultyLevel(DifficultyLevel.Moderate)]
	[BoundStepTypes(typeof(DirectSubsetStep))]
	[StaticDifficulty(3.3)]
	[ExtraDifficultyFactors(Size, Locked)]
	NakedTripleNakedSingle,

	/// <summary>
	/// Indicates naked single, with naked triple (+).
	/// </summary>
	[TechniqueGroup(TechniqueGroup.ComplexSingle)]
	[TechniqueFeature(TechniqueFeature.DirectTechniques)]
	[StaticDifficultyLevel(DifficultyLevel.Moderate)]
	[BoundStepTypes(typeof(DirectSubsetStep))]
	[StaticDifficulty(3.3)]
	[ExtraDifficultyFactors(Size, Locked)]
	NakedTriplePlusNakedSingle,

	/// <summary>
	/// Indicates naked single, with hidden triple.
	/// </summary>
	[TechniqueGroup(TechniqueGroup.ComplexSingle)]
	[TechniqueFeature(TechniqueFeature.DirectTechniques)]
	[StaticDifficultyLevel(DifficultyLevel.Moderate)]
	[BoundStepTypes(typeof(DirectSubsetStep))]
	[StaticDifficulty(3.7)]
	[ExtraDifficultyFactors(Size, Locked)]
	HiddenTripleNakedSingle,

	/// <summary>
	/// Indicates naked single, with locked triple.
	/// </summary>
	[TechniqueGroup(TechniqueGroup.ComplexSingle)]
	[TechniqueFeature(TechniqueFeature.DirectTechniques)]
	[StaticDifficultyLevel(DifficultyLevel.Moderate)]
	[BoundStepTypes(typeof(DirectSubsetStep))]
	[StaticDifficulty(3.3)]
	[ExtraDifficultyFactors(Size, Locked)]
	LockedTripleNakedSingle,

	/// <summary>
	/// Indicates naked single, with locked hidden triple.
	/// </summary>
	[TechniqueGroup(TechniqueGroup.ComplexSingle)]
	[TechniqueFeature(TechniqueFeature.DirectTechniques)]
	[StaticDifficultyLevel(DifficultyLevel.Moderate)]
	[BoundStepTypes(typeof(DirectSubsetStep))]
	[StaticDifficulty(3.7)]
	[ExtraDifficultyFactors(Size, Locked)]
	LockedHiddenTripleNakedSingle,

	/// <summary>
	/// Indicates naked single, with naked quadruple.
	/// </summary>
	[TechniqueGroup(TechniqueGroup.ComplexSingle)]
	[TechniqueFeature(TechniqueFeature.DirectTechniques)]
	[StaticDifficultyLevel(DifficultyLevel.Moderate)]
	[BoundStepTypes(typeof(DirectSubsetStep))]
	[StaticDifficulty(3.3)]
	[ExtraDifficultyFactors(Size, Locked)]
	NakedQuadrupleNakedSingle,

	/// <summary>
	/// Indicates naked single, with naked quadruple (+).
	/// </summary>
	[TechniqueGroup(TechniqueGroup.ComplexSingle)]
	[TechniqueFeature(TechniqueFeature.DirectTechniques)]
	[StaticDifficultyLevel(DifficultyLevel.Moderate)]
	[BoundStepTypes(typeof(DirectSubsetStep))]
	[StaticDifficulty(3.3)]
	[ExtraDifficultyFactors(Size, Locked)]
	NakedQuadruplePlusNakedSingle,

	/// <summary>
	/// Indicates full house, with hidden quadruple.
	/// </summary>
	[TechniqueGroup(TechniqueGroup.ComplexSingle)]
	[TechniqueFeature(TechniqueFeature.DirectTechniques)]
	[StaticDifficultyLevel(DifficultyLevel.Moderate)]
	[BoundStepTypes(typeof(DirectSubsetStep))]
	[StaticDifficulty(3.7)]
	[ExtraDifficultyFactors(Size, Locked)]
	HiddenQuadrupleNakedSingle,
	#endregion

	//
	// Locked Candidates
	//
	#region Locked Candidates
	/// <summary>
	/// Indicates pointing.
	/// </summary>
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Locked_Candidates.html")]
	[HodokuTechniquePrefix("0100")]
	[HodokuDifficultyRating(50, HodokuDifficultyLevel.Medium)]
	[HodokuAliasedNames("Locked Candidates Type 1")]
	[SudokuExplainerTechnique(SudokuExplainerTechnique.Pointing)]
	[SudokuExplainerDifficultyRating(2.6)]
	[TechniqueGroup(TechniqueGroup.LockedCandidates)]
	[StaticDifficultyLevel(DifficultyLevel.Moderate)]
	[BoundStepTypes(typeof(LockedCandidatesStep))]
	[StaticDifficulty(2.6)]
	Pointing,

	/// <summary>
	/// Indicates claiming.
	/// </summary>
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Locked_Candidates.html")]
	[HodokuTechniquePrefix("0101")]
	[HodokuDifficultyRating(50, HodokuDifficultyLevel.Medium)]
	[HodokuAliasedNames("Locked Candidates Type 2")]
	[SudokuExplainerTechnique(SudokuExplainerTechnique.Claiming)]
	[SudokuExplainerDifficultyRating(2.8)]
	[TechniqueGroup(TechniqueGroup.LockedCandidates)]
	[StaticDifficultyLevel(DifficultyLevel.Moderate)]
	[BoundStepTypes(typeof(LockedCandidatesStep))]
	[StaticDifficulty(2.8)]
	Claiming,

	/// <summary>
	/// Indicates law of leftover.
	/// </summary>
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Law_of_Leftovers.html")]
	[TechniqueFeature(TechniqueFeature.DirectTechniques)]
	[TechniqueGroup(TechniqueGroup.LockedCandidates)]
	[StaticDifficultyLevel(DifficultyLevel.Moderate)]
	[Abbreviation("LoL")]
	[BoundStepTypes(typeof(LawOfLeftoverStep))]
	[StaticDifficulty(2.0)]
	LawOfLeftover,
	#endregion

	//
	// Subsets
	//
	#region Subsets
	/// <summary>
	/// Indicates naked pair.
	/// </summary>
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Naked_Pair.html")]
	[HodokuTechniquePrefix("0200")]
	[HodokuDifficultyRating(60, HodokuDifficultyLevel.Medium)]
	[SudokuExplainerTechnique(SudokuExplainerTechnique.NakedPair)]
	[SudokuExplainerDifficultyRating(3.0)]
	[TechniqueGroup(TechniqueGroup.Subset)]
	[StaticDifficultyLevel(DifficultyLevel.Moderate)]
	[BoundStepTypes(typeof(NakedSubsetStep))]
	[StaticDifficulty(3.0)]
	[ExtraDifficultyFactors(Size, Locked)]
	NakedPair,

	/// <summary>
	/// Indicates naked pair plus (naked pair (+)).
	/// </summary>
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Naked_Pair.html")]
	[TechniqueGroup(TechniqueGroup.Subset)]
	[StaticDifficultyLevel(DifficultyLevel.Moderate)]
	[BoundStepTypes(typeof(NakedSubsetStep))]
	[StaticDifficulty(3.0)]
	[ExtraDifficultyFactors(Size, Locked)]
	NakedPairPlus,

	/// <summary>
	/// Indicates locked pair.
	/// </summary>
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Locked_Pair.html")]
	[HodokuTechniquePrefix("0110-1")]
	[HodokuDifficultyRating(40, HodokuDifficultyLevel.Medium)]
#if COMPATIBLE_ORIGINAL_TECHNIQUE_RULES
	[SudokuExplainerTechnique(SudokuExplainerTechnique.DirectHiddenPair)]
	[SudokuExplainerDifficultyRating(2.0)]
	[SudokuExplainerAliasNames("Direct Hidden Pair")]
#endif
	[TechniqueGroup(TechniqueGroup.Subset)]
	[StaticDifficultyLevel(DifficultyLevel.Moderate)]
	[BoundStepTypes(typeof(NakedSubsetStep))]
	[StaticDifficulty(3.0)]
	[ExtraDifficultyFactors(Size, Locked)]
	LockedPair,

	/// <summary>
	/// Indicates hidden pair.
	/// </summary>
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Hidden_Pair.html")]
	[HodokuTechniquePrefix("0210")]
	[HodokuDifficultyRating(70, HodokuDifficultyLevel.Medium)]
	[SudokuExplainerTechnique(SudokuExplainerTechnique.HiddenPair)]
	[SudokuExplainerDifficultyRating(3.4)]
	[TechniqueGroup(TechniqueGroup.Subset)]
	[StaticDifficultyLevel(DifficultyLevel.Moderate)]
	[BoundStepTypes(typeof(HiddenSubsetStep))]
	[StaticDifficulty(3.4)]
	[ExtraDifficultyFactors(Size, Locked)]
	HiddenPair,

	/// <summary>
	/// Indicates locked hidden pair.
	/// </summary>
	[HodokuTechniquePrefix("0110-2")]
	[TechniqueGroup(TechniqueGroup.Subset)]
	[StaticDifficultyLevel(DifficultyLevel.Moderate)]
	[BoundStepTypes(typeof(HiddenSubsetStep))]
	[StaticDifficulty(3.4)]
	[ExtraDifficultyFactors(Size, Locked)]
	LockedHiddenPair,

	/// <summary>
	/// Indicates naked triple.
	/// </summary>
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Naked_Triple.html")]
	[HodokuTechniquePrefix("0201")]
	[HodokuDifficultyRating(80, HodokuDifficultyLevel.Medium)]
	[SudokuExplainerTechnique(SudokuExplainerTechnique.NakedTriplet)]
	[SudokuExplainerDifficultyRating(3.6)]
	[SudokuExplainerNames("Naked Triplet")]
	[TechniqueGroup(TechniqueGroup.Subset)]
	[StaticDifficultyLevel(DifficultyLevel.Moderate)]
	[BoundStepTypes(typeof(NakedSubsetStep))]
	[StaticDifficulty(3.0)]
	[ExtraDifficultyFactors(Size, Locked)]
	NakedTriple,

	/// <summary>
	/// Indicates naked triple plus (naked triple (+)).
	/// </summary>
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Naked_Triple.html")]
	[TechniqueGroup(TechniqueGroup.Subset)]
	[StaticDifficultyLevel(DifficultyLevel.Moderate)]
	[BoundStepTypes(typeof(NakedSubsetStep))]
	[StaticDifficulty(3.0)]
	[ExtraDifficultyFactors(Size, Locked)]
	NakedTriplePlus,

	/// <summary>
	/// Indicates locked triple.
	/// </summary>
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Locked_Triple.html")]
	[HodokuTechniquePrefix("0111-1")]
	[HodokuDifficultyRating(60, HodokuDifficultyLevel.Medium)]
#if COMPATIBLE_ORIGINAL_TECHNIQUE_RULES
	[SudokuExplainerTechnique(SudokuExplainerTechnique.DirectHiddenTriplet)]
	[SudokuExplainerDifficultyRating(2.5)]
	[SudokuExplainerAliasNames("Direct Hidden Triplet")]
#endif
	[TechniqueGroup(TechniqueGroup.Subset)]
	[StaticDifficultyLevel(DifficultyLevel.Moderate)]
	[BoundStepTypes(typeof(NakedSubsetStep))]
	[StaticDifficulty(3.0)]
	[ExtraDifficultyFactors(Size, Locked)]
	LockedTriple,

	/// <summary>
	/// Indicates hidden triple.
	/// </summary>
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Hidden_Triple.html")]
	[HodokuTechniquePrefix("0211")]
	[HodokuDifficultyRating(100, HodokuDifficultyLevel.Medium)]
	[SudokuExplainerTechnique(SudokuExplainerTechnique.HiddenTriplet)]
	[SudokuExplainerDifficultyRating(4.0)]
	[SudokuExplainerNames("Hidden Triplet")]
	[TechniqueGroup(TechniqueGroup.Subset)]
	[StaticDifficultyLevel(DifficultyLevel.Moderate)]
	[BoundStepTypes(typeof(HiddenSubsetStep))]
	[StaticDifficulty(3.4)]
	[ExtraDifficultyFactors(Size, Locked)]
	HiddenTriple,

	/// <summary>
	/// Indicates locked hidden triple.
	/// </summary>
	[HodokuTechniquePrefix("0111-2")]
	[TechniqueGroup(TechniqueGroup.Subset)]
	[StaticDifficultyLevel(DifficultyLevel.Moderate)]
	[BoundStepTypes(typeof(HiddenSubsetStep))]
	[StaticDifficulty(3.4)]
	[ExtraDifficultyFactors(Size, Locked)]
	LockedHiddenTriple,

	/// <summary>
	/// Indicates naked quadruple.
	/// </summary>
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Naked_Quad.html")]
	[HodokuTechniquePrefix("0202")]
	[HodokuDifficultyRating(120, HodokuDifficultyLevel.Hard)]
	[SudokuExplainerTechnique(SudokuExplainerTechnique.NakedQuad)]
	[SudokuExplainerDifficultyRating(5.0)]
	[SudokuExplainerNames("Naked Quad")]
	[TechniqueGroup(TechniqueGroup.Subset)]
	[StaticDifficultyLevel(DifficultyLevel.Moderate)]
	[BoundStepTypes(typeof(NakedSubsetStep))]
	[StaticDifficulty(3.0)]
	[ExtraDifficultyFactors(Size, Locked)]
	NakedQuadruple,

	/// <summary>
	/// Indicates naked quadruple plus (naked quadruple (+)).
	/// </summary>
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Naked_Quad.html")]
	[TechniqueGroup(TechniqueGroup.Subset)]
	[StaticDifficultyLevel(DifficultyLevel.Moderate)]
	[BoundStepTypes(typeof(NakedSubsetStep))]
	[StaticDifficulty(3.0)]
	[ExtraDifficultyFactors(Size, Locked)]
	NakedQuadruplePlus,

	/// <summary>
	/// Indicates hidden quadruple.
	/// </summary>
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Hidden_Quad.html")]
	[HodokuTechniquePrefix("0212")]
	[HodokuDifficultyRating(150, HodokuDifficultyLevel.Hard)]
	[SudokuExplainerTechnique(SudokuExplainerTechnique.HiddenQuad)]
	[SudokuExplainerDifficultyRating(5.4)]
	[SudokuExplainerNames("Hidden Quad")]
	[TechniqueGroup(TechniqueGroup.Subset)]
	[StaticDifficultyLevel(DifficultyLevel.Moderate)]
	[BoundStepTypes(typeof(HiddenSubsetStep))]
	[StaticDifficulty(3.4)]
	[ExtraDifficultyFactors(Size, Locked)]
	HiddenQuadruple,
	#endregion

	//
	// Fishes
	//
	#region Fishes
	/// <summary>
	/// Indicates X-Wing.
	/// </summary>
	[ReferenceLink("http://sudopedia.enjoysudoku.com/X-Wing.html")]
	[ReferenceLink("http://forum.enjoysudoku.com/the-ultimate-fish-guide-t4993.html")]
	[HodokuTechniquePrefix("0300")]
	[HodokuDifficultyRating(140, HodokuDifficultyLevel.Hard)]
	[SudokuExplainerTechnique(SudokuExplainerTechnique.XWing)]
	[SudokuExplainerDifficultyRating(3.2)]
	[TechniqueGroup(TechniqueGroup.NormalFish)]
	[StaticDifficultyLevel(DifficultyLevel.Hard)]
	[BoundStepTypes(typeof(NormalFishStep))]
	[StaticDifficulty(3.2)]
	[ExtraDifficultyFactors(Size, Sashimi)]
	XWing,

	/// <summary>
	/// Indicates finned X-Wing.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/the-ultimate-fish-guide-t4993.html")]
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Finned_Fish.html")]
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Finned_X-Wing.html")]
	[ReferenceLink("http://forum.enjoysudoku.com/viewtopic.php?t=2793")]
	[HodokuTechniquePrefix("0310")]
	[HodokuDifficultyRating(130, HodokuDifficultyLevel.Hard)]
	[SudokuExplainerDifficultyRating(3.4, IsAdvancedDefined = true)]
	[TechniqueGroup(TechniqueGroup.NormalFish)]
	[StaticDifficultyLevel(DifficultyLevel.Hard)]
	[BoundStepTypes(typeof(NormalFishStep))]
	[StaticDifficulty(3.2)]
	[ExtraDifficultyFactors(Size, Sashimi)]
	FinnedXWing,

	/// <summary>
	/// Indicates sashimi X-Wing.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/the-ultimate-fish-guide-t4993.html")]
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Sashimi_Fish.html")]
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Sashimi_X-Wing.html")]
	[HodokuTechniquePrefix("0320")]
	[HodokuDifficultyRating(150, HodokuDifficultyLevel.Hard)]
	[SudokuExplainerDifficultyRating(3.5, IsAdvancedDefined = true)]
	[TechniqueGroup(TechniqueGroup.NormalFish)]
	[StaticDifficultyLevel(DifficultyLevel.Hard)]
	[BoundStepTypes(typeof(NormalFishStep))]
	[StaticDifficulty(3.2)]
	[ExtraDifficultyFactors(Size, Sashimi)]
	SashimiXWing,

	/// <summary>
	/// Indicates Siamese finned X-Wing.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/the-ultimate-fish-guide-t4993.html")]
	[ReferenceLink("http://forum.enjoysudoku.com/viewtopic.php?t=2793")]
	[TechniqueGroup(TechniqueGroup.NormalFish)]
	[StaticDifficultyLevel(DifficultyLevel.Hard)]
	[BoundStepTypes(typeof(NormalFishStep))]
	[StaticDifficulty(3.2)]
	[ExtraDifficultyFactors(Size, Sashimi)]
	SiameseFinnedXWing,

	/// <summary>
	/// Indicates Siamese sashimi X-Wing.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/the-ultimate-fish-guide-t4993.html")]
	[TechniqueGroup(TechniqueGroup.NormalFish)]
	[StaticDifficultyLevel(DifficultyLevel.Hard)]
	[BoundStepTypes(typeof(NormalFishStep))]
	[StaticDifficulty(3.2)]
	[ExtraDifficultyFactors(Size, Sashimi)]
	SiameseSashimiXWing,

	/// <summary>
	/// Indicates franken X-Wing.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/the-ultimate-fish-guide-t4993.html")]
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Franken_Fish.html")]
	[HodokuTechniquePrefix("0330")]
	[HodokuDifficultyRating(300, HodokuDifficultyLevel.Unfair)]
	[TechniqueGroup(TechniqueGroup.ComplexFish)]
	[StaticDifficultyLevel(DifficultyLevel.Hard)]
	[BoundStepTypes(typeof(ComplexFishStep))]
	[StaticDifficulty(3.2)]
	[ExtraDifficultyFactors(Size, Sashimi, FishShape, Cannibalism)]
	FrankenXWing,

	/// <summary>
	/// Indicates finned franken X-Wing.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/the-ultimate-fish-guide-t4993.html")]
	[ReferenceLink("http://forum.enjoysudoku.com/viewtopic.php?t=2793")]
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Franken_Fish.html")]
	[HodokuTechniquePrefix("0340")]
	[HodokuDifficultyRating(390, HodokuDifficultyLevel.Unfair)]
	[TechniqueGroup(TechniqueGroup.ComplexFish)]
	[StaticDifficultyLevel(DifficultyLevel.Hard)]
	[BoundStepTypes(typeof(ComplexFishStep))]
	[StaticDifficulty(3.2)]
	[ExtraDifficultyFactors(Size, Sashimi, FishShape, Cannibalism)]
	FinnedFrankenXWing,

	/// <summary>
	/// Indicates sashimi franken X-Wing.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/the-ultimate-fish-guide-t4993.html")]
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Franken_Fish.html")]
	[TechniqueGroup(TechniqueGroup.ComplexFish)]
	[StaticDifficultyLevel(DifficultyLevel.Hard)]
	[BoundStepTypes(typeof(ComplexFishStep))]
	[StaticDifficulty(3.2)]
	[ExtraDifficultyFactors(Size, Sashimi, FishShape, Cannibalism)]
	SashimiFrankenXWing,

	/// <summary>
	/// Indicates Siamese finned franken X-Wing.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/the-ultimate-fish-guide-t4993.html")]
	[ReferenceLink("http://forum.enjoysudoku.com/viewtopic.php?t=2793")]
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Franken_Fish.html")]
	[TechniqueGroup(TechniqueGroup.ComplexFish)]
	[StaticDifficultyLevel(DifficultyLevel.Hard)]
	[BoundStepTypes(typeof(ComplexFishStep))]
	[StaticDifficulty(3.2)]
	[ExtraDifficultyFactors(Size, Sashimi, FishShape, Cannibalism)]
	SiameseFinnedFrankenXWing,

	/// <summary>
	/// Indicates Siamese sashimi franken X-Wing.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/the-ultimate-fish-guide-t4993.html")]
	[TechniqueGroup(TechniqueGroup.ComplexFish)]
	[StaticDifficultyLevel(DifficultyLevel.Hard)]
	[BoundStepTypes(typeof(ComplexFishStep))]
	[StaticDifficulty(3.2)]
	[ExtraDifficultyFactors(Size, Sashimi, FishShape, Cannibalism)]
	SiameseSashimiFrankenXWing,

	/// <summary>
	/// Indicates mutant X-Wing.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/the-ultimate-fish-guide-t4993.html")]
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Mutant_Fish.html")]
	[HodokuTechniquePrefix("0350")]
	[HodokuDifficultyRating(450, HodokuDifficultyLevel.Extreme)]
	[TechniqueGroup(TechniqueGroup.ComplexFish)]
	[StaticDifficultyLevel(DifficultyLevel.Hard)]
	[BoundStepTypes(typeof(ComplexFishStep))]
	[StaticDifficulty(3.2)]
	[ExtraDifficultyFactors(Size, Sashimi, FishShape, Cannibalism)]
	MutantXWing,

	/// <summary>
	/// Indicates finned mutant X-Wing.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/the-ultimate-fish-guide-t4993.html")]
	[ReferenceLink("http://forum.enjoysudoku.com/viewtopic.php?t=2793")]
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Mutant_Fish.html")]
	[HodokuTechniquePrefix("0360")]
	[HodokuDifficultyRating(470, HodokuDifficultyLevel.Extreme)]
	[TechniqueGroup(TechniqueGroup.ComplexFish)]
	[StaticDifficultyLevel(DifficultyLevel.Hard)]
	[BoundStepTypes(typeof(ComplexFishStep))]
	[StaticDifficulty(3.2)]
	[ExtraDifficultyFactors(Size, Sashimi, FishShape, Cannibalism)]
	FinnedMutantXWing,

	/// <summary>
	/// Indicates sashimi mutant X-Wing.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/the-ultimate-fish-guide-t4993.html")]
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Mutant_Fish.html")]
	[TechniqueGroup(TechniqueGroup.ComplexFish)]
	[StaticDifficultyLevel(DifficultyLevel.Hard)]
	[BoundStepTypes(typeof(ComplexFishStep))]
	[StaticDifficulty(3.2)]
	[ExtraDifficultyFactors(Size, Sashimi, FishShape, Cannibalism)]
	SashimiMutantXWing,

	/// <summary>
	/// Indicates Siamese finned mutant X-Wing.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/the-ultimate-fish-guide-t4993.html")]
	[ReferenceLink("http://forum.enjoysudoku.com/viewtopic.php?t=2793")]
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Mutant_Fish.html")]
	[TechniqueGroup(TechniqueGroup.ComplexFish)]
	[StaticDifficultyLevel(DifficultyLevel.Hard)]
	[BoundStepTypes(typeof(ComplexFishStep))]
	[StaticDifficulty(3.2)]
	[ExtraDifficultyFactors(Size, Sashimi, FishShape, Cannibalism)]
	SiameseFinnedMutantXWing,

	/// <summary>
	/// Indicates Siamese sashimi mutant X-Wing.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/the-ultimate-fish-guide-t4993.html")]
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Mutant_Fish.html")]
	[TechniqueGroup(TechniqueGroup.ComplexFish)]
	[StaticDifficultyLevel(DifficultyLevel.Hard)]
	[BoundStepTypes(typeof(ComplexFishStep))]
	[StaticDifficulty(3.2)]
	[ExtraDifficultyFactors(Size, Sashimi, FishShape, Cannibalism)]
	SiameseSashimiMutantXWing,

	/// <summary>
	/// Indicates swordfish.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/the-ultimate-fish-guide-t4993.html")]
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Swordfish.html")]
	[HodokuTechniquePrefix("0301")]
	[HodokuDifficultyRating(150, HodokuDifficultyLevel.Hard)]
	[SudokuExplainerTechnique(SudokuExplainerTechnique.Swordfish)]
	[SudokuExplainerDifficultyRating(3.8)]
	[TechniqueGroup(TechniqueGroup.NormalFish)]
	[StaticDifficultyLevel(DifficultyLevel.Hard)]
	[BoundStepTypes(typeof(NormalFishStep))]
	[StaticDifficulty(3.2)]
	[ExtraDifficultyFactors(Size, Sashimi)]
	Swordfish,

	/// <summary>
	/// Indicates finned swordfish.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/the-ultimate-fish-guide-t4993.html")]
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Finned_Fish.html")]
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Finned_Swordfish.html")]
	[ReferenceLink("http://forum.enjoysudoku.com/viewtopic.php?t=2793")]
	[HodokuTechniquePrefix("0311")]
	[HodokuDifficultyRating(200, HodokuDifficultyLevel.Unfair)]
	[SudokuExplainerDifficultyRating(4.0, IsAdvancedDefined = true)]
	[TechniqueGroup(TechniqueGroup.NormalFish)]
	[StaticDifficultyLevel(DifficultyLevel.Hard)]
	[BoundStepTypes(typeof(NormalFishStep))]
	[StaticDifficulty(3.2)]
	[ExtraDifficultyFactors(Size, Sashimi)]
	FinnedSwordfish,

	/// <summary>
	/// Indicates sashimi swordfish.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/the-ultimate-fish-guide-t4993.html")]
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Sashimi_Fish.html")]
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Sashimi_Swordfish.html")]
	[HodokuTechniquePrefix("0321")]
	[HodokuDifficultyRating(240, HodokuDifficultyLevel.Unfair)]
	[SudokuExplainerDifficultyRating(4.1, IsAdvancedDefined = true)]
	[TechniqueGroup(TechniqueGroup.NormalFish)]
	[StaticDifficultyLevel(DifficultyLevel.Hard)]
	[BoundStepTypes(typeof(NormalFishStep))]
	[StaticDifficulty(3.2)]
	[ExtraDifficultyFactors(Size, Sashimi)]
	SashimiSwordfish,

	/// <summary>
	/// Indicates Siamese finned swordfish.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/the-ultimate-fish-guide-t4993.html")]
	[ReferenceLink("http://forum.enjoysudoku.com/viewtopic.php?t=2793")]
	[TechniqueGroup(TechniqueGroup.NormalFish)]
	[StaticDifficultyLevel(DifficultyLevel.Hard)]
	[BoundStepTypes(typeof(NormalFishStep))]
	[StaticDifficulty(3.2)]
	[ExtraDifficultyFactors(Size, Sashimi)]
	SiameseFinnedSwordfish,

	/// <summary>
	/// Indicates Siamese sashimi swordfish.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/the-ultimate-fish-guide-t4993.html")]
	[TechniqueGroup(TechniqueGroup.NormalFish)]
	[StaticDifficultyLevel(DifficultyLevel.Hard)]
	[BoundStepTypes(typeof(NormalFishStep))]
	[StaticDifficulty(3.2)]
	[ExtraDifficultyFactors(Size, Sashimi)]
	SiameseSashimiSwordfish,

	/// <summary>
	/// Indicates swordfish.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/the-ultimate-fish-guide-t4993.html")]
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Franken_Fish.html")]
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Franken_Fish.html")]
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Franken_Swordfish.html")]
	[HodokuTechniquePrefix("0331")]
	[HodokuDifficultyRating(350, HodokuDifficultyLevel.Unfair)]
	[TechniqueGroup(TechniqueGroup.ComplexFish)]
	[StaticDifficultyLevel(DifficultyLevel.Fiendish)]
	[BoundStepTypes(typeof(ComplexFishStep))]
	[StaticDifficulty(3.2)]
	[ExtraDifficultyFactors(Size, Sashimi, FishShape, Cannibalism)]
	FrankenSwordfish,

	/// <summary>
	/// Indicates finned franken swordfish.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/the-ultimate-fish-guide-t4993.html")]
	[ReferenceLink("http://forum.enjoysudoku.com/viewtopic.php?t=2793")]
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Franken_Fish.html")]
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Franken_Swordfish.html")]
	[HodokuTechniquePrefix("0341")]
	[HodokuDifficultyRating(410, HodokuDifficultyLevel.Unfair)]
	[TechniqueGroup(TechniqueGroup.ComplexFish)]
	[StaticDifficultyLevel(DifficultyLevel.Fiendish)]
	[BoundStepTypes(typeof(ComplexFishStep))]
	[StaticDifficulty(3.2)]
	[ExtraDifficultyFactors(Size, Sashimi, FishShape, Cannibalism)]
	FinnedFrankenSwordfish,

	/// <summary>
	/// Indicates sashimi franken swordfish.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/the-ultimate-fish-guide-t4993.html")]
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Franken_Fish.html")]
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Franken_Swordfish.html")]
	[TechniqueGroup(TechniqueGroup.ComplexFish)]
	[StaticDifficultyLevel(DifficultyLevel.Fiendish)]
	[BoundStepTypes(typeof(ComplexFishStep))]
	[StaticDifficulty(3.2)]
	[ExtraDifficultyFactors(Size, Sashimi, FishShape, Cannibalism)]
	SashimiFrankenSwordfish,

	/// <summary>
	/// Indicates Siamese finned franken swordfish.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/the-ultimate-fish-guide-t4993.html")]
	[ReferenceLink("http://forum.enjoysudoku.com/viewtopic.php?t=2793")]
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Franken_Fish.html")]
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Franken_Swordfish.html")]
	[TechniqueGroup(TechniqueGroup.ComplexFish)]
	[StaticDifficultyLevel(DifficultyLevel.Fiendish)]
	[BoundStepTypes(typeof(ComplexFishStep))]
	[StaticDifficulty(3.2)]
	[ExtraDifficultyFactors(Size, Sashimi, FishShape, Cannibalism)]
	SiameseFinnedFrankenSwordfish,

	/// <summary>
	/// Indicates Siamese sashimi franken swordfish.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/the-ultimate-fish-guide-t4993.html")]
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Franken_Fish.html")]
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Franken_Swordfish.html")]
	[TechniqueGroup(TechniqueGroup.ComplexFish)]
	[StaticDifficultyLevel(DifficultyLevel.Fiendish)]
	[BoundStepTypes(typeof(ComplexFishStep))]
	[StaticDifficulty(3.2)]
	[ExtraDifficultyFactors(Size, Sashimi, FishShape, Cannibalism)]
	SiameseSashimiFrankenSwordfish,

	/// <summary>
	/// Indicates mutant swordfish.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/the-ultimate-fish-guide-t4993.html")]
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Mutant_Fish.html")]
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Mutant_Swordfish.html")]
	[HodokuTechniquePrefix("0351")]
	[HodokuDifficultyRating(450, HodokuDifficultyLevel.Extreme)]
	[TechniqueGroup(TechniqueGroup.ComplexFish)]
	[StaticDifficultyLevel(DifficultyLevel.Fiendish)]
	[BoundStepTypes(typeof(ComplexFishStep))]
	[StaticDifficulty(3.2)]
	[ExtraDifficultyFactors(Size, Sashimi, FishShape, Cannibalism)]
	MutantSwordfish,

	/// <summary>
	/// Indicates finned mutant swordfish.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/the-ultimate-fish-guide-t4993.html")]
	[ReferenceLink("http://forum.enjoysudoku.com/viewtopic.php?t=2793")]
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Mutant_Fish.html")]
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Mutant_Swordfish.html")]
	[HodokuTechniquePrefix("0361")]
	[HodokuDifficultyRating(470, HodokuDifficultyLevel.Extreme)]
	[TechniqueGroup(TechniqueGroup.ComplexFish)]
	[StaticDifficultyLevel(DifficultyLevel.Fiendish)]
	[BoundStepTypes(typeof(ComplexFishStep))]
	[StaticDifficulty(3.2)]
	[ExtraDifficultyFactors(Size, Sashimi, FishShape, Cannibalism)]
	FinnedMutantSwordfish,

	/// <summary>
	/// Indicates sashimi mutant swordfish.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/the-ultimate-fish-guide-t4993.html")]
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Mutant_Fish.html")]
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Mutant_Swordfish.html")]
	[TechniqueGroup(TechniqueGroup.ComplexFish)]
	[StaticDifficultyLevel(DifficultyLevel.Fiendish)]
	[BoundStepTypes(typeof(ComplexFishStep))]
	[StaticDifficulty(3.2)]
	[ExtraDifficultyFactors(Size, Sashimi, FishShape, Cannibalism)]
	SashimiMutantSwordfish,

	/// <summary>
	/// Indicates Siamese finned mutant swordfish.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/the-ultimate-fish-guide-t4993.html")]
	[ReferenceLink("http://forum.enjoysudoku.com/viewtopic.php?t=2793")]
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Mutant_Fish.html")]
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Mutant_Swordfish.html")]
	[TechniqueGroup(TechniqueGroup.ComplexFish)]
	[StaticDifficultyLevel(DifficultyLevel.Fiendish)]
	[BoundStepTypes(typeof(ComplexFishStep))]
	[StaticDifficulty(3.2)]
	[ExtraDifficultyFactors(Size, Sashimi, FishShape, Cannibalism)]
	SiameseFinnedMutantSwordfish,

	/// <summary>
	/// Indicates Siamese sashimi mutant swordfish.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/the-ultimate-fish-guide-t4993.html")]
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Mutant_Fish.html")]
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Mutant_Swordfish.html")]
	[TechniqueGroup(TechniqueGroup.ComplexFish)]
	[StaticDifficultyLevel(DifficultyLevel.Fiendish)]
	[BoundStepTypes(typeof(ComplexFishStep))]
	[StaticDifficulty(3.2)]
	[ExtraDifficultyFactors(Size, Sashimi, FishShape, Cannibalism)]
	SiameseSashimiMutantSwordfish,

	/// <summary>
	/// Indicates jellyfish.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/the-ultimate-fish-guide-t4993.html")]
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Jellyfish.html")]
	[HodokuTechniquePrefix("0302")]
	[HodokuDifficultyRating(160, HodokuDifficultyLevel.Hard)]
	[SudokuExplainerTechnique(SudokuExplainerTechnique.Jellyfish)]
	[SudokuExplainerDifficultyRating(5.2)]
	[TechniqueGroup(TechniqueGroup.NormalFish)]
	[StaticDifficultyLevel(DifficultyLevel.Hard)]
	[BoundStepTypes(typeof(NormalFishStep))]
	[StaticDifficulty(3.2)]
	[ExtraDifficultyFactors(Size, Sashimi)]
	Jellyfish,

	/// <summary>
	/// Indicates finned jellyfish.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/the-ultimate-fish-guide-t4993.html")]
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Finned_Fish.html")]
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Finned_Jellyfish.html")]
	[ReferenceLink("http://forum.enjoysudoku.com/viewtopic.php?t=2793")]
	[HodokuTechniquePrefix("0312")]
	[HodokuDifficultyRating(250, HodokuDifficultyLevel.Unfair)]
	[SudokuExplainerDifficultyRating(5.4, IsAdvancedDefined = true)]
	[TechniqueGroup(TechniqueGroup.NormalFish)]
	[StaticDifficultyLevel(DifficultyLevel.Hard)]
	[BoundStepTypes(typeof(NormalFishStep))]
	[StaticDifficulty(3.2)]
	[ExtraDifficultyFactors(Size, Sashimi)]
	FinnedJellyfish,

	/// <summary>
	/// Indicates sashimi jellyfish.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/the-ultimate-fish-guide-t4993.html")]
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Sashimi_Fish.html")]
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Sashimi_Jellyfish.html")]
	[HodokuTechniquePrefix("0322")]
	[HodokuDifficultyRating(260, HodokuDifficultyLevel.Unfair)]
	[SudokuExplainerDifficultyRating(5.6, IsAdvancedDefined = true)]
	[TechniqueGroup(TechniqueGroup.NormalFish)]
	[StaticDifficultyLevel(DifficultyLevel.Hard)]
	[BoundStepTypes(typeof(NormalFishStep))]
	[StaticDifficulty(3.2)]
	[ExtraDifficultyFactors(Size, Sashimi)]
	SashimiJellyfish,

	/// <summary>
	/// Indicates Siamese finned jellyfish.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/the-ultimate-fish-guide-t4993.html")]
	[ReferenceLink("http://forum.enjoysudoku.com/viewtopic.php?t=2793")]
	[TechniqueGroup(TechniqueGroup.NormalFish)]
	[StaticDifficultyLevel(DifficultyLevel.Hard)]
	[BoundStepTypes(typeof(NormalFishStep))]
	[StaticDifficulty(3.2)]
	[ExtraDifficultyFactors(Size, Sashimi)]
	SiameseFinnedJellyfish,

	/// <summary>
	/// Indicates Siamese sashimi jellyfish.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/the-ultimate-fish-guide-t4993.html")]
	[TechniqueGroup(TechniqueGroup.NormalFish)]
	[StaticDifficultyLevel(DifficultyLevel.Hard)]
	[BoundStepTypes(typeof(NormalFishStep))]
	[StaticDifficulty(3.2)]
	[ExtraDifficultyFactors(Size, Sashimi)]
	SiameseSashimiJellyfish,

	/// <summary>
	/// Indicates franken jellyfish.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/the-ultimate-fish-guide-t4993.html")]
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Franken_Fish.html")]
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Franken_Jellyfish.html")]
	[HodokuTechniquePrefix("0332")]
	[HodokuDifficultyRating(370, HodokuDifficultyLevel.Unfair)]
	[TechniqueGroup(TechniqueGroup.ComplexFish)]
	[StaticDifficultyLevel(DifficultyLevel.Fiendish)]
	[BoundStepTypes(typeof(ComplexFishStep))]
	[StaticDifficulty(3.2)]
	[ExtraDifficultyFactors(Size, Sashimi, FishShape, Cannibalism)]
	FrankenJellyfish,

	/// <summary>
	/// Indicates finned franken jellyfish.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/the-ultimate-fish-guide-t4993.html")]
	[ReferenceLink("http://forum.enjoysudoku.com/viewtopic.php?t=2793")]
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Franken_Fish.html")]
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Franken_Jellyfish.html")]
	[HodokuTechniquePrefix("0342")]
	[HodokuDifficultyRating(430, HodokuDifficultyLevel.Unfair)]
	[TechniqueGroup(TechniqueGroup.ComplexFish)]
	[StaticDifficultyLevel(DifficultyLevel.Fiendish)]
	[BoundStepTypes(typeof(ComplexFishStep))]
	[StaticDifficulty(3.2)]
	[ExtraDifficultyFactors(Size, Sashimi, FishShape, Cannibalism)]
	FinnedFrankenJellyfish,

	/// <summary>
	/// Indicates sashimi franken jellyfish.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/the-ultimate-fish-guide-t4993.html")]
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Franken_Fish.html")]
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Franken_Jellyfish.html")]
	[TechniqueGroup(TechniqueGroup.ComplexFish)]
	[StaticDifficultyLevel(DifficultyLevel.Fiendish)]
	[BoundStepTypes(typeof(ComplexFishStep))]
	[StaticDifficulty(3.2)]
	[ExtraDifficultyFactors(Size, Sashimi, FishShape, Cannibalism)]
	SashimiFrankenJellyfish,

	/// <summary>
	/// Indicates Siamese finned franken jellyfish.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/the-ultimate-fish-guide-t4993.html")]
	[ReferenceLink("http://forum.enjoysudoku.com/viewtopic.php?t=2793")]
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Franken_Fish.html")]
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Franken_Jellyfish.html")]
	[TechniqueGroup(TechniqueGroup.ComplexFish)]
	[StaticDifficultyLevel(DifficultyLevel.Fiendish)]
	[BoundStepTypes(typeof(ComplexFishStep))]
	[StaticDifficulty(3.2)]
	[ExtraDifficultyFactors(Size, Sashimi, FishShape, Cannibalism)]
	SiameseFinnedFrankenJellyfish,

	/// <summary>
	/// Indicates Siamese sashimi franken jellyfish.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/the-ultimate-fish-guide-t4993.html")]
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Franken_Fish.html")]
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Franken_Jellyfish.html")]
	[TechniqueGroup(TechniqueGroup.ComplexFish)]
	[StaticDifficultyLevel(DifficultyLevel.Fiendish)]
	[BoundStepTypes(typeof(ComplexFishStep))]
	[StaticDifficulty(3.2)]
	[ExtraDifficultyFactors(Size, Sashimi, FishShape, Cannibalism)]
	SiameseSashimiFrankenJellyfish,

	/// <summary>
	/// Indicates mutant jellyfish.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/the-ultimate-fish-guide-t4993.html")]
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Mutant_Fish.html")]
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Mutant_Jellyfish.html")]
	[HodokuTechniquePrefix("0352")]
	[HodokuDifficultyRating(450, HodokuDifficultyLevel.Extreme)]
	[TechniqueGroup(TechniqueGroup.ComplexFish)]
	[StaticDifficultyLevel(DifficultyLevel.Fiendish)]
	[BoundStepTypes(typeof(ComplexFishStep))]
	[StaticDifficulty(3.2)]
	[ExtraDifficultyFactors(Size, Sashimi, FishShape, Cannibalism)]
	MutantJellyfish,

	/// <summary>
	/// Indicates finned mutant jellyfish.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/the-ultimate-fish-guide-t4993.html")]
	[ReferenceLink("http://forum.enjoysudoku.com/viewtopic.php?t=2793")]
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Mutant_Fish.html")]
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Mutant_Jellyfish.html")]
	[HodokuTechniquePrefix("0362")]
	[HodokuDifficultyRating(470, HodokuDifficultyLevel.Extreme)]
	[TechniqueGroup(TechniqueGroup.ComplexFish)]
	[StaticDifficultyLevel(DifficultyLevel.Fiendish)]
	[BoundStepTypes(typeof(ComplexFishStep))]
	[StaticDifficulty(3.2)]
	[ExtraDifficultyFactors(Size, Sashimi, FishShape, Cannibalism)]
	FinnedMutantJellyfish,

	/// <summary>
	/// Indicates sashimi mutant jellyfish.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/the-ultimate-fish-guide-t4993.html")]
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Mutant_Fish.html")]
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Mutant_Jellyfish.html")]
	[TechniqueGroup(TechniqueGroup.ComplexFish)]
	[StaticDifficultyLevel(DifficultyLevel.Fiendish)]
	[BoundStepTypes(typeof(ComplexFishStep))]
	[StaticDifficulty(3.2)]
	[ExtraDifficultyFactors(Size, Sashimi, FishShape, Cannibalism)]
	SashimiMutantJellyfish,

	/// <summary>
	/// Indicates Siamese finned mutant jellyfish.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/the-ultimate-fish-guide-t4993.html")]
	[ReferenceLink("http://forum.enjoysudoku.com/viewtopic.php?t=2793")]
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Mutant_Fish.html")]
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Mutant_Jellyfish.html")]
	[TechniqueGroup(TechniqueGroup.ComplexFish)]
	[StaticDifficultyLevel(DifficultyLevel.Fiendish)]
	[BoundStepTypes(typeof(ComplexFishStep))]
	[StaticDifficulty(3.2)]
	[ExtraDifficultyFactors(Size, Sashimi, FishShape, Cannibalism)]
	SiameseFinnedMutantJellyfish,

	/// <summary>
	/// Indicates Siamese sashimi mutant jellyfish.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/the-ultimate-fish-guide-t4993.html")]
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Mutant_Fish.html")]
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Mutant_Jellyfish.html")]
	[TechniqueGroup(TechniqueGroup.ComplexFish)]
	[StaticDifficultyLevel(DifficultyLevel.Fiendish)]
	[BoundStepTypes(typeof(ComplexFishStep))]
	[StaticDifficulty(3.2)]
	[ExtraDifficultyFactors(Size, Sashimi, FishShape, Cannibalism)]
	SiameseSashimiMutantJellyfish,

	/// <summary>
	/// Indicates squirmbag.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/the-ultimate-fish-guide-t4993.html")]
	[HodokuTechniquePrefix("0303")]
	[HodokuDifficultyRating(470, HodokuDifficultyLevel.Unfair)]
	[TechniqueGroup(TechniqueGroup.NormalFish)]
	[TechniqueFeature(TechniqueFeature.OnlyExistInTheory)]
	[StaticDifficultyLevel(DifficultyLevel.Fiendish)]
	Squirmbag,

	/// <summary>
	/// Indicates finned squirmbag.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/the-ultimate-fish-guide-t4993.html")]
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Finned_Fish.html")]
	[ReferenceLink("http://forum.enjoysudoku.com/viewtopic.php?t=2793")]
	[HodokuTechniquePrefix("0313")]
	[HodokuDifficultyRating(470, HodokuDifficultyLevel.Unfair)]
	[TechniqueGroup(TechniqueGroup.NormalFish)]
	[TechniqueFeature(TechniqueFeature.OnlyExistInTheory)]
	[StaticDifficultyLevel(DifficultyLevel.Fiendish)]
	FinnedSquirmbag,

	/// <summary>
	/// Indicates sashimi squirmbag.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/the-ultimate-fish-guide-t4993.html")]
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Sashimi_Fish.html")]
	[HodokuTechniquePrefix("0323")]
	[HodokuDifficultyRating(470, HodokuDifficultyLevel.Unfair)]
	[TechniqueGroup(TechniqueGroup.NormalFish)]
	[TechniqueFeature(TechniqueFeature.OnlyExistInTheory)]
	[StaticDifficultyLevel(DifficultyLevel.Fiendish)]
	SashimiSquirmbag,

	/// <summary>
	/// Indicates Siamese finned squirmbag.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/the-ultimate-fish-guide-t4993.html")]
	[ReferenceLink("http://forum.enjoysudoku.com/viewtopic.php?t=2793")]
	[TechniqueGroup(TechniqueGroup.NormalFish)]
	[TechniqueFeature(TechniqueFeature.OnlyExistInTheory)]
	[StaticDifficultyLevel(DifficultyLevel.Fiendish)]
	SiameseFinnedSquirmbag,

	/// <summary>
	/// Indicates Siamese sashimi squirmbag.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/the-ultimate-fish-guide-t4993.html")]
	[TechniqueGroup(TechniqueGroup.NormalFish)]
	[TechniqueFeature(TechniqueFeature.OnlyExistInTheory)]
	[StaticDifficultyLevel(DifficultyLevel.Fiendish)]
	SiameseSashimiSquirmbag,

	/// <summary>
	/// Indicates franken squirmbag.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/the-ultimate-fish-guide-t4993.html")]
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Franken_Fish.html")]
	[HodokuTechniquePrefix("0333")]
	[HodokuDifficultyRating(470, HodokuDifficultyLevel.Extreme)]
	[TechniqueGroup(TechniqueGroup.ComplexFish)]
	[StaticDifficultyLevel(DifficultyLevel.Nightmare)]
	[TechniqueFeature(TechniqueFeature.OnlyExistInTheory)]
	[BoundStepTypes(typeof(ComplexFishStep))]
	[StaticDifficulty(3.2)]
	[ExtraDifficultyFactors(Size, Sashimi, FishShape, Cannibalism)]
	FrankenSquirmbag,

	/// <summary>
	/// Indicates finned franken squirmbag.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/the-ultimate-fish-guide-t4993.html")]
	[ReferenceLink("http://forum.enjoysudoku.com/viewtopic.php?t=2793")]
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Franken_Fish.html")]
	[HodokuTechniquePrefix("0343")]
	[HodokuDifficultyRating(470, HodokuDifficultyLevel.Extreme)]
	[TechniqueGroup(TechniqueGroup.ComplexFish)]
	[StaticDifficultyLevel(DifficultyLevel.Nightmare)]
	[BoundStepTypes(typeof(ComplexFishStep))]
	[StaticDifficulty(3.2)]
	[ExtraDifficultyFactors(Size, Sashimi, FishShape, Cannibalism)]
	FinnedFrankenSquirmbag,

	/// <summary>
	/// Indicates sashimi franken squirmbag.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/the-ultimate-fish-guide-t4993.html")]
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Franken_Fish.html")]
	[TechniqueGroup(TechniqueGroup.ComplexFish)]
	[StaticDifficultyLevel(DifficultyLevel.Nightmare)]
	[BoundStepTypes(typeof(ComplexFishStep))]
	[StaticDifficulty(3.2)]
	[ExtraDifficultyFactors(Size, Sashimi, FishShape, Cannibalism)]
	SashimiFrankenSquirmbag,

	/// <summary>
	/// Indicates Siamese finned franken squirmbag.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/the-ultimate-fish-guide-t4993.html")]
	[ReferenceLink("http://forum.enjoysudoku.com/viewtopic.php?t=2793")]
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Franken_Fish.html")]
	[TechniqueGroup(TechniqueGroup.ComplexFish)]
	[StaticDifficultyLevel(DifficultyLevel.Nightmare)]
	[BoundStepTypes(typeof(ComplexFishStep))]
	[StaticDifficulty(3.2)]
	[ExtraDifficultyFactors(Size, Sashimi, FishShape, Cannibalism)]
	SiameseFinnedFrankenSquirmbag,

	/// <summary>
	/// Indicates Siamese sashimi franken squirmbag.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/the-ultimate-fish-guide-t4993.html")]
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Franken_Fish.html")]
	[TechniqueGroup(TechniqueGroup.ComplexFish)]
	[StaticDifficultyLevel(DifficultyLevel.Nightmare)]
	[BoundStepTypes(typeof(ComplexFishStep))]
	[StaticDifficulty(3.2)]
	[ExtraDifficultyFactors(Size, Sashimi, FishShape, Cannibalism)]
	SiameseSashimiFrankenSquirmbag,

	/// <summary>
	/// Indicates mutant squirmbag.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/the-ultimate-fish-guide-t4993.html")]
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Mutant_Fish.html")]
	[HodokuTechniquePrefix("0353")]
	[HodokuDifficultyRating(470, HodokuDifficultyLevel.Extreme)]
	[TechniqueGroup(TechniqueGroup.ComplexFish)]
	[StaticDifficultyLevel(DifficultyLevel.Nightmare)]
	[TechniqueFeature(TechniqueFeature.OnlyExistInTheory)]
	[BoundStepTypes(typeof(ComplexFishStep))]
	[StaticDifficulty(3.2)]
	[ExtraDifficultyFactors(Size, Sashimi, FishShape, Cannibalism)]
	MutantSquirmbag,

	/// <summary>
	/// Indicates finned mutant squirmbag.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/the-ultimate-fish-guide-t4993.html")]
	[ReferenceLink("http://forum.enjoysudoku.com/viewtopic.php?t=2793")]
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Mutant_Fish.html")]
	[HodokuTechniquePrefix("0363")]
	[HodokuDifficultyRating(470, HodokuDifficultyLevel.Extreme)]
	[TechniqueGroup(TechniqueGroup.ComplexFish)]
	[StaticDifficultyLevel(DifficultyLevel.Nightmare)]
	[BoundStepTypes(typeof(ComplexFishStep))]
	[StaticDifficulty(3.2)]
	[ExtraDifficultyFactors(Size, Sashimi, FishShape, Cannibalism)]
	FinnedMutantSquirmbag,

	/// <summary>
	/// Indicates sashimi mutant squirmbag.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/the-ultimate-fish-guide-t4993.html")]
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Mutant_Fish.html")]
	[TechniqueGroup(TechniqueGroup.ComplexFish)]
	[StaticDifficultyLevel(DifficultyLevel.Nightmare)]
	[BoundStepTypes(typeof(ComplexFishStep))]
	[StaticDifficulty(3.2)]
	[ExtraDifficultyFactors(Size, Sashimi, FishShape, Cannibalism)]
	SashimiMutantSquirmbag,

	/// <summary>
	/// Indicates Siamese finned mutant squirmbag.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/the-ultimate-fish-guide-t4993.html")]
	[ReferenceLink("http://forum.enjoysudoku.com/viewtopic.php?t=2793")]
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Mutant_Fish.html")]
	[TechniqueGroup(TechniqueGroup.ComplexFish)]
	[StaticDifficultyLevel(DifficultyLevel.Nightmare)]
	[BoundStepTypes(typeof(ComplexFishStep))]
	[StaticDifficulty(3.2)]
	[ExtraDifficultyFactors(Size, Sashimi, FishShape, Cannibalism)]
	SiameseFinnedMutantSquirmbag,

	/// <summary>
	/// Indicates Siamese sashimi mutant squirmbag.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/the-ultimate-fish-guide-t4993.html")]
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Mutant_Fish.html")]
	[TechniqueGroup(TechniqueGroup.ComplexFish)]
	[StaticDifficultyLevel(DifficultyLevel.Nightmare)]
	[BoundStepTypes(typeof(ComplexFishStep))]
	[StaticDifficulty(3.2)]
	[ExtraDifficultyFactors(Size, Sashimi, FishShape, Cannibalism)]
	SiameseSashimiMutantSquirmbag,

	/// <summary>
	/// Indicates whale.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/the-ultimate-fish-guide-t4993.html")]
	[HodokuTechniquePrefix("0304")]
	[HodokuDifficultyRating(470, HodokuDifficultyLevel.Unfair)]
	[TechniqueGroup(TechniqueGroup.NormalFish)]
	[TechniqueFeature(TechniqueFeature.OnlyExistInTheory)]
	[StaticDifficultyLevel(DifficultyLevel.Fiendish)]
	Whale,

	/// <summary>
	/// Indicates finned whale.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/the-ultimate-fish-guide-t4993.html")]
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Finned_Fish.html")]
	[ReferenceLink("http://forum.enjoysudoku.com/viewtopic.php?t=2793")]
	[HodokuTechniquePrefix("0314")]
	[HodokuDifficultyRating(470, HodokuDifficultyLevel.Unfair)]
	[TechniqueGroup(TechniqueGroup.NormalFish)]
	[TechniqueFeature(TechniqueFeature.OnlyExistInTheory)]
	[StaticDifficultyLevel(DifficultyLevel.Fiendish)]
	FinnedWhale,

	/// <summary>
	/// Indicates sashimi whale.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/the-ultimate-fish-guide-t4993.html")]
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Sashimi_Fish.html")]
	[HodokuTechniquePrefix("0324")]
	[HodokuDifficultyRating(470, HodokuDifficultyLevel.Unfair)]
	[TechniqueGroup(TechniqueGroup.NormalFish)]
	[TechniqueFeature(TechniqueFeature.OnlyExistInTheory)]
	[StaticDifficultyLevel(DifficultyLevel.Fiendish)]
	SashimiWhale,

	/// <summary>
	/// Indicates Siamese finned whale.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/the-ultimate-fish-guide-t4993.html")]
	[ReferenceLink("http://forum.enjoysudoku.com/viewtopic.php?t=2793")]
	[TechniqueGroup(TechniqueGroup.NormalFish)]
	[TechniqueFeature(TechniqueFeature.OnlyExistInTheory)]
	[StaticDifficultyLevel(DifficultyLevel.Fiendish)]
	SiameseFinnedWhale,

	/// <summary>
	/// Indicates Siamese sashimi whale.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/the-ultimate-fish-guide-t4993.html")]
	[TechniqueGroup(TechniqueGroup.NormalFish)]
	[TechniqueFeature(TechniqueFeature.OnlyExistInTheory)]
	[StaticDifficultyLevel(DifficultyLevel.Fiendish)]
	SiameseSashimiWhale,

	/// <summary>
	/// Indicates franken whale.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/the-ultimate-fish-guide-t4993.html")]
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Franken_Fish.html")]
	[HodokuTechniquePrefix("0334")]
	[HodokuDifficultyRating(470, HodokuDifficultyLevel.Extreme)]
	[TechniqueGroup(TechniqueGroup.ComplexFish)]
	[StaticDifficultyLevel(DifficultyLevel.Nightmare)]
	[TechniqueFeature(TechniqueFeature.OnlyExistInTheory)]
	[BoundStepTypes(typeof(ComplexFishStep))]
	[StaticDifficulty(3.2)]
	[ExtraDifficultyFactors(Size, Sashimi, FishShape, Cannibalism)]
	FrankenWhale,

	/// <summary>
	/// Indicates finned franken whale.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/the-ultimate-fish-guide-t4993.html")]
	[ReferenceLink("http://forum.enjoysudoku.com/viewtopic.php?t=2793")]
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Franken_Fish.html")]
	[HodokuTechniquePrefix("0344")]
	[HodokuDifficultyRating(470, HodokuDifficultyLevel.Extreme)]
	[TechniqueGroup(TechniqueGroup.ComplexFish)]
	[StaticDifficultyLevel(DifficultyLevel.Nightmare)]
	[BoundStepTypes(typeof(ComplexFishStep))]
	[StaticDifficulty(3.2)]
	[ExtraDifficultyFactors(Size, Sashimi, FishShape, Cannibalism)]
	FinnedFrankenWhale,

	/// <summary>
	/// Indicates sashimi franken whale.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/the-ultimate-fish-guide-t4993.html")]
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Franken_Fish.html")]
	[TechniqueGroup(TechniqueGroup.ComplexFish)]
	[StaticDifficultyLevel(DifficultyLevel.Nightmare)]
	[BoundStepTypes(typeof(ComplexFishStep))]
	[StaticDifficulty(3.2)]
	[ExtraDifficultyFactors(Size, Sashimi, FishShape, Cannibalism)]
	SashimiFrankenWhale,

	/// <summary>
	/// Indicates Siamese finned franken whale.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/the-ultimate-fish-guide-t4993.html")]
	[ReferenceLink("http://forum.enjoysudoku.com/viewtopic.php?t=2793")]
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Franken_Fish.html")]
	[TechniqueGroup(TechniqueGroup.ComplexFish)]
	[StaticDifficultyLevel(DifficultyLevel.Nightmare)]
	[BoundStepTypes(typeof(ComplexFishStep))]
	[StaticDifficulty(3.2)]
	[ExtraDifficultyFactors(Size, Sashimi, FishShape, Cannibalism)]
	SiameseFinnedFrankenWhale,

	/// <summary>
	/// Indicates Siamese sashimi franken whale.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/the-ultimate-fish-guide-t4993.html")]
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Franken_Fish.html")]
	[TechniqueGroup(TechniqueGroup.ComplexFish)]
	[StaticDifficultyLevel(DifficultyLevel.Nightmare)]
	[BoundStepTypes(typeof(ComplexFishStep))]
	[StaticDifficulty(3.2)]
	[ExtraDifficultyFactors(Size, Sashimi, FishShape, Cannibalism)]
	SiameseSashimiFrankenWhale,

	/// <summary>
	/// Indicates mutant whale.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/the-ultimate-fish-guide-t4993.html")]
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Mutant_Fish.html")]
	[HodokuTechniquePrefix("0354")]
	[HodokuDifficultyRating(470, HodokuDifficultyLevel.Extreme)]
	[TechniqueGroup(TechniqueGroup.ComplexFish)]
	[StaticDifficultyLevel(DifficultyLevel.Nightmare)]
	[TechniqueFeature(TechniqueFeature.OnlyExistInTheory)]
	[BoundStepTypes(typeof(ComplexFishStep))]
	[StaticDifficulty(3.2)]
	[ExtraDifficultyFactors(Size, Sashimi, FishShape, Cannibalism)]
	MutantWhale,

	/// <summary>
	/// Indicates finned mutant whale.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/the-ultimate-fish-guide-t4993.html")]
	[ReferenceLink("http://forum.enjoysudoku.com/viewtopic.php?t=2793")]
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Mutant_Fish.html")]
	[HodokuTechniquePrefix("0364")]
	[HodokuDifficultyRating(470, HodokuDifficultyLevel.Extreme)]
	[TechniqueGroup(TechniqueGroup.ComplexFish)]
	[StaticDifficultyLevel(DifficultyLevel.Nightmare)]
	[BoundStepTypes(typeof(ComplexFishStep))]
	[StaticDifficulty(3.2)]
	[ExtraDifficultyFactors(Size, Sashimi, FishShape, Cannibalism)]
	FinnedMutantWhale,

	/// <summary>
	/// Indicates sashimi mutant whale.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/the-ultimate-fish-guide-t4993.html")]
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Mutant_Fish.html")]
	[TechniqueGroup(TechniqueGroup.ComplexFish)]
	[StaticDifficultyLevel(DifficultyLevel.Nightmare)]
	[BoundStepTypes(typeof(ComplexFishStep))]
	[StaticDifficulty(3.2)]
	[ExtraDifficultyFactors(Size, Sashimi, FishShape, Cannibalism)]
	SashimiMutantWhale,

	/// <summary>
	/// Indicates Siamese finned mutant whale.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/the-ultimate-fish-guide-t4993.html")]
	[ReferenceLink("http://forum.enjoysudoku.com/viewtopic.php?t=2793")]
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Mutant_Fish.html")]
	[TechniqueGroup(TechniqueGroup.ComplexFish)]
	[StaticDifficultyLevel(DifficultyLevel.Nightmare)]
	[BoundStepTypes(typeof(ComplexFishStep))]
	[StaticDifficulty(3.2)]
	[ExtraDifficultyFactors(Size, Sashimi, FishShape, Cannibalism)]
	SiameseFinnedMutantWhale,

	/// <summary>
	/// Indicates Siamese sashimi mutant whale.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/the-ultimate-fish-guide-t4993.html")]
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Mutant_Fish.html")]
	[TechniqueGroup(TechniqueGroup.ComplexFish)]
	[StaticDifficultyLevel(DifficultyLevel.Nightmare)]
	[BoundStepTypes(typeof(ComplexFishStep))]
	[StaticDifficulty(3.2)]
	[ExtraDifficultyFactors(Size, Sashimi, FishShape, Cannibalism)]
	SiameseSashimiMutantWhale,

	/// <summary>
	/// Indicates leviathan.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/the-ultimate-fish-guide-t4993.html")]
	[HodokuTechniquePrefix("0305")]
	[HodokuDifficultyRating(470, HodokuDifficultyLevel.Unfair)]
	[TechniqueGroup(TechniqueGroup.NormalFish)]
	[TechniqueFeature(TechniqueFeature.OnlyExistInTheory)]
	[StaticDifficultyLevel(DifficultyLevel.Fiendish)]
	Leviathan,

	/// <summary>
	/// Indicates finned leviathan.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/the-ultimate-fish-guide-t4993.html")]
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Finned_Fish.html")]
	[ReferenceLink("http://forum.enjoysudoku.com/viewtopic.php?t=2793")]
	[HodokuTechniquePrefix("0315")]
	[HodokuDifficultyRating(470, HodokuDifficultyLevel.Unfair)]
	[TechniqueGroup(TechniqueGroup.NormalFish)]
	[TechniqueFeature(TechniqueFeature.OnlyExistInTheory)]
	[StaticDifficultyLevel(DifficultyLevel.Fiendish)]
	FinnedLeviathan,

	/// <summary>
	/// Indicates sashimi leviathan.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/the-ultimate-fish-guide-t4993.html")]
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Sashimi_Fish.html")]
	[HodokuTechniquePrefix("0325")]
	[HodokuDifficultyRating(470, HodokuDifficultyLevel.Unfair)]
	[TechniqueGroup(TechniqueGroup.NormalFish)]
	[TechniqueFeature(TechniqueFeature.OnlyExistInTheory)]
	[StaticDifficultyLevel(DifficultyLevel.Fiendish)]
	SashimiLeviathan,

	/// <summary>
	/// Indicates Siamese finned leviathan.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/the-ultimate-fish-guide-t4993.html")]
	[ReferenceLink("http://forum.enjoysudoku.com/viewtopic.php?t=2793")]
	[TechniqueGroup(TechniqueGroup.NormalFish)]
	[TechniqueFeature(TechniqueFeature.OnlyExistInTheory)]
	[StaticDifficultyLevel(DifficultyLevel.Fiendish)]
	SiameseFinnedLeviathan,

	/// <summary>
	/// Indicates Siamese sashimi leviathan.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/the-ultimate-fish-guide-t4993.html")]
	[TechniqueGroup(TechniqueGroup.NormalFish)]
	[TechniqueFeature(TechniqueFeature.OnlyExistInTheory)]
	[StaticDifficultyLevel(DifficultyLevel.Fiendish)]
	SiameseSashimiLeviathan,

	/// <summary>
	/// Indicates franken leviathan.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/the-ultimate-fish-guide-t4993.html")]
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Franken_Fish.html")]
	[HodokuTechniquePrefix("0335")]
	[HodokuDifficultyRating(470, HodokuDifficultyLevel.Extreme)]
	[TechniqueGroup(TechniqueGroup.ComplexFish)]
	[StaticDifficultyLevel(DifficultyLevel.Nightmare)]
	[TechniqueFeature(TechniqueFeature.OnlyExistInTheory)]
	[BoundStepTypes(typeof(ComplexFishStep))]
	[StaticDifficulty(3.2)]
	[ExtraDifficultyFactors(Size, Sashimi, FishShape, Cannibalism)]
	FrankenLeviathan,

	/// <summary>
	/// Indicates finned franken leviathan.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/the-ultimate-fish-guide-t4993.html")]
	[ReferenceLink("http://forum.enjoysudoku.com/viewtopic.php?t=2793")]
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Franken_Fish.html")]
	[HodokuTechniquePrefix("0345")]
	[HodokuDifficultyRating(470, HodokuDifficultyLevel.Extreme)]
	[TechniqueGroup(TechniqueGroup.ComplexFish)]
	[StaticDifficultyLevel(DifficultyLevel.Nightmare)]
	[BoundStepTypes(typeof(ComplexFishStep))]
	[StaticDifficulty(3.2)]
	[ExtraDifficultyFactors(Size, Sashimi, FishShape, Cannibalism)]
	FinnedFrankenLeviathan,

	/// <summary>
	/// Indicates sashimi franken leviathan.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/the-ultimate-fish-guide-t4993.html")]
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Franken_Fish.html")]
	[TechniqueGroup(TechniqueGroup.ComplexFish)]
	[StaticDifficultyLevel(DifficultyLevel.Nightmare)]
	[BoundStepTypes(typeof(ComplexFishStep))]
	[StaticDifficulty(3.2)]
	[ExtraDifficultyFactors(Size, Sashimi, FishShape, Cannibalism)]
	SashimiFrankenLeviathan,

	/// <summary>
	/// Indicates Siamese finned franken leviathan.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/the-ultimate-fish-guide-t4993.html")]
	[ReferenceLink("http://forum.enjoysudoku.com/viewtopic.php?t=2793")]
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Franken_Fish.html")]
	[TechniqueGroup(TechniqueGroup.ComplexFish)]
	[StaticDifficultyLevel(DifficultyLevel.Nightmare)]
	[BoundStepTypes(typeof(ComplexFishStep))]
	[StaticDifficulty(3.2)]
	[ExtraDifficultyFactors(Size, Sashimi, FishShape, Cannibalism)]
	SiameseFinnedFrankenLeviathan,

	/// <summary>
	/// Indicates Siamese sashimi franken leviathan.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/the-ultimate-fish-guide-t4993.html")]
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Franken_Fish.html")]
	[TechniqueGroup(TechniqueGroup.ComplexFish)]
	[StaticDifficultyLevel(DifficultyLevel.Nightmare)]
	[BoundStepTypes(typeof(ComplexFishStep))]
	[StaticDifficulty(3.2)]
	[ExtraDifficultyFactors(Size, Sashimi, FishShape, Cannibalism)]
	SiameseSashimiFrankenLeviathan,

	/// <summary>
	/// Indicates mutant leviathan.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/the-ultimate-fish-guide-t4993.html")]
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Mutant_Fish.html")]
	[HodokuTechniquePrefix("0355")]
	[HodokuDifficultyRating(470, HodokuDifficultyLevel.Extreme)]
	[TechniqueGroup(TechniqueGroup.ComplexFish)]
	[StaticDifficultyLevel(DifficultyLevel.Nightmare)]
	[TechniqueFeature(TechniqueFeature.OnlyExistInTheory)]
	[BoundStepTypes(typeof(ComplexFishStep))]
	[StaticDifficulty(3.2)]
	[ExtraDifficultyFactors(Size, Sashimi, FishShape, Cannibalism)]
	MutantLeviathan,

	/// <summary>
	/// Indicates finned mutant leviathan.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/the-ultimate-fish-guide-t4993.html")]
	[ReferenceLink("http://forum.enjoysudoku.com/viewtopic.php?t=2793")]
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Mutant_Fish.html")]
	[HodokuTechniquePrefix("0365")]
	[HodokuDifficultyRating(470, HodokuDifficultyLevel.Extreme)]
	[TechniqueGroup(TechniqueGroup.ComplexFish)]
	[StaticDifficultyLevel(DifficultyLevel.Nightmare)]
	[BoundStepTypes(typeof(ComplexFishStep))]
	[StaticDifficulty(3.2)]
	[ExtraDifficultyFactors(Size, Sashimi, FishShape, Cannibalism)]
	FinnedMutantLeviathan,

	/// <summary>
	/// Indicates sashimi mutant leviathan.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/the-ultimate-fish-guide-t4993.html")]
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Mutant_Fish.html")]
	[TechniqueGroup(TechniqueGroup.ComplexFish)]
	[StaticDifficultyLevel(DifficultyLevel.Nightmare)]
	[BoundStepTypes(typeof(ComplexFishStep))]
	[StaticDifficulty(3.2)]
	[ExtraDifficultyFactors(Size, Sashimi, FishShape, Cannibalism)]
	SashimiMutantLeviathan,

	/// <summary>
	/// Indicates Siamese finned mutant leviathan.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/the-ultimate-fish-guide-t4993.html")]
	[ReferenceLink("http://forum.enjoysudoku.com/viewtopic.php?t=2793")]
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Mutant_Fish.html")]
	[TechniqueGroup(TechniqueGroup.ComplexFish)]
	[StaticDifficultyLevel(DifficultyLevel.Nightmare)]
	[BoundStepTypes(typeof(ComplexFishStep))]
	[StaticDifficulty(3.2)]
	[ExtraDifficultyFactors(Size, Sashimi, FishShape, Cannibalism)]
	SiameseFinnedMutantLeviathan,

	/// <summary>
	/// Indicates Siamese sashimi mutant leviathan.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/the-ultimate-fish-guide-t4993.html")]
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Mutant_Fish.html")]
	[TechniqueGroup(TechniqueGroup.ComplexFish)]
	[StaticDifficultyLevel(DifficultyLevel.Nightmare)]
	[BoundStepTypes(typeof(ComplexFishStep))]
	[StaticDifficulty(3.2)]
	[ExtraDifficultyFactors(Size, Sashimi, FishShape, Cannibalism)]
	SiameseSashimiMutantLeviathan,
	#endregion

	//
	// Regular Wings
	//
	#region Regular Wings
	/// <summary>
	/// Indicates XY-Wing.
	/// </summary>
	[ReferenceLink("http://sudopedia.enjoysudoku.com/XY-Wing.html")]
	[HodokuTechniquePrefix("0800")]
	[HodokuDifficultyRating(160, HodokuDifficultyLevel.Hard)]
	[SudokuExplainerTechnique(SudokuExplainerTechnique.XyWing)]
	[SudokuExplainerDifficultyRating(4.2)]
	[TechniqueGroup(TechniqueGroup.RegularWing)]
	[StaticDifficultyLevel(DifficultyLevel.Hard)]
	[BoundStepTypes(typeof(RegularWingStep))]
	[StaticDifficulty(4.2)]
	[ExtraDifficultyFactors(Size, Incompleteness)]
	XyWing,

	/// <summary>
	/// Indicates XYZ-Wing.
	/// </summary>
	[ReferenceLink("http://sudopedia.enjoysudoku.com/XYZ-Wing.html")]
	[HodokuTechniquePrefix("0801")]
	[HodokuDifficultyRating(180, HodokuDifficultyLevel.Hard)]
	[SudokuExplainerTechnique(SudokuExplainerTechnique.XyzWing)]
	[SudokuExplainerDifficultyRating(4.4)]
	[TechniqueGroup(TechniqueGroup.RegularWing)]
	[StaticDifficultyLevel(DifficultyLevel.Hard)]
	[BoundStepTypes(typeof(RegularWingStep))]
	[StaticDifficulty(4.2)]
	[ExtraDifficultyFactors(Size, Incompleteness)]
	XyzWing,

	/// <summary>
	/// Indicates WXYZ-Wing.
	/// </summary>
	[ReferenceLink("http://sudopedia.enjoysudoku.com/WXYZ-Wing.html")]
	[HodokuTechniquePrefix("0802")]
	[SudokuExplainerTechnique(SudokuExplainerTechnique.XyzWing, IsAdvancedDefined = true)]
	[SudokuExplainerDifficultyRating(4.6, IsAdvancedDefined = true)]
	[TechniqueGroup(TechniqueGroup.RegularWing)]
	[StaticDifficultyLevel(DifficultyLevel.Hard)]
	[BoundStepTypes(typeof(RegularWingStep))]
	[StaticDifficulty(4.2)]
	[ExtraDifficultyFactors(Size, Incompleteness)]
	WxyzWing,

	/// <summary>
	/// Indicates VWXYZ-Wing.
	/// </summary>
	[SudokuExplainerDifficultyRating(double.NaN, IsAdvancedDefined = true)]
	[TechniqueGroup(TechniqueGroup.RegularWing)]
	[TechniqueFeature(TechniqueFeature.HardToBeGenerated)]
	[StaticDifficultyLevel(DifficultyLevel.Fiendish)]
	[BoundStepTypes(typeof(RegularWingStep))]
	[StaticDifficulty(4.2)]
	[ExtraDifficultyFactors(Size, Incompleteness)]
	VwxyzWing,

	/// <summary>
	/// Indicates UVWXYZ-Wing.
	/// </summary>
	[SudokuExplainerDifficultyRating(double.NaN, IsAdvancedDefined = true)]
	[TechniqueGroup(TechniqueGroup.RegularWing)]
	[TechniqueFeature(TechniqueFeature.HardToBeGenerated)]
	[StaticDifficultyLevel(DifficultyLevel.Fiendish)]
	[BoundStepTypes(typeof(RegularWingStep))]
	[StaticDifficulty(4.2)]
	[ExtraDifficultyFactors(Size, Incompleteness)]
	UvwxyzWing,

	/// <summary>
	/// Indicates TUVWXYZ-Wing.
	/// </summary>
	[SudokuExplainerDifficultyRating(double.NaN, IsAdvancedDefined = true)]
	[TechniqueGroup(TechniqueGroup.RegularWing)]
	[TechniqueFeature(TechniqueFeature.HardToBeGenerated)]
	[StaticDifficultyLevel(DifficultyLevel.Fiendish)]
	[BoundStepTypes(typeof(RegularWingStep))]
	[StaticDifficulty(4.2)]
	[ExtraDifficultyFactors(Size, Incompleteness)]
	TuvwxyzWing,

	/// <summary>
	/// Indicates STUVWXYZ-Wing.
	/// </summary>
	[SudokuExplainerDifficultyRating(double.NaN, IsAdvancedDefined = true)]
	[TechniqueGroup(TechniqueGroup.RegularWing)]
	[TechniqueFeature(TechniqueFeature.HardToBeGenerated)]
	[StaticDifficultyLevel(DifficultyLevel.Fiendish)]
	[BoundStepTypes(typeof(RegularWingStep))]
	[StaticDifficulty(4.2)]
	[ExtraDifficultyFactors(Size, Incompleteness)]
	StuvwxyzWing,

	/// <summary>
	/// Indicates RSTUVWXYZ-Wing.
	/// </summary>
	[SudokuExplainerDifficultyRating(double.NaN, IsAdvancedDefined = true)]
	[TechniqueGroup(TechniqueGroup.RegularWing)]
	[TechniqueFeature(TechniqueFeature.HardToBeGenerated)]
	[StaticDifficultyLevel(DifficultyLevel.Fiendish)]
	[BoundStepTypes(typeof(RegularWingStep))]
	[StaticDifficulty(4.2)]
	[ExtraDifficultyFactors(Size, Incompleteness)]
	RstuvwxyzWing,

	/// <summary>
	/// Indicates incomplete WXYZ-Wing.
	/// </summary>
	[ReferenceLink("http://sudopedia.enjoysudoku.com/WXYZ-Wing.html")]
	[TechniqueGroup(TechniqueGroup.RegularWing)]
	[StaticDifficultyLevel(DifficultyLevel.Hard)]
	[BoundStepTypes(typeof(RegularWingStep))]
	[StaticDifficulty(4.2)]
	[ExtraDifficultyFactors(Size, Incompleteness)]
	IncompleteWxyzWing,

	/// <summary>
	/// Indicates incomplete VWXYZ-Wing.
	/// </summary>
	[TechniqueGroup(TechniqueGroup.RegularWing)]
	[StaticDifficultyLevel(DifficultyLevel.Fiendish)]
	[BoundStepTypes(typeof(RegularWingStep))]
	[StaticDifficulty(4.2)]
	[ExtraDifficultyFactors(Size, Incompleteness)]
	IncompleteVwxyzWing,

	/// <summary>
	/// Indicates incomplete UVWXYZ-Wing.
	/// </summary>
	[TechniqueGroup(TechniqueGroup.RegularWing)]
	[TechniqueFeature(TechniqueFeature.HardToBeGenerated)]
	[StaticDifficultyLevel(DifficultyLevel.Fiendish)]
	[BoundStepTypes(typeof(RegularWingStep))]
	[StaticDifficulty(4.2)]
	[ExtraDifficultyFactors(Size, Incompleteness)]
	IncompleteUvwxyzWing,

	/// <summary>
	/// Indicates incomplete TUVWXYZ-Wing.
	/// </summary>
	[TechniqueGroup(TechniqueGroup.RegularWing)]
	[TechniqueFeature(TechniqueFeature.HardToBeGenerated)]
	[StaticDifficultyLevel(DifficultyLevel.Fiendish)]
	[BoundStepTypes(typeof(RegularWingStep))]
	[StaticDifficulty(4.2)]
	[ExtraDifficultyFactors(Size, Incompleteness)]
	IncompleteTuvwxyzWing,

	/// <summary>
	/// Indicates incomplete STUVWXYZ-Wing.
	/// </summary>
	[TechniqueGroup(TechniqueGroup.RegularWing)]
	[TechniqueFeature(TechniqueFeature.HardToBeGenerated)]
	[StaticDifficultyLevel(DifficultyLevel.Fiendish)]
	[BoundStepTypes(typeof(RegularWingStep))]
	[StaticDifficulty(4.2)]
	[ExtraDifficultyFactors(Size, Incompleteness)]
	IncompleteStuvwxyzWing,

	/// <summary>
	/// Indicates incomplete RSTUVWXYZ-Wing.
	/// </summary>
	[TechniqueGroup(TechniqueGroup.RegularWing)]
	[TechniqueFeature(TechniqueFeature.HardToBeGenerated)]
	[StaticDifficultyLevel(DifficultyLevel.Fiendish)]
	[BoundStepTypes(typeof(RegularWingStep))]
	[StaticDifficulty(4.2)]
	[ExtraDifficultyFactors(Size, Incompleteness)]
	IncompleteRstuvwxyzWing,
	#endregion

	//
	// Irregular Wings
	//
	#region Irregular Wings
	/// <summary>
	/// Indicates W-Wing.
	/// </summary>
	[ReferenceLink("http://sudopedia.enjoysudoku.com/W-Wing.html")]
	[HodokuTechniquePrefix("0803")]
	[HodokuDifficultyRating(150, HodokuDifficultyLevel.Hard)]
	[SudokuExplainerTechnique(SudokuExplainerTechnique.WWing, IsAdvancedDefined = true)]
	[SudokuExplainerDifficultyRating(4.4, IsAdvancedDefined = true)]
	[TechniqueGroup(TechniqueGroup.IrregularWing)]
	[StaticDifficultyLevel(DifficultyLevel.Hard)]
	[BoundStepTypes(typeof(WWingStep))]
	[StaticDifficulty(4.4)]
	WWing,

	/// <summary>
	/// Indicates Multi-Branch W-Wing.
	/// </summary>
	[ReferenceLink("http://sudopedia.enjoysudoku.com/W-Wing.html")]
	[TechniqueGroup(TechniqueGroup.IrregularWing)]
	[StaticDifficultyLevel(DifficultyLevel.Hard)]
	[BoundStepTypes(typeof(MultiBranchWWingStep))]
	[StaticDifficulty(4.4)]
	[ExtraDifficultyFactors(Size)]
	MultiBranchWWing,

	/// <summary>
	/// Indicates grouped W-Wing.
	/// </summary>
	[ReferenceLink("http://sudopedia.enjoysudoku.com/W-Wing.html")]
	[TechniqueGroup(TechniqueGroup.IrregularWing)]
	[StaticDifficultyLevel(DifficultyLevel.Hard)]
	[BoundStepTypes(typeof(WWingStep))]
	[StaticDifficulty(4.4)]
	GroupedWWing,

	/// <summary>
	/// Indicates M-Wing.
	/// </summary>
	[TechniqueGroup(TechniqueGroup.IrregularWing)]
	[StaticDifficultyLevel(DifficultyLevel.Hard)]
	[BoundStepTypes(typeof(MWingStep))]
	[StaticDifficulty(4.5)]
	MWing,

	/// <summary>
	/// Indicates grouped M-Wing.
	/// </summary>
	[TechniqueGroup(TechniqueGroup.IrregularWing)]
	[StaticDifficultyLevel(DifficultyLevel.Hard)]
	[BoundStepTypes(typeof(MWingStep))]
	[StaticDifficulty(4.5)]
	GroupedMWing,

	/// <summary>
	/// Indicates S-Wing.
	/// </summary>
	[TechniqueGroup(TechniqueGroup.IrregularWing)]
	[StaticDifficultyLevel(DifficultyLevel.Hard)]
	[BoundStepTypes(typeof(SWingStep))]
	[StaticDifficulty(4.7)]
	SWing,

	/// <summary>
	/// Indicates grouped S-Wing.
	/// </summary>
	[TechniqueGroup(TechniqueGroup.IrregularWing)]
	[StaticDifficultyLevel(DifficultyLevel.Hard)]
	[BoundStepTypes(typeof(SWingStep))]
	[StaticDifficulty(4.7)]
	GroupedSWing,

	/// <summary>
	/// Indicates local wing.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/local-wing-t34685.html")]
	[TechniqueGroup(TechniqueGroup.IrregularWing)]
	[StaticDifficultyLevel(DifficultyLevel.Hard)]
	[BoundStepTypes(typeof(LWingStep))]
	[StaticDifficulty(4.8)]
	LWing,

	/// <summary>
	/// Indicates grouped local wing.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/local-wing-t34685.html")]
	[TechniqueGroup(TechniqueGroup.IrregularWing)]
	[StaticDifficultyLevel(DifficultyLevel.Hard)]
	[BoundStepTypes(typeof(LWingStep))]
	[StaticDifficulty(4.8)]
	GroupedLWing,

	/// <summary>
	/// Indicates hybrid wing.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/hybrid-wings-work-in-progress-t34212.html")]
	[TechniqueGroup(TechniqueGroup.IrregularWing)]
	[StaticDifficultyLevel(DifficultyLevel.Hard)]
	[BoundStepTypes(typeof(HWingStep))]
	[StaticDifficulty(4.7)]
	HWing,

	/// <summary>
	/// Indicates grouped hybrid wing.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/hybrid-wings-work-in-progress-t34212.html")]
	[TechniqueGroup(TechniqueGroup.IrregularWing)]
	[StaticDifficultyLevel(DifficultyLevel.Hard)]
	[BoundStepTypes(typeof(HWingStep))]
	[StaticDifficulty(4.7)]
	GroupedHWing,
	#endregion

	//
	// Almost Locked Candidates
	//
	#region Almost Locked Candidates
	/// <summary>
	/// Indicates almost locked pair.
	/// </summary>
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Almost_Locked_Candidates.html")]
	[ReferenceLink("http://forum.enjoysudoku.com/viewtopic.php?t=4477")]
	[SudokuExplainerDifficultyRating(4.5, IsAdvancedDefined = true)]
	[TechniqueGroup(TechniqueGroup.AlmostLockedCandidates)]
	[Abbreviation("ALP")]
	[StaticDifficultyLevel(DifficultyLevel.Hard)]
	[BoundStepTypes(typeof(AlmostLockedCandidatesStep))]
	[StaticDifficulty(4.5)]
	[ExtraDifficultyFactors(Size, ValueCell)]
	AlmostLockedPair,

	/// <summary>
	/// Indicates almost locked triple.
	/// </summary>
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Almost_Locked_Candidates.html")]
	[ReferenceLink("http://forum.enjoysudoku.com/viewtopic.php?t=4477")]
	[SudokuExplainerDifficultyRating(5.2, IsAdvancedDefined = true)]
	[TechniqueGroup(TechniqueGroup.AlmostLockedCandidates)]
	[Abbreviation("ALT")]
	[StaticDifficultyLevel(DifficultyLevel.Hard)]
	[BoundStepTypes(typeof(AlmostLockedCandidatesStep))]
	[StaticDifficulty(4.5)]
	[ExtraDifficultyFactors(Size, ValueCell)]
	AlmostLockedTriple,

	/// <summary>
	/// Indicates almost locked quadruple.
	/// The technique may not be useful because it'll be replaced with Sue de Coq.
	/// </summary>
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Almost_Locked_Candidates.html")]
	[ReferenceLink("http://forum.enjoysudoku.com/viewtopic.php?t=4477")]
	[TechniqueGroup(TechniqueGroup.AlmostLockedCandidates)]
	[Abbreviation("ALQ")]
	[TechniqueFeature(TechniqueFeature.OnlyExistInTheory)]
	[StaticDifficultyLevel(DifficultyLevel.Hard)]
	[BoundStepTypes(typeof(AlmostLockedCandidatesStep))]
	[StaticDifficulty(4.5)]
	[ExtraDifficultyFactors(Size, ValueCell)]
	AlmostLockedQuadruple,

	/// <summary>
	/// Indicates almost locked triple value type.
	/// The technique may not be often used because it'll be replaced with some kinds of Sue de Coq.
	/// </summary>
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Almost_Locked_Candidates.html")]
	[ReferenceLink("http://forum.enjoysudoku.com/viewtopic.php?t=4477")]
	[TechniqueGroup(TechniqueGroup.AlmostLockedCandidates)]
	[StaticDifficultyLevel(DifficultyLevel.Hard)]
	[BoundStepTypes(typeof(AlmostLockedCandidatesStep))]
	[StaticDifficulty(4.5)]
	[ExtraDifficultyFactors(Size, ValueCell)]
	AlmostLockedTripleValueType,

	/// <summary>
	/// Indicates almost locked quadruple value type.
	/// The technique may not be often used because it'll be replaced with some kinds of Sue de Coq.
	/// </summary>
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Almost_Locked_Candidates.html")]
	[ReferenceLink("http://forum.enjoysudoku.com/viewtopic.php?t=4477")]
	[TechniqueGroup(TechniqueGroup.AlmostLockedCandidates)]
	[StaticDifficultyLevel(DifficultyLevel.Hard)]
	[BoundStepTypes(typeof(AlmostLockedCandidatesStep))]
	[StaticDifficulty(4.5)]
	[ExtraDifficultyFactors(Size, ValueCell)]
	AlmostLockedQuadrupleValueType,
	#endregion

	//
	// Extended Subset Principle
	//
	#region Extended Subset Principle
	/// <summary>
	/// Indicates extended subset principle.
	/// </summary>
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Subset_Counting.html")]
	[ReferenceLink("http://forum.enjoysudoku.com/viewtopic.php?t=3479")]
	[HodokuTechniquePrefix("1102")]
	[Abbreviation("ESP")]
	[HodokuAliasedNames("Subset Counting")]
	[TechniqueGroup(TechniqueGroup.ExtendedSubsetPrinciple)]
	[StaticDifficultyLevel(DifficultyLevel.Fiendish)]
	[BoundStepTypes(typeof(ExtendedSubsetPrincipleStep))]
	[StaticDifficulty(5.5)]
	[ExtraDifficultyFactors(Size)]
	ExtendedSubsetPrinciple,
	#endregion

	//
	// Unique Rectangle
	//
	#region Unique Rectangle
	/// <summary>
	/// Indicates unique rectangle type 1.
	/// </summary>
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Unique_Rectangle.html")]
	[ReferenceLink("http://forum.enjoysudoku.com/viewtopic.php?t=2000")]
	[ReferenceLink("http://forum.enjoysudoku.com/uniqueness-type-6-ur-meets-x-wing-t3709-30.html#p26448")]
	[ReferenceLink("http://forum.enjoysudoku.com/unique-rectangles-gallery-t33752.html")]
	[HodokuTechniquePrefix("0600")]
	[HodokuDifficultyRating(100, HodokuDifficultyLevel.Hard)]
	[HodokuAliasedNames("Uniqueness Test 1")]
	[SudokuExplainerTechnique(SudokuExplainerTechnique.UniqueRectangle)]
	[SudokuExplainerDifficultyRating(4.5)]
	[TechniqueGroup(TechniqueGroup.UniqueRectangle)]
	[StaticDifficultyLevel(DifficultyLevel.Hard)]
	[BoundStepTypes(typeof(UniqueRectangleType1Step))]
	[StaticDifficulty(4.5)]
	UniqueRectangleType1,

	/// <summary>
	/// Indicates unique rectangle type 2.
	/// </summary>
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Unique_Rectangle.html")]
	[ReferenceLink("http://forum.enjoysudoku.com/viewtopic.php?t=2000")]
	[ReferenceLink("http://forum.enjoysudoku.com/uniqueness-type-6-ur-meets-x-wing-t3709-30.html#p26448")]
	[ReferenceLink("http://forum.enjoysudoku.com/unique-rectangles-gallery-t33752.html")]
	[HodokuTechniquePrefix("0601")]
	[HodokuDifficultyRating(100, HodokuDifficultyLevel.Hard)]
	[HodokuAliasedNames("Uniqueness Test 2")]
	[SudokuExplainerTechnique(SudokuExplainerTechnique.UniqueRectangle)]
	[SudokuExplainerDifficultyRating(4.5)]
	[SudokuExplainerDifficultyRating(4.6, IsAdvancedDefined = true)]
	[TechniqueGroup(TechniqueGroup.UniqueRectangle)]
	[StaticDifficultyLevel(DifficultyLevel.Hard)]
	[BoundStepTypes(typeof(UniqueRectangleType2Step))]
	[StaticDifficulty(4.6)]
	UniqueRectangleType2,

	/// <summary>
	/// Indicates unique rectangle type 3.
	/// </summary>
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Unique_Rectangle.html")]
	[ReferenceLink("http://forum.enjoysudoku.com/viewtopic.php?t=2000")]
	[ReferenceLink("http://forum.enjoysudoku.com/uniqueness-type-6-ur-meets-x-wing-t3709-30.html#p26448")]
	[ReferenceLink("http://forum.enjoysudoku.com/unique-rectangles-gallery-t33752.html")]
	[HodokuTechniquePrefix("0602")]
	[HodokuDifficultyRating(100, HodokuDifficultyLevel.Hard)]
	[HodokuAliasedNames("Uniqueness Test 3")]
	[SudokuExplainerTechnique(SudokuExplainerTechnique.UniqueRectangle)]
	[SudokuExplainerDifficultyRating(4.5, 4.8)]
	[SudokuExplainerDifficultyRating(4.6, 4.9, IsAdvancedDefined = true)]
	[TechniqueGroup(TechniqueGroup.UniqueRectangle)]
	[StaticDifficultyLevel(DifficultyLevel.Hard)]
	[BoundStepTypes(typeof(UniqueRectangleType3Step))]
	[StaticDifficulty(4.5)]
	[ExtraDifficultyFactors(Size, Hidden)]
	UniqueRectangleType3,

	/// <summary>
	/// Indicates unique rectangle type 4.
	/// </summary>
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Unique_Rectangle.html")]
	[ReferenceLink("http://forum.enjoysudoku.com/viewtopic.php?t=2000")]
	[ReferenceLink("http://forum.enjoysudoku.com/uniqueness-type-6-ur-meets-x-wing-t3709-30.html#p26448")]
	[ReferenceLink("http://forum.enjoysudoku.com/unique-rectangles-gallery-t33752.html")]
	[HodokuTechniquePrefix("0603")]
	[HodokuDifficultyRating(100, HodokuDifficultyLevel.Hard)]
	[HodokuAliasedNames("Uniqueness Test 4")]
	[SudokuExplainerTechnique(SudokuExplainerTechnique.UniqueRectangle)]
	[SudokuExplainerDifficultyRating(4.5)]
	[SudokuExplainerDifficultyRating(4.6, IsAdvancedDefined = true)]
	[TechniqueGroup(TechniqueGroup.UniqueRectangle)]
	[StaticDifficultyLevel(DifficultyLevel.Hard)]
	[BoundStepTypes(typeof(UniqueRectangleWithConjugatePairStep))]
	[StaticDifficulty(4.4)]
	[ExtraDifficultyFactors(ConjugatePair)]
	UniqueRectangleType4,

	/// <summary>
	/// Indicates unique rectangle type 5.
	/// </summary>
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Unique_Rectangle.html")]
	[ReferenceLink("http://forum.enjoysudoku.com/uniqueness-type-6-ur-meets-x-wing-t3709-30.html#p26448")]
	[ReferenceLink("http://forum.enjoysudoku.com/unique-rectangles-gallery-t33752.html")]
	[HodokuTechniquePrefix("0604")]
	[HodokuDifficultyRating(100, HodokuDifficultyLevel.Hard)]
	[HodokuAliasedNames("Uniqueness Test 5")]
	[SudokuExplainerTechnique(SudokuExplainerTechnique.UniqueRectangle, IsAdvancedDefined = true)]
	[SudokuExplainerDifficultyRating(4.6, IsAdvancedDefined = true)]
	[TechniqueGroup(TechniqueGroup.UniqueRectangle)]
	[StaticDifficultyLevel(DifficultyLevel.Hard)]
	[BoundStepTypes(typeof(UniqueRectangleType2Step))]
	[StaticDifficulty(4.5)]
	UniqueRectangleType5,

	/// <summary>
	/// Indicates unique rectangle type 6.
	/// </summary>
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Unique_Rectangle.html")]
	[ReferenceLink("http://forum.enjoysudoku.com/uniqueness-type-6-ur-meets-x-wing-t3709-30.html#p26448")]
	[ReferenceLink("http://forum.enjoysudoku.com/unique-rectangles-gallery-t33752.html")]
	[HodokuTechniquePrefix("0605")]
	[HodokuDifficultyRating(100, HodokuDifficultyLevel.Hard)]
	[HodokuAliasedNames("Uniqueness Test 6")]
	[SudokuExplainerTechnique(SudokuExplainerTechnique.UniqueRectangle, IsAdvancedDefined = true)]
	[SudokuExplainerDifficultyRating(4.6, IsAdvancedDefined = true)]
	[TechniqueGroup(TechniqueGroup.UniqueRectangle)]
	[StaticDifficultyLevel(DifficultyLevel.Hard)]
	[BoundStepTypes(typeof(UniqueRectangleWithConjugatePairStep))]
	[StaticDifficulty(4.4)]
	[ExtraDifficultyFactors(ConjugatePair)]
	UniqueRectangleType6,

	/// <summary>
	/// Indicates hidden unique rectangle.
	/// </summary>
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Unique_Rectangle.html")]
	[ReferenceLink("http://forum.enjoysudoku.com/uniqueness-type-6-ur-meets-x-wing-t3709-30.html#p26448")]
	[ReferenceLink("http://forum.enjoysudoku.com/unique-rectangles-gallery-t33752.html")]
	[HodokuTechniquePrefix("0606")]
	[HodokuDifficultyRating(100, HodokuDifficultyLevel.Hard)]
	[HodokuAliasedNames("Hidden Rectangle")]
	[SudokuExplainerTechnique(SudokuExplainerTechnique.UniqueRectangle, IsAdvancedDefined = true)]
	[SudokuExplainerDifficultyRating(4.8, IsAdvancedDefined = true)]
	[TechniqueGroup(TechniqueGroup.UniqueRectangle)]
	[Abbreviation("HUR")]
	[StaticDifficultyLevel(DifficultyLevel.Hard)]
	[BoundStepTypes(typeof(HiddenUniqueRectangleStep), SecondaryTypes = [typeof(UniqueRectangleWithConjugatePairStep)])]
	[StaticDifficulty(4.4)]
	[ExtraDifficultyFactors(ConjugatePair, Avoidable)]
	HiddenUniqueRectangle,

	/// <summary>
	/// Indicates unique rectangle + 2D.
	/// </summary>
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Unique_Rectangle.html")]
	[ReferenceLink("http://forum.enjoysudoku.com/uniqueness-type-6-ur-meets-x-wing-t3709-30.html#p26448")]
	[ReferenceLink("http://forum.enjoysudoku.com/unique-rectangles-gallery-t33752.html")]
	[TechniqueGroup(TechniqueGroup.UniqueRectangle)]
	[TechniqueFeature(TechniqueFeature.HardToBeGenerated)]
	[StaticDifficultyLevel(DifficultyLevel.Hard)]
	[BoundStepTypes(typeof(UniqueRectangle2DOr3XStep))]
	[StaticDifficulty(4.6)]
	UniqueRectangle2D,

	/// <summary>
	/// Indicates unique rectangle + 2B / 1SL.
	/// </summary>
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Unique_Rectangle.html")]
	[ReferenceLink("http://forum.enjoysudoku.com/uniqueness-type-6-ur-meets-x-wing-t3709-30.html#p26448")]
	[ReferenceLink("http://forum.enjoysudoku.com/unique-rectangles-gallery-t33752.html")]
	[TechniqueGroup(TechniqueGroup.UniqueRectangle)]
	[StaticDifficultyLevel(DifficultyLevel.Hard)]
	[BoundStepTypes(typeof(UniqueRectangleWithConjugatePairStep))]
	[StaticDifficulty(4.4)]
	[ExtraDifficultyFactors(ConjugatePair, Avoidable)]
	UniqueRectangle2B1,

	/// <summary>
	/// Indicates unique rectangle + 2D / 1SL.
	/// </summary>
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Unique_Rectangle.html")]
	[ReferenceLink("http://forum.enjoysudoku.com/uniqueness-type-6-ur-meets-x-wing-t3709-30.html#p26448")]
	[ReferenceLink("http://forum.enjoysudoku.com/unique-rectangles-gallery-t33752.html")]
	[TechniqueGroup(TechniqueGroup.UniqueRectangle)]
	[StaticDifficultyLevel(DifficultyLevel.Hard)]
	[BoundStepTypes(typeof(UniqueRectangleWithConjugatePairStep))]
	[StaticDifficulty(4.4)]
	[ExtraDifficultyFactors(ConjugatePair, Avoidable)]
	UniqueRectangle2D1,

	/// <summary>
	/// Indicates unique rectangle + 3X.
	/// </summary>
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Unique_Rectangle.html")]
	[ReferenceLink("http://forum.enjoysudoku.com/uniqueness-type-6-ur-meets-x-wing-t3709-30.html#p26448")]
	[ReferenceLink("http://forum.enjoysudoku.com/unique-rectangles-gallery-t33752.html")]
	[TechniqueGroup(TechniqueGroup.UniqueRectangle)]
	[TechniqueFeature(TechniqueFeature.HardToBeGenerated)]
	[StaticDifficultyLevel(DifficultyLevel.Hard)]
	[BoundStepTypes(typeof(UniqueRectangle2DOr3XStep))]
	[StaticDifficulty(4.6)]
	UniqueRectangle3X,

	/// <summary>
	/// Indicates unique rectangle + 3x / 1SL.
	/// </summary>
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Unique_Rectangle.html")]
	[ReferenceLink("http://forum.enjoysudoku.com/uniqueness-type-6-ur-meets-x-wing-t3709-30.html#p26448")]
	[ReferenceLink("http://forum.enjoysudoku.com/unique-rectangles-gallery-t33752.html")]
	[TechniqueGroup(TechniqueGroup.UniqueRectangle)]
	[StaticDifficultyLevel(DifficultyLevel.Hard)]
	UniqueRectangle3X1L,

	/// <summary>
	/// Indicates unique rectangle + 3X / 1SL.
	/// </summary>
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Unique_Rectangle.html")]
	[ReferenceLink("http://forum.enjoysudoku.com/uniqueness-type-6-ur-meets-x-wing-t3709-30.html#p26448")]
	[ReferenceLink("http://forum.enjoysudoku.com/unique-rectangles-gallery-t33752.html")]
	[TechniqueGroup(TechniqueGroup.UniqueRectangle)]
	[StaticDifficultyLevel(DifficultyLevel.Hard)]
	UniqueRectangle3X1U,

	/// <summary>
	/// Indicates unique rectangle + 3X / 2SL.
	/// </summary>
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Unique_Rectangle.html")]
	[ReferenceLink("http://forum.enjoysudoku.com/uniqueness-type-6-ur-meets-x-wing-t3709-30.html#p26448")]
	[ReferenceLink("http://forum.enjoysudoku.com/unique-rectangles-gallery-t33752.html")]
	[TechniqueGroup(TechniqueGroup.UniqueRectangle)]
	[StaticDifficultyLevel(DifficultyLevel.Hard)]
	[BoundStepTypes(typeof(UniqueRectangleWithConjugatePairStep))]
	[StaticDifficulty(4.4)]
	[ExtraDifficultyFactors(ConjugatePair, Avoidable)]
	UniqueRectangle3X2,

	/// <summary>
	/// Indicates unique rectangle + 3N / 2SL.
	/// </summary>
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Unique_Rectangle.html")]
	[ReferenceLink("http://forum.enjoysudoku.com/uniqueness-type-6-ur-meets-x-wing-t3709-30.html#p26448")]
	[ReferenceLink("http://forum.enjoysudoku.com/unique-rectangles-gallery-t33752.html")]
	[TechniqueGroup(TechniqueGroup.UniqueRectangle)]
	[StaticDifficultyLevel(DifficultyLevel.Hard)]
	[BoundStepTypes(typeof(UniqueRectangleWithConjugatePairStep))]
	[StaticDifficulty(4.4)]
	[ExtraDifficultyFactors(ConjugatePair, Avoidable)]
	UniqueRectangle3N2,

	/// <summary>
	/// Indicates unique rectangle + 3U / 2SL.
	/// </summary>
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Unique_Rectangle.html")]
	[ReferenceLink("http://forum.enjoysudoku.com/uniqueness-type-6-ur-meets-x-wing-t3709-30.html#p26448")]
	[ReferenceLink("http://forum.enjoysudoku.com/unique-rectangles-gallery-t33752.html")]
	[TechniqueGroup(TechniqueGroup.UniqueRectangle)]
	[StaticDifficultyLevel(DifficultyLevel.Hard)]
	[BoundStepTypes(typeof(UniqueRectangleWithConjugatePairStep))]
	[StaticDifficulty(4.4)]
	[ExtraDifficultyFactors(ConjugatePair, Avoidable)]
	UniqueRectangle3U2,

	/// <summary>
	/// Indicates unique rectangle + 3E / 2SL.
	/// </summary>
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Unique_Rectangle.html")]
	[ReferenceLink("http://forum.enjoysudoku.com/uniqueness-type-6-ur-meets-x-wing-t3709-30.html#p26448")]
	[ReferenceLink("http://forum.enjoysudoku.com/unique-rectangles-gallery-t33752.html")]
	[TechniqueGroup(TechniqueGroup.UniqueRectangle)]
	[StaticDifficultyLevel(DifficultyLevel.Hard)]
	[BoundStepTypes(typeof(UniqueRectangleWithConjugatePairStep))]
	[StaticDifficulty(4.4)]
	[ExtraDifficultyFactors(ConjugatePair, Avoidable)]
	UniqueRectangle3E2,

	/// <summary>
	/// Indicates unique rectangle + 4x / 1SL.
	/// </summary>
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Unique_Rectangle.html")]
	[ReferenceLink("http://forum.enjoysudoku.com/uniqueness-type-6-ur-meets-x-wing-t3709-30.html#p26448")]
	[ReferenceLink("http://forum.enjoysudoku.com/unique-rectangles-gallery-t33752.html")]
	[TechniqueGroup(TechniqueGroup.UniqueRectangle)]
	[StaticDifficultyLevel(DifficultyLevel.Hard)]
	UniqueRectangle4X1L,

	/// <summary>
	/// Indicates unique rectangle + 4X / 1SL.
	/// </summary>
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Unique_Rectangle.html")]
	[ReferenceLink("http://forum.enjoysudoku.com/uniqueness-type-6-ur-meets-x-wing-t3709-30.html#p26448")]
	[ReferenceLink("http://forum.enjoysudoku.com/unique-rectangles-gallery-t33752.html")]
	[TechniqueGroup(TechniqueGroup.UniqueRectangle)]
	[StaticDifficultyLevel(DifficultyLevel.Hard)]
	UniqueRectangle4X1U,

	/// <summary>
	/// Indicates unique rectangle + 4x / 2SL.
	/// </summary>
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Unique_Rectangle.html")]
	[ReferenceLink("http://forum.enjoysudoku.com/uniqueness-type-6-ur-meets-x-wing-t3709-30.html#p26448")]
	[ReferenceLink("http://forum.enjoysudoku.com/unique-rectangles-gallery-t33752.html")]
	[TechniqueGroup(TechniqueGroup.UniqueRectangle)]
	[StaticDifficultyLevel(DifficultyLevel.Hard)]
	UniqueRectangle4X2L,

	/// <summary>
	/// Indicates unique rectangle + 4X / 2SL.
	/// </summary>
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Unique_Rectangle.html")]
	[ReferenceLink("http://forum.enjoysudoku.com/uniqueness-type-6-ur-meets-x-wing-t3709-30.html#p26448")]
	[ReferenceLink("http://forum.enjoysudoku.com/unique-rectangles-gallery-t33752.html")]
	[TechniqueGroup(TechniqueGroup.UniqueRectangle)]
	[StaticDifficultyLevel(DifficultyLevel.Hard)]
	UniqueRectangle4X2U,

	/// <summary>
	/// Indicates unique rectangle + 4X / 3SL.
	/// </summary>
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Unique_Rectangle.html")]
	[ReferenceLink("http://forum.enjoysudoku.com/uniqueness-type-6-ur-meets-x-wing-t3709-30.html#p26448")]
	[ReferenceLink("http://forum.enjoysudoku.com/unique-rectangles-gallery-t33752.html")]
	[TechniqueGroup(TechniqueGroup.UniqueRectangle)]
	[StaticDifficultyLevel(DifficultyLevel.Hard)]
	[BoundStepTypes(typeof(UniqueRectangleWithConjugatePairStep))]
	[StaticDifficulty(4.4)]
	[ExtraDifficultyFactors(ConjugatePair, Avoidable)]
	UniqueRectangle4X3,

	/// <summary>
	/// Indicates unique rectangle + 4C / 3SL.
	/// </summary>
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Unique_Rectangle.html")]
	[ReferenceLink("http://forum.enjoysudoku.com/uniqueness-type-6-ur-meets-x-wing-t3709-30.html#p26448")]
	[ReferenceLink("http://forum.enjoysudoku.com/unique-rectangles-gallery-t33752.html")]
	[TechniqueGroup(TechniqueGroup.UniqueRectangle)]
	[StaticDifficultyLevel(DifficultyLevel.Hard)]
	[BoundStepTypes(typeof(UniqueRectangleWithConjugatePairStep))]
	[StaticDifficulty(4.4)]
	[ExtraDifficultyFactors(ConjugatePair, Avoidable)]
	UniqueRectangle4C3,

	/// <summary>
	/// Indicates unique rectangle-XY-Wing.
	/// </summary>
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Unique_Rectangle.html")]
	[ReferenceLink("http://forum.enjoysudoku.com/uniqueness-type-6-ur-meets-x-wing-t3709-30.html#p26448")]
	[ReferenceLink("http://forum.enjoysudoku.com/unique-rectangles-gallery-t33752.html")]
	[TechniqueGroup(TechniqueGroup.UniqueRectangle)]
	[StaticDifficultyLevel(DifficultyLevel.Hard)]
	[BoundStepTypes(typeof(UniqueRectangleWithWingStep))]
	[StaticDifficulty(4.5)]
	[ExtraDifficultyFactors(Avoidable, WingSize)]
	UniqueRectangleXyWing,

	/// <summary>
	/// Indicates unique rectangle-XYZ-Wing.
	/// </summary>
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Unique_Rectangle.html")]
	[ReferenceLink("http://forum.enjoysudoku.com/uniqueness-type-6-ur-meets-x-wing-t3709-30.html#p26448")]
	[ReferenceLink("http://forum.enjoysudoku.com/unique-rectangles-gallery-t33752.html")]
	[TechniqueGroup(TechniqueGroup.UniqueRectangle)]
	[StaticDifficultyLevel(DifficultyLevel.Hard)]
	[BoundStepTypes(typeof(UniqueRectangleWithWingStep))]
	[StaticDifficulty(4.5)]
	[ExtraDifficultyFactors(Avoidable, WingSize)]
	UniqueRectangleXyzWing,

	/// <summary>
	/// Indicates unique rectangle-WXYZ-Wing.
	/// </summary>
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Unique_Rectangle.html")]
	[ReferenceLink("http://forum.enjoysudoku.com/uniqueness-type-6-ur-meets-x-wing-t3709-30.html#p26448")]
	[ReferenceLink("http://forum.enjoysudoku.com/unique-rectangles-gallery-t33752.html")]
	[TechniqueGroup(TechniqueGroup.UniqueRectangle)]
	[StaticDifficultyLevel(DifficultyLevel.Hard)]
	[BoundStepTypes(typeof(UniqueRectangleWithWingStep))]
	[StaticDifficulty(4.5)]
	[ExtraDifficultyFactors(Avoidable, WingSize)]
	UniqueRectangleWxyzWing,

	/// <summary>
	/// Indicates unique rectangle W-Wing.
	/// </summary>
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Unique_Rectangle.html")]
	[ReferenceLink("http://forum.enjoysudoku.com/uniqueness-type-6-ur-meets-x-wing-t3709-30.html#p26448")]
	[ReferenceLink("http://forum.enjoysudoku.com/unique-rectangles-gallery-t33752.html")]
	[TechniqueGroup(TechniqueGroup.UniqueRectangle)]
	[StaticDifficultyLevel(DifficultyLevel.Hard)]
#if UNIQUE_RECTANGLE_W_WING
	[RuntimeStepTypes(typeof(UniqueRectangleWWingStep))]
	[BaseDifficulty(4.5)]
	[SupportedExtraDifficultyRules(Avoidable)]
#endif
	UniqueRectangleWWing,

	/// <summary>
	/// Indicates unique rectangle sue de coq.
	/// </summary>
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Unique_Rectangle.html")]
	[ReferenceLink("http://forum.enjoysudoku.com/uniqueness-type-6-ur-meets-x-wing-t3709-30.html#p26448")]
	[ReferenceLink("http://forum.enjoysudoku.com/unique-rectangles-gallery-t33752.html")]
	[TechniqueGroup(TechniqueGroup.UniqueRectangle)]
	[StaticDifficultyLevel(DifficultyLevel.Fiendish)]
	[BoundStepTypes(typeof(UniqueRectangleWithSueDeCoqStep))]
	[StaticDifficulty(5.0)]
	[ExtraDifficultyFactors(Size, Isolated, Cannibalism, Avoidable)]
	UniqueRectangleSueDeCoq,

	/// <summary>
	/// Indicates unique rectangle baba grouping.
	/// </summary>
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Unique_Rectangle.html")]
	[ReferenceLink("http://forum.enjoysudoku.com/uniqueness-type-6-ur-meets-x-wing-t3709-30.html#p26448")]
	[ReferenceLink("http://forum.enjoysudoku.com/unique-rectangles-gallery-t33752.html")]
	[TechniqueGroup(TechniqueGroup.UniqueRectangle)]
	[StaticDifficultyLevel(DifficultyLevel.Fiendish)]
	[BoundStepTypes(typeof(UniqueRectangleWithBabaGroupingStep))]
	[StaticDifficulty(4.9)]
	UniqueRectangleBabaGrouping,

	/// <summary>
	/// Indicates unique rectangle external type 1.
	/// </summary>
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Unique_Rectangle.html")]
	[ReferenceLink("http://forum.enjoysudoku.com/uniqueness-type-6-ur-meets-x-wing-t3709-30.html#p26448")]
	[ReferenceLink("http://forum.enjoysudoku.com/unique-rectangles-gallery-t33752.html")]
	[TechniqueGroup(TechniqueGroup.UniqueRectangle)]
	[StaticDifficultyLevel(DifficultyLevel.Fiendish)]
	[BoundStepTypes(typeof(UniqueRectangleExternalType1Or2Step))]
	[StaticDifficulty(4.5)]
	UniqueRectangleExternalType1,

	/// <summary>
	/// Indicates unique rectangle external type 2.
	/// </summary>
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Unique_Rectangle.html")]
	[ReferenceLink("http://forum.enjoysudoku.com/uniqueness-type-6-ur-meets-x-wing-t3709-30.html#p26448")]
	[ReferenceLink("http://forum.enjoysudoku.com/unique-rectangles-gallery-t33752.html")]
	[TechniqueGroup(TechniqueGroup.UniqueRectangle)]
	[StaticDifficultyLevel(DifficultyLevel.Fiendish)]
	[BoundStepTypes(typeof(UniqueRectangleExternalType1Or2Step))]
	[StaticDifficulty(4.5)]
	UniqueRectangleExternalType2,

	/// <summary>
	/// Indicates unique rectangle external type 3.
	/// </summary>
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Unique_Rectangle.html")]
	[ReferenceLink("http://forum.enjoysudoku.com/uniqueness-type-6-ur-meets-x-wing-t3709-30.html#p26448")]
	[ReferenceLink("http://forum.enjoysudoku.com/unique-rectangles-gallery-t33752.html")]
	[TechniqueGroup(TechniqueGroup.UniqueRectangle)]
	[StaticDifficultyLevel(DifficultyLevel.Fiendish)]
	[BoundStepTypes(typeof(UniqueRectangleExternalType3Step))]
	[StaticDifficulty(4.6)]
	[ExtraDifficultyFactors(Size, Avoidable, Incompleteness)]
	UniqueRectangleExternalType3,

	/// <summary>
	/// Indicates unique rectangle external type 4.
	/// </summary>
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Unique_Rectangle.html")]
	[ReferenceLink("http://forum.enjoysudoku.com/uniqueness-type-6-ur-meets-x-wing-t3709-30.html#p26448")]
	[ReferenceLink("http://forum.enjoysudoku.com/unique-rectangles-gallery-t33752.html")]
	[TechniqueGroup(TechniqueGroup.UniqueRectangle)]
	[StaticDifficultyLevel(DifficultyLevel.Fiendish)]
	[BoundStepTypes(typeof(UniqueRectangleExternalType4Step))]
	[StaticDifficulty(4.7)]
	[ExtraDifficultyFactors(Avoidable, Incompleteness)]
	UniqueRectangleExternalType4,

	/// <summary>
	/// Indicates unique rectangle external turbot fish.
	/// </summary>
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Unique_Rectangle.html")]
	[ReferenceLink("http://forum.enjoysudoku.com/uniqueness-type-6-ur-meets-x-wing-t3709-30.html#p26448")]
	[ReferenceLink("http://forum.enjoysudoku.com/unique-rectangles-gallery-t33752.html")]
	[TechniqueGroup(TechniqueGroup.UniqueRectangle)]
	[StaticDifficultyLevel(DifficultyLevel.Fiendish)]
	[BoundStepTypes(typeof(UniqueRectangleExternalTurbotFishStep))]
	[StaticDifficulty(4.6)]
	[ExtraDifficultyFactors(Guardian, Incompleteness)]
	UniqueRectangleExternalTurbotFish,

	/// <summary>
	/// Indicates unique rectangle external W-Wing.
	/// </summary>
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Unique_Rectangle.html")]
	[ReferenceLink("http://forum.enjoysudoku.com/uniqueness-type-6-ur-meets-x-wing-t3709-30.html#p26448")]
	[ReferenceLink("http://forum.enjoysudoku.com/unique-rectangles-gallery-t33752.html")]
	[TechniqueGroup(TechniqueGroup.UniqueRectangle)]
	[StaticDifficultyLevel(DifficultyLevel.Fiendish)]
	[BoundStepTypes(typeof(UniqueRectangleExternalWWingStep))]
	[StaticDifficulty(4.8)]
	[ExtraDifficultyFactors(Guardian, Avoidable, Incompleteness)]
	UniqueRectangleExternalWWing,

	/// <summary>
	/// Indicates unique rectangle external XY-Wing.
	/// </summary>
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Unique_Rectangle.html")]
	[ReferenceLink("http://forum.enjoysudoku.com/uniqueness-type-6-ur-meets-x-wing-t3709-30.html#p26448")]
	[ReferenceLink("http://forum.enjoysudoku.com/unique-rectangles-gallery-t33752.html")]
	[TechniqueGroup(TechniqueGroup.UniqueRectangle)]
	[StaticDifficultyLevel(DifficultyLevel.Fiendish)]
	[BoundStepTypes(typeof(UniqueRectangleExternalXyWingStep))]
	[StaticDifficulty(4.7)]
	[ExtraDifficultyFactors(Guardian, Avoidable, Incompleteness)]
	UniqueRectangleExternalXyWing,

	/// <summary>
	/// Indicates unique rectangle external almost locked sets XZ rule.
	/// </summary>
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Unique_Rectangle.html")]
	[ReferenceLink("http://forum.enjoysudoku.com/uniqueness-type-6-ur-meets-x-wing-t3709-30.html#p26448")]
	[ReferenceLink("http://forum.enjoysudoku.com/unique-rectangles-gallery-t33752.html")]
	[TechniqueGroup(TechniqueGroup.UniqueRectangle)]
	[StaticDifficultyLevel(DifficultyLevel.Fiendish)]
	[BoundStepTypes(typeof(UniqueRectangleExternalAlmostLockedSetsXzStep))]
	[StaticDifficulty(4.8)]
	[ExtraDifficultyFactors(Guardian, Avoidable, Incompleteness)]
	UniqueRectangleExternalAlmostLockedSetsXz,
	#endregion

	//
	// Avoidable Rectangle
	//
	#region Avoidable Rectangle
	/// <summary>
	/// Indicates avoidable rectangle type 1.
	/// </summary>
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Avoidable_Rectangle.html")]
	[HodokuTechniquePrefix("0607")]
	[HodokuDifficultyRating(100, HodokuDifficultyLevel.Hard)]
	[SudokuExplainerTechnique(SudokuExplainerTechnique.AvoidableRectangle, IsAdvancedDefined = true)]
	[SudokuExplainerDifficultyRating(4.7, IsAdvancedDefined = true)] // I think this difficulty may be a mistake.
	[TechniqueGroup(TechniqueGroup.AvoidableRectangle)]
	[StaticDifficultyLevel(DifficultyLevel.Hard)]
	[BoundStepTypes(typeof(UniqueRectangleType1Step))]
	[StaticDifficulty(4.5)]
	AvoidableRectangleType1,

	/// <summary>
	/// Indicates avoidable rectangle type 2.
	/// </summary>
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Avoidable_Rectangle.html")]
	[HodokuTechniquePrefix("0608")]
	[HodokuDifficultyRating(100, HodokuDifficultyLevel.Hard)]
	[SudokuExplainerTechnique(SudokuExplainerTechnique.AvoidableRectangle, IsAdvancedDefined = true)]
	[SudokuExplainerDifficultyRating(4.5, IsAdvancedDefined = true)] // I think this difficulty may be a mistake.
	[TechniqueGroup(TechniqueGroup.AvoidableRectangle)]
	[StaticDifficultyLevel(DifficultyLevel.Hard)]
	[BoundStepTypes(typeof(UniqueRectangleType2Step))]
	[StaticDifficulty(4.6)]
	AvoidableRectangleType2,

	/// <summary>
	/// Indicates avoidable rectangle type 3.
	/// </summary>
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Avoidable_Rectangle.html")]
	[TechniqueGroup(TechniqueGroup.AvoidableRectangle)]
	[StaticDifficultyLevel(DifficultyLevel.Hard)]
	[BoundStepTypes(typeof(UniqueRectangleType3Step))]
	[StaticDifficulty(4.5)]
	[ExtraDifficultyFactors(Size, Hidden)]
	AvoidableRectangleType3,

	/// <summary>
	/// Indicates avoidable rectangle type 5.
	/// </summary>
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Avoidable_Rectangle.html")]
	[TechniqueGroup(TechniqueGroup.AvoidableRectangle)]
	[StaticDifficultyLevel(DifficultyLevel.Hard)]
	[BoundStepTypes(typeof(UniqueRectangleType2Step))]
	[StaticDifficulty(4.5)]
	AvoidableRectangleType5,

	/// <summary>
	/// Indicates hidden avoidable rectangle.
	/// </summary>
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Avoidable_Rectangle.html")]
	[TechniqueGroup(TechniqueGroup.AvoidableRectangle)]
	[Abbreviation("HAR")]
	[StaticDifficultyLevel(DifficultyLevel.Hard)]
	[BoundStepTypes(typeof(HiddenUniqueRectangleStep), SecondaryTypes = [typeof(UniqueRectangleWithConjugatePairStep)])]
	[StaticDifficulty(4.4)]
	[ExtraDifficultyFactors(ConjugatePair, Avoidable)]
	HiddenAvoidableRectangle,

	/// <summary>
	/// Indicates avoidable rectangle + 2D.
	/// </summary>
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Avoidable_Rectangle.html")]
	[TechniqueGroup(TechniqueGroup.AvoidableRectangle)]
	[TechniqueFeature(TechniqueFeature.HardToBeGenerated)]
	[StaticDifficultyLevel(DifficultyLevel.Hard)]
	[BoundStepTypes(typeof(UniqueRectangle2DOr3XStep))]
	[StaticDifficulty(4.6)]
	AvoidableRectangle2D,

	/// <summary>
	/// Indicates avoidable rectangle + 3X.
	/// </summary>
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Avoidable_Rectangle.html")]
	[TechniqueGroup(TechniqueGroup.AvoidableRectangle)]
	[TechniqueFeature(TechniqueFeature.HardToBeGenerated)]
	[StaticDifficultyLevel(DifficultyLevel.Hard)]
	[BoundStepTypes(typeof(UniqueRectangle2DOr3XStep))]
	[StaticDifficulty(4.6)]
	AvoidableRectangle3X,

	/// <summary>
	/// Indicates avoidable rectangle XY-Wing.
	/// </summary>
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Avoidable_Rectangle.html")]
	[TechniqueGroup(TechniqueGroup.AvoidableRectangle)]
	[StaticDifficultyLevel(DifficultyLevel.Hard)]
	[BoundStepTypes(typeof(UniqueRectangleWithWingStep))]
	[StaticDifficulty(4.5)]
	[ExtraDifficultyFactors(Avoidable, WingSize)]
	AvoidableRectangleXyWing,

	/// <summary>
	/// Indicates avoidable rectangle XYZ-Wing.
	/// </summary>
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Avoidable_Rectangle.html")]
	[TechniqueGroup(TechniqueGroup.AvoidableRectangle)]
	[StaticDifficultyLevel(DifficultyLevel.Hard)]
	[BoundStepTypes(typeof(UniqueRectangleWithWingStep))]
	[StaticDifficulty(4.5)]
	[ExtraDifficultyFactors(Avoidable, WingSize)]
	AvoidableRectangleXyzWing,

	/// <summary>
	/// Indicates avoidable rectangle WXYZ-Wing.
	/// </summary>
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Avoidable_Rectangle.html")]
	[TechniqueGroup(TechniqueGroup.AvoidableRectangle)]
	[StaticDifficultyLevel(DifficultyLevel.Hard)]
	[BoundStepTypes(typeof(UniqueRectangleWithWingStep))]
	[StaticDifficulty(4.5)]
	[ExtraDifficultyFactors(Avoidable, WingSize)]
	AvoidableRectangleWxyzWing,

	/// <summary>
	/// Indicates avoidable rectangle W-Wing.
	/// </summary>
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Avoidable_Rectangle.html")]
	[TechniqueGroup(TechniqueGroup.AvoidableRectangle)]
	[StaticDifficultyLevel(DifficultyLevel.Hard)]
#if UNIQUE_RECTANGLE_W_WING
	[RuntimeStepTypes(typeof(UniqueRectangleWWingStep))]
	[BaseDifficulty(4.5)]
	[SupportedExtraDifficultyRules(Avoidable)]
#endif
	AvoidableRectangleWWing,

	/// <summary>
	/// Indicates avoidable rectangle sue de coq.
	/// </summary>
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Avoidable_Rectangle.html")]
	[TechniqueGroup(TechniqueGroup.AvoidableRectangle)]
	[StaticDifficultyLevel(DifficultyLevel.Fiendish)]
	[BoundStepTypes(typeof(UniqueRectangleWithSueDeCoqStep))]
	[StaticDifficulty(5.0)]
	[ExtraDifficultyFactors(Size, Isolated, Cannibalism, Avoidable)]
	AvoidableRectangleSueDeCoq,

	/// <summary>
	/// Indicates avoidable rectangle hidden single in block.
	/// </summary>
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Avoidable_Rectangle.html")]
	[TechniqueGroup(TechniqueGroup.AvoidableRectangle)]
	[StaticDifficultyLevel(DifficultyLevel.Hard)]
	[BoundStepTypes(typeof(AvoidableRectangleWithHiddenSingleStep))]
	[StaticDifficulty(4.7)]
	AvoidableRectangleHiddenSingleBlock,

	/// <summary>
	/// Indicates avoidable rectangle hidden single in row.
	/// </summary>
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Avoidable_Rectangle.html")]
	[TechniqueGroup(TechniqueGroup.AvoidableRectangle)]
	[StaticDifficultyLevel(DifficultyLevel.Hard)]
	[BoundStepTypes(typeof(AvoidableRectangleWithHiddenSingleStep))]
	[StaticDifficulty(4.7)]
	AvoidableRectangleHiddenSingleRow,

	/// <summary>
	/// Indicates avoidable rectangle hidden single in column.
	/// </summary>
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Avoidable_Rectangle.html")]
	[TechniqueGroup(TechniqueGroup.AvoidableRectangle)]
	[StaticDifficultyLevel(DifficultyLevel.Hard)]
	[BoundStepTypes(typeof(AvoidableRectangleWithHiddenSingleStep))]
	[StaticDifficulty(4.7)]
	AvoidableRectangleHiddenSingleColumn,

	/// <summary>
	/// Indicates avoidable rectangle external type 1.
	/// </summary>
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Avoidable_Rectangle.html")]
	[TechniqueGroup(TechniqueGroup.AvoidableRectangle)]
	[StaticDifficultyLevel(DifficultyLevel.Fiendish)]
	[BoundStepTypes(typeof(UniqueRectangleExternalType1Or2Step))]
	[StaticDifficulty(4.5)]
	AvoidableRectangleExternalType1,

	/// <summary>
	/// Indicates avoidable rectangle external type 2.
	/// </summary>
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Avoidable_Rectangle.html")]
	[TechniqueGroup(TechniqueGroup.AvoidableRectangle)]
	[StaticDifficultyLevel(DifficultyLevel.Fiendish)]
	[BoundStepTypes(typeof(UniqueRectangleExternalType1Or2Step))]
	[StaticDifficulty(4.5)]
	AvoidableRectangleExternalType2,

	/// <summary>
	/// Indicates avoidable rectangle external type 3.
	/// </summary>
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Avoidable_Rectangle.html")]
	[TechniqueGroup(TechniqueGroup.AvoidableRectangle)]
	[StaticDifficultyLevel(DifficultyLevel.Fiendish)]
	[BoundStepTypes(typeof(UniqueRectangleExternalType3Step))]
	[StaticDifficulty(4.6)]
	[ExtraDifficultyFactors(Size, Avoidable, Incompleteness)]
	AvoidableRectangleExternalType3,

	/// <summary>
	/// Indicates avoidable rectangle external type 4.
	/// </summary>
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Avoidable_Rectangle.html")]
	[TechniqueGroup(TechniqueGroup.AvoidableRectangle)]
	[StaticDifficultyLevel(DifficultyLevel.Fiendish)]
	[BoundStepTypes(typeof(UniqueRectangleExternalType4Step))]
	[StaticDifficulty(4.7)]
	[ExtraDifficultyFactors(Avoidable, Incompleteness)]
	AvoidableRectangleExternalType4,

	/// <summary>
	/// Indicates avoidable rectangle external XY-Wing.
	/// </summary>
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Avoidable_Rectangle.html")]
	[TechniqueGroup(TechniqueGroup.AvoidableRectangle)]
	[StaticDifficultyLevel(DifficultyLevel.Fiendish)]
	[BoundStepTypes(typeof(UniqueRectangleExternalXyWingStep))]
	[StaticDifficulty(4.7)]
	[ExtraDifficultyFactors(Guardian, Avoidable, Incompleteness)]
	AvoidableRectangleExternalXyWing,

	/// <summary>
	/// Indicates avoidable rectangle external W-Wing.
	/// </summary>
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Avoidable_Rectangle.html")]
	[TechniqueGroup(TechniqueGroup.AvoidableRectangle)]
	[StaticDifficultyLevel(DifficultyLevel.Fiendish)]
	[TechniqueFeature(TechniqueFeature.NotImplemented)]
	[BoundStepTypes(typeof(UniqueRectangleExternalWWingStep))]
	[StaticDifficulty(4.8)]
	[ExtraDifficultyFactors(Guardian, Avoidable, Incompleteness)]
	AvoidableRectangleExternalWWing,

	/// <summary>
	/// Indicates avoidable rectangle external almost locked sets XZ rule.
	/// </summary>
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Avoidable_Rectangle.html")]
	[TechniqueGroup(TechniqueGroup.AvoidableRectangle)]
	[StaticDifficultyLevel(DifficultyLevel.Fiendish)]
	[BoundStepTypes(typeof(UniqueRectangleExternalAlmostLockedSetsXzStep))]
	[StaticDifficulty(4.8)]
	[ExtraDifficultyFactors(Guardian, Avoidable, Incompleteness)]
	AvoidableRectangleExternalAlmostLockedSetsXz,
	#endregion

	//
	// Unique Loop
	//
	#region Unique Loop
	/// <summary>
	/// Indicates unique loop type 1.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/viewtopic.php?p=39748#p39748")]
	[SudokuExplainerTechnique(SudokuExplainerTechnique.UniqueLoop)]
	[SudokuExplainerDifficultyRating(4.6, 5.0)]
	[TechniqueGroup(TechniqueGroup.UniqueLoop)]
	[StaticDifficultyLevel(DifficultyLevel.Hard)]
	[BoundStepTypes(typeof(UniqueLoopType1Step))]
	[StaticDifficulty(4.5)]
	[ExtraDifficultyFactors(Length)]
	UniqueLoopType1,

	/// <summary>
	/// Indicates unique loop type 2.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/viewtopic.php?p=39748#p39748")]
	[SudokuExplainerTechnique(SudokuExplainerTechnique.UniqueLoop)]
	[SudokuExplainerDifficultyRating(4.6, 5.0)]
	[SudokuExplainerDifficultyRating(4.7, 5.1, IsAdvancedDefined = true)]
	[TechniqueGroup(TechniqueGroup.UniqueLoop)]
	[StaticDifficultyLevel(DifficultyLevel.Hard)]
	[BoundStepTypes(typeof(UniqueLoopType2Step))]
	[StaticDifficulty(4.6)]
	[ExtraDifficultyFactors(Length)]
	UniqueLoopType2,

	/// <summary>
	/// Indicates unique loop type 3.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/viewtopic.php?p=39748#p39748")]
	[SudokuExplainerTechnique(SudokuExplainerTechnique.UniqueLoop)]
	[SudokuExplainerDifficultyRating(4.6, 5.0)]
	[SudokuExplainerDifficultyRating(4.7, 5.1, IsAdvancedDefined = true)]
	[TechniqueGroup(TechniqueGroup.UniqueLoop)]
	[StaticDifficultyLevel(DifficultyLevel.Hard)]
	[BoundStepTypes(typeof(UniqueLoopType3Step))]
	[StaticDifficulty(4.5)]
	[ExtraDifficultyFactors(Length, Size)]
	UniqueLoopType3,

	/// <summary>
	/// Indicates unique loop type 4.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/viewtopic.php?p=39748#p39748")]
	[SudokuExplainerTechnique(SudokuExplainerTechnique.UniqueLoop)]
	[SudokuExplainerDifficultyRating(4.6, 5.0)]
	[SudokuExplainerDifficultyRating(4.7, 5.1, IsAdvancedDefined = true)]
	[TechniqueGroup(TechniqueGroup.UniqueLoop)]
	[StaticDifficultyLevel(DifficultyLevel.Hard)]
	[BoundStepTypes(typeof(UniqueLoopType4Step))]
	[StaticDifficulty(4.6)]
	[ExtraDifficultyFactors(Length)]
	UniqueLoopType4,
	#endregion

	//
	// Extended Rectangle
	//
	#region Extended Rectangle
	/// <summary>
	/// Indicates extended rectangle type 1.
	/// </summary>
#if COMPATIBLE_ORIGINAL_TECHNIQUE_RULES
	[HodokuTechniquePrefix("0620")]
#endif
	[TechniqueGroup(TechniqueGroup.ExtendedRectangle)]
	[StaticDifficultyLevel(DifficultyLevel.Hard)]
	[BoundStepTypes(typeof(ExtendedRectangleType1Step))]
	[StaticDifficulty(4.5)]
	[ExtraDifficultyFactors(Size)]
	ExtendedRectangleType1,

	/// <summary>
	/// Indicates extended rectangle type 2.
	/// </summary>
#if COMPATIBLE_ORIGINAL_TECHNIQUE_RULES
	[HodokuTechniquePrefix("0621")]
#endif
	[TechniqueGroup(TechniqueGroup.ExtendedRectangle)]
	[StaticDifficultyLevel(DifficultyLevel.Hard)]
	[BoundStepTypes(typeof(ExtendedRectangleType2Step))]
	[StaticDifficulty(4.6)]
	[ExtraDifficultyFactors(Size)]
	ExtendedRectangleType2,

	/// <summary>
	/// Indicates extended rectangle type 3.
	/// </summary>
#if COMPATIBLE_ORIGINAL_TECHNIQUE_RULES
	[HodokuTechniquePrefix("0622")]
#endif
	[TechniqueGroup(TechniqueGroup.ExtendedRectangle)]
	[StaticDifficultyLevel(DifficultyLevel.Hard)]
	[BoundStepTypes(typeof(ExtendedRectangleType3Step))]
	[StaticDifficulty(4.5)]
	[ExtraDifficultyFactors(Size, ExtraDigit)]
	ExtendedRectangleType3,

	/// <summary>
	/// Indicates extended rectangle type 4.
	/// </summary>
#if COMPATIBLE_ORIGINAL_TECHNIQUE_RULES
	[HodokuTechniquePrefix("0623")]
#endif
	[TechniqueGroup(TechniqueGroup.ExtendedRectangle)]
	[StaticDifficultyLevel(DifficultyLevel.Hard)]
	[BoundStepTypes(typeof(ExtendedRectangleType4Step))]
	[StaticDifficulty(4.6)]
	[ExtraDifficultyFactors(Size)]
	ExtendedRectangleType4,
	#endregion

	//
	// Bivalue Universal Grave
	//
	#region Bivalue Universal Grave
	/// <summary>
	/// Indicates bi-value universal grave type 1.
	/// </summary>
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Bivalue_Universal_Grave.html")]
	[ReferenceLink("http://forum.enjoysudoku.com/viewtopic.php?t=2352")]
	[HodokuTechniquePrefix("0610")]
	[HodokuDifficultyRating(100, HodokuDifficultyLevel.Hard)]
	[HodokuAliasedNames("Bivalue Universal Grave + 1")]
	[SudokuExplainerTechnique(SudokuExplainerTechnique.BivalueUniversalGrave)]
	[SudokuExplainerDifficultyRating(5.6)]
	[TechniqueGroup(TechniqueGroup.BivalueUniversalGrave)]
	[StaticDifficultyLevel(DifficultyLevel.Hard)]
	[BoundStepTypes(typeof(BivalueUniversalGraveType1Step))]
	[StaticDifficulty(5.6)]
	BivalueUniversalGraveType1,

	/// <summary>
	/// Indicates bi-value universal grave type 2.
	/// </summary>
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Bivalue_Universal_Grave.html")]
	[ReferenceLink("http://forum.enjoysudoku.com/viewtopic.php?t=2352")]
	[SudokuExplainerTechnique(SudokuExplainerTechnique.BivalueUniversalGrave)]
	[SudokuExplainerDifficultyRating(5.7)]
	[TechniqueGroup(TechniqueGroup.BivalueUniversalGrave)]
	[StaticDifficultyLevel(DifficultyLevel.Hard)]
	[BoundStepTypes(typeof(BivalueUniversalGraveType2Step))]
	[StaticDifficulty(5.6)]
	[ExtraDifficultyFactors(ExtraDigit)]
	BivalueUniversalGraveType2,

	/// <summary>
	/// Indicates bi-value universal grave type 3.
	/// </summary>
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Bivalue_Universal_Grave.html")]
	[ReferenceLink("http://forum.enjoysudoku.com/viewtopic.php?t=2352")]
	[SudokuExplainerTechnique(SudokuExplainerTechnique.BivalueUniversalGrave)]
	[SudokuExplainerDifficultyRating(5.8, 6.1)]
	[TechniqueGroup(TechniqueGroup.BivalueUniversalGrave)]
	[StaticDifficultyLevel(DifficultyLevel.Hard)]
	[BoundStepTypes(typeof(BivalueUniversalGraveType3Step))]
	[StaticDifficulty(5.6)]
	[ExtraDifficultyFactors(Size, Hidden)]
	BivalueUniversalGraveType3,

	/// <summary>
	/// Indicates bi-value universal grave type 4.
	/// </summary>
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Bivalue_Universal_Grave.html")]
	[ReferenceLink("http://forum.enjoysudoku.com/viewtopic.php?t=2352")]
	[SudokuExplainerTechnique(SudokuExplainerTechnique.BivalueUniversalGrave)]
	[SudokuExplainerDifficultyRating(5.7)]
	[TechniqueGroup(TechniqueGroup.BivalueUniversalGrave)]
	[StaticDifficultyLevel(DifficultyLevel.Hard)]
	[BoundStepTypes(typeof(BivalueUniversalGraveType4Step))]
	[StaticDifficulty(5.6)]
	[ExtraDifficultyFactors(ConjugatePair)]
	BivalueUniversalGraveType4,

	/// <summary>
	/// Indicates bi-value universal grave + n.
	/// </summary>
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Bivalue_Universal_Grave.html")]
	[ReferenceLink("http://forum.enjoysudoku.com/viewtopic.php?t=2352")]
	[SudokuExplainerTechnique(SudokuExplainerTechnique.BivalueUniversalGravePlusN, IsAdvancedDefined = true)]
	[SudokuExplainerDifficultyRating(5.7)]
	[TechniqueGroup(TechniqueGroup.BivalueUniversalGrave)]
	[Abbreviation("BUG + n")]
	[StaticDifficultyLevel(DifficultyLevel.Hard)]
	[BoundStepTypes(typeof(BivalueUniversalGraveMultipleStep))]
	[StaticDifficulty(5.7)]
	[ExtraDifficultyFactors(Size)]
	BivalueUniversalGravePlusN,

	/// <summary>
	/// Indicates bi-value universal grave false candidate type.
	/// </summary>
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Bivalue_Universal_Grave.html")]
	[ReferenceLink("http://forum.enjoysudoku.com/viewtopic.php?t=2352")]
	[TechniqueGroup(TechniqueGroup.BivalueUniversalGrave)]
	[StaticDifficultyLevel(DifficultyLevel.Hard)]
	[BoundStepTypes(typeof(BivalueUniversalGraveFalseCandidateTypeStep))]
	[StaticDifficulty(5.7)]
	BivalueUniversalGraveFalseCandidateType,

	/// <summary>
	/// Indicates bi-value universal grave + n with forcing chains.
	/// </summary>
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Bivalue_Universal_Grave.html")]
	[ReferenceLink("http://forum.enjoysudoku.com/viewtopic.php?t=2352")]
	[TechniqueGroup(TechniqueGroup.BivalueUniversalGrave)]
	[StaticDifficultyLevel(DifficultyLevel.Nightmare)]
	[TechniqueFeature(TechniqueFeature.NotImplemented)]
	BivalueUniversalGravePlusNForcingChains,

	/// <summary>
	/// Indicates bi-value universal grave XZ rule.
	/// </summary>
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Bivalue_Universal_Grave.html")]
	[ReferenceLink("http://forum.enjoysudoku.com/viewtopic.php?t=2352")]
	[TechniqueGroup(TechniqueGroup.BivalueUniversalGrave)]
	[Abbreviation("BUG-XZ")]
	[StaticDifficultyLevel(DifficultyLevel.Hard)]
	[BoundStepTypes(typeof(BivalueUniversalGraveXzStep))]
	[StaticDifficulty(5.8)]
	BivalueUniversalGraveXzRule,

	/// <summary>
	/// Indicates bi-value universal grave XY-Wing.
	/// </summary>
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Bivalue_Universal_Grave.html")]
	[ReferenceLink("http://forum.enjoysudoku.com/viewtopic.php?t=2352")]
	[TechniqueGroup(TechniqueGroup.BivalueUniversalGrave)]
	[Abbreviation("BUG-XY-Wing")]
	[StaticDifficultyLevel(DifficultyLevel.Hard)]
	BivalueUniversalGraveXyWing,
	#endregion

	//
	// Reverse Bivalue Universal Grave
	//
	#region Reverse Bivalue Universal Grave
	/// <summary>
	/// Indicates reverse bi-value universal grave type 1.
	/// </summary>
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Reverse_BUG.html")]
	[ReferenceLink("http://forum.enjoysudoku.com/viewtopic.php?t=4431")]
	[TechniqueGroup(TechniqueGroup.ReverseBivalueUniversalGrave)]
	[StaticDifficultyLevel(DifficultyLevel.Fiendish)]
	[BoundStepTypes(typeof(ReverseBivalueUniversalGraveType1Step))]
	[StaticDifficulty(6.0)]
	[ExtraDifficultyFactors(Length)]
	ReverseBivalueUniversalGraveType1,

	/// <summary>
	/// Indicates reverse bi-value universal grave type 2.
	/// </summary>
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Reverse_BUG.html")]
	[ReferenceLink("http://forum.enjoysudoku.com/viewtopic.php?t=4431")]
	[TechniqueGroup(TechniqueGroup.ReverseBivalueUniversalGrave)]
	[StaticDifficultyLevel(DifficultyLevel.Fiendish)]
	[BoundStepTypes(typeof(ReverseBivalueUniversalGraveType2Step))]
	[StaticDifficulty(6.1)]
	[ExtraDifficultyFactors(Length)]
	ReverseBivalueUniversalGraveType2,

	/// <summary>
	/// Indicates reverse bi-value universal grave type 3.
	/// </summary>
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Reverse_BUG.html")]
	[ReferenceLink("http://forum.enjoysudoku.com/viewtopic.php?t=4431")]
	[TechniqueGroup(TechniqueGroup.ReverseBivalueUniversalGrave)]
	[StaticDifficultyLevel(DifficultyLevel.Fiendish)]
	[BoundStepTypes(typeof(ReverseBivalueUniversalGraveType3Step))]
	[StaticDifficulty(6.0)]
	[ExtraDifficultyFactors(Length)]
	ReverseBivalueUniversalGraveType3,

	/// <summary>
	/// Indicates reverse bi-value universal grave type 4.
	/// </summary>
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Reverse_BUG.html")]
	[ReferenceLink("http://forum.enjoysudoku.com/viewtopic.php?t=4431")]
	[TechniqueGroup(TechniqueGroup.ReverseBivalueUniversalGrave)]
	[StaticDifficultyLevel(DifficultyLevel.Fiendish)]
	[BoundStepTypes(typeof(ReverseBivalueUniversalGraveType4Step))]
	[StaticDifficulty(6.3)]
	[ExtraDifficultyFactors(Length)]
	ReverseBivalueUniversalGraveType4,
	#endregion

	//
	// Uniqueness Clue Cover
	//
	#region Uniqueness Clue Cover
	/// <summary>
	/// Indicates uniqueness clue cover.
	/// </summary>
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Uniqueness_Clue_Cover.html")]
	[ReferenceLink("http://forum.enjoysudoku.com/uniqueness-clue-cover-t40814.html")]
	[TechniqueGroup(TechniqueGroup.UniquenessClueCover)]
	[TechniqueFeature(TechniqueFeature.HardToBeGenerated)]
	[StaticDifficultyLevel(DifficultyLevel.Fiendish)]
	[BoundStepTypes(typeof(UniquenessClueCoverStep))]
	[StaticDifficulty(6.5)]
	[ExtraDifficultyFactors(Size)]
	UniquenessClueCover,
	#endregion

	//
	// RW's Theory
	//
	#region RW's Theory
	/// <summary>
	/// Indicates RW's deadly pattern.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/yet-another-crazy-uniqueness-technique-t5589.html")]
	[TechniqueGroup(TechniqueGroup.RwDeadlyPattern)]
	[TechniqueFeature(TechniqueFeature.NotImplemented)]
	RwDeadlyPattern,
	#endregion

	//
	// Borescoper's Deadly Pattern
	//
	#region Borescoper's Deadly Pattern
	/// <summary>
	/// Indicates Borescoper's deadly pattern type 1.
	/// </summary>
	[TechniqueGroup(TechniqueGroup.BorescoperDeadlyPattern)]
	[StaticDifficultyLevel(DifficultyLevel.Hard)]
	[BoundStepTypes(typeof(BorescoperDeadlyPatternType1Step))]
	[StaticDifficulty(5.3)]
	BorescoperDeadlyPatternType1,

	/// <summary>
	/// Indicates Borescoper's deadly pattern type 2.
	/// </summary>
	[TechniqueGroup(TechniqueGroup.BorescoperDeadlyPattern)]
	[StaticDifficultyLevel(DifficultyLevel.Hard)]
	[BoundStepTypes(typeof(BorescoperDeadlyPatternType2Step))]
	[StaticDifficulty(5.5)]
	BorescoperDeadlyPatternType2,

	/// <summary>
	/// Indicates Borescoper's deadly pattern type 3.
	/// </summary>
	[TechniqueGroup(TechniqueGroup.BorescoperDeadlyPattern)]
	[StaticDifficultyLevel(DifficultyLevel.Hard)]
	[BoundStepTypes(typeof(BorescoperDeadlyPatternType3Step))]
	[StaticDifficulty(5.3)]
	[ExtraDifficultyFactors(Size)]
	BorescoperDeadlyPatternType3,

	/// <summary>
	/// Indicates Borescoper's deadly pattern type 4.
	/// </summary>
	[TechniqueGroup(TechniqueGroup.BorescoperDeadlyPattern)]
	[StaticDifficultyLevel(DifficultyLevel.Hard)]
	[BoundStepTypes(typeof(BorescoperDeadlyPatternType4Step))]
	[StaticDifficulty(5.5)]
	BorescoperDeadlyPatternType4,
	#endregion

	//
	// Qiu's Deadly Pattern
	//
	#region Qiu's Deadly Pattern
	/// <summary>
	/// Indicates Qiu's deadly pattern type 1.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/distinction-theory-t35042.html")]
	[TechniqueGroup(TechniqueGroup.QiuDeadlyPattern)]
	[TechniqueFeature(TechniqueFeature.HardToBeGenerated)]
	[StaticDifficultyLevel(DifficultyLevel.Fiendish)]
	[BoundStepTypes(typeof(QiuDeadlyPatternType1Step))]
	[StaticDifficulty(5.8)]
	QiuDeadlyPatternType1,

	/// <summary>
	/// Indicates Qiu's deadly pattern type 2.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/distinction-theory-t35042.html")]
	[TechniqueGroup(TechniqueGroup.QiuDeadlyPattern)]
	[TechniqueFeature(TechniqueFeature.HardToBeGenerated)]
	[StaticDifficultyLevel(DifficultyLevel.Fiendish)]
	[BoundStepTypes(typeof(QiuDeadlyPatternType2Step))]
	[StaticDifficulty(5.9)]
	QiuDeadlyPatternType2,

	/// <summary>
	/// Indicates Qiu's deadly pattern type 3.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/distinction-theory-t35042.html")]
	[TechniqueGroup(TechniqueGroup.QiuDeadlyPattern)]
	[TechniqueFeature(TechniqueFeature.HardToBeGenerated)]
	[StaticDifficultyLevel(DifficultyLevel.Fiendish)]
	[BoundStepTypes(typeof(QiuDeadlyPatternType3Step))]
	[StaticDifficulty(5.8)]
	[ExtraDifficultyFactors(Size)]
	QiuDeadlyPatternType3,

	/// <summary>
	/// Indicates Qiu's deadly pattern type 4.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/distinction-theory-t35042.html")]
	[TechniqueGroup(TechniqueGroup.QiuDeadlyPattern)]
	[TechniqueFeature(TechniqueFeature.HardToBeGenerated)]
	[StaticDifficultyLevel(DifficultyLevel.Fiendish)]
	[BoundStepTypes(typeof(QiuDeadlyPatternType4Step))]
	[StaticDifficulty(6.0)]
	QiuDeadlyPatternType4,

	/// <summary>
	/// Indicates locked Qiu's deadly pattern.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/distinction-theory-t35042.html")]
	[TechniqueGroup(TechniqueGroup.QiuDeadlyPattern)]
	[TechniqueFeature(TechniqueFeature.HardToBeGenerated)]
	[StaticDifficultyLevel(DifficultyLevel.Fiendish)]
	[BoundStepTypes(typeof(QiuDeadlyPatternLockedTypeStep))]
	[StaticDifficulty(6.0)]
	LockedQiuDeadlyPattern,

	/// <summary>
	/// Indicates Qiu's deadly pattern external type 1.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/distinction-theory-t35042.html")]
	[TechniqueGroup(TechniqueGroup.QiuDeadlyPattern)]
	[TechniqueFeature(TechniqueFeature.HardToBeGenerated)]
	[StaticDifficultyLevel(DifficultyLevel.Fiendish)]
	[BoundStepTypes(typeof(QiuDeadlyPatternExternalType1Step))]
	[StaticDifficulty(6.0)]
	QiuDeadlyPatternExternalType1,

	/// <summary>
	/// Indicates Qiu's deadly pattern external type 2.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/distinction-theory-t35042.html")]
	[TechniqueGroup(TechniqueGroup.QiuDeadlyPattern)]
	[TechniqueFeature(TechniqueFeature.HardToBeGenerated)]
	[StaticDifficultyLevel(DifficultyLevel.Fiendish)]
	[BoundStepTypes(typeof(QiuDeadlyPatternExternalType2Step))]
	[StaticDifficulty(6.1)]
	QiuDeadlyPatternExternalType2,
	#endregion

	//
	// Unique Matrix
	//
	#region Unique Matrix
	/// <summary>
	/// Indicates unique matrix type 1.
	/// </summary>
	[TechniqueGroup(TechniqueGroup.UniqueMatrix)]
	[TechniqueFeature(TechniqueFeature.HardToBeGenerated)]
	[StaticDifficultyLevel(DifficultyLevel.Fiendish)]
	[BoundStepTypes(typeof(UniqueMatrixType1Step))]
	[StaticDifficulty(5.3)]
	UniqueMatrixType1,

	/// <summary>
	/// Indicates unique matrix type 2.
	/// </summary>
	[TechniqueGroup(TechniqueGroup.UniqueMatrix)]
	[TechniqueFeature(TechniqueFeature.HardToBeGenerated)]
	[StaticDifficultyLevel(DifficultyLevel.Fiendish)]
	[BoundStepTypes(typeof(UniqueMatrixType2Step))]
	[StaticDifficulty(5.4)]
	UniqueMatrixType2,

	/// <summary>
	/// Indicates unique matrix type 3.
	/// </summary>
	[TechniqueGroup(TechniqueGroup.UniqueMatrix)]
	[TechniqueFeature(TechniqueFeature.HardToBeGenerated)]
	[StaticDifficultyLevel(DifficultyLevel.Fiendish)]
	[BoundStepTypes(typeof(UniqueMatrixType3Step))]
	[StaticDifficulty(5.3)]
	[ExtraDifficultyFactors(Size)]
	UniqueMatrixType3,

	/// <summary>
	/// Indicates unique matrix type 4.
	/// </summary>
	[TechniqueGroup(TechniqueGroup.UniqueMatrix)]
	[TechniqueFeature(TechniqueFeature.HardToBeGenerated)]
	[StaticDifficultyLevel(DifficultyLevel.Fiendish)]
	[BoundStepTypes(typeof(UniqueMatrixType4Step))]
	[StaticDifficulty(5.5)]
	UniqueMatrixType4,
	#endregion

	//
	// Sue de Coq
	//
	#region Sue de Coq
	/// <summary>
	/// Indicates sue de coq.
	/// </summary>
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Sue_de_Coq.html")]
	[ReferenceLink("http://forum.enjoysudoku.com/two-sector-disjoint-subsets-t2033.html")]
	[ReferenceLink("http://forum.enjoysudoku.com/benchmark-sudoku-list-t3834-15.html#p43170")]
	[HodokuTechniquePrefix("1101")]
	[HodokuDifficultyRating(250, HodokuDifficultyLevel.Unfair)]
	[SudokuExplainerDifficultyRating(5.0, IsAdvancedDefined = true)]
	[TechniqueGroup(TechniqueGroup.SueDeCoq)]
	[Abbreviation("SdC")]
	[StaticDifficultyLevel(DifficultyLevel.Fiendish)]
	[BoundStepTypes(typeof(SueDeCoqStep))]
	[StaticDifficulty(5.0)]
	[ExtraDifficultyFactors(Isolated, Cannibalism)]
	SueDeCoq,

	/// <summary>
	/// Indicates sue de coq with isolated digit.
	/// </summary>
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Sue_de_Coq.html")]
	[ReferenceLink("http://forum.enjoysudoku.com/two-sector-disjoint-subsets-t2033.html")]
	[ReferenceLink("http://forum.enjoysudoku.com/benchmark-sudoku-list-t3834-15.html#p43170")]
	[HodokuTechniquePrefix("1101")]
	[HodokuDifficultyRating(250, HodokuDifficultyLevel.Unfair)]
	[SudokuExplainerDifficultyRating(5.0, IsAdvancedDefined = true)]
	[TechniqueGroup(TechniqueGroup.SueDeCoq)]
	[StaticDifficultyLevel(DifficultyLevel.Fiendish)]
	[BoundStepTypes(typeof(SueDeCoqStep))]
	[StaticDifficulty(5.0)]
	[ExtraDifficultyFactors(Isolated, Cannibalism)]
	SueDeCoqIsolated,

	/// <summary>
	/// Indicates 3-dimensional sue de coq.
	/// </summary>
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Sue_de_Coq.html")]
	[TechniqueGroup(TechniqueGroup.SueDeCoq)]
	[StaticDifficultyLevel(DifficultyLevel.Fiendish)]
	[BoundStepTypes(typeof(SueDeCoq3DimensionStep))]
	[StaticDifficulty(5.5)]
	SueDeCoq3Dimension,

	/// <summary>
	/// Indicates sue de coq cannibalism.
	/// </summary>
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Sue_de_Coq.html")]
	[TechniqueGroup(TechniqueGroup.SueDeCoq)]
	[StaticDifficultyLevel(DifficultyLevel.Fiendish)]
	[BoundStepTypes(typeof(SueDeCoqStep))]
	[StaticDifficulty(5.0)]
	[ExtraDifficultyFactors(Isolated, Cannibalism)]
	SueDeCoqCannibalism,
	#endregion

	//
	// Fireworks
	//
	#region Fireworks
	/// <summary>
	/// Indicates firework triple.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/fireworks-t39513.html")]
	[TechniqueGroup(TechniqueGroup.Firework)]
	[StaticDifficultyLevel(DifficultyLevel.Fiendish)]
	[BoundStepTypes(typeof(FireworkTripleStep))]
	[StaticDifficulty(6.0)]
	FireworkTriple,

	/// <summary>
	/// Indicates firework quadruple.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/fireworks-t39513.html")]
	[TechniqueGroup(TechniqueGroup.Firework)]
	[StaticDifficultyLevel(DifficultyLevel.Fiendish)]
	[BoundStepTypes(typeof(FireworkQuadrupleStep))]
	[StaticDifficulty(6.3)]
	FireworkQuadruple,
	#endregion

	//
	// Broken Wing
	//
	#region Broken Wing
	/// <summary>
	/// Indicates broken wing.
	/// </summary>
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Broken_Wing.html")]
	[HodokuTechniquePrefix("0705")]
	[TechniqueGroup(TechniqueGroup.BrokenWing)]
	[StaticDifficultyLevel(DifficultyLevel.Fiendish)]
	[BoundStepTypes(typeof(GuardianStep))]
	[StaticDifficulty(5.5)]
	BrokenWing,
	#endregion

	//
	// Bi-value Oddagon
	//
	#region Bivalue Oddagon
	/// <summary>
	/// Indicates bi-value oddagon type 2.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/technique-share-odd-bivalue-loop-bivalue-oddagon-t33153.html")]
	[TechniqueGroup(TechniqueGroup.BivalueOddagon)]
	[StaticDifficultyLevel(DifficultyLevel.Fiendish)]
	[BoundStepTypes(typeof(BivalueOddagonType2Step))]
	[StaticDifficulty(6.1)]
	BivalueOddagonType2,

	/// <summary>
	/// Indicates bi-value oddagon type 3.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/technique-share-odd-bivalue-loop-bivalue-oddagon-t33153.html")]
	[TechniqueGroup(TechniqueGroup.BivalueOddagon)]
	[StaticDifficultyLevel(DifficultyLevel.Fiendish)]
	[BoundStepTypes(typeof(BivalueOddagonType3Step))]
	[StaticDifficulty(6.0)]
	[ExtraDifficultyFactors(Size)]
	BivalueOddagonType3,
	#endregion

	//
	// Chromatic Pattern
	//
	#region Chromatic Pattern
	/// <summary>
	/// Indicates chromatic pattern (tri-value oddagon) type 1.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/chromatic-patterns-t39885.html")]
	[ReferenceLink("http://forum.enjoysudoku.com/the-tridagon-rule-t39859.html")]
	[TechniqueGroup(TechniqueGroup.RankTheory)]
	[TechniqueFeature(TechniqueFeature.HardToBeGenerated)]
	[StaticDifficultyLevel(DifficultyLevel.Fiendish)]
	[BoundStepTypes(typeof(ChromaticPatternType1Step))]
	[StaticDifficulty(6.5)]
	ChromaticPatternType1,

	/// <summary>
	/// Indicates chromatic pattern (tri-value oddagon) type 2.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/chromatic-patterns-t39885.html")]
	[ReferenceLink("http://forum.enjoysudoku.com/the-tridagon-rule-t39859.html")]
	[TechniqueGroup(TechniqueGroup.RankTheory)]
	[TechniqueFeature(TechniqueFeature.HardToBeGenerated)]
	[StaticDifficultyLevel(DifficultyLevel.Fiendish)]
	ChromaticPatternType2,

	/// <summary>
	/// Indicates chromatic pattern (tri-value oddagon) type 3.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/chromatic-patterns-t39885.html")]
	[ReferenceLink("http://forum.enjoysudoku.com/the-tridagon-rule-t39859.html")]
	[TechniqueGroup(TechniqueGroup.RankTheory)]
	[TechniqueFeature(TechniqueFeature.HardToBeGenerated)]
	[StaticDifficultyLevel(DifficultyLevel.Fiendish)]
	ChromaticPatternType3,

	/// <summary>
	/// Indicates chromatic pattern (tri-value oddagon) type 4.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/chromatic-patterns-t39885.html")]
	[ReferenceLink("http://forum.enjoysudoku.com/the-tridagon-rule-t39859.html")]
	[TechniqueGroup(TechniqueGroup.RankTheory)]
	[TechniqueFeature(TechniqueFeature.HardToBeGenerated)]
	[StaticDifficultyLevel(DifficultyLevel.Fiendish)]
	ChromaticPatternType4,

	/// <summary>
	/// Indicates chromatic pattern (tri-value oddagon) XZ rule.
	/// </summary>
	[TechniqueGroup(TechniqueGroup.RankTheory)]
	[TechniqueFeature(TechniqueFeature.HardToBeGenerated)]
	[StaticDifficultyLevel(DifficultyLevel.Fiendish)]
	[BoundStepTypes(typeof(ChromaticPatternXzStep))]
	[StaticDifficulty(6.7)]
	ChromaticPatternXzRule,
	#endregion

	//
	// Single Digit Pattern
	//
	#region Single Digit Pattern
	/// <summary>
	/// Indicates skyscraper.
	/// </summary>
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Skyscraper.html")]
	[HodokuTechniquePrefix("0400")]
	[HodokuDifficultyRating(130, HodokuDifficultyLevel.Hard)]
#if COMPATIBLE_ORIGINAL_TECHNIQUE_RULES
	[SudokuExplainerTechnique(SudokuExplainerTechnique.TurbotFish)]
	[SudokuExplainerDifficultyRating(6.6)]
	[SudokuExplainerAliasNames("Turbot Fish")]
#endif
	[TechniqueGroup(TechniqueGroup.SingleDigitPattern)]
	[StaticDifficultyLevel(DifficultyLevel.Hard)]
	[BoundStepTypes(typeof(TwoStrongLinksStep))]
	[StaticDifficulty(4.0)]
	Skyscraper,

	/// <summary>
	/// Indicates two-string kite.
	/// </summary>
	[ReferenceLink("http://sudopedia.enjoysudoku.com/2-String_Kite.html")]
	[HodokuTechniquePrefix("0401")]
	[HodokuDifficultyRating(150, HodokuDifficultyLevel.Hard)]
#if COMPATIBLE_ORIGINAL_TECHNIQUE_RULES
	[SudokuExplainerTechnique(SudokuExplainerTechnique.TurbotFish)]
	[SudokuExplainerDifficultyRating(6.6)]
	[SudokuExplainerAliasNames("Turbot Fish")]
#endif
	[TechniqueGroup(TechniqueGroup.SingleDigitPattern)]
	[StaticDifficultyLevel(DifficultyLevel.Hard)]
	[BoundStepTypes(typeof(TwoStrongLinksStep))]
	[StaticDifficulty(4.1)]
	TwoStringKite,

	/// <summary>
	/// Indicates turbot fish.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/viewtopic.php?t=833")]
	[HodokuTechniquePrefix("0403")]
	[HodokuDifficultyRating(120, HodokuDifficultyLevel.Hard)]
	[SudokuExplainerTechnique(SudokuExplainerTechnique.TurbotFish)]
	[SudokuExplainerDifficultyRating(6.6)]
	[TechniqueGroup(TechniqueGroup.SingleDigitPattern)]
	[StaticDifficultyLevel(DifficultyLevel.Hard)]
	[BoundStepTypes(typeof(TwoStrongLinksStep))]
	[StaticDifficulty(4.2)]
	TurbotFish,

	/// <summary>
	/// Indicates grouped skyscraper.
	/// </summary>
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Skyscraper.html")]
	[TechniqueGroup(TechniqueGroup.SingleDigitPattern)]
	[StaticDifficultyLevel(DifficultyLevel.Hard)]
	[BoundStepTypes(typeof(TwoStrongLinksStep))]
	[StaticDifficulty(4.2)]
	GroupedSkyscraper,

	/// <summary>
	/// Indicates grouped two-string kite.
	/// </summary>
	[ReferenceLink("http://sudopedia.enjoysudoku.com/2-String_Kite.html")]
	[TechniqueGroup(TechniqueGroup.SingleDigitPattern)]
	[StaticDifficultyLevel(DifficultyLevel.Hard)]
	[BoundStepTypes(typeof(TwoStrongLinksStep))]
	[StaticDifficulty(4.3)]
	GroupedTwoStringKite,

	/// <summary>
	/// Indicates grouped turbot fish.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/viewtopic.php?t=833")]
	[TechniqueGroup(TechniqueGroup.SingleDigitPattern)]
	[StaticDifficultyLevel(DifficultyLevel.Hard)]
	[BoundStepTypes(typeof(TwoStrongLinksStep))]
	[StaticDifficulty(4.4)]
	GroupedTurbotFish,
	#endregion

	//
	// Empty Rectangle
	//
	#region Empty Rectangle
	/// <summary>
	/// Indicates empty rectangle.
	/// </summary>
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Empty_Rectangle.html")]
	[HodokuTechniquePrefix("0402")]
	[HodokuDifficultyRating(120, HodokuDifficultyLevel.Hard)]
	[TechniqueGroup(TechniqueGroup.EmptyRectangle)]
	[Abbreviation("ER")]
	[StaticDifficultyLevel(DifficultyLevel.Hard)]
	[BoundStepTypes(typeof(EmptyRectangleStep))]
	[StaticDifficulty(4.6)]
	EmptyRectangle,
	#endregion

	//
	// Alternating Inference Chain
	//
	#region Chaining
	/// <summary>
	/// Indicates X-Chain.
	/// </summary>
	[ReferenceLink("http://sudopedia.enjoysudoku.com/X-Chain.html")]
	[HodokuTechniquePrefix("0701")]
	[HodokuDifficultyRating(260, HodokuDifficultyLevel.Unfair)]
	[SudokuExplainerDifficultyRating(6.6, 6.9)]
	[TechniqueGroup(TechniqueGroup.AlternatingInferenceChain)]
	[StaticDifficultyLevel(DifficultyLevel.Fiendish)]
	[BoundStepTypes(typeof(ForcingChainStep))]
	[StaticDifficulty(4.6)]
	[ExtraDifficultyFactors(Length)]
	XChain,

	/// <summary>
	/// Indicates Y-Chain.
	/// </summary>
#if COMPATIBLE_ORIGINAL_TECHNIQUE_RULES
	[HodokuTechniquePrefix("0702")]
	[HodokuDifficultyRating(260, HodokuDifficultyLevel.Unfair)]
#endif
	[TechniqueGroup(TechniqueGroup.AlternatingInferenceChain)]
	[TechniqueFeature(TechniqueFeature.WillBeReplacedByOtherTechnique)]
	[StaticDifficultyLevel(DifficultyLevel.Fiendish)]
	[BoundStepTypes(typeof(ForcingChainStep))]
	[StaticDifficulty(4.6)]
	[ExtraDifficultyFactors(Length)]
	YChain,

	/// <summary>
	/// Indicates fishy cycle (X-Cycle).
	/// </summary>
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Fishy_Cycle.html")]
	[HodokuTechniquePrefix("0704")]
	[SudokuExplainerDifficultyRating(6.5, 6.6)]
	[SudokuExplainerNames("Bidirectional X-Cycle")]
	[TechniqueGroup(TechniqueGroup.AlternatingInferenceChain)]
	[TechniqueFeature(TechniqueFeature.WillBeReplacedByOtherTechnique)]
	[StaticDifficultyLevel(DifficultyLevel.Fiendish)]
	[BoundStepTypes(typeof(ForcingChainStep))]
	[StaticDifficulty(4.6)]
	[ExtraDifficultyFactors(Length)]
	FishyCycle,

	/// <summary>
	/// Indicates XY-Chain.
	/// </summary>
	[ReferenceLink("http://sudopedia.enjoysudoku.com/XY-Chain.html")]
	[HodokuTechniquePrefix("0702")]
	[HodokuDifficultyRating(260, HodokuDifficultyLevel.Unfair)]
	[TechniqueGroup(TechniqueGroup.AlternatingInferenceChain)]
	[TechniqueFeature(TechniqueFeature.NotImplemented)]
	[StaticDifficultyLevel(DifficultyLevel.Fiendish)]
	[BoundStepTypes(typeof(ForcingChainStep))]
	[StaticDifficulty(4.6)]
	[ExtraDifficultyFactors(Length)]
	XyChain,

	/// <summary>
	/// Indicates XY-Cycle.
	/// </summary>
	[TechniqueGroup(TechniqueGroup.AlternatingInferenceChain)]
	[SudokuExplainerDifficultyRating(6.6, 7.0)]
	[TechniqueFeature(TechniqueFeature.NotImplemented)]
	[StaticDifficultyLevel(DifficultyLevel.Fiendish)]
	[BoundStepTypes(typeof(ForcingChainStep))]
	[StaticDifficulty(4.6)]
	[ExtraDifficultyFactors(Length)]
	XyCycle,

	/// <summary>
	/// Indicates XY-X-Chain.
	/// </summary>
	[TechniqueGroup(TechniqueGroup.AlternatingInferenceChain)]
	[TechniqueFeature(TechniqueFeature.NotImplemented)]
	[StaticDifficultyLevel(DifficultyLevel.Fiendish)]
	[BoundStepTypes(typeof(ForcingChainStep))]
	[StaticDifficulty(4.6)]
	[ExtraDifficultyFactors(Length)]
	XyXChain,

	/// <summary>
	/// Indicates remote pair.
	/// </summary>
	[HodokuTechniquePrefix("0703")]
	[HodokuDifficultyRating(110, HodokuDifficultyLevel.Hard)]
	[TechniqueGroup(TechniqueGroup.AlternatingInferenceChain)]
	[TechniqueFeature(TechniqueFeature.NotImplemented)]
	[StaticDifficultyLevel(DifficultyLevel.Fiendish)]
	[BoundStepTypes(typeof(ForcingChainStep))]
	[StaticDifficulty(4.6)]
	[ExtraDifficultyFactors(Length)]
	RemotePair,

	/// <summary>
	/// Indicates purple cow.
	/// </summary>
	[TechniqueGroup(TechniqueGroup.AlternatingInferenceChain)]
	[TechniqueFeature(TechniqueFeature.NotImplemented)]
	[StaticDifficultyLevel(DifficultyLevel.Fiendish)]
	[BoundStepTypes(typeof(ForcingChainStep))]
	[StaticDifficulty(4.6)]
	[ExtraDifficultyFactors(Length)]
	PurpleCow,

	/// <summary>
	/// Indicates discontinuous nice loop.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/viewtopic.php?t=2859")]
	[HodokuTechniquePrefix("0707")]
	[HodokuDifficultyRating(280, HodokuDifficultyLevel.Unfair)]
	[SudokuExplainerDifficultyRating(7.0, 7.6)]
	[SudokuExplainerNames("Forcing Chain")]
	[TechniqueGroup(TechniqueGroup.AlternatingInferenceChain)]
	[Abbreviation("DNL")]
	[TechniqueFeature(TechniqueFeature.NotImplemented)]
	[StaticDifficultyLevel(DifficultyLevel.Fiendish)]
	[BoundStepTypes(typeof(ForcingChainStep))]
	[StaticDifficulty(4.6)]
	[ExtraDifficultyFactors(Length)]
	DiscontinuousNiceLoop,

	/// <summary>
	/// Indicates continuous nice loop.
	/// </summary>
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Nice_Loop.html")]
	[HodokuTechniquePrefix("0706")]
	[HodokuDifficultyRating(280, HodokuDifficultyLevel.Unfair)]
	[SudokuExplainerDifficultyRating(7.0, 7.3)]
	[SudokuExplainerNames("Bidirectional Cycle")]
	[TechniqueGroup(TechniqueGroup.AlternatingInferenceChain)]
	[Abbreviation("CNL")]
	[StaticDifficultyLevel(DifficultyLevel.Fiendish)]
	[BoundStepTypes(typeof(ForcingChainStep))]
	[StaticDifficulty(4.6)]
	[ExtraDifficultyFactors(Length)]
	ContinuousNiceLoop,

	/// <summary>
	/// Indicates alternating inference chain.
	/// </summary>
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Alternating_Inference_Chain.html")]
	[ReferenceLink("http://forum.enjoysudoku.com/viewtopic.php?t=3865")]
	[HodokuTechniquePrefix("0708")]
	[HodokuDifficultyRating(280, HodokuDifficultyLevel.Unfair)]
	[SudokuExplainerDifficultyRating(7.0, 7.6)]
	[TechniqueGroup(TechniqueGroup.AlternatingInferenceChain)]
	[Abbreviation("AIC")]
	[StaticDifficultyLevel(DifficultyLevel.Fiendish)]
	[BoundStepTypes(typeof(ForcingChainStep))]
	[StaticDifficulty(4.6)]
	[ExtraDifficultyFactors(Length)]
	AlternatingInferenceChain,

	/// <summary>
	/// Indicates grouped X-Chain.
	/// </summary>
	[TechniqueGroup(TechniqueGroup.AlternatingInferenceChain)]
	[TechniqueFeature(TechniqueFeature.NotImplemented)]
	[StaticDifficultyLevel(DifficultyLevel.Fiendish)]
	GroupedXChain,

	/// <summary>
	/// Indicates grouped fishy cycle (grouped X-Cycle).
	/// </summary>
	[TechniqueGroup(TechniqueGroup.AlternatingInferenceChain)]
	[TechniqueFeature(TechniqueFeature.NotImplemented)]
	[StaticDifficultyLevel(DifficultyLevel.Fiendish)]
	GroupedFishyCycle,

	/// <summary>
	/// Indicates grouped XY-Chain.
	/// </summary>
	[TechniqueGroup(TechniqueGroup.AlternatingInferenceChain)]
	[TechniqueFeature(TechniqueFeature.NotImplemented)]
	[StaticDifficultyLevel(DifficultyLevel.Fiendish)]
	GroupedXyChain,

	/// <summary>
	/// Indicates grouped XY-Cycle.
	/// </summary>
	[TechniqueGroup(TechniqueGroup.AlternatingInferenceChain)]
	[TechniqueFeature(TechniqueFeature.NotImplemented)]
	[StaticDifficultyLevel(DifficultyLevel.Fiendish)]
	GroupedXyCycle,

	/// <summary>
	/// Indicates grouped XY-X-Chain.
	/// </summary>
	[TechniqueGroup(TechniqueGroup.AlternatingInferenceChain)]
	[TechniqueFeature(TechniqueFeature.NotImplemented)]
	[StaticDifficultyLevel(DifficultyLevel.Fiendish)]
	GroupedXyXChain,

	/// <summary>
	/// Indicates grouped purple cow.
	/// </summary>
	[TechniqueGroup(TechniqueGroup.AlternatingInferenceChain)]
	[TechniqueFeature(TechniqueFeature.NotImplemented)]
	[StaticDifficultyLevel(DifficultyLevel.Fiendish)]
	GroupedPurpleCow,

	/// <summary>
	/// Indicates grouped discontinuous nice loop.
	/// </summary>
	[HodokuTechniquePrefix("0710")]
	[HodokuDifficultyRating(300, HodokuDifficultyLevel.Unfair)]
	[TechniqueGroup(TechniqueGroup.AlternatingInferenceChain)]
	[TechniqueFeature(TechniqueFeature.NotImplemented)]
	[StaticDifficultyLevel(DifficultyLevel.Fiendish)]
	GroupedDiscontinuousNiceLoop,

	/// <summary>
	/// Indicates grouped continuous nice loop.
	/// </summary>
	[HodokuTechniquePrefix("0709")]
	[HodokuDifficultyRating(300, HodokuDifficultyLevel.Unfair)]
	[TechniqueGroup(TechniqueGroup.AlternatingInferenceChain)]
	[TechniqueFeature(TechniqueFeature.NotImplemented)]
	[StaticDifficultyLevel(DifficultyLevel.Fiendish)]
	GroupedContinuousNiceLoop,

	/// <summary>
	/// Indicates grouped alternating inference chain.
	/// </summary>
	[HodokuTechniquePrefix("0711")]
	[HodokuDifficultyRating(300, HodokuDifficultyLevel.Unfair)]
	[TechniqueGroup(TechniqueGroup.AlternatingInferenceChain)]
	[TechniqueFeature(TechniqueFeature.NotImplemented)]
	[StaticDifficultyLevel(DifficultyLevel.Fiendish)]
	GroupedAlternatingInferenceChain,

	/// <summary>
	/// Indicates special case that a grouped alternating inference chain has a collision between start and end node.
	/// </summary>
	[TechniqueGroup(TechniqueGroup.AlternatingInferenceChain)]
	[TechniqueFeature(TechniqueFeature.NotImplemented)]
	[StaticDifficultyLevel(DifficultyLevel.Fiendish)]
	NodeCollision,
	#endregion

	//
	// Forcing Chains
	//
	#region Forcing Chains
	/// <summary>
	/// Indicates nishio forcing chains.
	/// </summary>
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Nishio.html")]
	[SudokuExplainerTechnique(SudokuExplainerTechnique.NishioForcingChain)]
	[SudokuExplainerDifficultyRating(7.6, 8.1)]
	[TechniqueGroup(TechniqueGroup.ForcingChains)]
	[StaticDifficultyLevel(DifficultyLevel.Nightmare)]
	NishioForcingChains,

	/// <summary>
	/// Indicates region forcing chains (i.e. house forcing chains).
	/// </summary>
	[HodokuTechniquePrefix("1301")]
	[HodokuDifficultyRating(500, HodokuDifficultyLevel.Extreme)]
	[SudokuExplainerTechnique(SudokuExplainerTechnique.MultipleForcingChain)]
	[SudokuExplainerDifficultyRating(8.2, 8.6)]
	[TechniqueGroup(TechniqueGroup.ForcingChains)]
	[StaticDifficultyLevel(DifficultyLevel.Nightmare)]
	[BoundStepTypes(typeof(RegionForcingChainsStep))]
	[StaticDifficulty(8.0)]
	[ExtraDifficultyFactors(Length)]
	RegionForcingChains,

	/// <summary>
	/// Indicates cell forcing chains.
	/// </summary>
	[HodokuTechniquePrefix("1301")]
	[HodokuDifficultyRating(500, HodokuDifficultyLevel.Extreme)]
	[SudokuExplainerTechnique(SudokuExplainerTechnique.MultipleForcingChain)]
	[SudokuExplainerDifficultyRating(8.2, 8.6)]
	[TechniqueGroup(TechniqueGroup.ForcingChains)]
	[StaticDifficultyLevel(DifficultyLevel.Nightmare)]
	[BoundStepTypes(typeof(CellForcingChainsStep))]
	[StaticDifficulty(8.0)]
	[ExtraDifficultyFactors(Length)]
	CellForcingChains,

	/// <summary>
	/// Indicates dynamic region forcing chains (i.e. dynamic house forcing chains).
	/// </summary>
	[HodokuTechniquePrefix("1303")]
	[HodokuDifficultyRating(500, HodokuDifficultyLevel.Extreme)]
	[SudokuExplainerDifficultyRating(8.6, 9.4)]
	[SudokuExplainerTechnique(SudokuExplainerTechnique.DynamicForcingChain)]
	[TechniqueGroup(TechniqueGroup.ForcingChains)]
	[StaticDifficultyLevel(DifficultyLevel.Nightmare)]
	[TechniqueFeature(TechniqueFeature.HardToBeGenerated)]
	[BoundStepTypes(typeof(RegionForcingChainsStep))]
	[StaticDifficulty(8.5)]
	[ExtraDifficultyFactors(Length)]
	DynamicRegionForcingChains,

	/// <summary>
	/// Indicates dynamic cell forcing chains.
	/// </summary>
	[HodokuTechniquePrefix("1303")]
	[HodokuDifficultyRating(500, HodokuDifficultyLevel.Extreme)]
	[SudokuExplainerTechnique(SudokuExplainerTechnique.DynamicForcingChain)]
	[SudokuExplainerDifficultyRating(8.6, 9.4)]
	[TechniqueGroup(TechniqueGroup.ForcingChains)]
	[StaticDifficultyLevel(DifficultyLevel.Nightmare)]
	[TechniqueFeature(TechniqueFeature.HardToBeGenerated)]
	[BoundStepTypes(typeof(CellForcingChainsStep))]
	[StaticDifficulty(8.5)]
	[ExtraDifficultyFactors(Length)]
	DynamicCellForcingChains,

	/// <summary>
	/// Indicates dynamic contradiction forcing chains.
	/// </summary>
	[HodokuTechniquePrefix("1304")]
	[HodokuDifficultyRating(500, HodokuDifficultyLevel.Extreme)]
	[SudokuExplainerTechnique(SudokuExplainerTechnique.DynamicForcingChain)]
	[SudokuExplainerDifficultyRating(8.8, 9.4)]
	[TechniqueGroup(TechniqueGroup.ForcingChains)]
	[StaticDifficultyLevel(DifficultyLevel.Nightmare)]
	[TechniqueFeature(TechniqueFeature.HardToBeGenerated)]
	[BoundStepTypes(typeof(BinaryForcingChainsStep))]
	[StaticDifficulty(9.5)]
	[ExtraDifficultyFactors(Length)]
	DynamicContradictionForcingChains,

	/// <summary>
	/// Indicates dynamic double forcing chains.
	/// </summary>
	[HodokuTechniquePrefix("1304")]
	[HodokuDifficultyRating(500, HodokuDifficultyLevel.Extreme)]
	[SudokuExplainerTechnique(SudokuExplainerTechnique.DynamicForcingChain)]
	[SudokuExplainerDifficultyRating(8.8, 9.4)]
	[TechniqueGroup(TechniqueGroup.ForcingChains)]
	[StaticDifficultyLevel(DifficultyLevel.Nightmare)]
	[TechniqueFeature(TechniqueFeature.HardToBeGenerated)]
	[BoundStepTypes(typeof(BinaryForcingChainsStep))]
	[StaticDifficulty(9.5)]
	[ExtraDifficultyFactors(Length)]
	DynamicDoubleForcingChains,

	/// <summary>
	/// Indicates dynamic forcing chains.
	/// </summary>
	[TechniqueGroup(TechniqueGroup.ForcingChains)]
	[StaticDifficultyLevel(DifficultyLevel.Nightmare)]
	DynamicForcingChains,
	#endregion

	//
	// Blossom Loop
	//
	#region Blossom Loop
	/// <summary>
	/// Indicates blossom loop.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/blossom-loop-t42270.html")]
	[TechniqueGroup(TechniqueGroup.BlossomLoop)]
	[StaticDifficultyLevel(DifficultyLevel.Nightmare)]
	[TechniqueFeature(TechniqueFeature.HardToBeGenerated)]
	[BoundStepTypes(typeof(BlossomLoopStep))]
	[StaticDifficulty(8.0)]
	[ExtraDifficultyFactors(Length)]
	BlossomLoop,
	#endregion

	//
	// Aligned Exclusion
	//
	#region Aligned Exclusion
	/// <summary>
	/// Indicates aligned pair exclusion.
	/// </summary>
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Subset_Exclusion.html")]
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Aligned_Pair_Exclusion.html")]
	[ReferenceLink("http://forum.enjoysudoku.com/viewtopic.php?t=3882")]
	[TechniqueGroup(TechniqueGroup.AlignedExclusion)]
	[Abbreviation("APE")]
	[SudokuExplainerTechnique(SudokuExplainerTechnique.AlignedPairExclusion)]
	[SudokuExplainerDifficultyRating(6.2)]
	[TechniqueFeature(TechniqueFeature.WillBeReplacedByOtherTechnique)]
	[StaticDifficultyLevel(DifficultyLevel.Fiendish)]
	[BoundStepTypes(typeof(AlignedExclusionStep))]
	[StaticDifficulty(6.2)]
	AlignedPairExclusion,

	/// <summary>
	/// Indicates aligned triple exclusion.
	/// </summary>
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Subset_Exclusion.html")]
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Aligned_Triple_Exclusion.html")]
	[TechniqueGroup(TechniqueGroup.AlignedExclusion)]
	[Abbreviation("ATE")]
	[SudokuExplainerTechnique(SudokuExplainerTechnique.AlignedTripletExclusion)]
	[SudokuExplainerDifficultyRating(7.5)]
	[TechniqueFeature(TechniqueFeature.WillBeReplacedByOtherTechnique)]
	[StaticDifficultyLevel(DifficultyLevel.Fiendish)]
	[BoundStepTypes(typeof(AlignedExclusionStep))]
	[StaticDifficulty(7.5)]
	AlignedTripleExclusion,

	/// <summary>
	/// Indicates aligned quadruple exclusion.
	/// </summary>
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Subset_Exclusion.html")]
	[TechniqueGroup(TechniqueGroup.AlignedExclusion)]
	[TechniqueFeature(TechniqueFeature.WillBeReplacedByOtherTechnique)]
	[StaticDifficultyLevel(DifficultyLevel.Fiendish)]
	[BoundStepTypes(typeof(AlignedExclusionStep))]
	[StaticDifficulty(8.1)]
	AlignedQuadrupleExclusion,

	/// <summary>
	/// Indicates aligned quintuple exclusion.
	/// </summary>
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Subset_Exclusion.html")]
	[TechniqueGroup(TechniqueGroup.AlignedExclusion)]
	[TechniqueFeature(TechniqueFeature.WillBeReplacedByOtherTechnique)]
	[StaticDifficultyLevel(DifficultyLevel.Fiendish)]
	[BoundStepTypes(typeof(AlignedExclusionStep))]
	[StaticDifficulty(8.4)]
	AlignedQuintupleExclusion,
	#endregion

	//
	// XYZ-Ring
	//
	#region XYZ-Ring
	/// <summary>
	/// Indicates XYZ loop.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/xyz-ring-t42209.html")]
	[TechniqueGroup(TechniqueGroup.XyzRing)]
	[StaticDifficultyLevel(DifficultyLevel.Fiendish)]
	[BoundStepTypes(typeof(XyzRingStep))]
	[StaticDifficulty(5.0)]
	XyzLoop,

	/// <summary>
	/// Indicates Siamese XYZ loop.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/xyz-ring-t42209.html")]
	[TechniqueGroup(TechniqueGroup.XyzRing)]
	[StaticDifficultyLevel(DifficultyLevel.Fiendish)]
	[BoundStepTypes(typeof(XyzRingStep))]
	[StaticDifficulty(5.0)]
	SiameseXyzLoop,

	/// <summary>
	/// Indicates XYZ nice loop.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/xyz-ring-t42209.html")]
	[TechniqueGroup(TechniqueGroup.XyzRing)]
	[StaticDifficultyLevel(DifficultyLevel.Fiendish)]
	[BoundStepTypes(typeof(XyzRingStep))]
	[StaticDifficulty(5.2)]
	XyzNiceLoop,

	/// <summary>
	/// Indicates Siamese XYZ nice loop.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/xyz-ring-t42209.html")]
	[TechniqueGroup(TechniqueGroup.XyzRing)]
	[StaticDifficultyLevel(DifficultyLevel.Fiendish)]
	[BoundStepTypes(typeof(XyzRingStep))]
	[StaticDifficulty(5.2)]
	SiameseXyzNiceLoop,

	/// <summary>
	/// Indicates grouped XYZ loop.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/xyz-ring-t42209.html")]
	[TechniqueGroup(TechniqueGroup.XyzRing)]
	[StaticDifficultyLevel(DifficultyLevel.Fiendish)]
	[BoundStepTypes(typeof(XyzRingStep))]
	[StaticDifficulty(5.0)]
	GroupedXyzLoop,

	/// <summary>
	/// Indicates Siamese grouped XYZ loop.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/xyz-ring-t42209.html")]
	[TechniqueGroup(TechniqueGroup.XyzRing)]
	[StaticDifficultyLevel(DifficultyLevel.Fiendish)]
	[BoundStepTypes(typeof(XyzRingStep))]
	[StaticDifficulty(5.0)]
	SiameseGroupedXyzLoop,

	/// <summary>
	/// Indicates grouped XYZ nice loop.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/xyz-ring-t42209.html")]
	[TechniqueGroup(TechniqueGroup.XyzRing)]
	[StaticDifficultyLevel(DifficultyLevel.Fiendish)]
	[BoundStepTypes(typeof(XyzRingStep))]
	[StaticDifficulty(5.2)]
	GroupedXyzNiceLoop,

	/// <summary>
	/// Indicates Siamese grouped XYZ nice loop.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/xyz-ring-t42209.html")]
	[TechniqueGroup(TechniqueGroup.XyzRing)]
	[StaticDifficultyLevel(DifficultyLevel.Fiendish)]
	[BoundStepTypes(typeof(XyzRingStep))]
	[StaticDifficulty(5.2)]
	SiameseGroupedXyzNiceLoop,
	#endregion

	//
	// Almost Locked Sets
	//
	#region Almost Locked Sets
	/// <summary>
	/// Indicates singly linked ALS-XZ.
	/// </summary>
	[ReferenceLink("http://sudopedia.enjoysudoku.com/ALS-XZ.html")]
	[HodokuTechniquePrefix("0901")]
	[HodokuDifficultyRating(300, HodokuDifficultyLevel.Unfair)]
	[SudokuExplainerTechnique(SudokuExplainerTechnique.AlsXz)]
	[SudokuExplainerDifficultyRating(7.5, IsAdvancedDefined = true)]
	[TechniqueGroup(TechniqueGroup.AlmostLockedSetsChainingLike)]
	[Abbreviation("ALS-XZ")]
	[StaticDifficultyLevel(DifficultyLevel.Fiendish)]
	[BoundStepTypes(typeof(AlmostLockedSetsXzStep))]
	[StaticDifficulty(5.5)]
	SinglyLinkedAlmostLockedSetsXzRule,

	/// <summary>
	/// Indicates doubly linked ALS-XZ.
	/// </summary>
	[ReferenceLink("http://sudopedia.enjoysudoku.com/ALS-XZ.html")]
	[HodokuTechniquePrefix("0901")]
	[HodokuDifficultyRating(300, HodokuDifficultyLevel.Unfair)]
	[SudokuExplainerDifficultyRating(7.5, IsAdvancedDefined = true)]
	[TechniqueGroup(TechniqueGroup.AlmostLockedSetsChainingLike)]
	[Abbreviation("ALS-XZ")]
	[StaticDifficultyLevel(DifficultyLevel.Fiendish)]
	[BoundStepTypes(typeof(AlmostLockedSetsXzStep))]
	[StaticDifficulty(5.7)]
	DoublyLinkedAlmostLockedSetsXzRule,

	/// <summary>
	/// Indicates ALS-XY-Wing.
	/// </summary>
	[ReferenceLink("http://sudopedia.enjoysudoku.com/ALS-XY-Wing.html")]
	[HodokuTechniquePrefix("0902")]
	[HodokuDifficultyRating(320, HodokuDifficultyLevel.Unfair)]
	[SudokuExplainerTechnique(SudokuExplainerTechnique.AlsXyWing)]
	[SudokuExplainerDifficultyRating(8.0, IsAdvancedDefined = true)]
	[TechniqueGroup(TechniqueGroup.AlmostLockedSetsChainingLike)]
	[Abbreviation("ALS-XY-Wing")]
	[StaticDifficultyLevel(DifficultyLevel.Fiendish)]
	[BoundStepTypes(typeof(AlmostLockedSetsXyWingStep))]
	[StaticDifficulty(6.0)]
	AlmostLockedSetsXyWing,

	/// <summary>
	/// Indicates ALS-W-Wing.
	/// </summary>
	[TechniqueGroup(TechniqueGroup.AlmostLockedSetsChainingLike)]
	[Abbreviation("ALS-W-Wing")]
	[StaticDifficultyLevel(DifficultyLevel.Fiendish)]
	[BoundStepTypes(typeof(AlmostLockedSetsWWingStep))]
	[StaticDifficulty(6.2)]
	AlmostLockedSetsWWing,

	/// <summary>
	/// Indicates ALS chain.
	/// </summary>
	[ReferenceLink("http://sudopedia.enjoysudoku.com/ALS-XY-Chain.html")]
	[HodokuTechniquePrefix("0903")]
	[HodokuDifficultyRating(340, HodokuDifficultyLevel.Unfair)]
	[TechniqueGroup(TechniqueGroup.AlmostLockedSetsChainingLike)]
	[TechniqueFeature(TechniqueFeature.NotImplemented)]
	[StaticDifficultyLevel(DifficultyLevel.Fiendish)]
	AlmostLockedSetsChain,
	#endregion

	//
	// Empty Rectangle Intersection Pair
	//
	#region Empty Rectangle Intersection Pair
	/// <summary>
	/// Indicates empty rectangle intersection pair.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/post288015.html")]
	[TechniqueGroup(TechniqueGroup.EmptyRectangleIntersectionPair)]
	[Abbreviation("ERIP")]
	[StaticDifficultyLevel(DifficultyLevel.Fiendish)]
	[BoundStepTypes(typeof(EmptyRectangleIntersectionPairStep))]
	[StaticDifficulty(6.0)]
	EmptyRectangleIntersectionPair,
	#endregion

	//
	// Death Blossom
	//
	#region Death Blossom
	/// <summary>
	/// Indicates death blossom.
	/// </summary>
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Death_Blossom.html")]
	[HodokuTechniquePrefix("0904")]
	[HodokuDifficultyRating(360, HodokuDifficultyLevel.Unfair)]
	[TechniqueGroup(TechniqueGroup.DeathBlossom)]
	[StaticDifficultyLevel(DifficultyLevel.Fiendish)]
	[Abbreviation("DB")]
	[BoundStepTypes(typeof(DeathBlossomStep))]
	[StaticDifficulty(8.2)]
	[ExtraDifficultyFactors(Petals)]
	DeathBlossom,

	/// <summary>
	/// Indicates death blossom (house blooming).
	/// </summary>
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Death_Blossom.html")]
	[TechniqueGroup(TechniqueGroup.DeathBlossom)]
	[StaticDifficultyLevel(DifficultyLevel.Fiendish)]
	[BoundStepTypes(typeof(HouseDeathBlossomStep))]
	[StaticDifficulty(8.3)]
	[ExtraDifficultyFactors(Petals)]
	HouseDeathBlossom,

	/// <summary>
	/// Indicates death blossom (rectangle blooming).
	/// </summary>
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Death_Blossom.html")]
	[TechniqueGroup(TechniqueGroup.DeathBlossom)]
	[StaticDifficultyLevel(DifficultyLevel.Nightmare)]
	[BoundStepTypes(typeof(RectangleDeathBlossomStep))]
	[StaticDifficulty(8.5)]
	[ExtraDifficultyFactors(Petals)]
	RectangleDeathBlossom,

	/// <summary>
	/// Indicates death blossom (A^nLS blooming).
	/// </summary>
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Death_Blossom.html")]
	[TechniqueGroup(TechniqueGroup.DeathBlossom)]
	[StaticDifficultyLevel(DifficultyLevel.Nightmare)]
	[BoundStepTypes(typeof(NTimesAlmostLockedSetDeathBlossomStep))]
	[StaticDifficulty(8.7)]
	[ExtraDifficultyFactors(Petals)]
	NTimesAlmostLockedSetDeathBlossom,
	#endregion

	//
	// Symmetry
	//
	#region Symmetry
	/// <summary>
	/// Indicates Gurth's symmetrical placement.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/viewtopic.php?p=32842#p32842")]
	[TechniqueGroup(TechniqueGroup.Symmetry)]
	[Abbreviation("GSP")]
	[TechniqueFeature(TechniqueFeature.HardToBeGenerated)]
	[StaticDifficultyLevel(DifficultyLevel.Fiendish)]
	[BoundStepTypes(typeof(GurthSymmetricalPlacementStep))]
	[StaticDifficulty(7.0)]
	GurthSymmetricalPlacement,

	/// <summary>
	/// Indicates extended Gurth's symmetrical placement.
	/// </summary>
	[TechniqueGroup(TechniqueGroup.Symmetry)]
	[TechniqueFeature(TechniqueFeature.NotImplemented | TechniqueFeature.HardToBeGenerated)]
	[StaticDifficultyLevel(DifficultyLevel.Nightmare)]
	ExtendedGurthSymmetricalPlacement,

	/// <summary>
	/// Indicates Anti-GSP (Anti- Gurth's Symmetrical Placement).
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/new-type-of-gsp-t40470.html")]
	[TechniqueGroup(TechniqueGroup.Symmetry)]
	[TechniqueFeature(TechniqueFeature.HardToBeGenerated)]
	[StaticDifficultyLevel(DifficultyLevel.Fiendish)]
	[BoundStepTypes(typeof(AntiGurthSymmetricalPlacementStep), SecondaryTypes = [typeof(GurthSymmetricalPlacementStep)])]
	[StaticDifficulty(7.3)]
	AntiGurthSymmetricalPlacement,
	#endregion

	//
	// Exocet
	//
	#region Exocet
	/// <summary>
	/// Indicates junior exocet.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/jexocet-compendium-t32370.html")]
	[TechniqueGroup(TechniqueGroup.Exocet)]
	[Abbreviation("JE")]
	[StaticDifficultyLevel(DifficultyLevel.Nightmare)]
	[BoundStepTypes(typeof(ExocetBaseStep))]
	[StaticDifficulty(9.4)]
	JuniorExocet,

	/// <summary>
	/// Indicates junior exocet with target conjugate pair.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/jexocet-compendium-t32370.html")]
	[TechniqueGroup(TechniqueGroup.Exocet)]
	[StaticDifficultyLevel(DifficultyLevel.Nightmare)]
	[BoundStepTypes(typeof(ExocetBaseStep))]
	[StaticDifficulty(9.4)]
	JuniorExocetConjugatePair,

	/// <summary>
	/// Indicates junior exocet mirror mirror conjugate pair.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/jexocet-compendium-t32370.html")]
	[TechniqueGroup(TechniqueGroup.Exocet)]
	[StaticDifficultyLevel(DifficultyLevel.Nightmare)]
	[BoundStepTypes(typeof(ExocetMirrorConjugatePairStep))]
	[StaticDifficulty(9.4)]
	[ExtraDifficultyFactors(Mirror)]
	JuniorExocetMirrorConjugatePair,

	/// <summary>
	/// Indicates junior exocet adjacent target.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/jexocet-compendium-t32370.html")]
	[TechniqueGroup(TechniqueGroup.Exocet)]
	[StaticDifficultyLevel(DifficultyLevel.Nightmare)]
	[BoundStepTypes(typeof(JuniorExocetAdjacentTargetStep))]
	[StaticDifficulty(9.4)]
	[ExtraDifficultyFactors(Mirror)]
	JuniorExocetAdjacentTarget,

	/// <summary>
	/// Indicates junior exocet incompatible pair.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/jexocet-compendium-t32370.html")]
	[TechniqueGroup(TechniqueGroup.Exocet)]
	[StaticDifficultyLevel(DifficultyLevel.Nightmare)]
	[BoundStepTypes(typeof(JuniorExocetIncompatiblePairStep))]
	[StaticDifficulty(9.4)]
	[ExtraDifficultyFactors(IncompatiblePair)]
	JuniorExocetIncompatiblePair,

	/// <summary>
	/// Indicates junior exocet target pair.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/jexocet-compendium-t32370.html")]
	[TechniqueGroup(TechniqueGroup.Exocet)]
	[StaticDifficultyLevel(DifficultyLevel.Nightmare)]
	[BoundStepTypes(typeof(JuniorExocetTargetPairStep))]
	[StaticDifficulty(9.4)]
	[ExtraDifficultyFactors(TargetPair)]
	JuniorExocetTargetPair,

	/// <summary>
	/// Indicates junior exocet generalized fish.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/jexocet-compendium-t32370.html")]
	[TechniqueGroup(TechniqueGroup.Exocet)]
	[StaticDifficultyLevel(DifficultyLevel.Nightmare)]
	[BoundStepTypes(typeof(JuniorExocetGeneralizedFishStep))]
	[StaticDifficulty(9.4)]
	[ExtraDifficultyFactors(GeneralizedFish)]
	JuniorExocetGeneralizedFish,

	/// <summary>
	/// Indicates junior exocet mirror almost hidden set.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/jexocet-compendium-t32370.html")]
	[TechniqueGroup(TechniqueGroup.Exocet)]
	[StaticDifficultyLevel(DifficultyLevel.Nightmare)]
	[BoundStepTypes(typeof(JuniorExocetMirrorAlmostHiddenSetStep))]
	[StaticDifficulty(9.4)]
	[ExtraDifficultyFactors(AlmostHiddenSet)]
	JuniorExocetMirrorAlmostHiddenSet,

	/// <summary>
	/// Indicates junior exocet locked member.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/jexocet-compendium-t32370.html")]
	[TechniqueGroup(TechniqueGroup.Exocet)]
	[StaticDifficultyLevel(DifficultyLevel.Nightmare)]
	[BoundStepTypes(typeof(ExocetLockedMemberStep))]
	[StaticDifficulty(9.4)]
	[ExtraDifficultyFactors(LockedMember)]
	JuniorExocetLockedMember,

	/// <summary>
	/// Indicates junior exocet mirror sync.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/jexocet-compendium-t32370.html")]
	[TechniqueGroup(TechniqueGroup.Exocet)]
	[StaticDifficultyLevel(DifficultyLevel.Nightmare)]
	[BoundStepTypes(typeof(JuniorExocetMirrorSyncStep))]
	[StaticDifficulty(9.4)]
	[ExtraDifficultyFactors(Mirror)]
	JuniorExocetMirrorSync,

	/// <summary>
	/// Indicates senior exocet.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/jexocet-compendium-t32370.html")]
	[TechniqueGroup(TechniqueGroup.Exocet)]
	[StaticDifficultyLevel(DifficultyLevel.Nightmare)]
	[Abbreviation("SE")]
	[BoundStepTypes(typeof(ExocetBaseStep))]
	[StaticDifficulty(9.6)]
	SeniorExocet,

	/// <summary>
	/// Indicates senior exocet mirror conjugate pair.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/jexocet-compendium-t32370.html")]
	[TechniqueGroup(TechniqueGroup.Exocet)]
	[StaticDifficultyLevel(DifficultyLevel.Nightmare)]
	[BoundStepTypes(typeof(ExocetMirrorConjugatePairStep))]
	[StaticDifficulty(9.4)]
	[ExtraDifficultyFactors(Mirror)]
	SeniorExocetMirrorConjugatePair,

	/// <summary>
	/// Indicates senior exocet locked member.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/jexocet-compendium-t32370.html")]
	[TechniqueGroup(TechniqueGroup.Exocet)]
	[StaticDifficultyLevel(DifficultyLevel.Nightmare)]
	[BoundStepTypes(typeof(ExocetLockedMemberStep))]
	[StaticDifficulty(9.4)]
	[ExtraDifficultyFactors(LockedMember)]
	SeniorExocetLockedMember,

	/// <summary>
	/// Indicates senior exocet true base.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/jexocet-compendium-t32370.html")]
	[TechniqueGroup(TechniqueGroup.Exocet)]
	[StaticDifficultyLevel(DifficultyLevel.Nightmare)]
	[BoundStepTypes(typeof(SeniorExocetTrueBaseStep))]
	[StaticDifficulty(9.4)]
	[ExtraDifficultyFactors(TrueBase)]
	SeniorExocetTrueBase,

	/// <summary>
	/// Indicates weak exocet.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/jexocet-compendium-t32370.html")]
	[ReferenceLink("http://forum.enjoysudoku.com/weak-exocet-t39651.html")]
	[TechniqueGroup(TechniqueGroup.Exocet)]
	[StaticDifficultyLevel(DifficultyLevel.Nightmare)]
	[Abbreviation("WE")]
	[BoundStepTypes(typeof(WeakExocetStep))]
	[StaticDifficulty(9.7)]
	[ExtraDifficultyFactors(MissingStabilityBalancer)]
	WeakExocet,

	/// <summary>
	/// Indicates weak exocet adjacent target.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/jexocet-compendium-t32370.html")]
	[ReferenceLink("http://forum.enjoysudoku.com/weak-exocet-t39651.html")]
	[TechniqueGroup(TechniqueGroup.Exocet)]
	[StaticDifficultyLevel(DifficultyLevel.Nightmare)]
	[BoundStepTypes(typeof(WeakExocetAdjacentTargetStep))]
	[StaticDifficulty(9.7)]
	[ExtraDifficultyFactors(Mirror)]
	WeakExocetAdjacentTarget,

	/// <summary>
	/// Indicates weak exocet slash.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/jexocet-compendium-t32370.html")]
	[ReferenceLink("http://forum.enjoysudoku.com/weak-exocet-t39651.html")]
	[TechniqueGroup(TechniqueGroup.Exocet)]
	[StaticDifficultyLevel(DifficultyLevel.Nightmare)]
	[BoundStepTypes(typeof(WeakExocetSlashStep))]
	[StaticDifficulty(9.7)]
	[ExtraDifficultyFactors(SlashElimination)]
	WeakExocetSlash,

	/// <summary>
	/// Indicates weak exocet BZ rectangle.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/jexocet-compendium-t32370.html")]
	[ReferenceLink("http://forum.enjoysudoku.com/weak-exocet-t39651.html")]
	[TechniqueGroup(TechniqueGroup.Exocet)]
	[StaticDifficultyLevel(DifficultyLevel.Nightmare)]
	[BoundStepTypes(typeof(WeakExocetBzRectangleStep))]
	[StaticDifficulty(9.7)]
	[ExtraDifficultyFactors(BzRectangle)]
	WeakExocetBzRectangle,

	/// <summary>
	/// Indicates lame weak exocet.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/jexocet-compendium-t32370.html")]
	[ReferenceLink("http://forum.enjoysudoku.com/weak-exocet-t39651.html")]
	[TechniqueGroup(TechniqueGroup.Exocet)]
	[StaticDifficultyLevel(DifficultyLevel.Nightmare)]
	[BoundStepTypes(typeof(WeakExocetStep))]
	[StaticDifficulty(9.7)]
	[ExtraDifficultyFactors(MissingStabilityBalancer)]
	LameWeakExocet,

	/// <summary>
	/// Indicates franken junior exocet.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/jexocet-compendium-t32370.html")]
	[TechniqueGroup(TechniqueGroup.Exocet)]
	[StaticDifficultyLevel(DifficultyLevel.Nightmare)]
	[BoundStepTypes(typeof(ComplexExocetBaseStep))]
	[StaticDifficulty(9.8)]
	FrankenJuniorExocet,

	/// <summary>
	/// Indicates franken junior exocet locked member.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/jexocet-compendium-t32370.html")]
	[TechniqueGroup(TechniqueGroup.Exocet)]
	[StaticDifficultyLevel(DifficultyLevel.Nightmare)]
	[BoundStepTypes(typeof(ComplexExocetLockedMemberStep))]
	[StaticDifficulty(9.8)]
	[ExtraDifficultyFactors(LockedMember)]
	FrankenJuniorExocetLockedMember,

	/// <summary>
	/// Indicates franken junior exocet adjacent target.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/jexocet-compendium-t32370.html")]
	[TechniqueGroup(TechniqueGroup.Exocet)]
	[StaticDifficultyLevel(DifficultyLevel.Nightmare)]
	[BoundStepTypes(typeof(ComplexJuniorExocetAdjacentTargetStep))]
	[StaticDifficulty(9.8)]
	[ExtraDifficultyFactors(Mirror)]
	FrankenJuniorExocetAdjacentTarget,

	/// <summary>
	/// Indicates franken junior exocet mirror conjugate pair.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/jexocet-compendium-t32370.html")]
	[TechniqueGroup(TechniqueGroup.Exocet)]
	[StaticDifficultyLevel(DifficultyLevel.Nightmare)]
	[BoundStepTypes(typeof(ComplexJuniorExocetMirrorConjugatePairStep))]
	[StaticDifficulty(9.8)]
	[ExtraDifficultyFactors(Mirror)]
	FrankenJuniorExocetMirrorConjugatePair,

	/// <summary>
	/// Indicates mutant junior exocet.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/jexocet-compendium-t32370.html")]
	[TechniqueGroup(TechniqueGroup.Exocet)]
	[StaticDifficultyLevel(DifficultyLevel.Nightmare)]
	[BoundStepTypes(typeof(ComplexExocetBaseStep))]
	[StaticDifficulty(10.0)]
	MutantJuniorExocet,

	/// <summary>
	/// Indicates mutant junior exocet locked member.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/jexocet-compendium-t32370.html")]
	[TechniqueGroup(TechniqueGroup.Exocet)]
	[StaticDifficultyLevel(DifficultyLevel.Nightmare)]
	[BoundStepTypes(typeof(ComplexExocetLockedMemberStep))]
	[StaticDifficulty(10.0)]
	[ExtraDifficultyFactors(LockedMember)]
	MutantJuniorExocetLockedMember,

	/// <summary>
	/// Indicates mutant junior exocet adjacent target.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/jexocet-compendium-t32370.html")]
	[TechniqueGroup(TechniqueGroup.Exocet)]
	[StaticDifficultyLevel(DifficultyLevel.Nightmare)]
	[BoundStepTypes(typeof(ComplexJuniorExocetAdjacentTargetStep))]
	[StaticDifficulty(10.0)]
	[ExtraDifficultyFactors(Mirror)]
	MutantJuniorExocetAdjacentTarget,

	/// <summary>
	/// Indicates mutant junior exocet mirror conjugate pair.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/jexocet-compendium-t32370.html")]
	[TechniqueGroup(TechniqueGroup.Exocet)]
	[StaticDifficultyLevel(DifficultyLevel.Nightmare)]
	[BoundStepTypes(typeof(ComplexJuniorExocetMirrorConjugatePairStep))]
	[StaticDifficulty(10.0)]
	[ExtraDifficultyFactors(Mirror)]
	MutantJuniorExocetMirrorConjugatePair,

	/// <summary>
	/// Indicates franken senior exocet.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/jexocet-compendium-t32370.html")]
	[TechniqueGroup(TechniqueGroup.Exocet)]
	[StaticDifficultyLevel(DifficultyLevel.Nightmare)]
	[BoundStepTypes(typeof(ComplexExocetBaseStep))]
	[StaticDifficulty(10.0)]
	FrankenSeniorExocet,

	/// <summary>
	/// Indicates franken senior exocet locked member.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/jexocet-compendium-t32370.html")]
	[TechniqueGroup(TechniqueGroup.Exocet)]
	[StaticDifficultyLevel(DifficultyLevel.Nightmare)]
	[BoundStepTypes(typeof(ComplexExocetLockedMemberStep))]
	[StaticDifficulty(10.0)]
	[ExtraDifficultyFactors(LockedMember)]
	FrankenSeniorExocetLockedMember,

	/// <summary>
	/// Indicates advanced franken senior exocet.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/jexocet-compendium-t32370.html")]
	[TechniqueGroup(TechniqueGroup.Exocet)]
	[StaticDifficultyLevel(DifficultyLevel.Nightmare)]
	[BoundStepTypes(typeof(AdvancedComplexSeniorExocetStep))]
	[StaticDifficulty(9.8)]
	AdvancedFrankenSeniorExocet,

	/// <summary>
	/// Indicates mutant senior exocet.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/jexocet-compendium-t32370.html")]
	[TechniqueGroup(TechniqueGroup.Exocet)]
	[StaticDifficultyLevel(DifficultyLevel.Nightmare)]
	[BoundStepTypes(typeof(ComplexExocetBaseStep))]
	[StaticDifficulty(10.2)]
	MutantSeniorExocet,

	/// <summary>
	/// Indicates mutant senior exocet locked member.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/jexocet-compendium-t32370.html")]
	[TechniqueGroup(TechniqueGroup.Exocet)]
	[StaticDifficultyLevel(DifficultyLevel.Nightmare)]
	[BoundStepTypes(typeof(ComplexExocetLockedMemberStep))]
	[StaticDifficulty(10.2)]
	[ExtraDifficultyFactors(LockedMember)]
	MutantSeniorExocetLockedMember,

	/// <summary>
	/// Indicates advanced mutant senior exocet.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/jexocet-compendium-t32370.html")]
	[TechniqueGroup(TechniqueGroup.Exocet)]
	[StaticDifficultyLevel(DifficultyLevel.Nightmare)]
	[BoundStepTypes(typeof(AdvancedComplexSeniorExocetStep))]
	[StaticDifficulty(10.1)]
	AdvancedMutantSeniorExocet,

	/// <summary>
	/// Indicates double exocet.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/jexocet-compendium-t32370.html")]
	[TechniqueGroup(TechniqueGroup.Exocet)]
	[StaticDifficultyLevel(DifficultyLevel.Nightmare)]
	[BoundStepTypes(typeof(DoubleExocetBaseStep))]
	[StaticDifficulty(9.4)]
	DoubleExocet,

	/// <summary>
	/// Indicates double exocet uni-fish pattern.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/jexocet-compendium-t32370.html")]
	[TechniqueGroup(TechniqueGroup.Exocet)]
	[StaticDifficultyLevel(DifficultyLevel.Nightmare)]
	[BoundStepTypes(typeof(DoubleExocetGeneralizedFishStep))]
	[StaticDifficulty(9.4)]
	[ExtraDifficultyFactors(GeneralizedFish)]
	DoubleExocetGeneralizedFish,

	/// <summary>
	/// Indicates pattern-locked quadruple. This quadruple is a special quadruple: it can only be concluded after both JE and SK-Loop are formed.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/jexocet-compendium-t32370.html")]
	[TechniqueGroup(TechniqueGroup.Exocet)]
	[StaticDifficultyLevel(DifficultyLevel.Nightmare)]
	[Abbreviation("PLQ")]
	PatternLockedQuadruple,
	#endregion

	//
	// Domino Loop
	//
	#region Domino Loop
	/// <summary>
	/// Indicates domino loop.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/domino-loops-sk-loops-beyond-t32789.html")]
	[TechniqueGroup(TechniqueGroup.DominoLoop)]
	[TechniqueFeature(TechniqueFeature.HardToBeGenerated)]
	[StaticDifficultyLevel(DifficultyLevel.Nightmare)]
	[BoundStepTypes(typeof(DominoLoopStep))]
	[StaticDifficulty(9.6)]
	DominoLoop,
	#endregion

	//
	// Multi-sector Locked Sets
	//
	#region Multi-sector Locked Sets
	/// <summary>
	/// Indicates multi-sector locked sets.
	/// </summary>
	[ReferenceLink("http://forum.enjoysudoku.com/exotic-patterns-a-resume-t30508-270.html")]
	[TechniqueGroup(TechniqueGroup.MultisectorLockedSets)]
	[Abbreviation("MSLS")]
	[TechniqueFeature(TechniqueFeature.HardToBeGenerated)]
	[StaticDifficultyLevel(DifficultyLevel.Nightmare)]
	[BoundStepTypes(typeof(MultisectorLockedSetsStep))]
	[StaticDifficulty(9.4)]
	MultisectorLockedSets,
	#endregion

	//
	// Pattern Overlay
	//
	#region Pattern Overlay
	/// <summary>
	/// Indicates pattern overlay method.
	/// </summary>
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Pattern_Overlay_Method.html")]
	[TechniqueGroup(TechniqueGroup.PatternOverlay)]
	[Abbreviation("POM")]
	[TechniqueFeature(TechniqueFeature.WillBeReplacedByOtherTechnique)]
	[StaticDifficultyLevel(DifficultyLevel.LastResort)]
	[BoundStepTypes(typeof(PatternOverlayStep))]
	[StaticDifficulty(8.5)]
	PatternOverlay,
	#endregion

	//
	// Templating
	//
	#region Templating
	/// <summary>
	/// Indicates template set.
	/// </summary>
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Templating.html")]
	[HodokuTechniquePrefix("1201")]
	[HodokuDifficultyRating(10000, HodokuDifficultyLevel.Extreme)]
	[TechniqueGroup(TechniqueGroup.Templating)]
	[TechniqueFeature(TechniqueFeature.WillBeReplacedByOtherTechnique)]
	[StaticDifficultyLevel(DifficultyLevel.LastResort)]
	[BoundStepTypes(typeof(TemplateStep))]
	[StaticDifficulty(9.0)]
	TemplateSet,

	/// <summary>
	/// Indicates template delete.
	/// </summary>
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Templating.html")]
	[HodokuTechniquePrefix("1202")]
	[HodokuDifficultyRating(10000, HodokuDifficultyLevel.Extreme)]
	[TechniqueGroup(TechniqueGroup.Templating)]
	[TechniqueFeature(TechniqueFeature.WillBeReplacedByOtherTechnique)]
	[StaticDifficultyLevel(DifficultyLevel.LastResort)]
	[BoundStepTypes(typeof(TemplateStep))]
	[StaticDifficulty(9.0)]
	TemplateDelete,
	#endregion

	//
	// Bowman's Bingo
	//
	#region Bowman's Bingo
	/// <summary>
	/// Indicates bowman's bingo.
	/// </summary>
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Bowman_Bingo.html")]
	[TechniqueGroup(TechniqueGroup.BowmanBingo)]
	[TechniqueFeature(TechniqueFeature.WillBeReplacedByOtherTechnique)]
	[StaticDifficultyLevel(DifficultyLevel.LastResort)]
	[BoundStepTypes(typeof(BowmanBingoStep))]
	[StaticDifficulty(8.0)]
	[ExtraDifficultyFactors(Length)]
	BowmanBingo,
	#endregion

	//
	// Brute Force
	//
	#region Brute Force
	/// <summary>
	/// Indicates brute force.
	/// </summary>
	[ReferenceLink("http://sudopedia.enjoysudoku.com/Trial_%26_Error.html")]
	[HodokuDifficultyRating(10000, HodokuDifficultyLevel.Extreme)]
	[SudokuExplainerNames("Try & Error")]
	[TechniqueGroup(TechniqueGroup.BruteForce)]
	[Abbreviation("BF")]
	[TechniqueFeature(TechniqueFeature.OnlyExistInTheory)]
	[StaticDifficultyLevel(DifficultyLevel.LastResort)]
	[BoundStepTypes(typeof(BruteForceStep))]
	[StaticDifficulty((double)AnalyzerResult.MaximumRatingValueTheory)]
	BruteForce,
	#endregion
}
