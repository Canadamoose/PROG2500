using System;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Screens_01
{
    public partial class FaceMakerTab : UserControl
    {
        private Person CurrentPerson => (DataContext as MainViewModel)?.Person;

        private BitmapImage[] hairImages = new BitmapImage[2];
        private BitmapImage[] eyeImages = new BitmapImage[2];
        private BitmapImage[] noseImages = new BitmapImage[2];
        private BitmapImage[] mouthImages = new BitmapImage[2];

        private int hairIndex, eyeIndex, noseIndex, mouthIndex;
        private Random rand = new Random();

        public FaceMakerTab()
        {
            InitializeComponent();
            LoadImages();
            ApplyCurrentFace();
        }

        private void LoadImages()
        {
            hairImages[0] = LoadImage("images/Hair_1.png");
            hairImages[1] = LoadImage("images/Hair_2.png");
            eyeImages[0] = LoadImage("images/Eye_1.png");
            eyeImages[1] = LoadImage("images/Eye_2.png");
            noseImages[0] = LoadImage("images/Nose_1.png");
            noseImages[1] = LoadImage("images/Nose_2.png");
            mouthImages[0] = LoadImage("images/Mouth_1.png");
            mouthImages[1] = LoadImage("images/Mouth_2.png");
        }

        private BitmapImage LoadImage(string path) =>
            new BitmapImage(new Uri(path, UriKind.Relative));

        private void ApplyCurrentFace()
        {
            imgHair.Source = hairImages[hairIndex];
            imgEyes.Source = eyeImages[eyeIndex];
            imgNose.Source = noseImages[noseIndex];
            imgMouth.Source = mouthImages[mouthIndex];

            if (CurrentPerson == null) return;

            CurrentPerson.HairIndex = hairIndex;
            CurrentPerson.EyeIndex = eyeIndex;
            CurrentPerson.NoseIndex = noseIndex;
            CurrentPerson.MouthIndex = mouthIndex;
        }

        private void btnHairPrev_Click(object s, System.Windows.RoutedEventArgs e)
        {
            hairIndex = (hairIndex - 1 + hairImages.Length) % hairImages.Length;
            ApplyCurrentFace();
        }

        private void btnHairNext_Click(object s, System.Windows.RoutedEventArgs e)
        {
            hairIndex = (hairIndex + 1) % hairImages.Length;
            ApplyCurrentFace();
        }

        private void btnEyePrev_Click(object s, System.Windows.RoutedEventArgs e)
        {
            eyeIndex = (eyeIndex - 1 + eyeImages.Length) % eyeImages.Length;
            ApplyCurrentFace();
        }

        private void btnEyeNext_Click(object s, System.Windows.RoutedEventArgs e)
        {
            eyeIndex = (eyeIndex + 1) % eyeImages.Length;
            ApplyCurrentFace();
        }

        private void btnNosePrev_Click(object s, System.Windows.RoutedEventArgs e)
        {
            noseIndex = (noseIndex - 1 + noseImages.Length) % noseImages.Length;
            ApplyCurrentFace();
        }

        private void btnNoseNext_Click(object s, System.Windows.RoutedEventArgs e)
        {
            noseIndex = (noseIndex + 1) % noseImages.Length;
            ApplyCurrentFace();
        }

        private void btnMouthPrev_Click(object s, System.Windows.RoutedEventArgs e)
        {
            mouthIndex = (mouthIndex - 1 + mouthImages.Length) % mouthImages.Length;
            ApplyCurrentFace();
        }

        private void btnMouthNext_Click(object s, System.Windows.RoutedEventArgs e)
        {
            mouthIndex = (mouthIndex + 1) % mouthImages.Length;
            ApplyCurrentFace();
        }

        private void Add_Face_Click(object s, System.Windows.RoutedEventArgs e)
        {
            hairIndex = rand.Next(hairImages.Length);
            eyeIndex = rand.Next(eyeImages.Length);
            noseIndex = rand.Next(noseImages.Length);
            mouthIndex = rand.Next(mouthImages.Length);
            ApplyCurrentFace();
        }
    }
}
