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


    // TODO: Make hold seeking pause playback
    private void HandleSeekbarPress(object? sender, Avalonia.Controls.Primitives.RangeBaseValueChangedEventArgs e)
    {
        //Debug.WriteLine("Seekbar pressed at: " + SeekbarUser.Value);
        if (DataContext is ViewModels.MainWindowViewModel vm)
        {
            vm.VideoPlayer.JumpTo((long)(SeekbarUser.Value));
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
                await AddVideoBlockAsync(file);
            }
        }
    }

    public async Task AddVideoBlockAsync(IStorageItem file)
    {
        if (DataContext is ViewModels.MainWindowViewModel vm)
        {
            long duration = await vm.VideoPlayer.GetVideoDurationAsync(file.Path.LocalPath);
            vm.Timeline.AddVideoChunkScaled(file.Name, file.Path, 0, duration, (long) (duration * _zoomRatio));
            //Debug.WriteLine(file.Path.LocalPath + " duration: " + duration);
            Rectangle rect = new Rectangle
            {
                Width = duration,
                Height = 150,
                Fill = Avalonia.Media.Brushes.Blue
            };
            //Video.Children.Add(rect);
        }
    }

    private void PointerWheelHandler(object? sender, PointerWheelEventArgs e)
    {
        var shift_pressed = e.KeyModifiers.HasFlag(KeyModifiers.Shift);
        var vertical_change = e.Delta.Y;
        //Debug.WriteLine("Shift pressed: " + shift_pressed);
        //Debug.WriteLine("Wheel changed: " + change.Y);
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
        Debug.WriteLine("Zoom Ratio: " + _zoomRatio);
    }
}