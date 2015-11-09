using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Test
{
	public class SpriteManager
	{
		private float _transitionTime;
		public int Index { get; set;}
		public int[] Sequence{ get; set;}

		public int TileX{ set; get;} // frames per row
		public int TileY{ set; get;} // frames per column


		public int Time{ set; get;}
		public bool Continuous{ set; get;}

		public Texture2D Texture{ get; set;}
		public Vector2 Position{ get; set;}

		/*Gets the width and height of one frame*/
		public Point TileSize{
			get{
				return GetMaxFrames(Texture.Width, Texture.Height);
			}
		}


		public SpriteManager ()
		{

		}

		public void Update(GameTime gameTime){
			_transitionTime = (float)gameTime.ElapsedGameTime.TotalMilliseconds;

			if (_transitionTime > Time) {
				_transitionTime = 0;
				Index++;

				if (Index > Sequence.Length - 1) {
					if (Continuous) {    // looping animation
						Index = 0;
					}
					else {
						Index = Sequence.Length - 1; // keep the index at the last frame
					}

				}
			}
		}

		public void Draw(SpriteBatch spriteBatch, Color color){
			if (Index > Sequence.Length - 1)
				 Index = Sequence.Length - 1;

			var xSequence = Sequence[Index] % TileX;
			var ySequence = Sequence[Index] / TileX;

			var rectSprite = new Rectangle(xSequence * TileSize.X, ySequence * TileSize.Y, TileSize.X, TileSize.Y);
			spriteBatch.Draw (Texture, Position, rectSprite, color);
		}


		public void Draw(SpriteBatch spriteBatch){
			Draw(spriteBatch, Color.White);
		}

		/*How many frames per row per column*/
		public Point GetMaxFrames(int width, int height){
			return new Point (width / TileX, height / TileY);

		}

		public static int[] Inject(params int[] sequence){
			return sequence;
		}
	}
}


