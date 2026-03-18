using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Video_Editor.Models
{
    internal class Timeline
    {
        public List<VideoChunk> VideoItems { get; set; } = new List<VideoChunk>();
        public List<AudioChunk> AudioItems { get; set; } = new List<AudioChunk>();
        public long TotalLength 
        { 
            get 
            {
                long videoLength = 0;
                foreach (var item in VideoItems)
                {
                    videoLength += item.endTime - item.startTime;
                }

                long audioLength = 0;
                foreach (var item in AudioItems)
                {
                    audioLength += item.endTime - item.startTime;
                }

                if (videoLength > audioLength)
                {
                    return videoLength;
                }
                else
                {
                    return audioLength;
                }
            }
        }

        public Timeline()
        {
        }
    }
}
