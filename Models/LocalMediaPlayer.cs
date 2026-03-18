using LibVLCSharp.Shared;

namespace Video_Editor.Models
{
    public class LocalMediaPlayer
    {
        private LibVLC _libVLC;
        public LibVLC LibVLC { get => _libVLC; set => _libVLC = value; }
        private MediaPlayer _mediaPlayer;
        public MediaPlayer MediaPlayer { get => _mediaPlayer; set => _mediaPlayer = value; }

        public LocalMediaPlayer(LibVLC libVLC)
        {
            _libVLC = libVLC;
            _mediaPlayer = new MediaPlayer(libVLC);
        }

        public LocalMediaPlayer()
        {
            _libVLC = new LibVLC();
            _mediaPlayer = new MediaPlayer(_libVLC);
        }
    }
}
