using LiveChartsCore;
using LiveChartsCore.Kernel.Sketches;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Drawing;
using LiveChartsCore.SkiaSharpView.Maui;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView.Painting.Effects;
using SkiaSharp;

namespace GENAP_MAUI.ContentViews.Graphs;

public partial class BalanceLineChart : ContentView
{
    private static readonly SKColor PositiveColor = SKColor.Parse("#1EFF03");
    private static readonly SKColor NegativeColor = SKColor.Parse("#FF0303");

    public static readonly BindableProperty BalanceHistoryProperty = BindableProperty.Create(
        nameof(BalanceHistory),
        typeof(decimal[]),
        typeof(BalanceLineChart),
        Array.Empty<decimal>(),
        propertyChanged: OnDataChanged);

    public decimal[] BalanceHistory
    {
        get => (decimal[])GetValue(BalanceHistoryProperty);
        set => SetValue(BalanceHistoryProperty, value);
    }

    public ISeries[] LineSeriesCollection { get; }
    public ICartesianAxis[] XAxes { get; }
    public ICartesianAxis[] YAxes { get; }
    public RectangularSection[] ZeroSection { get; }

    private readonly LineSeries<double> _balanceSeries;
    public BalanceLineChart()
	{
        _balanceSeries = new LineSeries<double>
        {
            Values = new double[] { 0 },
            GeometrySize = 0,
            LineSmoothness = 0.5,
            Stroke = new SolidColorPaint(PositiveColor) { StrokeThickness = 3 },
            Fill = new LiveChartsCore.SkiaSharpView.Painting.LinearGradientPaint(
                    new[]
                    {
                    PositiveColor.WithAlpha(90),
                    PositiveColor.WithAlpha(0)
                    },
                    new SKPoint(0.5f, 0),
                    new SKPoint(0.5f, 1)),
            YToolTipLabelFormatter = point => $"{point.Coordinate.PrimaryValue:N2}$"
        };

        LineSeriesCollection = [_balanceSeries];

        XAxes =
        [
            new Axis
            {
                IsVisible = false
            }
        ];

        YAxes =
        [
            new Axis
            {
                Labeler = value => $"{value:N0}$",
                TextSize = 12,
                LabelsPaint = new SolidColorPaint(SKColors.White),
                SeparatorsPaint = new SolidColorPaint(SKColors.White.WithAlpha(30))
                {
                    StrokeThickness = 1
                }
            }
        ];

        ZeroSection =
        [
            new RectangularSection
            {
                Yi = 0,
                Yj = 0,
                Stroke = new SolidColorPaint(SKColors.White.WithAlpha(120))
                {
                    StrokeThickness = 1,
                    PathEffect = new DashEffect([4, 4])
                }
            }
        ];

        InitializeComponent();
	}

    private static void OnDataChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var control = (BalanceLineChart)bindable;
        control.UpdateChart();
    }

    private void UpdateChart()
    {
        if (BalanceHistory is null || BalanceHistory.Length == 0)
        {
            _balanceSeries.Values = new double[] { 0 };
            return;
        }

        var doubleValues = new double[BalanceHistory.Length];
        for (var i = 0; i < BalanceHistory.Length; i++)
        {
            doubleValues[i] = (double)BalanceHistory[i];
        }

        _balanceSeries.Values = doubleValues;

        var lastValue = BalanceHistory[^1];
        var color = lastValue >= 0 ? PositiveColor : NegativeColor;

        _balanceSeries.Stroke = new SolidColorPaint(color) { StrokeThickness = 3 };
        _balanceSeries.Fill = new LiveChartsCore.SkiaSharpView.Painting.LinearGradientPaint(
            new[]
            {
                color.WithAlpha(90),
                color.WithAlpha(0)
            },
            new SKPoint(0.5f, 0),
            new SKPoint(0.5f, 1));
    }
}