using DataServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace GENAP_MAUI.ViewModels
{
    public sealed partial class GraphsPageViewModel(DataProjectionService dataProjectionService) : BaseViewModel
    {
        private DataProjectionService _DataProjectionService = dataProjectionService;
    }
}
