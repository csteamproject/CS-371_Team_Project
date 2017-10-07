using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Scratch {

	public class Game1 : Game {

		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;
		private AnimatedSprite zombie;
		private Player player;

		//KeyboardState keys;

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
			Texture2D zombieTexture = Content.Load<Texture2D>("zombie_0");
			Texture2D playerTexture = Content.Load<Texture2D>("player");
			zombie = new AnimatedSprite(zombieTexture, 8, 36);
			player = new Player(playerTexture, 4, 4);
		}

		protected override void Update( GameTime gameTime ) {
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
				Exit();
			// TODO: Add your update logic here

			if (IsActive) {
				zombie.Update(gameTime);
				player.Update(gameTime);
				base.Update(gameTime);
			}
		}

		protected override void Draw( GameTime gameTime ) {
			graphics.GraphicsDevice.Clear(Color.CornflowerBlue);

			//TODO: Add your drawing code here
			zombie.Draw(spriteBatch, new Vector2(50,50));
			player.Draw(spriteBatch, player.pos);
			base.Draw(gameTime);
		}
	}
}
