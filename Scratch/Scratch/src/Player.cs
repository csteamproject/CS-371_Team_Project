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

namespace Scratch {
	public class Player : AnimatedSprite {

		private Vector2 vel;
		public float spd = 80;
		public Vector2 pos;

		public Player( Texture2D texture, int row, int column ) : base(texture, row, column) {
		}

		public void initialize() {
			vel = new Vector2(0, 0);
			pos = new Vector2(0, 0);
		}

		public new void Update( GameTime gameTime ) {
			KeyboardState keys = Keyboard.GetState();;

			this.column = 1;
			float? angle = null;
			if (keys.IsKeyDown(Keys.D)) {
				angle = 0;
				this.row = 2;
			} else if (keys.IsKeyDown(Keys.A)) {
				angle = MathHelper.Pi;
				this.row = 1;
			} else if (keys.IsKeyDown(Keys.W)) {
				angle = 3.0f * MathHelper.PiOver2;
				this.row = 3;
			} else if (keys.IsKeyDown(Keys.S)) {
				this.row = 0;
				angle = MathHelper.PiOver2;
			} else {
				this.column = 0;
			}

			if (angle.HasValue)
				vel = new Vector2((float)Math.Cos((double)angle) * spd, (float)Math.Sin((double)angle) * spd);
			else
				vel = new Vector2(0, 0);

			pos = Vector2.Add(pos, Vector2.Multiply(vel, (float)gameTime.ElapsedGameTime.TotalSeconds));
			base.Update(gameTime);
		}
	}
}
