using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


//Remove all platforms at end of game to facilitate a restart function

namespace Dream_of_the_Doodle_Runner
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont Font1;

        //add language select as first state later
        const byte Menu = 0, Play = 1, Instructions = 2, Win = 3, Lose = 4;
        int gameState = Menu;
        int level = 1;
        //textures
        Texture2D ruledPageBackground;
        Texture2D normalButton;
        Texture2D jumpNotPressed;
        Texture2D jumpPressed;
        Texture2D leftArrowNotPressed;
        Texture2D leftArrowPressed;
        Texture2D rightArrowNotPressed;
        Texture2D rightArrowPressed;
        Texture2D DreamOfTheDoodleRunnerLogo;
        Texture2D platform;
        Texture2D platformStoodOn;
        Texture2D playerTestTexture;
        Texture2D doorTexture;
        //player right
        Texture2D idleRight;
        Texture2D runRight1;
        Texture2D runRight2;
        Texture2D runRight3;
        Texture2D runRight4;
        Texture2D runRight5;
        //player left
        Texture2D idleLeft;
        Texture2D runLeft1;
        Texture2D runLeft2;
        Texture2D runLeft3;
        Texture2D runLeft4;
        Texture2D runLeft5;

        Texture2D cursor;

        //objects
        menuButton playButton;
        menuButton instructionsButton;
        menuButton instructionsBackButton;
        Player player;
        Door door;
        //mouse and keyboard states
        MouseState mouseState, previousMouseState;
        //KeyboardState keyState = Keyboard.GetState();
        KeyboardState oldKeyState;


        bool jumpPushed;
        bool rightPushed;
        bool leftPushed;

        //Platform p1;



        List<Platform> platformList = new List<Platform>();

        SoundEffect deathScream;
        SoundEffect jumpySound;

        
        
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            //this.IsMouseVisible = true;
            this.Window.Title = "Dream of the Doodle Runner";
            oldKeyState = Keyboard.GetState();
            jumpPushed = false;
            rightPushed = false;
            leftPushed = false;

            //platformList.Add(new Platform(new Vector2(10,10), platform, platformStoodOn));
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Font1 = Content.Load<SpriteFont>("Font1");

            ruledPageBackground = Content.Load<Texture2D>("backgrounds/ruledPageBackground");

            normalButton = Content.Load<Texture2D>("menuButtons/menuButton");
            playButton = new menuButton(normalButton, new Vector2(GraphicsDevice.Viewport.Width / 2.5f, GraphicsDevice.Viewport.Height / 4));
            instructionsButton = new menuButton(normalButton, new Vector2(GraphicsDevice.Viewport.Width / 2.5f, GraphicsDevice.Viewport.Height/2));
            instructionsBackButton = new menuButton(normalButton, new Vector2(GraphicsDevice.Viewport.Width / 2.5f, GraphicsDevice.Viewport.Height - 100));

            jumpNotPressed = Content.Load<Texture2D>("jumpButton/jumpButtonDefault");
            jumpPressed = Content.Load<Texture2D>("jumpButton/jumpButtonPressed");
            leftArrowNotPressed = Content.Load<Texture2D>("leftArrow/arrowLeftDefault");
            leftArrowPressed = Content.Load<Texture2D>("leftArrow/arrowLeftPressed");
            rightArrowNotPressed = Content.Load<Texture2D>("rightArrow/arrowRightDefault");
            rightArrowPressed = Content.Load<Texture2D>("rightArrow/arrowRightPressed");
            DreamOfTheDoodleRunnerLogo = Content.Load<Texture2D>("logo's/logo");
            platform = Content.Load<Texture2D>("platforms/improvedPlatform");
            platformStoodOn = Content.Load<Texture2D>("platforms/improvedPlatformStoodOn");
            playerTestTexture = Content.Load<Texture2D>("player/playerTestTexture");
            doorTexture = Content.Load<Texture2D>("doors/door");
            //player right
            idleRight = Content.Load<Texture2D>("player/idleRight");
            runRight1 = Content.Load<Texture2D>("player/runR1");
            runRight2 = Content.Load<Texture2D>("player/runR2");
            runRight3 = Content.Load<Texture2D>("player/runR3");
            runRight4 = Content.Load<Texture2D>("player/runR4");
            runRight5 = Content.Load<Texture2D>("player/runR5");
            //player left
            idleLeft = Content.Load <Texture2D>("player/idleRight");
            runLeft1 = Content.Load<Texture2D>("player/runL1");
            runLeft2 = Content.Load<Texture2D>("player/runL2");
            runLeft3 = Content.Load<Texture2D>("player/runL3");
            runLeft4 = Content.Load<Texture2D>("player/runL4");
            runLeft5 = Content.Load<Texture2D>("player/runL5");


            cursor = Content.Load<Texture2D>("penCursor");


            deathScream = Content.Load<SoundEffect>("Wilhelm Scream");
            jumpySound = Content.Load<SoundEffect>("Boing");
            
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            KeyboardState keyState = Keyboard.GetState();
            // TODO: Add your update logic here
            switch (gameState)
            {
                case Play:

                    this.Window.Title = "Dream of the Doodle Runner: Level " + level;

                    player.Update(keyState);
                    checkKeyboardInput(keyState);
                    //DeterminePlayerMovement();
                    if (rightPushed == true && leftPushed == false)
                    {
                        player.Direction = 0;
                        player.Move();
                    }
                    else if (leftPushed == true && rightPushed == false)
                    {
                        player.Direction = 1;
                        player.Move();
                    }
                    if(jumpPushed == true)
                    {
                        player.InAir = true;
                        //jumpySound.Play();
                        
                    }
                    if (player.InAir == true)
                    {
                        player.Jump();
                        
                    }


                    if (checkIfPlayerOnPlatform() == true)
                    {
                        //p1.StoodOn = true;
                        player.InAir = false;
                        player.AirSpeed = 2;
                    }
                    //else if (checkIfPlayerOnPlatform() == false)
                    //{
                    //    //p1.StoodOn = false;
                    //}
                    if (checkIfPlayerOnPlatform() == false && player.InAir == false)
                    {
                        player.Fall();
                    }
                    //if (CheckIfPlayerHitsLeftEdge() == true)
                    //{
                    //    player.BounceLeft();
                    //}
                    //if (CheckIfPlayerHitsRightEdge() == true)
                    //{
                    //    player.BounceRight();
                    //}

                    //ResetForNextLevel();

                    //if (level == 1)
                    //    door.Position = new Vector2(200, 200);

                    if (door.CollisionWithPlayer(player.Position) == true)
                    {
                        if (level == 1)
                            player.Position = new Vector2(GraphicsDevice.Viewport.Width / 12, 40);
                        else if (level == 2)
                            player.Position = new Vector2(GraphicsDevice.Viewport.Width / 2, 0);
                        else if (level == 3)
                            player.Position = new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height - 80);
                        level += 1;
                        ResetForNextLevel();
                    }

                    KillZone();

                    break;
            }

            checkMouseInput();

            base.Update(gameTime);
        }
        private void ResetForNextLevel()
        {
            if (level == 1)
                door.Position = new Vector2(GraphicsDevice.Viewport.Width - 50, GraphicsDevice.Viewport.Height - 120);
            else if (level == 2)
            {
                //for (int i = 0; i < platformList.Count; i++)
                //    platformList.RemoveAt(i);
                door.Position = new Vector2(GraphicsDevice.Viewport.Width -80, 40);
                platformList[0].Position = new Vector2(GraphicsDevice.Viewport.Width / 12, GraphicsDevice.Viewport.Height/4);
                platformList[1].Position = new Vector2(GraphicsDevice.Viewport.Width / 4 - 40, GraphicsDevice.Viewport.Height / 3);
                platformList[2].Position = new Vector2(GraphicsDevice.Viewport.Width / 8, GraphicsDevice.Viewport.Height / 2);
                platformList[3].Position = new Vector2(GraphicsDevice.Viewport.Width / 12, GraphicsDevice.Viewport.Height-100);
                platformList[4].Position = new Vector2(GraphicsDevice.Viewport.Width / 3, GraphicsDevice.Viewport.Height - 130);
                platformList[5].Position = new Vector2(GraphicsDevice.Viewport.Width - 275, GraphicsDevice.Viewport.Height - 160);
                //6
                platformList.Add(new Platform(new Vector2(GraphicsDevice.Viewport.Width - 150, GraphicsDevice.Viewport.Height/2), platform, platformStoodOn));
                //7
                platformList.Add(new Platform(new Vector2(GraphicsDevice.Viewport.Width - 300, GraphicsDevice.Viewport.Height/2-50), platform, platformStoodOn));
                //8
                platformList.Add(new Platform(new Vector2(GraphicsDevice.Viewport.Width -110, 120), platform, platformStoodOn));
            }
            else if (level == 3)
            {
                door.Position = new Vector2(doorTexture.Width+25, GraphicsDevice.Viewport.Height - 115);
                platformList[0].Position = new Vector2(GraphicsDevice.Viewport.Width / 2, 80);
                platformList[1].Position = new Vector2(GraphicsDevice.Viewport.Width / 4, 150);
                platformList[2].Position = new Vector2(GraphicsDevice.Viewport.Width / 8, GraphicsDevice.Viewport.Height / 2+50);
                platformList[3].Position = new Vector2(20, GraphicsDevice.Viewport.Height / 2+50);
                platformList[4].Position = new Vector2(GraphicsDevice.Viewport.Width/3, GraphicsDevice.Viewport.Height / 2 + 140);
                platformList[5].Position = new Vector2(GraphicsDevice.Viewport.Width / 5, GraphicsDevice.Viewport.Height / 2 + 180);
                platformList[6].Position = new Vector2(doorTexture.Width+25, GraphicsDevice.Viewport.Height-35);
                platformList[7].Position = new Vector2(GraphicsDevice.Viewport.Width - 300, GraphicsDevice.Viewport.Height / 2 - 50);
                platformList[8].Position = new Vector2(GraphicsDevice.Viewport.Width - 400, GraphicsDevice.Viewport.Height / 2+60);
            }
            else if (level == 4)
            {
                door.Position = new Vector2(20, 60); 
                platformList[1].Position = new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height - 20);
            }
        }

        private void KillZone()
        {
            if (player.Position.Y > GraphicsDevice.Viewport.Height)
            {
                if (level == 1)
                {
                    player.Position = new Vector2(GraphicsDevice.Viewport.Width / 14, GraphicsDevice.Viewport.Height - 120);
                }
                else if (level == 2)
                {
                    player.Position = new Vector2(GraphicsDevice.Viewport.Width / 12, 40);
                }
                else if (level == 3)
                {
                    player.Position = new Vector2(GraphicsDevice.Viewport.Width / 2, 20);
                }

                deathScream.Play();
                player.Deaths += 1;
            }
        }
        //private void DeterminePlayerMovement()
        //{

        //}

        //Create a collision manager class to handle all the collisions

        private bool CheckIfPlayerHitsLeftEdge()
        {
            bool hit = false;
            for (int i = 0; i < platformList.Count; i++)
            {
                if (player.Position.X + (idleRight.Width / 2) == platformList[i].Position.X && platformList[i].Position.Y >= platformList[i].Position.Y && platformList[i].Position.Y <= player.Position.Y + (idleRight.Height / 2) && player.InAir == true)
                {
                    hit = true;
                }
            }
            return hit;
        }
        private bool CheckIfPlayerHitsRightEdge()
        {
            bool hit = false;
            for (int i = 0; i < platformList.Count; i++)
            {
                if (player.Position.X == platformList[i].Position.X  + platform.Width && platformList[i].Position.Y >= platformList[i].Position.Y && platformList[i].Position.Y <= player.Position.Y + (idleRight.Height / 2) && player.InAir == true)
                {
                    hit = true;
                }
            }
            return hit;
        }

        //collision detection needs to be redone. per pixel detection???
        //or try to detect collision with both edges and the top separetly
        //This is more viable really
        private bool checkIfPlayerOnPlatform()
        {
            bool collision = false;
            //to land on platform(check x against x and width and check y with y)
            for (int i = 0; i < platformList.Count; i++)
            {
                if (player.Position.X >= platformList[i].Position.X - 17 && player.Position.X <= platformList[i].Position.X + platform.Width && player.Position.Y + (idleRight.Height / 2) >= platformList[i].Position.Y && player.Position.Y + (idleRight.Height / 2) <= platformList[i].Position.Y + 10)// && player.Position.Y+55 >= platformList[i].Position.Y && player.Position.Y+55 <= platformList[i].Position.Y + platform.Height)
                {
                    collision = true;
                    platformList[i].StoodOn = true;
                    break;
                }
                else platformList[i].StoodOn = false;
            }
            if (collision == true)
                return true;
            else return false;
            //    if (player.Position.X >= p1.Position.X && player.Position.X <= p1.Position.X + platform.Width && player.Position.Y >= p1.Position.Y - 48)
            //    {
            //        return true;
            //    }
            //else return false;
            //to bounce off side(checkleft edge against playerRec and right edge against
            //playerRec separetly
            //to hit bottom( check x and width and player y against platform height)
        }



        //checks input from the keyboard
        private void checkKeyboardInput(KeyboardState keyState)
        {
            //JUMP
            // Is the SPACE key down?
            if (keyState.IsKeyDown(Keys.Space))
            {
                // If not down last update, key has just been pressed.
                if (!oldKeyState.IsKeyDown(Keys.Space))
                    jumpPushed = true;
            }
            else if (oldKeyState.IsKeyDown(Keys.Space))
            {
                // Key was down last update, but not down now, so
                // it has just been released.
                jumpPushed = false;
            }
            //RIGHT
            if (keyState.IsKeyDown(Keys.Right))
            {
                if (!oldKeyState.IsKeyDown(Keys.Right))
                    rightPushed = true;
            }
            else if (oldKeyState.IsKeyDown(Keys.Right))
                rightPushed = false;
            //LEFT
            if (keyState.IsKeyDown(Keys.Left))
            {
                if (!oldKeyState.IsKeyDown(Keys.Left))
                    leftPushed = true;
            }
            else if (oldKeyState.IsKeyDown(Keys.Left))
                leftPushed = false;

            // Update saved state.
            oldKeyState = keyState;
        }
        //checks input from the mouse
        private void checkMouseInput()
        {
            previousMouseState = mouseState;
            mouseState = Mouse.GetState();
            switch(gameState)
            {
                case Menu:
                    if (mouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Released)
                    {
                        if (playButton.Clicked(new Vector2(mouseState.X, mouseState.Y)) == true)
                        {
                            gameState = Play;
                            player = new Player(idleRight, runRight1, runRight2, runRight3, runRight4, runRight5, idleLeft, runLeft1, runLeft2, runLeft3, runLeft4, runLeft5);
                            player.Position = new Vector2(GraphicsDevice.Viewport.Width / 14, GraphicsDevice.Viewport.Height - 120);
                            this.Window.Title = "Dream of the Doodle Runner: Level " + level;
                            //p1 = new Platform(new Vector2(60, 200), platform, platformStoodOn);
                            door = new Door(doorTexture);
                            for (int i = 0; i < 6; i++)
                            {
                                if(i==0)
                                    platformList.Add(new Platform(new Vector2(GraphicsDevice.Viewport.Width / 14, GraphicsDevice.Viewport.Height - 60), platform, platformStoodOn));
                                else if(i ==1)
                                    platformList.Add(new Platform(new Vector2(GraphicsDevice.Viewport.Width / 6, GraphicsDevice.Viewport.Height - 140), platform, platformStoodOn));
                                else if (i==2)
                                    platformList.Add(new Platform(new Vector2(GraphicsDevice.Viewport.Width / 2-80, GraphicsDevice.Viewport.Height - 180), platform, platformStoodOn));
                                else if (i== 3)
                                    platformList.Add(new Platform(new Vector2(GraphicsDevice.Viewport.Width / 2 + 60, GraphicsDevice.Viewport.Height - 180), platform, platformStoodOn));
                                else if (i==4)
                                    platformList.Add(new Platform(new Vector2(GraphicsDevice.Viewport.Width / 2+190, GraphicsDevice.Viewport.Height - 140), platform, platformStoodOn));
                                else if(i ==5)
                                    platformList.Add(new Platform(new Vector2(GraphicsDevice.Viewport.Width-80, GraphicsDevice.Viewport.Height - 40), platform, platformStoodOn));
                            }
                            //platformList.Add(new Platform(new Vector2(10,10), platform, platformStoodOn));
                            ResetForNextLevel();
                        }
                        else if (instructionsButton.Clicked(new Vector2(mouseState.X, mouseState.Y)) == true)
                            gameState = Instructions;
                    }
                break;
                case Instructions:
                    if (mouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Released)
                    {
                        if (instructionsBackButton.Clicked(new Vector2(mouseState.X, mouseState.Y)) == true)
                        {
                            gameState = Menu;
                        }
                    }
                break;
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();

            spriteBatch.Draw(ruledPageBackground, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.White);


            switch (gameState)
            {
                case Menu:
                    playButton.Draw(spriteBatch);
                    spriteBatch.DrawString(Font1, "PLAY", new Vector2(GraphicsDevice.Viewport.Width / 2.5f + 50, GraphicsDevice.Viewport.Height / 4 + 17), Color.LimeGreen);
                    instructionsButton.Draw(spriteBatch);
                    spriteBatch.DrawString(Font1, "INSTRUCTIONS", new Vector2(GraphicsDevice.Viewport.Width / 2.5f + 10, GraphicsDevice.Viewport.Height / 2 + 17), Color.LimeGreen);
                    spriteBatch.Draw(DreamOfTheDoodleRunnerLogo, new Rectangle(10, 10, DreamOfTheDoodleRunnerLogo.Width, DreamOfTheDoodleRunnerLogo.Height), Color.White);
                    spriteBatch.Draw(platformStoodOn, new Rectangle(GraphicsDevice.Viewport.Width / 2 + 150, 70, platform.Width, platform.Height/2), Color.White);
                    spriteBatch.Draw(platform, new Rectangle(GraphicsDevice.Viewport.Width / 6, GraphicsDevice.Viewport.Height - 50, platform.Width, platform.Height/2), Color.White);
                    spriteBatch.Draw(platform, new Rectangle(GraphicsDevice.Viewport.Width / 4 + 30, GraphicsDevice.Viewport.Height - 70, platform.Width, platform.Height/2), Color.White);
                    spriteBatch.Draw(runRight3, new Rectangle(GraphicsDevice.Viewport.Width / 4, GraphicsDevice.Viewport.Height - 140, idleRight.Width/2, idleRight.Height/2), Color.White);
                    spriteBatch.Draw(idleRight, new Rectangle(GraphicsDevice.Viewport.Width-230, GraphicsDevice.Viewport.Height/32, idleRight.Width / 2, idleRight.Height / 2), Color.White);
                    spriteBatch.Draw(cursor, new Rectangle(mouseState.X, mouseState.Y, cursor.Width, cursor.Height), Color.White);
                    break;
                case Instructions:
                    spriteBatch.DrawString(Font1, "1. Move with the right and left arrow keys", new Vector2(10, 52), Color.MidnightBlue);
                    spriteBatch.DrawString(Font1, "2. Jump with the space key", new Vector2(10, 69), Color.MidnightBlue);
                    spriteBatch.DrawString(Font1, "3. Navigate through each level to reach the exit door to go", new Vector2(10, 86), Color.MidnightBlue);
                    spriteBatch.DrawString(Font1, "to the next level", new Vector2(10, 101), Color.MidnightBlue);
                    spriteBatch.DrawString(Font1, "4. ", new Vector2(10, 118), Color.MidnightBlue);
                    spriteBatch.DrawString(Font1, "5. ", new Vector2(10, 135), Color.MidnightBlue);
                    instructionsBackButton.Draw(spriteBatch);
                    spriteBatch.DrawString(Font1, "BACK", new Vector2(GraphicsDevice.Viewport.Width / 2 - 25, GraphicsDevice.Viewport.Height - 85), Color.Lime);
                    spriteBatch.Draw(cursor, new Rectangle(mouseState.X, mouseState.Y, cursor.Width, cursor.Height), Color.White);
                    break;
                case Play:
                    spriteBatch.DrawString(Font1, "Level " + level, new Vector2(GraphicsDevice.Viewport.Width / 2 - 30, 35), Color.MidnightBlue);
                    spriteBatch.DrawString(Font1, "Deaths:  " + player.Deaths, new Vector2(20, 35), Color.MidnightBlue);
                    //JUMP
                    if(jumpPushed == false)
                        spriteBatch.Draw(jumpNotPressed, new Rectangle(GraphicsDevice.Viewport.Width / 2 - 80, GraphicsDevice.Viewport.Height -80, jumpNotPressed.Width, jumpNotPressed.Height), Color.White);
                    else if(jumpPushed == true)
                        spriteBatch.Draw(jumpPressed, new Rectangle(GraphicsDevice.Viewport.Width / 2 - 80, GraphicsDevice.Viewport.Height - 80, jumpNotPressed.Width, jumpNotPressed.Height), Color.White);
                    //LEFT
                    if(leftPushed == false)
                        spriteBatch.Draw(leftArrowNotPressed, new Rectangle(GraphicsDevice.Viewport.Width / 4 - 80, GraphicsDevice.Viewport.Height - 80, leftArrowNotPressed.Width, leftArrowNotPressed.Height), Color.White);
                    else if (leftPushed == true)
                        spriteBatch.Draw(leftArrowPressed, new Rectangle(GraphicsDevice.Viewport.Width / 4 - 80, GraphicsDevice.Viewport.Height - 80, leftArrowNotPressed.Width, leftArrowNotPressed.Height), Color.White);
                    //RIGHT
                    if (rightPushed == false)
                        spriteBatch.Draw(rightArrowNotPressed, new Rectangle(GraphicsDevice.Viewport.Width - 200, GraphicsDevice.Viewport.Height - 80, rightArrowNotPressed.Width, rightArrowNotPressed.Height), Color.White);
                    else if (rightPushed == true)
                        spriteBatch.Draw(rightArrowPressed, new Rectangle(GraphicsDevice.Viewport.Width - 200, GraphicsDevice.Viewport.Height - 80, rightArrowNotPressed.Width, rightArrowNotPressed.Height), Color.White);

                    player.Draw(spriteBatch);
                    //p1.Draw(spriteBatch);
                    door.Draw(spriteBatch);
                    for (int i = 0; i < platformList.Count; i++)
                        platformList[i].Draw(spriteBatch);
                    break;
                    
            }

            

            spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
