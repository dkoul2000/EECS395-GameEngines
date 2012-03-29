//Dhruv Koul and Khalid Aziz
//Quickstart XNA project
//EECS 395 - Spring 2012

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace WindowsGame1
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //objects used for sprites - vectors for position and textures for images
        Vector2 shipPosition = new Vector2(200, 200);
        Vector2 sunPosition = new Vector2(40, 90);
        Texture2D shipTexture, sunTexture;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //loaded from images folder
            shipTexture = Content.Load<Texture2D>("images/Ship");
            sunTexture = Content.Load<Texture2D>("images/Sun");

        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

            float rotationAngle = 0f;
            float elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            float circle = MathHelper.Pi * 2;
            
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                rotationAngle += elapsedTime;
                rotationAngle = rotationAngle % circle;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                rotationAngle -= elapsedTime;
                rotationAngle = rotationAngle % circle;
            }

            Vector2 shipFacingUnit = new Vector2((float)Math.Cos(rotationAngle), (float)Math.Sin(rotationAngle));

            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                shipPosition.X += shipFacingUnit.X;
                shipPosition.Y += shipFacingUnit.Y;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                shipPosition.X -= shipFacingUnit.X;
                shipPosition.Y -= shipFacingUnit.Y;
            }

            Vector2 gravityVector = Vector2.Normalize(shipPosition - sunPosition);
            gravityVector.X /= -2;
            gravityVector.Y /= -2;
            shipPosition += gravityVector;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.MidnightBlue);

            //draw sprites on screen
            spriteBatch.Begin();
            spriteBatch.Draw(shipTexture, shipPosition, null, Color.White, rotationAngle, new Vector2(0, 0),
                1.0f, SpriteEffects.None, 0f);
            spriteBatch.Draw(sunTexture, sunPosition, Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
