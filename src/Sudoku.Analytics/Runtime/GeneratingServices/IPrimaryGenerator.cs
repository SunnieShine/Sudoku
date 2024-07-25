namespace Sudoku.Runtime.GeneratingServices;

/// <summary>
/// Represents a generator type that creates a puzzle that can only uses the current technique to solve.
/// </summary>
public interface IPrimaryGenerator
{
	/// <summary>
	/// Generates a puzzle and return a <see cref="Grid"/> instance that can be solved by only using the specified technique;
	/// using <paramref name="cancellationToken"/> to cancel the operation.
	/// </summary>
	/// <param name="cancellationToken">The cancellation token instance that can cancel the current operation.</param>
	/// <returns>The result <see cref="Grid"/> instance generated.</returns>
	public abstract Grid GeneratePrimary(CancellationToken cancellationToken = default);
}
