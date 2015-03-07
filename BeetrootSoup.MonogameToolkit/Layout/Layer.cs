
namespace BeetrootSoup.MonogameToolkit.Layout
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using System.Collections.Generic;

    public class Layer : Node
    {
        private readonly SpriteBatch spriteBatch;
        private readonly IList<Effect> effects;

        public DynamicCamera Camera { get; set; }

        public Layer(SpriteBatch spriteBatch)
        {
            this.spriteBatch = spriteBatch;
            this.effects = new List<Effect>();
        }

        public void AddEffect(Effect effect)
        {
            this.effects.Add(effect);
        }
        
        public void RemoveEffect(Effect effect)
        {
            this.effects.Remove(effect);
        }

        public override void Draw(GameTime gameTime)
        {
            this.spriteBatch.Begin(SpriteSortMode.Immediate, transformMatrix: this.Camera.TranslationMatrix);
            foreach (var effect in this.effects)
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