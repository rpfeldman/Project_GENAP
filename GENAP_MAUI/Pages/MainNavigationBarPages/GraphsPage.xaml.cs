using DataServices;

namespace GENAP_MAUI.Pages.MainNavigationBarPages;

public partial class GraphsPage : ContentPage
{
	public GraphsPage(DataProjectionService vm)
	{
		InitializeComponent();

		BindingContext = vm;
	}
}