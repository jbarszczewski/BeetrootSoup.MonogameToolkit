namespace BeetrootSoup.MonogameToolkit.Helpers
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public static class TextureGenerator
    {
        public static GraphicsDevice GraphicsDevice;

        public static Texture2D CreateTexture(int width, int height, Color color)
        {
            var result = new Texture2D(GraphicsDevice, width, height);
            var pixelData = new Color[width * height];
            for (int i = 0; i < pixelData.Length; i++) 
                pixelData[i] = color;
            result.SetData(pixelData);
            return result;
        }
    }
}
