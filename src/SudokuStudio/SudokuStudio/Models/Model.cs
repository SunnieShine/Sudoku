﻿namespace SudokuStudio.Models;

/// <summary>
/// Represents a model.
/// </summary>
public abstract class Model : INotifyPropertyChanged
{
	/// <summary>
	/// Initializes an instance derived from current type, with default triggering event handler.
	/// </summary>
	protected Model()
	{
	}


	/// <inheritdoc/>
	public event PropertyChangedEventHandler? PropertyChanged;


	/// <summary>
	/// Try to set the backing field with the specified value, and then automatically triggers the event <see cref="PropertyChanged"/>.
	/// </summary>
	/// <typeparam name="T">The type of the property and the backing field.</typeparam>
	/// <param name="field">
	/// The reference of the backing field. <see langword="ref"/> keyword is required because we may update the inner value of the field.
	/// </param>
	/// <param name="value">The value to replace the original value of the target field.</param>
	/// <param name="equalityComparer">
	/// <para>
	/// An equality comparer that determines whether the backing field should be updated or not.
	/// If this method returns <see langword="true"/>, we should skip the assignment and not trigger the event
	/// because the value is same as original one. The first argument stands for the original field value, and the second
	/// argument stands for the newer value to replace. For example:
	/// <code><![CDATA[
	/// static (field, value) => field == value
	/// ]]></code>
	/// </para>
	/// <para>
	/// This argument can be <see langword="null"/>, it will be replaced with the method <see cref="EqualityComparer{T}.Equals(T, T)"/>.
	/// </para>
	/// </param>
	/// <param name="extraCheck">
	/// <para>
	/// Indicates the extra checking method. If <see langword="null"/>, no extra checking will be applied;
	/// otherwise, if newer value is not equal to original one, but this argument fails to be checked,
	/// the target field will still not be replaced with the newer value. For example:
	/// <code><![CDATA[
	/// static value => value >= 0
	/// ]]></code>
	/// </para>
	/// <para>The default value is <see langword="null"/>.</para>
	/// </param>
	/// <param name="propertyName">
	/// <inheritdoc cref="OnPropertyChanged(string?)" path="/param[@name='propertyName']"/>
	/// </param>
	/// <exception cref="ArgumentOutOfRangeException">Throws when <paramref name="extraCheck"/> is failed to check the newer value.</exception>
	/// <seealso cref="PropertyChanged"/>
	[DebuggerStepThrough]
	protected void SetBackingField<T>(
		ref T field, T value,
		Func<T, T, bool>? equalityComparer = null,
		Predicate<T>? extraCheck = null,
		[CallerMemberName] string? propertyName = null
	)
	{
		equalityComparer ??= EqualityComparer<T>.Default.Equals;
		extraCheck ??= static _ => true;

		if (!equalityComparer(field, value))
		{
			if (!extraCheck(value))
			{
				throw new ArgumentOutOfRangeException(nameof(value));
			}

			field = value;

			OnPropertyChanged(propertyName);
		}
	}

	/// <summary>
	/// The default behavior to trigger event <see cref="PropertyChanged"/> event.
	/// </summary>
	/// <param name="propertyName">
	/// Indicates the property that triggers the event, represented as its name.
	/// You can just keep this argument be <see langword="null"/> value; this value will be automatically generated by runtime.
	/// </param>
	[DebuggerStepThrough]
	protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
		=> PropertyChanged?.Invoke(this, new(propertyName));
}
