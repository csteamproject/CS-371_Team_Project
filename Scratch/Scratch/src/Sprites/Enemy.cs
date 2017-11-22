using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Scratch {
	public class Enemy : AnimatedSprite {

		int speed { get; set; }
		int health { get; set; }
		public Vector2 ePos, eVel;
		double a, b, yOffset = 52;
		private readonly int DEFAULT_WALK_START_FRAME = 5, DEFAULT_WALK_END_FRAME = 11,
									  		  EAT_PLAYER_START_FRAME = 12, EAT_PLAYER_END_FRAME = 22;

		public Rectangle BoundingBox { get { return new Rectangle( (int)ePos.X, (int)ePos.Y, 5, 5); } }

		public Enemy( Texture2D texture, int row, int column, int speed, int health, int millisecondsPerFrame ) : base(texture, row, column) {
			this.speed = speed;
			this.health = health;
			if (speed < 20)
				this.millisecondsPerFrame = 225;
			else
				base.millisecondsPerFrame = millisecondsPerFrame;
		}

		public Boolean checkCollision( Player player ) {
			if (player.BoundingBox.Intersects(this.BoundingBox) || this.BoundingBox.Intersects(player.BoundingBox)) {
				//this.startFrame = EAT_PLAYER_START_FRAME;
				//this.endFrame = EAT_PLAYER_END_FRAME;
				return true;
			}
			return false;
		}

		public static void LoadContent(ContentManager Content, Random rnd, List<Enemy> zombies) {
			Texture2D zombieTexture = Content.Load<Texture2D>("zombie_0");
			for (int i = 0; i < 400; i++) {
				zombies.Add(new Enemy(zombieTexture, 8, 36, rnd.Next(5, 50), 5, 90));
				zombies[i].initialize(rnd);
			}
		}

		public static void randomSpawn( List<Enemy> zombies, Random rnd, ContentManager Content ) {
			if (zombies.Count < 500 && rnd.Next(1, 100) == 1) {
				zombies.Add(new Enemy(Content.Load<Texture2D>("zombie_0"), 8, 36, rnd.Next(5, 50), 5, 90));
				int zombieCount = zombies.Count - 1;
				zombies[zombieCount].initialize(rnd);
			}
		}

		public void initialize(Random rnd) {
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

			if (vert == true) eVel = new Vector2(eVel.X, (eVel.Y/this.speed)*(-1*(100-this.speed)));
			if (horiz == true) eVel = new Vector2((eVel.X / this.speed) * (-1 * (100 - this.speed)), eVel.Y);

			ePos = Vector2.Add(ePos, Vector2.Multiply(eVel, (float)gameTime.ElapsedGameTime.TotalSeconds));
			base.Update(gameTime);
		}
	}
}
