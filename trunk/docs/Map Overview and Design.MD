
Design Document for Map classes and functions


System Overview: 
	
The map system is an isometric map designed to be the main and only map of the game. The system also handles the light aura around the player. It is comprised of 4 classes, a sprite sheet, and 1 light aura image.
	
Design Rationale:
	
Every game requires a map, however I went with a isometric map beacause I think it just looks better then a regular 2d map. Some design contraints that came with this map however is the ease of editing the map. Most games allow you to easily drag and drop objects on the map and be done, however with mine you have to edit the column and row id manually. Meaning a lot of guess work goes into editing the map since there is hundereds of rows and columns. My rationale behind doing it this way was it was easier to code, since I am pretty inexperienced, and I wasnt sure how to make an entire level editor. For the light aura, it was pretty simple, just follow the player position. The rationale behind the light aura was a stylisic choice. I think it fits the theme of a zombie horror game more if you can't see around you very well. 

Classes and their duties: 

 	Camera - a simple instantiater for the map camera 
 
	MapCell - Instantiates the different tile types (Topper, Base, Height, etc)
 
 	Tile - creates the base tiles for the map
 
 	TileMap - the main map class, where most of the work is done, this class draws all the tiles to the screen along with 	the light aura and camera 

Definitions, Glossary, Acronyms:

	light aura - the circle of visibility around the player, the rest is dimmed to simulate darkness around the player. 

	sprite sheet - a sheet with different "images" that are used in the map (trees, rocks, etc.)
