using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using ZUtilLib.TUI;

namespace UnitTests
{
	[TestClass]
	public class TUIElementUnitTests
	{
		//[TestMethod]
		//public void TestTUISection()
		//{
		//	Assert.ThrowsException<ArgumentNullException>(() => new TUISection(null, 0, 0, 0, 0, 0, ConsoleColor.Black, ConsoleColor.White, ConsoleColor.Gray));
		//}

		[TestMethod]
		public void TestTUIRoot()
		{
			TUIConsoleRoot tcr;
			Assert.IsNotNull(tcr = new(new()));
			Assert.IsTrue(tcr.IsRootElement());

			TUISection tSec;
			Assert.IsNotNull(tSec = new(tcr, 20, 20, 5, 5, 1, null, null, null));

			Assert.IsTrue(tSec.BorderColor == tcr.BorderColor);
		}

		[TestMethod]
		public void TestTUIChildren()
		{
			TUIConsoleRoot tcr = new(new());

			ushort c = (ushort)Random.Shared.Next(10, 200);
			for (ushort i = 0; i < c; i++)
				Assert.IsNotNull(new TUISection(tcr, null, null, null, null, null, null, null, null));

			Assert.IsTrue(tcr.GetChildren().Count == c);
			Assert.IsTrue(TUIElementBase.AllElements.Count == c + 1);

			ushort o = (ushort)Random.Shared.Next(10, 200);
			var parent = TUIElementBase.AllElements.First(e => e != tcr);
			for (ushort i = 0; i < o; i++)
				Assert.IsNotNull(new TUISection(parent, null, null, null, null, null, null, null, null));

			Assert.IsTrue(parent.GetChildren().Count == o);
			Assert.IsTrue(tcr.GetChildren().Count == c);
			Assert.IsTrue(tcr.GetAllDescendants().Count == c + o);
		}
	}
}