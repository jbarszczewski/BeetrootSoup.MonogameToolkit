
namespace BeetrootSoup.MonogameToolkit.Layout
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using System.Collections.Generic;

    public class LayerNode : Node
    {
        private readonly SpriteBatch spriteBatch;
        internal readonly IList<Effect> Effects;

        public DynamicCamera Camera { get; set; }

        public LayerNode(SpriteBatch spriteBatch)
        {
            this.spriteBatch = spriteBatch;
            this.Effects = new List<Effect>();
        }

        public void AddEffect(Effect effect)
        {
            this.Effects.Add(effect);
        }
        
        public void RemoveEffect(Effect effect)
        {
            this.Effects.Remove(effect);
        }

        public override void Draw(GameTime gameTime)
        {
            //this.spriteBatch.Begin(SpriteSortMode.Immediate, transformMatrix: this.Camera.TranslationMatrix);
            this.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.Default, RasterizerState.CullCounterClockwise, null, this.Camera != null ? this.Camera.TranslationMatrix : new Matrix());
            foreach (var effect in this.Effects)
            {
                effect.CurrentTechnique.Passes[0].Apply();
            }

            foreach (var node in this.Nodes)
            {
                node.Draw(gameTime);
            }

            this.spriteBatch.End();
        }
    }
}