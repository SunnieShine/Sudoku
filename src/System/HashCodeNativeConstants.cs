namespace System;

/// <summary>
/// Provides constants that is defined in .NET API <see cref="HashCode"/> but aren't exposed.
/// </summary>
/// <seealso cref="HashCode"/>
public static class HashCodeNativeConstants
{
#if !NET9_0_OR_GREATER
	/// <summary>
	/// Indicates the third prime number.
	/// </summary>
	private const uint Prime3Value = 3266489917U;
#endif


	/// <summary>
	/// Indicates the third prime number defined in type <see cref="HashCode"/>.
	/// </summary>
	/// <returns>The value.</returns>
	/// <remarks>
	/// <include
	///     file="../../global-doc-comments.xml"
	///     path="//g/dotnet/version[@value='8']/feature[@name='unsafe-accessor']/target[@name='others']"/>
	/// <include
	///     file="../../global-doc-comments.xml"
	///     path="//g/dotnet/version[@value='9' and @preview-value='4']/feature[@name='unsafe-accessor']"/>
	/// </remarks>
#if NET9_0_OR_GREATER
	[UnsafeAccessor(UnsafeAccessorKind.StaticField, Name = "Prime3")]
	public static extern ref uint Prime3();
#else
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static uint Prime3() => Prime3Value;
#endif
}