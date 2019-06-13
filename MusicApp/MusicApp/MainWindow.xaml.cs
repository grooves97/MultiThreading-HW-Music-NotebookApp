using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace MusicApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MediaPlayer _player;
        private Thread _musicThread;
        private OpenFileDialog _openFileDialog;
        public MainWindow()
        {
            InitializeComponent();

            _player = new MediaPlayer();
            _openFileDialog = new OpenFileDialog();
        }

        private void PlayMusic()
        {
            _musicThread = new Thread(PlayMusic);
            _musicThread.IsBackground = true;
            _musicThread.Start();
        }

        private void PlayMusicButtonClick(object sender, RoutedEventArgs e)
        {
            Dispatcher.BeginInvoke(new ThreadStart(delegate { _player.Pause(); }));
        }

        private void PauseMusicButtonClick(object sender, RoutedEventArgs e)
        {
            Dispatcher.BeginInvoke(new ThreadStart(delegate { _player.Pause(); }));
        }

        private void OpenFileButtonClick(object sender, RoutedEventArgs e)
        {
            _openFileDialog.Filter = "MP3 files (*.mp3)|*.mp3|All files (*.*)|*.*";
            if (_openFileDialog.ShowDialog() == true)
                _player.Open(new Uri(_openFileDialog.FileName));

            PlayMusic();
            songNameTextBlock.Text = "Playing now: " + _openFileDialog.SafeFileName;
        }

        private void SaveText()
        {
            Dispatcher.BeginInvoke(new ThreadStart(delegate
            {
                TextRange doc = new TextRange(noteRichTextBox.Document.ContentStart, noteRichTextBox.Document.ContentEnd);
                using (FileStream fileStream = File.Create(DateTime.Now.ToLongDateString() + ".txt"))
                {
                    doc.Save(fileStream, DataFormats.Text);
                }
            }));
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Thread saveText = new Thread(SaveText);
            saveText.IsBackground = false;
            saveText.Start();
        }
    }
}
