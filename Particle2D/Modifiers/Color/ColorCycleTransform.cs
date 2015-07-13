using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Particle2D.Modifiers.Color
{
    /// <summary>
    ///     Modifier to repeatedly change particle colours over time
    /// </summary>
    public class ColorCycleTransform : IModifier
    {
        private int currentColorIndex;
        private float cycleInception;
        private int nextColorIndex = 1;

        /// <summary>
        ///     Gets or sets the length of each cycle
        /// </summary>
        public float CycleLength { get; set; }

        /// <summary>
        ///     Gets or sets the list of colors to cycle through
        /// </summary>
        public List<Microsoft.Xna.Framework.Color> ColorList { get; set; }

        public void Update(float particleAge, double totalMilliseconds, double elapsedSeconds,
            Particle2D particle)
        {
            if (ColorList.Count > 1)
            {
                if (cycleInception == 0f)
                    cycleInception = (float) totalMilliseconds;

                var age = (float) (totalMilliseconds - cycleInception);

                if (age >= CycleLength)
                {
                    SetColorIndices();
                    cycleInception = (float) totalMilliseconds;
                }

                var cycleAge = age/CycleLength;

                var r = (int) MathHelper.Lerp(ColorList[currentColorIndex].R, ColorList[nextColorIndex].R, cycleAge);
                var g = (int) MathHelper.Lerp(ColorList[currentColorIndex].G, ColorList[nextColorIndex].G, cycleAge);
                var b = (int) MathHelper.Lerp(ColorList[currentColorIndex].B, ColorList[nextColorIndex].B, cycleAge);

                particle.Color = new Microsoft.Xna.Framework.Color((byte) r, (byte) g, (byte) b);
            }
            else
                particle.Color = ColorList[0];
        }

        private void SetColorIndices()
        {
            currentColorIndex++;
            nextColorIndex++;

            if (nextColorIndex > ColorList.Count - 1)
                nextColorIndex = 0;

            if (currentColorIndex > ColorList.Count - 1)
                currentColorIndex = 0;
        }
    }
}