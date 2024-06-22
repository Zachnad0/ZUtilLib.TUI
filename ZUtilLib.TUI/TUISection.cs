using System.Drawing;

namespace ZUtilLib.TUI
{
	public class TUISection : TUIElementBase
	{
		public TUISection(TUIElementBase? parentElement, ushort? width, ushort? height, ushort? xLeftPos, ushort? yTopPos, int? zIndex, Color? textColor, Color? backgroundColor, Color? borderColor, params TUIElementBase[] childElements)
			: base(parentElement, width, height, xLeftPos, yTopPos, zIndex, textColor, backgroundColor, borderColor, childElements)
		{
			// CONTINUE HERE with writing TUISection constructor
		}
	}
}
