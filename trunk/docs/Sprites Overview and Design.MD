Design Document for Sprite classes and functions

System Overview:

This system handles various animated and non-animated sprites of a larger system and builds two types of unique classes, the Enemy class and the Player class. Both Player and Enemy inherit the generic building blocks of the Sprite system to animate a defined sprite sheet.


Design Rationale:

   The Animated Sprites class builds the start of an animation process when given a sprite sheet with its rows and columns.
   Because Animations continuously progressed and the player would stop moving but animations would continue, some public variables            were necessary to control the animations. Different start and stop frames can be set using startFrame and endFrame. stopFrame is used   when the player stops moving and keeps the frame steady at the area where the stop occurred. FrameReset is used for enemies, when they collide with the player they run a chomp animation that needs to reset back to default walking afterwards.

         Update- Updates the frame of the animation based on the amount of time that has passed in game.
         Draw- Draws the new frame to the screen and checks if conditions have met for the player that indicate movement has stopped.
         Player- The player class inherits from the AnimatedSprites class for its animations and builds a player for the game. In this file  we handle player movement, collision detection boxes for the player, projectiles, and player inventory.
         Enemy- Enemy handles the same logic as Player with the addition that player tracking is incorporated into the design. Random spawning of zombies and collision detection events involving zombies are also handled in this class.
