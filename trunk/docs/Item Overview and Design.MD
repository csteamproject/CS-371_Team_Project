Design Document for Item classes and functions

System Overview:

The item system is a group of classes that provide functionality related to game items. This includes a class that defines
item objects which assigns different ids and textures to distinct items, a class for items on the game screen which
loads, draws, and maintains the position of items that have been dropped on the screen, and a crafting class which deals
with functions related to combining of collected items to produce new items for player use.

Side note: item class inherits from animatedSprite in our implementation which makes this system seem to be part
of the sprite system but typically a drawSprite class would be part of this system making it an independent system.
We decided to inherit from animatedSprite because already have that class from drawing other sprites and did not want
to have another class whose function could be met by a class already present. This decision was made after refactoring
when initially we did have two classes for drawing. However we still consider items to be a separate system because 
it addresses a separate concern and could easily be completely independent.

Design Rationale:

The ItemsOnScreen, inventoryList, and combinedInventoryList all utilize lists of items since all three require regular addition
and removal of items. Lists are convenient to use for this reason. We decided to make an ItemsOnScreen class because we needed
a class that could perform needed functions on all items that are on the screen and that this concern was separate from other
concerns. The functions needed to deal with items in a player inventory are different from ones that are on screen and the
maintenance of an inventory is a distinct concern from the maintaining of a free item on the screen. So to remain consistent with
the component based design we decided to make a class for ItemsOnScreen. Similarly, the functionality related to combining items
seemed to be a separate concern from others and worked with same information (ex player inventoryList) so we decided to make a 
seperate class for this functionality. Putting these two classes together in a system seemed logical since their concerns are
related and that the functionality of one relies on the other and vice versa.

Classes and their duties:

 	Items objects-  define all items in the game whether they be items on the screen (dropped by enemy or player) or items
	contained within inventory lists. This class gives each item with a particular texture a specific id to distinguish it from
	other items. This class handles the drawing of items on to the screen(with its inherited behavior) and it mantains the 
	relative position of the item from the player.

	ItemsOnScreen objects- typically adds new items to its list when an enemy is defeated. Each item that is on screen is
	maintained and updated. It will hold and update up to 20 items. After this, items are removed from the list each time a 
	new item is added. The updating of the ItemsOnScreen typically requires iterating through up to 20 items and updating each
	one.

	 Crafting-  related functions are invoked upon keystroke and typically add up to 2 items to inventory lists when used.
	For every item that is added to the combinedInventoryList two items are removed from inventoryList. The crafting as implemented
	iterates through the player inventory 4 times however when the crafting is invoked the inventory the inventory will typically
	shrink so these functions should not be very demanding.

Definitions, Glossary, Acronyms:

	ItemsOnScreen - object that contains and updates all items that appear on the game screen (enemy item drop or player dropped).
	Basically if the item is visible to the player, it is contained in a ItemsOnScreen object.

	inventoryList - list that belongs to player which contains all items that player has picked up from the game screen.
	If the player collides with an item in ItemsOnScreen, it is removed from ItemsOnScreen and added to the players inventoryList.

	combineInventoryList - list that belongs to player which contains all created or combined items.
	If a player combines two items in the inventoryList, the new item is added to combineInventoryList.

        
