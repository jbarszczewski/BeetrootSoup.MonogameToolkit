namespace BeetrootSoup.MonogameToolkit.Layout
{
    using System.Collections.Generic;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class ParallaxObject : Node
    {
        private readonly SpriteBatch spriteBatch;

        public IList<ParallaxLayer> ParallaxLayers;

        public Vector2 PositionOffset;

        public ParallaxObject(SpriteBatch spriteBatch)
        {
            this.spriteBatch = spriteBatch;
            this.ParallaxLayers = new List<ParallaxLayer>();
            this.PositionOffset = Vector2.Zero;
        }
        
        public override void Draw(GameTime gameTime)
        {
            foreach (ParallaxLayer parallaxLayer in this.ParallaxLayers)
            {
                spriteBatch.Draw(
              texture: parallaxLayer.Texture,
              position: base.GetAbsolutePosition() + parallaxLayer.PositionOffset + this.PositionOffset * parallaxLayer.OffsetFactor,
              rotation: base.GetAbsoluteRotation(),
              origin: new Vector2(parallaxLayer.Texture.Width / 2f, parallaxLayer.Texture.Height / 2f));
            }

            base.Draw(gameTime);
        }
    }
}