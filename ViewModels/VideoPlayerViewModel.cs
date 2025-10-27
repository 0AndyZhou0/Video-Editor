using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Platform;
using CommunityToolkit.Mvvm.ComponentModel;
using LibVLCSharp.Avalonia;
using LibVLCSharp.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Video_Editor.ViewModels
{
    public partial class VideoPlayerViewModel : ViewModelBase
    {
        [ObservableProperty]
        private string? _VideoPath;

        LibVLC? libVLC;

        [ObservableProperty]
        private MediaPlayer? _MediaPlayer;

        [ObservableProperty]
        private long _CurrentTime = 0; // in ms

        [ObservableProperty]
        private long _TotalTime = 1; // in ms

        [ObservableProperty]
        private long _CurrentPercent = 0; // 0 to 10000

        public VideoPlayerViewModel()
        {
            //var os = Environment.OSVersion.Platform;
            Core.Initialize();
            libVLC = new LibVLC();
            MediaPlayer = new MediaPlayer(libVLC);

            MediaPlayer.LengthChanged += (sender, args) =>
            {
                TotalTime = MediaPlayer.Length;
            };

            MediaPlayer.TimeChanged += (sender, args) =>
            {
                CurrentTime = MediaPlayer.Time;
                CurrentPercent = 10000 * CurrentTime / TotalTime;
            };
        }

        public string GetVideoPath()
        {
            return VideoPath ?? string.Empty;
        }

        public void Play()
        {
            string path = GetVideoPath();
            if (string.IsNullOrEmpty(path) || MediaPlayer == null || libVLC == null)
            {
                return;
            }
            Media media = new Media(libVLC, path);
            MediaPlayer.Play(media);
            CurrentTime = 0;
            CurrentPercent = 0;
            media.Dispose();
        }

        public void PlayPause()
        {
            if (MediaPlayer == null)
            {
                return;
            }
            MediaPlayer.Pause();
        }

        public void Stop()
        {
            if (MediaPlayer == null)
            {
                return;
            }
            MediaPlayer.Stop();
        }

        public void JumpBack(long ms)
        {
            if (MediaPlayer == null)
            {
                return;
            }
            MediaPlayer.Time -= ms;
        }

        public void JumpForward(long ms)
        {
            if (MediaPlayer == null)
            {
                return;
            }
            MediaPlayer.Time += ms;
            CurrentTime = MediaPlayer.Time;
        }

        public void JumpTo(long ms)
        {
            if (MediaPlayer == null)
            {
                return;
            }
            MediaPlayer.Time += ms - MediaPlayer.Time;
        }
    }
}
