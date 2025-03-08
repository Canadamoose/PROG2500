using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace M01_First_WPF_Proj
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// 3 slashes creates the comment header
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
        }


        /// <summary>
        /// Draw a line on the canvas, using the supplied points.
        /// </summary>
        /// <param name="xLoc1">X1</param>
        /// <param name="yLoc1">Y1</param>
        /// <param name="xLoc2">X2</param>
        /// <param name="yLoc2">Y2</param>
        public void MyLineMethod(int xLoc1, int yLoc1, int xLoc2, int yLoc2)
        {
            // Add line to Grid
            Line myLine = new Line();
            myLine.Stroke = System.Windows.Media.Brushes.LightSteelBlue;
            myLine.X1 = xLoc1;
            myLine.Y1 = yLoc1;
            myLine.X2 = xLoc2 + 50;
            myLine.Y2 = yLoc2 + 50;
            myLine.HorizontalAlignment = HorizontalAlignment.Left;
            myLine.VerticalAlignment = VerticalAlignment.Center;
            myLine.StrokeThickness = 2;
            myCanvas.Children.Add(myLine);
            //myGrid.Children.Add(myLine);

        }

        /// <summary>
        /// Random Number generator for line positions.
        /// </summary>
        Random random = new Random();

        /// <summary>
        /// Event Handler for button.  Not renamed, so lots of work to 
        /// name it properly now (but not impossible).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Click(object sender, RoutedEventArgs e)
        {
            int rx1 = random.Next(10, 200);
            int ry1 = random.Next(10, 200);
            int rx2 = random.Next(10, 200);
            int ry2 = random.Next(10, 200);
            MyLineMethod(rx1, ry1, rx2, ry2);
        }

        /// <summary>
        /// Just drag a checkbox onto the grid and double-click it to
        /// create this event handler.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBox_Checked(object sender, RoutedEventArgs e)
        {
            int rx1 = random.Next(10, 200);
            int ry1 = random.Next(10, 200);
            int rx2 = random.Next(10, 200);
            int ry2 = random.Next(10, 200);
            MyLineMethod(rx1, ry1, rx2, ry2);
        }




        /// <summary>
        /// Load the face image file from local directory.  
        /// 
        /// To make a transparent GIF, use Gimp2 and delete
        /// background layer using fuzzy select->delete.
        /// 
        /// Images (images dir) needs to be in the "M01_First_WPF_Proj\bin\Debug" directory
        /// </summary>
        BitmapImage BMimg = new BitmapImage(new Uri("../../images/layerA.gif", UriKind.Relative));


        /// <summary>
        /// Draw an image on thre canvas at the supplied x,y location.
        /// </summary>
        /// <param name="xLoc1"></param>
        /// <param name="yLoc1"></param>
        public void MyImageMethod(int xLoc1, int yLoc1)
        {
            // may need to copy /images/*.png into /Debug folder
            // Need to create a new image every time we place a copy on the canvas
            Image img = new Image();

            // Minimum image attributes
            img.Source = BMimg;
            img.Width = BMimg.Width;  // exception if images not copied down
            img.Height = BMimg.Height;

            // Attach to canvas in position
            Canvas.SetLeft(img, xLoc1);
            Canvas.SetTop(img, yLoc1);
            myCanvas.Children.Add(img);

        }

        /// <summary>
        /// Button to add the image, which happens to be a face
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Add_Face_Click(object sender, RoutedEventArgs e)
        {
            int rx1 = random.Next(10, 200);
            int ry1 = random.Next(10, 200);
            MyImageMethod(rx1, ry1);

        }

        private void buttonTest_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Button was pressed...");
        }

        private void comboTest_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //MessageBox.Show(e.ToString());
            //MessageBox.Show(comboTest.SelectedItem.ToString());
            Console.WriteLine("Combo=" + comboTest.SelectedItem.ToString());
        }

        private void sliderTest_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            //MessageBox.Show(e.ToString());
            //MessageBox.Show(sliderTest.Value.ToString());
            Console.WriteLine("Slider=" + sliderTest.Value.ToString());
            Console.WriteLine("Slider e=" + e.ToString());

        }

        private void combo_02_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
