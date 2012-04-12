/*-----------------------------------------------------------------------------
 * Class: Bullet
 * 
 * This is a subclass of Kim since firing bullets is Kim's special move. 
 -------------------------------------------------------------------------------*/


using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace _2DProject
{
    class Bullet : GameComponent
    {
        public Bullet(Game g, Vector2 pos, Boolean dir)
            : base(g)
        {
            position = pos;
            direction = dir;
        }

        protected override void LoadContent()
        {
            sprite = Game.Content.Load<Texture2D>("bullet");
            spriteBatch = new SpriteBatch(Game.GraphicsDevice);
        }

        private Texture2D sprite;
        private SpriteBatch spriteBatch;
        private Vector2 position;
        private Boolean direction; //Tell us whether the bullet is travelling left or right

        public override void Update(GameTime gameTime)
        { 
            position.X++;
        }

        public override void Draw(GameTime gameTime)
        {
            // Adjustment so that the Position refers to the center of the sprite,
            // not the top-left corner (which is the default)
            // Offset to account for centering the sprite at the position, not drawing from top-left corner

            spriteBatch.Begin();        // Begin the batch
                    spriteBatch.Draw(sprite, position - new Vector2(sprite.Width / 2, sprite.Height / 2), Color.White); // Draw to the batch
            spriteBatch.End();        // end the batch,
            // causing it to render the sprite to the screen
        }
    }
}
