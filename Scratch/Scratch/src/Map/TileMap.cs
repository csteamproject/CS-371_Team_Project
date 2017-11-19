using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Scratch {

	class MapRow {
		public List<MapCell> Columns = new List<MapCell>();
	}

	class TileMap {
		public List<MapRow> Rows = new List<MapRow>();
		public int MapWidth = 50;
		public int MapHeight = 50;
		static int baseOffsetX = -32;
		static int baseOffsetY = -64;
		static float heightRowDepthMod = 0.00001f;

		float lastCX, lastCY;
		public bool camMoveVert, camMoveHoriz;
		readonly int squaresAcross;
		readonly int squaresDown;
        readonly Texture2D lightAura; 

		public TileMap( int squaresDown, int squaresAcross) {
			for (int y = 0; y < MapHeight; y++) {
				MapRow thisRow = new MapRow();

				for (int x = 0; x < MapWidth; x++)
					thisRow.Columns.Add(new MapCell(0));

				Rows.Add(thisRow);
			}

			this.squaresDown = squaresDown;
			this.squaresAcross = squaresAcross;
           
            


			/*
                Tile IDs

                0 = default
                1 - 6 = row 1
                10 - 19 = row 2
                20 - 29 =  row 3
                30 - 39 = row 4, dirt elevated things
                40 - 49 = row 5, dirt elevated things (cont)
                50 - 59 = row 6, big rocks
                60 - 69 = row 7, big water rocks/ little rocks
                70 - 79 = row 8, big elevation rocks with dirt on top
                80 - 109 = row 9, 10, and 11, water
                110 - 119 = row 12, grass
                120 - 129 = row 13, bushes and top of trees
                132- 133 = row 14, bottom of trees and tops of tippy top of big trees
                140 - 149 = row 15, top of big trees
                150 - 159 = row 16, bottom of big trees
            */

			//Tree
			Rows[2].Columns[8].AddHeightTile(130);
			Rows[6].Columns[8].AddHeightTile(140);
			Rows[10].Columns[8].AddHeightTile(150);
			//end Tree

			//Dead tree
			Rows[2].Columns[9].AddHeightTile(131);
			Rows[6].Columns[9].AddHeightTile(141);
			Rows[10].Columns[9].AddHeightTile(151);
			//end Dead Tree
			//Dead Tree
			Rows[1].Columns[8].AddHeightTile(131);
			Rows[5].Columns[8].AddHeightTile(141);
			Rows[9].Columns[8].AddHeightTile(151);
			//End Dead Tree
			//mountain thing
			Rows[16].Columns[4].AddHeightTile(54);
			Rows[17].Columns[3].AddHeightTile(54);
			Rows[15].Columns[3].AddHeightTile(54);
			Rows[16].Columns[3].AddHeightTile(53);
			Rows[15].Columns[4].AddHeightTile(54);
			Rows[15].Columns[4].AddHeightTile(54);
			Rows[15].Columns[4].AddHeightTile(51);

			Rows[18].Columns[3].AddHeightTile(51);
			Rows[19].Columns[3].AddHeightTile(50);
			Rows[18].Columns[4].AddHeightTile(55);

			Rows[14].Columns[4].AddHeightTile(54);

			Rows[14].Columns[5].AddHeightTile(62);
			Rows[14].Columns[5].AddHeightTile(61);
			Rows[14].Columns[5].AddHeightTile(63);

			Rows[17].Columns[4].AddTopperTile(114);
			Rows[16].Columns[5].AddTopperTile(115);
			Rows[14].Columns[4].AddTopperTile(125);
			Rows[15].Columns[5].AddTopperTile(85);
			Rows[16].Columns[6].AddTopperTile(85);
			Rows[16].Columns[7].AddTopperTile(85);
			Rows[15].Columns[6].AddTopperTile(85);
			Rows[15].Columns[5].AddTopperTile(85);
			Rows[15].Columns[6].AddTopperTile(85);
			Rows[15].Columns[7].AddTopperTile(85);
			Rows[16].Columns[5].AddTopperTile(85);
			Rows[14].Columns[7].AddTopperTile(85);
			Rows[13].Columns[6].AddTopperTile(114);
			Rows[17].Columns[6].AddTopperTile(114);
			Rows[17].Columns[7].AddTopperTile(32);
			Rows[16].Columns[8].AddTopperTile(32);
			Rows[14].Columns[6].AddHeightTile(56);
			Rows[13].Columns[7].AddTopperTile(56);
			//end mountain/lake thing

			//random rocks
			Rows[25].Columns[25].AddHeightTile(55);
			Rows[26].Columns[25].AddHeightTile(55);
			Rows[27].Columns[25].AddHeightTile(55);
			Rows[25].Columns[26].AddHeightTile(55);
			Rows[24].Columns[27].AddHeightTile(55);
			//end random rocks



		}

		public void Update( GameTime gameTime, Vector2 pPos, GraphicsDevice graphDev ) {

			camMoveVert = true;
			camMoveHoriz = true;

			lastCX = Camera.Location.X;
			lastCY = Camera.Location.Y;

			KeyboardState ks = Keyboard.GetState();

			if (ks.IsKeyDown(Keys.W) && pPos.Y < 110)
				Camera.Location.Y = MathHelper.Clamp(Camera.Location.Y - 2, 0, (this.MapHeight - squaresDown) * 17);
			if (ks.IsKeyDown(Keys.S) && pPos.Y > graphDev.Viewport.Height - 200)
				Camera.Location.Y = MathHelper.Clamp(Camera.Location.Y + 2, 0, (this.MapHeight - squaresDown) * 17);
			if (ks.IsKeyDown(Keys.A) && pPos.X < 110)
				Camera.Location.X = MathHelper.Clamp(Camera.Location.X - 2, 0, (this.MapWidth - squaresAcross) * 37);
			if (ks.IsKeyDown(Keys.D) && pPos.X > graphDev.Viewport.Width - 200)
				Camera.Location.X = MathHelper.Clamp(Camera.Location.X + 2, 0, (this.MapWidth - squaresAcross) * 37);
			if (Math.Abs(Camera.Location.X - lastCX) < 0.5f)
				camMoveHoriz = false;
			if (Math.Abs(Camera.Location.Y - lastCY) < 0.5f)
				camMoveVert = false;

            UpdateFogOfWar();

		}

        private void UpdateFogOfWar() {
            int playerMapX = (int)Camera.Location.X / Tile.TileWidth;
            int playerMapY = (int)Camera.Location.Y / Tile.TileHeight;


            // Loop for "Light Aura" effect:

            for (int y = playerMapY - 3; y <= playerMapY + 3; y++)
                for (int x = playerMapX - 3; x <= playerMapX + 3; x++)
                {
                    if ((x >= 0) && (x < MapWidth) && (y >= 0) && (y < MapHeight))
                    {
                        if ((x >= playerMapX - 2) && (y >= playerMapY - 2) &&
                            (x <= playerMapX + 2) && (y <= playerMapY + 2))
                             Rows[y].Columns[x].Explored = true;
                        else
                             Rows[y].Columns[x].Explored = false;
                    }
                }
        }

        public void Draw( GraphicsDeviceManager graphics, SpriteBatch spriteBatch, ContentManager Content) {

			graphics.GraphicsDevice.Clear(Color.Black);

			Vector2 firstSquare = new Vector2(Camera.Location.X / Tile.TileStepX, Camera.Location.Y / Tile.TileStepY);
			int firstX = (int)firstSquare.X;
			int firstY = (int)firstSquare.Y;

			Vector2 squareOffset = new Vector2(Camera.Location.X % Tile.TileStepX, Camera.Location.Y % Tile.TileStepY);
			int offsetX = (int)squareOffset.X;
			int offsetY = (int)squareOffset.Y;

			float maxdepth = ((this.MapWidth + 1) * ((this.MapHeight + 1) * Tile.TileWidth)) / 10;
			float depthOffset;

			for (int y = 0; y < squaresDown; y++) {
				int rowOffset = 0;
				if ((firstY + y) % 2 == 1)
					rowOffset = Tile.OddRowXOffset;

				for (int x = 0; x < squaresAcross; x++) {
					int mapx = (firstX + x);
					int mapy = (firstY + y);
					depthOffset = 0.7f - ((mapx + (mapy * Tile.TileWidth)) / maxdepth);


					foreach (int tileID in this.Rows[mapy].Columns[mapx].BaseTiles) {
						spriteBatch.Draw(

							Tile.TileSetTexture,
							new Rectangle(
								(x * Tile.TileStepX) - offsetX + rowOffset + baseOffsetX,
								(y * Tile.TileStepY) - offsetY + baseOffsetY,
								Tile.TileWidth, Tile.TileHeight),
							Tile.GetSourceRectangle(tileID),
							Color.White,
							0.0f,
							Vector2.Zero,
							SpriteEffects.None,
							1.0f);
					}

					int heightRow = 0;

					foreach (int tileID in this.Rows[mapy].Columns[mapx].HeightTiles) {
						spriteBatch.Draw(
							Tile.TileSetTexture,
							new Rectangle(
								(x * Tile.TileStepX) - offsetX + rowOffset + baseOffsetX,
								(y * Tile.TileStepY) - offsetY + baseOffsetY - (heightRow * Tile.HeightTileOffset),
								Tile.TileWidth, Tile.TileHeight),
							Tile.GetSourceRectangle(tileID),
							Color.White,
							0.0f,
							Vector2.Zero,
							SpriteEffects.None,
							depthOffset - ((float)heightRow * heightRowDepthMod));
						heightRow++;
					}

					foreach (int tileID in this.Rows[y + firstY].Columns[x + firstX].TopperTiles) {
						spriteBatch.Draw(
							Tile.TileSetTexture,
							new Rectangle(
								(x * Tile.TileStepX) - offsetX + rowOffset + baseOffsetX,
								(y * Tile.TileStepY) - offsetY + baseOffsetY - (heightRow * Tile.HeightTileOffset),
								Tile.TileWidth, Tile.TileHeight),
							Tile.GetSourceRectangle(tileID),
							Color.White,
							0.0f,
							Vector2.Zero,
							SpriteEffects.None,
							depthOffset - ((float)heightRow * heightRowDepthMod));
					}


                }
			}
            //where error occurs
            Content.Load<Texture2D>;
            spriteBatch.Draw(lightAura,
                new Rectangle(
                    (int)Camera.Location.X - (lightAura.Width / 2),
                    (int)Camera.Location.Y - (lightAura.Height / 2),
                    lightAura.Width, lightAura.Height),
                    Color.White);
        }//end draw
	}
}
