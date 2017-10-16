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

            /*
                Tile IDs
                1 = default
                2 - 19 = slight color variations of default

            */


          
            // Rows[3].Columns[2].TileID = 1;
           

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
       

            Rows[36].Columns[24].AddHeightTile(54);
            Rows[37].Columns[23].AddHeightTile(54);
            Rows[33].Columns[23].AddHeightTile(54);
            Rows[36].Columns[23].AddHeightTile(53);
            Rows[35].Columns[24].AddHeightTile(54);
            Rows[35].Columns[24].AddHeightTile(54);
            Rows[35].Columns[24].AddHeightTile(51);

            Rows[38].Columns[23].AddHeightTile(51);
            Rows[39].Columns[23].AddHeightTile(50);
            Rows[38].Columns[24].AddHeightTile(55);

            Rows[34].Columns[24].AddHeightTile(54);

            Rows[34].Columns[25].AddHeightTile(62);
            Rows[34].Columns[25].AddHeightTile(61);
            Rows[34].Columns[25].AddHeightTile(63);


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
