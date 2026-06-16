using GENAP_MAUI.Pages.MainNavigationBarPages;
using GENAP_MAUI.Pages.TransactionRelatedPages;

namespace GENAP_MAUI
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(Routes.TransactionMenu, typeof(TransactionPage));
        }
    }
}
