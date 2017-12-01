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
		private int Rows; private int Columns;
		public int row { get; set; }
		private Vector2 pos;
		Random rnd = new Random();
//		public Rectangle BoundingBox{get{ if (pos == null) return new Rectangle(rnd.Next(1, 5000), rnd.Next(1, 5000), 5, 5); return new Rectangle((int)pos.X + 10, (int)pos.Y - 50, 20, 20);}}

		/*StopFrame is intended to work as a boolean value, if the player or enemy has stopped the row
		which rendered the last animation is used and the first image is displayed. */
		public int stopFrame { get; set; }

		/*
		 * These values are used to indicate the frame at which an animation starts and ends.
		 * These needed to be new variables because Rows and Columns needs to remain
		 * untouched to divide the provided texture into even rectangles.
		*/
		public int startFrame { get; set; }
		public int endFrame { get; set; }
		public bool frameReset = false;

		public int currentFrame { get; set; }
		int timeSinceLastFrame = 0;
		public int millisecondsPerFrame { get; set; }= 150;

		public AnimatedSprite(Texture2D texture, int rows, int columns) {
			Texture = texture;
			Rows = rows;
			Columns = columns;
			currentFrame = 0;
			endFrame = Rows * Columns;
			startFrame = 0;
		}

		public void Update(GameTime gameTime) {
			timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;

			if (timeSinceLastFrame > millisecondsPerFrame) {
				timeSinceLastFrame -= millisecondsPerFrame;
				currentFrame++;
				frameReset = false;
				if (currentFrame == endFrame) {
					frameReset = true;
					currentFrame = startFrame;
				}
					
			}
		}

		public void Draw( SpriteBatch spriteBatch, Vector2 location ) {
			int width = Texture.Width / Columns;
			int height = Texture.Height / Rows;
			if (stopFrame != 0)
				stopFrame = currentFrame % Columns;

			Rectangle sourceRectangle = new Rectangle(width * stopFrame, height * row, width, height);
			Rectangle destinationRectangle = new Rectangle((int)location.X, (int)location.Y, width, height);

			spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White);

		}
	}
}
