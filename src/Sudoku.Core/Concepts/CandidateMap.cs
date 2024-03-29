namespace Sudoku.Concepts;

/// <summary>
/// Encapsulates a binary series of candidate state table.
/// The internal buffer size 12 is equivalent to expression <c><![CDATA[floor(729 / sizeof(long) << 6)]]></c>.
/// </summary>
/// <remarks>
/// <include file="../../global-doc-comments.xml" path="/g/large-structure"/>
/// </remarks>
[JsonConverter(typeof(CandidateMapConverter))]
[StructLayout(LayoutKind.Auto)]
[CollectionBuilder(typeof(CandidateMap), nameof(Create))]
[DebuggerStepThrough]
[InlineArrayField<long>("_bits", 12)]
[LargeStructure]
[Equals]
[EqualityOperators]
public partial struct CandidateMap :
	IAdditionOperators<CandidateMap, Candidate, CandidateMap>,
	ICoordinateObject<CandidateMap>,
	IDivisionOperators<CandidateMap, Digit, CellMap>,
	ISubtractionOperators<CandidateMap, Candidate, CandidateMap>,
	IBitStatusMap<CandidateMap, Candidate, CandidateMap.Enumerator>
{
	/// <inheritdoc cref="IMinMaxValue{TSelf}.MaxValue"/>
	public static readonly CandidateMap MaxValue = ~default(CandidateMap);

	/// <inheritdoc cref="IMinMaxValue{TSelf}.MinValue"/>
	public static readonly CandidateMap MinValue;


	/// <summary>
	/// Initializes a <see cref="CandidateMap"/> instance via a list of candidate offsets represented as a RxCy notation.
	/// </summary>
	/// <param name="segments">The candidate offsets, represented as a RxCy notation.</param>
	[JsonConstructor]
	public CandidateMap(string[] segments)
	{
		this = [];
		foreach (var segment in segments)
		{
			this |= ParseExact(segment, new RxCyParser());
		}
	}

	/// <summary>
	/// Indicates a <see cref="CandidateMap"/> instance with the peer candidates of the specified candidate and a <see cref="bool"/>
	/// value indicating whether the map will process itself with <see langword="true"/> value.
	/// </summary>
	/// <param name="candidate">The candidate.</param>
	/// <param name="withItself">Indicates whether the map will process itself with <see langword="true"/> value.</param>
	private CandidateMap(Candidate candidate, bool withItself)
	{
		(this, var cell, var digit) = ([], candidate / 9, candidate % 9);
		foreach (var c in PeersMap[cell])
		{
			Add(c * 9 + digit);
		}
		for (var d = 0; d < 9; d++)
		{
			if (d != digit || d == digit && withItself)
			{
				Add(cell * 9 + d);
			}
		}
	}


	/// <inheritdoc/>
	[ImplicitField(RequiredReadOnlyModifier = false)]
	public readonly int Count
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		get => _count;
	}

	/// <inheritdoc/>
	[JsonInclude]
	public readonly string[] StringChunks
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		get
		{
			return this switch { { _count: 0 } => [], [var a] => [GlobalizedConverter.InvariantCultureConverter.CandidateConverter([a])], _ => f(Offsets) };


			static string[] f(Candidate[] offsets)
			{
				var list = new List<string>();
				foreach (var digitGroup in
					from candidate in offsets
					group candidate by candidate % 9 into digitGroups
					orderby digitGroups.Key
					select digitGroups)
				{
					var sb = new StringBuilder(50);
					var cells = (CellMap)[];
					foreach (var candidate in digitGroup)
					{
						cells.Add(candidate / 9);
					}

					list.Add(
						sb
							.Append(GlobalizedConverter.InvariantCultureConverter.CellConverter(in cells))
							.Append($"({digitGroup.Key + 1})")
							.ToString()
					);
				}

				return [.. list];
			}
		}
	}

	/// <summary>
	/// Indicates the digits used in this pattern.
	/// </summary>
	public readonly Mask Digits
	{
		get
		{
			var result = (Mask)0;
			for (var digit = 0; digit < 9; digit++)
			{
				if (this / digit)
				{
					result |= (Mask)(1 << digit);
				}
			}

			return result;
		}
	}

	/// <summary>
	/// Indicates the cells used in this pattern.
	/// </summary>
	public readonly CellMap Cells => [.. CellDistribution.Keys];

	/// <inheritdoc/>
	public readonly CandidateMap PeerIntersection
	{
		get
		{
			if (_count == 0)
			{
				// Empty list can't contain any peer intersections.
				return [];
			}

			var result = MaxValue;
			foreach (var candidate in Offsets)
			{
				result &= new CandidateMap(candidate, false);
			}

			return result;
		}
	}

	/// <summary>
	/// Returns a <see cref="FrozenDictionary{TKey, TValue}"/> that describes the distribution of digits appeared in cells, grouped by digit.
	/// </summary>
	public readonly FrozenDictionary<Digit, CellMap> DigitDistribution
	{
		get
		{
			var dictionary = new Dictionary<Digit, CellMap>(9);
			for (var digit = 0; digit < 9; digit++)
			{
				if (this / digit is var map and not [])
				{
					dictionary.Add(digit, map);
				}
			}

			return dictionary.ToFrozenDictionary();
		}
	}

	/// <summary>
	/// Returns a <see cref="FrozenDictionary{TKey, TValue}"/> that describes the distribution of digits appeared in cells, grouped by cell.
	/// </summary>
	public readonly FrozenDictionary<Cell, Mask> CellDistribution
	{
		get
		{
			var dictionary = new Dictionary<Cell, Mask>(81);
			foreach (var element in Offsets)
			{
				if (!dictionary.TryAdd(element / 9, (Mask)(1 << element % 9)))
				{
					dictionary[element / 9] |= (Mask)(1 << element % 9);
				}
			}

			return dictionary.ToFrozenDictionary();
		}
	}

	/// <summary>
	/// Indicates the cell offsets in this collection.
	/// </summary>
	internal readonly Candidate[] Offsets
	{
		get
		{
			if (!this)
			{
				return [];
			}

			var arr = new Candidate[_count];
			var pos = 0;
			for (var i = 0; i < 729; i++)
			{
				if (Contains(i))
				{
					arr[pos++] = i;
				}
			}

			return arr;
		}
	}

	/// <inheritdoc/>
	readonly int IBitStatusMap<CandidateMap, Candidate, Enumerator>.Shifting => sizeof(long) << 3;

	/// <inheritdoc/>
	readonly Candidate[] IBitStatusMap<CandidateMap, Candidate, Enumerator>.Offsets => Offsets;


	/// <inheritdoc/>
	public static ref readonly CandidateMap NullRef => ref Ref.MakeNullReference<CandidateMap>();

	/// <inheritdoc/>
	static Candidate IBitStatusMap<CandidateMap, Candidate, Enumerator>.MaxCount => 9 * 9 * 9;

	/// <inheritdoc/>
	static CandidateMap IMinMaxValue<CandidateMap>.MaxValue => MaxValue;

	/// <inheritdoc/>
	static CandidateMap IMinMaxValue<CandidateMap>.MinValue => MinValue;


	/// <inheritdoc/>
	[IndexerName("CandidateIndex")]
	public readonly Candidate this[int index]
	{
		get
		{
			if (!this)
			{
				return -1;
			}

			var pos = 0;
			for (var i = 0; i < 729; i++)
			{
				if (Contains(i) && pos++ == index)
				{
					return i;
				}
			}

			return -1;
		}
	}


	/// <inheritdoc/>
	public readonly void CopyTo(scoped ref Candidate sequence, int length)
	{
		if (length >= 729)
		{
			Unsafe.CopyBlock(
				ref Ref.AsByteRef(ref sequence),
				in Ref.AsReadOnlyByteRef(in Offsets[0]),
				(uint)(sizeof(Candidate) * length)
			);
		}
	}

	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public readonly bool Contains(Candidate offset) => (_bits[offset >> 6] >> (offset & 63) & 1) != 0;

	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	[ExplicitInterfaceImpl(typeof(IEquatable<>))]
	public readonly bool Equals(scoped ref readonly CandidateMap other) => _bits == other._bits;

	/// <inheritdoc/>
	public readonly int IndexOf(Candidate offset)
	{
		for (var index = 0; index < _count; index++)
		{
			if (this[index] == offset)
			{
				return index;
			}
		}

		return -1;
	}

	/// <inheritdoc/>
	public readonly void ForEach(Action<Candidate> action)
	{
		foreach (var element in this)
		{
			action(element);
		}
	}

	/// <inheritdoc cref="object.GetHashCode"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public override readonly int GetHashCode() => _bits.GetHashCode();

	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public readonly Candidate[] ToArray() => Offsets;

	/// <inheritdoc cref="object.ToString"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public override readonly string ToString() => ToString(GlobalizedConverter.InvariantCultureConverter);

	/// <summary>
	/// Try to get digits that is in the current collection.
	/// </summary>
	/// <param name="cell">The desired cell.</param>
	/// <returns>The digits.</returns>
	public readonly Mask GetDigitsFor(Cell cell)
	{
		var result = (Mask)0;
		for (var (candidate, digit) = (cell * 9, 0); digit < 9; candidate++, digit++)
		{
			if (Contains(candidate))
			{
				result |= (Mask)(1 << digit);
			}
		}

		return result;
	}

	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public readonly string ToString(CultureInfo? culture = null)
		=> ToString(GlobalizedConverter.GetConverter(culture ?? CultureInfo.CurrentUICulture));

	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public readonly string ToString(CoordinateConverter converter) => converter.CandidateConverter(in this);

	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public readonly Enumerator GetEnumerator() => new(Offsets);

	/// <summary>
	/// Try to enumerate cell and digit value on each candidates.
	/// </summary>
	/// <returns>The enumerator instance.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public readonly CellDigitEnumerator EnumerateCellDigit() => new(Offsets);

	/// <inheritdoc/>
	public readonly CandidateMap Slice(int start, int count)
	{
		var result = (CandidateMap)[];
		var offsets = Offsets;
		for (int i = start, end = start + count; i < end; i++)
		{
			result.Add(offsets[i]);
		}

		return result;
	}

	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public readonly unsafe ReadOnlySpan<CandidateMap> GetSubsets(int subsetSize)
	{
		if (subsetSize == 0 || subsetSize > _count)
		{
			return [];
		}

		if (subsetSize == _count)
		{
			return (CandidateMap[])[this];
		}

		var n = _count;
		var buffer = stackalloc int[subsetSize];
		if (n <= 30 && subsetSize <= 30)
		{
			// Optimization: Use table to get the total number of result elements.
			var totalIndex = 0;
			var result = new CandidateMap[PascalTriangle[n - 1][subsetSize - 1]];
			enumerateWithLimit(subsetSize, n, subsetSize, Offsets);
			return result;


			void enumerateWithLimit(int size, int last, int index, Candidate[] offsets)
			{
				for (var i = last; i >= index; i--)
				{
					buffer[index - 1] = i - 1;
					if (index > 1)
					{
						enumerateWithLimit(size, i - 1, index - 1, offsets);
					}
					else
					{
						var temp = new Candidate[size];
						for (var j = 0; j < size; j++)
						{
							temp[j] = offsets[buffer[j]];
						}

						result[totalIndex++] = [.. temp];
					}
				}
			}
		}
		else
		{
			if (n > 30 && subsetSize > 30)
			{
				throw new NotSupportedException(ResourceDictionary.ExceptionMessage("SubsetsExceeded"));
			}
			var result = new List<CandidateMap>();
			enumerateWithoutLimit(subsetSize, n, subsetSize, Offsets);
			return result.AsReadOnlySpan();


			void enumerateWithoutLimit(int size, int last, int index, Candidate[] offsets)
			{
				for (var i = last; i >= index; i--)
				{
					buffer[index - 1] = i - 1;
					if (index > 1)
					{
						enumerateWithoutLimit(size, i - 1, index - 1, offsets);
					}
					else
					{
						var temp = new Candidate[size];
						for (var j = 0; j < size; j++)
						{
							temp[j] = offsets[buffer[j]];
						}

						result.AddRef([.. temp]);
					}
				}
			}
		}
	}

	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public readonly ReadOnlySpan<CandidateMap> GetSubsetsAll() => GetSubsetsAllBelow(_count);

	/// <inheritdoc/>
	public readonly ReadOnlySpan<CandidateMap> GetSubsetsAllBelow(int limitSubsetSize)
	{
		if (limitSubsetSize == 0 || !this)
		{
			return [];
		}

		var (n, desiredSize) = (_count, 0);
		var length = Math.Min(n, limitSubsetSize);
		for (var i = 1; i <= length; i++)
		{
			desiredSize += PascalTriangle[n - 1][i - 1];
		}

		var result = new List<CandidateMap>(desiredSize);
		for (var i = 1; i <= length; i++)
		{
			result.AddRangeRef(GetSubsets(i));
		}

		return result.AsReadOnlySpan();
	}

	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public readonly CandidateMap RandomSelect(int count)
	{
		var result = Offsets[..];
		Random.Shared.Shuffle(result);
		return [.. result[..count]];
	}

	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool Add(Candidate offset)
	{
		scoped ref var v = ref _bits[offset >> 6];
		var older = Contains(offset);
		v |= 1L << (offset & 63);
		if (!older)
		{
			_count++;
			return true;
		}

		return false;
	}

	/// <inheritdoc/>
	public int AddRange(scoped ReadOnlySpan<Candidate> offsets)
	{
		var result = 0;
		foreach (var offset in offsets)
		{
			if (Add(offset))
			{
				result++;
			}
		}

		return result;
	}

	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool Remove(Candidate offset)
	{
		scoped ref var v = ref _bits[offset >> 6];
		var older = Contains(offset);
		v &= ~(1L << (offset & 63));
		if (older)
		{
			_count--;
			return true;
		}

		return false;
	}

	/// <inheritdoc/>
	public int RemoveRange(scoped ReadOnlySpan<Candidate> offsets)
	{
		var result = 0;
		foreach (var offset in offsets)
		{
			if (Remove(offset))
			{
				result++;
			}
		}

		return result;
	}

	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void Clear() => this = default;

	/// <inheritdoc/>
	void IBitStatusMap<CandidateMap, Candidate, Enumerator>.ExceptWith(IEnumerable<Candidate> other)
	{
		foreach (var element in other)
		{
			Remove(element);
		}
	}

	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	void IBitStatusMap<CandidateMap, Candidate, Enumerator>.IntersectWith(IEnumerable<Candidate> other) => this &= [.. other];

	/// <inheritdoc/>
	void IBitStatusMap<CandidateMap, Candidate, Enumerator>.SymmetricExceptWith(IEnumerable<Candidate> other)
	{
		var left = this;
		foreach (var element in other)
		{
			left.Remove(element);
		}

		var right = [.. other] - this;
		this = left | right;
	}

	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	void IBitStatusMap<CandidateMap, Candidate, Enumerator>.UnionWith(IEnumerable<Candidate> other) => this |= [.. other];


	/// <inheritdoc/>
	public static bool TryParse(string str, out CandidateMap result)
	{
		try
		{
			result = Parse(str);
			return true;
		}
		catch (FormatException)
		{
			Unsafe.SkipInit(out result);
			return false;
		}
	}

	/// <summary>
	/// Creates a <see cref="CandidateMap"/> instance via the specified candidates.
	/// </summary>
	/// <param name="candidates">The candidates.</param>
	/// <returns>A <see cref="CandidateMap"/> instance.</returns>
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static CandidateMap Create(scoped ReadOnlySpan<Candidate> candidates)
	{
		if (candidates.IsEmpty)
		{
			return default;
		}

		var result = default(CandidateMap);
		foreach (var candidate in candidates)
		{
			result.Add(candidate);
		}
		return result;
	}

	/// <inheritdoc/>
	public static CandidateMap Parse(string str)
	{
		foreach (var parser in
			from element in Enum.GetValues<CoordinateType>()
			let parser = element.GetParser()
			where parser is not null
			select parser)
		{
			if (parser.CandidateParser(str) is { Count: not 0 } result)
			{
				return result;
			}
		}

		throw new FormatException(ResourceDictionary.ExceptionMessage("StringValueInvalidToBeParsed"));
	}

	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static CandidateMap ParseExact(string str, CoordinateParser parser) => parser.CandidateParser(str);

	/// <inheritdoc/>
	static bool IParsable<CandidateMap>.TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, out CandidateMap result)
	{
		try
		{
			if (s is null)
			{
				goto ReturnFalse;
			}

			return TryParse(s, out result);
		}
		catch
		{
		}

	ReturnFalse:
		Unsafe.SkipInit(out result);
		return false;
	}


	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool operator !(scoped in CandidateMap offsets) => offsets._count == 0;

	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool operator true(scoped in CandidateMap value) => value._count != 0;

	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool operator false(scoped in CandidateMap value) => value._count == 0;

	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static CandidateMap operator ~(scoped in CandidateMap offsets)
	{
		var result = offsets;
		result._bits[0] = ~result._bits[0];
		result._bits[1] = ~result._bits[1];
		result._bits[2] = ~result._bits[2];
		result._bits[3] = ~result._bits[3];
		result._bits[4] = ~result._bits[4];
		result._bits[5] = ~result._bits[5];
		result._bits[6] = ~result._bits[6];
		result._bits[7] = ~result._bits[7];
		result._bits[8] = ~result._bits[8];
		result._bits[9] = ~result._bits[9];
		result._bits[10] = ~result._bits[10];
		result._bits[11] = ~result._bits[11] & 0x1FFFFFF;

		result._count = 729 - offsets._count;
		return result;
	}

	/// <inheritdoc cref="IDivisionOperators{TSelf, TOther, TResult}.op_Division(TSelf, TOther)"/>
	[ExplicitInterfaceImpl(typeof(IDivisionOperators<,,>))]
	public static CellMap operator /(scoped in CandidateMap offsets, Digit digit)
	{
		var result = (CellMap)[];
		foreach (var element in offsets)
		{
			if (element % 9 == digit)
			{
				result.Add(element / 9);
			}
		}

		return result;
	}

	/// <inheritdoc/>
	public static CandidateMap operator +(scoped in CandidateMap collection, Candidate offset)
	{
		var result = collection;
		result.Add(offset);

		return result;
	}

	/// <inheritdoc/>
	public static CandidateMap operator -(scoped in CandidateMap collection, Candidate offset)
	{
		var result = collection;
		result.Remove(offset);

		return result;
	}

	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static CandidateMap operator &(scoped in CandidateMap left, scoped in CandidateMap right)
	{
		var finalCount = 0;
		var result = left;
		finalCount += PopCount((ulong)(result._bits[0] &= right._bits[0]));
		finalCount += PopCount((ulong)(result._bits[1] &= right._bits[1]));
		finalCount += PopCount((ulong)(result._bits[2] &= right._bits[2]));
		finalCount += PopCount((ulong)(result._bits[3] &= right._bits[3]));
		finalCount += PopCount((ulong)(result._bits[4] &= right._bits[4]));
		finalCount += PopCount((ulong)(result._bits[5] &= right._bits[5]));
		finalCount += PopCount((ulong)(result._bits[6] &= right._bits[6]));
		finalCount += PopCount((ulong)(result._bits[7] &= right._bits[7]));
		finalCount += PopCount((ulong)(result._bits[8] &= right._bits[8]));
		finalCount += PopCount((ulong)(result._bits[9] &= right._bits[9]));
		finalCount += PopCount((ulong)(result._bits[10] &= right._bits[10]));
		finalCount += PopCount((ulong)(result._bits[11] &= right._bits[11]));

		result._count = finalCount;
		return result;
	}

	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static CandidateMap operator |(scoped in CandidateMap left, scoped in CandidateMap right)
	{
		var finalCount = 0;
		var result = left;
		finalCount += PopCount((ulong)(result._bits[0] |= right._bits[0]));
		finalCount += PopCount((ulong)(result._bits[1] |= right._bits[1]));
		finalCount += PopCount((ulong)(result._bits[2] |= right._bits[2]));
		finalCount += PopCount((ulong)(result._bits[3] |= right._bits[3]));
		finalCount += PopCount((ulong)(result._bits[4] |= right._bits[4]));
		finalCount += PopCount((ulong)(result._bits[5] |= right._bits[5]));
		finalCount += PopCount((ulong)(result._bits[6] |= right._bits[6]));
		finalCount += PopCount((ulong)(result._bits[7] |= right._bits[7]));
		finalCount += PopCount((ulong)(result._bits[8] |= right._bits[8]));
		finalCount += PopCount((ulong)(result._bits[9] |= right._bits[9]));
		finalCount += PopCount((ulong)(result._bits[10] |= right._bits[10]));
		finalCount += PopCount((ulong)(result._bits[11] |= right._bits[11]));

		result._count = finalCount;
		return result;
	}

	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static CandidateMap operator ^(scoped in CandidateMap left, scoped in CandidateMap right)
	{
		var finalCount = 0;
		var result = left;
		finalCount += PopCount((ulong)(result._bits[0] ^= right._bits[0]));
		finalCount += PopCount((ulong)(result._bits[1] ^= right._bits[1]));
		finalCount += PopCount((ulong)(result._bits[2] ^= right._bits[2]));
		finalCount += PopCount((ulong)(result._bits[3] ^= right._bits[3]));
		finalCount += PopCount((ulong)(result._bits[4] ^= right._bits[4]));
		finalCount += PopCount((ulong)(result._bits[5] ^= right._bits[5]));
		finalCount += PopCount((ulong)(result._bits[6] ^= right._bits[6]));
		finalCount += PopCount((ulong)(result._bits[7] ^= right._bits[7]));
		finalCount += PopCount((ulong)(result._bits[8] ^= right._bits[8]));
		finalCount += PopCount((ulong)(result._bits[9] ^= right._bits[9]));
		finalCount += PopCount((ulong)(result._bits[10] ^= right._bits[10]));
		finalCount += PopCount((ulong)(result._bits[11] ^= right._bits[11]));

		result._count = finalCount;
		return result;
	}

	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static CandidateMap operator -(scoped in CandidateMap left, scoped in CandidateMap right)
	{
		var finalCount = 0;
		var result = left;
		finalCount += PopCount((ulong)(result._bits[0] &= ~right._bits[0]));
		finalCount += PopCount((ulong)(result._bits[1] &= ~right._bits[1]));
		finalCount += PopCount((ulong)(result._bits[2] &= ~right._bits[2]));
		finalCount += PopCount((ulong)(result._bits[3] &= ~right._bits[3]));
		finalCount += PopCount((ulong)(result._bits[4] &= ~right._bits[4]));
		finalCount += PopCount((ulong)(result._bits[5] &= ~right._bits[5]));
		finalCount += PopCount((ulong)(result._bits[6] &= ~right._bits[6]));
		finalCount += PopCount((ulong)(result._bits[7] &= ~right._bits[7]));
		finalCount += PopCount((ulong)(result._bits[8] &= ~right._bits[8]));
		finalCount += PopCount((ulong)(result._bits[9] &= ~right._bits[9]));
		finalCount += PopCount((ulong)(result._bits[10] &= ~right._bits[10]));
		finalCount += PopCount((ulong)(result._bits[11] &= ~right._bits[11]));

		result._count = finalCount;
		return result;
	}

	/// <summary>
	/// Expands the operator to <c><![CDATA[(a & b).PeerIntersection & b]]></c>.
	/// </summary>
	/// <param name="base">The base map.</param>
	/// <param name="template">The template map that the base map to check and cover.</param>
	/// <returns>The result map.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static CandidateMap operator %(scoped in CandidateMap @base, scoped in CandidateMap template)
		=> (@base & template).PeerIntersection & template;

	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	static CandidateMap IAdditionOperators<CandidateMap, Candidate, CandidateMap>.operator +(CandidateMap left, Candidate right)
		=> left + right;

	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	static CandidateMap ISubtractionOperators<CandidateMap, Candidate, CandidateMap>.operator -(CandidateMap left, Candidate right)
		=> left - right;


	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static explicit operator CandidateMap(Candidate[] offsets) => [.. offsets];

	/// <inheritdoc/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static explicit operator CandidateMap(scoped ReadOnlySpan<Candidate> offsets) => [.. offsets];
}
