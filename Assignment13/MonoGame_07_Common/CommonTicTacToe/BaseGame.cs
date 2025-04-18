using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CommonTicTacToe
{
    public abstract class BaseGame : Game
    {
        protected GraphicsDeviceManager Graphics;
        protected SpriteBatch SpriteBatch;
        protected BaseGrid Grid;
        protected InputHandler Input = new InputHandler();
        protected MyModel[] Models;
        protected GridVal CurrentPlayer = GridVal.X;

        protected Matrix View, Projection;
        protected Vector3 CameraPos = new(0, 150, 150);

        protected BaseGame(int dimensions)
        {
            Graphics = new GraphicsDeviceManager(this);

            Graphics.PreferredBackBufferWidth = 1280;
            Graphics.PreferredBackBufferHeight = 720;   
            Graphics.ApplyChanges();

            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            Grid = new BaseGrid(3, 3, dimensions);
        }

        protected override void LoadContent()
        {
            SpriteBatch = new SpriteBatch(GraphicsDevice);
            Models = MyModel.Load(Content);
        }



        protected override void Update(GameTime gameTime)
        {
            Input.Update();
            if (Input.IsKeyDown(Keys.Escape)) Exit();

            UpdateCursor();
            HandleSelection();
            HandleCamera();

            base.Update(gameTime);
        }
        protected virtual void UpdateCursor()
        {
          
            var newCursor = Grid.Cursor + Input.MovementInput;


            newCursor.X = (newCursor.X % Grid.Dimensions.X + Grid.Dimensions.X) % Grid.Dimensions.X;
            newCursor.Y = (newCursor.Y % Grid.Dimensions.Y + Grid.Dimensions.Y) % Grid.Dimensions.Y;
            newCursor.Z = (newCursor.Z % Grid.Dimensions.Z + Grid.Dimensions.Z) % Grid.Dimensions.Z;

            Grid.Cursor = newCursor;
        }
        protected void HandleSelection()
        {
            if (Input.SelectPressed)
            {
                Grid.CycleCell(CurrentPlayer);
                CurrentPlayer = CurrentPlayer == GridVal.X ? GridVal.O : GridVal.X;
            }
        }

        protected void HandleCamera()
        {
           
            if (Input.IsKeyPressed(Keys.D1))
                CameraPos = new Vector3(0, 350, 350);
            if (Input.IsKeyPressed(Keys.D2))
                CameraPos = new Vector3(50, 450, 450);

            View = Matrix.CreateLookAt(CameraPos, Vector3.Zero, Vector3.Up);
            Projection = Matrix.CreatePerspectiveFieldOfView(
                MathHelper.ToRadians(45f), 
                GraphicsDevice.Viewport.AspectRatio,
                1f,  
                10000f 
            );
        }
    }
}