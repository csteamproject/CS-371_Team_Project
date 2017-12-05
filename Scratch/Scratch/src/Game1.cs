/*
* Game1.cs
* The purpose of this class is to generate attributes for the entire game.
* A Game1 object is created and then this class runs the game loop. All 
* objects in the game are created here and all actions that occur in the 
* game are called from here.
* The update and draw methods are run repeated in a loop to produce the 
* output of the game.
*/

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
		private Vector2 fontPos;
		private Vector2 fontPos2;


		private Player player;
		private ItemsOnScreen items;
		Vector2 enemyP;

		private src.Bullet bullet;

		static int squaresAcross = 17;
		static int squaresDown = 37;

		TileMap myMap = new TileMap(squaresDown, squaresAcross);

		menuScreen gameScreen = new menuScreen();

		/*
		 * Description: Constructor for Game1 object.
 		 * Pre-Conditions: None.
		 * Post-Conditions: GraphicsDeviceManager object created and Content root set
		*/
		public Game1(){
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
		}

		/*
		 * Description: Initializes Game1 object and menuscreen object.
 		 * Pre-Conditions: Game1 object should exist.
		 * Post-Conditions: Game1 and menuscreen are initalized.
		*/
		protected override void Initialize(){
			//TODO: Add your initialization logic here
			fontPos = new Vector2 (10, 10);
			fontPos2 = new Vector2 (15, 15);
			gameScreen.Initialize(graphics);
			base.Initialize();
		}

		/*
		 * Description: Loads all content to be displayed in the game
		 * and initalizes game objects.
 		 * Pre-Conditions: Game objects and texture arrays should exists.
		 * Post-Conditions: Content loaded into texture arrays and game objects
		 * intialized
		*/
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

		/*
		 * Description: Updates the whole game. Is ran in a loop.
 		 * Pre-Conditions: Must be passed GameTime object and all previous methods should be run before 
 		 * the first call of update.
		 * Post-Conditions: All game objects are updated, collisions are checked for, and player input
		 * actions are performed here.
		*/
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
						itItem.isVisible = false;
					}
				}
				

				player.crafting.CombineItems(itemIdCountList, combinedItemTextureArray, player.inventoryList, player.combinedInventoryList);
				player.crafting.UseFirstAid(player.combinedInventoryList, player.health);
				player.crafting.DropMine(player.combinedInventoryList, player.pos, items);
				player.Update(gameTime, this.GraphicsDevice, myMap.camMoveVert, myMap.camMoveHoriz);


			}
		}

		/*
		 * Description: Draws all game objects to the screen and handles start screen and 
		 * end screen drawing. Is run in a loop.
 		 * Pre-Conditions: GameTime object must be passed and should be run after update.
		 * Post-Conditions: All objects are redrawn to screen or screens are redrawn.
		*/
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

				//try to display health and score
				//spriteBatch.DrawString (gameScreen.font, "Score " + enemiesDefeated.ToString(), fontPos, Color.White);
				//spriteBatch.DrawString (gameScreen.font, "Health " + player.health.ToString (), fontPos2, Color.White);

					
			}
			spriteBatch.End();
		}
	}
}