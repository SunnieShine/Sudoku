namespace Sudoku.Concepts;

/// <summary>
/// Represents a <see cref="GridFormatInfo"/> type that supports mask formatting.
/// </summary>
/// <remarks>
/// <para>This type is used by diagnostic only.</para>
/// <para>
/// Please note that the method cannot be called with a correct behavior using
/// <see cref="DebuggerDisplayAttribute"/> to output. It seems that Visual Studio
/// doesn't print correct values when indices of this grid aren't 0. In other words,
/// when we call this method using <see cref="DebuggerDisplayAttribute"/>, only <c>grid[0]</c>
/// can be output correctly, and other values will be incorrect: they're always 0.
/// </para>
/// </remarks>
public sealed class MaskGridFormatInfo : GridFormatInfo
{
	/// <inheritdoc/>
	[return: NotNullIfNotNull(nameof(formatType))]
	public override object? GetFormat(Type? formatType) => formatType == typeof(MaskGridFormatInfo) ? this : null;

	/// <inheritdoc/>
	public override bool Equals([NotNullWhen(true)] GridFormatInfo? other) => other is MaskGridFormatInfo;

	/// <inheritdoc/>
	public override int GetHashCode() => HashCode.Combine(typeof(MaskGridFormatInfo));

	/// <inheritdoc/>
	public override MaskGridFormatInfo Clone() => new();

	/// <inheritdoc/>
	protected internal override string FormatGrid(ref readonly Grid grid)
	{
		var sb = new StringBuilder(400);
		foreach (var mask in grid)
		{
			sb.Append(mask).Append(Separator);
		}
		return sb.RemoveFrom(^Separator.Length).ToString();
	}

	/// <inheritdoc/>
	[DoesNotReturn]
	protected internal override Grid ParseGrid(string str) => throw new NotSupportedException();
}
