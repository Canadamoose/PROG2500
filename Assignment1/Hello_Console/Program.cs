using System;
using System.Collections.Generic;
using System.Text;

namespace Hello_Console
{
    class Program
    {
        /*
* Run this code in the debugger and look at where it
* goes step-by-step, and what the internal data structures
* look like after each step.
* 
* Remember...console writes go to the DOS prompt.
* 
*/
        static void Main(string[] args)
        {
            // make some squares and circles


            Square s1 = new Square(10, 20);
            s1.moveShape(300,400);

            Circle s2 = new Circle(20);
            s2.moveShape(800,800);

            // get the area of the shapes, and location 
            Console.WriteLine("Area Square s1 =" + s1.Area() + "  X=" + s1.getX() + "  Y=" + s1.getY());
            Console.WriteLine("Area Circle s2 =" + s2.Area() + "  X=" + s2.getX() + "  Y=" + s2.getY());

            Console.WriteLine("==========================================");
            Console.WriteLine("Waiting for you to press enter");
            Console.ReadLine();

        }
    }
}
