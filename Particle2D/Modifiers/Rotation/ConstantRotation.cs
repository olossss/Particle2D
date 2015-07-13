namespace Particle2D.Modifiers.Rotation
{
    internal class ConstantRotation : IModifier
    {
        public double Strength { get; set; }

        public void Update(float particleAge, double totalMilliseconds, double elapsedSeconds, Particle2D particle)
        {
            var amount = Strength*(totalMilliseconds/1000);
            particle.Rotation = (float) amount;
        }
    }
}