using Microsoft.Xna.Framework;

namespace Particle2D.Modifiers.Alpha
{
    /// <summary>
    ///     Modifier to fade particles based on particle age
    /// </summary>
    public class AlphaAgeTransform : IModifier
    {
        /// <summary>
        ///     Gets or sets whether the modifier should fade in or out
        /// </summary>
        public bool FadeIn { get; set; }

        public void Update(float particleAge, double totalMilliseconds, double elapsedSeconds, Particle2D particle)
        {
            particle.Alpha = FadeIn ? MathHelper.Lerp(0f, 1f, particleAge) : MathHelper.Lerp(1f, 0f, particleAge);
        }
    }
}