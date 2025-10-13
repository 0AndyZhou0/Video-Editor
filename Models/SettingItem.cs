using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Video_Editor.Models
{
    internal class SettingItem
    {
        public required string Name { get; init; }
        public string? Value { get; set; }

        public SettingItem(string Name) {
            this.Name = Name;
        }

        public SettingItem(string Name, string Value) {
            this.Name = Name;
            this.Value = Value;
        }
    }
}
