﻿#region File Description
/*-----------------------------------------------------------------------------
 * Class: Bulb
 * 
 * The light bulb is an obstacle to getting a nacho. Rufus must run on a wheel to get the bulb to turn on, 
 * illuminating a nacho.
 * 
 -------------------------------------------------------------------------------*/
#endregion

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;



namespace _2DProject
{
    class Bulb : DrawableGameComponent
    {
        //used for Game states (title screen, game mode, ending mode)
        enum GameState
        {
            TitleScreen = 0,
            GameStarted,
            GameEnded,
        }

        GameState gameState = GameState.TitleScreen;
        
        public Bulb(Game g, Vector2 pos)
            : base(g)
        {
            illuminated = false;
            Position = pos;
            wheelTexture = new AnimatedTexture(Vector2.Zero, Rotation, Scale, Depth); //initalize tornado texture
        }

        //--- Public getters/setters for member variables ---//
        public Vector2 Position
        {
            get { return spritePosition; }
            set { spritePosition = value; }
        }

        public Boolean IsIlluminated
        {
            get { return illuminated; }
        }


        //--- Member variables are always private ---//
        private Vector2 spritePosition;
        private Texture2D lightbulb;
        private SpriteBatch spriteBatch;
        private AnimatedTexture wheelTexture; //Hamster wheel spins when Rufus gets on
        private bool illuminated; //Keeps track of if the bulb is illuminated or not.
        private string image = "lightbulb"; //toggles with bulb on and off. 


        //--- Member variables for animated sprites ---//
        private const float Rotation = 0;
        private const float Scale = 1.0f;
        private const float Depth = 0.5f;


        // Called when the object is created in Game's Initialize() method
        // Reads in whatever resources are needed for the object
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(Game.GraphicsDevice);
            lightbulb = Game.Content.Load<Texture2D>(image);
            wheelTexture.Load(Game.Content, "hamsterwheel",4,4);
            wheelTexture.AdjustSpriteFrame(50, 45, 4);
        }


        public override void Draw(GameTime gameTime)
        {
            if (gameState == GameState.GameStarted)
            {

                // Adjustment so that the Position refers to the center of the sprite,
                // not the top-left corner (which is the default)

                // Offset to account for centering the sprite at the position, not drawing from top-left corner
                Vector2 offset = new Vector2(lightbulb.Width / 2, 0);
                Vector2 offsetwheel = new Vector2(wheelTexture.spriteWidth / 2, wheelTexture.spriteHeight / 2);
                spriteBatch.Begin();                                            // Begin the batch
                spriteBatch.Draw(lightbulb, Position - offset, Color.White);
                wheelTexture.DrawFrame(spriteBatch, (Position - offsetwheel) + new Vector2(80, (90 - wheelTexture.spriteHeight / 2)), true);
                spriteBatch.End();                                              // end the batch,
                // causing it to render the sprite to the screen
            }
        }




        /*---------------------------------------------------------------------------
          Name:     FunctionName
          Purpose:  Description of what function does
          Receives: a parameter
          Returns:  void
        ---------------------------------------------------------------------------*/
        public override void Update(GameTime gameTime)
        {
            KeyboardState k = Keyboard.GetState();
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (k.IsKeyDown(Keys.Enter))
                gameState = GameState.GameStarted;

            foreach (var component in Game.Components)
            {
                int width = wheelTexture.spriteWidth/2;
                int height = wheelTexture.spriteHeight/2;
                Vector2 wheelpos = Position + new Vector2(80, (90 - wheelTexture.spriteHeight / 2));
                //The wheel is offset from the bulb but I'm grouping them so wheelpos calculated the wheel's position
                
                Rufus r = component as Rufus;
                if (r != null) // there is a character
                {
                    if (k.IsKeyDown(Keys.Space)&&!illuminated) //rufus is doing special move. 
                    {//if the bulb is already illuminated turn everything off. 
                        if (((wheelpos.X - width) < r.Position.X) && (r.Position.X < (wheelpos.X + width)))
                        //above check if the sprite is in the width of the tornado
                        {
                            if (((wheelpos.Y - height) < r.Position.Y) && (r.Position.Y < (wheelpos.Y + height)))
                            //above check if the sprite is in the height of the tornado
                            {
                                image = "lightbulb-lit";
                                illuminated = true;
                                r.runRufus = true;
                                r.IsSelected = false; 
                            }
                        }
                    }
                    else if (k.IsKeyDown(Keys.Space)&&illuminated)
                    {//bulb is already illuminated and we are pressing space again
                        image = "lightbulb";
                        illuminated = false;
                        r.runRufus = false;
                        r.IsSelected = true; 
                    }
                }
            }//end of this massive foreach loop
                
            lightbulb = Game.Content.Load<Texture2D>(image);

            if(illuminated)
            wheelTexture.UpdateFrame(elapsed);
        
        }

    }
}
