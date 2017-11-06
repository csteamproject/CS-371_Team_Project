using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Scratch
{
	public class ItemsOnScreen
	{
		Texture2D[] textureArray;
		Item[] itemArray;
		int currentIndex = 0;

		public ItemsOnScreen()
		{
		}

		public void initialize(Texture2D[] a)
		{
			itemArray = new Item[10];
			textureArray = a;

		}

		public void Update(GameTime gameTime, float? pAngle, bool vert, bool horiz, float spd, Vector2 pPos, Vector2 ePos)
		{

			foreach (Item j in itemArray)
			{

				if (j != null)

					j.Update(gameTime, pAngle, vert, horiz, spd, pPos);

			}

			Random rnd = new Random();
			if (rnd.Next(1, 10000) % 100 == 0)
			{

				itemArray[currentIndex] = new Item(textureArray[rnd.Next(1, 100) % 3], 1, 1);

				itemArray[currentIndex].initialize(ePos);

				currentIndex++;

				if (currentIndex >= 10) currentIndex = 0;


			}




		}

		public void Draw(SpriteBatch spriteBatch)
		{

			foreach (Item j in itemArray)
			{

				if (j != null)
					j.Draw(spriteBatch, j.pos);

			}

		}
	}
}