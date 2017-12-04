Design Document for Bullet class and functions


System Overview: 
	
  The Bullet function is a class that will handle the template of a bullet object and use that template to create multiple instances 
  within the player class. Note the bullet object is independent of the player.
  
Design Rationale:
	
We created the bullet design to be esily creatable and destroyable thus the list aspect of keeping track of the objects and the template method where we can freely create bullets off a template and add more to them at the time of creation. This will allow the bullet to also inherit other properties depending on the bullet need at the time. If there was no further development of the bullet we would most likely diverge from this design to a more static one for speed.


Definitions, Glossary, Acronyms, and Functions:


	Bullet - object that will spawn at player position and travel in direction of player at that time.
	
	Shoot()- will create the bullet object at the time the keystoke defined (space by default) is activated.
	
	isVisible - a parameter used to keep track of the bullet object and if it is visible to the player.
	
	drawBullet() - function that will look through the list of bullets active and draw the bullets as nessary with updated prarmeters.
	
	UpdateBullet()- the function that will continually update the positions of the bullets and is called from drawBullet to aid in 
			updating the bullets in the object list.
	Bulletcreate()- function that will pull the texture for the bullet texture to use for every bullet that is created in the
			player class.
	
        Textureget()- unused function in the bullet class that was intended to allow swapping out of textures of the bullet sprite.
