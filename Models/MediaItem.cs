using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Video_Editor.Models
{
    public class MediaItem
    {
        public required string Name { get; init; }
        public required Uri Uri { get; init; }

        [SetsRequiredMembers]
        public MediaItem(string name, Uri uri)
        {
            Name = name;
            Uri = uri;
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
    }
}
