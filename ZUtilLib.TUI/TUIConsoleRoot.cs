using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;

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

		private readonly List<TUIPostProcessFrame> _postProcesses = new();
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

		public TUIConsoleRoot(TUISettings settings, params TUIPostProcessFrame[] postProcesses)
			: base(null, 0, 0, 0, 0, int.MinValue, settings.DefaultTextColor, settings.DefaultBackgroundColor, settings.DefaultBorderColor)
		{
			// Set instance and readonly fields
			Instance ??= this;
			_settings = settings;
			_postProcesses = postProcesses.ToList();

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

		public (ushort width, ushort height) GetRenderDimensions() => (_totalWidth, _totalHeight);

		public void RenderFrame()
		{
			// Render in parallel, then store each frame
			List<TUIElementBase> renderableElements = AllElements.Where(e => e.IsVisible).ToList();
			Dictionary<TUIElementBase, CharPoint[,]> finalRenders = new();
			finalRenders.EnsureCapacity(renderableElements.Count);

			Parallel.ForEach(renderableElements, element =>
			{
				var render = element.RenderElementMatrix();
				lock (finalRenders) if (!finalRenders.TryAdd(element, render)) throw new Exception("This should never happen.");
			});

			// Sort elements from lowest to highest Z-index
			renderableElements.Sort((x, y) => Math.Sign((long)x.ZIndex - (long)y.ZIndex));

			// Then stack on frames in that order
			_currFrameRenderMatrix = new CharPoint[_totalWidth, _totalHeight];
			for (int i = 0; i < renderableElements.Count; i++)
			{
				// Check validity and visibility of current window
				TUIElementBase currElement = renderableElements[i];
				if (!currElement.IsVisible || currElement.Width == 0 || currElement.Height == 0)
					continue;
				CharPoint[,] currMatrix = finalRenders.TryGetValue(currElement, out CharPoint[,]? frame) ? frame : throw new Exception($"TUIConsoleRoot Critical Error: Render frame missing for element: {currElement}");

				// Write matrix onto frame originating from position
				if (currElement.XLeftPos < 0 || currElement.YTopPos)
			}

			// Run post-process functions
		}

		public static T?[,] OffsetMatrix<T>(T?[,] inputMatrix, int rightAmount, int downAmount)
		{
			// Validity check
			if ((rightAmount == 0 && downAmount == 0) || inputMatrix == null)
				return inputMatrix;

			int width = inputMatrix.GetLength(0), height = inputMatrix.GetLength(1);
			T?[,] outputMatrix = new T[width, height];
			for (int x = 0; x < width; x++)
			{
				bool fillerX = x - rightAmount < 0 || x - rightAmount >= width;

				for (int y = 0; y < height; y++)
				{
					bool fillerY = y - downAmount < 0 || y - downAmount >= height;
					outputMatrix[x, y] = (fillerX || fillerY) ?
						default :
						inputMatrix[x - rightAmount, y - downAmount];
				}
			}

			return outputMatrix;
		}

		public static T?[,] CropMatrix<T>(T?[,] inputMatrix, int left, int right, int top, int bottom)
		{
			// Negative values expand with default
			if ((left == 0 && right == 0 && top == 0 && bottom == 0) || inputMatrix == null)
				return inputMatrix;

			// CONTINUE HERE write CropMatrix method, then remove it from this project and put into a minor for ZUtilLib

			return default;
		}

		internal override CharPoint[,] RenderElementMatrix() => throw new NotImplementedException();
	}

	/// <summary>
	/// Processes the given TUI frame and returns the result.
	/// </summary>
	/// <param name="inputMatrix">The inputted matrix of the frame.</param>
	/// <returns>The resulting processed frame matrix.</returns>
	public delegate CharPoint[,] TUIPostProcessFrame(CharPoint[,] inputMatrix);
}
