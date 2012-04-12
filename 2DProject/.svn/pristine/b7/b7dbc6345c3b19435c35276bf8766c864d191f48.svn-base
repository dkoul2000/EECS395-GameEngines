#region File Description
/*-----------------------------------------------------------------------------
 * Class: BrickWall
 *
 * BrickWall is an obstacle Ron must defeat in order to get a Nacho.
 * Ron can break the brick wall by burping fire on it.
 * 
 * actually jk, right now only kims bullets destroy it
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
    class BrickWall : DrawableGameComponent
    {
        public BrickWall(Game g, Vector2 pos, String img)
            : base(g)
        {
            spritePosition = pos;   //initial position
            destroyed = false;      //checks if the wall still exists
            imgName = img;      // to use in load content function
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

        public void Destroy()
        {
            destroyed = true;
        }


        //--- Member variables are always private ---//
        private Vector2 spritePosition;
        private String imgName;
        private Texture2D wallTexture;
        private SpriteBatch spriteBatch;

        private bool destroyed; //keeps track of whether or not this obstacle has been destroyed




        // Called when the object is created in Game's Initialize() method
        // Reads in whatever resources are needed for the object
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(Game.GraphicsDevice);
            wallTexture = Game.Content.Load<Texture2D>(imgName);
        }


        public override void Draw(GameTime gameTime)
        {
            if (!destroyed)
            {
                Vector2 offset = new Vector2(wallTexture.Width / 2, wallTexture.Height / 2);

                spriteBatch.Begin();                                            // Begin the batch
                spriteBatch.Draw(wallTexture, spritePosition - offset, Color.White); // Draw to the batch
                spriteBatch.End();                                              // end the batch,
                // causing it to render the sprite to the screen// causing it to render the sprite to the screen
            }
        }


        public override void Update(GameTime gameTime)
        {
            //if the tornado has been destroyed don't do all this!
            if (destroyed)
                Game.Components.Remove(this);


            // primarily collision detection - same loop from tornado
            foreach (var component in Game.Components)
            {
                int width = wallTexture.Width;
                int height = wallTexture.Height;
                /*
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
                }*/

                Ron r = component as Ron;
                if (r != null)
                {
                    if (((spritePosition.X - width) < r.Position.X) &&
                        (r.Position.X < (spritePosition.X + width / 2)) &&
                        ((spritePosition.Y - height / 2) < r.Position.Y) &&
                        (r.Position.Y < (spritePosition.Y + height / 2)) &&
                        r.isRonBurping)
                    {
                       // Super hacky way to detect if ron is colliding with the wall 
                        // and burping, therefore destroying it
                        if (!destroyed) //Have to make the tornado dissappear to destroy it
                        {
                            destroyed = true;
                        }
                    }


                }
            }//end of this massive foreach loop
        }

    }
}
