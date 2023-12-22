using System.SourceGeneration;
using Sudoku.Analytics.Categorization;
using Sudoku.Analytics.Configuration;
using Sudoku.Concepts;
using Sudoku.Rendering;

namespace Sudoku.Analytics.Steps;

/// <summary>
/// Provides with a step that is a <b>Domino Loop</b> technique.
/// </summary>
/// <param name="conclusions"><inheritdoc/></param>
/// <param name="views"><inheritdoc/></param>
/// <param name="options"><inheritdoc/></param>
/// <param name="cells">Indicates the cells used.</param>
public sealed partial class DominoLoopStep(
	Conclusion[] conclusions,
	View[]? views,
	StepSearcherOptions options,
	[Data] scoped ref readonly CellMap cells
) : LockedSetStep(conclusions, views, options)
{
	/// <inheritdoc/>
	public override decimal BaseDifficulty => 9.6M;

	/// <inheritdoc/>
	public override Technique Code => Technique.DominoLoop;

	/// <inheritdoc/>
	public override FormatInterpolation[] FormatInterpolationParts
		=> [new(EnglishLanguage, [CellsCountStr, CellsStr]), new(ChineseLanguage, [CellsCountStr, CellsStr])];

	private string CellsCountStr => Cells.Count.ToString();

	private string CellsStr => Options.Converter.CellConverter(Cells);
}
