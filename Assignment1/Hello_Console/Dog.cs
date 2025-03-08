using System;
using System.Collections.Generic;
using System.Text;

namespace Hello_Console
{
    class Dog : Animal
    {
        private const int DefaultSpeed = 2;
        private const int MaxSpeed = 4;

        public Dog()
        {
            Dx = DefaultSpeed;
            Dy = DefaultSpeed;
        }

        public override void Voice()
        {
            Console.WriteLine("Bark!");
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