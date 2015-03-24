namespace BeetrootSoup.MonogameToolkit.Particles
{
    using System.Collections;
    using System.Collections.Generic;

    using BeetrootSoup.MonogameToolkit.Helpers;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class BurstParticleEmmiter : ParticleEmitter
    {
        private readonly Texture2D particleTexture;

        private readonly float scale;

        private readonly int numberOfParticles;

        private readonly float particleLifetime;

        private readonly float speed;

        private readonly float speedVariation;

        private readonly float acceleration;

        private readonly Vector2 accelerationThreshold;

        public BurstParticleEmmiter(SpriteBatch spriteBatch, Texture2D particleTexture, int numberOfParticles, float particleLifetime, float speed, float speedVariation, float acceleration, Vector2 accelerationThreshold, float scale = 1f)
            : base(spriteBatch)
        {
            this.particleTexture = particleTexture;
            this.scale = scale;
            this.numberOfParticles = numberOfParticles;
            this.particleLifetime = particleLifetime;
            this.speed = speed;
            this.speedVariation = speedVariation;
            this.acceleration = acceleration;
            this.accelerationThreshold = accelerationThreshold;

            this.Initialize();
        }

        private void Initialize()
        {
            var particleCollection = new List<Particle>();
            for (int i = 0; i < this.numberOfParticles; i++)
            {
                var particle = new Particle
                {
                    Position = this.GetAbsolutePosition(),
                    Speed = new Vector2(RandomGeneratorHelper.GetRandomSignFloat() * speed, RandomGeneratorHelper.GetRandomSignFloat() * speed) + RandomGeneratorHelper.GetRandomVector2(speedVariation),
                    Acceleration = new Vector2(this.acceleration),
                    AccelerationStopThreshold = this.accelerationThreshold,
                    Life = particleLifetime,
                    Texture = this.particleTexture,
                    Scale = new Vector2(this.scale)
                };

                particleCollection.Add(particle);
            }
            foreach (var particle in particleCollection)
            {
                this.Particles.AddLast(particle);
            }
        }
    }
}