using System.Collections.Generic;

namespace ZUtilLib.TUI
{
	public sealed class TUIFrame : ITUIElement
	{
		/// <summary>
		/// The current instance of the running frame.
		/// </summary>
		public static TUIFrame? Instance { get; private set; }
		private readonly List<TUISection> _sections = new();

		public TUIFrame()
		{
			Instance ??= this;
		}

		private static (uint width, uint height) GetFrameDimensions()
		{
			// TODO find way of determining console width and height, and maybe setting it too?
			return default;
		}
	}
}
