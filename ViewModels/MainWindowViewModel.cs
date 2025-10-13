using Avalonia.Interactivity;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace Video_Editor.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        public ViewModels.VideoPlayerViewModel VideoPlayer { get; }

        public MainWindowViewModel()
        {
            VideoPlayer = new ViewModels.VideoPlayerViewModel();
        }
    }
}
