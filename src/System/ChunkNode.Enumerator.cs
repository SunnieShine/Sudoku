namespace System;

public partial struct ChunkNode<T>
{
	/// <summary>
	/// Indicates the enumerator of current type.
	/// </summary>
	/// <param name="value">The value.</param>
	public ref struct Enumerator(ref readonly ChunkNode<T> value) : IEnumerator<T>
	{
		/// <summary>
		/// Indicates the values.
		/// </summary>
		private readonly ReadOnlySpan<T> _values =
			value.Type switch
			{
				ChunkNodeType.Value => (T[])[value.Value!],
				ChunkNodeType.Array => (T[])value.ValueRef!,
				_ => ((List<T>)value.ValueRef!).AsReadOnlySpan()
			};

		/// <summary>
		/// Indicates the index.
		/// </summary>
		private int _index = -1;


		/// <inheritdoc cref="IEnumerator.Current"/>
		public readonly ref readonly T Current => ref _values[_index];

		/// <inheritdoc/>
		readonly object? IEnumerator.Current => Current;

		/// <inheritdoc/>
		readonly T IEnumerator<T>.Current => Current;


		/// <inheritdoc cref="IEnumerator.MoveNext"/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool MoveNext() => ++_index < _values.Length;

		/// <inheritdoc/>
		readonly void IDisposable.Dispose() { }

		/// <inheritdoc/>
		[DoesNotReturn]
		readonly void IEnumerator.Reset() => throw new NotImplementedException();
	}
}
