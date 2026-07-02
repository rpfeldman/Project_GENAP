using DataServices;
using Microsoft.Extensions.DependencyInjection;

namespace GENAP_MAUI
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }

        protected override async void OnStart()
        {
            base.OnStart();

            try
            {

            }
            catch (Exception x)
            {
                System.Diagnostics.Debug.WriteLine($"Seed failed: {x.Message}");
            }
        }
    }
}