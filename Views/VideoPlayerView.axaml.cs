using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using LibVLCSharp.Avalonia;
using LibVLCSharp.Shared;
using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace Video_Editor.Views;

public partial class VideoPlayerView : UserControl
{
    public required ViewModels.MainWindowViewModel vm;

    public VideoPlayerView()
    {
        InitializeComponent();
        if (DataContext is ViewModels.MainWindowViewModel vm) 
        {
            this.vm = vm;
        }
    }

    public void HandlePlayButton(object sender, RoutedEventArgs e)
    {
        if (DataContext is ViewModels.MainWindowViewModel vm)
        {
            vm.VideoPlayer.Play();
        }
    }

    public void HandleRewindButton(object sender, RoutedEventArgs e)
    {
        throw new NotImplementedException();
    }

    public void HandlePlayPauseButton(object sender, RoutedEventArgs e)
    {
        if (DataContext is ViewModels.MainWindowViewModel vm)
        {
            vm.VideoPlayer.PlayPause();
        }
    }

    public void HandleStopButton(object sender, RoutedEventArgs e)
    {
        if (DataContext is ViewModels.MainWindowViewModel vm)
        {
            vm.VideoPlayer.Stop();
        }
    }

    public void HandleJumpBackButton(object sender, RoutedEventArgs e)
    {
        if (DataContext is ViewModels.MainWindowViewModel vm)
        {
            vm.VideoPlayer.JumpBack(5000);
        }
    }

    public void HandleJumpForwardButton(object sender, RoutedEventArgs e)
    {
        if (DataContext is ViewModels.MainWindowViewModel vm)
        {
            vm.VideoPlayer.JumpForward(5000);
        }
    }

    // TODO: Make hold seeking pause playback
    //private void HandleSeekbarPress(object? sender, Avalonia.Controls.Primitives.RangeBaseValueChangedEventArgs e)
    //{
    //    if (DataContext is ViewModels.MainWindowViewModel vm)
    //    {
    //        vm.VideoPlayer.JumpTo((long)(SeekbarUser.Value));
    //    }
    //}
}