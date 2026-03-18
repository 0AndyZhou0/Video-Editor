using CommunityToolkit.Mvvm.ComponentModel;
using LibVLCSharp.Shared;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Video_Editor.Models
{
    public class MediaItem : ObservableObject
    {
        public required string Name { get; init; }
        public required Uri Uri { get; init; }
        public bool isEmpty = false;
        public required long startTime { get; init; }
        public required long endTime { get; init; }
        public long startPosition { get; set; }
        private double _scale;
        public double Scale 
        { 
            set { SetProperty(ref _scale, value); }
            get { return _scale; }
        }

        // public double Length => (endTime - startTime) * _scale;

        private long _length;
        public long Length
        {
            set { SetProperty(ref _length, value); }
            get { return _length; }
        }

        [SetsRequiredMembers]
        public MediaItem(string name, Uri uri, double scale)
        {
            Name = name;
            Uri = uri;
            startTime = 0;
            endTime = getDuration();
            _scale = scale;
            _length = (long)((endTime - startTime) * _scale);
        }

        [SetsRequiredMembers]
        public MediaItem(string name, Uri uri, long startPos)
        {
            Name = name;
            Uri = uri;
            startTime = 0;
            endTime = getDuration();
            startPosition = startPos;
            _scale = 0.2;
            _length = (long)((endTime - startTime) * _scale);
        }

        public long getDuration()
        {
            LocalMediaPlayer vlcPlayer = new LocalMediaPlayer(); // TODO: Maybe make a singleton
            Media media = new Media(vlcPlayer.LibVLC, getPath());
            media.Parse(MediaParseOptions.ParseLocal);
            while (media.ParsedStatus != MediaParsedStatus.Done) { };
            return media.Duration;
        }

        public override bool Equals(object? obj)
        {
            if (obj is MediaItem other)
            {
                if (this.Name == other.Name && this.Uri == other.Uri)
                {
                    return true;
                }
            }
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Uri.ToString());
        }

        public string getPath()
        {
            return Uri.LocalPath;
        }
    }
}
