using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Video_Editor.Models
{
    public class VideoChunk : MediaItem
    {
        [SetsRequiredMembers]
        public VideoChunk(string name, Uri uri, long startPosition, long endPosition) : base(name, uri, startPosition, endPosition)
        {
        }

        [SetsRequiredMembers]
        public VideoChunk(string name, Uri uri, long startPosition, long endPosition, long length) : base(name, uri, startPosition, endPosition, length)
        {
        }

        [SetsRequiredMembers]
        public VideoChunk(string name, Uri uri) : base(name, uri)
        {
        }

    }
}
