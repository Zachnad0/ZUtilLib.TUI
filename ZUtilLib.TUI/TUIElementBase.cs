using ZUtilLib;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace ZUtilLib.TUI
{
	public abstract class TUIElementBase
	{
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
		public HashSet<TUIElementBase> Children { get => new(_children); }

		private readonly HashSet<TUIElementBase> _children = new();
		private readonly TUIElementBase? _parent;

		protected TUIElementBase(TUIElementBase parentElement, ushort? width, ushort? height, ushort? xLeftPos, ushort? yTopPos, int? zIndex, Color? textColor, Color? backgroundColor, Color? borderColor, params TUIElementBase[] childElements)
		{
			_parent = parentElement;

			// Set or inherit properties
			try
			{
				Width = width ?? _parent.Width;
				Height = height ?? _parent.Height;
				XLeftPos = xLeftPos ?? _parent.XLeftPos;
				YTopPos = yTopPos ?? _parent.YTopPos;
				ZIndex = zIndex ?? _parent.ZIndex + 1;
				TextColor = textColor ?? _parent.TextColor;
				BackgroundColor = backgroundColor ?? _parent.BackgroundColor;
				BorderColor = borderColor ?? _parent.BorderColor;
			}
			finally
			{
				// No inheriting if root
				if (IsRootElement())
					throw new ArgumentException("TUIElementBase Critical Error: Root cannot inherit properties.");
			}

			childElements.Foreach((i, v) => _children.Add(v));
			foreach (var c in childElements) _children.Add(c);
		}

		public bool IsRootElement() => _parent == null;
	}
}
