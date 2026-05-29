
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
                new CategoryDto("Indumentaria", "#466C87", 0),

                new CategoryDto("Comida", "#F5E727", 1),

                new CategoryDto("Social", "#43EB28", 2),

                new CategoryDto("Gaming", "#9028EB", 3),

                new CategoryDto("Suscripciones", "#28EBB7", 4),
            ];

		}
    }
}
