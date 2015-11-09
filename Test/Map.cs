using System;

using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.IO;


namespace Test
{
	public class Map
	{
		private List<MapSegment> mapSeg;
		private List<SegmentDefinition> segDef;
		//private List<List<MapSegment>> segmentLayers;
		const float BACK_LAYER = 0f;
		const float MID_LAYER = .5f;
		const float FRONT_LAYER = 1f;
		//Rectangle sRect;
		private int segmentIndex;

		public Map ()
		{
			mapSeg = new List<MapSegment> ();
			segDef = new List<SegmentDefinition>();
			segmentIndex = 0;
			/*segmentLayers = new List<List<MapSegment>> ();

			//Adding 3 layers
			segmentLayers.Add (new List<MapSegment> ());
			segmentLayers.Add (new List<MapSegment> ());
			segmentLayers.Add (new List<MapSegment> ());*/
			ReadSegmentDefinitions ();

		}

		public List<List<MapSegment>> SegmentLayers {
			get{return this.SegmentLayers; }
		}

		public int SegmentIndex
		{
			get{ return segmentIndex;}
		}

		public List<MapSegment> Segments
		{
			get{ return this.mapSeg;}
		}

		/*public int HoverOver(int mosX, int mosY, float currentLayer)
		{
			int index = -1;
			foreach (MapSegment seg in mapSeg) 
				{
					if (seg.DRect.Contains (mosX, mosY) && seg.Layer.CompareTo (currentLayer) == 0)
						index = seg.Index;

				}


			return index;
		}*/

		public int HoverOver(int mosX, int mosY, float currentLayer)
		{
			int index = -1;
			foreach (MapSegment seg in mapSeg) 
			{
				if (seg.DRect.Contains (mosX, mosY) && seg.Layer.CompareTo (currentLayer) == 0)
					index = seg.Index;

			}


			return index;
		}


		/*public MapSegment GetMapSegment(int index, float currentLayer)
		{
			MapSegment segment = null;

			foreach (MapSegment seg in mapSeg) 
			{
				if (seg.Index == index && seg.Layer.CompareTo (currentLayer) == 0)
					segment = seg;
			}

			return segment;
		}*/

		public MapSegment GetMapSegment(int index, float currentLayer)
		{
			MapSegment segment = null;
			foreach (MapSegment seg in mapSeg) 
			{
				if (seg.Index == index && seg.Layer.CompareTo (currentLayer) == 0)
					segment = seg;
			}


			return segment;
		}



		public void AddSeg(float layer, int index, int locX, int locY)
		{
			MapSegment seg = new MapSegment (mapSeg.Count, layer);
			seg.location.X = locX;
			seg.location.Y = locY;
			seg.SRect = segDef [index].SourceRect;
			mapSeg.Add(seg);
			segmentIndex++;
		}

		/*public void AddSeg(float segLayer, int palletSegIndex, int locX, int locY)
		{
			int layerIndex = getLayerIndex(segLayer);

			MapSegment mapSegment = new MapSegment( segmentLayers[layerIndex].Count, segLayer);
			mapSegment.location.X = locX;
			mapSegment.location.Y = locY;

			int dWidth = (int) (segDef[palletSegIndex].SourceRect.Width * mapSegment.Scale);
			int dHeight = (int) (segDef[palletSegIndex].SourceRect.Height * mapSegment.Scale);


			mapSegment.DRect = new Rectangle ((int)mapSegment.location.X, (int)mapSegment.location.Y, dWidth, dHeight);
			mapSegment.SRect = segDef [palletSegIndex].SourceRect;


			segmentLayers[layerIndex].Add(mapSegment);

		}*/


		private int getLayerIndex(float segLayer)
		{	
			int layerIndex = 0;
			 
			if (segLayer.CompareTo (BACK_LAYER) == 0) 
			{
				layerIndex = 0;
			}
			else if (segLayer.CompareTo (MID_LAYER) == 0) 
			{
				layerIndex = 1;
			}		
			else if (segLayer.CompareTo (FRONT_LAYER) == 0) 
			{
				layerIndex = 2;
			}

			return layerIndex;
		}




		public void Draw(SpriteBatch spriteBatch, Texture2D mapTex)
		{
			spriteBatch.Begin (SpriteSortMode.FrontToBack, BlendState.AlphaBlend);

			foreach (MapSegment seg in this.mapSeg) 
			{
				

				//int sWidth = (int) (segDef[seg.Index].SourceRect.Width * seg.Scale);
				//int sHeight = (int) (segDef[seg.Index].SourceRect.Height * seg.Scale);

				int sWidth = (int) (seg.SRect.Width * seg.Scale);
				int sHeight = (int) (seg.SRect.Height * seg.Scale);

				seg.DRect = new Rectangle ((int)seg.location.X, (int)seg.location.Y, sWidth, sHeight);
				//Rectangle sRect = segDef [seg.Index].SourceRect;

				//Vector2 origin = new Vector2 (sRect.Width / 2, sRect.Height / 2);

				Vector2 origin = new Vector2 (seg.SRect.Width / 2, seg.SRect.Height / 2);

				//spriteBatch.Draw(mapTex, seg.DRect, sRect, Color.White);
				//spriteBatch.Draw(mapTex, seg.DRect, sRect, seg.Color ,0f,origin, SpriteEffects.None, seg.Layer); 
				//spriteBatch.Draw(playerBoundsTex,playerBounds,null,Color.White,0f, new Vector2(playerBoundsTex.Width/2, playerBounds.Width/2),SpriteEffects.None,0f)
				spriteBatch.Draw(mapTex, seg.DRect, seg.SRect, seg.Color ,0f,origin, SpriteEffects.None, seg.Layer); 
							
			}

			spriteBatch.End ();
		}

		/*public void Draw(SpriteBatch spriteBatch, Texture2D mapTex)
		{
			spriteBatch.Begin (SpriteSortMode.FrontToBack, BlendState.AlphaBlend);

			foreach (List<MapSegment> layer in segmentLayers) 
			{
				foreach (MapSegment seg in layer) 
				{
					

					Vector2 origin = new Vector2 (seg.SRect.Width/ 2, seg.SRect.Height / 2);
					spriteBatch.Draw(mapTex, seg.DRect, seg.SRect, Color.White, 0f, origin, SpriteEffects.None, seg.Layer); 
				}

			}

			spriteBatch.End ();
		}*/




		private void ReadSegmentDefinitions()
		{
			StreamReader s = new StreamReader(@"Content/maps.zdx");

			string t = "";
			int n;
			int currentTex = 0;
			//int curDef = -1;

			Rectangle tRect = new Rectangle();
			string[] split;

			t = s.ReadLine();

			while (!s.EndOfStream)
			{
				t = s.ReadLine();

				if (t.StartsWith("#"))
				{
					if (t.StartsWith("#src"))
					{
						split = t.Split(' ');
						if (split.Length > 1)
						{
							n = Convert.ToInt32(split[1]);
							currentTex = n - 1;
						}
					}
				}
				else
				{
					//curDef++;

					string name = t;

					t = s.ReadLine();
					split = t.Split(' ');

					if (split.Length > 3)
					{
						tRect.X = Convert.ToInt32(split[0]);
						tRect.Y = Convert.ToInt32(split[1]);
						tRect.Width = Convert.ToInt32(split[2]) - tRect.X;
						tRect.Height = Convert.ToInt32(split[3]) - tRect.Y;
					}
					else
						Console.WriteLine("read fail: " + name);

					int tex = currentTex;

					t = s.ReadLine();
					int flags = Convert.ToInt32(t);

					segDef.Add(new SegmentDefinition(name, tex, tRect, flags));
				}
			}
		}

		public List<SegmentDefinition> SegmentDefinitions
		{
			get{ return segDef;}
		
		}



	}
}

