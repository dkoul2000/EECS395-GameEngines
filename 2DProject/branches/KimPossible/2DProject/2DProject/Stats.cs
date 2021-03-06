﻿#region File Description
/*-----------------------------------------------------------------------------
 * Class: Stats
 * 
 * The stats class is the class that controls the status bar, which displays
 * the current character and number of nachos left to collect.
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
    class Stats : DrawableGameComponent
    {
        //used for Game states (title screen, game mode, ending mode)
        enum GameState
        {
            TitleScreen = 0,
            GameStarted,
            GameEnded,
        }

        GameState gameState = GameState.TitleScreen;
        
        public Stats(Game g, Vector2 pos)
            : base(g)
        {
            spritePosition = pos;
        }


        //--- Public getters/setters for member variables ---//
        public Vector2 Position
        {
            get { return spritePosition; }
            set { spritePosition = value; }
        }


        //--- Member variables are always private ---//
        private Vector2 spritePosition;
        private Texture2D sprite;       // array of sprites for same as above
        private Texture2D currentchar;  // sprite of the currently selected character
        private SpriteBatch spriteBatch;
        private string correctname = "kim-possible"; //don't want to use sprite sheet so have to selected correct still pic

        // Called when the object is created in Game's Initialize() method
        // Reads in whatever resources are needed for the object
        protected override void LoadContent()
        {
            currentchar = Game.Content.Load<Texture2D>("StatsBar/"+correctname);
            sprite = Game.Content.Load<Texture2D>("nachos");
            spriteBatch = new SpriteBatch(Game.GraphicsDevice);
        }


        public override void Draw(GameTime gameTime)
        {
            if (gameState == GameState.GameStarted)
            {

                // Adjustment so that the Position refers to the center of the sprite,
                // not the top-left corner (which is the default)
                Vector2 i = new Vector2(80, -45);
                // Offset to account for centering the sprite at the position, not drawing from top-left corner


                spriteBatch.Begin();        // Begin the batch

                spriteBatch.Draw(currentchar, spritePosition - new Vector2(-20, 70), Color.White);
                foreach (var component in Game.Components)
                {
                    Nacho n = component as Nacho;
                    if ((n != null))
                    {
                        i = i + new Vector2(60, 0);
                        spriteBatch.Draw(sprite, spritePosition + i - new Vector2(sprite.Width / 2, sprite.Height / 2), Color.White); // Draw to the batch
                    }
                }
                spriteBatch.End();        // end the batch,
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
            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                gameState = GameState.GameStarted;
            
            foreach (var component in Game.Components)
            {
                Character c = component as Character;
                if (c != null && c.IsSelected)//character is selected
                {
                    switch (c.CharName)
                    {
                        case "kim":
                            correctname = "kim-possible";
                            break;
                        case "ron":
                            correctname = "ron-stoppable";
                            break;
                        case "rufus":
                            correctname = "rufus";
                            break; 
                        default:
                            correctname = "kim-possible";
                            break;
                    }

                    currentchar = Game.Content.Load<Texture2D>("StatsBar/"+correctname);
                }
            }

        }

    }
}
