using System;
using Microsoft.Xna.Framework;

namespace Particle2D.Patterns
{
    public class CircleEmissionPattern : IEmissionPattern
    {
        private readonly float radius;

        public CircleEmissionPattern(float radius)
        {
            this.radius = radius;
        }

        public Vector2 CalculateParticlePosition(Random random, Vector2 emitPosition)
        {
            var rads = (float) (random.NextDouble()*MathHelper.TwoPi);
            var offset = new Vector2((float) Math.Cos(rads)*radius, (float) Math.Sin(rads)*radius);

            return Vector2.Add(emitPosition, offset);
        }
    }
}