using System;
using System.Collections.Generic;
using System.Text;

namespace Hello_Console
{
    class Circle : ShapesClass
    {

        // constructor
        public Circle(int r)
        {
            this.R = r;
        }

        public int R { get; set; }

        public override int Area()
        {
            // use float inside paren, but convert to int afterwards
            return (int)(3.1415 * (float)R * (float)R);  // area of circle
        }
    }

}
