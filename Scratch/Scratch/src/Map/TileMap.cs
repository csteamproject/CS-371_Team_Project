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
		public int MapWidth = 50;
		public int MapHeight = 50;
       

        public Texture2D text;
		float lastCX, lastCY;
		public bool camMoveVert, camMoveHoriz;

		int squaresDown, squaresAcross;

		public TileMap(int squaresDown, int squaresAcross)
		{
			for (int y = 0; y < MapHeight; y++)
			{
				MapRow thisRow = new MapRow();
				for (int x = 0; x < MapWidth; x++)
				{
					thisRow.Columns.Add(new MapCell(0));
				}
				Rows.Add(thisRow);
			}

			this.squaresDown = squaresDown;
			this.squaresAcross = squaresAcross;

            // Create Sample Map Data
            Rows[0].Columns[0].TileID = 6;
            
            Rows[0].Columns[3].TileID = 1;
            Rows[0].Columns[4].TileID = 1;
            Rows[0].Columns[5].TileID = 1;
            Rows[0].Columns[6].TileID = 1;
            Rows[0].Columns[7].TileID = 1;

            Rows[1].Columns[3].TileID = 1;
            Rows[1].Columns[3].TileID = 78;
            Rows[1].Columns[4].TileID = 1;
            Rows[1].Columns[5].TileID = 1;
            Rows[1].Columns[6].TileID = 1;
            Rows[1].Columns[7].TileID = 1;

            Rows[2].Columns[2].TileID = 1;
            Rows[2].Columns[3].TileID = 1;
            Rows[2].Columns[4].TileID = 1;
            Rows[2].Columns[5].TileID = 1;
            Rows[2].Columns[6].TileID = 1;
            Rows[2].Columns[7].TileID = 1;

            Rows[3].Columns[2].TileID = 1;
            Rows[3].Columns[3].TileID = 1;
            Rows[3].Columns[4].TileID = 1;
            Rows[3].Columns[5].TileID = 1;
            Rows[3].Columns[6].TileID = 1;
            Rows[3].Columns[7].TileID = 1;

            Rows[4].Columns[2].TileID = 1;
            Rows[4].Columns[3].TileID = 1;
            Rows[4].Columns[4].TileID = 1;
            Rows[4].Columns[5].TileID = 2;
            Rows[4].Columns[6].TileID = 2;
            Rows[4].Columns[7].TileID = 2;
            Rows[4].Columns[7].TileID = 130;


            Rows[5].Columns[2].TileID = 1;
            Rows[5].Columns[3].TileID = 1;
            Rows[5].Columns[4].TileID = 1;
            Rows[5].Columns[5].TileID = 1;
            Rows[5].Columns[6].TileID = 1;
            Rows[5].Columns[7].TileID = 1;

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
            Rows[15].Columns[5].AddTopperTile(91);
            Rows[16].Columns[6].AddTopperTile(94);
            Rows[3].Columns[5].AddBaseTile(30);
            Rows[4].Columns[5].AddBaseTile(27);
            Rows[5].Columns[5].AddBaseTile(28);

            Rows[3].Columns[6].AddBaseTile(25);
            Rows[5].Columns[6].AddBaseTile(24);

            Rows[3].Columns[7].AddBaseTile(31);
            Rows[4].Columns[7].AddBaseTile(26);
            Rows[5].Columns[7].AddBaseTile(29);

            Rows[4].Columns[6].AddBaseTile(104);

            // End Create Sample Map Data
        }

		public void Update(GameTime gameTime, Vector2 pPos, GraphicsDevice graphDev)
		{
			camMoveVert = true;
			camMoveHoriz = true;

			lastCX = Camera.Location.X;
			lastCY = Camera.Location.Y;

			KeyboardState ks = Keyboard.GetState();
		

			if (ks.IsKeyDown(Keys.W) && pPos.Y < 110)
				Camera.Location.Y = MathHelper.Clamp(Camera.Location.Y - 2, 0, (this.MapHeight - squaresDown) * 17);

			if (ks.IsKeyDown(Keys.S) && pPos.Y > graphDev.Viewport.Height-200)
				Camera.Location.Y = MathHelper.Clamp(Camera.Location.Y + 2, 0, (this.MapHeight - squaresDown) * 17);

			if (ks.IsKeyDown(Keys.A) && pPos.X < 110)
				Camera.Location.X = MathHelper.Clamp(Camera.Location.X - 2, 0, (this.MapWidth - squaresAcross) * 37);

			if (ks.IsKeyDown(Keys.D) && pPos.X > graphDev.Viewport.Width-200)
				Camera.Location.X = MathHelper.Clamp(Camera.Location.X + 2, 0, (this.MapWidth - squaresAcross) * 37);

			if (Math.Abs(Camera.Location.X - lastCX) < 0.5f)
				camMoveHoriz = false;

			if (Math.Abs(Camera.Location.Y - lastCY) < 0.5f)
				camMoveVert = false;

		}

		public void Draw(SpriteBatch spriteBatch)
		{
			
		}//end draw
	}
}
