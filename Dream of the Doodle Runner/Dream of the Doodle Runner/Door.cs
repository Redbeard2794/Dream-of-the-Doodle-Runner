using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Dream_of_the_Doodle_Runner
{
    class Door
    {
        Vector2 position;
        Texture2D texture;
        //bool collisionWithPlayer;

        public Door(Texture2D text)
        {
            texture = text;
        }
        public bool CollisionWithPlayer(Vector2 playerPos)
        {
            if (playerPos.X >= position.X && playerPos.X <= position.X + texture.Width && playerPos.Y >= position.Y && playerPos.Y <= position.Y + texture.Height)
            {
                return true;
            }
            else return false;
        }
        public void Draw(SpriteBatch sBatch)
        {
            sBatch.Draw(texture, new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height), Color.White);
        }
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }
    }
}
