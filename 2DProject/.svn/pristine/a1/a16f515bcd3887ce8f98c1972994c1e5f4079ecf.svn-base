#region File Description
//-----------------------------------------------------------------------------
// AnimatedTexture.cs
//
// AnimatedTexture draws the sprite using the subrectangle of the texture that contains the desired animation.
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//
// Tutorial from: http://msdn.microsoft.com/en-us/library/bb203866.aspx
// for animating sprites
//
// Some edits and commenting by: Lizz Bartos
// Also used Animation.cs and AnimationPlayer.cs for reference from platformer...
//
// maybe better name would be AnimatedLoop? SpriteSheet?
//-----------------------------------------------------------------------------
#endregion

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace _2DProject
{
    public class AnimatedTexture
    {

        private Texture2D spriteSheet;    // The sprite sheet (formerly called myTexture)

        public float Rotation, Scale, Depth;
        public Vector2 Origin;          // Origin of the texture

        private int frameCount;         // Total number of frames in the animation loop
        private float TimePerFrame;     // Duration of time to show each frame
        private int Frame;              // The current frame number/frame index
        private float TotalElapsed;     // The amount of time (in seconds) that the current frame has been shown for
        private bool Paused;            // Whether or not the animation is currently paused

        private Rectangle sourceRect;   // Rectangle that highlights the appropriate sprite in the sprite sheet

        private int frameWidth;     // The width of the box that highlights the current sprite image in the sheet
        private int frameHeight;    // The height of the box that highlights the current sprite image in the sheet


        //testing....------------------###
        //private bool animationIsPlaying = false;
        //----

        /// <summary>
        /// When the end of the animation is reached, should it
        /// continue playing from the beginning?
        /// </summary>
        /*public bool IsLooping
        {
            get { return isLooping; }
            set { isLooping = value; }
        }
        bool isLooping;*/
        //---------------------testing end###

        //--- Public getters/setters for member variables ---//

        public int spriteWidth
        {
            get { return frameWidth; }
            set { frameWidth = value; }
        }

        public int spriteHeight
        {
            get { return frameHeight; }
            set { frameHeight = value; }
        }

        /*---------------------------------------------------------------------------
          Name:     AdjustSpriteFrame
          Purpose:  Allows the source frame for the current sprite to be adjusted
                    (by the Character class)
          Receives: the width and height for the sprite frame,
                    and the number of frames in the animation loop
          Returns:  void
        ---------------------------------------------------------------------------*/
        public void AdjustSpriteFrame(int width, int height, int numFrames)
        {
            frameWidth = width;
            frameHeight = height;
            frameCount = numFrames;
        }

        ///
        /// AnimatedTexture Constructor
        ///
        public AnimatedTexture(Vector2 origin, float rotation, float scale, float depth)
        {
            this.Origin = origin;
            this.Rotation = rotation;
            this.Scale = scale;
            this.Depth = depth;


            //...........
            //isLooping = true; //.........default true animation continues going
            // rather than playing once through ( like for special move or jumping????)
        }


        //
        // Load one or more textures to provide the image data for the animation.
        // Loads a single texture and divides it into frames of animation.
        // It uses the last parameter to determine how many frames to draw each second.
        //
        public void Load(ContentManager content, string asset, 
            int count, int framesPerSec)
        {
            frameCount = count;
            spriteSheet = content.Load<Texture2D>(asset);
            TimePerFrame = (float)1 / 5;
            Frame = 0;
            TotalElapsed = 0;
            Paused = false;

            // Defaults - make sure that frameWidth, frameHeight, and sourceRect
            // are initialized to reasonable values
            // May need to adjust per character --> use AdjustSpriteFrame from Character class
            frameWidth = spriteSheet.Width / frameCount;
            frameHeight = spriteSheet.Height / frameCount;
            sourceRect = new Rectangle(frameWidth * Frame, 0, frameWidth, frameHeight);
        }


        /// <summary>
        /// Determines which animation frame to display by taking the elapsed seconds between updates as a parameter.
        /// </summary>
        public void UpdateFrame(float elapsed)
        {
            if (Paused)
                return;

            // Process passing time.
            TotalElapsed += elapsed;
            if (TotalElapsed > TimePerFrame)
            {
               /* if (!isLooping)
                {
                    animationIsPlaying = true;
                    PlayAnimation();
                }
                else
                {*/
                    
                    Frame++;    // Increment to the next Frame number
                    // Keep the Frame between 0 and the total frames, minus one.
                    // i.e., if the sourceRect gets to the end of the sprite sheet,
                    // go back to the beginning of the sprite sheet
                    Frame = Frame % frameCount;
                    TotalElapsed -= TimePerFrame;
                    
                
                    // Advance the frame index; looping or clamping as appropriate.
                    /*  if (isLooping)
                      {
                          //frameIndex = (frameIndex + 1) % Animation.FrameCount;
                          Frame = (Frame + 1) % frameCount;
                      }
                      else
                      {
                          //frameIndex = Math.Min(frameIndex + 1, Animation.FrameCount - 1);
                          Frame = Math.Min(Frame + 1, frameCount - 1);
                      }*/
                //}
            }
        }

        /// 
        /// DrawFrame - wrapper method to be called from Character Class
        /// 
        public void DrawFrame(SpriteBatch batch, Vector2 screenPos, Boolean isFacingRight)
        {
            // Use the direction that the character is moving
            // to decide whether or not to horizontally flip the sprite
            // (sprite sheets depict characters facing right)
            SpriteEffects flip = SpriteEffects.FlipHorizontally;
            if (isFacingRight) flip = SpriteEffects.None;

            DrawFrame(batch, Frame, screenPos, flip);
        }

        private void DrawFrame(SpriteBatch batch, int frame, Vector2 screenPos, SpriteEffects flip)
        {
            // Calculate the source rectangle of the current frame.
            sourceRect = new Rectangle(frameWidth * frame, 0, frameWidth, frameHeight);

            // Draw the current frame.
            batch.Draw(spriteSheet, screenPos, sourceRect, Color.White,
                Rotation, Origin, Scale, flip, Depth);
        }

        //
        //--- Functions to control the animation of the sprite ---//
        //

        //testing....
        /// <summary>
        /// Begins or continues playback of an animation.
        /// </summary>
      /*  public void PlayAnimation() // plays full animation, does not allow it to restart in middle
        {
            // If this animation is already running, do not restart it.
            if (animationIsPlaying)
                return;
            
            Frame = 0; //frameindex = 0
            TotalElapsed = 0;
            // Process passing time.
            //TotalElapsed += elapsed;
            if (TotalElapsed > TimePerFrame)
            {
                Frame = Math.Min(Frame + 1, frameCount - 1);
            }
            
                animationIsPlaying = false;
        }*/
                





        public void Play() { Paused = false; }

        public void Pause() { Paused = true; }

        public bool IsPaused {  get { return Paused; }  }

        public void Stop()
        {
            Pause();
            Reset();
        }

        public void Reset()
        {
            Frame = 0;
            TotalElapsed = 0f;
        }

    }
}