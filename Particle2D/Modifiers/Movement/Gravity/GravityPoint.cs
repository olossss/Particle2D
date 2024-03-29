using Microsoft.Xna.Framework;

namespace Particle2D.Modifiers.Movement.Gravity
{
    /// <summary>
    ///     Modifier which will pull particles towards a gravitational point
    /// </summary>
    public class GravityPoint : IModifier
    {
        /// <summary>
        ///     Gets or sets the center of the gravity point
        /// </summary>
        public Vector2 Position { get; set; }

        /// <summary>
        ///     Gets or sets the radius of influence
        /// </summary>
        public float Radius { get; set; }

        /// <summary>
        ///     Gets or sets the strength of the pull of gravity
        /// </summary>
        public float Strength { get; set; }

        public void Update(float particleAge, double totalMilliseconds, double elapsedSeconds,
            Particle2D particle)
        {
            var distance = Vector2.Subtract(Position, particle.Position);

            if (distance.LengthSquared() < Radius*Radius)
            {
                var force = Vector2.Normalize(distance);
                force = Vector2.Multiply(force, Strength);
                force = Vector2.Multiply(force, (float) elapsedSeconds);

                particle.Affect(ref force);
            }
        }
    }
}