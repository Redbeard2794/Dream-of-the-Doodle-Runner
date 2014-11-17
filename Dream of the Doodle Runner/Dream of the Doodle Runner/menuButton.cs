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
    class menuButton
    {
        Texture2D texture;
        Vector2 position;
        public menuButton(Texture2D texture1, Vector2 pos)
        {
            texture = texture1;
            position = pos;
        }
        public void Update()
        {
            
        }
        public bool Clicked(Vector2 mousePos)
        {
            if (mousePos.X >= position.X && mousePos.X <= position.X + texture.Width && mousePos.Y >= position.Y && mousePos.Y <= position.Y + texture.Height)
            {
                return true;
            }
            else return false;
        }
        public void Draw(SpriteBatch sBatch)
        {
            sBatch.Draw(texture, new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height), Color.White);
        }
    }
}
