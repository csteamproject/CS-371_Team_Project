using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Scratch {
	public class menuScreen {

		private Texture2D startButton;
		private Texture2D exitButton;
		private Texture2D endButton;
		private Vector2 startButtonPos;
		private Vector2 exitButtonPos;
		private Vector2 endButtonPos;

		public enum GameState { StartMenu, EndMenu, Playing }
		public GameState gameState;

		public menuScreen() {
		}

		public void Initialize(GraphicsDeviceManager graphics){
			startButtonPos = new Vector2((graphics.GraphicsDevice.Viewport.Width / 2) - 400, 200);
			exitButtonPos = new Vector2((graphics.GraphicsDevice.Viewport.Width / 2) - 50, 200);
			endButtonPos = new Vector2((graphics.GraphicsDevice.Viewport.Width / 2), 100);
			gameState = GameState.StartMenu;
		}

		public void LoadContent(ContentManager Content){
			startButton = Content.Load<Texture2D>("start");
			exitButton = Content.Load<Texture2D>("exit");
			endButton = Content.Load<Texture2D>("gameover2");
		}

		public void Update(){
			KeyboardState keys = Keyboard.GetState();

			if (gameState == GameState.StartMenu) {
				if (keys.IsKeyDown(Keys.Enter)) {
					gameState = GameState.Playing;
				}
			} else if (gameState == GameState.EndMenu) {
				if (keys.IsKeyDown(Keys.Enter)) {
					//player.lives = 3;
					gameState = GameState.Playing;
				}
			} else {
				gameState = GameState.Playing;
			}
		}

		public void StartDraw(GraphicsDeviceManager graphics, SpriteBatch spriteBatch) {
			graphics.GraphicsDevice.Clear(Color.Crimson);
			spriteBatch.Draw(startButton, startButtonPos, Color.White);
			spriteBatch.Draw(exitButton, exitButtonPos, Color.White);
		}

		public void EndDraw(GraphicsDeviceManager graphics, SpriteBatch spriteBatch) {
			graphics.GraphicsDevice.Clear(Color.Crimson);
			spriteBatch.Draw(endButton, endButtonPos, Color.White);
		}
	}
}
