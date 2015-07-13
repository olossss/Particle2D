using Microsoft.Xna.Framework;

namespace Particle2D.Modifiers.Color
{
    /// <summary>
    ///     Modifier to change particle color based on particle age
    /// </summary>
    public class ColorAgeTransform : IModifier
    {
        /// <summary>
        ///     Gets or sets the starting color
        /// </summary>
        public Microsoft.Xna.Framework.Color StartColor { get; set; }

        /// <summary>
        ///     Gets or sets the end color
        /// </summary>
        public Microsoft.Xna.Framework.Color EndColor { get; set; }

        public void Update(float particleAge, double totalMilliseconds, double elapsedSeconds,
            Particle2D particle)
        {
            var r = (int) MathHelper.Lerp(StartColor.R, EndColor.R, particleAge);
            var g = (int) MathHelper.Lerp(StartColor.G, EndColor.G, particleAge);
            var b = (int) MathHelper.Lerp(StartColor.B, EndColor.B, particleAge);

            particle.Color = new Microsoft.Xna.Framework.Color((byte) r, (byte) g, (byte) b);
        }
    }
}