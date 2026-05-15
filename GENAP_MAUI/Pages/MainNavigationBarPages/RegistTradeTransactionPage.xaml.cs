using GENAP_MAUI.ViewModels;

namespace GENAP_MAUI.Pages.MainNavigationBarPages;

public partial class RegistTradeTransactionPage : ContentPage
{
	public RegistTradeTransactionPage(RegistTradeTransactionPageViewModel vm)
	{
		InitializeComponent();

		BindingContext = vm;
	}
}