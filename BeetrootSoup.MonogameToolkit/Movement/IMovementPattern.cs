namespace BeetrootSoup.MonogameToolkit.Movement
{
    using Microsoft.Xna.Framework;

    public interface IMovementPattern
    {
        Vector2 GetPosition(GameTime gameTime);
    }
}
