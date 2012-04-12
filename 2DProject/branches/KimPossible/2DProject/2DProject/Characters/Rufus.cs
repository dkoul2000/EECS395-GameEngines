/*-----------------------------------------------------------------------------
 * Class: Rufus
 *
 * Description.....
 *
 * Author: Lizz Bartos
 *
 * Notes: Rufus extends the base class Character. 
 -------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;


namespace _2DProject
{
    class Rufus : Character
    {

        public Rufus(Game g, Vector2 pos, String charName)
            : base(g, pos, charName)
        {
            keepRufusRunning = false; 
        }

        //private SoundEffect eatingSound;

        protected override void LoadContent()
        {
            base.LoadContent();

            // Specify the size of the frame for the sprite source rectangle
            // (individual sprite width/height) for all AnimationTextures
            // i.e. idle, running, jumping, special
            AdjustAllSpriteFrames(25, 23, 4);

            //eatingSound = Game.Content.Load<SoundEffect>("rufus-eating");

        }

        /*---------------------------------------------------------------------------
          Name:     SpecialMove
          Purpose:  Makes character do special move
                    Override base class function (which does nothing...is virtual??)
                    Rufus's special move is.......
          Receives: none
          Returns:  void
        ---------------------------------------------------------------------------*/
        protected override void SpecialMove()
        {

            // Maybe rufus' special move is just the ability to each a nacho? or at least play his
            // sound effect
            //eatingSound.Play();
        }

    }
}
