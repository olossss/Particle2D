using Microsoft.Xna.Framework;

namespace Particle2D.Modifiers.Scale
{
    /// <summary>
    ///     Modifier to scale particles repeatedly between two sizes over time
    /// </summary>
    public class ScaleCycleTransform : IModifier
    {
        private Vector2 currentScale;
        private float cycleInception;

        /// <summary>
        ///     Gets or sets the length of each cycle
        /// </summary>
        public float CycleLength { get; set; }

        /// <summary>
        ///     Gets or sets the minimum scale
        /// </summary>
        public Vector2 MinimumScale { get; set; }

        /// <summary>
        ///     Gets or sets the maximum scale
        /// </summary>
        public Vector2 MaximumScale { get; set; }

        /// <summary>
        ///     Gets or sets whether the current cycle should scale up
        /// </summary>
        public bool ScaleUp { get; set; }

        public void Update(float particleAge, double totalMilliseconds, double elapsedSeconds,
            Particle2D particle)
        {
            if (cycleInception == 0f)
                cycleInception = (float) totalMilliseconds;

            var age = (float) (totalMilliseconds - cycleInception);

            if (age < particle.Lifespan)
            {
                if (age >= CycleLength)
                {
                    ScaleUp = !ScaleUp;
                    cycleInception = (float) totalMilliseconds;
                }

                var cycleStage = age/CycleLength;

                if (!ScaleUp)
                {
                    currentScale.X = MathHelper.Lerp(MaximumScale.X, MinimumScale.X, cycleStage);
                    currentScale.Y = MathHelper.Lerp(MaximumScale.Y, MinimumScale.Y, cycleStage);
                }
                else
                {
                    currentScale.X = MathHelper.Lerp(MinimumScale.X, MaximumScale.X, cycleStage);
                    currentScale.Y = MathHelper.Lerp(MinimumScale.Y, MaximumScale.Y, cycleStage);
                }
            }

            particle.Scale = currentScale;
        }
    }
}