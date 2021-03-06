﻿namespace BeetrootSoup.MonogameToolkit.Layout
{
    using Microsoft.Xna.Framework;
    using System;

    public class DynamicCamera
    {
        // Construct a new Camera class with standard zoom (no scaling)
        public DynamicCamera()
        {
            Zoom = 1.0f;
        }

        // Centered Position of the Camera in pixels.
        public Vector2 Position { get; set; }
        // Current Zoom level with 1.0f being standard
        // 0.5f Means 2x zoom out
        public float Zoom { get; set; }
        // Current Rotation amount with 0.0f being standard orientation
        public float Rotation { get; set; }

        // Height and width of the viewport window which we need to adjust
        // any time the player resizes the game window.
        public int ViewportWidth { get; set; }
        public int ViewportHeight { get; set; }

        // Center of the Viewport which does not account for scale
        public Vector2 ViewportCenter
        {
            get { return new Vector2(ViewportWidth * 0.5f, ViewportHeight * 0.5f); }
        }

        // Create a matrix for the camera to offset everything we draw,
        // the map and our objects. since the camera coordinates are where
        // the camera is, we offset everything by the negative of that to simulate
        // a camera moving. We also cast to integers to avoid filtering artifacts.
        public Matrix TranslationMatrix
        {
            get
            {
                return Matrix.CreateTranslation(-(int)Position.X,
                   -(int)Position.Y, 0) *
                   Matrix.CreateRotationZ(Rotation) *
                   Matrix.CreateScale(new Vector3(Zoom, Zoom, 1)) *
                   Matrix.CreateTranslation(new Vector3(ViewportCenter, 0));
            }
        }

        public Rectangle ViewportWorldBoundry()
        {
            Vector2 viewPortCorner = ScreenToWorld(new Vector2(0, 0));
            Vector2 viewPortBottomCorner =
               ScreenToWorld(new Vector2(ViewportWidth, ViewportHeight));

            return new Rectangle((int)viewPortCorner.X,
               (int)viewPortCorner.Y,
               (int)(viewPortBottomCorner.X - viewPortCorner.X),
               (int)(viewPortBottomCorner.Y - viewPortCorner.Y));
        }

        // Center the camera on specific pixel coordinates
        public void CenterOn(Vector2 position)
        {
            Position = position;
        }

        public Vector2 WorldToScreen(Vector2 worldPosition)
        {
            return Vector2.Transform(worldPosition, TranslationMatrix);
        }

        public Vector2 ScreenToWorld(Vector2 screenPosition)
        {
            return Vector2.Transform(screenPosition,
                Matrix.Invert(TranslationMatrix));
        }

        //// Move the camera's position based on input
        //public void HandleInput(InputState inputState,
        //   PlayerIndex? controllingPlayer)
        //{
        //    Vector2 cameraMovement = Vector2.Zero;

        //    if (inputState.IsScrollLeft(controllingPlayer))
        //    {
        //        cameraMovement.X = -1;
        //    }
        //    else if (inputState.IsScrollRight(controllingPlayer))
        //    {
        //        cameraMovement.X = 1;
        //    }
        //    if (inputState.IsScrollUp(controllingPlayer))
        //    {
        //        cameraMovement.Y = -1;
        //    }
        //    else if (inputState.IsScrollDown(controllingPlayer))
        //    {
        //        cameraMovement.Y = 1;
        //    }
        //    if (inputState.IsZoomIn(controllingPlayer))
        //    {
        //        AdjustZoom(0.25f);
        //    }
        //    else if (inputState.IsZoomOut(controllingPlayer))
        //    {
        //        AdjustZoom(-0.25f);
        //    }

        //    // When using a controller, to match the thumbstick behavior,
        //    // we need to normalize non-zero vectors in case the user
        //    // is pressing a diagonal direction.
        //    if (cameraMovement != Vector2.Zero)
        //    {
        //        cameraMovement.Normalize();
        //    }

        //    // scale our movement to move 25 pixels per second
        //    cameraMovement *= 25f;

        //    MoveCamera(cameraMovement, true);
        //}
    }
}
