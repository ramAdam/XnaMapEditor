using System;

using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace Test
{
	public class DrawMapSegment
	{
		private List<Rectangle> dRectList, sRectList;
		private List<SegmentDefinition> segDef;
		private Point point;
		private int deltaY, dRectWidth, dRectHeight;
		private Text text;

		public DrawMapSegment (List<SegmentDefinition> segDef, int dRectX, int dRectY, int dRectWidth, int dRectHeight, int deltaY)
		{
			this.segDef = segDef;
			this.point = new Point (dRectX, dRectY);
			this.deltaY = deltaY;
			this.dRectWidth = dRectWidth;
			this.dRectHeight = dRectHeight;
			this.dRectList = new List<Rectangle> ();
			this.sRectList = new List<Rectangle> ();
			createRecList ();
		}

		//TODO: segDef.capacity size is 16
		private void createRecList()
		{
			for(int i = 0; i < segDef.Count; i++)
			{
				this.dRectList.Add (new Rectangle (this.point.X, this.point.Y + (this.deltaY * i), dRectWidth, dRectHeight));
				this.sRectList.Add (segDef [i].SourceRect);
			} 

		}

		public Text Text
		{
			get{ return text; }
			set{ text = value;}
		}

		public List<Rectangle> DRectList
		{
			get{return this.DRectList; }
		}


		public List<Rectangle> SRectList
		{
			get{return this.SRectList; }
		}


		public void DrawSegments(SpriteBatch spriteBatch, Texture2D mapTex)
		{
			spriteBatch.Begin ();

			for(int i = 0; i < segDef.Count; i++)
			{
				spriteBatch.Draw (mapTex, this.dRectList [i], this.sRectList[i], Color.White);


				if (text != null) 
				{
					text.XPos = dRectList [i].X + dRectList [i].Width + text.Space;
					text.YPos = dRectList [i].Y;
					String name = segDef [i].Name;
					text.drawText (name);
				}
			}

			spriteBatch.End ();
		}

		public int getIndexOfPalletSegment(int x, int y)
		{
			for (int i = 0; i < dRectList.Count; i++) 
			{
				if (this.dRectList [i].Contains(x, y)) 
					return i;
				
			}

			return -1;
		}


	}
}

