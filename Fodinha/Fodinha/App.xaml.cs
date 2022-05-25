using Xamarin.Forms;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;

namespace Fodinha
{
    public partial class App : Application
    {
        public App ()
        {
            InitializeComponent();

            MainPage = new NavigationPage( new MainPage());
        }

        protected override void OnStart ()
        {
            AppCenter.Start("android={c321d70b-4b3c-46dc-93f4-ea31da4d40fe};" + "ios={84d182d8-3e14-4fe8-9fef-1fe3f2c080d8};", typeof(Analytics), typeof(Crashes));
        }

        protected override void OnSleep ()
        {
        }

        protected override void OnResume ()
        {
        }
    }
}
