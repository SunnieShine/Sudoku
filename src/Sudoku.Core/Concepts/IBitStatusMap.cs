namespace Sudoku.Concepts;

/// <summary>
/// Extracts a base type that describes state table from elements of <typeparamref name="TSelf"/> type.
/// </summary>
/// <typeparam name="TSelf">The type of the instance that implements this interface type.</typeparam>
/// <typeparam name="TElement">The type of each element.</typeparam>
/// <typeparam name="TEnumerator">The type of the enumerator.</typeparam>
[TypeImpl(TypeImplFlag.EqualityOperators, EqualityOperatorsBehavior = EqualityOperatorsBehavior.MakeVirtual, IsLargeStructure = true)]
public partial interface IBitStatusMap<TSelf, TElement, TEnumerator> :
	IAdditiveIdentity<TSelf, TSelf>,
	IAdditionOperators<TSelf, TElement, TSelf>,
	IAnyAllMethod<TSelf, TElement>,
	IBitwiseOperators<TSelf, TSelf, TSelf>,
	IComparable<TSelf>,
	IComparisonOperators<TSelf, TSelf, bool>,
	IContainsMethod<TSelf, TElement>,
	ICountMethod<TSelf, TElement>,
	IElementAtMethod<TSelf, TElement>,
	IEqualityOperators<TSelf, TSelf, bool>,
	IEquatable<TSelf>,
	IFirstLastMethod<TSelf, TElement>,
	IGetSubsetMethod<TSelf, TElement>,
	IGroupByMethod<TSelf, TElement>,
	ILogicalOperators<TSelf>,
	IMinMaxValue<TSelf>,
	IModulusOperators<TSelf, TSelf, TSelf>,
	IReadOnlyList<TElement>,
	IReadOnlySet<TElement>,
	ISelectMethod<TSelf, TElement>,
	ISet<TElement>,
	ISpanFormattable,
	ISpanParsable<TSelf>,
	ISubtractionOperators<TSelf, TElement, TSelf>,
	IWhereMethod<TSelf, TElement>
	where TSelf : unmanaged, IBitStatusMap<TSelf, TElement, TEnumerator>
	where TElement : unmanaged, IBinaryInteger<TElement>
	where TEnumerator : struct, IEnumerator<TElement>
{
	/// <summary>
	/// Indicates the size of combinatorial calculation.
	/// </summary>
	private protected const int MaxLimit = 30;


	/// <inheritdoc cref="IReadOnlyCollection{T}.Count"/>
	public new abstract int Count { get; }

	/// <summary>
	/// Gets all chunks of the current collection, meaning a list of <see cref="string"/> values that can describe
	/// all offset values (cell indices and candidate indices), grouped with same row/column.
	/// </summary>
	public abstract string[] StringChunks { get; }

	/// <summary>
	/// Indicates the peer intersection of the current instance.
	/// </summary>
	/// <remarks>
	/// A <b>Peer Intersection</b> is a set of offsets that all offsets from the base collection can be seen.
	/// For more information please visit <see href="http://sudopedia.enjoysudoku.com/Peer.html">this link</see>.
	/// </remarks>
	public abstract TSelf PeerIntersection { get; }

	/// <summary>
	/// Indicates the offsets in this collection.
	/// </summary>
	protected internal abstract TElement[] Offsets { get; }

	/// <summary>
	/// Indicates the size of each unit.
	/// </summary>
	protected abstract int Shifting { get; }

	/// <inheritdoc/>
	bool ICollection<TElement>.IsReadOnly => false;


	/// <summary>
	/// Indicates an empty instance containing no elements.
	/// </summary>
	public static abstract TSelf Empty { get; }

	/// <summary>
	/// Indicates an instance that contains all possible elements.
	/// </summary>
	public static abstract TSelf Full { get; }

	/// <summary>
	/// Indicates the maximum number of elements that the collection can be reached.
	/// </summary>
	protected static abstract TElement MaxCount { get; }

	/// <summary>
	/// Indicates a converter instance that supports for serialization on the current instance.
	/// </summary>
	protected static abstract JsonConverter<TSelf> JsonConverterInstance { get; }

	/// <inheritdoc/>
	static TSelf IAdditiveIdentity<TSelf, TSelf>.AdditiveIdentity => TSelf.Empty;

	/// <inheritdoc/>
	static TSelf IMinMaxValue<TSelf>.MinValue => TSelf.Empty;

	/// <inheritdoc/>
	static TSelf IMinMaxValue<TSelf>.MaxValue => TSelf.Empty;


	/// <inheritdoc cref="IReadOnlyCollection{T}.Count"/>
	public new abstract TElement this[int index] { get; }


	/// <summary>
	/// Adds a list of offsets into the current collection.
	/// </summary>
	/// <param name="offsets">
	/// <para>Offsets to be added.</para>
	/// <include file="../../global-doc-comments.xml" path="//g/csharp12/feature[@name='params-collections']/target[@name='parameter']"/>
	/// </param>
	/// <returns>The number of offsets succeeded to be added.</returns>
	public abstract int AddRange(scoped ReadOnlySpan<TElement> offsets);

	/// <summary>
	/// Removes a list of offsets from the current collection.
	/// </summary>
	/// <param name="offsets">
	/// <para>Offsets to be removed.</para>
	/// <include file="../../global-doc-comments.xml" path="//g/csharp12/feature[@name='params-collections']/target[@name='parameter']"/>
	/// </param>
	/// <returns>The number of offsets succeeded to be removed.</returns>
	public abstract int RemoveRange(scoped ReadOnlySpan<TElement> offsets);

	/// <summary>
	/// Copies the current instance to the target sequence specified as a reference
	/// to an element of type <typeparamref name="TElement"/>.
	/// </summary>
	/// <param name="sequence">
	/// The reference that points to the first element in a sequence of type <typeparamref name="TElement"/>.
	/// </param>
	/// <param name="length">The length of that array.</param>
	/// <exception cref="ArgumentNullException">
	/// Throws when the argument <paramref name="sequence"/> is <see langword="null"/>.
	/// </exception>
	/// <exception cref="InvalidOperationException">
	/// Throws when the capacity isn't enough to store all values.
	/// </exception>
	public abstract void CopyTo(ref TElement sequence, int length);

	/// <summary>
	/// Iterates on each element in this collection.
	/// </summary>
	/// <param name="action">The visitor that handles for each element in this collection.</param>
	public abstract void ForEach(Action<TElement> action);

	/// <summary>
	/// Try to toggle the offset, which means the value will be added if not exist in collection, or removed if exists.
	/// </summary>
	/// <param name="offset">The offset to be added or removed.</param>
	public abstract void Toggle(TElement offset);

	/// <summary>
	/// Try to get the specified index of the offset.
	/// </summary>
	/// <param name="offset">The desired offset.</param>
	/// <returns>The index of the offset.</returns>
	public abstract int IndexOf(TElement offset);

	/// <summary>
	/// <inheritdoc cref="IComparable{TSelf}.CompareTo(TSelf)" path="/summary"/>
	/// </summary>
	/// <param name="other"><inheritdoc cref="IComparable{TSelf}.CompareTo(TSelf)" path="/param[@name='other']"/></param>
	/// <returns>
	/// <para>The result value only contains 3 possible values: 1, 0 and -1.</para>
	/// <para>
	/// The comparison rule is:
	/// <list type="number">
	/// <item>
	/// If <see langword="this"/> holds more offsets than <paramref name="other"/>, then return 1
	/// indicating <see langword="this"/> is greater.
	/// </item>
	/// <item>
	/// If <see langword="this"/> holds less offsets than <paramref name="other"/>, then return -1
	/// indicating <paramref name="other"/> is greater.
	/// </item>
	/// <item>
	/// If they two hold same offsets, then checks for indices held:
	/// <list type="bullet">
	/// <item>
	/// If <see langword="this"/> holds a cell whose index is greater than all offsets appeared in <paramref name="other"/>,
	/// then return 1 indicating <see langword="this"/> is greater.
	/// </item>
	/// <item>
	/// If <paramref name="other"/> holds a cell whose index is greater than all offsets
	/// appeared in <paramref name="other"/>, then return -1 indicating <paramref name="other"/> is greater.
	/// </item>
	/// </list>
	/// </item>
	/// </list>
	/// If all rules are compared, but they are still considered equal, then return 0.
	/// </para>
	/// </returns>
	public abstract int CompareTo(ref readonly TSelf other);

	/// <inheritdoc cref="IEquatable{T}.Equals(T)"/>
	public abstract bool Equals(scoped ref readonly TSelf other);

	/// <summary>
	/// Get all offsets whose bits are set <see langword="true"/>.
	/// </summary>
	/// <returns>An array of offsets.</returns>
	public abstract TElement[] ToArray();

	/// <summary>
	/// Slices the current instance, and get the new instance with some of elements between two indices.
	/// </summary>
	/// <param name="start">The start index.</param>
	/// <param name="count">The number of elements.</param>
	/// <returns>The target instance.</returns>
	public abstract TSelf Slice(int start, int count);

	/// <summary>
	/// Gets the enumerator of the current instance in order to use <see langword="foreach"/> loop.
	/// </summary>
	/// <returns>The enumerator instance.</returns>
	public new abstract TEnumerator GetEnumerator();

	/// <inheritdoc/>
	void ICollection<TElement>.Clear() => Clear();

	/// <inheritdoc/>
	void ICollection<TElement>.CopyTo(TElement[] array, int arrayIndex)
		=> CopyTo(ref array[arrayIndex], ((ICollection<TElement>)this).Count - arrayIndex);

	/// <inheritdoc/>
	bool IReadOnlySet<TElement>.Contains(TElement item) => ((ICollection<TElement>)this).Contains(item);

	/// <inheritdoc/>
	bool IEquatable<TSelf>.Equals(TSelf other) => Equals(in other);

	/// <inheritdoc/>
	void ICollection<TElement>.Add(TElement item) => Add(item);

	/// <inheritdoc/>
	bool ISet<TElement>.Add(TElement item) => Add(item);

	/// <inheritdoc/>
	bool ISet<TElement>.IsProperSubsetOf(IEnumerable<TElement> other)
	{
		var otherCells = (TSelf)[];
		foreach (var element in other)
		{
			otherCells.Add(element);
		}

		return (TSelf)this != otherCells && (otherCells & (TSelf)this) == (TSelf)this;
	}

	/// <inheritdoc/>
	bool ISet<TElement>.IsProperSupersetOf(IEnumerable<TElement> other)
	{
		var otherCells = (TSelf)[];
		foreach (var element in other)
		{
			otherCells.Add(element);
		}
		return (TSelf)this != otherCells && ((TSelf)this & otherCells) == otherCells;
	}

	/// <inheritdoc/>
	bool ISet<TElement>.IsSubsetOf(IEnumerable<TElement> other)
	{
		var otherCells = (TSelf)[];
		foreach (var element in other)
		{
			otherCells.Add(element);
		}
		return (otherCells & (TSelf)this) == (TSelf)this;
	}

	/// <inheritdoc/>
	bool ISet<TElement>.IsSupersetOf(IEnumerable<TElement> other)
	{
		var otherCells = (TSelf)[];
		foreach (var element in other)
		{
			otherCells.Add(element);
		}
		return ((TSelf)this & otherCells) == otherCells;
	}

	/// <inheritdoc/>
	bool ISet<TElement>.Overlaps(IEnumerable<TElement> other) => !!((TSelf)this & [.. other]);

	/// <inheritdoc/>
	bool ISet<TElement>.SetEquals(IEnumerable<TElement> other) => (TSelf)this == [.. other];

	/// <inheritdoc/>
	void ISet<TElement>.ExceptWith(IEnumerable<TElement> other)
	{
		foreach (var element in other)
		{
			Remove(element);
		}
	}

	/// <inheritdoc/>
	void ISet<TElement>.IntersectWith(IEnumerable<TElement> other)
	{
		var result = (TSelf)this;
		foreach (var element in other)
		{
			if (((ICollection<TElement>)this).Contains(element))
			{
				result.Add(element);
			}
		}

		Clear();
		foreach (var element in result)
		{
			Add(element);
		}
	}

	/// <inheritdoc/>
	void ISet<TElement>.SymmetricExceptWith(IEnumerable<TElement> other)
	{
		var left = this;
		foreach (var element in other)
		{
			left.Remove(element);
		}

		var right = [.. other] & ~(TSelf)this;
		Clear();
		foreach (var element in (TSelf)left | right)
		{
			Add(element);
		}
	}

	/// <inheritdoc/>
	void ISet<TElement>.UnionWith(IEnumerable<TElement> other)
	{
		foreach (var element in other)
		{
			Add(element);
		}
	}

	/// <inheritdoc/>
	bool IReadOnlySet<TElement>.Overlaps(IEnumerable<TElement> other) => ((ISet<TElement>)this).Overlaps(other);

	/// <inheritdoc/>
	bool IReadOnlySet<TElement>.SetEquals(IEnumerable<TElement> other) => ((ISet<TElement>)this).SetEquals(other);

	/// <inheritdoc/>
	bool IReadOnlySet<TElement>.IsProperSubsetOf(IEnumerable<TElement> other) => ((ISet<TElement>)this).IsProperSubsetOf(other);

	/// <inheritdoc/>
	bool IReadOnlySet<TElement>.IsProperSupersetOf(IEnumerable<TElement> other) => ((ISet<TElement>)this).IsProperSupersetOf(other);

	/// <inheritdoc/>
	bool IReadOnlySet<TElement>.IsSubsetOf(IEnumerable<TElement> other) => ((ISet<TElement>)this).IsSubsetOf(other);

	/// <inheritdoc/>
	bool IReadOnlySet<TElement>.IsSupersetOf(IEnumerable<TElement> other) => ((ISet<TElement>)this).IsSupersetOf(other);

	/// <inheritdoc/>
	bool ICollection<TElement>.Remove(TElement item) => Remove(item);

	/// <inheritdoc/>
	bool IContainsMethod<TSelf, TElement>.Contains(TElement value) => ((ICollection<TElement>)this).Contains(value);

	/// <inheritdoc/>
	int ICountMethod<TSelf, TElement>.Count() => Count;

	/// <inheritdoc/>
	int IComparable<TSelf>.CompareTo(TSelf other) => CompareTo(in other);

	/// <inheritdoc/>
	TElement IElementAtMethod<TSelf, TElement>.ElementAt(int index) => this[index];

	/// <inheritdoc/>
	TElement IElementAtMethod<TSelf, TElement>.ElementAt(Index index) => this[index];

	/// <inheritdoc/>
	TElement IElementAtMethod<TSelf, TElement>.ElementAtOrDefault(int index) => this[index];

	/// <inheritdoc/>
	TElement IElementAtMethod<TSelf, TElement>.ElementAtOrDefault(Index index) => this[index];

	/// <inheritdoc/>
	IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable<TElement>)this).GetEnumerator();

	/// <inheritdoc/>
	IEnumerator<TElement> IEnumerable<TElement>.GetEnumerator()
	{
		if (Offsets.Length == 0)
		{
			yield break;
		}

		foreach (var element in Offsets)
		{
			yield return element;
		}
	}

	/// <inheritdoc/>
	IEnumerable<TElement[]> IGetSubsetMethod<TSelf, TElement>.GetSubsets(int subsetSize)
	{
		var result = new List<TElement[]>();
		foreach (var element in (TSelf)this & subsetSize)
		{
			result.Add(element.ToArray());
		}
		return result;
	}


	/// <summary>
	/// Determines whether the current collection is empty.
	/// </summary>
	/// <param name="offsets">The offsets to be checked.</param>
	/// <returns>A <see cref="bool"/> value indicating that.</returns>
	/// <remarks>
	/// The type of the current collection supports using <see cref="bool"/>-like expression to determine whether the collection is not empty,
	/// for example:
	/// <code><![CDATA[
	/// if (collection)
	///     // ...
	/// ]]></code>
	/// The statement <c>collection</c> will be expanded to <c>collection.Count != 0</c>. Therefore, the negation operator <c>!</c>
	/// will invert the result of above expression. This is why I use <see langword="operator"/> <c>!</c> to determine on this.
	/// </remarks>
	public static abstract bool operator !(in TSelf offsets);

	/// <summary>
	/// Reverse state for all offsets, which means all <see langword="true"/> bits
	/// will be set <see langword="false"/>, and all <see langword="false"/> bits
	/// will be set <see langword="true"/>.
	/// </summary>
	/// <param name="offsets">The instance to negate.</param>
	/// <returns>The negative result.</returns>
	public static abstract TSelf operator ~(in TSelf offsets);

	/// <summary>
	/// Determines whether the specified <typeparamref name="TSelf"/> collection is not empty.
	/// </summary>
	/// <param name="map">The collection.</param>
	/// <returns>A <see cref="bool"/> result indicating that.</returns>
	public static abstract bool operator true(in TSelf map);

	/// <summary>
	/// Determines whether the specified <typeparamref name="TSelf"/> collection is empty.
	/// </summary>
	/// <param name="map">The collection.</param>
	/// <returns>A <see cref="bool"/> result indicating that.</returns>
	public static abstract bool operator false(in TSelf map);

	/// <summary>
	/// Adds the specified <paramref name="offset"/> to the <paramref name="collection"/>,
	/// and returns the added result.
	/// </summary>
	/// <param name="collection">The collection.</param>
	/// <param name="offset">The offset to be added.</param>
	/// <returns>The result collection.</returns>
	public static abstract TSelf operator +(in TSelf collection, TElement offset);

	/// <summary>
	/// Removes the specified <paramref name="offset"/> from the <paramref name="collection"/>,
	/// and returns the removed result.
	/// </summary>
	/// <param name="collection">The collection.</param>
	/// <param name="offset">The offset to be removed.</param>
	/// <returns>The result collection.</returns>
	public static abstract TSelf operator -(in TSelf collection, TElement offset);

	/// <summary>
	/// Get the elements that both <paramref name="left"/> and <paramref name="right"/> contain.
	/// </summary>
	/// <param name="left">The left instance.</param>
	/// <param name="right">The right instance.</param>
	/// <returns>The result.</returns>
	public static abstract TSelf operator &(in TSelf left, in TSelf right);

	/// <summary>
	/// Combine the elements from <paramref name="left"/> and <paramref name="right"/>,
	/// and return the merged result.
	/// </summary>
	/// <param name="left">The left instance.</param>
	/// <param name="right">The right instance.</param>
	/// <returns>The result.</returns>
	public static abstract TSelf operator |(in TSelf left, in TSelf right);

	/// <summary>
	/// Get the elements that either <paramref name="left"/> or <paramref name="right"/> contains.
	/// </summary>
	/// <param name="left">The left instance.</param>
	/// <param name="right">The right instance.</param>
	/// <returns>The result.</returns>
	public static abstract TSelf operator ^(in TSelf left, in TSelf right);

	/// <summary>
	/// Gets the subsets of the current collection via the specified size indicating the number of elements of the each subset.
	/// </summary>
	/// <param name="map">The instance to check for subsets.</param>
	/// <param name="subsetSize">The size to get.</param>
	/// <returns>
	/// All possible subsets. If:
	/// <list type="table">
	/// <listheader>
	/// <term>Condition</term>
	/// <description>Meaning</description>
	/// </listheader>
	/// <item>
	/// <term><c><paramref name="subsetSize"/> &gt; Count</c></term>
	/// <description>Will return an empty array</description>
	/// </item>
	/// <item>
	/// <term><c><paramref name="subsetSize"/> == Count</c></term>
	/// <description>
	/// Will return an array that contains only one element, same as the current instance.
	/// </description>
	/// </item>
	/// <item>
	/// <term>Other cases</term>
	/// <description>The valid combinations.</description>
	/// </item>
	/// </list>
	/// </returns>
	/// <exception cref="NotSupportedException">
	/// Throws when both the count of the current instance and <paramref name="subsetSize"/> are greater than 30.
	/// </exception>
	/// <remarks>
	/// For example, if the current instance is <c>r1c1</c>, <c>r1c2</c> and <c>r1c3</c>
	/// and the argument <paramref name="subsetSize"/> is 2,
	/// the method will return an array of 3 elements given below: <c>r1c12</c>, <c>r1c13</c> and <c>r1c23</c>.
	/// </remarks>
	public static abstract ReadOnlySpan<TSelf> operator &(in TSelf map, int subsetSize);

	/// <summary>
	/// Gets all subsets of the current collection via the specified size
	/// indicating the <b>maximum</b> number of elements of the each subset.
	/// </summary>
	/// <param name="map">The instance to check subsets.</param>
	/// <param name="subsetSize">The size to get.</param>
	/// <returns>
	/// All possible subsets. If:
	/// <list type="table">
	/// <listheader>
	/// <term>Condition</term>
	/// <description>Meaning</description>
	/// </listheader>
	/// <item>
	/// <term><c><paramref name="subsetSize"/> &gt; Count</c></term>
	/// <description>Will return an empty array</description>
	/// </item>
	/// <item>
	/// <term>Other cases</term>
	/// <description>The valid combinations.</description>
	/// </item>
	/// </list>
	/// </returns>
	public static abstract ReadOnlySpan<TSelf> operator |(in TSelf map, int subsetSize);

	/// <inheritdoc/>
	static bool ILogicalOperators<TSelf>.operator true(TSelf value) => value ? true : false;

	/// <inheritdoc/>
	static bool ILogicalOperators<TSelf>.operator false(TSelf value) => !(value ? true : false);

	/// <inheritdoc/>
	static bool ILogicalOperators<TSelf>.operator !(TSelf value) => !value;

	/// <inheritdoc/>
	static bool IEqualityOperators<TSelf, TSelf, bool>.operator ==(TSelf left, TSelf right) => left == right;

	/// <inheritdoc/>
	static bool IEqualityOperators<TSelf, TSelf, bool>.operator !=(TSelf left, TSelf right) => left != right;

	/// <inheritdoc/>
	static TSelf IAdditionOperators<TSelf, TElement, TSelf>.operator +(TSelf left, TElement right) => left + right;

	/// <inheritdoc/>
	static TSelf ISubtractionOperators<TSelf, TElement, TSelf>.operator -(TSelf left, TElement right) => left - right;

	/// <inheritdoc/>
	static TSelf IModulusOperators<TSelf, TSelf, TSelf>.operator %(TSelf left, TSelf right) => (left & right).PeerIntersection & right;

	/// <inheritdoc/>
	static TSelf IBitwiseOperators<TSelf, TSelf, TSelf>.operator ~(TSelf value) => ~value;

	/// <inheritdoc/>
	static TSelf IBitwiseOperators<TSelf, TSelf, TSelf>.operator &(TSelf left, TSelf right) => left & right;

	/// <inheritdoc/>
	static TSelf IBitwiseOperators<TSelf, TSelf, TSelf>.operator |(TSelf left, TSelf right) => left | right;

	/// <inheritdoc/>
	static TSelf IBitwiseOperators<TSelf, TSelf, TSelf>.operator ^(TSelf left, TSelf right) => left ^ right;

	/// <inheritdoc/>
	static TSelf ILogicalOperators<TSelf>.operator &(TSelf left, TSelf right) => left & right;

	/// <inheritdoc/>
	static TSelf ILogicalOperators<TSelf>.operator |(TSelf left, TSelf right) => left | right;

	/// <inheritdoc/>
	static TSelf ILogicalOperators<TSelf>.operator ^(TSelf left, TSelf right) => left ^ right;
}
