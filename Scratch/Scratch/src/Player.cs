/*
* Player.cs
* The purpose of this class is to generate attributes for the player in the game.
* Player.cs inherits from AnimatedSprite.cs to build a working sprite animation.
* This class also sets up the keyboard functionality for W, A, S, and D keys.
*/

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Scratch
{
	public class Player : AnimatedSprite
	{

		private Vector2 vel;
		public float spd { get; set; } = 100;
		public Vector2 pos;
		Texture2D tex;
		int width = 900;
		int height = 625;
		public float? angle;
		public int lives = 10;

		public Rectangle BoundingBox
		{
			get
			{
				return new Rectangle(
					(int)pos.X,
					(int)pos.Y - 52,
					5,
					5);
				//	tex.Width,
				//	tex.Height);
			}
		}


		public Player(Texture2D texture, int row, int column) : base(texture, row, column)
		{
			tex = texture;
		}

		public void initialize()
		{
			vel = new Vector2(0, 0);
			pos = new Vector2(0, 0);
		}

		public void Update(GameTime gameTime, GraphicsDevice graphDev, bool vert, bool horiz)
		{

			KeyboardState keys = Keyboard.GetState(); ;

			this.column = 1;
			angle = null;
			if (keys.IsKeyDown(Keys.D))
			{
				angle = 0;
				this.row = 2;
			}
			else if (keys.IsKeyDown(Keys.A))
			{
				angle = MathHelper.Pi;
				this.row = 1;
			}
			else if (keys.IsKeyDown(Keys.W))
			{
				angle = 3.0f * MathHelper.PiOver2;
				this.row = 3;
			}
			else if (keys.IsKeyDown(Keys.S))
			{
				this.row = 0;
				angle = MathHelper.PiOver2;
			}
			else
			{
				this.column = 0;
			}

			if (angle.HasValue)
				vel = new Vector2((float)Math.Cos((double)angle) * spd, (float)Math.Sin((double)angle) * spd);
			else
				vel = new Vector2(0, 0);

			pos = Vector2.Add(pos, Vector2.Multiply(vel, (float)gameTime.ElapsedGameTime.TotalSeconds));

			if (horiz == false)
			{
				if (pos.X + tex.Width > width)
					pos.X = width - tex.Width;
				if (pos.X < 0)
					pos.X = 0;
			}
			else
			{
				if (pos.X + tex.Width > graphDev.Viewport.Width)
					pos.X = graphDev.Viewport.Width - tex.Width;
				if (pos.X < 100)
					pos.X = 100;
			}

			if (vert == false)
			{
				if (pos.Y + tex.Height > height)
					pos.Y = height - tex.Height;
				if (pos.Y < 0)
					pos.Y = 0;
			}
			else
			{
				if (pos.Y + tex.Height > graphDev.Viewport.Height)
					pos.Y = graphDev.Viewport.Height - tex.Height;
				if (pos.Y < 100)
					pos.Y = 100;
			}



			base.Update(gameTime);
		}
	}
}