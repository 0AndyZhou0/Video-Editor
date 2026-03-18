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
        public VideoChunk(string name, Uri uri, long startPosition) : base(name, uri, startPosition)
        {
        }

        [SetsRequiredMembers]
        public VideoChunk(string name, Uri uri, double scale) : base(name, uri, scale)
        {
        }

    }
}
