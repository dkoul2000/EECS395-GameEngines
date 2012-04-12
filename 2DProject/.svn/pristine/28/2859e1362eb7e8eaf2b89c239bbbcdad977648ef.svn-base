#region File Description
/*-----------------------------------------------------------------------------
 * Class: Level ----> can we rename this to platform?
 * 
 * I get so confused as to whether it is a level (suggesting we have multiple levels
 * of game play) or if it is a platform to stand on.... -lizz
 * 
 -------------------------------------------------------------------------------*/
#endregion


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



namespace _2DProject
{
    public class Level : DrawableGameComponent
    {
        private SpriteBatch spriteBatch;

        // image of one platform unit
        private Texture2D platformTexture;

        // a 2-d array that corresponds to the platform layout in the level
        public int[,] platformArray;

        // sections the level is divided into; so it's 16 units wide and 4 units tall
        public const int columns = 16;
        public const int rows = 6;
        public const int titleOffset = 120;

        // level width and height in pixels
        public int widthPixels;
        public int heightPixels;
        ContentManager content;

        public Level(Game game)
            : base(game)
        {
            // TODO: Construct any child components here
            content = game.Content;
        }

        public override void Initialize()
        {
            platformArray = new int[rows, columns] {  {1,0,1,0, 0,1,1,0, 0,1,1,1, 0,1,1,1},
                                                      {1,1,0,1, 0,1,0,1, 0,1,0,1, 0,0,1,0},
                                                      {1,1,1,0, 1,0,1,0, 1,0,1,0, 0,1,1,1},
                                                      {1,0,1,0, 0,1,1,0, 0,1,1,1, 0,1,1,0},
                                                      {1,1,1,0, 0,1,1,0, 0,1,1,1, 0,1,1,1},
                                                      {1,1,0,1, 1,0,0,1, 0,1,0,1, 0,0,0,1}};

            base.Initialize();
        }


        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(Game.GraphicsDevice);
            platformTexture = content.Load<Texture2D>("platform");

        }

        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            int platformSeparation = 60;
            spriteBatch.Begin();
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    if (platformArray[i, j] != 0)
                        spriteBatch.Draw(platformTexture,
                            new Vector2(j * platformTexture.Width, titleOffset + platformSeparation * i),
                            Color.White);
                }
            }

            spriteBatch.End();

        }

        public bool isOnPlatform(Vector2 objPosition, int heightOffset)
        {


            if (objPosition.Y + heightOffset < titleOffset)
                return false;
            else if (objPosition.Y + heightOffset >= heightPixels || objPosition.X < 0 || objPosition.X > widthPixels)
                return true;

            // find out whether character is near one of the lines that can have platforms
            bool isOnPlatform = (((int)objPosition.Y + heightOffset - titleOffset) % (((heightPixels - titleOffset) / rows)) <= 3);

            int[] cell = currentCell(objPosition, heightOffset);

            // find out whether a platform exists in the cell the character is in.
            if (platformArray[(int)cell[1], (int)cell[0]] == 0)
                isOnPlatform = false;
            Console.WriteLine("Cell X: " + cell[0] + "; Cell Y: " + cell[1]);

            return isOnPlatform;

        }

        public int[] currentCell(Vector2 objPosition, int heightOffset)
        {
            int[] cell = new int[2];
            cell[0] = ((int)objPosition.X / (widthPixels / columns));
            cell[1] = (((int)objPosition.Y + heightOffset - titleOffset) / ((heightPixels - titleOffset) / (rows)));

            return cell;
        }

        public bool isHeadNearPlatform(Vector2 objPosition, int heightOffset)
        {
            return false;

            //int diff = (((int)objPosition.Y + heightOffset - titleOffset) / ((heightPixels - titleOffset) / (rows)));
            //bool headNearPlatform = (diff <= 16);
            // Console.WriteLine(headPosition);

            int[] bodyCell = currentCell(objPosition, 0);
            int[] headCell = currentCell(objPosition + new Vector2(0, heightOffset), 0);
            int[] feetCell = currentCell(objPosition + new Vector2(0, -heightOffset), 0);

            // if on topmost row, no further calculations
            if (feetCell[1] == 0)
            {
                return false;
            }

            //Vector2 temp = new Vector2(currCell[0], currCell[1]);

            //if (headNearPlatform)
            //    Console.WriteLine(temp);

            // if platform right above, prevent more jumping
            if (platformArray[(int)bodyCell[1], (int)bodyCell[0]] == platformArray[(int)headCell[1], (int)headCell[0]])
            {
                if (platformArray[(int)bodyCell[1] - 1, (int)bodyCell[0]] == 1)
                    return true;
            }

            return false;
        }

    }

}