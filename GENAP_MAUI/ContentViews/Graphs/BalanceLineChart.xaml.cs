using LiveChartsCore;
using LiveChartsCore.Defaults;
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
    private static readonly SKColor PositiveColor = SKColor.Parse("#16C784");
    private static readonly SKColor NegativeColor = SKColor.Parse("#EA3943");

    private static readonly SKColor AxisTextColor = SKColor.Parse("#94A3B8");
    private static readonly SKColor GridLineColor = SKColor.Parse("#263241");
    private static readonly SKColor ZeroLineColor = SKColor.Parse("#CBD5E1");

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

    public ISeries[] LineSeriesCollection { get; private set; }
    public ICartesianAxis[] XAxes { get; }
    public ICartesianAxis[] YAxes { get; }
    public RectangularSection[] ZeroSection { get; }

    public BalanceLineChart()
    {
        LineSeriesCollection =
        [
            CreateSegmentSeries(
                [new ObservablePoint(0, 0)],
                PositiveColor)
        ];

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
                TextSize = 11,
                LabelsPaint = new SolidColorPaint(AxisTextColor),
                SeparatorsPaint = new SolidColorPaint(GridLineColor.WithAlpha(90))
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
                Stroke = new SolidColorPaint(ZeroLineColor.WithAlpha(120))
                {
                    StrokeThickness = 1,
                    PathEffect = new DashEffect([6, 6])
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
            LineSeriesCollection =
            [
                CreateSegmentSeries(
                    [new ObservablePoint(0, 0)],
                    PositiveColor)
            ];

            OnPropertyChanged(nameof(LineSeriesCollection));
            return;
        }

        var values = new double[BalanceHistory.Length];

        for (var i = 0; i < BalanceHistory.Length; i++)
        {
            values[i] = (double)BalanceHistory[i];
        }

        LineSeriesCollection = BuildColoredSegments(values).ToArray();

        OnPropertyChanged(nameof(LineSeriesCollection));
    }

    private static List<ISeries> BuildColoredSegments(double[] values)
    {
        var result = new List<ISeries>();

        if (values.Length == 1)
        {
            var color = values[0] >= 0 ? PositiveColor : NegativeColor;

            result.Add(CreateSegmentSeries(
                [new ObservablePoint(0, values[0])],
                color));

            return result;
        }

        var currentPoints = new List<ObservablePoint>
        {
            new(0, values[0])
        };

        var currentColor = values[0] >= 0 ? PositiveColor : NegativeColor;

        for (var i = 1; i < values.Length; i++)
        {
            var previousValue = values[i - 1];
            var currentValue = values[i];

            var previousIsPositive = previousValue >= 0;
            var currentIsPositive = currentValue >= 0;

            if (previousIsPositive == currentIsPositive)
            {
                currentPoints.Add(new ObservablePoint(i, currentValue));
                continue;
            }

            var crossingX = (i - 1) + ((0 - previousValue) / (currentValue - previousValue));
            var zeroPoint = new ObservablePoint(crossingX, 0);

            currentPoints.Add(zeroPoint);

            result.Add(CreateSegmentSeries(currentPoints, currentColor));

            currentColor = currentIsPositive ? PositiveColor : NegativeColor;

            currentPoints =
            [
                zeroPoint,
                new ObservablePoint(i, currentValue)
            ];
        }

        result.Add(CreateSegmentSeries(currentPoints, currentColor));

        return result;
    }

    private static LineSeries<ObservablePoint> CreateSegmentSeries(
        IEnumerable<ObservablePoint> values,
        SKColor color)
    {
        return new LineSeries<ObservablePoint>
        {
            Values = values.ToArray(),
            GeometrySize = 0,
            LineSmoothness = 0.35,
            Stroke = new SolidColorPaint(color)
            {
                StrokeThickness = 3
            },
            Fill = new LiveChartsCore.SkiaSharpView.Painting.LinearGradientPaint(
                new[]
                {
                    color.WithAlpha(70),
                    color.WithAlpha(18),
                    color.WithAlpha(0)
                },
                new SKPoint(0.5f, 0),
                new SKPoint(0.5f, 1)),
            GeometryFill = null,
            GeometryStroke = null,
            YToolTipLabelFormatter = point => $"{point.Coordinate.PrimaryValue:N2}$"
        };
    }
}