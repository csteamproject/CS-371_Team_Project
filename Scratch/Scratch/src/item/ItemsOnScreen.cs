/*
 * ItemsOnScreen.cs
 * The purpose of this class is to manage all items that are on the screen.
 * This class maintains a list of all items on the screen and iterates 
 * through the list to update and draw them. Adding of new items to the
 * screen is also handled by this class whether the case be player dropped or 
 * enemy dropped items. 
*/

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

		/*
		 * Description: Method that allows player to drop items.
		 * Pre-Conditions: Must be passed player position vector and item id
		 * of dropped item.
		 * Post-Conditions: Item is added to ItemsOnScreen (is dropped).
		*/ 
		public void PlayerDropItem(Vector2 playerPosition, int id) { 
			if (currentIndex >= 20){
				itemArray.RemoveAt(1);
				currentIndex--;
			}
			Item droppedItem = new Item(textureArrayCombined[1], id);
			droppedItem.combined = true;
			itemArray.Add(droppedItem);
			droppedItem.initialize(playerPosition);
			currentIndex++;
		}

		/*
 		 * Description: Constructor for ItemsOnScreen object( empty).
 		 * Pre-Conditions: None.
		 * Post-Conditions: ItemsOnScreen object initialized.
		*/
		public ItemsOnScreen() {
		}

		/*
		 * Description: Initializes list containing items on screen.
 		 * Pre-Conditions: Must be provided texture arrays for objects to be drawn.
		 * Post-Conditions: List of items on screen initalized and texture arrays set. 
		*/ 
		public void initialize( Texture2D[] a, Texture2D[] b) {
			itemArray = new List<Item>();
			textureArray = a;
			textureArrayCombined = b;
		}

		/*
		 * Description: Updates all items on screen.
 		 * Pre-Conditions: Must be given GameTime, player angle,
 		 * camera booleans, player speed and position, enemy position,
 		 * and a boolean to check if items are to be dropped.
		 * Post-Conditions: Items on the screen are updated and new
		 * items are potentially dropped if itemDrop is true.
		*/
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
				temp = new Item(textureArray[idHold], idHold);
				if (currentIndex == 1) rem = temp;
				itemArray.Add(temp);
				temp.initialize(ePos);
				currentIndex++;
			}
		}

		/*
		 * Description: Draws each item to the screen.
 		 * Pre-Conditions: Must be given SpriteBatch object.
		 * Post-Conditions: Each item in the list is drawn to
		 * the screen.
		*/ 
		public void Draw( SpriteBatch spriteBatch) {
			foreach (Item j in itemArray) {
				if (j != null && j.isVisible == true)
					j.Draw(spriteBatch, j.pos);
			}
		}
	}
}