namespace Sudoku.Measuring;

/// <summary>
/// Represents some methods operating with <see cref="Factor"/> instances.
/// </summary>
/// <seealso cref="Factor"/>
public static class FactorMarshal
{
	/// <summary>
	/// Try to get the scale unit length from the <see cref="Factor.Scale"/> value.
	/// </summary>
	/// <exception cref="InvalidOperationException">Throws when the scale value is negative.</exception>
	/// <seealso cref="Factor.Scale"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static int GetScaleUnitLength(decimal scale)
		=> scale switch
		{
			< 0 => throw new InvalidOperationException(ResourceDictionary.ExceptionMessage("ScaleValueCannotBeNegative")),
			0 => 0, // Special case: return 0 if scale is 0.
			_ when scale.ToString() is var str && str.IndexOf('.') is var posOfPeriod => posOfPeriod switch
			{
				-1 => 0, // This is an integer (no period token is found). Return 0.
				_ => str.Length - posOfPeriod - 1
			}
		};

	/// <summary>
	/// Try to get the scale unit from the <see cref="Factor.Scale"/> value.
	/// </summary>
	/// <seealso cref="Factor.Scale"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string? GetScaleFormatString(decimal scale)
		=> GetScaleUnitLength(scale) is var p and not 0 ? $"#.{new('0', p)}" : null;
}
