/*
 * Crafting.cs
 * The purpose of this class is to add the functionality of item
 * combination as well as define the behavior of combined items.
*/

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace Scratch{
	public class Crafting{


		/*
		 * Description: Constructor for Crafting object( empty).
		 * Pre-Conditions: None.
		 * Post-Conditions: Crafting object initialized.
		*/
		public Crafting(){
		}

		/*
		 * Description: Method that allows for combination of items
		 * in player inventory when the C key is pushed.
		 * Pre-Conditions: Crafting object should exist and method should be provided 
		 * a list of integers representing the counts of each item in player inventory,
		 * a texture array containing textures for newly created combined items, the player
		 * inventory, and the player's combined item inventory. C key should be pushed or
		 * the method will do nothing.
		 * Post-Conditions: All possible combined items are created based on inventory contents
		 * if C key is pushed. Otherwise nothing
		*/
		public void CombineItems(List<int> itemIdCountList, Texture2D[] combinedItemTextureArray, List<Item> inventoryList, List<Item> combinedInventoryList){
			if (Keyboard.GetState().IsKeyDown(Keys.C)){

				if (itemIdCountList[0] != 0 && itemIdCountList[1] != 0){
					Item combinedItem = new Item(combinedItemTextureArray[0], 4);
					combinedInventoryList.Add(combinedItem);
					int i = 0;
					foreach (Item item in inventoryList){
						if (item.itemId == 0){
							inventoryList.RemoveAt(i);
							break;
						}
						i++;
					}
					i = 0;
					foreach (Item item in inventoryList){
						if (item.itemId == 1){
							inventoryList.RemoveAt(i);
							break;
						}
						i++;
					}
				}

				if (itemIdCountList[2] != 0 && itemIdCountList[3] != 0){

					Item combinedItem = new Item(combinedItemTextureArray[0], 5);
					combinedInventoryList.Add(combinedItem);
					int i = 0;
					foreach (Item item in inventoryList){
						if (item.itemId == 2){
							inventoryList.RemoveAt(i);
							break;
						}
						i++;
					}
					i = 0;
					foreach (Item item in inventoryList){
						if (item.itemId == 3){
							inventoryList.RemoveAt(i);
							break;
						}
						i++;
					}
				}
			}
		}

		/*
		 *  Description: Method that allows use of and defines behavior of use of FirstAid item.
		 * Pre-Conditions: Crafting object should exist and method should be provided the player
		 * health, and the player's combined item inventory. F key should be pushed and combined
		 * inventory should have at least one FirstAid item (itemId 4).
		 * Post-Conditions: Player health is increase by 100 and one FirstAid item is removed from
		 * player's combined inventory.
		*/
		public void UseFirstAid(List<Item> combinedInventoryList, int health){
			if (Keyboard.GetState().IsKeyDown(Keys.F)){
				int count = 0;
				bool found = false;
				foreach (Item item in combinedInventoryList){
					if (item.itemId == 4){
						found = true;
						break;
					}
					else count++;
				}

				if (found){
					health = health + 100;
					combinedInventoryList.RemoveAt(count);
				}
			}
		}

		/*
		 * Description: Method that allows for player to drop mines on the screen.
		 * Pre-Conditions: Crafting object should exist and method should be provided the player
		 * position, the player's combined item inventory, and a ItemsOScren object. 
		 * E key should be pushed and combined inventory should have at least one mine item (itemId 5).
		 * Post-Conditions: Player will drop a mine onto the screen which will remain there until
		 * it collides with an enemy.
		*/
		public void DropMine(List<Item> combinedInventoryList, Vector2 pos, ItemsOnScreen items){
			if (Keyboard.GetState().IsKeyDown(Keys.E)){
				int count = 0;
				bool found = false;
				foreach (Item item in combinedInventoryList){
					if (item.itemId == 5){
						found = true;
						break;
					}
					else count++;
				}

				if (found){
					items.PlayerDropItem(pos, 5);
					combinedInventoryList.RemoveAt(count);
				}
			}
		}
	}
}