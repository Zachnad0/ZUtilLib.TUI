using System;

namespace ZUtilLib.TUI
{
	/// <summary>
	/// An instance of settings for the <see cref="TUIConsoleRoot"/>
	/// </summary>
	public sealed class TUISettings
	{
		/// <summary>
		/// The number of empty chars padding around contents.<br/>Default is 0.
		/// </summary>
		public ushort PaddingHoriz { get; init; } = 0;
		/// <summary>
		/// The number of empty chars padding around contents.<br/>Default is 0.
		/// </summary>
		public ushort PaddingVert { get; init; } = 0;
		/// <summary>
		/// Sets the inheritable text color for the root element.<br/>Default is white.
		/// </summary>
		public ConsoleColor DefaultTextColor { get; init; } = ConsoleColor.White;
		/// <summary>
		/// Sets the inheritable background color for the root element.<br/>Default is black.
		/// </summary>
		public ConsoleColor DefaultBackgroundColor { get; init; } = ConsoleColor.Black;
		/// <summary>
		/// Sets the inheritable border color for the root element.<br/>Default is gray.
		/// </summary>
		public ConsoleColor DefaultBorderColor { get; init; } = ConsoleColor.Gray;

		// TODO add more TUISettings?
	}
}