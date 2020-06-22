using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace mediaplayer
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
        string[] files, paths; 
        private void button_Click(object sender, RoutedEventArgs e)
        {
            mediaElement.Play();
        }
        void timer_tick (object sender,EventArgs e)
        {
            slider.Value = mediaElement.Position.TotalSeconds;
        }
        private void button3_Click(object sender, RoutedEventArgs e)
        {
            mediaElement.Stop();
        }

        private void slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            TimeSpan ts = TimeSpan.FromSeconds(e.NewValue);
            mediaElement.Position = ts;
        }

        private void slider1_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            mediaElement.Volume = slider1.Value;
        }
        private void mediaElement_mediaopened(object sender,RoutedEventArgs e)
        {
            if(mediaElement.NaturalDuration.HasTimeSpan)
            {
                TimeSpan ts = TimeSpan.FromMilliseconds(mediaElement.NaturalDuration.TimeSpan.TotalMilliseconds);
                slider.Maximum = ts.TotalSeconds;
            }

        }

        private void button2_Click_1(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.AddExtension = true;
            ofd.DefaultExt = "*.*";
            ofd.Filter = "Media Files (*.*)|*.*";
            ofd.ShowDialog();
            textBox.Text = ofd.FileName;
            try { mediaElement.Source = new Uri(ofd.FileName); }
            catch { new NullReferenceException("error"); }
            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += new EventHandler(timer_tick);
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();

        }

        private void button1_Click_1(object sender, RoutedEventArgs e)
        {
            mediaElement.Pause();
        }
  
        private void button3_Click_1(object sender, RoutedEventArgs e)
        {
            mediaElement.Stop();
        }

        private void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try { mediaElement.Source = new Uri(paths[listBox.SelectedIndex]); }
            catch { new NullReferenceException("error"); }
        }

        private void button5_Click(object sender, RoutedEventArgs e)
        {
            mediaElement.Position = mediaElement.Position + TimeSpan.FromSeconds(10);
        }

        private void button7_Click(object sender, RoutedEventArgs e)
        {
            mediaElement.SpeedRatio *= 1.5;
        }

        private void button8_Click(object sender, RoutedEventArgs e)
        {
            mediaElement.SpeedRatio /= 1.5;
        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = true;
            files = ofd.SafeFileNames;
            paths = ofd.FileNames;
            for (int i = 0; i < files.Length; i++)
            {
                listBox.Items.Add(files[i]);
            }
        }
    }
}
