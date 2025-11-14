using Avalonia.Platform.Storage;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Video_Editor.Models;

namespace Video_Editor.ViewModels
{
    public partial class TimelineViewModel : ViewModelBase
    {
        [ObservableProperty]
        private ObservableCollection<MediaItem> _VideoTimeline;

        [ObservableProperty]
        private ObservableCollection<MediaItem> _AudioTimeline;

        [ObservableProperty]
        public double zoomRatio = 1.0;

        public TimelineViewModel()
        {
            _VideoTimeline = new ObservableCollection<MediaItem>();
            _AudioTimeline = new ObservableCollection<MediaItem>();
        }

        public VideoChunk AddVideoChunk(string name, Uri uri, long startPosition, long endPosition)
        {
            var videoChunk = new VideoChunk(name, uri, startPosition, endPosition);
            VideoTimeline.Add(videoChunk);
            return videoChunk;
        }

        public VideoChunk AddVideoChunkScaled(string name, Uri uri, long startPosition, long endPosition, long length)
        {
            var videoChunk = new VideoChunk(name, uri, startPosition, endPosition, length);
            VideoTimeline.Add(videoChunk);
            return videoChunk;
        }

        public AudioChunk AddAudioChunk(string name, Uri uri, long startPosition, long endPosition)
        {
            var audioChunk = new AudioChunk(name, uri, startPosition, endPosition);
            AudioTimeline.Add(audioChunk);
            return audioChunk;
        }

        public void OnZoomChanged()
        {
            for (int i = 0; i < VideoTimeline.Count; i++)
            {
                VideoTimeline[i].Length = (long)((VideoTimeline[i].endPosition - VideoTimeline[i].startPosition) * ZoomRatio);
                Debug.WriteLine($"Video Item: {VideoTimeline[i].Name}, New Length: {VideoTimeline[i].Length}");
            }

            // Audio timeline
        }
    }
}
