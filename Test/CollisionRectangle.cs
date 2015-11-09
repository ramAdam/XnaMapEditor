using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Test
{
	public class CollisionRectangle:DrawableGameComponent
	{
		private Texture2D rectangle;
		private SpriteBatch spriteBatch;



		public CollisionRectangle(Game game):base(game)
		{
			


		}


		protected override void LoadContent()
		{
			rectangle = Game.Content.Load<Texture2D> ("red");
			base.LoadContent ();
		}

		public override void Draw(GameTime gameTime)
		{
			Text text = Game.Services.GetService (typeof(Text)) as Text;
			this.spriteBatch = Game.Services.GetService (typeof(SpriteBatch)) as SpriteBatch;
			spriteBatch.Begin ();
			spriteBatch.Draw (rectangle, text.DRect, rectangle.Bounds, Color.Black * 0.3f);
			spriteBatch.End ();
		}

	}
}

