namespace Sudoku.Recognition.Imaging;

/// <summary>
/// Indicates the exception that throws when the tesseract has encountered an error.
/// </summary>
/// <param name="detail">Indicates the detail.</param>
public sealed partial class TesseractException([RecordParameter] string detail) : Exception
{
	/// <inheritdoc/>
	public override string Message => $"Tesseract has encountered an error: {Detail}.";

	/// <inheritdoc/>
	public override string? HelpLink => null;
}