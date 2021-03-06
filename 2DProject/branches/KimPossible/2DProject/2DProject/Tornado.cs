﻿#region File Description
/*-----------------------------------------------------------------------------
 * Class: Tornado
 *
 * Tornado is an obstacle Kim must defeat in order to get a Nacho.
 * Tornados can be destroyed by bullets. 
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
    class Tornado : DrawableGameComponent
    {
        //used for Game states (title screen, game mode, ending mode)
        enum GameState
        {
            TitleScreen = 0,
            GameStarted,
            GameEnded,
        }

        GameState gameState = GameState.TitleScreen;
        
        public Tornado(Game g, Vector2 pos)
            : base(g)
        {
            spritePosition = pos;//initial position
            destroyed = false;  //checks if the tornado still exists
            tornadoTexture = new AnimatedTexture(Vector2.Zero, Rotation, Scale, Depth); //initalize tornado texture
            yomax = r.Next(50, 300);
        }

        //--- Public getters/setters for member variables ---//
        public Vector2 Position
        {
            get { return spritePosition; }
            set { spritePosition = value; }
        }

        public Boolean IsDestroyed
        {
            get { return destroyed; }
         }


        //--- Member variables are always private ---//
        private Vector2 spritePosition;
        private int yoyo = 0;//creates a yoyo effect for tornado movement
        private AnimatedTexture tornadoTexture;//animated texture for tornado. Want it to look like it's spinning
        private SpriteBatch spriteBatch;
        private bool destroyed; //keeps track of whether or not player has shot the tornado. 

        //--- Member variables for animated sprites ---//
        private const float Rotation = 0;
        private const float Scale = 1.0f;
        private const float Depth = 0.5f;
        private Random r = new Random();
        private int yomax = 0;


        // Called when the object is created in Game's Initialize() method
        // Reads in whatever resources are needed for the object
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(Game.GraphicsDevice);
            tornadoTexture.Load(Game.Content, "tornado", 4, 2);
            tornadoTexture.AdjustSpriteFrame(21, 22, 4);
        
        }


        public override void Draw(GameTime gameTime)
        {
            // Adjustment so that the Position refers to the center of the sprite,
            // not the top-left corner (which is the default)

            // Offset to account for centering the sprite at the position, not drawing from top-left corner

            if (gameState == GameState.GameStarted)
            {
                Vector2 offset = new Vector2(tornadoTexture.spriteWidth / 2, tornadoTexture.spriteHeight / 2);
                spriteBatch.Begin();                                            // Begin the batch
                tornadoTexture.DrawFrame(spriteBatch, Position - offset, true);
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
            //if the tornado has been destroyed don't do all this!
            if (destroyed)
                Game.Components.Remove(this);

            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                gameState = GameState.GameStarted;

            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            tornadoTexture.UpdateFrame(elapsed);


            // primarily collision detection... for bullet to destroy tornado
            foreach (var component in Game.Components)
            {
                int width = tornadoTexture.spriteWidth;
                int height = tornadoTexture.spriteHeight;
                Bullet b = component as Bullet;
                if (b != null) // there is a character (visual studio required me to check for this)
                {
                    if (((spritePosition.X - width) < b.Position.X) && (b.Position.X < (spritePosition.X + width / 2)))
                    //above check if the sprite is in the width of the tornado
                    {
                        if (((spritePosition.Y - height / 2) < b.Position.Y) && (b.Position.Y < (spritePosition.Y + height / 2)))
                        //above check if the sprite is in the height of the tornado
                        {
                            if (!destroyed) //Have to make the tornado dissappear to destroy it
                            {
                                destroyed = true;
                                b.Found = true; 
                            }
                        }
                    }
                }
            }//end of this massive foreach loop
        

        //create a yoyo effect for tornado
            if (yoyo < yomax ) 
            {
                spritePosition.X = spritePosition.X + 1;
                yoyo++;
            }
            else if (yoyo < yomax * 2)
            {
                spritePosition.X = spritePosition.X - 1;
                yoyo++;
            }
            else
            {
                yoyo = 0;
                yomax = r.Next(50, 300);
            }
        }

    }
}
