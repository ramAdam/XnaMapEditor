#region Using Statements
using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

#endregion

namespace Test
{
	/// <summary>
	/// This is the main type for your game.
	/// </summary>
	public class Game1 : Game
	{
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;
		Texture2D map, nullTex, icons;
		int mosX, mosY, segIndex, mapSegIndex;
		//private SpriteManager _spriteManager;
		private Map mapEd;
		private DrawMapSegment drawMapSeg;
		//private bool m;
		MouseState previousMouseState;
		MouseState currentMouseState;
		SpriteFont font;
		Text text;
		Text clickAbleText;
		String[] layer ;
		String layerName;
		float currentLayer;
		Boolean mouseClick, dropSegment, holdSegment, mouseDrag;

		public Game1 ()
		{
			//Console.WriteLine("This is C#");
			graphics = new GraphicsDeviceManager (this);
			Content.RootDirectory = "Content";
			graphics.IsFullScreen = false;		

		}



		protected override void Initialize ()
		{
			// TODO: Add your initialization logic here
			mapEd = new Map ();
			drawMapSeg = new DrawMapSegment (mapEd.SegmentDefinitions, 500, 30, 50, 30, 40);
			segIndex = -1;
			mapSegIndex = -1;
			currentLayer = 0f;
			layer = new string[]{ "back", "mid", "front" };
			layerName = layer[0];

			mouseClick = false;
			dropSegment = false;
			holdSegment = false;
			mouseDrag = false;

			//Components.Add (new CollisionRectangle (this));

			base.Initialize ();

				
		}



		protected override void LoadContent ()
		{
			spriteBatch = new SpriteBatch (GraphicsDevice);
			map = Content.Load<Texture2D>("maps1");
			nullTex = Content.Load<Texture2D> ("1x1");
			icons = Content.Load<Texture2D> ("icon");
			font = Content.Load<SpriteFont> ("Arial");
			text = new Text (this.spriteBatch, font, 0, 0);
			clickAbleText = new Text (spriteBatch, font, 10, 5);
			clickAbleText.Size = 1.0f;
			clickAbleText.Str = "layer:" + layerName;
			drawMapSeg.Text = text;



			//Services.AddService (typeof(SpriteBatch), spriteBatch);
			//Services.AddService (typeof(Text), clickAbleText);
		}



		protected override void Update (GameTime gameTime)
		{
			//MouseState mState = Mouse.GetState ();
			previousMouseState = currentMouseState;
			currentMouseState = Mouse.GetState();

			//pMosX = mosX;
			//pMosY = mosY;

			mosX = currentMouseState.X;
			mosY = currentMouseState.Y;




			if((currentMouseState.LeftButton == ButtonState.Released && previousMouseState.LeftButton == ButtonState.Pressed) && clickAbleText.Ishovering(mosX, mosY))
			{
				

				switch(layerName)
				{
				case "back":
					layerName = "mid";
					currentLayer = .5f;
					break;
				case "mid":
					layerName = "front";
					currentLayer = 1f;
					break;
				case "front":
					layerName = "back";
					currentLayer = 0f;
					break;
				}

				clickAbleText.Str = "layer: " + layerName;

				//Console.WriteLine(string.Format(layerName +": {0:0.0}", currentLayer));	

			}
			else if (!(currentMouseState.LeftButton == ButtonState.Released) && previousMouseState.LeftButton == ButtonState.Pressed) 
			{
				//Console.WriteLine ("Drag!!");

				if (drawMapSeg.getIndexOfPalletSegment (mosX, mosY) != -1)
				{
					segIndex = drawMapSeg.getIndexOfPalletSegment (mosX, mosY);
					//Console.Write("Pallet Seg Index:");
					//Console.WriteLine (segIndex);
				}
				else if (mapEd.HoverOver (mosX, mosY, currentLayer) != -1 && !mouseDrag)
				{


					mapSegIndex = mapEd.HoverOver (mosX, mosY, currentLayer);
					mouseDrag= true;

					Console.WriteLine("Chosen!!");


				}else if (mapSegIndex != -1 && mouseDrag) 
				{
					mapEd.GetMapSegment (mapSegIndex, currentLayer).location.X = mosX;
					mapEd.GetMapSegment (mapSegIndex, currentLayer).location.Y = mosY;


					//mapEd.GetMapSegment (mapSegIndex, currentLayer).DRect.X = mosX;
					//mapEd.GetMapSegment (mapSegIndex, currentLayer).DRect.Y = mosY;
					//Console.Write ("Map Seg Index :");
					//Console.WriteLine (mapSegIndex);
					//Console.Write ("Size of layer1 is:");
					//Console.WriteLine (mapEd.SegmentLayers[0].Count);

				}

				 
			}
			else
			{
				if (segIndex != -1)
				{
					//change the layer
					mapEd.AddSeg (currentLayer, segIndex, mosX, mosY);
					segIndex = -1;
				}

				mapSegIndex = -1;
				mouseDrag = false;

						

			}


			base.Update (gameTime);
		}


		protected override void Draw (GameTime gameTime)
		{
			graphics.GraphicsDevice.Clear (Color.CornflowerBlue);


			mapEd.Draw (spriteBatch, map);

			drawMapSeg.DrawSegments(spriteBatch, map);

			spriteBatch.Begin ();
			clickAbleText.DrawClickText (mosX, mosY);
			spriteBatch.End ();


			this.DrawCursor();



			base.Draw (gameTime);
		}




		private void DrawCursor()
		{
			spriteBatch.Begin ();
			spriteBatch.Draw (nullTex, new Rectangle (500, 20, 280, 550), Color.Black * 0.3f);
			spriteBatch.Draw (icons, new Vector2 (mosX, mosY), new Rectangle (0, 0, 32, 32), Color.White, 0.0f, new Vector2(0,0), 1.0f, SpriteEffects.None, 0.0f);
			spriteBatch.End ();
		}



	}
}

