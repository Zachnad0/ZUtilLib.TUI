using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;

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
		/// <summary>
		/// Total dimension length, excluding padding.
		/// </summary>
		private readonly ushort _totalWidth, _totalHeight;
		/// <summary>
		/// [leftX][topY], from top left to bottom right, excluding padding.
		/// <br/>Essentially the most recent final result from the render pipeline.
		/// </summary>
		private CharPoint[,] _currFrameRenderMatrix;

		public TUIConsoleRoot(TUISettings settings)
			: base(null, 0, 0, 0, 0, int.MinValue, settings.DefaultTextColor, settings.DefaultBackgroundColor, settings.DefaultBorderColor)
		{
			// Set instance and readonly fields
			Instance ??= this;
			_settings = settings;

			// Ignore padding if it shrinks it to zero or less
			var winDim = GetTotalWindowDimensions();
			_totalWidth = (ushort)((_settings.PaddingHoriz * 2 < winDim.width) ? (winDim.width - (2 * _settings.PaddingHoriz)) : winDim.width);
			_totalHeight = (ushort)((_settings.PaddingVert * 2 < winDim.height) ? (winDim.height - (2 * _settings.PaddingVert)) : winDim.height);

			// Apply calculated properties
			Width = _totalWidth;
			Height = _totalHeight;

			// Initialize frame render matrix
			_currFrameRenderMatrix = new CharPoint[_totalWidth, _totalHeight];
		}

		public static (ushort width, ushort height) GetTotalWindowDimensions()
		{
			try
			{
				return ((ushort)Console.WindowWidth, (ushort)Console.WindowHeight);
			}
			catch { return (0, 0); }
		}

		public (ushort width, ushort height) GetRenderDimensions() => (Instance._totalWidth, Instance._totalHeight);

		public void RenderFrame()
		{
			// Render in parallel, then store each frame
			Dictionary<TUIElementBase, CharPoint[][]> finalRenders = new();
			finalRenders.EnsureCapacity(AllElements.Count);
			Parallel.ForEach(AllElements, element =>
			{
				var render = element.RenderElementMatrix();
				lock (finalRenders) if (!finalRenders.TryAdd(element, render)) throw new Exception("This should never happen.");
			});

			// Sort elements from lowest to highest Z-index
			AllElements.Sort((x, y) => Math.Sign((long)x.ZIndex - (long)y.ZIndex));
			// Then stack on frames in that order
			_currFrameRenderMatrix = new CharPoint[_totalWidth, _totalHeight];
			for (uint i = 0; i < AllElements.Count; i++)
			{
				// CONTINUE HERE with writing the stack of rendered matrices in order
			}
		}

		internal override CharPoint[][] RenderElementMatrix() => throw new NotImplementedException();
	}
}
