using NUnit.Framework;
using System;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Test
{
	[TestFixture ()]
	public class NUnitTestClass
	{
		Map map;
		DrawMapSegment pallet;


		[SetUp]
		public void Init()
		{
			map = new Map ();
			map.AddSeg (1f, 0, 45, 45);
			pallet =new DrawMapSegment (map.SegmentDefinitions, 500, 30, 50, 30, 40);

		}


		[Test]
		public void getSegListSize()
		{
			Assert.AreEqual (11, map.SegmentDefinitions.Count);
		}

		[Test]
		public void getSegmentIndex()
		{
			Assert.AreEqual (1, pallet.getIndexOfPalletSegment (503, 75));	
		}

		[Test]
		public void checkSizeOfLayers()
		{
			Assert.AreEqual (1, map.SegmentLayers [0].Count);
		}





	}
}

