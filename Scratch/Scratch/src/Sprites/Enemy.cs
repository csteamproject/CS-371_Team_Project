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
		public double a, b, yOffset = 52;
		public bool isVisible = true;
		public static int totalZombieCount = 10;
		private readonly int DEFAULT_WALK_START_FRAME = 5, DEFAULT_WALK_END_FRAME = 11,
									  		  EAT_PLAYER_START_FRAME = 12, EAT_PLAYER_END_FRAME = 22;

		public Rectangle BoundingBox { get { return new Rectangle( (int)ePos.X+40, (int)ePos.Y+50, 45, 50); } }

		public Enemy( Texture2D texture, int row, int column, int speed, int health, int millisecondsPerFrame ) : base(texture, row, column) {
			this.speed = speed;
			this.health = health;
			this.startFrame = DEFAULT_WALK_START_FRAME;
			this.endFrame = DEFAULT_WALK_END_FRAME;
			if (speed < 20)
				this.millisecondsPerFrame = 225;
			else
				base.millisecondsPerFrame = millisecondsPerFrame;
		}

		public Boolean checkCollision( Player player ) {
			if (player.BoundingBox.Intersects(this.BoundingBox) || this.BoundingBox.Intersects(player.BoundingBox)) {
				this.startFrame = this.currentFrame = EAT_PLAYER_START_FRAME;
				this.endFrame = EAT_PLAYER_END_FRAME;
				return true;
			} else if (frameReset) {
				frameReset = false;
				this.startFrame = this.currentFrame = DEFAULT_WALK_START_FRAME;
				this.endFrame = DEFAULT_WALK_END_FRAME;
			}
			return false;
		}

		public Boolean checkBulletCollision(src.Bullet bullet){
			if (bullet.BoundingBox.Intersects(this.BoundingBox) || this.BoundingBox.Intersects(bullet.BoundingBox)){
				return true;
			}
			return false;
		}

		public Boolean checkMineCollision(Item mine){
			if (mine.BoundingBox.Intersects(this.BoundingBox) || this.BoundingBox.Intersects(mine.BoundingBox)){
				return true;
			}
			return false;
		}

		public static void LoadContent(ContentManager Content, Random rnd, List<Enemy> zombies) {
			Texture2D zombieTexture = Content.Load<Texture2D>("zombie_0");
			for (int i = 0; i < 1; i++) {
				zombies.Add(new Enemy(zombieTexture, 8, 36, rnd.Next(5, 50), 5, 90));
				zombies[i].initialize(rnd);
			}
		}

		public static void randomSpawn( List<Enemy> zombies, Random rnd, ContentManager Content ) {
			if (zombies.Count < totalZombieCount && rnd.Next(1, 100) == 1) {
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
			this.stopFrame = 1;

			float? angle = null;
			a = (playerPos.X) - ePos.X;
			b = (playerPos.Y - yOffset) - ePos.Y;

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

			if (vert == true){
				if(b > 0 && angle > MathHelper.PiOver2/2) eVel = new Vector2(eVel.X, (eVel.Y / this.speed) * (-1 * (100 - this.speed)));
				if(b < 0 && angle > MathHelper.Pi) eVel = new Vector2(eVel.X, (eVel.Y / this.speed) * (-1 * (100 - this.speed)));
			}
			if (horiz == true){
				if(a > 0 && (angle < MathHelper.PiOver2 || angle > MathHelper.TwoPi)) eVel = new Vector2((eVel.X / this.speed) * (-1 * (100 - this.speed)), eVel.Y);
				if(a < 0 && angle > MathHelper.Pi) eVel = new Vector2((eVel.X / this.speed) * (-1 * (100 - this.speed)), eVel.Y);
			}

			ePos = Vector2.Add(ePos, Vector2.Multiply(eVel, (float)gameTime.ElapsedGameTime.TotalSeconds));
			base.Update(gameTime);
		}
	}
}

//	if (vert == true){
//				if(b > 0 && angle >= MathHelper.PiOver2) eVel = new Vector2(eVel.X, (eVel.Y / this.speed) * (-1 * (100 - this.speed)));
//}
//			if (horiz == true){
//				if (a > 0 && angle<MathHelper.PiOver2) eVel = new Vector2((eVel.X / this.speed) * (-1 * (100 - this.speed)), eVel.Y);
//}