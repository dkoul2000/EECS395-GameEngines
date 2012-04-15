/*-----------------------------------------------------------------------------
 * Class: Firebomb
 * 
 * Ron's special ability is Firebomb so these are his bullets
 -------------------------------------------------------------------------------*/


using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace _2DProject
{
    class FireBomb : DrawableGameComponent
    {
        public FireBomb(Game g, Vector2 pos, Boolean dir)
            : base(g)
        {
            spritePosition = pos;
            direction = dir;
        }


        protected override void LoadContent()
        {
            sprite = Game.Content.Load<Texture2D>("fireball");
            spriteBatch = new SpriteBatch(Game.GraphicsDevice);
        }

        private Texture2D sprite;
        private SpriteBatch spriteBatch;
        private Vector2 spritePosition;
        private Boolean direction; //Tell us whether the bullet is travelling left(false) or right(true).
        private Boolean foundtarget; //You hit a tornado! The bullet should disappear

        public Vector2 Position
        {
            get { return spritePosition; }
            set { spritePosition = value; }
        }

        public Boolean Found
        {
            get { return foundtarget; }
            set { foundtarget = value; }
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var component in Game.Components)
            {
                BrickWall bw = component as BrickWall;

                if (bw!=null)
                    {
                        if (((spritePosition.X - sprite.Width) < bw.Position.X) && (bw.Position.X < (spritePosition.X + sprite.Width / 2)))
                    //above check if the firebomb is in the width of the tornado
                    {
                        //if ((spritePosition.Y < bw.Position.Y - bw.Height / 2) && (bw.Position.Y < (spritePosition.Y + sprite.Height / 2)))
                        //above check if the sprite is in the height of the wall
                        //{
                            if (!foundtarget) //Have to make the tornado dissappear to destroy it
                            {
                                foundtarget = true;
                                bw.Destroy();  
                            }
                        //}
                    }
                }
            }
                
            if (foundtarget)
                Game.Components.Remove(this);

            //check if bullet is off screen
            if (spritePosition.X > GraphicsDevice.PresentationParameters.BackBufferWidth) //off to the right
                Game.Components.Remove(this);
            else if (spritePosition.X < 0) //off to the left
                Game.Components.Remove(this);

            if (direction)
                spritePosition.X += 5;
            else
                spritePosition.X -= 5;
        }

        public override void Draw(GameTime gameTime)
        {
            // Adjustment so that the Position refers to the center of the sprite,
            // not the top-left corner (which is the default)
            // Offset to account for centering the sprite at the position, not drawing from top-left corner
            spriteBatch.Begin();        // Begin the batch
            spriteBatch.Draw(sprite, spritePosition - new Vector2(sprite.Width / 2, sprite.Height / 2), Color.White); // Draw to the batch
            spriteBatch.End();        // end the batch,
            // causing it to render the sprite to the screen
        }
    }
}
