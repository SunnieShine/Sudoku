namespace Sudoku.Drawing;

public partial class GridCanvas
{
	public partial void DrawEliminations(ReadOnlySpan<Conclusion> conclusions, ReadOnlySpan<CandidateViewNode> nodes)
	{
		if (_settings is not { EliminationColor: var eColor, CannibalismColor: var cColor })
		{
			return;
		}

		using var elimBrush = new SolidBrush(eColor);
		using var cannibalBrush = new SolidBrush(cColor);
		using var elimBrushLighter = new SolidBrush(eColor.QuarterAlpha());
		using var cannibalismBrushLighter = new SolidBrush(cColor.QuarterAlpha());
		foreach (var (t, c, d) in conclusions)
		{
			if (t != Elimination)
			{
				continue;
			}

			var cannibalism = false;
			if (nodes.Length == 0)
			{
				goto Drawing;
			}

			foreach (var node in nodes)
			{
				if (node.Candidate == c * 9 + d)
				{
					cannibalism = true;
					break;
				}
			}

		Drawing:
			_g.FillEllipse(
				(cannibalism, nodes.Any((ref readonly CandidateViewNode node) => node.Cell == c)) switch
				{
					(true, true) => cannibalismBrushLighter,
					(true, false) => cannibalBrush,
					(false, true) => elimBrushLighter,
					_ => elimBrush
				},
				_calculator.GetMouseRectangle(c, d)
			);
		}
	}
}
