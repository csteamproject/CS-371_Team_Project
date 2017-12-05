/*
* Player.cs
* The purpose of this class is to generate attributes for the player in the game.
* Player.cs inherits from AnimatedSprite.cs to build a working sprite animation.
* This class sets up the keyboard functionality for W, A, S, and D keys as well
* as handles the creation of Bullet objects for the player use for Spacebar key.
*/

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Scratch {
	public class Player : AnimatedSprite {

		private Vector2 vel;
		public float spd { get; set; } = 100;
		public Vector2 pos;
		Texture2D tex;
		int width = 900;
		int height = 625;
		public float? angle;
		public int health = 100;
		public List<Item> inventoryList;
		public List<Item> combinedInventoryList;
		public Crafting crafting;

        //bullet stuff
        public float? lastAngle = null;
        Texture2D bulletTexture;
        public float bulletDelay;
        public List<src.Bullet> bulletList;

		//bounding box for player
        public Rectangle BoundingBox {get {return new Rectangle((int)pos.X+5,(int)pos.Y,40,50);}}

		/*
		 * Description: Constructor for Player object.
 		 * Pre-Conditions: Must be passed texture, and information
 		 * for animated sprite.
		 * Post-Conditions: Player object created.
		*/
		public Player( Texture2D texture, int row, int column ) : base(texture, row, column) {
			tex = texture;
            bulletList = new List<src.Bullet>();
            bulletDelay = 20;
        }

		/*
		 * Description: Sets the texture for a Bullet object
 		 * Pre-Conditions: Must be passed texture.
		 * Post-Conditions: Bullet texture set
		*/
        public void Bulletcreate(Texture2D texture){
            bulletTexture = texture;
        }

		/*
		 * Description: Draws all bullets to the screen.
 		 * Pre-Conditions: Must be passed a SpriteBatch object
		 * Post-Conditions: Each bullet in the list drawn to screen.
		*/
        public void DrawBullet(SpriteBatch spriteBatch){
            foreach (src.Bullet b in bulletList)
                b.Draw(spriteBatch);
        }

		/*
		 * Description: Initializes player velocity, position, inventory, combined
		 * inventory, and crafting object.
 		 * Pre-Conditions: Player object must first be inialized.
		 * Post-Conditions: Player values intialized and/or set.
		*/
        public void Initialize() {
			vel = new Vector2(0, 0);
			pos = new Vector2(this.tex.Width/2, this.tex.Height/2); //possible fix to the player position box
			inventoryList = new List<Item>();
			combinedInventoryList = new List<Item>();
			crafting = new Crafting();
		}

		/*
		 * Description: Updates player object position and manages
		 * boundary collision and camera movement.
 		 * Pre-Conditions: Must be passed as GameTime object, a 
 		 * GraphicsDevice object, and camera movement booleans.
		 * Post-Conditions: Player position and camera position updated
		 * based on user input.
		*/
		public void Update( GameTime gameTime, GraphicsDevice graphDev, bool vert, bool horiz ) {

			KeyboardState keys = Keyboard.GetState(); ;

			//sets player angle based on user input
			this.stopFrame = 1;
			angle = null;
			if (keys.IsKeyDown(Keys.D)) {
				angle = 0;
				this.row = 2;
			} else if (keys.IsKeyDown(Keys.A)) {
				angle = MathHelper.Pi;
				this.row = 1;
			} else if (keys.IsKeyDown(Keys.W)) {
				angle = 3.0f * MathHelper.PiOver2;
				this.row = 3;
			} else if (keys.IsKeyDown(Keys.S)) {
				this.row = 0;
				angle = MathHelper.PiOver2;
			} else {
				this.stopFrame = 0;
			}

			//sets player velocity based on angle
			if (angle.HasValue)
				vel = new Vector2((float)Math.Cos((double)angle) * spd, (float)Math.Sin((double)angle) * spd);
			else
				vel = new Vector2(0, 0);

			//sets new position based on velocity
			pos = Vector2.Add(pos, Vector2.Multiply(vel, (float)gameTime.ElapsedGameTime.TotalSeconds));

			//boundary of screen and camera movement checks
			if (horiz == false) {
				if (pos.X + tex.Width > width)
					pos.X = width - tex.Width;
				if (pos.X < 0)
					pos.X = 0;
			} else {
				if (pos.X + tex.Width > graphDev.Viewport.Width)
					pos.X = graphDev.Viewport.Width - tex.Width;
				if (pos.X < 100)
					pos.X = 100;
			}

			if (vert == false) {
				if (pos.Y + tex.Height > height)
					pos.Y = height - tex.Height;
				if (pos.Y < 0)
					pos.Y = 0;
			} else {
				if (pos.Y + tex.Height > graphDev.Viewport.Height)
					pos.Y = graphDev.Viewport.Height - tex.Height;
				if (pos.Y < 100)
					pos.Y = 100;
			}
            //bullet things
            if (keys.IsKeyDown(Keys.Space)){
                if (angle == null)
                    shoot(lastAngle);
                else
                    shoot(angle);
            }

            UpdateBullet(angle);
            if(angle != null)
                lastAngle = angle; //catch last know angle of player to fix bullet dropping in place

            base.Update(gameTime);
		}

        /*
		 * Description: Allows for bullet shooting.
 		 * Pre-Conditions: Must be passed player angle.
		 * Post-Conditions: New bullet added to bullet list
		 * and it shot in the direction of player.
		*/
        public void shoot(float? angle){

            if (bulletDelay >= 0)
                bulletDelay--;

            if (bulletDelay <= 0){

                src.Bullet newBullet = new src.Bullet(bulletTexture);
                newBullet.position = new Vector2(pos.X, pos.Y);
                newBullet.orgin = new Vector2(pos.X, pos.Y);
                newBullet.angle = angle;
                newBullet.isVisibile = true;

                if (bulletList.Count < 20)
                    bulletList.Add(newBullet);
            }

            //reset delay
            if (bulletDelay == 0)
                bulletDelay = 20;
        }

        /*
         * Description: Updates Bullet objects in the bullet list.
         * Updates position and removes if they have existed for
         * a certain amount of time.
 		 * Pre-Conditions: Must be passed player angle.
		 * Post-Conditions: The position of bullets in the list are 
		 * updated and removes bullets based on time they have existed.
		*/
        public void UpdateBullet(float? angle){
			
            //for each bullet in bullet list update pos and do things
            foreach (src.Bullet b in bulletList){
                
                //setting movement
                if (b.angle == 0)//d key
                    b.position.X = b.position.X + b.speed;
                if (b.angle == MathHelper.Pi)//a key
                    b.position.X = b.position.X - b.speed;
                if (b.angle == 3.0f * MathHelper.PiOver2)//w key)
                    b.position.Y = b.position.Y - b.speed;
                if (b.angle == MathHelper.PiOver2)
                    b.position.Y = b.position.Y + b.speed;
                if (Math.Abs(b.position.Y - b.orgin.Y) >= 600 || Math.Abs(b.position.X - b.orgin.X) >= 600) //corrected bullet destroy logic to take into account the orgin point of the bullet
                    b.isVisibile = false;
            }

            //check to dissapear bullet
            for (int i = 0; i < bulletList.Count; i++){
                if (!bulletList[i].isVisibile){
                    bulletList.RemoveAt(i);
                    i--;
                }
            }
        }
    }
}