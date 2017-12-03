using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

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
		private Song backGround;

		public enum GameState { StartMenu, EndMenu, Playing, Paused,}
		public GameState gameState;

		public menuScreen() {
		}

		public void Initialize(GraphicsDeviceManager graphics){
			startButtonPos = new Vector2((graphics.GraphicsDevice.Viewport.Width / 2) - 400, 200);
			exitButtonPos = new Vector2((graphics.GraphicsDevice.Viewport.Width / 2) - 50, 200);
			endButtonPos = new Vector2((graphics.GraphicsDevice.Viewport.Width / 2) - 250, 200);
			resumeButtonPos = new Vector2((graphics.GraphicsDevice.Viewport.Width / 2), 100);
			fontPos = new Vector2 (10, 10); 
			gameState = GameState.StartMenu;
		}

		public void LoadContent(ContentManager Content){
			startButton = Content.Load<Texture2D>("start");
			exitButton = Content.Load<Texture2D>("exit");
			endButton = Content.Load<Texture2D>("menus/generatedtext");
			resumeButton = Content.Load<Texture2D>("resume");
			font = Content.Load<SpriteFont> ("font");
			backGround = Content.Load<Song>("media/background");
			MediaPlayer.Play(backGround);

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
			string text = "Push enter to play\n(a-s-w-d) player movement\nc to combine items in inventory \nf to use first aid\nr to drop mine\np to pause\nspace to fire bullet\n";
			spriteBatch.DrawString (font, text, fontPos, Color.Black);
		}

		public void ResumeDraw (GraphicsDeviceManager graphics, SpriteBatch spriteBatch) {
			graphics.GraphicsDevice.Clear (Color.Crimson);
			spriteBatch.Draw (resumeButton, resumeButtonPos, Color.White);
			string text = "Push r to resume\n(a-s-w-d) player movement\nc to combine items in inventory \nf to use first aid\nr to drop mine\np to pause\nspace to fire bullet\n";
			spriteBatch.DrawString (font, text, fontPos, Color.Black);
		}

		public void EndDraw(GraphicsDeviceManager graphics, SpriteBatch spriteBatch) {
			graphics.GraphicsDevice.Clear(Color.Black);
			spriteBatch.Draw(endButton, endButtonPos, Color.White);
			string text = "Push enter to play\n(a-s-w-d) player movement\nc to combine items in inventory \nf to use first aid\nr to drop mine\np to pause\nspace to fire bullet\n";
			spriteBatch.DrawString (font, text, fontPos, Color.Black);
		}
	}
}
