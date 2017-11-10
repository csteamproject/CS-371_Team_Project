using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Scratch {

	public class Game1 : Game {

		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;
		List<Enemy> zombies = new List<Enemy>();
		AnimatedSprite explosion;
		private Player player;
		private ItemsOnScreen items;
		Vector2 enemyP;
		Random rnd = new Random();

		Boolean attack = false;

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
			backGround = Content.Load<Song>("media/background");
			MediaPlayer.Play(backGround);
			Texture2D playerTexture = Content.Load<Texture2D>("player");
			Texture2D explosionTexture = Content.Load<Texture2D>("Explosion");
			Texture2D zombieTexture = Content.Load<Texture2D>("zombie_0");
			Texture2D itemTexture = Content.Load<Texture2D>("hammer1");
			Texture2D[] itemTextureArray = { Content.Load<Texture2D>("piskel2"),
				Content.Load<Texture2D>("gem4"), Content.Load<Texture2D>("hammer5"),
				Content.Load<Texture2D>("hammer2") };
			Tile.TileSetTexture = Content.Load<Texture2D>(@"MapSprite2");
			items = new ItemsOnScreen();

			player = new Player(playerTexture, 4, 4);
			player.initialize();

			explosion = new AnimatedSprite(explosionTexture, 9, 9);

			for (int i = 0; i < 150; i++) {
				zombies.Add(new Enemy(zombieTexture, 8, 36, rnd.Next(5, 50), 5, 90));
				zombies[i].initialize(rnd);
			}

			items.initialize(itemTextureArray);
		}

		protected override void Update( GameTime gameTime ) {

			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
				Exit();

			gameScreen.Update();
			if (gameScreen.gameState == menuScreen.GameState.Playing && IsActive) {
				// TODO: Add your update logic here
				if (zombies.Count < 500 && rnd.Next(1, 100) == 1) {
					zombies.Add(new Enemy(Content.Load<Texture2D>("zombie_0"), 8, 36, rnd.Next(5, 50), 5, 90));
					int zombieCount = zombies.Count - 1;
					zombies[zombieCount].initialize(rnd);
				}

				foreach (Enemy enemy in zombies) {
					enemy.Update(gameTime, player.pos, myMap.camMoveVert, myMap.camMoveHoriz);
					if (rnd.Next(1, 100) % 2 == 1) enemyP = enemy.ePos;
					else enemyP = enemy.ePos;
					items.Update(gameTime, player.angle, myMap.camMoveVert, myMap.camMoveHoriz, player.spd, player.pos, enemyP);

					if (enemy.checkCollision(player)) {
						player.lives--;
						player.pos.X = rnd.Next(500);
						player.pos.Y = rnd.Next(500);
					}
				}

				player.Update(gameTime, this.GraphicsDevice, myMap.camMoveVert, myMap.camMoveHoriz);

				explosion.column = 1;
				explosion.row = 0;
				if (explosion.row != explosion.Rows && explosion.column == explosion.Columns){
					explosion.row++;
					explosion.Rows = explosion.row;
				}
				explosion.Update(gameTime);


				myMap.Update(gameTime, player.pos, this.GraphicsDevice);

				if (player.lives == 0) {
					gameScreen.gameState = menuScreen.GameState.EndMenu;
				}
				base.Update(gameTime);
			}
		}

		protected override void Draw( GameTime gameTime ) {
			spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
			if (gameScreen.gameState == menuScreen.GameState.StartMenu) {
				gameScreen.StartDraw(graphics, spriteBatch);
			} else if (gameScreen.gameState == menuScreen.GameState.EndMenu) {
				gameScreen.EndDraw(graphics, spriteBatch);
			} else {
				myMap.Draw(graphics, spriteBatch);
				items.Draw(spriteBatch);
				foreach (Enemy enemy in zombies) {
					enemy.Draw(spriteBatch, enemy.ePos);
				}
				player.Draw(spriteBatch, player.pos);
				explosion.Draw(spriteBatch, player.pos);
				base.Draw(gameTime);
			}
			spriteBatch.End();
		}
	}
}
