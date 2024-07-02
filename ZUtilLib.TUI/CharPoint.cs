using System;

namespace ZUtilLib.TUI
{
	/// <summary>
	/// Represents a character printable to the console.
	/// </summary>
	/// <param name="Character">The char to write.</param>
	/// <param name="ForegroundColor">The foreground color.</param>
	/// <param name="BackgroundColor">The background color.</param>
	public readonly record struct CharPoint(char Character, ConsoleColor ForegroundColor, ConsoleColor BackgroundColor)
	{
		/// <summary>
		/// Creates a <see cref="CharPoint"/> with the default properties.
		/// <br/>Default: (' ', Black, Black)
		/// </summary>
		public CharPoint() : this(' ', ConsoleColor.Black, ConsoleColor.Black) { }
	}
}