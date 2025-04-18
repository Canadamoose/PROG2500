using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Project1
{
    public class StationaryObject : MyModel
    {
        public StationaryObject(Model model) : base(model) { }
        public StationaryObject(Model model, Color color) : base(model, color) { }
        public StationaryObject(Model model, Vector3 position, Vector3 rotation, Color color)
            : base(model, position, rotation, color) { }

        public override void Move()
        {
            // Stationary objects don't move
        }
    }
}
