﻿namespace System.Diagnostics.CodeGen;

/// <summary>
/// Defines an attributes that allows user using it applying to a type
/// (especially for <see langword="class"/> or <see langword="struct"/>), indicating the overriden metadata
/// for <c><see langword="operator"/> ==</c> and <c><see langword="operator"/> !=</c>,
/// and make source generator generates their own operators automatically.
/// </summary>
/// <remarks>
/// At default, the emitted generated operator overloading will use <see cref="IEquatable{T}.Equals(T)"/> invocation.
/// </remarks>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = false, Inherited = true)]
public sealed class AutoOverloadsEqualityOperatorsAttribute : Attribute
{
	/// <summary>
	/// Indicates whether the source generator will emit keyword <see langword="in"/> to the parameters.
	/// The default value is <see langword="false"/>.
	/// </summary>
	public bool EmitInKeyword { get; init; } = false;

	/// <summary>
	/// Indicates whether the source generator will emit nullable annotation <c>?</c> onto the arguments.
	/// The default value is <see langword="false"/>.
	/// </summary>
	public bool WithNullableAnnotation { get; init; } = false;
}
