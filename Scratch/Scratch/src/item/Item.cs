using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Scratch {

	public class Item : AnimatedSprite {

		public Vector2 pos, vel;
		//int xPos, yPos;
		float? angleHold = null;
		public bool isVisible = true;
		Texture2D tex;
		public int itemId;

		public Boolean checkCollision(Player player){
			if (player.BoundingBox.Intersects(this.BoundingBox) || this.BoundingBox.Intersects(player.BoundingBox)){
				//this.startFrame = EAT_PLAYER_START_FRAME;
				//this.endFrame = EAT_PLAYER_END_FRAME;
				return true;
			}
			return false;
		}

		public Item( Texture2D texture, int row, int column ) : base(texture, row, column) {
		}
		public void initialize( Vector2 ePos ) {
			pos.X = ePos.X;
			pos.Y = ePos.Y;
			tex = Texture;
			itemId = 0;
		}

		//tex.Width/2,//tex.Height/2);
		public Rectangle BoundingBox{get{return new Rectangle((int)pos.X,(int)pos.Y,20,20);}}

		public void Update( GameTime gameTime, float? playerAngle, bool vert, bool horiz, float spd, Vector2 pPos ) {
			float? angle = null;
			if (playerAngle.HasValue) {
				if (vert) {
					//if (playerAngle - MathHelper.PiOver2 < 1) angle = (float)3.0 * MathHelper.PiOver2;
					//else if (playerAngle - 3.0 * MathHelper.PiOver2 < 1) angle = MathHelper.PiOver2;
					if (pPos.Y > 200) angle = (float)3.0 * MathHelper.PiOver2;
					else angle = MathHelper.PiOver2;
				} else if (horiz) {
					if (pPos.X > 200) angle = MathHelper.Pi;
					else angle = 0;
				}
			}

			if (angle.HasValue) {

				angleHold = angle;
				vel = new Vector2((float)Math.Cos((double)angle) * spd, (float)Math.Sin((double)angle) * spd);
			} else
				vel = new Vector2(0, 0);

			pos = Vector2.Add(pos, Vector2.Multiply(vel, (float)gameTime.ElapsedGameTime.TotalSeconds));
			base.Update(gameTime);
		}
	}
}