namespace Sudoku.Cli.Options;

/// <summary>
/// Represents an option to be used by a <see cref="Command"/>.
/// </summary>
/// <typeparam name="TSelf">The type of option instance itself.</typeparam>
/// <typeparam name="T">The type of the default value of the current option.</typeparam>
/// <seealso cref="Command"/>
public interface IOption<TSelf, out T> where TSelf : class, IOption<TSelf, T>, new()
{
	/// <summary>
	/// Indicates the description to be used.
	/// </summary>
	static abstract string Description { get; }

	/// <summary>
	/// Indicates the aliases to be used.
	/// </summary>
	static abstract string[] Aliases { get; }

	/// <summary>
	/// Indicates the default value to be used.
	/// </summary>
	static abstract T DefaultValue { get; }


	/// <summary>
	/// Create an <see cref="Option{T}"/> instance.
	/// </summary>
	/// <returns>An <see cref="Option{T}"/> instance.</returns>
	static Option<T> CreateOption() => new(TSelf.Aliases, static () => TSelf.DefaultValue, TSelf.Description);
}
