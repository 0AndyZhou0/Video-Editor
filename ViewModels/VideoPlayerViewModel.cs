using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Platform;
using CommunityToolkit.Mvvm.ComponentModel;
using LibVLCSharp.Avalonia;
using LibVLCSharp.Shared;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Video_Editor.Models;

namespace Video_Editor.ViewModels
{
    public partial class VideoPlayerViewModel : ViewModelBase
    {
        [ObservableProperty]
        private string? _VideoPath;

        public LibVLC LibVLC;

        [ObservableProperty]
        private MediaPlayer _MediaPlayer;

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
            LibVLC = new LibVLC();
            MediaPlayer = new MediaPlayer(LibVLC);

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

        public string GetVideoPath(MediaItem item)
        {
            return item.getPath();
        }

        public long GetVideoDuration()
        {
            return MediaPlayer.Length;
        }

        public async Task<long> GetVideoDurationAsync(string path)
        {
            using Media media = new Media(LibVLC, path);
            if (await media.Parse(MediaParseOptions.ParseLocal) == MediaParsedStatus.Done)
            {
                return media.Duration;
            }
            else
            {
                return -1;
            }
        }


        public void Play(MediaItem? item) 
        {
            if (item == null)
            {
                return;
            }
            string path = item.getPath();
            if (string.IsNullOrEmpty(path))
            {
                return;
            }
            Media media = new Media(LibVLC, path);
            MediaPlayer.Play(media);
            CurrentTime = 0;
            CurrentPercent = 0;
            media.Dispose();
        }
        public void Play(string path)
        {
            Debug.WriteLine("Playing video: " + path);
            if (string.IsNullOrEmpty(path))
            {
                return;
            }
            Media media = new Media(LibVLC, path);
            MediaPlayer.Play(media);
            CurrentTime = 0;
            CurrentPercent = 0;
            media.Dispose();
        }

        public void Play()
        {
            string path = GetVideoPath();
            Debug.WriteLine("Playing video: " + path);
            if (string.IsNullOrEmpty(path))
            {
                return;
            }
            Media media = new Media(LibVLC, path);
            MediaPlayer.Play(media);
            CurrentTime = 0;
            CurrentPercent = 0;
            media.Dispose();
        }

        public void PlayPause()
        {
            MediaPlayer.Pause();
        }

        public void Stop()
        {
            MediaPlayer.Stop();
        }

        public void JumpBack(long ms)
        {
            MediaPlayer.Time -= ms;
        }

        public void JumpForward(long ms)
        {
            MediaPlayer.Time += ms;
            CurrentTime = MediaPlayer.Time;
        }

        public void JumpTo(long ms)
        {
            MediaPlayer.Time += ms - MediaPlayer.Time;
        }
    }
}
