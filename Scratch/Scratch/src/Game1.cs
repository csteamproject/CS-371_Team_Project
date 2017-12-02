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
		private int enemiesDefeated = 0;
		Random rnd = new Random();

		private Player player;
		private ItemsOnScreen items;
		Vector2 enemyP;

        private src.Bullet bullet;

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
			Texture2D[] itemTextureArray = { Content.Load<Texture2D>("cloth"),
				Content.Load<Texture2D>("string"), Content.Load<Texture2D>("gunpowder"),
				Content.Load<Texture2D>("ointment") };
            Tile.TileSetTexture = Content.Load<Texture2D>(@"MapSprite2");
            Texture2D lightAura = Content.Load<Texture2D>(@"lightaura");
            Texture2D bulletTexture = Content.Load<Texture2D>("projectile2");
            myMap.lightAuracreate(Content);
            items = new ItemsOnScreen();
			player = new Player(playerTexture, 4, 4);
			items.initialize(itemTextureArray);
            bullet = new src.Bullet(bulletTexture);
            player.Bulletcreate(bulletTexture);
            player.Initialize();
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
					if (enemy.isVisible){
						enemy.Update(gameTime, player.pos, myMap.camMoveVert, myMap.camMoveHoriz);
						if (rnd.Next(1, 100) % 2 == 1) enemyP = enemy.ePos;
						else enemyP = enemy.ePos;
						items.Update(gameTime, player.angle, myMap.camMoveVert, myMap.camMoveHoriz, player.spd, player.pos, enemyP, false);
						base.Update(gameTime);
						myMap.Update(gameTime, player.pos, this.GraphicsDevice, player);

						if (enemy.isVisible && enemy.checkCollision(player))
						{
							player.lives--;
							player.pos.X = rnd.Next(500);
							player.pos.Y = rnd.Next(500);
						}

						foreach (src.Bullet b in player.bulletList)
							if (enemy.checkBulletCollision(b)){
								enemy.isVisible = false;
								items.Update(gameTime, player.angle, myMap.camMoveVert, myMap.camMoveHoriz, player.spd, player.pos, enemyP, true);
								enemiesDefeated++;
							}
					}

					if (enemiesDefeated >= Enemy.totalZombieCount - 1) Enemy.totalZombieCount = (Enemy.totalZombieCount * 2) + Enemy.totalZombieCount;
				}

				foreach (Item itItem in items.itemArray){
					if (itItem.checkCollision(player)){
						player.inventoryList.Add(itItem);
						//item.pos.X = rnd.Next(500);
						//item.pos.Y = rnd.Next(500);
						itItem.isVisible = false;
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
				if (gameScreen.gameState == menuScreen.GameState.Paused) {
					gameScreen.ResumeDraw (graphics, spriteBatch);
				} else

					myMap.Draw(graphics, spriteBatch, player);
				items.Draw(spriteBatch);
				foreach (Enemy enemy in zombies) {
					if (enemy.isVisible) enemy.Draw(spriteBatch, enemy.ePos);
				}
				player.Draw(spriteBatch, player.pos);
                player.DrawBullet(spriteBatch);
                base.Draw(gameTime);


			}
			spriteBatch.End();
		}
	}
}

// BoundingBox test code
//Texture2D rect = new Texture2D(graphics.GraphicsDevice, player.BoundingBox.Width, player.BoundingBox.Height);

//Color[] data = new Color[player.BoundingBox.Width * player.BoundingBox.Height];
//				for (int i = 0; i<data.Length; ++i) data[i] = Color.Chocolate;
//				rect.SetData(data);

//				Vector2 coor = new Vector2(player.BoundingBox.X, player.BoundingBox.Y);
//spriteBatch.Draw(rect, coor, Color.White);