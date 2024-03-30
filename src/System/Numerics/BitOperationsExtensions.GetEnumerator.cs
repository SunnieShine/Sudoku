namespace System.Numerics;

public partial class BitOperationsExtensions
{
	/// <summary>
	/// <para>Get an enumerator to iterate on each bits of the specified integer value.</para>
	/// <para>This method will allow you to use <see langword="foreach"/> loop to iterate on all indices of set bits.</para>
	/// </summary>
	/// <param name="this">The value.</param>
	/// <returns>All indices of set bits.</returns>
	/// <remarks>
	/// This method allows you using <see langword="foreach"/> loop to iterate this value:
	/// <code><![CDATA[
	/// foreach (var bit in 17)
	/// {
	///     // Do something...
	/// }
	/// ]]></code>
	/// </remarks>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static partial Int32Enumerator GetEnumerator(this sbyte @this) => new((uint)@this);

	/// <inheritdoc cref="GetEnumerator(sbyte)"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static partial Int32Enumerator GetEnumerator(this byte @this) => new(@this);

	/// <inheritdoc cref="GetEnumerator(sbyte)"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static partial Int32Enumerator GetEnumerator(this short @this) => new((uint)@this);

	/// <inheritdoc cref="GetEnumerator(sbyte)"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static partial Int32Enumerator GetEnumerator(this ushort @this) => new(@this);

	/// <inheritdoc cref="GetEnumerator(sbyte)"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static partial Int32Enumerator GetEnumerator(this int @this) => new((uint)@this);

	/// <inheritdoc cref="GetEnumerator(sbyte)"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static partial Int32Enumerator GetEnumerator(this uint @this) => new(@this);

	/// <inheritdoc cref="GetEnumerator(sbyte)"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static partial Int64Enumerator GetEnumerator(this long @this) => new((ulong)@this);

	/// <inheritdoc cref="GetEnumerator(sbyte)"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static partial Int64Enumerator GetEnumerator(this ulong @this) => new(@this);

	/// <inheritdoc cref="GetEnumerator(sbyte)"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static partial NativeIntegerEnumerator GetEnumerator(this nint @this) => new((nuint)@this);

	/// <inheritdoc cref="GetEnumerator(sbyte)"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static partial NativeIntegerEnumerator GetEnumerator(this nuint @this) => new(@this);
}
