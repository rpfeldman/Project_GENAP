using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DataServices;
using DomainModel;
using GENAP_MAUI.InnerComponents;
using LiveChartsCore;
using LiveChartsCore.Defaults;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using Microcharts;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace GENAP_MAUI.ViewModels
{
	public sealed partial class GraphsPageViewModel : BaseViewModel
	{
		private DataProjectionService _dataProjectionService;
		private GlobalResources _globalResources;

		public GraphsPageViewModel(DataProjectionService dataProjectionService, GlobalResources globalResources)
		{
			_dataProjectionService = dataProjectionService;
			_globalResources = globalResources;

			Categories = new(_globalResources.GlobalCategories);

			Period = TimePeriods.Keys.First();
		}

		public enum TimePeriodsEnum {Historical, HistoricalToday, Month, ThirtyDays, ThreeMonths, Semester, Year };
		public Dictionary<TimePeriodsEnum, string> TimePeriods { get; } = new()
		{
			{TimePeriodsEnum.Historical, "Historico"},
			{TimePeriodsEnum.HistoricalToday, "Historico hasta hoy"},
			{TimePeriodsEnum.Month, "Este mes"},
			{TimePeriodsEnum.ThirtyDays, "Ultimos 30 dias"},
			{TimePeriodsEnum.ThreeMonths, "Ultimos 3 meses"},
			{TimePeriodsEnum.Semester, "Ultimo semestre"},
			{TimePeriodsEnum.Year, "Ultimo año"}
		};

		[ObservableProperty]
		public partial TimePeriodsEnum Period { get; set; }

		[ObservableProperty]
		public partial ObservableCollection<CategoryDto> Categories { get; set; }

        [ObservableProperty]
        public partial List<TransactionDto> ExpensesLog { get; set; } = [];

		[ObservableProperty]
		public partial List<TransactionDto> IncomeLog { get; set; } = [];

		[ObservableProperty]
		public partial List<TransactionDto> TransactionsLog { get; set; } = [];


		[RelayCommand]
        public async Task FillGraphs()
        {
			var GetExpensesTask = _dataProjectionService.GetAllAsync(true);
			var GetIncomeTask = _dataProjectionService.GetAllAsync(false);
			var GetTransactionsTask = _dataProjectionService.GetAllAsync();

			var TaskResults = await Task.WhenAll(GetExpensesTask, GetIncomeTask, GetTransactionsTask);

			ExpensesLog = new (TaskResults[0]);
			IncomeLog = new(TaskResults[1]);
			TransactionsLog = new(TaskResults[2]);

			return;
		}
    }
}
