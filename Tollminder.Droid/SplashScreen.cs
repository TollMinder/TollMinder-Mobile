using Android.App;
using Android.Content.PM;
using Cirrious.MvvmCross.Droid.Views;

namespace Tollminder.Droid
{
	[Activity (Label = "Tollminder.Droid", MainLauncher = true, Icon = "@mipmap/icon", Theme = "@style/Theme.Splash", NoHistory = true, ScreenOrientation = ScreenOrientation.Portrait)]
	public class SplashScreen : MvxSplashScreenActivity
	{
		public SplashScreen () : base (Resource.Layout.SplashScreen)
		{
		}

		protected override void OnCreate (Bundle savedInstanceState)
		{
			Xamarin.Insights.Initialize (global::Tollminder.Droid.XamarinInsights.ApiKey, this);
			base.OnCreate (savedInstanceState);
		}
	}
}
