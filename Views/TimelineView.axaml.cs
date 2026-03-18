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
using Avalonia.VisualTree;
using System.Linq;
using Avalonia.Controls.Presenters;

namespace Video_Editor.Views;

public partial class TimelineView : UserControl
{
    private double _zoomRatio = 0.2;
    private const double ZoomFactor = 0.1;
    private const double MinZoomRatio = 0.00005;
    private const double MaxZoomRatio = 5.0;

    public TimelineView()
    {
        InitializeComponent();

        AddHandler(DragDrop.DragOverEvent, DragOver);
        AddHandler(DragDrop.DropEvent, DropAsync);
    }

    public ScrollViewer? getScrollViewer()
    {
        var scrollViewer = this.FindControl<ScrollViewer>("Timeline");
        return scrollViewer;
    }


    // TODO: Make hold seeking pause playback
    private void HandleSeekbarPress(object? sender, Avalonia.Controls.Primitives.RangeBaseValueChangedEventArgs e)
    {
        Debug.WriteLine("Seekbar pressed");
        if (DataContext is ViewModels.MainWindowViewModel vm)
        {
            // vm.VideoPlayer.JumpTo((long)(SeekbarUser.Value));
        }
    }

    public void DragOver(object? sender, DragEventArgs e)
    {
        var currentPosition = e.GetPosition(this);
        var offsetX = currentPosition.X;
        var offsetY = currentPosition.Y;
        // Add visual feedback for dragging
        if (e.DataTransfer.Items is not null && e.DataTransfer.Items.Count == 2)
        {
            e.DragEffects = DragDropEffects.Copy;
        }
        else
        {
            e.DragEffects = DragDropEffects.None;
        }
    }

    public async Task DropAsync(object? sender, DragEventArgs e)
    {
        var currentPosition = e.GetPosition(this);
        var offsetX = currentPosition.X;
        var offsetY = currentPosition.Y;
        Debug.WriteLine($"Drop Position: {offsetX}, {offsetY}");
        if (DataContext is ViewModels.MainWindowViewModel vm)
        {
            if (e.DataTransfer.Items is not null && e.DataTransfer.Items.Count == 2)
            {
                var one = e.DataTransfer.Items[0];
                var two = e.DataTransfer.Items[1];

                var item = one;
                if (one.TryGetFile() == null)
                {
                    item = two;
                }

                IStorageItem? file = item.TryGetFile();

                if (file == null)
                {
                    Debug.WriteLine("Not a file or Format is not available.");
                    return;
                }

                vm.MediaLibrary.AddMediaFromDragItem(file);
                await AddVideoBlock(file, offsetX);
            }
        }
    }

    public async Task AddVideoBlockAtEnd(IStorageItem file)
    {
        if (DataContext is ViewModels.MainWindowViewModel vm)
        {
            VideoChunk videoChunk = vm.Timeline.AddVideoChunk(file.Name, file.Path, _zoomRatio);
            Rectangle rect = new Rectangle
            {
                Width = videoChunk.Length,
                Height = 150,
                Fill = Avalonia.Media.Brushes.Blue
            };
            //Video.Children.Add(rect);
        }
    }

    public async Task AddVideoBlock(IStorageItem file, double pointerPosition)
    {
        if (DataContext is ViewModels.MainWindowViewModel vm)
        {
            // long duration = await vm.VideoPlayer.GetVideoDurationAsync(file.Path.LocalPath);
            // Debug.WriteLine(file.Path.LocalPath + " duration: " + duration);
            var scrollOffset = 0.0;
            var scrollViewer = getScrollViewer();
            if (scrollViewer != null)
            {
                scrollOffset = scrollViewer.Offset.X;
            }
            long totalDuration = vm.Timeline.GetTotalDurationOfVideoChunks(); // TODO: Use pointer position and add at index instead
            (int index, long position) = vm.Timeline.GetVideoChunkAtPosition((long)(pointerPosition + scrollOffset));
            Debug.WriteLine($"Pointer position: {pointerPosition + scrollOffset}, Index: {index}, Position: {position}");
            VideoChunk videoChunk = vm.Timeline.AddVideoChunk(file.Name, file.Path, _zoomRatio);
            // VideoChunk videoChunk = vm.Timeline.AddVideoChunkAtIndex(file.Name, file.Path, index);
            Debug.WriteLine(videoChunk.Length);
            Rectangle rect = new Rectangle
            {
                Width = videoChunk.Length,
                Height = 150,
                Fill = Avalonia.Media.Brushes.Blue
            };
            //Video.Children.Add(rect);
        }
    }

    private void PointerWheelHandler(object? sender, PointerWheelEventArgs e)
    {
        if (sender is ScrollViewer sv)
        {
            Debug.WriteLine(sv.Offset);
        }
        var shift_pressed = e.KeyModifiers.HasFlag(KeyModifiers.Shift);
        var vertical_change = e.Delta.Y;
        // Debug.WriteLine("Shift pressed: " + shift_pressed);
        // Debug.WriteLine("Wheel changed: " + vertical_change);
        if (shift_pressed || vertical_change == 0)
        {
            return;
        }
        _zoomRatio += vertical_change * _zoomRatio * ZoomFactor;
        if (_zoomRatio < MinZoomRatio)
        {
            _zoomRatio = MinZoomRatio;
        }
        else if (_zoomRatio > MaxZoomRatio)
        {
            _zoomRatio = MaxZoomRatio;
        }
        if (DataContext is ViewModels.MainWindowViewModel vm)
        {
            vm.Timeline.ZoomRatio = _zoomRatio;
            vm.Timeline.OnZoomChanged();
        }
        // Debug.WriteLine("Zoom Ratio: " + _zoomRatio);
    }
}