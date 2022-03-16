using ShootingMobile.Model;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShootingMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReadImage : ContentPage
    {
        Extractor extractor;
        SKBitmap roi;
        List<Coardinate> coardinates;
        bool isCoardinate;
        public ReadImage(SKBitmap roi)
        {
            this.roi = roi;
            this.isCoardinate = BitmapMethods.isCoardinates(roi);
            extractor = new Extractor(roi);
            InitializeComponent();
            DependencyService.Get<IMessage>().ShortAlert("Here are the numeric results, press 'Plot' if you want to visualize them");
            Read();
        }

        private void Read()
        {
            coardinates = isCoardinate ?
                extractor.Coardinates() : extractor.Results();
            if (coardinates!=null)
            {
                string s = coardinates[0].ToString() + "\n\n";
                for (int i = 1; i < coardinates.Count; i++)
                {
                    s += coardinates[i].ToString() + "\n";
                }
                label.Text = s;
            }
            else
            {
                label.Text = "Sorry! \nWe couldn't extract the numeric values.\nPlease press 'Cancel' to try again";
                plotButton.IsEnabled = false;
            }
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            if (isCoardinate)
            {
                await Navigation.PushAsync(new ResultsFromCoardintes( coardinates ),true);
            }
            else
            {
                await Navigation.PushAsync(new ChartLocation(coardinates, isCoardinate),true);
            }
        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new GetPhoto(),true);
        }
    }
}