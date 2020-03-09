﻿using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Sudoku.Data.Stepping;

namespace Sudoku.Data
{
	/// <summary>
	/// Provides an undoable sudoku grid. This data structure is nearly same
	/// as <see cref="Grid"/>, but only add two methods <see cref="Undo"/>
	/// and <see cref="Redo"/>.
	/// </summary>
	/// <seealso cref="Grid"/>
	public sealed class UndoableGrid : Grid, IEquatable<UndoableGrid>, IUndoable
	{
		/// <summary>
		/// The undo stack.
		/// </summary>
		private readonly Stack<Step> _undoStack = new Stack<Step>();

		/// <summary>
		/// The redo stack.
		/// </summary>
		private readonly Stack<Step> _redoStack = new Stack<Step>();


		/// <inheritdoc/>
		public UndoableGrid(short[] masks) : base(masks)
		{
		}

		/// <summary>
		/// Initializes an instance with the specified grid (to convert to
		/// an undoable grid).
		/// </summary>
		/// <param name="grid">The grid.</param>
		public UndoableGrid(Grid grid) : this((short[])grid._masks.Clone())
		{
		}


		/// <inheritdoc/>
		public override int this[int offset]
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get => base[offset];
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				var step = new AssignmentStep(value, offset, _masks[offset], new GridMap(offset, false));
				_undoStack.Push(step);
				step.DoStepTo(this);
			}
		}

		/// <inheritdoc/>
		public override bool this[int offset, int digit]
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get => base[offset, digit];
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				var step = value
					? (Step)new EliminationStep(digit, offset)
					: new AssignmentStep(digit, offset, _masks[offset], new GridMap(offset, false));
				_undoStack.Push(step);
				step.DoStepTo(this);
			}
		}


		/// <inheritdoc/>
		public override void Fix()
		{
			var map = GridMap.Empty;
			for (int i = 0; i < 81; i++)
			{
				if (GetCellStatus(i) == CellStatus.Modifiable)
				{
					map[i] = true;
				}
			}

			var step = new FixStep(map);
			_undoStack.Push(step);
			step.DoStepTo(this);
		}

		/// <inheritdoc/>
		public override void Unfix()
		{
			var map = GridMap.Empty;
			for (int i = 0; i < 81; i++)
			{
				if (GetCellStatus(i) == CellStatus.Given)
				{
					map[i] = true;
				}
			}

			var step = new UnfixStep(map);
			_undoStack.Push(step);
			step.DoStepTo(this);
		}

		/// <inheritdoc/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override void Reset()
		{
			var step = new ResetStep(_initialMasks, _masks);
			_undoStack.Push(step);
			step.DoStepTo(this);
		}

		/// <inheritdoc/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override void SetCellStatus(int offset, CellStatus cellStatus)
		{
			var step = new SetCellStatusStep(offset, GetCellStatus(offset), cellStatus);
			_undoStack.Push(step);
			step.DoStepTo(this);
		}

		/// <inheritdoc/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override void SetMask(int offset, short value)
		{
			var step = new SetMaskStep(offset, GetMask(offset), value);
			_undoStack.Push(step);
			step.DoStepTo(this);
		}

		/// <inheritdoc/>
		/// <exception cref="InvalidOperationException">
		/// Throws when the redo stack is empty.
		/// </exception>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Redo()
		{
			if (_redoStack.Count == 0)
			{
				throw new InvalidOperationException("The redo stack is already empty.");
			}

			var step = _redoStack.Pop();
			_undoStack.Push(step);
			step.DoStepTo(this);
		}

		/// <inheritdoc/>
		/// <exception cref="InvalidOperationException">
		/// Throws when the undo stack is empty.
		/// </exception>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Undo()
		{
			if (_undoStack.Count == 0)
			{
				throw new InvalidOperationException("The undo stack is already empty.");
			}

			var step = _undoStack.Pop();
			_redoStack.Push(step);
			step.UndoStepTo(this);
		}

		/// <inheritdoc/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override bool Equals(object? obj) => base.Equals(obj);

		/// <inheritdoc/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(UndoableGrid other) => Equals((Grid)other);

		/// <inheritdoc/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode() => base.GetHashCode();


		/// <include file='../GlobalDocComments.xml' path='comments/operator[@name="op_Equality"]'/>
		public static bool operator ==(UndoableGrid left, UndoableGrid right) =>
			left.Equals(right);

		/// <include file='../GlobalDocComments.xml' path='comments/operator[@name="op_Equality"]'/>
		public static bool operator ==(Grid left, UndoableGrid right) =>
			left.Equals(right);
		
		/// <include file='../GlobalDocComments.xml' path='comments/operator[@name="op_Equality"]'/>
		public static bool operator ==(UndoableGrid left, Grid right) =>
			left.Equals(right);

		/// <include file='../GlobalDocComments.xml' path='comments/operator[@name="op_Inequality"]'/>
		public static bool operator !=(UndoableGrid left, UndoableGrid right) =>
			!(left == right);

		/// <include file='../GlobalDocComments.xml' path='comments/operator[@name="op_Inequality"]'/>
		public static bool operator !=(Grid left, UndoableGrid right) =>
			!(left == right);

		/// <include file='../GlobalDocComments.xml' path='comments/operator[@name="op_Inequality"]'/>
		public static bool operator !=(UndoableGrid left, Grid right) =>
			!(left == right);
	}
}
