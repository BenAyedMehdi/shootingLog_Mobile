using ShootingMobile.Model;
using Syncfusion.SfChart.XForms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShootingMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChartLocation : ContentPage
    {
        List<Coardinate> regressionLine;
        private List<Coardinate> coardinates;
        float[] arr;
        bool isCoardinate;
        bool isPistol;
        public ChartLocation(Object coardinates, bool isCoardinate)
        {
            InitializeComponent();
            this.isCoardinate = isCoardinate;
            this.coardinates = (List<Coardinate>)coardinates;
            this.regressionLine = new List<Coardinate>();
            Start();
        }
        public ChartLocation(Object coardinates, bool isCoardinate, bool isPistol)
        {
            InitializeComponent();
            this.isPistol = isPistol;
            this.isCoardinate = isCoardinate;
            this.coardinates = (List<Coardinate>)coardinates;
            this.regressionLine = new List<Coardinate>();
            Start();
        }
        private void Start()
        {
            ChartZoomPanBehavior zoomPanBehavior = new ChartZoomPanBehavior();
            zoomPanBehavior.EnableZooming = true;
            chart.ChartBehaviors.Add(zoomPanBehavior);
            //chart.Behaviors.Add(zoomPanBehavior);
            DependencyService.Get<IMessage>().ShortAlert("Please press on 'Regression' if you want to plot the regression line");
            ViewChartAsync();
            arr = SlopeAndIntercept(this.coardinates);
            ArrayToLine(arr);
        }

        private void ArrayToLine(float[] arr)
        {
            if (isCoardinate)
            {
                regressionLine.Add(new Coardinate(-700,(arr[0] * (-700)) + arr[1]));
                regressionLine.Add(new Coardinate(700, (arr[0] * 700) + arr[1]));
            }
            else
            {
                regressionLine.Add(new Coardinate(0, arr[1]));
                regressionLine.Add(new Coardinate(12, (arr[0] * 12) + arr[1]));
            }
        }

        private  void ViewChartAsync()
        {
            if (isCoardinate)
            {

                PlotChart(isCoardinate, -1800, 1800, 4000);
            }
            else
            {
                PlotChart(isCoardinate, 0, 12, 1);
            }
        }

        private void PlotChart(bool isCoardinate, int min, int max, int range)
        {
            PlotAxes(min, max, range);
            
            if (isCoardinate)
            {
                PlotAnnotations();
                //PlotBubbleSeries();
                int size = isPistol ? 10 : 45;
                chart.Series.Add(new ScatterSeries()
                {
                    ItemsSource = coardinates,
                    XBindingPath = "x",
                    YBindingPath = "y",
                    Color = Color.Red,
                    ScatterWidth = size ,
                    ScatterHeight =size
                });
            }
            else
            {
                
                chart.Series.Add(new ColumnSeries()
                {
                    ItemsSource = coardinates,
                    XBindingPath = "x",
                    YBindingPath = "y",
                    DataMarker = new ChartDataMarker()
                    {
                        ShowLabel = false,
                        ShowMarker = true,
                        MarkerType = DataMarkerType.Ellipse,
                        MarkerColor = Color.Blue,
                    }
                });
            }
        }

        private void PlotBubbleSeries()
        {
            ObservableCollection<ChartDataPoint> data = new ObservableCollection<ChartDataPoint>();
            foreach (var item in coardinates)
            {
                data.Add(new ChartDataPoint(item.x, item.y, 20));
            }
            chart.Series.Add(new BubbleSeries()
            {
                ItemsSource = data
            }) ;
        }

        private void PlotAnnotations()
        {
            for (int i = 1; i < 6; i++)
            {
                chart.ChartAnnotations.Add(new EllipseAnnotation()
                {
                    X1 = -(i * 250) - 25,
                    Y1 = -(i * 250) -25,
                    X2 = i * 250 + 25,
                    Y2 = i * 250 + 25,
                });
                chart.ChartAnnotations.Add(new TextAnnotation()
                {
                    X1 = 0,
                    Y1 = (i * 250),
                    Text= (10-i).ToString()
                    
                }) ;
            }
        }

        private void PlotAxes(int min, int max, int range)
        {
            chart.PrimaryAxis = new NumericalAxis()
            {
                Minimum = min,
                Maximum = max,
                Interval = range,
                CrossesAt = 0
            };
            chart.SecondaryAxis = new NumericalAxis()
            {
                Minimum = min*2,
                Maximum = max*2,
                Interval = range*2,
                CrossesAt = 0
            };
        }

        private float[] SlopeAndIntercept(List<Coardinate> coardinates)
        {
            float xmean = 0;
            float ymean = 0;
            double x2mean = 0;
            float xymean = 0;


            for (int i = 0; i < coardinates.Count; i++)
            {
                xmean += coardinates.ElementAt(i).x;
                ymean += coardinates.ElementAt(i).y;
                x2mean += Math.Pow(coardinates.ElementAt(i).x, 2);
                xymean += coardinates.ElementAt(i).x * coardinates.ElementAt(i).y;

            }
            xmean = xmean / coardinates.Count;
            ymean = ymean / coardinates.Count;
            x2mean = x2mean / coardinates.Count;
            xymean = xymean / coardinates.Count;

            float a = (float)(((xmean * ymean) - xymean) / ((Math.Pow(xmean, 2) - x2mean)));
            float b = ymean - a * xmean;
            float[] arr = { a, b };
            return arr;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            regressionButton.IsEnabled = false;   
            chart.Series.Add(new LineSeries()
            {
                ItemsSource = regressionLine,
                XBindingPath = "x",
                YBindingPath = "y"
            });
        }

        async void Button_Clicked_1(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new GetPhoto(),true);
        }
    }
}