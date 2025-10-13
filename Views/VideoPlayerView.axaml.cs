using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using LibVLCSharp.Avalonia;
using LibVLCSharp.Shared;
using System;
using System.Diagnostics;

namespace Video_Editor.Views;

public partial class VideoPlayerView : UserControl
{
    public VideoPlayerView()
    {
        InitializeComponent();
        VideoView.Width = 800;
        VideoView.Height = 450;
    }

    public void HandlePlayButton(object sender, RoutedEventArgs e)
    {
        if (DataContext is ViewModels.VideoPlayerViewModel vm)
        {
            vm.Play();
        }
    }

    public void HandleRewindButton(object sender, RoutedEventArgs e)
    {
        throw new NotImplementedException();
    }

    public void HandlePlayPauseButton(object sender, RoutedEventArgs e)
    {
        if (DataContext is ViewModels.VideoPlayerViewModel vm)
        {
            vm.PlayPause();
        }
    }

    public void HandleStopButton(object sender, RoutedEventArgs e)
    {
        if (DataContext is ViewModels.VideoPlayerViewModel vm)
        {
            vm.Stop();
        }
    }

    public void HandleJumpBackButton(object sender, RoutedEventArgs e)
    {
        if (DataContext is ViewModels.VideoPlayerViewModel vm)
        {
            vm.JumpBack(5000);
        }
    }

    public void HandleJumpForwardButton(object sender, RoutedEventArgs e)
    {
        if (DataContext is ViewModels.VideoPlayerViewModel vm)
        {
            vm.JumpForward(5000);
        }
    }

    private void HandleSeekbarPress(object sender, RoutedEventArgs e)
    {
        Debug.WriteLine("Seekbar pressed at: " + SeekbarUser.Value);
        if (DataContext is ViewModels.VideoPlayerViewModel vm)
        {
            vm.JumpTo((long)(SeekbarUser.Value));
        }
    }
}