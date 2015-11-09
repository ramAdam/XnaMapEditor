using System;

using Microsoft.Xna.Framework;

namespace Test
{
	public class MapSegment
	{
		public Vector2 location;
		private int segmentIndex;
		private float layer;
		private Color color;
		private float scale;
		private Rectangle dRect;


		public MapSegment(int segmentIndex, float layer){
			this.segmentIndex = segmentIndex;
			this.layer = layer;
			this.scale = 1.0f;
			this.color = Color.White;
			SetColor ();

		}


		public int Index
		{
			get{ return segmentIndex;}
			set{ segmentIndex = value;}
		}


		public float Layer
		{
			get{ return layer;}
			set{ layer = value;}
		}

		public Color Color
		{
			get
			{ 
				return color;
			}

			private set
			{
				color = value;
			}


		}


		public float Scale
		{
			get{ return scale;}
			private set
			{ 
				this.scale = value;
			}
		}





		public Rectangle DRect
		{

			get{ return this.dRect;}
			set
			{ 
				dRect = value; 

			}      		
		}


		private void SetColor()
		{
			if (this.layer.CompareTo(0f) == 0) 
			{
				this.Color = Color.Gray;
				this.Scale = 0.75f;
			}
			else if (this.Layer.CompareTo(1f)== 0)
			{
				this.Color = Color.DarkGray;
				this.Scale = 1.25f;
			}
    	}


	}
}
