using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Scratch {
	public class Enemy : AnimatedSprite {

		int speed { get; set; }
		int health { get; set; }
		public Vector2 ePos, eVel;
		double a, b, yOffset = 52;

		public Rectangle BoundingBox {
			get {
				return new Rectangle(
					(int)ePos.X,
					(int)ePos.Y,
					5,
					5);

				//	tex.Width,
				//	tex.Height);
			}
		}

		public Enemy( Texture2D texture, int row, int column, int speed, int health, int millisecondsPerFrame ) : base(texture, row, column) {
			this.speed = speed;
			this.health = health;
			base.millisecondsPerFrame = millisecondsPerFrame;
		}

		public void initialize() {
			Random rnd = new Random();
			int xPos = rnd.Next(0, 500);
			int yPos = rnd.Next(0, 700);
			eVel = new Vector2(0, 0);
			ePos = new Vector2(xPos, yPos);
		}

		public void Update( GameTime gameTime, Vector2 playerPos, bool vert, bool horiz ) {
			this.column = 1;

			float? angle = null;
			a = (playerPos.X) - ePos.X;
			b = (playerPos.Y - yOffset) - ePos.Y;
			//c = Math.Sqrt(a * a + b * b);

			if (ePos.X <= playerPos.X)
				angle = (float)Math.Atan(b / a);

			else
				angle = (float)Math.Atan(b / a) + MathHelper.Pi;

			if (a > 0) {
				if (b > 0) this.row = 5;
				else if (b < 0) this.row = 3;
				else this.row = 4;
			} else if (a < 0) {
				if (b > 0) this.row = 7;
				else if (b < 0) this.row = 1;
				else this.row = 6;
			} else {
				if (b > 0) this.row = 1;
				else if (b < 0) this.row = 2;
				else this.row = 3;
			}

			if (angle.HasValue)
				eVel = new Vector2((float)Math.Cos((double)angle) * this.speed, (float)Math.Sin((double)angle) * this.speed);
			else
				eVel = new Vector2(0, 0);

			if (vert == true) eVel = new Vector2(eVel.X, 0);
			if (horiz == true) eVel = new Vector2(0, eVel.Y);

			ePos = Vector2.Add(ePos, Vector2.Multiply(eVel, (float)gameTime.ElapsedGameTime.TotalSeconds));
			base.Update(gameTime);
		}
	}
}
