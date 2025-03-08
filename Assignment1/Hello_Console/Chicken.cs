using System;

namespace Hello_Console
{
    public class Chicken : Animal
    {
        private const int DefaultSpeed = 1;
        private const int MaxSpeed = 3;

        public Chicken()
        {
            Dx = DefaultSpeed;
            Dy = DefaultSpeed;
        }

        public override void Voice()
        {
            Console.WriteLine("Buk Buk!");
        }

        public override void Move(int dx, int dy)
        {
            X += Math.Clamp(dx, -MaxSpeed, MaxSpeed);
            Y += Math.Clamp(dy, -MaxSpeed, MaxSpeed);
        }

        public void Move()
        {
            Move(DefaultSpeed, DefaultSpeed);
        }
    }
}
