using ShootingMobile.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShootingMobile
{
    public partial class App : Application
    {
        public App()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NTIwOTIwQDMxMzkyZTMzMmUzMEJDVkZIRHByL0VWbythdzlObk5ROWlUOVhUTGRsRUFuQVJvS0V5TzFmcW89");
            DevExpress.XamarinForms.Charts.Initializer.Init();
            InitializeComponent();

            MainPage =new NavigationPage(new GetPhoto());
        }
        
        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
