using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Scratch {

	public class NotificationWindow {

		SpriteFont Font1;
		Vector2 FontPos;
		string message;

		public NotificationWindow(String message) {
			this.message = message;
		}

		public void LoadContent(GraphicsDeviceManager graphics, ContentManager Content) {
			Font1 = Content.Load<SpriteFont>("defaultfont");
			FontPos = new Vector2(graphics.GraphicsDevice.Viewport.Width / 2,
			graphics.GraphicsDevice.Viewport.Height / 2);
		}

		public void Draw(SpriteBatch spriteBatch) {
			spriteBatch.Begin();
			spriteBatch.DrawString(Font1, message, FontPos, Color.Black);
			spriteBatch.End();
		}
	}
}
