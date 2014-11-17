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
    class Player
    {
        Vector2 position;
        bool inAir;
        int speed;
        float airSpeed;
        const byte Right = 0, Left = 1;
        int direction = Right;
        Texture2D idleRightTexture;
        Texture2D runR1Texture;
        Texture2D runR2Texture;
        Texture2D runR3Texture;
        Texture2D runR4Texture;
        Texture2D runR5Texture;

        Texture2D idleLeftTexture;
        Texture2D runL1Texture;
        Texture2D runL2Texture;
        Texture2D runL3Texture;
        Texture2D runL4Texture;
        Texture2D runL5Texture;

        int timer = 0;
        int deaths;
        //bool moving = false;
        //KeyboardState ks = new KeyboardState();

        //constructor
        public Player(Texture2D rItexture, Texture2D r1, Texture2D r2, Texture2D r3, Texture2D r4, Texture2D r5, Texture2D liTexture, Texture2D l1, Texture2D l2, Texture2D l3, Texture2D l4, Texture2D l5)
        {
            //position = new Vector2(60, 150);
            speed = 1;
            airSpeed = 2;
            idleRightTexture = rItexture;
            runR1Texture = r1;
            runR2Texture = r2;
            runR3Texture = r3;
            runR4Texture = r4;
            runR5Texture = r5;
            idleLeftTexture = liTexture;
            runL1Texture = l1;
            runL2Texture = l2;
            runL3Texture = l3;
            runL4Texture = l4;
            runL5Texture = l5;
            deaths = 0;
        }
        //update
        public void Update(KeyboardState ks)
        {
            if (ks.IsKeyDown(Keys.Left) || ks.IsKeyDown(Keys.Right))
            {
                timer++;
                if (timer >= 25)
                    timer = 0;
            }
            else timer = 0;
        }
        //horizontal movement 
        public void Move()
        {
            if (direction == Right)
                position.X += speed;
            else if (direction == Left)
                position.X -= speed;
        }
        //vertical movement
        public void Jump()
        {
            position.Y -= airSpeed;
            if(airSpeed >= -1.7)//1
                airSpeed -= 0.02f;//fiddle with later, maybe?
        }
        public void Fall()
        {
            position.Y += airSpeed;
        }
        public void BounceLeft()
        {
            position.X -= 30;
        }
        public void BounceRight()
        {
            position.X += 30;
        }
        //draw
        public void Draw(SpriteBatch sBatch)
        {
            

            //if (ks.IsKeyDown(Keys.Left) || ks.IsKeyDown(Keys.Right))
            //{
                //timer++;
            if(direction == Right)
            {
                if (timer >= 0 && timer <= 5)
                    sBatch.Draw(runR1Texture, new Rectangle((int)position.X, (int)position.Y, idleRightTexture.Width / 2, idleRightTexture.Height / 2), Color.White);
                else if (timer >= 5 && timer <= 10)
                    sBatch.Draw(runR2Texture, new Rectangle((int)position.X, (int)position.Y, idleRightTexture.Width / 2, idleRightTexture.Height / 2), Color.White);
                else if (timer >= 10 && timer <= 15)
                    sBatch.Draw(runR3Texture, new Rectangle((int)position.X, (int)position.Y, idleRightTexture.Width / 2, idleRightTexture.Height / 2), Color.White);
                else if (timer >= 15 && timer <= 20)
                    sBatch.Draw(runR4Texture, new Rectangle((int)position.X, (int)position.Y, idleRightTexture.Width / 2, idleRightTexture.Height / 2), Color.White);
                else if (timer >= 20 && timer <= 25)
                    sBatch.Draw(runR5Texture, new Rectangle((int)position.X, (int)position.Y, idleRightTexture.Width / 2, idleRightTexture.Height / 2), Color.White);
            
            //}

                else if(timer == 0)//and direction = right
                {
                    //timer = 0;
                    sBatch.Draw(idleRightTexture, new Rectangle((int)position.X, (int)position.Y, idleRightTexture.Width / 2, idleRightTexture.Height / 2), Color.White);
                }
            }
            else if (direction == Left)
            {
                if (timer >= 0 && timer <= 5)
                    sBatch.Draw(runL1Texture, new Rectangle((int)position.X, (int)position.Y, idleRightTexture.Width / 2, idleRightTexture.Height / 2), Color.White);
                else if (timer >= 5 && timer <= 10)
                    sBatch.Draw(runL2Texture, new Rectangle((int)position.X, (int)position.Y, idleRightTexture.Width / 2, idleRightTexture.Height / 2), Color.White);
                else if (timer >= 10 && timer <= 15)
                    sBatch.Draw(runL3Texture, new Rectangle((int)position.X, (int)position.Y, idleRightTexture.Width / 2, idleRightTexture.Height / 2), Color.White);
                else if (timer >= 15 && timer <= 20)
                    sBatch.Draw(runL4Texture, new Rectangle((int)position.X, (int)position.Y, idleRightTexture.Width / 2, idleRightTexture.Height / 2), Color.White);
                else if (timer >= 20 && timer <= 25)
                    sBatch.Draw(runL5Texture, new Rectangle((int)position.X, (int)position.Y, idleRightTexture.Width / 2, idleRightTexture.Height / 2), Color.White);

            //}

                else if (timer == 0)//and direction = right
                {
                    //timer = 0;
                    sBatch.Draw(idleLeftTexture, new Rectangle((int)position.X, (int)position.Y, idleRightTexture.Width / 2, idleRightTexture.Height / 2), Color.White);
                }
            }
        }
        //properties
        public int Direction
        {
            get { return direction; }
            set { direction = value; }
        }
        public bool InAir
        {
            get { return inAir; }
            set { inAir = value; }
        }
        public float AirSpeed
        {
            get { return airSpeed; }
            set { airSpeed = value; }
        }
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }
        public int Deaths
        {
            get { return deaths; }
            set { deaths = value; }
        }
    }
}
