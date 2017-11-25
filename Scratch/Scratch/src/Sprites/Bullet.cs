using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace Scratch.src{

    public class Bullet{

        public Rectangle bounding;
        public Texture2D Texture;
        public Vector2 orgin;
        public Vector2 position;
        public bool isVisibile;
        public float speed;
        public float? angle;

        //contructorrr
        public Bullet(Texture2D newtexture){
            speed = 10;
            Texture = newtexture;
            isVisibile = false;
        }

        public void Draw(SpriteBatch spriteBatch){

            spriteBatch.Draw(Texture, position, Color.White);
        }

        public Texture2D Textureget(){

            return Texture;
        }
    }
}