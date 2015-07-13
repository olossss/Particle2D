using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Particle2D.Modifiers.Alpha;
using Particle2D.Modifiers.Color;
using Particle2D.Modifiers.Rotation;
using Particle2D.Patterns;

namespace Particle2D
{
    public class SampleGame : Game
    {
        private readonly GraphicsDeviceManager graphics;
        private ParticleEffect2D particleEffect;
        private SpriteBatch spriteBatch;

        public SampleGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            particleEffect = ParticleEffect2DFactory.Initialize(1000, 2000)
                .SetEmissionPattern(new RectangleEmissionPattern(300, 300))
                .SetMaxParticleSpeed(5f)
                .SetEmitAmount(75)
                .AddTexture(Content.Load<Texture2D>("star.png"))
                .AddModifier(new AlphaAgeTransform())
                .AddModifier(new ColorAgeTransform {EndColor = Color.AliceBlue, StartColor = Color.BlueViolet})
                .AddModifier(new ConstantRotation {Strength = 5})
                .Create();
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            var emitPosition = new Vector2(GraphicsDevice.Viewport.Width/2f, GraphicsDevice.Viewport.Height/2f);

            particleEffect.Emit((float) gameTime.TotalGameTime.TotalMilliseconds, emitPosition);
            particleEffect.Update((float) gameTime.TotalGameTime.TotalMilliseconds,
                (float) gameTime.ElapsedGameTime.TotalSeconds);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin(blendState: BlendState.Additive);
            particleEffect.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}