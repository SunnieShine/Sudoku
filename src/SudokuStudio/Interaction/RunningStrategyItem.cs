namespace SudokuStudio.Interaction;

/// <summary>
/// Represents a type that describes for a running strategy item.
/// </summary>
/// <param name="titleKey">Indicates the title key in resource.</param>
/// <param name="updater">The updater that can assign new value to the target place.</param>
public sealed partial class RunningStrategyItem(
	[PrimaryCosntructorParameter(SetterExpression = "set")] string titleKey,
	[PrimaryCosntructorParameter(SetterExpression = "set")] RunningStrategyUpdater updater
);
