using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace AndroidApp
{
  [Activity(Label = "Приложение на Xamarin", MainLauncher = true, Icon = "@drawable/icon")]
  public class MainActivity : Activity
  {
    int count = 1;

    protected override void OnCreate(Bundle bundle)
    {
      base.OnCreate(bundle);

      // Set our view from the "main" layout resource
      SetContentView(Resource.Layout.Main);

      // Get our button from the layout resource,
      // and attach an event to it
      {
        var button = FindViewById<Button>(Resource.Id.MyButton);
        button.Click += (s, e) => button.Text = $"{count++} Кликни меня";
        //button.TextColors.
      }
 
      FindViewById<Button>(Resource.Id.buttonQuit).Click += (s, e) => Process.KillProcess(Process.MyPid());
      

    }
  }
}

