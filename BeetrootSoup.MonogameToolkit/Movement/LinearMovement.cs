namespace BeetrootSoup.MonogameToolkit.Movement
{
    using Microsoft.Xna.Framework;

    public class LinearMovement : IMovementPattern
    {
        private Vector2 Speed;

        public LinearMovement(Vector2 speedPerSecond)
        {
            this.Speed = speedPerSecond;
        }

        public Vector2 GetPosition(GameTime gameTime)
        {
            return this.Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
    }
}