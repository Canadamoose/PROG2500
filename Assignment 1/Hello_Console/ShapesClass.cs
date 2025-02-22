using System;
using System.Collections.Generic;
using System.Text;

namespace Hello_Console
{
    abstract class ShapesClass
    {
        // color is treated the same for all shapes
        public enum mycolors { red, blue, orange };
        public mycolors c;

        // Location of shape as x,y coordinates
        private int x = 100;
        private int y = 200;

        public int X
        {
            get
            {
                return x;
            }

            set
            {
                x = value;
            }
        }

        public int Y
        {
            get
            {
                return y;
            }

            set
            {
                y = value;
            }
        }

        // Use F11 in debug...when is this constructor run?
        public ShapesClass()
        {
            this.c = mycolors.blue;
        }

        // Methods to set and get the location
        public void moveShape(int x, int y) 
        {
            this.X = x;
            this.Y = y;
        }

        // get the x,y values
        public int getX() { return this.X; }
        public int getY() { return this.Y; }

        // area is computed differently with different shapes
        abstract public int Area();
    }
}
