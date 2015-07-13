using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Particle2D.Utilities
{
    public static class Texture2DFactory
    {
        public static Texture2D New(GraphicsDevice graphicsDevice, int width, int height)
        {
            var texture = new Texture2D(graphicsDevice, width, height, false, SurfaceFormat.Color);

            var colorData = new Color[width*height];
            for (var i = 0; i < colorData.Length; i++)
            {
                colorData[i] = Color.White;
            }
            texture.SetData(colorData);

            return texture;
        }
    }
}