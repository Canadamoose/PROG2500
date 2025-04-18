using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;  //add Threading

using System.IO;  // add File IO
using System.ComponentModel;
using System.Windows.Threading;  //add background worker

namespace kin4
{
    /// <summary>
    /// Demonstrate threads mixing data when not synched
    /// </summary>
    public partial class MainWindow : Window
    {

        TextBox textBoxValue;  // Need to send text output to other classes

        public MainWindow()
        {
            InitializeComponent();

            textBoxValue = Text_Output ;  // Set output for use in other classes
                     
            // invoke GUI update using GUI thread...to output on textbox
            Dispatcher.Invoke(new Action(() =>
            {
                //Tell user the app is ready
                Text_Output.AppendText(" .. Startup ... ready to run either synched on non-synched data access");
            }));
        }


        // Clear the textbox...BackgroundWorker keeps running       
        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            Text_Output.Clear();  // we're on the GUI thread here
        }

        private void Synch_Button_Click(object sender, RoutedEventArgs e)
        {
            Text_Output.AppendText("\r\nStarting SYNCHED version...\r\n");
            var main = new synched.Main(Dispatcher);
            new Thread(() => main.Main1(Text_Output)) { IsBackground = true }.Start();
        }

        private void NotSynch_button_Click(object sender, RoutedEventArgs e)
        {
            Text_Output.AppendText("\r\nStarting NOT SYNCHED version...\r\n");
            var main = new notsynched.Main(Dispatcher);
            new Thread(() => main.Main1(Text_Output)) { IsBackground = true }.Start();
        }

        private void Run_Background_Synch_Button_Click(object sender, RoutedEventArgs e)
        {
            Text_Output.AppendText("\rRun_Background_Synch_Button_Click\r");
            Dispatcher Dis = Dispatcher;
            synched.Main m1 = new synched.Main(Dis);
            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                m1.Main1(textBoxValue);  // run giving output textbox
            }).Start();
        }

        private void Run_Background_NotSynch_Button_Click(object sender, RoutedEventArgs e)
        {
            Text_Output.AppendText("\rRun_Background_NotSynch_Button_Click\r");
            Dispatcher Dis = Dispatcher;
            notsynched.Main m1 = new notsynched.Main(Dis);


            //// Start thread as Lamda...
            //new Thread(() =>
            //{
            //    Thread.CurrentThread.IsBackground = true;
            //    m1.Main1(textBoxValue);  // run giving output textbox
            //}).Start();


            // not as Lamda...
            void worker()
            {
                Thread.CurrentThread.IsBackground = true;
                m1.Main1(textBoxValue);  // run giving output textbox
            }
            Thread t = new Thread(worker);
            t.Start();
        }

        private void Say_Hi_Button_Click(object sender, RoutedEventArgs e)
        {
            Text_Output.AppendText("\rHi from GUI Thread\r");  // we're on the GUI thread here
        }
    }
}
