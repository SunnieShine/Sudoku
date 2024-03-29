namespace System.Numerics;

/// <summary>
/// Represents an enumerator that iterates an <see cref="nint"/> or <see cref="nuint"/> value.
/// </summary>
/// <param name="value">The value to be iterated.</param>
[StructLayout(LayoutKind.Auto)]
public ref partial struct NativeIntEnumerator([PrimaryConstructorParameter(MemberKinds.Field, IsImplicitlyReadOnly = false)] nuint value)
{
	/// <summary>
	/// Indicates the population count of the value.
	/// </summary>
	public readonly int PopulationCount => PopCount(_value);

	/// <summary>
	/// Indicates the bits set.
	/// </summary>
	public readonly ReadOnlySpan<int> Bits => _value.GetAllSets();

	/// <inheritdoc cref="IEnumerator{T}.Current"/>
	public int Current { get; private set; } = -1;


	/// <inheritdoc cref="BitOperationsExtensions.SetAt(uint, int)"/>
	public readonly int this[int index] => _value.SetAt(index);


	/// <inheritdoc cref="IEnumerator.MoveNext"/>
	public unsafe bool MoveNext()
	{
		while (++Current < sizeof(nuint) << 3)
		{
			if ((_value >> Current & 1) != 0)
			{
				return true;
			}
		}

		return false;
	}
}
