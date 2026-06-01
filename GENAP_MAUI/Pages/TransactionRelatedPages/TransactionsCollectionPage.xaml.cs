using GENAP_MAUI.ViewModels;

namespace GENAP_MAUI.Pages.TransactionRelatedPages;

public partial class TransactionsCollectionPage : ContentPage
{
	public TransactionsCollectionPage(TransactionsCollectionPageViewModel vm)
	{
		InitializeComponent();

		BindingContext = vm;
	}
}