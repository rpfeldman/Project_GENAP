
using GENAP_MAUI.InnerComponents;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace GENAP_MAUI
{
    public sealed class GlobalResources
    {
        public ObservableCollection<CategoryDto> GlobalCategories { get; set; } = new();

        public GlobalResources()
        {
            // This is temporary, GlobalCategories should get the categories from a JSON file
            GlobalCategories =
            [
                new CategoryDto("Indumentaria"),
                new CategoryDto("Comida"),
                new CategoryDto("Social"),
                new CategoryDto("Gaming"),
                new CategoryDto("Suscripciones"),
            ];
        }
    }
}
