using ZUtilLib;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace ZUtilLib.TUI
{
	public abstract class TUIElementBase // TODO implement unit tests
	{
		public static List<TUIElementBase> AllElements { get; private set; } = new();

		// Inheritable Properties
		public ushort Width { get; protected set; }
		public ushort Height { get; protected set; }
		public ushort XLeftPos { get; protected set; }
		public ushort YTopPos { get; protected set; }
		public int ZIndex { get; protected set; }
		public ConsoleColor TextColor { get; protected set; }
		public ConsoleColor BackgroundColor { get; protected set; }
		public ConsoleColor BorderColor { get; protected set; }
		public bool IsVisible { get; protected set; } = true;

		private readonly TUIElementBase? _parent;

		protected TUIElementBase(TUIElementBase? parentElement, ushort? width, ushort? height, ushort? xLeftPos, ushort? yTopPos, int? zIndex, ConsoleColor? textColor, ConsoleColor? backgroundColor, ConsoleColor? borderColor)
		{
			// Check and assign nullable parent
			if (parentElement == this) throw new ArgumentException($"TUIElementBase Critical Error: Parent of element cannot be itself.");
			_parent = parentElement;

			// Set or inherit properties
			bool invalidArg = false;
			try
			{
				Width = width ?? _parent.Width; // Will throw exception if _parent is null
				Height = height ?? _parent.Height;
				XLeftPos = xLeftPos ?? _parent.XLeftPos;
				YTopPos = yTopPos ?? _parent.YTopPos;
				ZIndex = zIndex ?? _parent.ZIndex + 1;
				TextColor = textColor ?? _parent.TextColor;
				BackgroundColor = backgroundColor ?? _parent.BackgroundColor;
				BorderColor = borderColor ?? _parent.BorderColor;
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
			if (IsRootElement()) AllElements = new();
			AllElements.Add(this);
		}

		/// <summary>
		/// Is this element the root element?
		/// </summary>
		public bool IsRootElement() => _parent == null;

		/// <summary>Finds all of this element's direct children.</summary>
		public List<TUIElementBase> GetChildren() => AllElements.Where(e => e._parent == this).ToList();

		/// <summary>
		/// Finds every single descendant of this element.
		/// </summary>
		public List<TUIElementBase> GetAllDescendants()
		{
			// CONTINUE HERE write this
			return new();
		}

		// TODO implement drawing to local sub-window CharPoint matrix then passing it forward to be drawn on by the next higher-up z-index element
	}
}
