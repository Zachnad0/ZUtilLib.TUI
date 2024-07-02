using ZUtilLib;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ZUtilLib.TUI
{
	public abstract class TUIElementBase // TODO implement unit tests
	{
		/// <summary>
		/// A copy of the hashset containing every element.
		/// </summary>
		public static List<TUIElementBase> AllElements { get => new(_allElements); }
		private static HashSet<TUIElementBase> _allElements = new();

		// Inheritable Properties
		public ushort Width { get; protected set; }
		public ushort Height { get; protected set; }
		public short XLeftPos { get; protected set; }
		public short YTopPos { get; protected set; }
		public int ZIndex { get; protected set; }
		public ConsoleColor TextColor { get; protected set; }
		public ConsoleColor BackgroundColor { get; protected set; }
		public ConsoleColor BorderColor { get; protected set; }
		public bool IsVisible { get; protected set; } = true;

		private readonly TUIElementBase? _parent;

		protected TUIElementBase(TUIElementBase? parentElement, ushort? width, ushort? height, short? xLeftPos, short? yTopPos, int? zIndex, ConsoleColor? textColor, ConsoleColor? backgroundColor, ConsoleColor? borderColor)
		{
			// Check and assign nullable parent
			if (parentElement == this) throw new ArgumentException($"TUIElementBase Critical Error: Parent of element cannot be itself.");
			_parent = parentElement;

			// Set or inherit properties
			bool invalidArg = false;
			try
			{
#pragma warning disable CS8602
				Width = width ?? _parent.Width; // Will throw exception if _parent is null
				Height = height ?? _parent.Height;
				XLeftPos = xLeftPos ?? _parent.XLeftPos;
				YTopPos = yTopPos ?? _parent.YTopPos;
				ZIndex = zIndex ?? _parent.ZIndex + 1;
				TextColor = textColor ?? _parent.TextColor;
				BackgroundColor = backgroundColor ?? _parent.BackgroundColor;
				BorderColor = borderColor ?? _parent.BorderColor;
#pragma warning restore CS8602
			}
			catch
			{
				invalidArg = true;
			}
			finally
			{
				// No inheriting if root
				if (IsRootElement() && invalidArg)
					throw new ArgumentException("TUIElementBase Critical Error: Root cannot inherit properties.");
			}

			// Clear all elements if this is a new root (just to be sure)
			if (IsRootElement()) _allElements = new();
			_allElements.Add(this);
		}

		/// <summary>
		/// Is this element the root element?
		/// </summary>
		public bool IsRootElement() => _parent == null;

		/// <summary>Finds all of this element's direct children.</summary>
		public List<TUIElementBase> GetChildren() => _allElements.Where(e => e._parent == this).ToList();

		/// <summary>
		/// Finds every single descendant of this element.<br/>Warning: Rather slow.
		/// </summary>
		public List<TUIElementBase> GetAllDescendants()
		{
			// Select each child element, and their own children, and so forth
			List<TUIElementBase> allDescendants = new(), subChildren = GetChildren();
			do
			{
				allDescendants.AddRange(subChildren);
				subChildren = subChildren.SelectMany(e => e.GetChildren()).ToList();

			} while (subChildren.Count > 0);

			allDescendants.RemoveAll(e => allDescendants.Count(e.Equals) > 1);
			return allDescendants;
		}

		/// <summary>
		/// This should be defined regardless of element visibility.
		/// </summary>
		internal abstract CharPoint[,] RenderElementMatrix();
	}
}
