namespace System.Linq;

/// <summary>
/// Represents a type that enumerates elements of type <typeparamref name="TSource"/> in a <see cref="ReadOnlySpan{T}"/>,
/// grouped by the specified key of type <typeparamref name="TKey"/>.
/// </summary>
/// <typeparam name="TSource">The type of each element.</typeparam>
/// <typeparam name="TKey">The type of the key.</typeparam>
/// <param name="elements">Indicates the elements.</param>
/// <param name="length">Indicates the length of elements stored in this collection.</param>
/// <param name="key">Indicates the key that can compare each element.</param>
[StructLayout(LayoutKind.Auto)]
[DebuggerStepThrough]
[Equals]
[GetHashCode]
[ToString]
[EqualityOperators]
public readonly unsafe partial struct SpanGrouping<TSource, TKey>(
	[PrimaryConstructorParameter(MemberKinds.Field, Accessibility = "private unsafe")] TSource* elements,
	[PrimaryConstructorParameter, HashCodeMember, StringMember] int length,
	[PrimaryConstructorParameter, HashCodeMember, StringMember] TKey key
) :
	IEnumerable<TSource>,
	IEqualityOperators<SpanGrouping<TSource, TKey>, SpanGrouping<TSource, TKey>, bool>,
	IEquatable<SpanGrouping<TSource, TKey>>,
	IGrouping<TKey, TSource>,
	IReadOnlyCollection<TSource>
	where TKey : notnull
{
	/// <inheritdoc/>
	int IReadOnlyCollection<TSource>.Count => Length;

	[HashCodeMember]
	private nint ElementsRawPointerValue => (nint)_elements;

	[StringMember]
	private string FirstElementString => _elements[0].ToString()!;

	/// <summary>
	/// Creates a <see cref="ReadOnlySpan{T}"/> instance that is aligned as <see cref="_elements"/>.
	/// </summary>
	/// <seealso cref="_elements"/>
	private ReadOnlySpan<TSource> SourceSpan
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		get => new(_elements, Length);
	}


	/// <summary>
	/// Gets the element at the specified index.
	/// </summary>
	/// <param name="index">The desired index.</param>
	/// <returns>The reference to the element at the specified index.</returns>
	public ref readonly TSource this[int index]
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		get => ref _elements[index];
	}


	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool Equals(SpanGrouping<TSource, TKey> other)
		=> _elements == other._elements && Length == other.Length && Key.Equals(other.Key);

	/// <summary>
	/// Projects elements into a new form.
	/// </summary>
	/// <typeparam name="TResult">The type of each element in result collection.</typeparam>
	/// <param name="selector">The selector method that transform the object into new one.</param>
	/// <returns>A list of <typeparamref name="TResult"/> values.</returns>
	public ReadOnlySpan<TResult> Select<TResult>(Func<TSource, TResult> selector)
	{
		var result = new List<TResult>(Length);
		foreach (var element in SourceSpan)
		{
			result.Add(selector(element));
		}
		return result.AsReadOnlySpan();
	}

	/// <summary>
	/// Filters the collection, only reserving elements satisfying the specified condition.
	/// </summary>
	/// <param name="predicate">The condition that checks for each element.</param>
	/// <returns>A list of <typeparamref name="TSource"/> elements satisfying the condition.</returns>
	public ReadOnlySpan<TSource> Where(Func<TSource, bool> predicate)
	{
		var result = new List<TSource>(Length);
		foreach (var element in SourceSpan)
		{
			if (predicate(element))
			{
				result.Add(element);
			}
		}
		return result.AsReadOnlySpan();
	}

	/// <summary>
	/// Creates an enumerator that can enumerate each element in the source collection.
	/// </summary>
	/// <returns>An enumerator instance.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public ReadOnlySpan<TSource>.Enumerator GetEnumerator() => SourceSpan.GetEnumerator();

	/// <inheritdoc cref="ReadOnlySpan{T}.GetPinnableReference"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public ref readonly TSource GetPinnableReference() => ref _elements[0];

	/// <inheritdoc/>
	IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable<TSource>)this).GetEnumerator();

	/// <inheritdoc/>
	IEnumerator<TSource> IEnumerable<TSource>.GetEnumerator() => ((IEnumerable<TSource>)SourceSpan.ToArray()).GetEnumerator();
}
