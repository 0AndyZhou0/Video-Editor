using Avalonia.Input;
using Avalonia.Platform.Storage;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Video_Editor.Models;

namespace Video_Editor.ViewModels
{
    public partial class MediaLibraryViewModel : ViewModelBase
    {
        public ObservableCollection<MediaItem> MediaItemsInLibrary { get; } = new ObservableCollection<MediaItem>();

        [ObservableProperty]
        private MediaItem? _SelectedMediaItem;

        public MediaLibraryViewModel()
        {
            //MediaItemsInLibrary.Add(new MediaItem("SampleVideo1.mp4"));
            //MediaItemsInLibrary.Add(new MediaItem("SampleVideo2.mp4"));
            //MediaItemsInLibrary.Add(new MediaItem("SampleVideo3.mp4"));
            //MediaItemsInLibrary.Add(new MediaItem("SampleVideo4.mp4"));
            //MediaItemsInLibrary.Add(new MediaItem("SampleVideo5.mp4"));
            //MediaItemsInLibrary.Add(new MediaItem("SampleVideo6.mp4"));
            //MediaItemsInLibrary.Add(new MediaItem("SampleVideo7.mp4"));
            //MediaItemsInLibrary.Add(new MediaItem("SampleVideo8.mp4"));
            //MediaItemsInLibrary.Add(new MediaItem("SampleVideo9.mp4"));
            //MediaItemsInLibrary.Add(new MediaItem("SampleVideo10.mp4"));
            //MediaItemsInLibrary.Add(new MediaItem("SampleVideo11.mp4"));
            //MediaItemsInLibrary.Add(new MediaItem("SampleVideo12.mp4"));
            //MediaItemsInLibrary.Add(new MediaItem("SampleVideo13.mp4"));
            //MediaItemsInLibrary.Add(new MediaItem("SampleVideo14.mp4"));
            //MediaItemsInLibrary.Add(new MediaItem("SampleVideo15.mp4"));
            //MediaItemsInLibrary.Add(new MediaItem("SampleVideo16.mp4"));
            //MediaItemsInLibrary.Add(new MediaItem("SampleVideo17.mp4"));
            //MediaItemsInLibrary.Add(new MediaItem("SampleVideo18.mp4"));
            //MediaItemsInLibrary.Add(new MediaItem("SampleVideo19.mp4"));
            //MediaItemsInLibrary.Add(new MediaItem("SampleVideo20.mp4"));
        }

        public void AddMediaItem(string name, Uri uri)
        {
            MediaItemsInLibrary.Add(new MediaItem(name, uri));
        }

        public void RemoveMediaItem(MediaItem item)
        {
            MediaItemsInLibrary.Remove(item);
        }

        internal void AddMediaFromDragItem(IStorageItem item)
        {
            if (item.Path is not null)
            {
                AddMediaItem(item.Name, item.Path);
            }
        }
    }
}
