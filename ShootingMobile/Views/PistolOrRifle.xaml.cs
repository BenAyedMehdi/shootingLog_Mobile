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
    public partial class PistolOrRifle : ContentPage
    {
        private List<Coardinate> coardinates;
        bool isCoardinate;
        bool isPistol;
        public PistolOrRifle(Object coardinates, bool isCoardinate)
        {
            InitializeComponent();
            this.coardinates = (List<Coardinate>)coardinates;
            this.isCoardinate = isCoardinate;

        }
        private async void Button_Clicked(object sender, EventArgs e)
        {
            isPistol = true;
            await Navigation.PushAsync(new ChartLocation(coardinates, isCoardinate,isPistol),true);
        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            isPistol = false;
            await Navigation.PushAsync(new ChartLocation(coardinates, isCoardinate, isPistol),true);
        }
    }
}