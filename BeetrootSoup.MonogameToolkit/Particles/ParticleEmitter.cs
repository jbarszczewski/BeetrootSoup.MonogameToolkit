namespace BeetrootSoup.MonogameToolkit.Particles
{
    using System.Collections.Generic;

    using BeetrootSoup.MonogameToolkit.Layout;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class ParticleEmitter : Node
    {
        public int ParticlesPerSecond;
        private readonly SpriteBatch spriteBatch;

        public LinkedList<Particle> particles;

        public ParticleEmitter(SpriteBatch spriteBatch)
        {
            this.spriteBatch = spriteBatch;
            this.particles = new LinkedList<Particle>();
        }

        public override void Update(GameTime gameTime)
        {
            var currentParticle = particles.First;
            while (currentParticle !=null)
            {
                currentParticle.Value.Life -= gameTime.ElapsedGameTime.Milliseconds;
                if (currentParticle.Value.Life <= 0)
                {
                    this.particles.Remove(currentParticle);
                }
                else
                {
                    currentParticle.Value.Update();
                }

                currentParticle = currentParticle.Next;
            }
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            var currentParticle = particles.First;
            while (currentParticle != null)
            {
                Particle particle = currentParticle.Value;
                spriteBatch.Draw(
                texture: particle.Texture,
                position: particle.Position,
                scale: particle.Scale,
                origin: new Vector2(particle.Texture.Width / 2f, particle.Texture.Height / 2f));

                currentParticle = currentParticle.Next;
            }

            base.Draw(gameTime);
        }
    }
}
