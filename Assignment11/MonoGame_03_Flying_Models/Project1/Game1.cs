using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;


//using Microsoft.Xna.Framework.Content.Pipeline.Graphics;
using System.Diagnostics;

namespace Project1
{

    /**
* 3D: Place an Image on the Screen using XNA:
* ===========================================
* This sample:
*      - Uses XNA programming...also can run on an xBox
*      - Shows how 3D mesh shapes are placed on the screen
*      - Shows how the shape can be rotated on the screen
*      
* Try This:
*      1) Run the program to see what it does (Debug->Start New Instance)
*      2) Change the mesh from X to O (change "Models\\X")
*      3) Change the speed of rotation (---.rot.X += 0.01f;)
*      4) Change the keys used to move the shape(Keys.Left)
*      5) Change the colour of the background (---.Clear(Color.Orange))
* 
**/

    public class Game1 : Game
    {
        public static GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            //set the GraphicsDeviceManager's fullscreen property
           // _graphics.IsFullScreen = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();

        }



        // List of models to draw
        System.Collections.ArrayList Model_list = new System.Collections.ArrayList();

        // The current model to change, of the List
        int current = 0;

        // Used to detect the B button being pressed
        ButtonState lastUpdateState;
        ButtonState thisUpdateState;

        // The aspect ratio determines how to scale 3d to 2d projection.
        float aspectRatio;


        protected override void LoadContent()
        {
            Debug.WriteLine("BasicCameraSample LoadContent");

            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            // Read mesh from file, set into arraylist
            //myModel O1 = new myModel(Content.Load<Model>("Models\\X"), Vector3.Zero, Vector3.Zero);
            //myModel O1 = new myModel(Content.Load<Model>("Models/thing"), Vector3.Zero, Vector3.Zero);
            //myModel O1 = new myModel(Content.Load<Model>("Models/A"), Vector3.Zero, Vector3.Zero);
            //Model_list.Add(O1);

            Model_list.Add(new MovingObject(Content.Load<Model>("Models/R"), Color.Red));
            Model_list.Add(new MovingObject(Content.Load<Model>("Models/U"), Color.Pink));
            Model_list.Add(new MovingObject(Content.Load<Model>("Models/S"), Color.Orange));
            Model_list.Add(new MovingObject(Content.Load<Model>("Models/S"), Color.Black));
            Model_list.Add(new MovingObject(Content.Load<Model>("Models/Sphere")));

            foreach (MovingObject model in Model_list)
            {
                model.SetRandomVelocity();
            }

            aspectRatio = (float)_graphics.GraphicsDevice.Viewport.Width /
                            (float)_graphics.GraphicsDevice.Viewport.Height;
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            //Get Keyboard Keystroke
            //KeyboardState k = Keyboard.GetState(PlayerIndex.One);  // Depricated

            KeyboardState k = Keyboard.GetState();


            if (k.IsKeyDown(Keys.E))
                this.Exit();                                      // E = Exit Game

            if (k.IsKeyDown(Keys.Up))
                ((MovingObject)Model_list[current]).rotation.X += 0.01f;    // Rotate Up

            if (k.IsKeyDown(Keys.Down))
                ((MovingObject)Model_list[current]).rotation.X -= 0.01f;    // Rotate Down

            if (k.IsKeyDown(Keys.PageUp))
                ((MovingObject)Model_list[current]).position.X += 5.0f;    // Rotate Up

            if (k.IsKeyDown(Keys.PageDown))
                ((MovingObject)Model_list[current]).position.X -= 5.0f;    // Rotate Down



            if (k.IsKeyDown(Keys.Left))
                ((MovingObject)Model_list[current]).rotation.Y += 0.1f;    // Rotate Left

            if (k.IsKeyDown(Keys.Right))
                ((MovingObject)Model_list[current]).rotation.Y -= 0.1f;    // Rotate Right

            if (k.IsKeyDown(Keys.Space))
                current = (current + 1) % Model_list.Count;      // Spacebar...next

            /////////////


            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // Cycle through array of meshes.  B button went from not pressed to pressed
            thisUpdateState = GamePad.GetState(PlayerIndex.One).Buttons.B;
            if ((thisUpdateState == ButtonState.Pressed) && (lastUpdateState == ButtonState.Released))
                current = (current + 1) % Model_list.Count;

            // Get rotation from thumbstick
            float yRotation = GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X;
            float xRotation = GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y;

            // set Rotation
            ((MovingObject)Model_list[current]).rotation.X += (float)gameTime.ElapsedGameTime.TotalMilliseconds * MathHelper.ToRadians(xRotation);
            ((MovingObject)Model_list[current]).rotation.Y += (float)gameTime.ElapsedGameTime.TotalMilliseconds * MathHelper.ToRadians(yRotation);

            // Get position from thumbstick
            float yPosition = GamePad.GetState(PlayerIndex.One).ThumbSticks.Right.X;
            float xPosition = GamePad.GetState(PlayerIndex.One).ThumbSticks.Right.Y;

            // set Position
            ((MovingObject)Model_list[current]).position.X += (float)gameTime.ElapsedGameTime.TotalMilliseconds * MathHelper.ToRadians(yPosition);
            ((MovingObject)Model_list[current]).position.Y += (float)gameTime.ElapsedGameTime.TotalMilliseconds * MathHelper.ToRadians(xPosition);

            // this is next time's last time...er...you know what I mean.
            lastUpdateState = thisUpdateState;

            ////////////////

            // update movement of models
            foreach (MyModel model in Model_list)
            {
                model.Move();
            }

            ////////////////

            base.Update(gameTime);
        }

        // Set the position of the camera in world space, for our view matrix.
        Vector3 cameraPosition = new Vector3(0.0f, 350.0f, 350.0f);
        Vector3 cameraTarget =  Vector3.Zero;
        Vector3 cameraUpDirection = Vector3.Up;


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);


            foreach (MyModel model in Model_list) {
                model.draw(cameraPosition, aspectRatio, cameraTarget, cameraUpDirection);
            }

            base.Draw(gameTime);
        }
    }
}
