/*
* Player.cs
* The purpose of this class is to generate attributes for the player in the game.
* Player.cs inherits from AnimatedSprite.cs to build a working sprite animation.
* This class also sets up the keyboard functionality for W, A, S, and D keys.
*/

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Scratch
{
	public class Player : AnimatedSprite
	{

		private Vector2 vel;
		public float spd { get; set; } = 100;
		public Vector2 pos;
		Texture2D tex;
		int width = 900;
		int height = 625;
		public float? angle;

        //bullet stuff
        Texture2D texture, bulletTexture;
        public float bulletDelay;
        public List<src.Bullet> bulletList;

        public Player(Texture2D texture, int row, int column) : base(texture, row, column)
		{
            bulletList = new List<src.Bullet>();
            bulletDelay = 20;
			tex = texture;
		}
         public void  Bulletcreate(Texture2D texture)
        {
            bulletTexture = texture;
        }

       public void DrawBullet(SpriteBatch spriteBatch)
       {
           foreach (src.Bullet b in bulletList)
               b.Draw(spriteBatch);
       }

		public void initialize()
		{
			vel = new Vector2(0, 0);
			pos = new Vector2(0, 0);
		}

		public void Update(GameTime gameTime, GraphicsDevice graphDev, bool vert, bool horiz)
		{

			KeyboardState keys = Keyboard.GetState(); ;


           
			this.column = 1;
			angle = null;
			if (keys.IsKeyDown(Keys.D))
			{
				angle = 0;
				this.row = 2;
			}
			else if (keys.IsKeyDown(Keys.A))
			{
				angle = MathHelper.Pi;
				this.row = 1;
			}
			else if (keys.IsKeyDown(Keys.W))
			{
				angle = 3.0f * MathHelper.PiOver2;
				this.row = 3;
			}
			else if (keys.IsKeyDown(Keys.S))
			{
				this.row = 0;
				angle = MathHelper.PiOver2;
			}
			else
			{
				this.column = 0;
			}
           

            if (angle.HasValue)
				vel = new Vector2((float)Math.Cos((double)angle) * spd, (float)Math.Sin((double)angle) * spd);
			else
				vel = new Vector2(0, 0);

			pos = Vector2.Add(pos, Vector2.Multiply(vel, (float)gameTime.ElapsedGameTime.TotalSeconds));

			if (horiz == false)
			{
				if (pos.X + tex.Width > width)
					pos.X = width - tex.Width;
				if (pos.X < 0)
					pos.X = 0;
			}
			else
			{
				if (pos.X + tex.Width > graphDev.Viewport.Width)
					pos.X = graphDev.Viewport.Width - tex.Width;
				if (pos.X < 100)
					pos.X = 100;
			}

			if (vert == false)
			{
				if (pos.Y + tex.Height > height)
					pos.Y = height - tex.Height;
				if (pos.Y < 0)
					pos.Y = 0;
			}
			else
			{
				if (pos.Y + tex.Height > graphDev.Viewport.Height)
					pos.Y = graphDev.Viewport.Height - tex.Height;
				if (pos.Y < 100)
					pos.Y = 100;
			}
            //bullet things
            if (keys.IsKeyDown(Keys.Space))
            {
                shoot(angle);
            }
            UpdateBullet(angle);

            base.Update(gameTime);
		}

        //shooting method
        public void shoot(float? angle)
        {
            if (bulletDelay >= 0)
                bulletDelay--;

            if (bulletDelay <= 0)
            {
                src.Bullet newBullet = new src.Bullet(bulletTexture);
                newBullet.position = new Vector2(pos.X , pos.Y);
                newBullet.angle = angle;
                newBullet.isVisibile = true;

                if (bulletList.Count < 20)
                    bulletList.Add(newBullet);
            }

            //reset delay
            if (bulletDelay == 0)
                bulletDelay = 20;
        }

        //update bullet
        public void UpdateBullet(float? angle)
        {
            //for each bullet in bullet list update pos and do things
            foreach (src.Bullet b in bulletList)
            {
                //setting movement
                if(b.angle == 0)//d key
                b.position.X = b.position.X + b.speed;
                if (b.angle == MathHelper.Pi)//a key
                    b.position.X = b.position.X - b.speed;
                if(b.angle == 3.0f * MathHelper.PiOver2)//w key)
                        b.position.Y = b.position.Y - b.speed;
                if (b.angle == MathHelper.PiOver2)
                    b.position.Y = b.position.Y + b.speed;
                if (Math.Abs(b.position.Y) >= 600 || Math.Abs(b.position.X) >= 600)
                    b.isVisibile = false;
            }

            //check to dissapear bullet
            for (int i = 0; i < bulletList.Count; i++)
            {
                if (!bulletList[i].isVisibile)
                {
                    bulletList.RemoveAt(i);
                    i--;
                }
            }
        }
    }
}