using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace GENAP_MAUI.InnerComponents
{
    public sealed partial class CategoryDto(string name, string Color) : ObservableObject
    {
        [ObservableProperty]
        public partial string CategoryName { get; set; } = name;

		[ObservableProperty]
		public partial string Color { get; set; } = Color;
	}
}
