/*
* Item.cs
* The purpose of this class is to generate attributes for items in the game.
* This class assigns each item an itemId to allow for different behavior
* (not within the class) for different classes. Holding items that appear
* on screen in the same relative position to player is handled here as well
* as drawing item to screen.
*/

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Scratch {

	public class Item {

		public Vector2 pos, vel;
		float? angleHold = null;
		public bool isVisible = true;
		Texture2D Texture;
		public int itemId;
		public bool combined = false;
		Rectangle sourceRectangle;

		/*
		 * Description: Constructor for Item object.
		 * Pre-Conditions: Needs to be passed a texture for the item and the item id.
		 * Post-Conditions: Item object initialized and texture and id set.
		*/ 
		public Item( Texture2D texture, int id ) {
			Texture = texture;
			itemId = id;
		}

		/*
 		* Description: Check for player/item collisions.
 		* Pre-Conditions: Must be passed player object.
 		* Post-Conditions: Returns true if a collision occurred false
		* otherwise.
		*/
		public Boolean checkCollision(Player player){
			if (player.BoundingBox.Intersects(this.BoundingBox) || this.BoundingBox.Intersects(player.BoundingBox)){
				return true;
			}
			return false;
		}

		/*
		 * Description: Initializes item objects.
		 * Pre-Conditions: Must be passed a Vector2 representing its inital position
		 * Post-Conditions: Position of item set and its texture rectangle is created.
		*/ 
		public void initialize( Vector2 ePos ) {
			sourceRectangle = new Rectangle(0, 0, Texture.Width, Texture.Height);
			pos.X = ePos.X;
			pos.Y = ePos.Y;
		}

		//bounding box for item
		public Rectangle BoundingBox{get{return new Rectangle((int)pos.X,(int)pos.Y,20,20);}}

		/*
		 * Description: Updates position of item based on camera movement.
		 * Pre-Conditions: Must be provided a GameTime object, the player's movement
		 * angle, boolean values for whether camera is moving vertically and/or horizontally
		 * player seed, and player position.
		 * Post-Conditions: Item position is updated if player moves camera.
		*/ 
		public void Update( GameTime gameTime, float? playerAngle, bool vert, bool horiz, float spd, Vector2 pPos ) {
			float? angle = null;
			if (playerAngle.HasValue) {
				if (vert) {
					if (pPos.Y > 200) angle = (float)3.0 * MathHelper.PiOver2;
					else angle = MathHelper.PiOver2;
				} else if (horiz) {
					if (pPos.X > 200) angle = MathHelper.Pi;
					else angle = 0;
				}
			}

			if (angle.HasValue) {

				angleHold = angle;
				vel = new Vector2((float)Math.Cos((double)angle) * spd, (float)Math.Sin((double)angle) * spd);
			} else
				vel = new Vector2(0, 0);

			pos = Vector2.Add(pos, Vector2.Multiply(vel, (float)gameTime.ElapsedGameTime.TotalSeconds));
		}

		/*
		 *  Description: Draws item to the screen.
		 * Pre-Conditions: Must be passed a SpriteBatch object and position to be drawn.
		 * Post-Conditions: Item drawn to the screen.
		*/ 
		public void Draw(SpriteBatch spriteBatch, Vector2 location){
			Rectangle destinationRectangle = new Rectangle((int)location.X, (int)location.Y, Texture.Width, Texture.Height);
			spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White);
		}
	}
}