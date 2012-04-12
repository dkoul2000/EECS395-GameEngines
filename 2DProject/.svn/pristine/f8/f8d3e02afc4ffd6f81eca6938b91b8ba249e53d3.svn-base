/*-----------------------------------------------------------------------------
 * Class: Ron
 *
 * Description.....
 *
 * Author: Lizz Bartos
 *
 * Notes: Ron extends the the base class Character. 
 -------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace _2DProject
{
    class Ron : Character
    {

        public Ron(Game g, Vector2 pos, String charName)
            : base(g, pos, charName)
        {

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
                AdjustAllSpriteFrames(100, 60, 11);//special move
            else if (characterIsSelected && k.IsKeyDown(Keys.Right))
                AdjustAllSpriteFrames(50, 52, 6);//running
            else if (characterIsSelected && k.IsKeyDown(Keys.Left))
                AdjustAllSpriteFrames(50, 52, 6);//running
            else
                AdjustAllSpriteFrames(50, 52, 6); //idle
            base.Update(gameTime);
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
            ronIsBurping = true;

        }
    }
}
