﻿#region File Description
/*-----------------------------------------------------------------------------
 * Class: Character
 *
 * Character is a DrawableGameComponent and a base class for Kim, Ron, and Rufus.
 * All characters can move left and right (controlled using the keyboard arrow keys),
 * jump (using the up arrow), and do a character-specific special move (space bar).
 * Characters are animated sprites....
 *
 * Author: Team Possible
 *
 * Notes: Character is a base class for Kim, Ron, and Rufus
 -------------------------------------------------------------------------------*/
#endregion


using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace _2DProject
{
    class Character : DrawableGameComponent
    {
        
        //--- Member variables are always private ---//

        private Vector2 initialPosition;    // The inital position of the sprite
                                            // If the character hits an obstacle, they are sent back to this location
                                            // (a nicer alternative to dying) :]
        private Vector2 spritePosition;     // The current x, y position of the sprite
        private SpriteBatch spriteBatch;    // Create a new SpriteBatch, which can be used to draw textures.


        // Description................................................................
        private Level level;


        //--- Member variables for animated sprites ---//
        private const float Rotation = 0;
        private const float Scale = 1.0f;
        private const float Depth = 0.5f;

        protected Boolean isFacingRight;  // Whether the character is facing right...to be used for SpriteEffects


        // These variables can be adjusted to make the correct rectangle source around sprites
        // (for AnimatedTexture.AdjustSpriteFrame parameters)
        private const int Frames = 25;   
        private const int FramesPerSec = 3; //2;



        //-- These member variables are PROTECTED, not private 
        // so that the child classes can still access them -- //

        // Whether or not this character is currently selected/the active character
        protected Boolean characterIsSelected;

        // The simple character name, used to find paths to images
        // (multiple image files for different animation loops)
        protected String characterName;     

        // An array of AnimatedTexture objects (sprite sheets),
        // contains possible animation loops for every possible state/animation of character
        protected AnimatedTexture[] sprite;

        // Different AnimatedTexture (sprite sheet) for each possible animation loop/state
        protected AnimatedTexture idleTexture;          // Images for character standing still/idle
        protected AnimatedTexture runningTexture;       // Images for character moving left or right
        protected AnimatedTexture jumpingTexture;       // Images for character jumping
        protected AnimatedTexture specialMoveTexture;   // Images for character doing special move


        // The current state of the character
        // Used to determine which animation loop to use to draw the correct sprite
        enum State { Idle = 0, Running, Jumping, Specialing }
        State currentState = State.Idle;    
        

        //--- Public getters/setters for member variables ---//
        public Vector2 Position
        {
            get { return spritePosition; }
            set { spritePosition = value; }
        }

        public Boolean IsSelected
        {
            get { return characterIsSelected; }
            set { characterIsSelected = value; }
        }

        // Allows stats bar to access character name
        public String CharName 
        {
            get { return characterName; }
        }


        /*---------------------------------------------------------------------------
          Name:     AdjustAllSpriteFrames
          Purpose:  Adjust the sprite frame size and count for all animated textures
         *          for this character
          Receives: the width and height of the frame to select a sprite in the sprite
         *          sheet, the number of frames in the AnimatedTexture (sprie sheet)
          Returns:  void
        ---------------------------------------------------------------------------*/
        protected void AdjustAllSpriteFrames(int width, int height, int numFrames)
        {
            idleTexture.AdjustSpriteFrame(width, height, numFrames);
            runningTexture.AdjustSpriteFrame(width, height, numFrames);
            jumpingTexture.AdjustSpriteFrame(width, height, numFrames);
            specialMoveTexture.AdjustSpriteFrame(width, height, numFrames);

            //specialMoveTexture.IsLooping = false; //testing...................no idea how to finish##
        }


        ///
        /// Constructor
        ///
        public Character(Game g, Vector2 startingPos, String charName)
            : base(g)
        {
            spritePosition = startingPos;   // Initial position of the sprite
            
            initialPosition = startingPos;  // Position that the character will return to if they die

            // Description............................................
            this.level = (g as Game1).level;

            // Each subclass/child class/class derived from Character (Kim, Ron, Rufus)
            // specifies own name when object is constructed (in Game1.cs)
            // This name is used to generate the file paths for all images for the character!!
            characterName = charName;

            isFacingRight = true; // the sprite sheets are by default right-facing

            IsSelected = false; // default to false
                                // Kim child class overrides (she is initial starting character)


            // Initialize the array to contain the AnimatedTextures (different sprite animation loops)
            sprite = new AnimatedTexture[4]; 

            //------------### need to do this or not???
            // Just make sure these things are initialized, have to wait til loadcontent function 
            // to actually make them right
            idleTexture = new AnimatedTexture(Vector2.Zero, Rotation, Scale, Depth);
            runningTexture = new AnimatedTexture(Vector2.Zero, Rotation, Scale, Depth);
            jumpingTexture = new AnimatedTexture(Vector2.Zero, Rotation, Scale, Depth);
            specialMoveTexture = new AnimatedTexture(Vector2.Zero, Rotation, Scale, Depth);
            //------------###


            // Set device frame rate to 30 fps.
            Game.TargetElapsedTime = TimeSpan.FromSeconds(1 / 30.0);
        }


        
        /*===========================================================================
           Name:     LoadContent        (override XNA function...?)
           
           Called when the object is created in Game's Initialize() method
           Reads in whatever resources are needed for the object
         ===========================================================================*/
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(Game.GraphicsDevice);


            //-- Use characterName to generate all the appropriate file names for sprite animation loops --//

            // ************
            // Dummy code, just using kim images for all characters to see if stuff is working
            // **************- remove later!!!

            idleTexture.Load(Game.Content, "Sprites/" + characterName + "-idle", Frames, FramesPerSec);
            runningTexture.Load(Game.Content, ("Sprites/"+ characterName + "-running"), Frames, FramesPerSec);
            jumpingTexture.Load(Game.Content, "Sprites/" + characterName + "-idle", Frames, FramesPerSec);
            specialMoveTexture.Load(Game.Content, ("Sprites/" + characterName + "-specialMove"), Frames, FramesPerSec); 
            

            // Defaults.... overridden by each subclass (sprites of different sizes)
            // is there a better way to do this stuff......### ???
            // Can we just assume that every character subclass is responsible 
            // for adjusting their own textures (all 4 of them) in their own loadcontent functions?!?!
            // do we even need to provide default adjustments??
            AdjustAllSpriteFrames(30, 50, 10);

            // Add the different possible textures (animation loops) to the AnimationTexture array, sprite
            sprite[(int)State.Idle] = idleTexture;          //sprite[0]
            sprite[(int)State.Running] = runningTexture;    //sprite[1]
            sprite[(int)State.Jumping] = jumpingTexture;    //sprite[2]
            sprite[(int)State.Specialing] = specialMoveTexture; //sprite[3]
        }


        ///===========================================================================
        ///   Name:     Update       
        ///===========================================================================
        ///

        public override void Update(GameTime gameTime)
        {
            // (Currently commented the below out because looks kind of weird when 
            // non-active characters are completely still)

            // Iff the character is the currently selected character, animate it

            // Pauses and plays the animation. 
            //if (characterIsSelected) SpriteTexture.Play();
            //else SpriteTexture.Pause();

            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            sprite[(int)currentState].UpdateFrame(elapsed);


            // Unit vector in the x and y directions to increment the position of the sprite
            Vector2 deltaX = new Vector2(1f, 0f);
            Vector2 deltaY = new Vector2(0f, 1f);

            // linear gravity
            Vector2 gravity = deltaY * 2;
            
            // returns true if the character is currently resting on a platform
            bool isOnPlatform = level.isOnPlatform(spritePosition, sprite[(int)currentState].spriteHeight / 2);


            //-sprite[(int)currentState].spriteHeight)
            //TODO: Add offset from Sprite
            if (!isOnPlatform)
                spritePosition += gravity;




            //                                                          
            // ---------- User inputs via keyboard ----------           
            //                                                          

            // Get the current state of the keyboard (check if any keys were pressed)
            KeyboardState k = Keyboard.GetState();
            float i;

            if (characterIsSelected && k.IsKeyDown(Keys.Right))         // Right arrow key pressed
            {
                // Move sprite right
                i = 5f;

                // check to see if not too far right
                if (spritePosition.X < level.widthPixels - 15) // 15px is the buffer so that the sprite isn't partly off the map
                    spritePosition += i * deltaX;
               
                // Character is moving right, and thus is facing right
                isFacingRight = true;

                // The character is currently moving, so update the state to Running 
                currentState = State.Running;

                ronIsBurping = false;
            }

            else if (characterIsSelected && k.IsKeyDown(Keys.Left))     // Left arrow key pressed
            {
                // Move sprite left
                i = 5f;

                // check to see if not too far left
                if (spritePosition.X > 15) // 15px is the buffer so that the sprite isn't partly off the map
                    spritePosition -= i * deltaX;
                // else do nothing

                // Character is moving left, and thus is facing left
                isFacingRight = false;

                // The character is currently moving, so update the state to Running 
                currentState = State.Running;

                ronIsBurping = false;
            }

            else if (characterIsSelected && k.IsKeyDown(Keys.Up))       // Up arrow key pressed
            {
               
                //spritePosition.Y -= 5.0f;
                if (isOnPlatform || currentState == State.Jumping)
                    Jump();

                //if ((!level.isHeadNearPlatform(spritePosition - new Vector2(0, this.jumpingTexture.spriteHeight / 2))) &&
                //    currentState != State.Jumping)
                //    Jump();
            }

            else if (characterIsSelected && k.IsKeyDown(Keys.Down))     // Down arrow key pressed
            {
                //add some code to see if we're not on a platform (shouldn't do anything), else move downwards
                if (!isOnPlatform)
                {
                    i = 3f;
                    spritePosition += i * deltaY;
                    i += 3f;
                }

                ronIsBurping = false;
            }

            else if (characterIsSelected && k.IsKeyDown(Keys.Space))    // Space bar pressed
            {
                // do special character-specific move

                // The character is currently jumping, so adjust the state to Jumping???.... or inside Jump() function? 
                SpecialMove();
                currentState = State.Specialing;


            }
            else if (keepRufusRunning)
                //This is hacky, but any changes in subclass to the state are overwritten here
                currentState = State.Running;
            else  //no key was pressed
            {
                // The character is currently standing still, so update the state to Idle 
                if (currentState != State.Jumping)
                    currentState = State.Idle;
            }


            //  collision detection - edited from Nacho class...
            //...might want something more elegant/sophisticated later
            foreach (var obstacle in Game.Components)
            {
                int width = sprite[(int)currentState].spriteWidth;
                int height = sprite[(int)currentState].spriteHeight;

                Tornado t = obstacle as Tornado;
                if (t != null) // there is a tornado (visual studio required me to check for this)
                {
                    // Check if the sprite is in the width of the obstacle
                    if (((spritePosition.X - width / 2) < t.Position.X) &&
                        (t.Position.X < (spritePosition.X + width / 2)))
                    
                    {
                        // Check if the sprite is also in the height of the obstacle
                        if (((spritePosition.Y - height / 2) < t.Position.Y) &&
                            (t.Position.Y < (spritePosition.Y + height / 2)))
                        {
                            // A character has run into an obstacle while it is visible.

                            Reset();
                            /*if (visible)  
                            {
                                visible = false;
                                collected = true;
                            }*/
                        }
                    }
                }

                BrickWall wall = obstacle as BrickWall;
                if (wall != null)
                {
                    // Check if the sprite is in the width of the obstacle
                    if (((spritePosition.X - width / 2) < wall.Position.X) &&
                        (wall.Position.X < (spritePosition.X + width / 2)))
                    {
                        // Check if the sprite is also in the height of the obstacle
                        if (((spritePosition.Y - height / 2) < wall.Position.Y) &&
                            (wall.Position.Y < (spritePosition.Y + height / 2)))
                        {
                            // A character has run into an obstacle while it is visible.

                            // This is hacky, all it does is push the character back right
                            // (due to placement of wall, this is okay) 
                            // so that they can't pass through it
                            if (!ronIsBurping)
                            {
                                i = 25f;
                                spritePosition += i * deltaX;
                            }


                            // THIS IS SO HACKY, FIX LATER ###
                            if (ronIsBurping && currentState == State.Specialing)
                            {

                                wall.Destroy();
                            }
                            

                            /*if (visible)  
                            {
                                visible = false;
                                collected = true;
                            }*/
                        }
                    }

                }


            }

        }

        /// <summary>
        /// Sends the character back to their initial starting position.
        /// Intended as a nicer alternative to dying upon colliding with an obstacle.
        /// </summary>
        private void Reset()
        {
            spritePosition = initialPosition;
        }

        /*---------------------------------------------------------------------------
          Name:     Jump
          Purpose:  Makes character jump when up arrow key pressed
          Receives: none
          Returns:  void
        ---------------------------------------------------------------------------*/
        int jumpTimer = 0;
        private void Jump()
        {
            Vector2 deltaY = new Vector2(0,1);
            
            if (currentState != State.Jumping)
            {
                jumpTimer = 15;
                currentState = State.Jumping;
            }

            if (jumpTimer != 0)
            {
                jumpTimer--;
                spritePosition -= 7 * deltaY;
            }
            else
            {
                currentState = State.Idle;
            }

        }

        /*---------------------------------------------------------------------------
          Name:     SpecialMove
          Purpose:  Makes character do special move
                    Virtual function, which makes this an abstract base class
                    Each child class MUST define their own implementation of this function
          Receives: none
          Returns:  void
        ---------------------------------------------------------------------------*/
        virtual protected void SpecialMove()
        {
            //..................................................states...*** or outside of loop?
            currentState = State.Specialing;
            
            
            //sprite[(int)currentState].PlayAnimation(); no idea how to go about this....

            // reset state at the end of this??? inside or outside of loop?
        }

        ///===========================================================================
        ///   Name:     Draw       
        ///===========================================================================
        public override void Draw(GameTime gameTime)
        {

            // Offset = adjustment so that the Position refers to the center of the sprite,
            // not the top-left corner (which is the default)
            Vector2 offset = new Vector2(sprite[(int)currentState].spriteWidth / 2, sprite[(int)currentState].spriteHeight / 2);

            spriteBatch.Begin();                                            // Begin the batch

            sprite[(int)currentState].DrawFrame(spriteBatch, spritePosition - offset, isFacingRight);

            spriteBatch.End();                                              // end the batch,
                                                                            // causing it to render the sprite to the screen
        }


        protected bool keepRufusRunning;

        public bool runRufus
        {
            get { return keepRufusRunning; }
            set { keepRufusRunning = value; }
        }

        // -- hacky stuff for quick fixes ... burping ron disables wall
        protected bool ronIsBurping;
        public bool isRonBurping
        {
            get { return ronIsBurping; }
        }

    }   
}