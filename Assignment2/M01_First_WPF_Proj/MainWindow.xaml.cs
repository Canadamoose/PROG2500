using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            LoadImages();
        }


        /// <summary>
        /// Draw a line on the canvas, using the supplied points.
        /// </summary>
        /// <param name="xLoc1">X1</param>
        /// <param name="yLoc1">Y1</param>
        /// <param name="xLoc2">X2</param>
        /// <param name="yLoc2">Y2</param>
        //public void MyLineMethod(int xLoc1, int yLoc1, int xLoc2, int yLoc2)
        //{
        //    // Add line to Grid
        //    Line myLine = new Line();
        //    myLine.Stroke = System.Windows.Media.Brushes.LightSteelBlue;
        //    myLine.X1 = xLoc1;
        //    myLine.Y1 = yLoc1;
        //    myLine.X2 = xLoc2 + 50;
        //    myLine.Y2 = yLoc2 + 50;
        //    myLine.HorizontalAlignment = HorizontalAlignment.Left;
        //    myLine.VerticalAlignment = VerticalAlignment.Center;
        //    myLine.StrokeThickness = 2;
        //    myCanvas.Children.Add(myLine);
        //    //myGrid.Children.Add(myLine);

        //}

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
        //private void button_Click(object sender, RoutedEventArgs e)
        //{
        //    int rx1 = random.Next(10, 200);
        //    int ry1 = random.Next(10, 200);
        //    int rx2 = random.Next(10, 200);
        //    int ry2 = random.Next(10, 200);
        //    MyLineMethod(rx1, ry1, rx2, ry2);
        //}

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
           // MyLineMethod(rx1, ry1, rx2, ry2);
        }


        BitmapImage[] hairImages = new BitmapImage[2];
        int hairIndex = 0;
        BitmapImage[] eyeImages = new BitmapImage[2];
        int eyeIndex = 0;
        BitmapImage[] noseImages = new BitmapImage[2];
        int noseIndex = 0;
        BitmapImage[] mouthImages = new BitmapImage[2];
        int mouthIndex = 0;

        /// <summary>
        /// Load the face image file from local directory.  
        /// 
        /// To make a transparent GIF, use Gimp2 and delete
        /// background layer using fuzzy select->delete.
        /// 
        /// Images (images dir) needs to be in the "M01_First_WPF_Proj\bin\Debug" directory
        /// </summary>

        /// <summary>
        /// Draw an image on thre canvas at the supplied x,y location.
        /// </summary>
        /// <param name="xLoc1"></param>
        /// <param name="yLoc1"></param>
        
        public void LoadImages()
        {
            hairImages[0] = new BitmapImage(new Uri("../../images/Hair_1.png", UriKind.Relative));
            hairImages[1] = new BitmapImage(new Uri("../../images/Hair_2.png", UriKind.Relative));
            eyeImages[0] = new BitmapImage(new Uri("../../images/Eye_1.png", UriKind.Relative));
            eyeImages[1] = new BitmapImage(new Uri("../../images/Eye_2.png", UriKind.Relative));
            noseImages[0] = new BitmapImage(new Uri("../../images/Nose_1.png", UriKind.Relative));
            noseImages[1] = new BitmapImage(new Uri("../../images/Nose_2.png", UriKind.Relative));
            mouthImages[0] = new BitmapImage(new Uri("../../images/Mouth_1.png", UriKind.Relative));
            mouthImages[1] = new BitmapImage(new Uri("../../images/Mouth_2.png", UriKind.Relative));
        }

        public void MyImageMethod()
        {
            hairImage.Source = hairImages[hairIndex];
            eyeImage.Source = eyeImages[eyeIndex];
            noseImage.Source = noseImages[noseIndex];
            mouthImage.Source = mouthImages[mouthIndex];
        }

        private void btnHairPrev_Click(object sender, RoutedEventArgs e)
        {
            hairIndex = (hairIndex - 1 + hairImages.Length) % hairImages.Length;
            MyImageMethod();
        }

        private void btnHairNext_Click(object sender, RoutedEventArgs e)
        {
            hairIndex = (hairIndex + 1) % hairImages.Length;
            MyImageMethod();
        }

        private void btnEyePrev_Click(object sender, RoutedEventArgs e)
        {
            eyeIndex = (eyeIndex - 1 + eyeImages.Length) % eyeImages.Length;
            MyImageMethod();
        }

        private void btnEyeNext_Click(object sender, RoutedEventArgs e)
        {
            eyeIndex = (eyeIndex + 1) % eyeImages.Length;
            MyImageMethod();
        }

        private void btnNosePrev_Click(object sender, RoutedEventArgs e)
        {
            noseIndex = (noseIndex - 1 + noseImages.Length) % noseImages.Length;
            MyImageMethod();
        }

        private void btnNoseNext_Click(object sender, RoutedEventArgs e)
        {
            noseIndex = (noseIndex + 1) % noseImages.Length;
            MyImageMethod();
        }

        private void btnMouthPrev_Click(object sender, RoutedEventArgs e)
        {
            mouthIndex = (mouthIndex - 1 + mouthImages.Length) % mouthImages.Length;
            MyImageMethod();
        }

        private void btnMouthNext_Click(object sender, RoutedEventArgs e)
        {
            mouthIndex = (mouthIndex + 1) % mouthImages.Length;
            MyImageMethod();
        }

        /// <summary>
        /// Button to add the image, which happens to be a face
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Add_Face_Click(object sender, RoutedEventArgs e)
        {
            hairIndex = random.Next(0, 2);
            eyeIndex = random.Next(0, 2);
            noseIndex = random.Next(0, 2);
            mouthIndex = random.Next(0, 2);
            MyImageMethod();
        }

        private void buttonTest_Click(object sender, RoutedEventArgs e)
        {
           Trace.WriteLine("Button was pressed...");
        }

        private void comboTest_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //MessageBox.Show(e.ToString());
            //MessageBox.Show(comboTest.SelectedItem.ToString());
            Trace.WriteLine("Combo=" + comboTest.SelectedItem.ToString());
        }

        private void sliderTest_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            //MessageBox.Show(e.ToString());
            //MessageBox.Show(sliderTest.Value.ToString());
            
            Trace.WriteLine("Slider=" + sliderTest.Value.ToString());
            Trace.WriteLine("Slider e=" + e.ToString());

        }

        private void combo_02_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
