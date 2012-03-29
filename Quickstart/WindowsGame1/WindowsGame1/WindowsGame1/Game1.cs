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


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            //draw sprites on screen
            spriteBatch.Begin();
            spriteBatch.Draw(shipTexture, shipPosition, Color.White);
            spriteBatch.Draw(sunTexture, sunPosition, Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
