#region File Description
/*-----------------------------------------------------------------------------
 * Class: _2DProject (why is there an underscore....?)
 *
 * Description here.....
 * Use 1, 2, 3 to toggle between kim, ron, and rufus ... must  collect nachos...
 *
 * Author: Team Possible
 *
 * Notes: Other relevant information goes here.
 -------------------------------------------------------------------------------*/
#endregion

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

namespace _2DProject
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        //used for Game states (title screen, game mode, ending mode)
        enum GameState
        {
            TitleScreen = 0,
            GameStarted,
            GameEnded,
        }

        GameState gameState = GameState.TitleScreen;
        
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D background, intro, ending;

        // Characters / Components of game
        Kim kim;
        Ron ron;
        Rufus rufus;

        Nacho nacho1;   // do we need to declare these as overall variables? not sure
        Nacho nacho2;
        Bulb lightBulb;


        // Platforms, etc
        public Level level;

        
        //// Not sure if need this, got it from a tutorial...check out later *****
        // private Viewport viewport;
        //private Rectangle TitleSafe;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            
            Content.RootDirectory = "Content";
        }


        ///===========================================================================
        ///   Name:     Initialize        
        ///===========================================================================
        protected override void Initialize()
        {
            // --- Add your initialization logic here --- //

            //
            // Centralized location where we specify all of the files to be used as sprite sheets for kim ron rufus
            //

            
            level = new Level(this);    // Description......................

            // Create the Character objects to use in the game
            // Second parameter specifies starting location on screen
            // Last parameter specifies last parameter specifies the characterName,
            // which is used to generate the images/sprite sheet asset names within the Character class
            kim = new Kim(this, new Vector2(20, 300), "kim");
            ron = new Ron(this, new Vector2(30, 300), "ron");
            rufus = new Rufus(this, new Vector2(40, 300), "rufus");

            // Add the current level to the game.......?!?! what is this exactly??

            Components.Add(level);

            // Add our characters to the game 
            Components.Add(kim);
            Components.Add(ron);
            Components.Add(rufus);

            // The tornado nacho
            TornadoNacho(); // renamed from "FirstNacho()"
            
            //The Hamster Wheel nacho
            HamsterWheelNacho();

            // Add the status bar
            Components.Add(new Stats(this, new Vector2(450, 70)));

            // The brick wall nacho...excellent commenting skillzz
            BrickWallNacho();

            base.Initialize();
        }


        private Viewport viewport; // from a tutorial.....

        ///===========================================================================
        ///   Name:     LoadContent        
        ///===========================================================================
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            
            background = Content.Load<Texture2D>("background");
            intro = Content.Load<Texture2D>("introscreen");
            ending = Content.Load<Texture2D>("endscreen");

            level.heightPixels = GraphicsDevice.PresentationParameters.BackBufferHeight;
            level.widthPixels = GraphicsDevice.PresentationParameters.BackBufferWidth;

            viewport = graphics.GraphicsDevice.Viewport; // from a tutorial....


            // not sure if need...check later ***
            //TitleSafe = GetTitleSafeArea(.8f);
            //viewport = Game.GraphicsDevice.Viewport; //***

            // TODO: use this.Content to load your game content here
        }


        ///===========================================================================
        ///   Name:     UnloadContent       
        ///===========================================================================
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }


        ///===========================================================================
        ///   Name:     Update       
        ///===========================================================================
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit on Xbox 36
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            Console.WriteLine(Components.Count);

            if (Components.Count == 6)
                gameState = GameState.GameEnded;


            // Allows the game to exit on Windows
            // press Q key or ESC key to exit

#if WINDOWS
            if (Keyboard.GetState().IsKeyDown(Keys.Escape) ||
                Keyboard.GetState().IsKeyDown(Keys.Q))
                this.Exit();
#endif

            // --- Add your update logic here --- //

            //                                                          
            // ---------- User inputs via keyboard ----------           
            //                                                          

            // Get the current state of the keyboard (check if any keys were pressed)
            KeyboardState k = Keyboard.GetState();


            // keys 1, 2, 3 toggle which character is currently selected
            // 1 = kim, 2 = ron, 3 = rufus

            if (k.IsKeyDown(Keys.D1))       // 1 key pressed
            {
                // Kim becomes the currently selected character
                kim.IsSelected = true;
                ron.IsSelected = false;
                rufus.IsSelected = false;
            }

            else if (k.IsKeyDown(Keys.D2))  // 2 key pressed
            {
                // Ron becomes the currently selected character
                kim.IsSelected = false;
                ron.IsSelected = true;
                rufus.IsSelected = false;
            }

            else if (k.IsKeyDown(Keys.D3))  // 3 key pressed
            {
                // Rufus becomes the currently selected character
                kim.IsSelected = false;
                ron.IsSelected = false;
                rufus.IsSelected = true;
            }

            else if (k.IsKeyDown(Keys.Enter) && gameState == GameState.TitleScreen) //Press enter
                gameState = GameState.GameStarted;  //send game into start mode where action begins

            else if (k.IsKeyDown(Keys.Enter) && gameState == GameState.GameEnded) //Press enter
                this.Exit();        //quit game after end screen has shown up

            /*===========================================================================
                Nacho 1 Functionality
            ===========================================================================*/
         bool allgone = true; //checks if all tornadoes have been destroyed 
            foreach (var component in Components) {
                Tornado t = component as Tornado;
                if (t != null)//There is still a tornado in the list
                    allgone=false;
            }
            if (allgone) //all the tornadoes are destroyed
                nacho1.IsVisible = true;

            //================================================================================//

            /*===========================================================================
    Nacho 2 Functionality
===========================================================================*/
            if (lightBulb.IsIlluminated)
                nacho2.IsVisible = true;
            else
                nacho2.IsVisible = false;
            //================================================================================//

            base.Update(gameTime);
        }


        /*===========================================================================
           Name:     Draw        (override XNA function...?)
         ===========================================================================*/
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            //draw the intro screen as that's the state we're in
            if (gameState == GameState.TitleScreen)
            {
            spriteBatch.Begin();
                DrawIntro();
                spriteBatch.End();
            }

            //draw the background since we're in play mode
            else if (gameState == GameState.GameStarted)
            {
                spriteBatch.Begin();
            DrawBackground();
                spriteBatch.End();
            }

            //draw the end screen since the game is over and now we're in end mode
            else if (gameState == GameState.GameEnded)
            {
                spriteBatch.Begin();
                DrawEnding();
            spriteBatch.End();
            }
            base.Draw(gameTime);
        }


        //
        // draw game background.........
        //
        private void DrawBackground()
        {
            Rectangle screenRectangle = new Rectangle(0, 0,GraphicsDevice.PresentationParameters.BackBufferWidth,
                GraphicsDevice.PresentationParameters.BackBufferHeight);
            spriteBatch.Draw(background, screenRectangle, Color.White);
        }

        //
        // Draw Intro Screen....
        //
        private void DrawIntro()
        {
            Rectangle introRect = new Rectangle(0, 0, GraphicsDevice.PresentationParameters.BackBufferWidth,
                GraphicsDevice.PresentationParameters.BackBufferHeight);
            spriteBatch.Draw(intro, introRect, Color.White);
        }


        private void DrawEnding()
        {
            Rectangle endRect = new Rectangle(0, 0, GraphicsDevice.PresentationParameters.BackBufferWidth,
                GraphicsDevice.PresentationParameters.BackBufferHeight);
            spriteBatch.Draw(ending, endRect, Color.White);
        }

                //private void FirstNacho()
        private void TornadoNacho()
        {//The tornado nacho!
            Components.Add(new Tornado(this, new Vector2(100, 175)));
            Components.Add(new Tornado(this, new Vector2(135, 175)));
            Components.Add(new Tornado(this, new Vector2(20, 175)));
            Components.Add(new Tornado(this, new Vector2(60, 175)));
            nacho1 = new Nacho(this, new Vector2(10,175),"nachos",false);
            Components.Add(nacho1);
        }

        private void HamsterWheelNacho()
        {
            lightBulb = new Bulb(this, new Vector2(690, 150));
            Components.Add(lightBulb);
            nacho2 = new Nacho(this, new Vector2(690, 225), "nachos", false);
            Components.Add(nacho2);
        }

        private void BrickWallNacho()
        {
            // This is hacky, since the full collisons aren't working right,
            // I just made a bunch on top of each other extending to the bottom of the page
            // So that it was harder to make it through
            // will fix forrealz later!
            Components.Add(new BrickWall(this, new Vector2(90, 420), "brickwall"));
            Components.Add(new BrickWall(this, new Vector2(90, 430), "brickwall"));
            Components.Add(new BrickWall(this, new Vector2(90, 440), "brickwall"));
            Components.Add(new Nacho(this, new Vector2(35, 420), "nachos", true));

    }
    }
}