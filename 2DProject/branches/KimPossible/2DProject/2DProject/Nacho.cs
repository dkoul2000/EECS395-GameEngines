/*-----------------------------------------------------------------------------
 * Class: Nacho
 *
 * Nacho is a class for scoring. The class keeps track of the team's progress
 * the level. 
 * 
 -------------------------------------------------------------------------------*/
#region File Description
/*-----------------------------------------------------------------------------
 * Class: Nacho
 *
 * A nacho is a collectable item. To win the game, the player must collect
 * all nachos, and they can be collected using any of the characters.
 * 
 -------------------------------------------------------------------------------*/
#endregion

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;



namespace _2DProject
{
    class Nacho : DrawableGameComponent
    {
        public Nacho(Game g, Vector2 pos, String img, bool appears)
            : base(g)
        {
            spritePosition = pos;
            imgName = img;      // to use in load content function.... each subclass can specify own file in constructor
            visible = appears;  // controls whether or not the nacho is visible to the player. 
            collected = false;  // tells the status bar if the nacho has been collected
        }

        //--- Public getters/setters for member variables ---//
        public Vector2 Position
        {
            get { return spritePosition; }
            set { spritePosition = value; }
        }

        public Boolean IsVisible
        {
            get { return visible; }
            set { visible = value; }        // where is toggling which character is selected controlled....?
        }

        public Boolean IsCollected
        {
            get{return collected; }
        }

        //--- Member variables are always private ---//
        private Vector2 spritePosition;
        private String imgName;
        private Texture2D sprite;
        private SpriteBatch spriteBatch;
        private bool visible; //controls whether or not the nacho is visible. 
        private bool collected; //keeps track of whether or not the nacho has been collected


        private SoundEffect eatingSound;

        // Called when the object is created in Game's Initialize() method
        // Reads in whatever resources are needed for the object
        protected override void LoadContent()
        {
            sprite = Game.Content.Load<Texture2D>(imgName);
            spriteBatch = new SpriteBatch(Game.GraphicsDevice);

            eatingSound = Game.Content.Load<SoundEffect>("rufus-eating");
        }


        public override void Draw(GameTime gameTime)
        {
            // Adjustment so that the Position refers to the center of the sprite,
            // not the top-left corner (which is the default)

            // Offset to account for centering the sprite at the position, not drawing from top-left corner
            if (visible) //if you are supposed to be able to see the nacho
            {
                Vector2 offset = new Vector2(sprite.Width / 2, sprite.Height / 2);

                spriteBatch.Begin();                                            // Begin the batch
                spriteBatch.Draw(sprite, spritePosition - offset, Color.White); // Draw to the batch
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
            //if the nacho has been collected than delete it!
            if (collected)
                Game.Components.Remove(this);


            // primarily collision detection
            foreach (var character in Game.Components)
            {
                // Maybe rufus' special talent is ability to eat nachos?? 
                // trying something out here

                Character c = character as Character;
                if (c != null) // there is a character (visual studio required me to check for this)
                {
                    if (((spritePosition.X-sprite.Width/2)<c.Position.X) && (c.Position.X<(spritePosition.X+sprite.Width/2)))
                    //above check if the sprite is in the width of the nacho
                    {
                        if (((spritePosition.Y - sprite.Height / 2) < c.Position.Y) && (c.Position.Y < (spritePosition.Y + sprite.Height / 2))) 
                        //above check if the sprite is in the height of the nacho
                        {
                            if (visible) // A character has run into the nacho while it is visible. 
                            {
                                visible = false;
                                collected = true;

                                // If rufus eats the nacho, sound plays
                                Rufus r = character as Rufus;
                                if (r != null) eatingSound.Play();  
                            }
                        }
                    }
                }
            } 
        }

    }
}
