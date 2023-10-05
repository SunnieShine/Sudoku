namespace Sudoku.Concepts.Converters;

/// <summary>
/// Represents an option provider for coordinates.
/// </summary>
/// <param name="DefaultSeparator"><inheritdoc/></param>
/// <param name="DigitsSeparator"><inheritdoc/></param>
/// <remarks>
/// You can use types <see cref="RxCyConverter"/>, <seealso cref="K9Converter"/>, <see cref="LiteralCoordinateConverter"/>
/// and <see cref="ExcelCoordinateConverter"/>.
/// They are the derived types of the current type.
/// </remarks>
/// <seealso cref="RxCyConverter"/>
/// <seealso cref="K9Converter"/>
/// <seealso cref="LiteralCoordinateConverter"/>
/// <seealso cref="ExcelCoordinateConverter"/>
public abstract record CoordinateConverter(string DefaultSeparator = ", ", string? DigitsSeparator = null) :
	GenericConceptConverter(DefaultSeparator, DigitsSeparator)
{
	/// <summary>
	/// The converter method that creates a <see cref="string"/> via the specified list of cells.
	/// </summary>
	public abstract CellNotationConverter CellConverter { get; }

	/// <summary>
	/// The converter method that creates a <see cref="string"/> via the specified list of candidates.
	/// </summary>
	public abstract CandidateNotationConverter CandidateConverter { get; }

	/// <summary>
	/// The converter method that creates a <see cref="string"/> via the specified list of houses.
	/// </summary>
	public abstract HouseNotationConverter HouseConverter { get; }

	/// <summary>
	/// The converter method that creates a <see cref="string"/> via the specified list of conclusions.
	/// </summary>
	public abstract ConclusionNotationConverter ConclusionConverter { get; }

	/// <summary>
	/// The converter method that creates a <see cref="string"/> via the specified list of digits.
	/// </summary>
	public abstract DigitNotationConverter DigitConverter { get; }

	/// <summary>
	/// The converter method that creates a <see cref="string"/> via the specified information for an intersection.
	/// </summary>
	public abstract IntersectionNotationConverter IntersectionConverter { get; }

	/// <summary>
	/// The converter method that creates a <see cref="string"/> via the specified list of chute.
	/// </summary>
	public abstract ChuteNotationConverter ChuteConverter { get; }

	/// <summary>
	/// The converter method that creates a <see cref="string"/> via the specified conjugate.
	/// </summary>
	public abstract ConjugateNotationConverter ConjugateConverter { get; }
}