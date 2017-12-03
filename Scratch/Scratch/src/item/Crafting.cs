using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace Scratch{
	public class Crafting{


		public Crafting(){
		}

		public void CombineItems(List<int> itemIdCountList, Texture2D[] combinedItemTextureArray, List<Item> inventoryList, List<Item> combinedInventoryList){
			if (Keyboard.GetState().IsKeyDown(Keys.C)){

				if (itemIdCountList[0] != 0 && itemIdCountList[1] != 0){
					Item combinedItem = new Item(combinedItemTextureArray[0], 1, 1, 4);
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

					Item combinedItem = new Item(combinedItemTextureArray[0], 1, 1, 5);
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

		public void UseFirstAid(List<Item> combinedInventoryList, int health){
			if (Keyboard.GetState().IsKeyDown(Keys.F)){
				int count = 0;
				bool found = false;
				foreach (Item item in combinedInventoryList){
					if (item.itemId == 4)
					{
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