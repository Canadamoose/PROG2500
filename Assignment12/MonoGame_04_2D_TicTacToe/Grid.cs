using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoGame_04_2D_TicTacToe
{ 
        public class Grid
        {
            public enum gridVal { X = 0, O = 1, center = 2, dot = 3 };

            private gridVal[,] grid = new gridVal[3, 3];
            private bool[,] selection = new bool[3, 3];
            private int[] current = new int[2];
            private gridVal WhoWon;
            private gridVal[] turn = { gridVal.X, gridVal.O };

            private myModel[] m;

            public Grid(myModel[] models)
            {
                m = models;
                SetAllGridPos(gridVal.dot);
                SetAllNotSelected();
                current[0] = 0;
                current[1] = 0;
            }

            private void SetGridPos(gridVal v, int x, int y)
            {
                grid[x, y] = v;
            }

            private void SetAllGridPos(gridVal v)
            {
                for (int i = 0; i < 3; i++)
                    for (int j = 0; j < 3; j++)
                        SetGridPos(v, i, j);
            }

            private void SetAllNotSelected()
            {
                for (int i = 0; i < 3; i++)
                    for (int j = 0; j < 3; j++)
                        selection[i, j] = false;
            }

            public void Update(KeyboardState k, ref bool lastSpace, ref bool lastUp, ref bool lastRight)
            {
                if (k.IsKeyDown(Keys.Up) && lastUp)
                    current[1] = (current[1] + 1) % 3;

                if (k.IsKeyDown(Keys.Right) && lastRight)
                    current[0] = (current[0] + 1) % 3;

                if (k.IsKeyDown(Keys.Space) && lastSpace)
                {
                    Debug.WriteLine(" current[0]=" + current[0] + " current[1]=" + current[1]);
                    selection[current[0], current[1]] = !selection[current[0], current[1]];

                    int eVal = (int)grid[current[0], current[1]];
                    int max_eVal = Enum.GetNames(typeof(gridVal)).Length;
                    eVal = (eVal + 1) % max_eVal;
                    grid[current[0], current[1]] = (gridVal)eVal;
                }

                lastUp = !k.IsKeyDown(Keys.Up);
                lastRight = !k.IsKeyDown(Keys.Right);
                lastSpace = !k.IsKeyDown(Keys.Space);

                WhoWon = GetWhoWon();
            }

            private gridVal GetWhoWon()
            {
                foreach (var player in turn)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        if ((grid[i, 0] == player) && (grid[i, 1] == player) && (grid[i, 2] == player))
                            return player;

                        if ((grid[0, i] == player) && (grid[1, i] == player) && (grid[2, i] == player))
                            return player;
                    }

                    if ((grid[0, 0] == player) && (grid[1, 1] == player) && (grid[2, 2] == player))
                        return player;

                    if ((grid[2, 0] == player) && (grid[1, 1] == player) && (grid[0, 2] == player))
                        return player;
                }

                return gridVal.dot;
            }

            public void Draw(Vector3 cameraPos, float aspectRatio, Vector3 target, Vector3 up, GraphicsDevice device)
            {
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        bool won = ((grid[i, j] != gridVal.dot) && (WhoWon == grid[i, j]));

                        Vector3 location = new Vector3(i * 100 - 50.0f, j * 100 - 50.0f, 0f);

                        myModel here = m[(int)grid[i, j]];
                        here.pos = location;
                        here.color = Color.PaleGreen;

                        if (grid[i, j] == gridVal.X) here.color = Color.BlueViolet;
                        if (grid[i, j] == gridVal.O) here.color = Color.DarkOrange;
                        if ((i == current[0]) && (j == current[1])) here.color = Color.Red;

                        here.draw(cameraPos, aspectRatio, target, up, won);
                    }
                }
            }
        }
    }
