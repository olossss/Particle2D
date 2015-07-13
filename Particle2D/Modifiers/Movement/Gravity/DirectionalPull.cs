using Microsoft.Xna.Framework;

namespace Particle2D.Modifiers.Movement.Gravity
{
    /// <summary>
    ///     Modifier to gradually pull particles in a particular direction
    /// </summary>
    public class DirectionalPull : IModifier
    {
        /// <summary>
        ///     Gets or sets the gravitational pull on particles
        /// </summary>
        public Vector2 Gravity { get; set; }

        public void Update(float particleAge, double totalMilliseconds, double elapsedSeconds,
            Particle2D particle)
        {
            var deltaGrav = Vector2.Multiply(Gravity, (float) elapsedSeconds);
            particle.Affect(ref deltaGrav);
        }
    }
}