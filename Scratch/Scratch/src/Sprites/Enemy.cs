using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Scratch {
	public class Enemy : AnimatedSprite {

		int speed { get; set; } int health { get; set; }
		public Vector2 ePos, eVel;
		double a, b;

		public Enemy( Texture2D texture, int row, int column, int speed, int health, int millisecondsPerFrame) : base(texture, row, column) {
			this.speed = speed;
			this.health = health;
			base.millisecondsPerFrame = millisecondsPerFrame;
		}

		public void initialize()
		{
			eVel = new Vector2(0, 0);
			ePos = new Vector2(200, 200);
		}

		public void Update(GameTime gameTime, Vector2 playerPos)
		{
			this.column = 3;

			float? angle = null;
			a = playerPos.X - ePos.X;
			b = playerPos.Y - ePos.Y;
			//c = Math.Sqrt(a * a + b * b);

			if (ePos.X <= playerPos.X)
			{
				angle = (float)Math.Atan(b / a);
				if (ePos.Y < playerPos.Y) this.row = 5;
				else this.row = 3;
			}
			else
			{
				angle = (float)Math.Atan(b / a) + MathHelper.Pi;
				if (ePos.Y < playerPos.Y) this.row = 7;
				else this.row = 1;
			}

			if (angle.HasValue)
				eVel = new Vector2((float)Math.Cos((double)angle) * this.speed, (float)Math.Sin((double)angle) * this.speed);
			else
				eVel = new Vector2(0, 0);

			ePos = Vector2.Add(ePos, Vector2.Multiply(eVel, (float)gameTime.ElapsedGameTime.TotalSeconds));

			base.Update(gameTime);
		}

	}
}
