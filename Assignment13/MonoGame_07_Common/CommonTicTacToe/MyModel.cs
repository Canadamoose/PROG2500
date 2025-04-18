using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace CommonTicTacToe
{
    public class MyModel
    {
        public static MyModel[] Load(ContentManager content)
        {
            return new MyModel[]
            {
                new(content.Load<Model>("Models/X"), Color.BlueViolet),
                new(content.Load<Model>("Models/O"), Color.DarkOrange),
                new(content.Load<Model>("Models/Z"), Color.Yellow),
                new(content.Load<Model>("Models/dot"), Color.PaleGreen)
            };
        }

        public Model Mesh { get; }
        public Color Color { get; private set; }

        public MyModel(Model mesh, Color color)
        {
            Mesh = mesh;
            Color = color;
        }

        public void Draw(Matrix world, Matrix view, Matrix projection, float scale = 1f)
        {
            if (Mesh?.Meshes == null) return;

            var scaledWorld = Matrix.CreateScale(scale) * world;

            foreach (var mesh in Mesh.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.World = scaledWorld;
                    effect.View = view;
                    effect.Projection = projection;
                    effect.DiffuseColor = Color.ToVector3();
                    effect.EnableDefaultLighting();
                }
                mesh.Draw();
            }
        }
    }
}