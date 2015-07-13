using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Particle2D.Modifiers;
using Particle2D.Patterns;

namespace Particle2D
{
    public class ParticleEffect2D
    {
        private static readonly Random Random = new Random();
        private readonly Queue<Particle2D> freeParticles = new Queue<Particle2D>();
        private readonly List<IModifier> modifiers = new List<IModifier>();
        private readonly Particle2D[] particles;
        private readonly List<Texture2D> textures = new List<Texture2D>();
        private int emissionAmount = 5;
        private float emissionSpeed = 1f;
        private Color particleColor = Color.White;

        public ParticleEffect2D(int maxParticles, int particleLifespan)
        {
            ParticleLifespan = particleLifespan;
            particles = new Particle2D[maxParticles];

            for (var i = 0; i < particles.Length; i++)
            {
                particles[i] = new Particle2D();
                freeParticles.Enqueue(particles[i]);
            }
        }

        public bool IsActive
        {
            get { return ActiveParticleCount > 0; }
        }

        public int ActiveParticleCount { get; private set; }

        public int EmissionAmount
        {
            get { return emissionAmount; }
            set { emissionAmount = value; }
        }

        public float EmissionSpeed
        {
            get { return emissionSpeed; }
            set { emissionSpeed = value; }
        }

        public int ParticleLifespan { get; set; }

        public Color ParticleColor
        {
            get { return particleColor; }
            set { particleColor = value; }
        }

        public IEmissionPattern EmissionPattern { get; set; }

        public List<IModifier> Modifiers
        {
            get { return modifiers; }
        }

        public List<Texture2D> Textures
        {
            get { return textures; }
        }

        public void Emit(float totalMilliseconds, Vector2 position)
        {
            if (textures.Count == 0)
                throw new InvalidOperationException("Error emitting particles - no textures have be specified.");

            if (freeParticles.Count >= 1)
            {
                var totalParticlesToEmit = MathHelper.Clamp(Random.Next((int) (emissionAmount*0.8f), emissionAmount), 1,
                    freeParticles.Count);

                for (var i = 0; i < totalParticlesToEmit; i++)
                {
                    var emitPosition = position;
                    var texture = textures[Random.Next(textures.Count)];

                    if (EmissionPattern != null)
                    {
                        emitPosition = EmissionPattern.CalculateParticlePosition(Random, position);
                    }

                    var angle = MathHelper.ToRadians(Random.Next(360));
                    var velocity = Vector2.Transform(new Vector2((float) Random.NextDouble()*emissionSpeed, 0),
                        Matrix.CreateRotationZ(angle));

                    var newParticle = freeParticles.Dequeue();
                    newParticle.Initialize(texture, emitPosition, velocity, particleColor, ParticleLifespan,
                        totalMilliseconds);

                    foreach (var modifier in modifiers)
                        modifier.Update(0f, totalMilliseconds, 0, newParticle);

                    ActiveParticleCount++;
                }
            }
        }

        public void Update(float totalMilliseconds, float elapsedSeconds)
        {
            if (IsActive)
            {
                foreach (var particle in particles)
                {
                    if (particle.IsAlive)
                    {
                        var particleAge = (totalMilliseconds - particle.InceptionTime)/particle.Lifespan;

                        foreach (var modifier in modifiers)
                            modifier.Update(particleAge, totalMilliseconds, elapsedSeconds, particle);

                        particle.Update(totalMilliseconds);

                        if (!particle.IsAlive)
                        {
                            freeParticles.Enqueue(particle);
                            ActiveParticleCount--;
                        }
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (IsActive)
            {
                foreach (var particle in particles)
                {
                    if (particle.IsAlive)
                    {
                        particle.Draw(spriteBatch);
                    }
                }
            }
        }
    }
}