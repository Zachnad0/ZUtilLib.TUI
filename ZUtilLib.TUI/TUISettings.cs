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
		///// <summary>
		///// Overrides the width to be a specific proportion of the maximum. Must be between 1.0 and 0.<br/>Default is 1.
		///// </summary>
		//public float ForceWidthPc { get; init; } = 1;
		///// <summary>
		///// Overrides the height to be a specific proportion of the maximum. Must be between 1.0 and 0.<br/>Default is 1.
		///// </summary>
		//public float ForceHeightPc { get; init; } = 1;

		// TODO add more TUISettings?
	}
}