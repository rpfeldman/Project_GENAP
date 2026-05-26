
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
                new CategoryDto("Indumentaria", "#466C87"),

                new CategoryDto("Comida", "#F5E727"),

                new CategoryDto("Social", "#43EB28"),

                new CategoryDto("Gaming", "#9028EB"),

                new CategoryDto("Suscripciones", "#28EBB7"),
            ];

		}
    }
}
