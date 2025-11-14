using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using Avalonia.Platform.Storage;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Video_Editor.Views;

public partial class MediaLibraryView : UserControl
{
    public MediaLibraryView()
    {
        InitializeComponent();

        AddHandler(DragDrop.DragOverEvent, DragOver);
        AddHandler(DragDrop.DropEvent, Drop);
    }

    public void DragOver(object? sender, DragEventArgs e)
    {
        var currentPosition = e.GetPosition(this);

        var offsetX = currentPosition.X;
        var offsetY = currentPosition.Y;

        // Add visual feedback for dragging

        if (e.DataTransfer.Items is not null && e.DataTransfer.Items.Count > 0)
        {
            e.DragEffects = DragDropEffects.Copy;
        }
        else
        {
            e.DragEffects = DragDropEffects.None;
        }
    }
    private void Drop(object? sender, DragEventArgs e)
    {
        ViewModels.MainWindowViewModel vm = DataContext as ViewModels.MainWindowViewModel ?? throw new InvalidOperationException("DataContext is not MainWindowViewModel");
        if (e.DataTransfer.Items is not null)
        {
            foreach (IDataTransferItem item in e.DataTransfer.Items/*.SkipLast(1)*/)
            {
                Avalonia.Platform.Storage.IStorageItem? file = item.TryGetFile();
                if (file == null)
                {
                    Debug.WriteLine("Not a file or Format is not available.");
                    continue;
                }
                vm.MediaLibrary.AddMediaFromDragItem(file);
            }
        }
    }

    private void HandleDoubleTap(object? sender, TappedEventArgs e)
    {
        if (DataContext is ViewModels.MainWindowViewModel vm)
        {
            if (vm.MediaLibrary.SelectedMediaItem is not null)
            {
                //Debug.WriteLine("Playing selected media: " + vm.MediaLibrary.SelectedMediaItem.Name);
                string path = vm.MediaLibrary.SelectedMediaItem.Uri.LocalPath;
                vm.VideoPlayer.VideoPath = path;
                vm.VideoPlayer.Play(path);
            }
            else
            {
                Debug.WriteLine("No media item is selected.");
            }
        }
    }

    private async void OnPointerPressed(object? sender, PointerPressedEventArgs e)
    {
        var dragData = new DataTransfer();
        var result = await DragDrop.DoDragDropAsync(e, dragData, DragDropEffects.Move);
        Debug.WriteLine($"DragDrop result: {result}");
    }
}