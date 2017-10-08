using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Scratch
{
    class MapRow
    {
        public List<MapCell> Columns = new List<MapCell>();
    }

    class TileMap
    {
        public List<MapRow> Rows = new List<MapRow>();
        public int MapWidth = 64;
        public int MapHeight = 50;

		int squaresDown, squaresAcross;

        public TileMap(int squaresDown, int squaresAcross) {
            for (int y = 0; y < MapHeight; y++) {
                MapRow thisRow = new MapRow();
                for (int x = 0; x < MapWidth; x++) {
                    thisRow.Columns.Add(new MapCell(0));
                }
                Rows.Add(thisRow);
            }

			this.squaresDown = squaresDown;
			this.squaresAcross = squaresAcross;

            // Create Sample Map Data
            Rows[0].Columns[3].TileID = 3;
            Rows[0].Columns[4].TileID = 3;
            Rows[0].Columns[5].TileID = 1;
            Rows[0].Columns[6].TileID = 1;
            Rows[0].Columns[7].TileID = 1;

            Rows[1].Columns[3].TileID = 3;
            Rows[1].Columns[4].TileID = 1;
            Rows[1].Columns[5].TileID = 1;
            Rows[1].Columns[6].TileID = 1;
            Rows[1].Columns[7].TileID = 1;

            Rows[2].Columns[2].TileID = 3;
            Rows[2].Columns[3].TileID = 1;
            Rows[2].Columns[4].TileID = 1;
            Rows[2].Columns[5].TileID = 1;
            Rows[2].Columns[6].TileID = 1;
            Rows[2].Columns[7].TileID = 1;

            Rows[3].Columns[2].TileID = 3;
            Rows[3].Columns[3].TileID = 1;
            Rows[3].Columns[4].TileID = 1;
            Rows[3].Columns[5].TileID = 2;
            Rows[3].Columns[6].TileID = 2;
            Rows[3].Columns[7].TileID = 2;

            Rows[4].Columns[2].TileID = 3;
            Rows[4].Columns[3].TileID = 1;
            Rows[4].Columns[4].TileID = 1;
            Rows[4].Columns[5].TileID = 2;
            Rows[4].Columns[6].TileID = 2;
            Rows[4].Columns[7].TileID = 2;

            Rows[5].Columns[2].TileID = 3;
            Rows[5].Columns[3].TileID = 1;
            Rows[5].Columns[4].TileID = 1;
            Rows[5].Columns[5].TileID = 2;
            Rows[5].Columns[6].TileID = 2;
            Rows[5].Columns[7].TileID = 2;

            // End Create Sample Map Data
        }

		public void Update(GameTime gameTime) {
			KeyboardState ks = Keyboard.GetState();
			if (ks.IsKeyDown(Keys.Left)) {
				Camera.Location.X = MathHelper.Clamp(Camera.Location.X - 2, 0, (this.MapWidth - squaresAcross) * 32);
			}

			if (ks.IsKeyDown(Keys.Right)) {
				Camera.Location.X = MathHelper.Clamp(Camera.Location.X + 2, 0, (this.MapWidth - squaresAcross) * 32);
			}

			if (ks.IsKeyDown(Keys.Up)) {
				Camera.Location.Y = MathHelper.Clamp(Camera.Location.Y - 2, 0, (this.MapHeight - squaresDown) * 32);
			}

			if (ks.IsKeyDown(Keys.Down)) {
				Camera.Location.Y = MathHelper.Clamp(Camera.Location.Y + 2, 0, (this.MapHeight - squaresDown) * 32);
			}
		}

		public void Draw(SpriteBatch spriteBatch) {
			Vector2 firstSquare = new Vector2(Camera.Location.X / 32, Camera.Location.Y / 32);
			int firstX = (int)firstSquare.X;
			int firstY = (int)firstSquare.Y;

			Vector2 squareOffset = new Vector2(Camera.Location.X % 32, Camera.Location.Y % 32);
			int offsetX = (int)squareOffset.X;
			int offsetY = (int)squareOffset.Y;

			for (int y = 0; y < squaresDown; y++) {
				for (int x = 0; x < squaresAcross; x++) {
					spriteBatch.Begin();
					spriteBatch.Draw(
						Tile.TileSetTexture,
						new Rectangle((x * 32) - offsetX, (y * 32) - offsetY, 32, 32),
						Tile.GetSourceRectangle(this.Rows[y + firstY].Columns[x + firstX].TileID),
						Color.White);
					spriteBatch.End();
				}
			}
		}
    }
}
