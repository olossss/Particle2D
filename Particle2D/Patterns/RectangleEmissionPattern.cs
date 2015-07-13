using System;
using Microsoft.Xna.Framework;

namespace Particle2D.Patterns
{
    public class RectangleEmissionPattern : IEmissionPattern
    {
        private readonly int height;
        private readonly int width;

        public RectangleEmissionPattern(int width, int height)
        {
            this.width = width;
            this.height = height;
        }

        public Vector2 CalculateParticlePosition(Random random, Vector2 emitPosition)
        {
            var halfWidth = width/2f;
            var halfHeight = height/2f;

            var offset = Vector2.Zero;
            offset.X = (float) ((halfWidth - (-halfWidth))*random.NextDouble() + (-halfWidth));
            offset.Y = (float) ((halfHeight - (-halfHeight))*random.NextDouble() + (-halfHeight));

            return Vector2.Add(emitPosition, offset);
        }
    }
}