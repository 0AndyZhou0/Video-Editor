using Avalonia.Interactivity;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace Video_Editor.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        public ViewModels.VideoPlayerViewModel VideoPlayer { get; }
        public ViewModels.MediaLibraryViewModel MediaLibrary { get; }

        public ViewModels.TimelineViewModel Timeline { get; }

        public MainWindowViewModel()
        {
            VideoPlayer = new ViewModels.VideoPlayerViewModel();
            MediaLibrary = new ViewModels.MediaLibraryViewModel();
            Timeline = new ViewModels.TimelineViewModel();
        }
    }
}
