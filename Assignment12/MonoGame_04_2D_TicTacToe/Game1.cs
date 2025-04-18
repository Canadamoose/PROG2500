using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using System;

namespace MonoGame_04_2D_TicTacToe;


/**
* 3D: Place an Image on the Screen using XNA:
* ===========================================
* This sample:
*      - Uses MonoGame programming...used to be xBox/XNA
*      - Shows how 3D mesh shapes are placed on the screen
*      - Too functional, should use more OOP
*      - myModel class isn't bad, but grid should be a class with it's own draw()
*      - note enumerations used to show meaning in variables
*      
*      
**/

public class Game1 : Game
{
    public static GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private Grid grid;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here

        base.Initialize();
    }


    // Set the position of the camera in world space, for our view matrix.
    Vector3 cameraPosition = new Vector3(0.0f, 350.0f, 350.0f);


    // List of models to draw
    System.Collections.ArrayList Model_list = new System.Collections.ArrayList();


    // Used to detect the B button being pressed
    ButtonState lastUpdateState;
    ButtonState thisUpdateState;

    //// The aspect ratio determines how to scale 3d to 2d projection.
    //float aspectRatio;

    //// Enumeration defines the type of meshes in the grid spots
    //private enum gridVal { X = 0, O = 1, center = 2, dot = 3 };

    //// list of player turns, first X, then O
    //// currentTurn indexes into turn So, the current 
    //// turn is turn[currentTurn], then inc and mod currentTurn
    //private gridVal[] turn = { gridVal.X, gridVal.O };

    //// Keep track of X's and O's, fill this matrix with gridVal
    //private gridVal[,] grid = new gridVal[3, 3];

    //// track who won (dot means no winner)
    //private gridVal WhoWon;


    // each and any spot my be selected
    private Boolean[,] selection = new Boolean[3, 3];

    // current "cursor" in 2D
    private int[] current = new int[2];
    private float aspectRatio;
    Boolean lastTimeWasNotSpace = false;
    Boolean lastTimeWasNotUp = false;
    Boolean lastTimeWasNotRight = false;

    /// <summary>
    /// Set the x,y,z grid postion to this value v.
    ///// </summary>
    //private void setGridPos(gridVal v, int x, int y)
    //{
    //    // The position in grid is same as display position 
    //    grid[x, y] = v;
    //}


    /// <summary>
    /// Set all grid postions to this value, and the center to the
    /// center value.
    /// </summary>
    //private void setAllGridPos(gridVal v)
    //{

    //    // Set each mesh spot to that given as input
    //    for (int i = 0; i < 3; i++)
    //    {
    //        for (int j = 0; j < 3; j++)
    //        {
    //            setGridPos(v, i, j);
    //        }
    //    }

    //}

    //private void setAllNotSelected()
    //{

    //    // Set each mesh spot to that given as input
    //    for (int i = 0; i < 3; i++)
    //    {
    //        for (int j = 0; j < 3; j++)
    //        {
    //            selection[i, j] = false;
    //        }
    //    }

    //    //// Make the center position look different, not a fair spot
    //    //setGridPos(gridVal.center, 1, 1, 1);
    //}

    private myModel[] m = new myModel[4];


    protected override void LoadContent()
    {
        Debug.WriteLine("BasicCameraSample LoadContent");

        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // Flyweight pattern: create one of each mesh, and reuse
        // these 4 meshes in the grid.  
        // load each mesh one at a time, 4 types of mesh objects
        // All mesh objects displayed, will render from one of these 4
        myModel.setupGraphics(_graphics);
        m[(int)Grid.gridVal.center] = new myModel(Content.Load<Model>("Models\\Z"), Vector3.Zero, Vector3.Zero, Color.Yellow);
        m[(int)Grid.gridVal.X] = new myModel(Content.Load<Model>("Models\\X"), Vector3.Zero, Vector3.Zero, Color.Yellow);
        m[(int)Grid.gridVal.O] = new myModel(Content.Load<Model>("Models\\O"), Vector3.Zero, Vector3.Zero, Color.Yellow);
        m[(int)Grid.gridVal.dot] = new myModel(Content.Load<Model>("Models\\dot"), Vector3.Zero, Vector3.Zero, Color.Yellow);

        //// initialize each grid position to a dot
        //this.setAllGridPos(gridVal.dot);
        //setAllNotSelected();
        //this.current = [0, 0];  // default current cursor

        grid = new Grid(m);

        aspectRatio = (float)_graphics.GraphicsDevice.Viewport.Width /
                        (float)_graphics.GraphicsDevice.Viewport.Height;
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        KeyboardState k = Keyboard.GetState();

        if (k.IsKeyDown(Keys.E))
            Exit();

        if (k.IsKeyDown(Keys.D1))
            cameraPosition = new Vector3(0.0f, 350.0f, 350.0f);
        if (k.IsKeyDown(Keys.D2))
            cameraPosition = new Vector3(50.0f, 450.0f, 450.0f);
        if (k.IsKeyDown(Keys.D3))
            cameraPosition = new Vector3(100.0f, 250.0f, 250.0f);
        if (k.IsKeyDown(Keys.D4))
            cameraPosition = new Vector3(0.0f, -350.0f, 250.0f);

        grid.Update(k, ref lastTimeWasNotSpace, ref lastTimeWasNotUp, ref lastTimeWasNotRight);

        base.Update(gameTime);
    }

    Vector3 cameraTarget = Vector3.Zero;
    Vector3 cameraUpDirection = Vector3.Up;

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        grid.Draw(cameraPosition, aspectRatio, cameraTarget, cameraUpDirection, GraphicsDevice);

        base.Draw(gameTime);
    }
}

