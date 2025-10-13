using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Video_Editor.ViewModels
{
    public partial class SettingItemViewModel : ViewModelBase
    {
        [ObservableProperty]
        private string _name;

        [ObservableProperty]
        private string? _value;

        public SettingItemViewModel(string name)
        {
            _name = name;
        }
    }
}
