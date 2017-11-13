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
		private Texture2D resumeButton;
		private Vector2 startButtonPos;
		private Vector2 exitButtonPos;
		private Vector2 endButtonPos;
		private Vector2 resumeButtonPos;
		private SpriteFont font;
		private Vector2 fontPos; 

		public enum GameState { StartMenu, EndMenu, Playing, Paused,}
		public GameState gameState;

		public menuScreen() {
		}

		public void Initialize(GraphicsDeviceManager graphics){
			startButtonPos = new Vector2((graphics.GraphicsDevice.Viewport.Width / 2) - 400, 200);
			exitButtonPos = new Vector2((graphics.GraphicsDevice.Viewport.Width / 2) - 50, 200);
			endButtonPos = new Vector2((graphics.GraphicsDevice.Viewport.Width / 2), 200);
			resumeButtonPos = new Vector2((graphics.GraphicsDevice.Viewport.Width / 2), 100);
			fontPos = new Vector2 (0, 100); 
			gameState = GameState.StartMenu;
		}

		public void LoadContent(ContentManager Content){
			startButton = Content.Load<Texture2D>("start");
			exitButton = Content.Load<Texture2D>("exit");
			endButton = Content.Load<Texture2D>("gameover2");
			resumeButton = Content.Load<Texture2D>("resume");
			font = Content.Load<SpriteFont> ("font");

		}

		public void Update(){
			KeyboardState keys = Keyboard.GetState();

			if (gameState == GameState.StartMenu) {
				if (keys.IsKeyDown (Keys.Enter)) {
					gameState = GameState.Playing;
				}
			} else if (gameState == GameState.EndMenu) {
				if (keys.IsKeyDown (Keys.Enter)) {
					//player.lives = 3;
					gameState = GameState.Playing;
				}
			} else if (gameState == GameState.Paused) {
				if (keys.IsKeyDown (Keys.R)) {
					gameState = GameState.Playing;
				}
			} else {
				gameState = GameState.Playing;
				if (keys.IsKeyDown (Keys.P)) {
					gameState = GameState.Paused;
				}

			}
		}

		public void StartDraw(GraphicsDeviceManager graphics, SpriteBatch spriteBatch) {
			graphics.GraphicsDevice.Clear(Color.Crimson);
			spriteBatch.Draw(startButton, startButtonPos, Color.White);
			spriteBatch.Draw(exitButton, exitButtonPos, Color.White);
			string text = "Push enter to play, (a-s-w-d) player movement, p to pause";
			spriteBatch.DrawString (font, text, fontPos, Color.Black);
		}

		public void ResumeDraw (GraphicsDeviceManager graphics, SpriteBatch spriteBatch) {
			graphics.GraphicsDevice.Clear (Color.Crimson);
			spriteBatch.Draw (resumeButton, resumeButtonPos, Color.White);
			string text = "Push r to resume play";
			spriteBatch.DrawString (font, text, fontPos, Color.Black);
		}

		public void EndDraw(GraphicsDeviceManager graphics, SpriteBatch spriteBatch) {
			graphics.GraphicsDevice.Clear(Color.Crimson);
			spriteBatch.Draw(endButton, endButtonPos, Color.White);
			string text = "Push enter to play again, (a-s-w-d) player movement, p to pause";
			spriteBatch.DrawString (font, text, fontPos, Color.Black);
		}
	}
}
