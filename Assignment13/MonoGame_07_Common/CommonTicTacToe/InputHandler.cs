using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace CommonTicTacToe
{
    public class InputHandler
    {
        private KeyboardState _lastState;
        private KeyboardState _currentState;

        public Vector3 MovementInput { get; private set; }
        public bool SelectPressed { get; private set; }

        public void Update()
        {
            _lastState = _currentState;
            _currentState = Keyboard.GetState();

           
            MovementInput = new Vector3(
                IsKeyPressed(Keys.Right) ? 1 : IsKeyPressed(Keys.Left) ? -1 : 0,
                IsKeyPressed(Keys.Down) ? -1 : IsKeyPressed(Keys.Up) ? 1 : 0,
                IsKeyPressed(Keys.PageUp) ? 1 : IsKeyPressed(Keys.PageDown) ? -1 : 0
            );

            SelectPressed = IsKeyPressed(Keys.Space);
        }

        public bool IsKeyPressed(Keys key)
        {
            return _currentState.IsKeyDown(key) && _lastState.IsKeyUp(key);
        }

        public bool IsKeyDown(Keys key)
        {
            return _currentState.IsKeyDown(key);
        }
    }
}