using CommonTicTacToe;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _2D_TicTacToe
{
    public class Game1 : BaseGame
    {
        public Game1() : base(1) { }

        protected override void UpdateCursor()
        {
          
            var newCursor = Grid.Cursor + new Vector3(Input.MovementInput.X, Input.MovementInput.Y, 0);

            newCursor.X = (newCursor.X % 3 + 3) % 3;
            newCursor.Y = (newCursor.Y % 3 + 3) % 3;

            Grid.Cursor = newCursor;
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
                    var pos = new Vector3(
                        x * CELL_SPACING - CELL_SPACING,
                        y * CELL_SPACING - CELL_SPACING,
                        0
                    );

                    Models[(int)Grid.Cells[x, y, 0]].Draw(
                        Matrix.CreateTranslation(pos),
                        View,
                        Projection,
                        MODEL_SCALE
                    );
                }
            }

            base.Draw(gameTime);
        }
    }
}