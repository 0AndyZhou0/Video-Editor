using Avalonia.Controls;
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
        private ObservableCollection<MediaItem> _VideoTimeline; // TODO: FIX ALL MediaItems

        [ObservableProperty]
        private ObservableCollection<MediaItem> _AudioTimeline;

        [ObservableProperty]
        public double zoomRatio = 1.0;

        public TimelineViewModel()
        {
            _VideoTimeline = new ObservableCollection<MediaItem>();
            _AudioTimeline = new ObservableCollection<MediaItem>();
        }

        public VideoChunk AddVideoChunk(string name, Uri uri, double scale)
        {
            var videoChunk = new VideoChunk(name, uri, scale);
            VideoTimeline.Add(videoChunk);
            return videoChunk;
        }

        public VideoChunk AddVideoChunkAtIndex(string name, Uri uri, double scale, int index)
        {
            var videoChunk = new VideoChunk(name, uri, scale);
            VideoTimeline.Insert(index, videoChunk);
            return videoChunk;
        }

        public VideoChunk AddVideoChunk(string name, Uri uri, long startPosition)
        {
            var videoChunk = new VideoChunk(name, uri, startPosition);
            VideoTimeline.Add(videoChunk);
            return videoChunk;
        }

        public long GetTotalDurationOfVideoChunks()
        {
            long length = 0;
            for (int i = 0; i < VideoTimeline.Count; i++)
            {
                length += VideoTimeline[i].getDuration();
            }
            return length;
        }

        public (int index, long position) GetVideoChunkAtPosition(long position)
        {
            long currentPosition = 0;
            for (int i = 0; i < VideoTimeline.Count; i++)
            {
                currentPosition += VideoTimeline[i].Length;
                if (currentPosition > position)
                {
                    return (i, position - (currentPosition - VideoTimeline[i].Length));
                }
            }
            return (VideoTimeline.Count, 0);
        }

        public AudioChunk AddAudioChunk(string name, Uri uri, long startPosition)
        {
            var audioChunk = new AudioChunk(name, uri, startPosition);
            AudioTimeline.Add(audioChunk);
            return audioChunk;
        }

        public long GetTotalDurationOfAudioChunks()
        {
            long length = 0;
            for (int i = 0; i < AudioTimeline.Count; i++)
            {
                length += AudioTimeline[i].getDuration();
            }
            return length;
        }

        public void OnZoomChanged()
        {
            for (int i = 0; i < VideoTimeline.Count; i++)
            {
                VideoTimeline[i].Scale = ZoomRatio;
                VideoTimeline[i].Length = (long)((VideoTimeline[i].endTime - VideoTimeline[i].startTime) * ZoomRatio);
                Debug.WriteLine($"TimeLineViewModel.cs: Video Item: {VideoTimeline[i].Name}, New Length: {VideoTimeline[i].Length}");
            }

            // Audio timeline
        }
    }
}
