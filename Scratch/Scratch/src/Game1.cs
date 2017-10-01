using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Scratch {

	public class Game1 : Game {

		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;
		private AnimatedSprite animatedSprite;

		public Game1() {
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
		}

		protected override void Initialize() {
			//TODO: Add your initialization logic here
			base.Initialize();
		}

		protected override void LoadContent() {
			// Create a new SpriteBatch, which can be used to draw textures.
			spriteBatch = new SpriteBatch(GraphicsDevice);
			Texture2D texture = Content.Load<Texture2D>("zombie_0");
			animatedSprite = new AnimatedSprite(texture, 8, 36);
		}

		protected override void Update( GameTime gameTime ) {
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
				Exit();

			// TODO: Add your update logic here
			if (IsActive) {
				animatedSprite.Update(gameTime);
				base.Update(gameTime);
			}
		}

		protected override void Draw( GameTime gameTime ) {

			graphics.GraphicsDevice.Clear(Color.CornflowerBlue);

			//TODO: Add your drawing code here
			animatedSprite.Draw(spriteBatch, new Vector2(400, 200));
			base.Draw(gameTime);
		}
	}
}
