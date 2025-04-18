using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Project1
{
    public class MovingObject : MyModel
    {
        private Vector3 velocity;
        private Vector3 rotationVelocity;

        public MovingObject(Model model) : base(model)
        {
            velocity = Vector3.Zero;
            rotationVelocity = Vector3.Zero;
        }

        public MovingObject(Model model, Vector3 position, Vector3 velocity, Vector3 rotation, Color color)
            : base(model, position, rotation, color)
        {
            this.velocity = velocity;
            this.rotationVelocity = Vector3.Zero;
        }

        public MovingObject(Model m, Color color) : base(m, color)
        {
        }

        public void SetRandomVelocity()
        {
            // make a ranom vector at set it to the model velocity
            Vector3 rnd = new Vector3();
            rnd.X = (float)random.NextDouble() * 2;
            rnd.Y = (float)random.NextDouble() * 2;
            rnd.Z = (float)random.NextDouble() * 2;
            velocity = rnd;

            // make a ranom vector at set it to the model velocity
            rnd = new Vector3();
            rnd.X = (float)random.NextDouble() / 10;
            rnd.Y = (float)random.NextDouble() / 10;
            rnd.Z = (float)random.NextDouble() / 10;
            rotationVelocity = rnd;

        }


        public override void Move()
        {
            // max on all sides
            float max = 200;

            // 3D movement
            position = position + velocity;

            if (Math.Abs(position.X) > max)
            {
                velocity.X = -velocity.X;
            }
            if (Math.Abs(position.Y) > max)
            {
                velocity.Y = -velocity.Y;
            }
            if (Math.Abs(position.Z) > max)
            {
                velocity.Z = -velocity.Z;
            }

            rotation = rotation + rotationVelocity;

        }


    }
}
