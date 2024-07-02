using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Drawing;
using System.Linq;
using ZUtilLib;
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

		[TestMethod]
		public void TestTUIRenderPipeline()
		{
			TUIConsoleRoot tcr = new(new());

			// Generate test elements
			ushort c = (ushort)Random.Shared.Next(10, 200);
			for (ushort i = 0; i < c; i++)
			{
				//ushort sLen = (ushort)Random.Shared.Next(5, 30);
				//(ushort winWidth, ushort winHeight) = TUIConsoleRoot.GetTotalWindowDimensions();
				//ushort posX = (ushort)Random.Shared.Next(0, winWidth - sLen), posY = (ushort)Random.Shared.Next(0, winHeight - sLen);
				//Assert.IsNotNull(new TUISection(tcr, sLen, sLen, posX, posY, Random.Shared.Next(int.MinValue + 1, int.MaxValue), null, null, null));

				Assert.IsNotNull(new TUISection(tcr, 0, 0, 0, 0, Random.Shared.Next(int.MinValue + 1, int.MaxValue), null, null, null));
			}

			tcr.RenderFrame();
		}

		[TestMethod]
		public void TestMatrixShift()
		{
			int[,] m = Random.Shared.NextMatrix(10, 10)
				.ToJaggedMatrix()
				.Select(a => a
					.Select(n => (int)Math.Round(n) + 1)
					.ToArray())
				.ToArray()
				.ToNonJaggedMatrix();

			string mr1 = m.ToJaggedMatrix().ToReadableString("");
			int[,] e;
			string mr2 = (e = TUIConsoleRoot.OffsetMatrix(m, 2, 4)).ToJaggedMatrix().ToReadableString("");
			string mr3 = TUIConsoleRoot.OffsetMatrix(e, -2, -4).ToJaggedMatrix().ToReadableString("");
			Assert.AreNotEqual(mr1, mr2);
			Assert.AreNotEqual(mr2, mr3);
		}
	}
}