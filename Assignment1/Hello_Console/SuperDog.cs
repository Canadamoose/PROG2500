using System;
using System.Collections.Generic;
using System.Text;

namespace Hello_Console
{
    public class SuperDog : Animal
    {
        private const int DefaultSpeed = 10;

        public SuperDog()
        {
            Dx = DefaultSpeed;
            Dy = DefaultSpeed;
        }

        public override void Voice()
        {
            Console.WriteLine("I'm Krypto!");
        }

        public override void Move(int dx, int dy)
        {
            X += dx;
            Y += dy;
        }

        public void Move()
        {
            Move(DefaultSpeed, DefaultSpeed);
        }
    }
}

