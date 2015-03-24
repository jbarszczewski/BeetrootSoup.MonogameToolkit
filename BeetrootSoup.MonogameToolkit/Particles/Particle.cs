namespace BeetrootSoup.MonogameToolkit.Particles
{
    using System;

    using BeetrootSoup.MonogameToolkit.Helpers;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class Particle
    {
        public Texture2D Texture;
        public Vector2 Scale;

        public Vector2 Speed;
        public Vector2 Acceleration;
        public Vector2 AccelerationStopThreshold = Vector2.Zero;
        public Vector2 Position;
        public float Life;

        private bool UpdateX = true;
        private bool UpdateY = true;

        public void Update()
        {
            if (this.UpdateX)
            {
                var sign = AnotherMathHelper.GetSign(this.Speed.X);
                this.Speed.X += sign * this.Acceleration.X;
                if ((sign.Equals(1f) && this.Speed.X < this.AccelerationStopThreshold.X) || (sign.Equals(-1f) && this.Speed.X >= this.AccelerationStopThreshold.X))
                {
                    this.Speed.X = this.AccelerationStopThreshold.X;
                    this.UpdateX = false;
                }
            }

            if (this.UpdateY)
            {
                var sign = AnotherMathHelper.GetSign(this.Speed.Y);
                this.Speed.Y += sign * this.Acceleration.Y;
                if ((sign.Equals(1f) && this.Speed.Y < this.AccelerationStopThreshold.Y) || (sign.Equals(-1f) && this.Speed.Y >= this.AccelerationStopThreshold.Y))
                {
                    this.Speed.Y = this.AccelerationStopThreshold.Y;
                    this.UpdateY = false;
                }
            }

            this.Position += this.Speed;
        }
    }
}
