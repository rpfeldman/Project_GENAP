using GENAP_MAUI.ViewModels;
using LiveChartsCore;
using LiveChartsCore.Defaults;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;

namespace GENAP_MAUI.ContentViews.Graphs;

public partial class BalanceDoughnutChart : ContentView
{
    private static readonly SKColor IncomeColor = SKColor.Parse("#16C784");
    private static readonly SKColor ExpenseColor = SKColor.Parse("#EA3943");

    public static readonly BindableProperty IncomeProperty = BindableProperty.Create(
        nameof(Income),
        typeof(decimal),
        typeof(BalanceDoughnutChart),
        0m,
        propertyChanged: OnDataChanged);

    public decimal Income
    {
        get => (decimal)GetValue(IncomeProperty);
        set => SetValue(IncomeProperty, value);
    }

    public static readonly BindableProperty ExpensesProperty = BindableProperty.Create(
        nameof(Expenses),
        typeof(decimal),
        typeof(BalanceDoughnutChart),
        0m,
        propertyChanged: OnDataChanged);

    public decimal Expenses
    {
        get => (decimal)GetValue(ExpensesProperty);
        set => SetValue(ExpensesProperty, value);
    }

    public static readonly BindableProperty TitleProperty = BindableProperty.Create(
        nameof(Title),
        typeof(string),
        typeof(BalanceDoughnutChart),
        string.Empty,
        propertyChanged: OnTitleChanged);

    public string Title
    {
        get => (string)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    public ISeries[] DoughnutChart { get; }

    public BalanceDoughnutChart()
    {
        DoughnutChart = [
            new PieSeries<ObservableValue>
            {
                Name = "Ingresos",
                Values = [new(0)],
                InnerRadius = 80,
                Fill = new SolidColorPaint(IncomeColor),
                DataLabelsPaint = new SolidColorPaint(SKColors.White),
                DataLabelsPosition = LiveChartsCore.Measure.PolarLabelsPosition.Middle,
                DataLabelsFormatter = point => ChartFormat.CompactCurrency(point.Coordinate.PrimaryValue),
                ToolTipLabelFormatter = point => $"{point.Coordinate.PrimaryValue:N2}$"
            },
            new PieSeries<ObservableValue>
            {
                Name = "Gastos",
                Values = [new(0)],
                InnerRadius = 80,
                Fill = new SolidColorPaint(ExpenseColor),
                DataLabelsPaint = new SolidColorPaint(SKColors.White),
                DataLabelsPosition = LiveChartsCore.Measure.PolarLabelsPosition.Middle,
                DataLabelsFormatter = point => ChartFormat.CompactCurrency(point.Coordinate.PrimaryValue),
                ToolTipLabelFormatter = point => $"{point.Coordinate.PrimaryValue:N2}$"
            }
        ];

        InitializeComponent();
    }

    private static void OnDataChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var control = (BalanceDoughnutChart)bindable;
        control.UpdateChart();
    }

    private static void OnTitleChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var control = (BalanceDoughnutChart)bindable;
        if (control.TitleLabel is null) return;
        control.TitleLabel.Text = (string)newValue;
        control.TitleLabel.IsVisible = !string.IsNullOrWhiteSpace((string)newValue);
    }

    private void UpdateChart()
    {
        var balance = Income - Expenses;
        BalanceLabel.Text = $"Balance:\n{balance:N0}$";

        ((PieSeries<ObservableValue>)DoughnutChart[0]).Values = [new((double)Income)];
        ((PieSeries<ObservableValue>)DoughnutChart[1]).Values = [new((double)Expenses)];
    }
}