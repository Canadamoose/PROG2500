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

namespace Screens_01
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
    
        }


        //private ImageSource _myImage = null;
        //public ImageSource MyImage
        //{
        //    get { return _myImage; }
        //    set
        //    {
        //        _myImage = value;
        //    }
        //}



        //private void btn_media_Click(object sender, RoutedEventArgs e)
        //{
        //    if (mediaElement.LoadedBehavior == MediaState.Pause)
        //    {
        //        mediaElement.LoadedBehavior = MediaState.Play;
        //    }
        //    else
        //    {
        //        mediaElement.LoadedBehavior = MediaState.Pause;
        //    }
        //}

        //private void btn_image_Click(object sender, RoutedEventArgs e)
        //{
        //    Trace.WriteLine("btn_image_Click start");

        //    try
        //    {
        //        _myImage = new BitmapImage(new Uri(String.Format("media/Tucker.jpg"), UriKind.Relative));
        //        _myImage.Freeze(); // -> to prevent error: "Must create DependencySource on same Thread as the DependencyObject"
        //        MyImage = _myImage;
        //    }
        //    catch
        //    {
        //        Trace.WriteLine("Image Exception");
        //    }

        //    image.Source = MyImage;
            
        //}

        //private void btn_browser_Click(object sender, RoutedEventArgs e)
        //{
        //    //bowser.Source = "www.google.ca";
        //    //bowser.Source = new Uri(String.Format("www.google.ca"), UriKind.Absolute);
        //    //bowser.Source = new Uri(String.Format("www.google.ca"));
        //    //bowser.Navigate("https://www.google.ca");
        //    MessageBox.Show("Button on 3rd tab");
        //}
    }
}
