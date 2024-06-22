using ZUtilLib;
using System;
using System.Collections.Generic;
using System.Drawing;

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
		public Color TextColor { get; protected set; }
		public Color BackgroundColor { get; protected set; }
		public Color BorderColor { get; protected set; }
		public bool IsVisible { get; protected set; }

		// Forcibly Unique Properties
		public List<TUIElementBase> Children => new(_children);

		private readonly HashSet<TUIElementBase> _children = new();
		private readonly TUIElementBase? _parent;

		protected TUIElementBase(TUIElementBase? parentElement, ushort? width, ushort? height, ushort? xLeftPos, ushort? yTopPos, int? zIndex, Color? textColor, Color? backgroundColor, Color? borderColor, params TUIElementBase[] childElements)
		{
			_parent = parentElement;

			// Set or inherit properties
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
			finally
			{
				// No inheriting if root
				if (IsRootElement())
					throw new ArgumentException("TUIElementBase Critical Error: Root cannot inherit properties.");
			}

			foreach (var c in childElements) _children.Add(c);
			AllElements.Add(this);
		}

		public bool IsRootElement() => _parent == null;

		protected bool TryAddChild(TUIElementBase childElement) => _children.Add(childElement);

		protected bool TryRemoveChild(TUIElementBase childElement) => _children.Remove(childElement);
	}
}
