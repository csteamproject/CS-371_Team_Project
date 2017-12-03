using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Scratch {
	public class ItemsOnScreen {
		public Texture2D[] textureArray;
		public Texture2D[] textureArrayCombined;
		public List<Item> itemArray;
		int currentIndex = 0, idHold;
		Random rnd = new Random();
		Item temp, rem;

		public void PlayerDropItem(Vector2 playerPosition, int id) { 
			if (currentIndex >= 20){
				itemArray.RemoveAt(1);
				currentIndex--;
			}
			Item droppedItem = new Item(textureArrayCombined[1], 1, 1, id);
			droppedItem.combined = true;
			itemArray.Add(droppedItem);
			droppedItem.initialize(playerPosition);
			currentIndex++;
		}

		public ItemsOnScreen() {
		}

				public void initialize( Texture2D[] a, Texture2D[] b) {
			itemArray = new List<Item>();
			textureArray = a;
			textureArrayCombined = b;
		}

		public void Update( GameTime gameTime, float? pAngle, bool vert, bool horiz, float spd, Vector2 pPos, Vector2 ePos, bool itemDrop ) {
			foreach (Item j in itemArray) {
				if (j != null && j.isVisible)
					j.Update(gameTime, pAngle, vert, horiz, spd, pPos);
			}


			if (itemDrop && rnd.Next(1, 10000) % 2 == 0) {
				if (currentIndex >= 20){
					itemArray.RemoveAt(1);
					currentIndex--;
				}
				idHold = rnd.Next(1, 100) % textureArray.Length;
				temp = new Item(textureArray[idHold], 1, 1, idHold);
				if (currentIndex == 1) rem = temp;
				itemArray.Add(temp);
				temp.initialize(ePos);
				currentIndex++;
			}
		}

		public void Draw( SpriteBatch spriteBatch) {
			foreach (Item j in itemArray) {
				if (j != null && j.isVisible == true)
					j.Draw(spriteBatch, j.pos);
			}
		}
	}
}