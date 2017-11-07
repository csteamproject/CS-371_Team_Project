using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Scratch
{

	public class Game1 : Game
	{

		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;
		private Enemy zombie, zombie1;
		private Player player;
		private ItemsOnScreen items;
		Vector2 enemyP;

		private Texture2D startButton;
		private Texture2D exitButton;
		private Texture2D endButton;
		private Vector2 startButtonPos;
		private Vector2 exitButtonPos;
		private Vector2 endButtonPos; 
		enum GameState{StartMenu,EndMenu,Playing}
		GameState gameState; 
		private Song backGround;

		static int squaresAcross = 17;
		static int squaresDown = 37;
		TileMap myMap = new TileMap(squaresDown, squaresAcross);

		public Game1()
		{
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
		}

		protected override void Initialize()
		{
			//TODO: Add your initialization logic here
			startButtonPos = new Vector2((GraphicsDevice.Viewport.Width / 2)-400, 200);
			exitButtonPos = new Vector2((GraphicsDevice.Viewport.Width / 2) -50, 200);
			endButtonPos = new Vector2((GraphicsDevice.Viewport.Width/2), 100);
			gameState = GameState.StartMenu;
			base.Initialize();
		}

		protected override void LoadContent()
		{
			// Create a new SpriteBatch, which can be used to draw textures.
			spriteBatch = new SpriteBatch(GraphicsDevice);
			startButton = Content.Load<Texture2D>("start");
			exitButton = Content.Load<Texture2D>("exit");
			endButton = Content.Load<Texture2D>("gameover2");
			backGround = Content.Load<Song>("media/background");
			MediaPlayer.Play(backGround);
			Texture2D zombieTexture = Content.Load<Texture2D>("zombie_0");
			Texture2D playerTexture = Content.Load<Texture2D>("player");
			Texture2D itemTexture = Content.Load<Texture2D>("hammer1");
			Texture2D[] itemTextureArray = { Content.Load<Texture2D>("piskel2"),
				Content.Load<Texture2D>("gem4"), Content.Load<Texture2D>("hammer5"),
				Content.Load<Texture2D>("hammer2") };
			Tile.TileSetTexture = Content.Load<Texture2D>(@"MapSprite2");
			items = new ItemsOnScreen();
			zombie = new Enemy(zombieTexture, 8, 36, 50, 5, 90);
			zombie1 = new Enemy(zombieTexture, 8, 36, 40, 5, 90);
			player = new Player(playerTexture, 4, 4);
			items.initialize(itemTextureArray);
			player.initialize();
			zombie.initialize();
		}

		protected override void Update(GameTime gameTime)
		{
			if (gameState == GameState.StartMenu)
			{
				KeyboardState keys = Keyboard.GetState();

				if (keys.IsKeyDown(Keys.Enter))
				{

					gameState = GameState.Playing;

				}
				if (keys.IsKeyDown(Keys.Back))
				{
					Exit();
				}
			}


			if (gameState == GameState.EndMenu)
			{
				KeyboardState keys = Keyboard.GetState();

				if (keys.IsKeyDown(Keys.Enter))
				{
					player.lives = 3; 
					gameState = GameState.Playing;

				}
				if (keys.IsKeyDown(Keys.Back))
				{
					Exit();
				}
			}
					



			if (gameState == GameState.Playing){

				if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
					Exit();
				// TODO: Add your update logic here
				if (IsActive)
				{
					//				Texture2D tex = myMap.text;
					player.Update(gameTime, this.GraphicsDevice, myMap.camMoveVert, myMap.camMoveHoriz);
					zombie.Update(gameTime, player.pos, myMap.camMoveVert, myMap.camMoveHoriz);
					zombie1.Update(gameTime, player.pos, myMap.camMoveVert, myMap.camMoveHoriz);
					Random rnd = new Random();
					if (rnd.Next(1, 100) % 2 == 1) enemyP = zombie.ePos;
					else enemyP = zombie1.ePos;
					items.Update(gameTime, player.angle, myMap.camMoveVert, myMap.camMoveHoriz, player.spd, player.pos, enemyP);
					base.Update(gameTime);
					myMap.Update(gameTime, player.pos, this.GraphicsDevice);

					if (player.BoundingBox.Intersects(zombie.BoundingBox) || player.BoundingBox.Intersects(zombie1.BoundingBox))
					{
						player.lives--;
						if (player.lives == 0)
							gameState = GameState.EndMenu; 
							//Exit();
						Random rnd1 = new Random();
						player.pos.X = rnd1.Next(500);
						player.pos.Y = rnd1.Next(500);

					}
				}
			}
		}

		protected override void Draw(GameTime gameTime)
		{
			spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
			if (gameState == GameState.StartMenu)
			{
				graphics.GraphicsDevice.Clear(Color.Crimson);
				spriteBatch.Draw(startButton, startButtonPos, Color.White);
				spriteBatch.Draw(exitButton, exitButtonPos, Color.White);
			}

			else if (gameState == GameState.EndMenu)
			{
				graphics.GraphicsDevice.Clear(Color.Crimson);
				spriteBatch.Draw(endButton, endButtonPos, Color.White);
			}



			else if (gameState == GameState.Playing)
			{
				
				myMap.Draw(graphics, spriteBatch);
				items.Draw(spriteBatch);
				zombie.Draw(spriteBatch, zombie.ePos);
				zombie1.Draw(spriteBatch, zombie1.ePos);
				player.Draw(spriteBatch, player.pos);
				base.Draw(gameTime);

			}
			spriteBatch.End();
		}
	}
}
