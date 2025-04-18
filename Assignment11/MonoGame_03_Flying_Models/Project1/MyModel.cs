using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Project1
{
    public abstract class MyModel
    {
        // Contain the mesh, position and rotation for this object
        public Model model;
        public Vector3 position;
        public Vector3 rotation;
        public Color color;
        protected Random random = new Random();


        // Constructor, set up the shape
        protected MyModel(Model model, Vector3 position, Vector3 rotation, Color color)
        {
            this.model = model;
            this.position = position;
            this.rotation = rotation;
            this.color = color;
        }

        protected MyModel(Model model)
        {
            this.model = model;
        }

        public abstract void Move();

        // Constructor, set up the shape
        //protected MyModel(Model m, Vector3 pos, Vector3 vel, Vector3 rot, Color color)
        //{
        //    this.model = m;
        //    this.position = pos;
        //    this.rotation = rot;
        //    this.color = color;
        //}

        //Constructor, set up the shape
        protected MyModel(Model m, Color color)
        {
            this.model = m;
            position = Vector3.Zero;
            rotation =Vector3.Zero;
            this.color = color;
        }

        public void setColor(Color newColor)
        {
            this.color = newColor;
        }

        //public void SetRND_Velocity()
        //{
        //    // make a ranom vector at set it to the model velocity
        //    Vector3 rnd = new Vector3();
        //    rnd.X = (float)random.NextDouble() * 2;
        //    rnd.Y = (float)random.NextDouble() * 2;
        //    rnd.Z = (float)random.NextDouble() * 2;
        //    vel = rnd;

        //    // make a ranom vector at set it to the model velocity
        //    rnd = new Vector3();
        //    rnd.X = (float)random.NextDouble() / 10;
        //    rnd.Y = (float)random.NextDouble() / 10;
        //    rnd.Z = (float)random.NextDouble() / 10;
        //    rot_vel = rnd;

        //}

        public static Texture2D CreateTexture(int width, int height, Func<int, Color> paint)
        {
            //initialize a texture
            Texture2D texture = new Texture2D(Game1._graphics.GraphicsDevice, width, height);

            //the array holds the color for each pixel in the texture
            Color[] data = new Color[width * height];
            for (int pixel = 0; pixel < data.Count(); pixel++)
            {
                //the function applies the color according to the specified pixel
                data[pixel] = paint(pixel);
            }

            //set the color
            texture.SetData(data);

            return texture;
        }


        public void draw(Vector3 cameraPosition, float aspectRatio, Vector3 cameraTarget, Vector3 cameraUpVector)
        {

            // Settings of each iteration through the models
            Model Models = model;
            Vector3 modelPosition = position;
            Vector3 modelRotation = rotation;

            Texture2D x = model.Meshes[0].MeshParts[0].Effect.Parameters["Texture"].GetValueTexture2D();

            // Copy any parent transforms.
            Matrix[] transforms = new Matrix[Models.Bones.Count];
            Models.CopyAbsoluteBoneTransformsTo(transforms);

            // Draw the model. A model can have multiple meshes, so loop.
            foreach (ModelMesh mesh in Models.Meshes)
            {
                // This is where the mesh orientation is set, as well as our camera and projection.
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.EnableDefaultLighting();
                    effect.World = transforms[mesh.ParentBone.Index]
                        * Matrix.CreateRotationY(modelRotation.Y)
                        * Matrix.CreateRotationX(modelRotation.X)
                        * Matrix.CreateTranslation(modelPosition);
                    effect.View = Matrix.CreateLookAt(cameraPosition, cameraTarget, cameraUpVector);
                    effect.Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45.0f),
                        aspectRatio, 1.0f, 10000.0f);

                    // need texture enabled to change color
                    effect.TextureEnabled = true; // sorta blue-ish
                    effect.Texture = CreateTexture(64, 64, pixel => color);

                }
                // Draw the mesh, using the effects set above.
                mesh.Draw();
            }

        }



    }
}
