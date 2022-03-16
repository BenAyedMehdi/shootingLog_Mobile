using ShootingMobile.Model;
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
    public partial class ResultsFromCoardintes : ContentPage
    {
        List<Coardinate> coardinates;
        public ResultsFromCoardintes(Object coordinates)
        {
            InitializeComponent();
            this.coardinates = (List<Coardinate>)coordinates;
            Read();
        }

        private void Read()
        {
            //List<float> results = new List<float>();
            float distance;
            float result;
            string msg = "";
            foreach (var item in coardinates)
            {
                distance = (float)Math.Sqrt(Math.Pow(item.x, 2) + Math.Pow(item.y, 2));
                result = (float)(10.9 - (((int)distance -1) / 25) * 0.1);
                msg += "result: " + result.ToString() + " ; distance in mm: " + distance/100.0 + "mm\n";
            }
            label.Text = msg;
        }

        private async void plotButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PistolOrRifle(coardinates, true), true);
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new GetPhoto(), true);
        }

    }
}