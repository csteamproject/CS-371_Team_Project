/*
 * AnimatedSprite.cs
 * The purpose of this class is to build a standard model for any sprite
 * that is either a single texture or an animated sprite sheet. Rows and 
 * columns can be specified for spritesheets using public variables row
 * and column.
*/

using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Scratch {
	public class AnimatedSprite {

		public Texture2D Texture { get; set; }
		public int Rows { get; set; }
		public int Columns { get; set; }
		public int row { get; set; }
		public int column { get; set; }
		private int currentFrame;
		private int totalFrames;
		int timeSinceLastFrame = 0;
		public int millisecondsPerFrame { get; set; }= 150;

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
			//row = (int)((float)currentFrame / (float)Columns);
			if (column != 0)
				column = currentFrame % Columns;

			Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);
			Rectangle destinationRectangle = new Rectangle((int)location.X, (int)location.Y, width, height);

			spriteBatch.Begin();
			spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White);
			spriteBatch.End();
			}
		}
}
