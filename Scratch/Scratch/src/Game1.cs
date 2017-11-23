using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Scratch {

	public class Game1 : Game {

		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;

		private List<Enemy> zombies = new List<Enemy>();

		Random rnd = new Random();

		private Player player;
		private ItemsOnScreen items;
		Vector2 enemyP;

		private Song backGround;
		static int squaresAcross = 17;
		static int squaresDown = 37;

		TileMap myMap = new TileMap(squaresDown, squaresAcross);

		menuScreen gameScreen = new menuScreen();

		public Game1() {
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
		}

		protected override void Initialize() {
			//TODO: Add your initialization logic here
			gameScreen.Initialize(graphics);
			base.Initialize();
		}

		protected override void LoadContent() {
			// Create a new SpriteBatch, which can be used to draw textures.
			spriteBatch = new SpriteBatch(GraphicsDevice);
			gameScreen.LoadContent(Content);
			Texture2D playerTexture = Content.Load<Texture2D>("player");
			Texture2D itemTexture = Content.Load<Texture2D>("hammer1");
			Texture2D[] itemTextureArray = { Content.Load<Texture2D>("piskel2"),
				Content.Load<Texture2D>("gem4"), Content.Load<Texture2D>("hammer5"),
				Content.Load<Texture2D>("hammer2") };
            Tile.TileSetTexture = Content.Load<Texture2D>(@"MapSprite2");
            Texture2D lightAura = Content.Load<Texture2D>(@"lightaura");
            myMap.lightAuracreate(Content);
            items = new ItemsOnScreen();
			zombie = new Enemy(zombieTexture, 8, 36, 50, 5, 90);
			zombie1 = new Enemy(zombieTexture, 8, 36, 40, 5, 90);
			player = new Player(playerTexture, 4, 4);
			items.initialize(itemTextureArray);
			player.initialize();
			Enemy.LoadContent(Content, rnd, zombies);
		}

        protected override void Update( GameTime gameTime ) {

			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
				Exit();

			gameScreen.Update();
			if (gameScreen.gameState == menuScreen.GameState.Playing && IsActive) {
				// TODO: Add your update logic here

				Enemy.randomSpawn(zombies, rnd, Content);

				foreach (Enemy enemy in zombies) {
					enemy.Update(gameTime, player.pos, myMap.camMoveVert, myMap.camMoveHoriz);
					if (rnd.Next(1, 100) % 2 == 1) enemyP = enemy.ePos;
					else enemyP = enemy.ePos;
					items.Update(gameTime, player.angle, myMap.camMoveVert, myMap.camMoveHoriz, player.spd, player.pos, enemyP);

					base.Update(gameTime);
					myMap.Update(gameTime, player.pos, this.GraphicsDevice, player);

					if (enemy.checkCollision(player)) {
						player.lives--;
						player.pos.X = rnd.Next(500);
						player.pos.Y = rnd.Next(500);
					}
				}

				player.Update(gameTime, this.GraphicsDevice, myMap.camMoveVert, myMap.camMoveHoriz);

				
			}
		}

		protected override void Draw( GameTime gameTime ) {
			spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
			if (gameScreen.gameState == menuScreen.GameState.StartMenu) {
				gameScreen.StartDraw(graphics, spriteBatch);
			} else if (gameScreen.gameState == menuScreen.GameState.EndMenu) {
				gameScreen.EndDraw(graphics, spriteBatch);
			} else {


				myMap.Draw(graphics, spriteBatch, player);
				items.Draw(spriteBatch);
				foreach (Enemy enemy in zombies) {
					enemy.Draw(spriteBatch, enemy.ePos);
				}
				player.Draw(spriteBatch, player.pos);
				base.Draw(gameTime);


			}
			spriteBatch.End();
		}
	}
}
