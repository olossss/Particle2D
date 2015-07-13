using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Particle2D
{
    public class Particle2D
    {
        private float alpha = 1f;
        private Color color = Color.White;
        private Vector2 origin;
        private Vector2 position = Vector2.Zero;
        private Vector2 projectedPosition = Vector2.Zero;
        private Vector2 scale = Vector2.One;
        private Vector2 velocity = Vector2.Zero;

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public Vector2 ProjectedPosition
        {
            get { return projectedPosition; }
        }

        public Vector2 Scale
        {
            get { return scale; }
            set { scale = value; }
        }

        public Vector2 Velocity
        {
            get { return velocity; }
            set { velocity = value; }
        }

        public float Rotation { get; set; }

        public Color Color
        {
            get { return color; }
            set { color = value; }
        }

        public float Alpha
        {
            get { return alpha; }
            set { alpha = value; }
        }

        public Texture2D Texture { get; set; }
        public float Lifespan { get; set; }
        public float InceptionTime { get; set; }
        public bool IsAlive { get; set; }

        public void Initialize(Texture2D particleTexture, Vector2 emitPosition, Vector2 initialVelocity,
            Color particleColor, float particleLifespan, float totalMilliseconds)
        {
            Texture = particleTexture;
            origin = new Vector2(particleTexture.Width/2f, particleTexture.Height/2f);

            IsAlive = true;
            alpha = 1f;
            InceptionTime = totalMilliseconds;
            position = emitPosition;
            color = particleColor;
            Lifespan = particleLifespan;
            velocity = initialVelocity;
        }

        internal void Update(double totalMilliseconds)
        {
            if (Lifespan < (totalMilliseconds - InceptionTime))
            {
                IsAlive = false;
            }
            else
            {
                position += velocity;
                projectedPosition = position + velocity;
            }
        }

        internal void Affect(ref Vector2 attraction)
        {
            velocity = Vector2.Add(velocity, attraction);
        }

        internal void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, position, null, color*alpha, Rotation, origin, scale, SpriteEffects.None, 0);
        }
    }
}