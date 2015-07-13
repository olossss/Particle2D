using System;
using Microsoft.Xna.Framework;

namespace Particle2D.Patterns
{
    public interface IEmissionPattern
    {
        Vector2 CalculateParticlePosition(Random random, Vector2 emitPosition);
    }
}