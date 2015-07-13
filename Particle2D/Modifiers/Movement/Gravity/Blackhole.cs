using Microsoft.Xna.Framework;

namespace Particle2D.Modifiers.Movement.Gravity
{
    /// <summary>
    ///     Modifier which will pull particles towards a gravitational point and kill them when within a specified radius
    /// </summary>
    public class Blackhole : IModifier
    {
        /// <summary>
        ///     Constructor
        /// </summary>
        public Blackhole()
        {
            RemovalRadius = 15f;
        }

        /// <summary>
        ///     Gets or sets the radius in which particles will be killed
        /// </summary>
        public float RemovalRadius { get; set; }

        /// <summary>
        ///     Gets or sets the GravityPoint for the black hole
        /// </summary>
        public GravityPoint GravityPoint { get; set; }

        public void Update(float particleAge, double totalMilliseconds, double elapsedSeconds,
            Particle2D particle)
        {
            GravityPoint.Update(particleAge, totalMilliseconds, elapsedSeconds, particle);

            var distance = Vector2.Subtract(GravityPoint.Position, particle.Position);

            if (distance.LengthSquared() < RemovalRadius*RemovalRadius)
                particle.IsAlive = false;
        }
    }
}