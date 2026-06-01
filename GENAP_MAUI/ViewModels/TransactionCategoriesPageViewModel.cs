

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DataServices;
using DomainModel;
using GENAP_MAUI.InnerComponents;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Transactions;

namespace GENAP_MAUI.ViewModels
{
    public sealed partial class TransactionCategoriesPageViewModel : BaseViewModel
    {
        private DataProjectionService _dataProjectionService;
        private DataManagementService _dataManagementService;
        private GlobalResources _GR;

        public TransactionCategoriesPageViewModel(GlobalResources globalResources, DataProjectionService dataProjectionService, DataManagementService dataManagementService)
        {
            _dataProjectionService = dataProjectionService;
            _dataManagementService = dataManagementService;
            _GR = globalResources;

            Categories = new(_GR.GlobalCategories.Select(c => new CategoryDto(c.CategoryName, c.Color, c.CategoryId)));
        }

        [ObservableProperty]
        public partial ObservableCollection<CategoryDto> Categories { get; set; }

        public Dictionary<string, string> CategoryColors = new()
        {
            {"Rojo", "#E74C3C"},
            {"Verde", "#2ECC71"},
            {"Azul", "#3498DB"},
            {"Amarillo", "#F1C40F"},
            {"Naranja", "#E67E22"},
            {"Turquesa", "#1ABC9C"},
            {"Celeste", "#5DADE2"},
            {"Rosa", "#E84393"},
            {"Lima", "#A3CB38"}
        };

        public List<KeyValuePair<string, string>> ColorsList => [.. CategoryColors];

        [ObservableProperty]
        public partial KeyValuePair<string, string> PickedColor { get; set; }

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(AddCategoryCommand))]
        public partial string NewCategory { get; set; } = string.Empty;

        [RelayCommand]
        public async Task DeleteCategory(CategoryDto Category)
        {
            Categories.Remove(Category);
            SaveCommand.NotifyCanExecuteChanged();
        }

        [RelayCommand(CanExecute = nameof(AddCategoryCanExecute))]
        public async Task AddCategory()
        {
            Categories.Add(new CategoryDto(NewCategory, PickedColor.Value, _GR.GlobalCategories.Count));

			SaveCommand.NotifyCanExecuteChanged();
            NewCategory = string.Empty;
        }

        [RelayCommand(CanExecute = nameof(SaveCanExecute))]
        public async Task Save()
        {
            foreach (var item in Categories)
            {
                if (_GR.GlobalCategories.Where(c => c.CategoryId == item.CategoryId).Count() == 1 && _GR.GlobalCategories.Where(c => c.CategoryId == item.CategoryId).First().CategoryName != item.CategoryName)
                {
                    var OldName = _GR.GlobalCategories.Where(c => c.CategoryId == item.CategoryId).First().CategoryName;
                    var NewName = item.CategoryName;

                    await UpdateTransactionsCategories(OldName, NewName);
                }
            }

            _GR.GlobalCategories = new(Categories);

            NewCategory = string.Empty;

            await Shell.Current.DisplayAlertAsync("Categorias", "Se guardaron las categorias","Aceptar");
        }

        private async Task UpdateTransactionsCategories(string OldName, string NewName)
        {
            await _dataManagementService.RenameCategory(OldName, NewName);
        }

        private bool AddCategoryCanExecute() => !string.IsNullOrWhiteSpace(NewCategory) && Categories.Where(c => c.CategoryName == NewCategory).Count() == 0;
        private bool SaveCanExecute() => Categories.Count > 0;
    }
}
