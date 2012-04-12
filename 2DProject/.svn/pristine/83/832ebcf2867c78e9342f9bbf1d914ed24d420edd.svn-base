/*-----------------------------------------------------------------------------
 * Class: Kim
 *
 * Description.....
 *
 * Author: Lizz Bartos
 *
 * Notes: Kim extends the base class Character. 
 -------------------------------------------------------------------------------*/


using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace _2DProject
{
    class Kim : Character
    {
        public Kim(Game g, Vector2 pos, String charName)
            : base(g, pos, charName)
        {
            characterIsSelected = true; // Kim is initially selected at the start of the game
            oldkeyboard = Keyboard.GetState(); 
        }

       protected KeyboardState oldkeyboard; //Distinguish if user is holding down or repeatedly pressing key
       protected Boolean shoot = true; //Keeps track of whether we should shoot or not. 

        protected override void LoadContent()
        {
            base.LoadContent();

            // Specify the size of the frame for the sprite source rectangle
            // (individual sprite width/height) for all AnimationTextures
            // i.e. idle, running, jumping, special
            AdjustAllSpriteFrames(51, 50, 8);

        }

        public override void Update(GameTime gameTime)
        { //Need an update method to compensate for different sized sprite sheets for each move
            KeyboardState k = Keyboard.GetState();
            
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (characterIsSelected && k.IsKeyDown(Keys.Space))
            {
                AdjustAllSpriteFrames(50, 51, 8); //special move sheet has more frames and should  move faster
                if (oldkeyboard.IsKeyDown(Keys.Space) != k.IsKeyDown(Keys.Space))
                {
                    shoot = true;
                }
                else
                    shoot = false; 
            }
            else if (characterIsSelected && k.IsKeyDown(Keys.Right))
                AdjustAllSpriteFrames(50, 51, 5); //running loop 
            else if (characterIsSelected && k.IsKeyDown(Keys.Left))
                AdjustAllSpriteFrames(50, 51, 5); //running loop
            else
                AdjustAllSpriteFrames(50, 51, 8); //idle sheet
            base.Update(gameTime);

            oldkeyboard = k; 
        }
        /*---------------------------------------------------------------------------
          Name:     SpecialMove
          Purpose:  Makes character do special move
                    Override base class function (which does nothing)
                    Kim's special move is.......
          Receives: none
          Returns:  void
        ---------------------------------------------------------------------------*/
        protected override void SpecialMove()
        {
             if (shoot)
            {
                Game.Components.Add(new Bullet(Game, Position, isFacingRight));
            }
            
       }

    }
}
