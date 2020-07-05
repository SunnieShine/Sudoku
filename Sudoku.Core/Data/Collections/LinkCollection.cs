﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Sudoku.Constants;
using Sudoku.Extensions;

namespace Sudoku.Data.Collections
{
	/// <summary>
	/// Provides a collection that contains the chain links.
	/// </summary>
	public readonly ref struct LinkCollection
	{
		/// <summary>
		/// The pointer to point <see cref="_collection"/>.
		/// If the constructor isn't <see cref="LinkCollection(Link)"/>,
		/// the field is keep the value <see cref="IntPtr.Zero"/>.
		/// </summary>
		/// <seealso cref="_collection"/>
		/// <seealso cref="LinkCollection(Link)"/>
		/// <seealso cref="IntPtr.Zero"/>
		private readonly IntPtr _ptr;

		/// <summary>
		/// The internal collection.
		/// </summary>
		private readonly Span<Link> _collection;


		/// <summary>
		/// Initializes an instance with one link.
		/// </summary>
		/// <param name="link">The chain link.</param>
		public unsafe LinkCollection(Link link)
		{
			var tempSpan = new Span<Link>((_ptr = Marshal.AllocHGlobal(sizeof(Link))).ToPointer(), 1);
			tempSpan[0] = link;
			_collection = tempSpan;
		}

		/// <summary>
		/// Initializes an instance with the specified collection.
		/// </summary>
		/// <param name="collection">The collection.</param>
		public LinkCollection(Span<Link> collection) : this() => _collection = collection;

		/// <summary>
		/// Initializes an instance with the specified collection.
		/// </summary>
		/// <param name="collection">The collection.</param>
		public LinkCollection(IEnumerable<Link> collection) : this() => _collection = collection.ToArray().AsSpan();

		/// <summary>
		/// To dispose this link (frees the unmanaged memory).
		/// </summary>
		public void Dispose()
		{
			if (_ptr != IntPtr.Zero)
			{
				Marshal.FreeHGlobal(_ptr);
			}
		}

		/// <include file='../GlobalDocComments.xml' path='comments/method[@name="Equals" and @paramType="object"]'/>
		/// <exception cref="NotSupportedException">Always throws.</exception>
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DoesNotReturn]
		public override bool Equals(object? obj) => throw Throwings.RefStructNotSupported;

		/// <include file='../GlobalDocComments.xml' path='comments/method[@name="Equals" and @paramType="__any"]'/>
		public bool Equals(LinkCollection other) => _collection == other._collection;

		/// <include file='../GlobalDocComments.xml' path='comments/method[@name="GetHashCode"]'/>
		/// <exception cref="NotSupportedException">Always throws.</exception>
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DoesNotReturn]
		public override int GetHashCode() => throw Throwings.RefStructNotSupported;

		/// <include file='../GlobalDocComments.xml' path='comments/method[@name="ToString" and @paramType="__noparam"]'/>
		public override string ToString()
		{
			switch (_collection.Length)
			{
				case 0:
				{
					return string.Empty;
				}
				case 1:
				{
					return _collection[0].ToString();
				}
				default:
				{
					var links = _collection.ToArray();
					var sb = new StringBuilder();
					for (int i = 0, length = links.Length; i < length; i++)
					{
						var (start, _, type) = links[i];
						sb
							.Append(new CandidateCollection(start).ToString())
							.Append(NameAttribute.GetName(type));
					}

					return sb.Append(new CandidateCollection(links[^1].EndCandidate).ToString()).ToString();
				}
			}
		}


		/// <include file='../GlobalDocComments.xml' path='comments/operator[@name="op_Equality"]'/>
		public static bool operator ==(LinkCollection left, LinkCollection right) => left.Equals(right);

		/// <include file='../GlobalDocComments.xml' path='comments/operator[@name="op_Inequality"]'/>
		public static bool operator !=(LinkCollection left, LinkCollection right) => !(left == right);
	}
}
