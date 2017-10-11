using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Scratch {
	public class Enemy : AnimatedSprite {

		int speed { get; set; } int health { get; set; }

		public Enemy( Texture2D texture, int row, int column, int speed, int health, int millisecondsPerFrame) : base(texture, row, column) {
			this.speed = speed;
			this.health = health;
			base.millisecondsPerFrame = millisecondsPerFrame;
		}

		public new void Update(GameTime gameTime) {
			this.column = 1;
			base.Update(gameTime);
		}

	}
}
