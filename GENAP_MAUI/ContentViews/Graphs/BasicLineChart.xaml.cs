using DomainModel;
using LiveChartsCore;
using LiveChartsCore.Kernel.Sketches;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using System.Globalization;

namespace GENAP_MAUI.ContentViews.Graphs;

public partial class BasicLineChart : ContentView
{
	private static readonly SKColor AxisTextColor = SKColor.Parse("#94A3B8");
	private static readonly SKColor GridLineColor = SKColor.Parse("#263241");

	private const int MinDaysVisible = 7;

	public static readonly BindableProperty TransactionsProperty = BindableProperty.Create(
		nameof(Transactions),
		typeof(List<TransactionDto>),
		typeof(BasicLineChart),
		new List<TransactionDto>(),
		propertyChanged: OnDataChanged);

	public List<TransactionDto> Transactions
	{
		get => (List<TransactionDto>)GetValue(TransactionsProperty);
		set => SetValue(TransactionsProperty, value);
	}

	public static readonly BindableProperty LineColorProperty = BindableProperty.Create(
		nameof(LineColor),
		typeof(Color),
		typeof(BasicLineChart),
		Colors.White,
		propertyChanged: OnDataChanged);

	public Color LineColor
	{
		get => (Color)GetValue(LineColorProperty);
		set => SetValue(LineColorProperty, value);
	}

	public ISeries[] LineSeriesCollection { get; private set; }
	public ICartesianAxis[] XAxes { get; private set; }
	public ICartesianAxis[] YAxes { get; }

	private DateOnly[] _dates = [];

	public BasicLineChart()
	{
		LineSeriesCollection =
		[
			CreateSeries([0d], ToSkColor(Colors.White))
		];

		XAxes =
		[
			new Axis { IsVisible = false }
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

		InitializeComponent();
	}

	private static void OnDataChanged(BindableObject bindable, object oldValue, object newValue)
	{
		var control = (BasicLineChart)bindable;
		control.UpdateChart();
	}

	private void UpdateChart()
	{
		var color = ToSkColor(LineColor);

		if (Transactions is null || Transactions.Count == 0)
		{
			_dates = [];
			LineSeriesCollection = [CreateSeries([0d], color)];
			XAxes = [new Axis { IsVisible = false }];
			OnPropertyChanged(nameof(LineSeriesCollection));
			OnPropertyChanged(nameof(XAxes));
			return;
		}

		var (dates, accumulatedValues) = AccumulateByDay(Transactions);
		_dates = dates;

		LineSeriesCollection = [CreateSeries(accumulatedValues, color)];
		XAxes = [BuildXAxis()];

		OnPropertyChanged(nameof(LineSeriesCollection));
		OnPropertyChanged(nameof(XAxes));
	}

	private static (DateOnly[] Dates, double[] Values) AccumulateByDay(List<TransactionDto> transactions)
	{
		var dailyTotal = transactions
			.GroupBy(t => t.Date)
			.OrderBy(g => g.Key)
			.ToDictionary(
				g => g.Key,
				g => g.Sum(t => t.Value));

		var from = dailyTotal.Keys.Min();
		var to = dailyTotal.Keys.Max();

		if (to.DayNumber - from.DayNumber < MinDaysVisible - 1)
		{
			from = to.AddDays(-(MinDaysVisible - 1));
		}

		var dates = new List<DateOnly>();
		var values = new List<double>();
		decimal running = 0;

		for (var day = from; day <= to; day = day.AddDays(1))
		{
			if (dailyTotal.TryGetValue(day, out var dayValue))
			{
				running += dayValue;
			}
			dates.Add(day);
			values.Add((double)running);
		}

		return (dates.ToArray(), values.ToArray());
	}

	private Axis BuildXAxis()
	{
		return new Axis
		{
			Labeler = value =>
			{
				var index = (int)Math.Round(value);
				if (index < 0 || index >= _dates.Length) return string.Empty;
				return _dates[index].ToString("dd/MM", CultureInfo.InvariantCulture);
			},
			TextSize = 11,
			LabelsPaint = new SolidColorPaint(AxisTextColor),
			MinStep = 1,
			SeparatorsPaint = null
		};
	}

	private LineSeries<double> CreateSeries(double[] values, SKColor color)
	{
		return new LineSeries<double>
		{
			Values = values,
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
			YToolTipLabelFormatter = point =>
			{
				var index = (int)Math.Round(point.Coordinate.SecondaryValue);
				var dateLabel = index >= 0 && index < _dates.Length
					? _dates[index].ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)
					: string.Empty;
				var valueLabel = point.Coordinate.PrimaryValue.ToString("N2", CultureInfo.InvariantCulture);
				return $"{dateLabel}\n{valueLabel}$";
			}
		};
	}

	private static SKColor ToSkColor(Color color)
	{
		return new SKColor(
			(byte)(color.Red * 255),
			(byte)(color.Green * 255),
			(byte)(color.Blue * 255),
			(byte)(color.Alpha * 255));
	}
}