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
		private Item item;

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
			Tile.TileSetTexture = Content.Load<Texture2D>(@"MapSprite2");
			zombie = new Enemy(zombieTexture, 8, 36, 50, 5, 90);
			zombie1 = new Enemy(zombieTexture, 8, 36, 40, 5, 90);
			player = new Player(playerTexture, 4, 4);
			item = new Item(itemTexture, 1, 1);
			item.initialize();
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
				item.Update(gameTime, player.angle, myMap.camMoveVert, myMap.camMoveHoriz, player.spd, player.pos);
				base.Update(gameTime);
				myMap.Update(gameTime, player.pos, this.GraphicsDevice);
			}
		}

		protected override void Draw(GameTime gameTime)
		{
			spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
			myMap.Draw(graphics, spriteBatch);
			zombie.Draw(spriteBatch, zombie.ePos);
			zombie1.Draw(spriteBatch, zombie1.ePos);
			player.Draw(spriteBatch, player.pos);
			item.Draw(spriteBatch, item.pos);
			base.Draw(gameTime);
			spriteBatch.End();
		}
	}
}
