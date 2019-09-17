using Microsoft.Win32;
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
using System.Windows.Threading;

namespace WpfAppGUI
{
    //****NEED TO DISPOSE:DispatchTimer,
    public partial class MainWindow : Window
    {
        //Instatiating a media player
        MediaPlayer media = new MediaPlayer();

        public MainWindow()
        {
            InitializeComponent();
        }
        
        public void LoadAudioFile()
        {
            //Creating a dialog to open an audio file
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "MP3 files (*.mp3)|*.mp3|All files (*.*)|*.*";
            if (dialog.ShowDialog() == true)
            {
                media.Open(new Uri(dialog.FileName));
            }
            //Inistatiating the timer 
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();
        }
        void Timer_Tick(object sender, EventArgs e)
        {
            if (media.Source != null)
            {
                labelStatus.Content = String.Format("{0}/{1}",media.Position.ToString(@"mm\:ss"),media.NaturalDuration.TimeSpan.ToString(@"mm\:ss"));
            }
            else
            {
                labelStatus.Content = "No media is selected";
            }
        }
        //ResumeLabel triggered when the PAUSE btn is pressed**
        public void ResumeLabel(object sender, RoutedEventArgs e)
        {
            playBtn.Content = "Resume";
        }
        //**Play Button**
        private void PlayBtn_Click(object sender, RoutedEventArgs e)
        {
            media.Play();
            //pauseBtn when clicked is subscribed to ResumeLabel 
            pauseBtn.Click += ResumeLabel ;
        }
        //PlayLabel triggered when the PLAY btn is pressed**
        public void PlayLabel(object sender, RoutedEventArgs e)
        {
            playBtn.Content = "Play";
        }
        //**Pause Button**
        private void PauseBtn_Click(object sender, RoutedEventArgs e)
        {
            //playBtn when clicked is subscribed to PlayLabel
            playBtn.Click += PlayLabel;
            labelStatus.Content = "Media Paused";
            media.Pause();
        }
        //**Stop Button**
        private void StopBtn_Click(object sender, RoutedEventArgs e)
        {
            //stopBtn when clicked is subscribed to PlayLabel
            stopBtn.Click += PlayLabel;
            media.Stop();
            labelStatus.Content = "Media Stopped";
        }
        //**Open Button**
        private void OpenBtn_Click(object sender, RoutedEventArgs e)
        {
            labelStatus.Content = "Opening Media...";
            LoadAudioFile();
            labelStatus.Content = "Media Opened";
        }

        //Desctor
        ~MainWindow()
        {
            media.Close();

        }
    }
    
}
