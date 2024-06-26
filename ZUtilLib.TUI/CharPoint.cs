using System;

namespace ZUtilLib.TUI
{
	internal readonly record struct CharPoint(char Character, ConsoleColor ForegroundColor, ConsoleColor BackgroundColor);
}