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

    public SeekbarView()
    {
        InitializeComponent();

        AddHandler(PointerPressedEvent, OnSeekbarPointerPress);
    }


    private void OnSeekbarPointerPress(object? sender, PointerPressedEventArgs e)
    {
        long pos = (long)(e.GetPosition(this).X);
        if (DataContext is ViewModels.MainWindowViewModel vm)
        {
            (int index, long position) = vm.Timeline.GetVideoChunkAtPosition(pos);
            if (index == vm.Timeline.VideoTimeline.Count)
            {
                vm.VideoPlayer.Play(vm.Timeline.VideoTimeline[index - 1]);
                long msPosition = vm.Timeline.VideoTimeline[index - 1].getDuration();
                vm.VideoPlayer.JumpTo(msPosition);
            } 
            else
            {
                vm.VideoPlayer.Play(vm.Timeline.VideoTimeline[index]);
                long msPosition = vm.Timeline.VideoTimeline[index].getDuration() * position / vm.Timeline.VideoTimeline[index].Length;
                vm.VideoPlayer.JumpTo(msPosition);
            }
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