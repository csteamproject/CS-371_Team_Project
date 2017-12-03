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
		List<int> itemIdCountList = new List<int>();
		Texture2D[] combinedItemTextureArray;
		Texture2D[] itemTextureArrayTemp;
		bool gameEnd = false;

		private Player player;
		private ItemsOnScreen items;
		Vector2 enemyP;

		private src.Bullet bullet;

		static int squaresAcross = 17;
		static int squaresDown = 37;

		TileMap myMap = new TileMap(squaresDown, squaresAcross);

		menuScreen gameScreen = new menuScreen();

		public void CombineItems(){
			if (Keyboard.GetState().IsKeyDown(Keys.C)){

				if (itemIdCountList[0] != 0 && itemIdCountList[1] != 0){
					Item combinedItem = new Item(combinedItemTextureArray[0], 1, 1, 4);
					player.combinedInventoryList.Add(combinedItem);
					int i = 0;
					foreach (Item item in player.inventoryList){
						if (item.itemId == 0){
							player.inventoryList.RemoveAt(i);
							break;
						}
						i++;
					}
					i = 0;
					foreach (Item item in player.inventoryList){
						if (item.itemId == 1){
							player.inventoryList.RemoveAt(i);
							break;
						}
						i++;
					}

				}

				if (itemIdCountList[2] != 0 && itemIdCountList[3] != 0){

					Item combinedItem = new Item(combinedItemTextureArray[0], 1, 1, 5);
					player.combinedInventoryList.Add(combinedItem);
					int i = 0;
					foreach (Item item in player.inventoryList){
						if (item.itemId == 2){
							player.inventoryList.RemoveAt(i);
							break;
						}
						i++;
					}
					i = 0;
					foreach (Item item in player.inventoryList){
						if (item.itemId == 3){
							player.inventoryList.RemoveAt(i);
							break;
						}
						i++;
					}

				}

			}
		}

		public void UseFirstAid(){
			if (Keyboard.GetState().IsKeyDown(Keys.F)){
				int count = 0;
				bool found = false;
				foreach (Item item in player.combinedInventoryList){
					if (item.itemId == 4){
						found = true;
						break;
					}
					else count++;
				}

				if (found){
					player.health = player.health + 100;
					player.combinedInventoryList.RemoveAt(count);
				}
			}
		}

		public void DropMine(){
			if (Keyboard.GetState().IsKeyDown(Keys.E)){
				int count = 0;
				bool found = false;
				foreach (Item item in player.combinedInventoryList){
					if (item.itemId == 5){
						found = true;
						break;
					}
					else count++;
				}

				if (found){
					items.PlayerDropItem(player.pos, 5);
					player.combinedInventoryList.RemoveAt(count);
				}
			}
		}

		public Game1(){
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
		}

		protected override void Initialize(){
			//TODO: Add your initialization logic here
			gameScreen.Initialize(graphics);
			base.Initialize();
		}

		protected override void LoadContent(){
			// Create a new SpriteBatch, which can be used to draw textures.
			spriteBatch = new SpriteBatch(GraphicsDevice);
			gameScreen.LoadContent(Content);
			Texture2D playerTexture = Content.Load<Texture2D>("sprites/player");
			Texture2D itemTexture = Content.Load<Texture2D>("items/hammer1");
			Texture2D[] itemTextureArray = { Content.Load<Texture2D>("items/cloth"),
				Content.Load<Texture2D>("items/ointment"), Content.Load<Texture2D>("items/gunpowder"),
				Content.Load<Texture2D>("items/string") };
			itemTextureArrayTemp = itemTextureArray;
			Texture2D[] combinedItemTextureArrayTemp = { Content.Load<Texture2D>("items/cloth"),
				Content.Load<Texture2D>("items/mine") }; //change ointment to mine sprite
			combinedItemTextureArray = combinedItemTextureArrayTemp;
			Tile.TileSetTexture = Content.Load<Texture2D>("sprites/MapSprite2");
			Texture2D lightAura = Content.Load<Texture2D>("sprites/lightaura");
			Texture2D bulletTexture = Content.Load<Texture2D>("items/projectile2");
			myMap.lightAuracreate(Content);
			items = new ItemsOnScreen();
			player = new Player(playerTexture, 4, 4);
			items.initialize(itemTextureArray, combinedItemTextureArray);
			foreach (Texture2D tex in items.textureArray)
				itemIdCountList.Add(0);
			bullet = new src.Bullet(bulletTexture);
			player.Bulletcreate(bulletTexture);
			player.Initialize();
			Enemy.LoadContent(Content, rnd, zombies);
		}

		protected override void Update(GameTime gameTime){

			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
				Exit();

			gameScreen.Update();
			if (gameScreen.gameState == menuScreen.GameState.Playing && IsActive){
				// TODO: Add your update logic here

				Enemy.randomSpawn(zombies, rnd, Content);

				foreach (Enemy enemy in zombies){
					if (enemy.isVisible){
						enemy.Update(gameTime, player.pos, myMap.camMoveVert, myMap.camMoveHoriz);
						if (rnd.Next(1, 100) % 2 == 1) enemyP = enemy.ePos;
						else enemyP = enemy.ePos;
						items.Update(gameTime, player.angle, myMap.camMoveVert, myMap.camMoveHoriz, player.spd, player.pos, enemyP, false);
						base.Update(gameTime);
						myMap.Update(gameTime, player.pos, this.GraphicsDevice, player);

						if (enemy.isVisible && enemy.checkCollision(player)){
							player.health = player.health - 10;

							if (player.health == 0) gameEnd = true;
							if (enemy.a > 0) player.pos.X = player.pos.X + 20;
							else player.pos.X = player.pos.X - 20;
							if (enemy.b > 0) player.pos.Y = player.pos.Y + 20;
							else player.pos.Y = player.pos.Y - 20;
						}

						foreach (src.Bullet b in player.bulletList)
							if (enemy.checkBulletCollision(b)){
								enemy.isVisible = false;
								items.Update(gameTime, player.angle, myMap.camMoveVert, myMap.camMoveHoriz, player.spd, player.pos, enemyP, true);
								enemiesDefeated++;
							}

						foreach (Item item in items.itemArray){
							if (item.combined && item.isVisible && enemy.checkMineCollision(item)){
								enemy.isVisible = false;
								item.isVisible = false;
								enemiesDefeated++;
							}
						}
					}

					if (enemiesDefeated >= Enemy.totalZombieCount - 1){
						zombies = new List<Enemy>();
						Enemy.totalZombieCount = (Enemy.totalZombieCount * 2) + Enemy.totalZombieCount;
					}
				}

				foreach (Item itItem in items.itemArray){
					if (!(itItem.combined) && itItem.isVisible && itItem.checkCollision(player)){
						player.inventoryList.Add(itItem);
						itemIdCountList[itItem.itemId] = itemIdCountList[itItem.itemId] + 1;
						//item.pos.X = rnd.Next(500);
						//item.pos.Y = rnd.Next(500);
						itItem.isVisible = false;
					}
				}
				

				CombineItems();
				UseFirstAid();
				DropMine();
				player.Update(gameTime, this.GraphicsDevice, myMap.camMoveVert, myMap.camMoveHoriz);


			}
		}

		protected override void Draw(GameTime gameTime){
			spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
			if (gameScreen.gameState == menuScreen.GameState.StartMenu){
				gameScreen.StartDraw(graphics, spriteBatch);
			}
			else if (gameScreen.gameState == menuScreen.GameState.EndMenu || gameEnd){
				gameScreen.EndDraw(graphics, spriteBatch);
			}
			else{
				if (gameScreen.gameState == menuScreen.GameState.Paused){
					gameScreen.ResumeDraw(graphics, spriteBatch);
				}
				else

					myMap.Draw(graphics, spriteBatch, player);
				items.Draw(spriteBatch);
				foreach (Enemy enemy in zombies){
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