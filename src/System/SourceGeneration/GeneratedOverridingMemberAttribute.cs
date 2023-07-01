namespace System.SourceGeneration;

/// <summary>
/// Represents a marker attribute to tell source generators that the member derived from type <see cref="object"/>
/// or <see cref="ValueType"/> should be automatically implemented.
/// </summary>
/// <remarks>
/// This attribute supports the following members:
/// <list type="bullet">
/// <item><see cref="object.Equals(object?)"/> and <see cref="ValueType.Equals(object?)"/></item>
/// <item><see cref="object.GetHashCode"/> and <see cref="ValueType.GetHashCode"/></item>
/// <item><see cref="object.ToString"/> and <see cref="ValueType.ToString"/></item>
/// </list>
/// </remarks>
/// <seealso cref="object"/>
/// <seealso cref="ValueType"/>
[AttributeUsage(AttributeTargets.Method, Inherited = false)]
[Obsolete($"This attribute will be replaced with another attribute types, e.g. '{nameof(EqualsAttribute)}'.", false)]
public sealed class GeneratedOverridingMemberAttribute : Attribute
{
	/// <summary>
	/// Initializes a <see cref="GeneratedOverridingMemberAttribute"/> instance via the specified behavior on generating
	/// <see cref="object.GetHashCode"/>, with the specified array as extra arguments.
	/// </summary>
	/// <param name="overridingGetHashCodeBehavior">The behavior.</param>
	/// <param name="arguments">Extra arguments.</param>
	public GeneratedOverridingMemberAttribute(GeneratedGetHashCodeBehavior overridingGetHashCodeBehavior, params object?[]? arguments)
	{
	}

	/// <summary>
	/// Initializes a <see cref="GeneratedOverridingMemberAttribute"/> instance via the specified behavior on generating
	/// <see cref="object.ToString"/>, with the specified array as extra arguments.
	/// </summary>
	/// <param name="overridingToStringBehavior">The behavior.</param>
	/// <param name="arguments">Extra arguments.</param>
	public GeneratedOverridingMemberAttribute(GeneratedToStringBehavior overridingToStringBehavior, params object?[]? arguments)
	{
	}
}
