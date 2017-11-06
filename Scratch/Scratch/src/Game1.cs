using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

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
			base.Initialize();
		}

		protected override void LoadContent()
		{
			// Create a new SpriteBatch, which can be used to draw textures.
			spriteBatch = new SpriteBatch(GraphicsDevice);
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
			}
		}

		protected override void Draw(GameTime gameTime)
		{
			spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
			myMap.Draw(graphics, spriteBatch);
			items.Draw(spriteBatch);
			zombie.Draw(spriteBatch, zombie.ePos);
			zombie1.Draw(spriteBatch, zombie1.ePos);
			player.Draw(spriteBatch, player.pos);
			base.Draw(gameTime);
			spriteBatch.End();
		}
	}
}
