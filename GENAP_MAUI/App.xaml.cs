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
                CategoryPersistenceService categoryPersistenService = IPlatformApplication.Current.Services.GetRequiredService<CategoryPersistenceService>();

                var anyCategoryOperation = await categoryPersistenService.HasCategories();

                // TO - DO: Apply a log system
                if (!anyCategoryOperation.Success)
                {
                    System.Diagnostics.Debug.WriteLine(anyCategoryOperation.ErrorMessage);
                }

                if (!anyCategoryOperation.Result)
                {
                    System.Diagnostics.Debug.WriteLine("no hay categorias xd");
                }
            }
            catch (Exception x)
            {
                System.Diagnostics.Debug.WriteLine($"Seed failed: {x.Message}");
            }
        }
    }
}