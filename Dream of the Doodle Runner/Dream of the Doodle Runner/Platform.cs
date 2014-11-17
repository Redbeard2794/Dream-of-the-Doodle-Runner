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
    class Platform
    {
        Vector2 position;
        Texture2D texture;
        Texture2D textureStoodOn;
        bool stoodOn;

        public Platform(Vector2 pos, Texture2D text, Texture2D stoodOnText)
        {
            position = pos;
            texture = text;
            textureStoodOn = stoodOnText;
            stoodOn = false;
        }
        public void Draw(SpriteBatch sBatch)
        {
            if(stoodOn == false)
                sBatch.Draw(texture, new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height/2), Color.White);
            else if (stoodOn == true)
                sBatch.Draw(textureStoodOn, new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height/2), Color.White);
        }

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }
        public bool StoodOn
        {
            get { return stoodOn; }
            set { stoodOn = value; }
        }
    }
}
