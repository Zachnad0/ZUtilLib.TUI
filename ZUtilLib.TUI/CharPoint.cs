using System;

namespace ZUtilLib.TUI
{
	internal readonly record struct CharPoint(char Character, ConsoleColor ForegroundColor, ConsoleColor BackgroundColor)
	{
		public CharPoint() : this(' ', ConsoleColor.Black, ConsoleColor.Black) { }
	}
}