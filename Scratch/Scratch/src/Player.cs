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

			float? angle = null;
			if (Keyboard.GetState().IsKeyDown(Keys.D)) {
				angle = 0;
			} else if (Keyboard.GetState().IsKeyDown(Keys.A)) {
				angle = MathHelper.Pi;
			} else if (Keyboard.GetState().IsKeyDown(Keys.W)) {
				angle = 3.0f * MathHelper.PiOver2;
			} else if (Keyboard.GetState().IsKeyDown(Keys.S)) {
				angle = MathHelper.PiOver2;
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
