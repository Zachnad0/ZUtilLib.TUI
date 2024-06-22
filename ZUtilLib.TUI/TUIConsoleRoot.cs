using System;
using System.Drawing;
using System.Collections.Generic;

namespace ZUtilLib.TUI
{
	// ASSUMPTIONS:	Console window size is CONSTANT, char size is CONSTANT.
	// TODO someday maybe allow for dynamic resizing???
	/// <summary>
	/// The root element of the text user-interface. Instantiate this to build a TUI.
	/// </summary>
	public sealed class TUIConsoleRoot : TUIElementBase
	{
		/// <summary>
		/// The current instance of the running frame.
		/// </summary>
		public static TUIConsoleRoot? Instance { get; private set; }

		private readonly TUISettings _settings;
		private readonly ushort _totalWidth, _totalHeight;

		public TUIConsoleRoot(TUISettings settings, params TUISection[] childSections)
			: base(null, 0, 0, 0, 0, int.MinValue, Color.Red, Color.Red, Color.Red, childSections)
		{
			// Set instance and readonly fields
			Instance ??= this;
			_settings = settings;

			// Ignore padding if it shrinks it to zero or less
			var winDim = GetWindowDimensions();
			_totalWidth = (ushort)((_settings.PaddingHoriz * 2 < winDim.width) ? (winDim.width - (2 * _settings.PaddingHoriz)) : winDim.width);
			_totalHeight = (ushort)((_settings.PaddingVert * 2 < winDim.height) ? (winDim.height - (2 * _settings.PaddingVert)) : winDim.height);

			// Apply calculated properties
			Width = _totalWidth;
			Height = _totalHeight;
		}

		private static (uint width, uint height) GetWindowDimensions()
		{
			return ((uint)Console.WindowWidth, (uint)Console.WindowHeight);
		}
	}
}
