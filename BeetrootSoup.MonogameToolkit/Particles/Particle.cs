namespace BeetrootSoup.MonogameToolkit.Particles
{
    using BeetrootSoup.MonogameToolkit.Layout;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class Particle
    {
        public Texture2D Texture;
        public Vector2 Scale;

        public Vector2 Speed;
        public Vector2 Acceleration;
        public Vector2 Position;
        public float Life;

        public void Update()
        {
            this.Speed += this.Acceleration;
            this.Position += this.Speed;
        }
    }
}
