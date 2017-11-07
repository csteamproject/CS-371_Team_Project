using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace Scratch.src
{   
    public class Bullet
    {
        public Rectangle bounding;
        public Texture2D Texture;
        public Vector2 orgin;
        public Vector2 position;
        public bool isVisibile;
        public float speed;
        public float? angle;

        //contructorrr
        public Bullet(Texture2D newtexture)
        {
            speed = 10;
            Texture = newtexture;
            isVisibile = false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, position, Color.White);
        }

        public Texture2D Textureget()
        {
            return Texture;
        }
    }



  /* public  class Bullet : AnimatedSprite
    {
        private float timing;
        private int lifespan = 3;

        public Bullet(Texture2D texture, int row, int col) : base(texture,row,col)
        {

        }

        public override void Update(GameTime gameTime)
        {
            timing += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (timing > lifespan)
                removed = true;

            positionB += positionB * 2 ;

        }
    }
    */
}
