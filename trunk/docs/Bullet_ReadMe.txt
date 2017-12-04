Software Architecture Document (SAD) for Bullet class and functions


System Overview: 
	
	The Bullet function is a class that will handle the template of a bullet object and use that template to create multiple instances 
  within the player class. Note the bullet object is independent of the player.
  
Views:

  The bullet design is a factory method design where a basic skeleton is made for bullet and when invoked it will add more functionality depending on the situation.

	

Software Units:

  The software units used are in floating point.

Analysis Data and Results:
	
  The bullet class is created uppon keystroke so the speed is fast and each instance is kept in a list which can easily remove a bullet.

	
Design Rationale:
	

	We created the bullet design to be esily creatable and destroyable thus the list aspect of keeping track of the objects and the template method
  where we can freely create bullets off a template and add more to them at the time of creation. This will allow the bullet to also inherit other 
  properties depending on the bullet need at the time. If there was no further development of the bullet we would most likely diverge from this
  design to a more static one for speed.


Definitions, Glossary, Acronyms, and Functions:


	Bullet - object that will spawn at player position and travel in direction of player at that time.
	
	Shoot()- will create the bullet object at the time the keystoke defined (space by default) is activated.
	
	isVisible - a parameter used to keep track of the bullet object and if it is visible to the player.
	
	drawBullet() - function that will look through the list of bullets active and draw the bullets as nessary with updated prarmeters.
	
