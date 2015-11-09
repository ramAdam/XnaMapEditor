using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Test
{
	public class Text
	{
		private SpriteFont font;
		private SpriteBatch sprite;
		private Color color = Color.White;
		private float size = 1f;
		private int space = 3;
		private String str;
		private Rectangle dRect;
		private int xPos, yPos;


		public Text (SpriteBatch spriteBatch, SpriteFont font, int xPos, int yPos)
		{
			this.sprite = spriteBatch;
			this.font = font;
			this.xPos = xPos;
			this.yPos = yPos;
		}


		public Color Color
		{
			get{ return color;}
			set{ color = value;}
		}

		public float Size
		{
			get{return size; }
			set{ size = value;}
		}

		public int Space
		{
			get{return space; }
			set{ space = value; }
		}

		public String Str
		{
			get{return str; }
			set{ str = value;}
		}

		public int XPos 
		{
			get{ return xPos;}
			set{ xPos = value;}
		}

		public int YPos 
		{
			get{ return yPos;}
			set{ yPos = value;}
		}



		public void drawText(String s)
		{
			//sprite.Begin ();

			sprite.DrawString (font, s, new Vector2 (xPos, yPos), color, 0f, new Vector2 (), size, SpriteEffects.None, 1f);
			//sprite.End ();
		}
		// TODO: fix the string, it should be a string builder (Memory Prob)


		public void DrawClickText(int mosX, int mosY)
		{
			Ishovering (mosX, mosY);
			drawText (str);


		}

		public Rectangle DRect
		{
			get
			{
				int fontWidth = (int)font.MeasureString(str).X * (int)size;
				int fontHeight =(int)font.MeasureString(str).Y * (int)size;
				this.dRect = new Rectangle (xPos, yPos, fontWidth, fontHeight);
				return dRect;
			}
		}

		public bool Ishovering (int mosX, int mosY)
		{
			int fontWidth = (int)font.MeasureString(str).X * (int)size;
			int fontHeight = (int)font.MeasureString(str).Y * (int)size;
			Rectangle dRect = new Rectangle (xPos, yPos, fontWidth, fontHeight);
			color = Color.White;
			bool hover = false;

			if (dRect.Contains(mosX, mosY))
			{
				color = Color.Yellow;
				hover = true;
			}


			return hover;
		}



	}
}

