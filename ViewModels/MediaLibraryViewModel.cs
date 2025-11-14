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
            
        }

        public MediaItem? AddMediaItem(string name, Uri uri)
        {
            for (int i = 0; i < MediaItemsInLibrary.Count; i++)
            {
                if (MediaItemsInLibrary[i].Uri == uri)
                {
                    return null;
                }
            }
            MediaItem newFile = new MediaItem(name, uri);
            MediaItemsInLibrary.Add(newFile);
            return newFile;
        }

        public void RemoveMediaItem(MediaItem item)
        {
            MediaItemsInLibrary.Remove(item);
        }

        internal bool AddMediaFromDragItem(IStorageItem item)
        {
            if (item.Path is not null)
            {
                if (AddMediaItem(item.Name, item.Path) is not null)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
