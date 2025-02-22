using System;
using System.Collections.Generic;
using System.Text;

namespace Hello_Console
{
    class Square : ShapesClass
    {
                // the size of the square
        private int h, w;  

        public int H
        {
            get
            {
                return h;
            }

            set
            {
                h = value;
            }
        }

        public int W
        {
            get
            {
                return w;
            }

            set
            {
                w = value;
            }
        }
        // constructor
        public Square(int h, int w)
        {
            this.H = h;
            this.W = w;
        }


        // Because ShapesClass.Area is abstract, failing to override
        // the Area method would result in a compilation error.
        public override int Area()
        {
            return H * W;
        }
    }
}
