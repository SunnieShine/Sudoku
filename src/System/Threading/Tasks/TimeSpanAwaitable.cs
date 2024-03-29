namespace System.Threading.Tasks;

/// <summary>
/// Provides with extension methods on <see cref="TimeSpan"/>.
/// </summary>
/// <seealso cref="TimeSpan"/>
public static class TimeSpanAwaitable
{
	/// <summary>
	/// Creates a <see cref="TimeSpanAwaiter"/> instance used for <see langword="await"/> expressions.
	/// </summary>
	/// <param name="this">The time span.</param>
	/// <returns>A <see cref="TimeSpanAwaiter"/> instance.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static TimeSpanAwaiter GetAwaiter(this TimeSpan @this) => new(Task.Delay(@this).GetAwaiter());
}

/// <summary>
/// Represents an awaiter for <see cref="TimeSpan"/> instance.
/// </summary>
/// <param name="awaiter">The base awaiter instance.</param>
/// <seealso cref="TimeSpan"/>
public readonly partial struct TimeSpanAwaiter([PrimaryConstructorParameter(MemberKinds.Field)] TaskAwaiter awaiter) : INotifyCompletion
{
	/// <inheritdoc cref="TaskAwaiter.IsCompleted"/>
	public bool IsCompleted
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		get => _awaiter.IsCompleted;
	}


	/// <inheritdoc cref="TaskAwaiter.GetResult"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void GetResult() => _awaiter.GetResult();

	/// <inheritdoc cref="TaskAwaiter.OnCompleted(Action)"/>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void OnCompleted(Action continuation) => _awaiter.OnCompleted(continuation);
}
