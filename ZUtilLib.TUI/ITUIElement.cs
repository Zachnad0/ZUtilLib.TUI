namespace ZUtilLib.TUI
{
	internal interface ITUIElement
	{
		public int Width { get; }
		public int Height { get; }
		public int XLeftPos { get; }
		public int YTopPos { get; } // CONTINUE HERE with making some properties or enforced getter/setters. Or maybe just make an abstract class???
	}
}
