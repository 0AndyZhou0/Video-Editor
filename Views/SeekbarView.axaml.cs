using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using System;
using System.Diagnostics;
using Avalonia.Platform.Storage;
using Avalonia.Controls.Shapes;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using Video_Editor.Models;

namespace Video_Editor.Views;

public partial class SeekbarView : UserControl
{

    private bool _isDragging = false;
    private bool _lastKnownIsPlaying = false;

    public SeekbarView()
    {
        InitializeComponent();

        AddHandler(PointerPressedEvent, OnSeekbarPointerPress);
        AddHandler(PointerMovedEvent, OnSeekbarPointerMove);
        AddHandler(PointerReleasedEvent, OnSeekbarPointerRelease);
    }

    private void OnSeekbarPointerRelease(object? sender, PointerReleasedEventArgs e)
    {
        _isDragging = false;
        if (DataContext is ViewModels.MainWindowViewModel vm)
        {
            if (_lastKnownIsPlaying)
            {
                vm.VideoPlayer.UnPause();
            }
        }
    }

    private void OnSeekbarPointerMove(object? sender, PointerEventArgs e)
    {
        if (!_isDragging) return;
        long pos = (long)e.GetPosition(this).X;
        if (pos < 0) pos = 0;
        SeekVideoToPosition(pos);
    }

    private void OnSeekbarPointerPress(object? sender, PointerPressedEventArgs e)
    {
        if (_isDragging) return; // TODO: This is a hack, maybe use a buffer to store user inputs so they only can trigger one at a time
        _isDragging = true;
        long pos = (long)e.GetPosition(this).X;
        if (pos < 0) pos = 0;
        SeekVideoToPosition(pos);
    }

    public void SeekVideoToPosition(long pos) // TODO: Probably need to store videos in buffer or something (Maybe at least two just for playback)
    {
        if (DataContext is ViewModels.MainWindowViewModel vm)
        {
            if (vm.Timeline.VideoTimeline.Count == 0) return;
            _lastKnownIsPlaying = vm.VideoPlayer.IsPlaying();
            (int index, long position) = vm.Timeline.GetVideoChunkAtPosition(pos);
            if (index == vm.Timeline.VideoTimeline.Count)
            {
                if (vm.VideoPlayer.GetVideoPath() != vm.Timeline.VideoTimeline[index - 1].getPath()) {
                    vm.VideoPlayer.Play(vm.Timeline.VideoTimeline[index - 1]);
                }
                long msPosition = vm.Timeline.VideoTimeline[index - 1].getDuration();
                vm.VideoPlayer.JumpTo(msPosition);
            } 
            else
            {
                if (vm.VideoPlayer.GetVideoPath() != vm.Timeline.VideoTimeline[index].getPath()) {
                    vm.VideoPlayer.Play(vm.Timeline.VideoTimeline[index]);
                }
                long msPosition = vm.Timeline.VideoTimeline[index].getDuration() * position / vm.Timeline.VideoTimeline[index].Length;
                vm.VideoPlayer.JumpTo(msPosition);
            }
            vm.VideoPlayer.JumpForward(0);
            vm.VideoPlayer.Pause();
        }
    }

    public void DragOver(object? sender, DragEventArgs e)
    {
        ;
    }

    public async Task DropAsync(object? sender, DragEventArgs e)
    {
        ;
    }

    public async Task AddVideoBlockAsync(IStorageItem file)
    {
        ;
    }

    private void PointerWheelHandler(object? sender, PointerWheelEventArgs e)
    {
        ;
    }
}