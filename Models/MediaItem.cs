using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Video_Editor.Models
{
    public class MediaItem : ObservableObject
    {
        public required string Name { get; init; }
        public required Uri Uri { get; init; }
        public bool isEmpty = false;
        public required long startPosition { get; init; }
        public required long endPosition { get; init; }

        private long _length;
        public long Length 
        { 
            get { return _length;  }
            set { SetProperty(ref _length, value); }
        }

        [SetsRequiredMembers]
        public MediaItem(string name, Uri uri)
        {
            Name = name;
            Uri = uri;
            startPosition = 0;
            endPosition = 0;
            Length = 0;
        }

        [SetsRequiredMembers]
        public MediaItem(string name, Uri uri, long startPos, long endPos)
        {
            Name = name;
            Uri = uri;
            startPosition = startPos;
            endPosition = endPos;
            Length = endPos - startPos;
        }

        [SetsRequiredMembers]
        public MediaItem(string name, Uri uri, long startPos, long endPos, long length)
        {
            Name = name;
            Uri = uri;
            startPosition = startPos;
            endPosition = endPos;
            Length = length;
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
