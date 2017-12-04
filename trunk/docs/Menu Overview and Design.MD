Design Document for menu classes and functions

System overview:

menuScreen.cs is composed of gamestates startmenu, endmenu, playing, and paused. It is used to determine what exactly to draw onto
the screen. We can either have a start menu which marks the beginning of the game, endMenu which marks the end of life, playing 
which is where most of the action occurs, and finally a paused menu which freezes all the action in the game and allows you to 
resume whenever you are ready. 


Design Ratoinale:

The class uses Texture2D objects to draw images onto the screen. The images act like buttons which if clicked will determine the
particular action. The buttons on the keyboard are used to click these buttons. I used spritefont to write
text that describes what key board buttons need to be clicked along with details on how to move the player and pause the game. 
Vector2's are used to determine the position on the screen the images/font will appear. The menuScreen class also has a song
object which plays music in the background and is intended to make the game a little more suspenseful. 

Classes and their duties: 
                          
    Initialize- In the initialize method all the positions for the images and text are initialized as well as the gamestate which is initialized to startMenu. 

    LoadContent- The load content method is used to load all the media being used through the content manager. 

    Update- The update method determines what game state the game will be in depending on the input from the user. 

    Draw methods- There are 3 draw methods one for start menu, end menu, and resume menu. Each of
    these use a graphicsdevicemanager and spritebatch as parameters. The graphicsdevicemanager is used to clear the background and make
     it a solid color. The spritebatch is used to draw the images and the text.  
