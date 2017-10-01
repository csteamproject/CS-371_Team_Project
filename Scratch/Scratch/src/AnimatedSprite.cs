using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Scratch {
	public class AnimatedSprite {

		public Texture2D Texture { get; set; }
		public int Rows { get; set; }
		public int Columns { get; set; }
		private int currentFrame;
		private int totalFrames;
		int timeSinceLastFrame = 0;
		int millisecondsPerFrame = 50;

		public AnimatedSprite(Texture2D texture, int rows, int columns) {
			Texture = texture;
			Rows = rows;
			Columns = columns;
			currentFrame = 0;
			totalFrames = Rows * Columns;
		}

		public void Update(GameTime gameTime) {
			timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
			if (timeSinceLastFrame > millisecondsPerFrame) {
				timeSinceLastFrame -= millisecondsPerFrame;
				currentFrame++;
				if (currentFrame == totalFrames)
					currentFrame = 0;
			}
		}

		public void Draw( SpriteBatch spriteBatch, Vector2 location ) {
			int width = Texture.Width / Columns;
			int height = Texture.Height / Rows;
			int row = (int)((float)currentFrame / (float)Columns);
			int column = currentFrame % Columns;

			Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);
			Rectangle destinationRectangle = new Rectangle((int)location.X, (int)location.Y, width, height);

			spriteBatch.Begin();
			spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White);
			spriteBatch.End();
			}
		}
}
