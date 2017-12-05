using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Scratch {
	public class menuScreen {
		//buttons made of Texture2D
		private Texture2D startButton;
		private Texture2D endButton;
		private Texture2D resumeButton;

		//positions of buttons on screen 
		private Vector2 startButtonPos;
		private Vector2 endButtonPos;
		private Vector2 resumeButtonPos;
		private Vector2 fontPos;

		//font and media used 
		public SpriteFont font;
		private Song backGround;

		//create different gamestates 
		public enum GameState { StartMenu, EndMenu, Playing, Paused,}
		public GameState gameState;

		public menuScreen() {
		}

		/**
		 * @param- graphics used to get the viewport of the window 
		 * initialize the positions and gamestate 
		 **/
		public void Initialize(GraphicsDeviceManager graphics){
			startButtonPos = new Vector2((graphics.GraphicsDevice.Viewport.Width / 2) - 400, 200);
			endButtonPos = new Vector2((graphics.GraphicsDevice.Viewport.Width / 2) - 250, 200);
			resumeButtonPos = new Vector2((graphics.GraphicsDevice.Viewport.Width / 2), 100);
			fontPos = new Vector2 (10, 10); 
			gameState = GameState.StartMenu;
		}

		/**
		 * @param- ContentManager where all our content is stored
		 * will load the pictures I need, font, and song
		 **/
		public void LoadContent(ContentManager Content){
			startButton = Content.Load<Texture2D>("menus/start");
			endButton = Content.Load<Texture2D>("menus/generatedtext");
			resumeButton = Content.Load<Texture2D>("menus/resume");
			font = Content.Load<SpriteFont> ("sprites/font");
			backGround = Content.Load<Song>("media/background");
			MediaPlayer.Play(backGround);//starts the song 

		}
		//determine the games states from user input 
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
		/**
		 * @param- graphics, spritebatch
		 * graphics is used to clear the background of the window and set to black
		 * spritebatch is used to draw the buttons and string 
		 */
		public void StartDraw(GraphicsDeviceManager graphics, SpriteBatch spriteBatch) {
			graphics.GraphicsDevice.Clear(Color.Black);
			spriteBatch.Draw(startButton, startButtonPos, Color.White);
			string text = "Push enter to play\n(a-s-w-d) player movement\nc to combine items in inventory \nf to use first aid\ne to drop mine\np to pause\nspace to fire bullet\n";
			spriteBatch.DrawString (font, text, fontPos, Color.Red);
		}

		public void ResumeDraw (GraphicsDeviceManager graphics, SpriteBatch spriteBatch) {
			graphics.GraphicsDevice.Clear (Color.Black);
			spriteBatch.Draw (resumeButton, resumeButtonPos, Color.White);
			string text = "Push r to resume\n(a-s-w-d) player movement\nc to combine items in inventory \nf to use first aid\ne to drop mine\np to pause\nspace to fire bullet\n";
			spriteBatch.DrawString (font, text, fontPos, Color.Red);
		}

		public void EndDraw(GraphicsDeviceManager graphics, SpriteBatch spriteBatch) {
			graphics.GraphicsDevice.Clear(Color.Black);
			spriteBatch.Draw(endButton, endButtonPos, Color.White);
			string text = "Push enter to play\n(a-s-w-d) player movement\nc to combine items in inventory \nf to use first aid\ne to drop mine\np to pause\nspace to fire bullet\n";
			spriteBatch.DrawString (font, text, fontPos, Color.Red);
		}
	}
}
