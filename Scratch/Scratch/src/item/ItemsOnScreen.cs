using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Scratch {
	public class ItemsOnScreen {
		Texture2D[] textureArray;
		public List<Item> itemArray;
		int currentIndex = 0, idHold;
		Random rnd = new Random();
		Item temp, rem;

		public ItemsOnScreen() {
		}

		public void initialize( Texture2D[] a ) {
			itemArray = new List<Item>();
			textureArray = a;
		}

		public void Update( GameTime gameTime, float? pAngle, bool vert, bool horiz, float spd, Vector2 pPos, Vector2 ePos ) {
			foreach (Item j in itemArray) {
				if (j != null)
					j.Update(gameTime, pAngle, vert, horiz, spd, pPos);
			}


			if (rnd.Next(1, 10000) % 100 == 0) {
				if (currentIndex >= 20){
					itemArray.RemoveAt(1);
					currentIndex--;

				}
				idHold = rnd.Next(1, 100) % textureArray.Length;
				temp = new Item(textureArray[idHold], 1, 1);
				temp.itemId = idHold;
				if (currentIndex == 1) rem = temp;
				itemArray.Add(temp);
				temp.initialize(ePos);
				currentIndex++;
			}
		}

		public void Draw( SpriteBatch spriteBatch) {
			foreach (Item j in itemArray) {
				if (j != null && j.draw == true)
					j.Draw(spriteBatch, j.pos);
			}
		}
	}
}