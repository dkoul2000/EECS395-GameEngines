﻿#region File Description
/*-----------------------------------------------------------------------------
 * Class: Ron
 *
 * Ron's special move (enacted through the space bar) is burping fire.
 *
 * Notes:   Ron extends the base class Character, so he can move right, left,
 *          and jump by using the left, right, up arrow keys.
 -------------------------------------------------------------------------------*/
#endregion

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace _2DProject
{
    class Ron : Character
    {

        public Ron(Game g, Vector2 pos)
            : base(g, pos, "ron") // for further encapsulation, moved the character names to subclass constructors
        {

            oldkeyboard = Keyboard.GetState();
            ronIsBurping = false; 
        }

        protected KeyboardState oldkeyboard; //Distinguish if user is holding down or repeatedly pressing key
        protected Boolean ronIsBurping; //Keeps track of whether we should shoot or not. 

        public bool isRonBurping
        {
            get { return ronIsBurping; }
            set { ronIsBurping = value; }
        }

        protected override void LoadContent()
        {
            base.LoadContent();

            // Specify the size of the frame for the sprite source rectangle
            // (individual sprite width/height) for all AnimationTextures
            // i.e. idle, running, jumping, special
            AdjustAllSpriteFrames(50, 52, 6);

        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState k = Keyboard.GetState();

            if (characterIsSelected && k.IsKeyDown(Keys.Space))
            {
                AdjustAllSpriteFrames(50, 50, 11);//special move
                if (oldkeyboard.IsKeyDown(Keys.Space) != k.IsKeyDown(Keys.Space))
                {
                    ronIsBurping = true;
                }
                else
                    ronIsBurping = false; 
            }
            else if (characterIsSelected && k.IsKeyDown(Keys.Right))
                AdjustAllSpriteFrames(50, 52, 6);//running
            else if (characterIsSelected && k.IsKeyDown(Keys.Left))
                AdjustAllSpriteFrames(50, 52, 6);//running
            else
                AdjustAllSpriteFrames(50, 52, 6); //idle
            base.Update(gameTime);

            oldkeyboard = k;
        }

        /*---------------------------------------------------------------------------
          Name:     SpecialMove
          Purpose:  Makes character do special move
                    Override base class function (which does nothing...is virtual??)
                    Ron's special move is.......
          Receives: none
          Returns:  void
        ---------------------------------------------------------------------------*/
        protected override void SpecialMove()
        {
            if (ronIsBurping)
            {
                Game.Components.Add(new FireBomb(Game, Position, isFacingRight));
            }

        }
    }
}
