using System.Drawing;
using System;

namespace ZUtilLib.TUI
{
	/// <summary>
	/// The most basic TUI element type. Just has the essential properties, and is mostly useful for frames and categorization.
	/// </summary>
	public sealed class TUISection : TUIElementBase
	{
		public TUISection(TUIElementBase? parentElement, ushort? width, ushort? height, ushort? xLeftPos, ushort? yTopPos, int? zIndex, ConsoleColor? textColor, ConsoleColor? backgroundColor, ConsoleColor? borderColor)
			: base(parentElement ?? throw new ArgumentNullException(nameof(parentElement)), width, height, xLeftPos, yTopPos, zIndex, textColor, backgroundColor, borderColor)
		{
			// It really just is what it is
		}
	}
}
