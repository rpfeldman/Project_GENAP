using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace GENAP_MAUI.InnerComponents
{
    public sealed partial class CategoryDto(string name) : ObservableObject
    {
        [ObservableProperty]
        public partial string CategoryName { get; set; } = name;
    }
}
