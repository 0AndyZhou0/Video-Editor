using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System.Diagnostics;

namespace Video_Editor.Views;

public partial class TimelineView : UserControl
{
    public TimelineView()
    {
        InitializeComponent();
    }


    // TODO: Make hold seeking pause playback
    private void HandleSeekbarPress(object? sender, Avalonia.Controls.Primitives.RangeBaseValueChangedEventArgs e)
    {
        //Debug.WriteLine("Seekbar pressed at: " + SeekbarUser.Value);
        if (DataContext is ViewModels.MainWindowViewModel vm)
        {
            vm.VideoPlayer.JumpTo((long)(SeekbarUser.Value));
        }
    }
}