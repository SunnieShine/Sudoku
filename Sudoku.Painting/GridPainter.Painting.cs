﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Extensions;
using System.Runtime.CompilerServices;
using Sudoku.Data;
using Sudoku.Models;
using Sudoku.Painting.Extensions;

namespace Sudoku.Painting
{
	partial record GridPainter
	{
		/// <summary>
		/// The square root of 2.
		/// </summary>
		private const double SqrtOf2 = 1.41421356;

		/// <summary>
		/// The rotate angle (45 degrees, i.e. <c><see cref="Math.PI"/> / 4</c>).
		/// This field is used for rotate the chains if some of them are overlapped.
		/// </summary>
		/// <seealso cref="Math.PI"/>
		private const double RotateAngle = .78539816;


		partial void PaintBackground(Graphics g) => g.Clear(Theme.BackgroundColor);

		partial void PaintGridAndBlockLines(Graphics g)
		{
			using var pg = new Pen(Theme.GridLineColor, Preferences.GridLineWidth);
			using var pb = new Pen(Theme.BlockLineColor, Preferences.BlockLineWidth);
			var gridPoints = Translator.GridPoints;
			for (int i = 0; i < 28; i += 3)
			{
				g.DrawLine(pg, gridPoints[i, 0], gridPoints[i, 27]);
				g.DrawLine(pg, gridPoints[0, i], gridPoints[27, i]);
			}

			for (int i = 0; i < 28; i += 9)
			{
				g.DrawLine(pb, gridPoints[i, 0], gridPoints[i, 27]);
				g.DrawLine(pb, gridPoints[0, i], gridPoints[27, i]);
			}
		}

		partial void PaintPresentationData(Graphics g, PresentationData? view, float offset)
		{
			if (view is null)
			{
				return;
			}

			PaintRegions(g, view.Regions, offset);
			PaintCells(g, view.Cells);
			PaintCandidates(g, view.Candidates, offset);
			PaintLinks(g, view.Links, offset);
			PaintDirectLines(g, view.DirectLines, offset);
		}

		partial void PaintFocusedCells(Graphics g)
		{
			if (FocusedCells.IsEmpty)
			{
				return;
			}

			using var b = new SolidBrush(Theme.FocusedCellsColor);
			foreach (int cell in FocusedCells)
			{
				g.FillRectangle(b, Translator.GetMouseRectangleViaCell(cell));
			}
		}

		partial void PaintEliminations(Graphics g, float offset)
		{
			if (Conclusions is null)
			{
				return;
			}

			using var eliminationBrush = new SolidBrush(Theme.EliminationColor);
			using var cannibalBrush = new SolidBrush(Theme.CannibalismColor);
			foreach (var (t, c, d) in Conclusions)
			{
				if (t != ConclusionType.Elimination)
				{
					continue;
				}

				bool isCannibalism = false;
				if (View is not { Candidates: not null })
				{
					goto Drawing;
				}

				foreach (var (_, value) in View.Candidates)
				{
					if (value == c * 9 + d)
					{
						isCannibalism = true;
						break;
					}
				}

			Drawing:
				g.FillEllipse(
					isCannibalism ? cannibalBrush : eliminationBrush,
					Translator.GetMouseRectangleViaCandidate(c, d).Zoom(-offset / 3));
			}
		}

		partial void PaintValues(Graphics g)
		{
			float cellWidth = Translator.CellSize.Width;
			float candidateWidth = Translator.CandidateSize.Width;
			float vOffsetValue = cellWidth / 9; // The vertical offset of rendering each value.
			float vOffsetCandidate = candidateWidth / 9; // The vertical offset of rendering each candidate.
			float halfWidth = cellWidth / 2;

			using var bGiven = new SolidBrush(Theme.GivenColor);
			using var bModifiable = new SolidBrush(Theme.ModifiableColor);
			using var bCandidate = new SolidBrush(Theme.CandidateColor);
			using var fGiven = GetFont(Preferences.GivenFontName, halfWidth, Preferences.ValueScale);
			using var fModifiable = GetFont(Preferences.ModifiableFontName, halfWidth, Preferences.ValueScale);
			using var fCandidate = GetFont(Preferences.CandidateFontName, halfWidth, Preferences.CandidateScale);
			using var sf = new StringFormat
			{
				Alignment = StringAlignment.Center,
				LineAlignment = StringAlignment.Center
			};

			for (int cell = 0; cell < 81; cell++)
			{
				short mask = Grid.GetMask(cell);
				var status = SudokuGrid.MaskGetStatus(mask);
				switch (status)
				{
					case CellStatus.Empty when Preferences.ShowCandidates:
					{
						// Draw candidates.
						short candidateMask = (short)(mask & SudokuGrid.MaxCandidatesMask);
						foreach (int digit in candidateMask)
						{
							var point = Translator.GetMouseCenter(cell, digit);
							point.Y += vOffsetCandidate;
							g.DrawString((digit + 1).ToString(), fCandidate, bCandidate, point, sf);
						}

						break;
					}
					case CellStatus.Modifiable:
					case CellStatus.Given:
					{
						// Draw values.
						var point = Translator.GetMouseCenter(cell);
						point.Y += vOffsetValue;
						g.DrawString(
							(Grid[cell] + 1).ToString(), status == CellStatus.Given ? fGiven : fModifiable,
							status == CellStatus.Given ? bGiven : bModifiable, point, sf);

						break;
					}
				}
			}
		}

		partial void PaintCells(Graphics g, IEnumerable<DrawingInfo>? cells)
		{
			if (cells is null)
			{
				return;
			}

			foreach (var (id, cell) in cells)
			{
				if (ColorId.IsColorId(id, out byte aWeight, out byte rWeight, out byte gWeight, out byte bWeight))
				{
					var (cw, ch) = Translator.CellSize;
					var (x, y) = Translator.GetMouseCenter(cell);
					using var brush = new SolidBrush(Color.FromArgb(aWeight, rWeight, gWeight, bWeight));
					g.FillRectangle(brush, Translator.GetMouseRectangleViaCell(cell)/*.Zoom(-offset)*/);
				}
				else if (Theme.TryGetPaletteColor(id, out var color))
				{
					var (cw, ch) = Translator.CellSize;
					var (x, y) = Translator.GetMouseCenter(cell);
					using var brush = new SolidBrush(Color.FromArgb(64, color));
					g.FillRectangle(brush, Translator.GetMouseRectangleViaCell(cell)/*.Zoom(-offset)*/);
				}
			}
		}

		partial void PaintCandidates(Graphics g, IEnumerable<DrawingInfo>? candidates, float offset)
		{
			if (candidates is null)
			{
				return;
			}

			// The offset values.
			// 'vOffsetCandidate': The vertical offset of rendering each candidate.
			float cellWidth = Translator.CellSize.Width, candidateWidth = Translator.CandidateSize.Width;
			float halfWidth = cellWidth / 2, vOffsetCandidate = candidateWidth / 9;

			using var bCandidate = new SolidBrush(Theme.CandidateColor);
			using var fCandidate = GetFont(Preferences.CandidateFontName, halfWidth, Preferences.CandidateScale);
			using var sf = new StringFormat
			{
				Alignment = StringAlignment.Center,
				LineAlignment = StringAlignment.Center
			};

			foreach (var (id, candidate) in candidates)
			{
				bool isOverlapped = false;
				if (Conclusions is null)
				{
					goto IsOverlapped;
				}

				foreach (var (concType, concCandidate) in Conclusions)
				{
					if (concType == ConclusionType.Elimination && concCandidate == candidate)
					{
						isOverlapped = true;
						break;
					}
				}

			IsOverlapped:
				if (!isOverlapped)
				{
					int cell = candidate / 9, digit = candidate % 9;
					if (ColorId.IsColorId(id, out byte aWeight, out byte rWeight, out byte gWeight, out byte bWeight))
					{
						using var brush = new SolidBrush(Color.FromArgb(aWeight, rWeight, gWeight, bWeight));
						g.FillEllipse(brush, Translator.GetMouseRectangleViaCandidate(cell, digit).Zoom(-offset / 3));

						// Candidates should be painted also.
						if (!Preferences.ShowCandidates)
						{
							d(cell, digit, vOffsetCandidate);
						}
					}
					else if (Theme.TryGetPaletteColor(id, out var color))
					{
						// In the normal case, I'll paint these circles.
						using var brush = new SolidBrush(color);
						g.FillEllipse(brush, Translator.GetMouseRectangleViaCandidate(cell, digit).Zoom(-offset / 3));

						// Candidates should be painted also.
						if (!Preferences.ShowCandidates)
						{
							d(cell, digit, vOffsetCandidate);
						}
					}
				}
			}

			if (this is { Preferences: { ShowCandidates: false }, Conclusions: not null })
			{
				foreach (var (type, cell, digit) in Conclusions)
				{
					if (type == ConclusionType.Elimination)
					{
						d(cell, digit, vOffsetCandidate);
					}
				}
			}

			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			void d(int cell, int digit, float vOffsetCandidate)
			{
				var point = Translator.GetMouseCenter(cell, digit);
				point.Y += vOffsetCandidate;
				g.DrawString((digit + 1).ToString(), fCandidate, bCandidate, point, sf);
			}
		}

		partial void PaintRegions(Graphics g, IEnumerable<DrawingInfo>? regions, float offset)
		{
			if (regions is null)
			{
				return;
			}

			foreach (var (id, region) in regions)
			{
				if (ColorId.IsColorId(id, out byte aWeight, out byte rWeight, out byte gWeight, out byte bWeight))
				{
					var color = Color.FromArgb(aWeight, rWeight, gWeight, bWeight);
					var rect = Translator.GetMouseRectangleViaRegion(region).Zoom(-offset / 3);
					using var brush = new SolidBrush(color);
					//using var pen = new Pen(color, 6F);
					//g.DrawRectangle(pen, rect.Truncate());
					g.FillRectangle(brush, rect);
				}
				else if (Theme.TryGetPaletteColor(id, out var color))
				{
					var rect = Translator.GetMouseRectangleViaRegion(region).Zoom(-offset / 3);
					using var brush = new SolidBrush(Color.FromArgb(64, color));
					//using var pen = new Pen(color, 6F);
					//g.DrawRectangle(pen, rect.Truncate());
					g.FillRectangle(brush, rect);
				}
			}
		}

		partial void PaintLinks(Graphics g, IEnumerable<Link>? links, float offset)
		{
			if (links is null)
			{
				return;
			}

			// Gather all points used.
			var points = new HashSet<PointF>();
			foreach (var (startCand, endCand) in links)
			{
				Candidates map1 = new() { startCand }, map2 = new() { endCand };

				points.Add(Translator.GetMouseCenter(map1));
				points.Add(Translator.GetMouseCenter(map2));
			}

			if (Conclusions is not null)
			{
				foreach (var conclusion in Conclusions)
				{
					points.Add(Translator.GetMouseCenter(conclusion.Cell, conclusion.Digit));
				}
			}

			// Iterate on each inference to draw the links and grouped nodes (if so).
			var (cw, ch) = Translator.CandidateSize;
			using var arrowPen = new Pen(Theme.ChainColor, 2F)
			{
				CustomEndCap = new AdjustableArrowCap(cw / 4F, ch / 3F)
			};
			using var linePen = new Pen(Theme.ChainColor, 2F);

#if OBSOLETE
			// This brush is used for drawing grouped nodes.
			using var groupedNodeBrush = new SolidBrush(Color.FromArgb(64, Color.Yellow));
#endif
			foreach (var (start, end, type) in links)
			{
				arrowPen.DashStyle = type switch
				{
					LinkType.Strong => DashStyle.Solid,
					LinkType.Weak => DashStyle.Dot,
					//LinkType.Default => DashStyle.Dash,
					_ => DashStyle.Dash
				};

				var pt1 = Translator.GetMouseCenter(new Candidates() { start });
				var pt2 = Translator.GetMouseCenter(new Candidates() { end });
				var (pt1x, pt1y) = pt1;
				var (pt2x, pt2y) = pt2;

#if OBSOLETE
				// Draw grouped node regions.
				if (startMap.Count != 1)
				{
					g.FillRoundedRectangle(
						groupedNodeBrush,
						Converter.GetMouseRectangleOfCandidates(startFullMap),
						offset);
				}
				if (endMap.Count != 1)
				{
					g.FillRoundedRectangle(
						groupedNodeBrush,
						Converter.GetMouseRectangleOfCandidates(endFullMap),
						offset);
				}
#endif

				var penToDraw = type != LinkType.Line ? arrowPen : linePen;
				if (type == LinkType.Line)
				{
					// Draw the link.
					g.DrawLine(penToDraw, pt1, pt2);
				}
				else
				{
					// If the distance of two points is lower than the one of two adjacent candidates,
					// the link will be emitted to draw because of too narrow.
					double distance = Math.Sqrt((pt1x - pt2x) * (pt1x - pt2x) + (pt1y - pt2y) * (pt1y - pt2y));
					if (distance <= cw * SqrtOf2 + offset || distance <= ch * SqrtOf2 + offset)
					{
						continue;
					}

					// Check if another candidate lies in the direct line.
					double deltaX = pt2x - pt1x, deltaY = pt2y - pt1y;
					double alpha = Math.Atan2(deltaY, deltaX);
					double dx1 = deltaX, dy1 = deltaY;
					bool through = false;
					adjust(pt1, pt2, out var p1, out var p2, alpha, cw, offset);
					foreach (var point in points)
					{
						if (point == pt1 || point == pt2)
						{
							// Self...
							continue;
						}

						double dx2 = point.X - p1.X, dy2 = point.Y - p1.Y;
						if (Math.Sign(dx1) == Math.Sign(dx2) && Math.Sign(dy1) == Math.Sign(dy2)
							&& Math.Abs(dx2) <= Math.Abs(dx1) && Math.Abs(dy2) <= Math.Abs(dy1)
							&& (dx1 == 0 || dy1 == 0 || (dx1 / dy1).NearlyEquals(dx2 / dy2, epsilon: 1E-1)))
						{
							through = true;
							break;
						}
					}

					// Now cut the link.
					cut(ref pt1, ref pt2, offset, cw, ch, pt1x, pt1y, pt2x, pt2y);

					if (through)
					{
						double bezierLength = 20;

						// The end points are rotated 45 degrees
						// (counterclockwise for the start point, clockwise for the end point).
						PointF oldPt1 = new(pt1x, pt1y), oldPt2 = new(pt2x, pt2y);
						rotate(oldPt1, ref pt1, -RotateAngle);
						rotate(oldPt2, ref pt2, RotateAngle);

						double aAlpha = alpha - RotateAngle;
						double bx1 = pt1.X + bezierLength * Math.Cos(aAlpha);
						double by1 = pt1.Y + bezierLength * Math.Sin(aAlpha);

						aAlpha = alpha + RotateAngle;
						double bx2 = pt2.X - bezierLength * Math.Cos(aAlpha);
						double by2 = pt2.Y - bezierLength * Math.Sin(aAlpha);

						g.DrawBezier(
							penToDraw, pt1.X, pt1.Y, (float)bx1, (float)by1, (float)bx2, (float)by2,
							pt2.X, pt2.Y);
					}
					else
					{
						// Draw the link.
						g.DrawLine(penToDraw, pt1, pt2);
					}
				}
			}

			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			static void rotate(in PointF pt1, ref PointF pt2, double angle)
			{
				// Translate 'pt2' to (0, 0).
				pt2.X -= pt1.X;
				pt2.Y -= pt1.Y;

				// Rotate.
				double sinAngle = Math.Sin(angle), cosAngle = Math.Cos(angle);
				double xAct = pt2.X, yAct = pt2.Y;
				pt2.X = (float)(xAct * cosAngle - yAct * sinAngle);
				pt2.Y = (float)(xAct * sinAngle + yAct * cosAngle);

				pt2.X += pt1.X;
				pt2.Y += pt1.Y;
			}

			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			static void adjust(
				in PointF pt1, in PointF pt2, out PointF p1, out PointF p2, double alpha,
				double candidateSize, float offset)
			{
				p1 = pt1;
				p2 = pt2;
				double tempDelta = candidateSize / 2 + offset;
				int px = (int)(tempDelta * Math.Cos(alpha)), py = (int)(tempDelta * Math.Sin(alpha));

				p1.X += px;
				p1.Y += py;
				p2.X -= px;
				p2.Y -= py;
			}

			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			static void cut(
				ref PointF pt1, ref PointF pt2, float offset, float cw, float ch,
				float pt1x, float pt1y, float pt2x, float pt2y)
			{
				float slope = Math.Abs((pt2y - pt1y) / (pt2x - pt1x));
				float x = cw / (float)Math.Sqrt(1 + slope * slope);
				float y = ch * (float)Math.Sqrt(slope * slope / (1 + slope * slope));

				float o = offset / 8;
				if (pt1y > pt2y && pt1x == pt2x) { pt1.Y -= ch / 2 - o; pt2.Y += ch / 2 - o; }
				else if (pt1y < pt2y && pt1x == pt2x) { pt1.Y += ch / 2 - o; pt2.Y -= ch / 2 - o; }
				else if (pt1y == pt2y && pt1x > pt2x) { pt1.X -= cw / 2 - o; pt2.X += cw / 2 - o; }
				else if (pt1y == pt2y && pt1x < pt2x) { pt1.X += cw / 2 - o; pt2.X -= cw / 2 - o; }
				else if (pt1y > pt2y && pt1x > pt2x)
				{
					pt1.X -= x / 2 - o; pt1.Y -= y / 2 - o;
					pt2.X += x / 2 - o; pt2.Y += y / 2 - o;
				}
				else if (pt1y > pt2y && pt1x < pt2x)
				{
					pt1.X += x / 2 - o; pt1.Y -= y / 2 - o;
					pt2.X -= x / 2 - o; pt2.Y += y / 2 - o;
				}
				else if (pt1y < pt2y && pt1x > pt2x)
				{
					pt1.X -= x / 2 - o; pt1.Y += y / 2 - o;
					pt2.X += x / 2 - o; pt2.Y -= y / 2 - o;
				}
				else if (pt1y < pt2y && pt1x < pt2x)
				{
					pt1.X += x / 2 - o; pt1.Y += y / 2 - o;
					pt2.X -= x / 2 - o; pt2.Y -= y / 2 - o;
				}
			}
		}

		partial void PaintDirectLines(Graphics g, IEnumerable<(Cells, Cells)>? directLines, float offset)
		{
			if (directLines is null)
			{
				return;
			}

			if (Preferences.ShowCandidates)
			{
				// Non-direct view (without candidates) don't show this function.
				return;
			}

			foreach (var (start, end) in directLines)
			{
				// Draw start cells (may be a capsule-like shape to block them).
				if (!start.IsEmpty)
				{
					// Step 1: Get the left-up cell and right-down cell to construct a rectangle.
					var p1 = Translator.GetMouseCenter(start[0]) - Translator.CellSize / 2;
					var p2 = Translator.GetMouseCenter(start[^1]) + Translator.CellSize / 2;
					var rect = RectangleEx.FromLeftUpAndRightDown(p1, p2).Zoom(-offset);

					// Step 2: Draw capsule.
					using var pen = new Pen(Theme.CrosshatchingOutlineColor, 3F);
					using var brush = new SolidBrush(Theme.CrosshatchingInnerColor);
					g.DrawEllipse(pen, rect);
					g.FillEllipse(brush, rect);
				}

				// Draw end cells (may be using cross sign to represent the current cell can't fill that digit).
				foreach (int cell in end)
				{
					// Step 1: Get the left-up cell and right-down cell to construct a rectangle.
					var rect = Translator.GetMouseRectangleViaCell(cell).Zoom(-offset * 2);

					// Step 2: Draw cross sign.
					using var pen = new Pen(Theme.CrossSignColor, 5F);
					g.DrawCrossSign(pen, rect);
				}
			}
		}
	}
}
