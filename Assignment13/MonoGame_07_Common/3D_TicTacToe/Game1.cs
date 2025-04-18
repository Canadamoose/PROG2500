using CommonTicTacToe;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _3D_TicTacToe
{
    public class Game1 : BaseGame
    {
        public Game1() : base(3)
        {
            Grid.Cells[1, 1, 1] = GridVal.Center; 
        }

        protected override void UpdateCursor()
        {
            Grid.Cursor += Input.MovementInput;
            Grid.Cursor = Vector3.Clamp(Grid.Cursor, Vector3.Zero, Grid.Dimensions - Vector3.One);
        }

        protected override void Draw(GameTime gameTime)
        {
            const float CELL_SPACING = 40f;
            const float MODEL_SCALE = 5.0f;

            GraphicsDevice.Clear(Color.CornflowerBlue);


            for (int x = 0; x < 3; x++)
            {
                for (int y = 0; y < 3; y++)
                {
                    for (int z = 0; z < 3; z++)
                    {
                        var pos = new Vector3(
                            x * CELL_SPACING - CELL_SPACING,
                            y * CELL_SPACING - CELL_SPACING,
                            z * CELL_SPACING - CELL_SPACING
                        );

                        Models[(int)Grid.Cells[x, y, z]].Draw(
                            Matrix.CreateTranslation(pos),
                            View,
                            Projection,
                            MODEL_SCALE
                        );
                    }
                }
            }

            base.Draw(gameTime);
        }
    }
}