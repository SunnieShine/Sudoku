namespace SudokuStudio.Drawing;

/// <summary>
/// Defines a factory type that is used for creating a list of <see cref="FrameworkElement"/>
/// to display for highlighted cells, candidates and so on.
/// </summary>
/// <remarks>
/// All created <see cref="FrameworkElement"/> instances will be tagged as a <see cref="string"/>, whose value is "<c>RenderableFactory</c>",
/// in order to be used for distinction with other controls in the collection.
/// </remarks>
/// <seealso cref="FrameworkElement"/>
internal static partial class DrawableFactory
{
	/// <summary>
	/// Refresh the pane view unit controls.
	/// </summary>
	/// <param name="pane">The pane.</param>
	/// <param name="reason">The reason why raising this updating operation.</param>
	/// <param name="value">The value specified as an <see cref="object"/> value.</param>
	public static partial void UpdateViewUnitControls(SudokuPane pane, DrawableItemUpdatingReason reason, object? value)
	{
		if (reason != DrawableItemUpdatingReason.None)
		{
			RemoveViewUnitControls(pane);
			if (pane.ViewUnit is not null)
			{
				AddViewUnitControls(pane, pane.ViewUnit);
			}
		}
	}

	/// <summary>
	/// Removes all possible controls that are used for displaying elements in a <see cref="ViewUnitBindableSource"/>.
	/// </summary>
	/// <param name="pane">The target pane.</param>
	/// <seealso cref="ViewUnitBindableSource"/>
	private static partial void RemoveViewUnitControls(SudokuPane pane)
	{
		foreach (var targetControl in getParentControls(pane))
		{
			if (targetControl is GridLayout { Children: var children })
			{
				children.RemoveAllViewUnitControls();
			}
		}

		// Manually update property.
		pane.ViewUnitUsedCandidates = [];


		static IEnumerable<FrameworkElement> getParentControls(SudokuPane sudokuPane)
		{
			foreach (var children in sudokuPane._children)
			{
				yield return children.MainGrid; // cell, candidate, baba grouping, icons
			}
			yield return sudokuPane.MainGrid; // house, chute, link
		}
	}

	/// <summary>
	/// Adds a list of <see cref="FrameworkElement"/>s that are used for displaying highlight elements in a <see cref="ViewUnitBindableSource"/>.
	/// </summary>
	/// <param name="pane">The target pane.</param>
	/// <param name="viewUnit">The view unit that you want to display.</param>
	/// <seealso cref="FrameworkElement"/>
	/// <seealso cref="ViewUnitBindableSource"/>
	private static partial void AddViewUnitControls(SudokuPane pane, ViewUnitBindableSource viewUnit)
	{
		// Check whether the data can be deconstructed.
		if (viewUnit is not { View: var view, Conclusions: var conclusions })
		{
			return;
		}

		var pencilmarkMode = ((App)Application.Current).Preference.UIPreferences.DisplayCandidates;
		var usedCandidates = CandidateMap.Empty;
		var (controlAddingActions, overlapped, links) = (new AnimatedResultCollection(), new List<Conclusion>(), new List<ILinkViewNode>());

		// Iterate on each view node, and get their own corresponding controls.
		foreach (var viewNode in view)
		{
			switch (viewNode)
			{
				case CellViewNode c:
				{
					ForCellNode(pane, c, controlAddingActions);
					break;
				}
				case IconViewNode i:
				{
					ForIconNode(pane, i, controlAddingActions);
					break;
				}
				case CandidateViewNode(_, var candidate) c:
				{
					ForCandidateNode(pane, c, conclusions, out var o, controlAddingActions);
					if (o is { } currentOverlappedConclusion)
					{
						overlapped.Add(currentOverlappedConclusion);
					}

					usedCandidates.Add(candidate);
					break;
				}
				case HouseViewNode h:
				{
					ForHouseNode(pane, h, controlAddingActions);
					break;
				}
				case ChuteViewNode c:
				{
					ForChuteNode(pane, c, controlAddingActions);
					break;
				}
				case BabaGroupViewNode b:
				{
					ForBabaGroupNode(pane, b, controlAddingActions);
					break;
				}
				case ILinkViewNode l:
				{
					links.Add(l);
					break;
				}
			}
		}

		// Then iterate on each conclusions. Those conclusions will also be rendered as real controls.
		foreach (var conclusion in conclusions)
		{
			ForConclusion(pane, conclusion, overlapped, controlAddingActions);

			usedCandidates.Add(conclusion.Candidate);
		}

		// Finally, iterate on links.
		// The links are special to be handled - they will create a list of line controls.
		// We should handle it at last.
		ForLinkNodes(pane, links.AsReadOnlySpan(), conclusions, controlAddingActions);

		foreach (var (animator, adder) in controlAddingActions)
		{
			(animator + adder)();
		}

		// Update property to get highlighted candidates.
		pane.ViewUnitUsedCandidates = usedCandidates;
	}


	/// <summary>
	/// The internal helper method that creates a <see cref="InvalidOperationException"/> instance without any other operation.
	/// </summary>
	/// <typeparam name="T">The type of the return value if the exception were not thrown.</typeparam>
	/// <param name="o">The object.</param>
	/// <param name="range">The range of the argument should be.</param>
	/// <param name="s">The caller expression for argument <paramref name="o"/>.</param>
	/// <returns><typeparamref name="T"/> instance. The value is unnecessary because an exception will be thrown.</returns>
	/// <exception cref="InvalidOperationException">Always throws.</exception>
	[DoesNotReturn]
	private static T? Throw<T>(object? o, int range, [CallerArgumentExpression(nameof(o))] string? s = null)
		where T : allows ref struct
		=> throw new InvalidOperationException($"The {s} index configured is invalid - it must be between 0 and {range}.");


	public static partial void UpdateViewUnitControls(SudokuPane pane, DrawableItemUpdatingReason reason, object? value = null);
	private static partial void RemoveViewUnitControls(SudokuPane pane);
	private static partial void AddViewUnitControls(SudokuPane pane, ViewUnitBindableSource viewUnit);
	private static partial void ForConclusion(SudokuPane sudokuPane, Conclusion conclusion, List<Conclusion> overlapped, AnimatedResultCollection animatedResults);
	private static partial void ForCellNode(SudokuPane sudokuPane, CellViewNode cellNode, AnimatedResultCollection animatedResults);
	private static partial void ForIconNode(SudokuPane sudokuPane, IconViewNode iconNode, AnimatedResultCollection animatedResults);
	private static partial void ForCandidateNode(SudokuPane sudokuPane, CandidateViewNode candidateNode, Conclusion[] conclusions, out Conclusion? overlapped, AnimatedResultCollection animatedResults);
	private static partial void ForCandidateNodeCore(ColorIdentifier id, Color color, Candidate candidate, SudokuPaneCell paneCellControl, AnimatedResultCollection animatedResults, bool isForConclusion = false, bool isForElimination = false, bool isOverlapped = false);
	private static partial void ForHouseNode(SudokuPane sudokuPane, HouseViewNode houseNode, AnimatedResultCollection animatedResults);
	private static partial void ForChuteNode(SudokuPane sudokuPane, ChuteViewNode chuteNode, AnimatedResultCollection animatedResults);
	private static partial void ForBabaGroupNode(SudokuPane sudokuPane, BabaGroupViewNode babaGroupNode, AnimatedResultCollection animatedResults);
	private static partial void ForLinkNodes(SudokuPane sudokuPane, ReadOnlySpan<ILinkViewNode> linkNodes, Conclusion[] conclusions, AnimatedResultCollection animatedResults);
}

/// <include file='../../global-doc-comments.xml' path='g/csharp11/feature[@name="file-local"]/target[@name="class" and @when="extension"]'/>
file static class Extensions
{
	/// <summary>
	/// Removes all possible <see cref="FrameworkElement"/>s that is used for displaying elements in a <see cref="ViewUnitBindableSource"/>.
	/// </summary>
	/// <param name="this">The collection.</param>
	public static void RemoveAllViewUnitControls(this UIElementCollection @this)
	{
		var controls = (
			from control in @this.OfType<FrameworkElement>()
			where control.Tag is nameof(DrawableFactory)
			select control
		).ToArray();
		foreach (var control in controls)
		{
			@this.Remove(control);
		}
	}
}
