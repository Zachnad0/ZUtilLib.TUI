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
		/// <summary>[leftX][topY], from top left to bottom right.</summary>
		private readonly CharPoint[][] _currFrameRenderMatrix;

		public TUIConsoleRoot(TUISettings settings)
			: base(null, 0, 0, 0, 0, int.MinValue, settings.DefaultTextColor, settings.DefaultBackgroundColor, settings.DefaultBorderColor)
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

			// Initialize frame render matrix
			_currFrameRenderMatrix = new CharPoint[_totalWidth][];
			for (ushort i = 0; i < _currFrameRenderMatrix.Length; i++)
				_currFrameRenderMatrix[i] = new CharPoint[_totalHeight];
		}

		private static (ushort width, ushort height) GetWindowDimensions()
		{
			try
			{
				return ((ushort)Console.WindowWidth, (ushort)Console.WindowHeight);
			}
			catch { return (0, 0); }
		}
	}
}
