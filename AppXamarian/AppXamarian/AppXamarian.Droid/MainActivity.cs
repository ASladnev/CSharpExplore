using Android.App;
using Android.Widget;
using Android.OS;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AppXamarian.Droid
{
	[Activity (Label = "Xamarin forms explorer!!!", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			// Get our button from the layout resource,
			// and attach an event to it
			var buttonStart = FindViewById<Button> (Resource.Id.buttonStart);
      var buttonStop = FindViewById<Button>(Resource.Id.buttonStop);
      var buttonUp = FindViewById<Button>(Resource.Id.buttonUp);
      var buttonDown = FindViewById<Button>(Resource.Id.buttonDown);
      var buttonQuit = FindViewById<Button>(Resource.Id.buttonQuit);
      var textViewSpeed = FindViewById<TextView>(Resource.Id.textViewSpeed);
      var textViewMark = FindViewById<TextView>(Resource.Id.textViewMark);
      

      var result = 0;
      var working = false;
      var speed = 1000;
      var waiting = 0;


      Action<int> SetTextViewSpeed = (sp) =>
      {
        if (sp == 0) sp = 1;
        speed = sp;
        textViewSpeed.Text = " Вялость:" + speed;
        if (!working) {
          textViewMark.Text = "";
          return;
        }
        if (speed > 2000) textViewMark.Text = "Listen me! You're a fucking DOWNSHIFTER from the fucking downshifting country! ";
        else if (speed > 200) textViewMark.Text = "You have to work more quickly! Gotcha me, bitch?";
        else textViewMark.Text = "You're working well enough! But I should reduce your salary!";
      };


      Action Init = () =>
      {
        buttonDown.Enabled = buttonUp.Enabled = buttonStop.Enabled = false;
        SetTextViewSpeed(1000);
      };

      Action Start = () =>
      {
        working = true;
        buttonStart.Enabled = false;
        buttonDown.Enabled = buttonUp.Enabled = buttonStop.Enabled = true;
        SetTextViewSpeed(speed);
      };

      Action Stop = () =>
      {
        if (!working) return;
        working = false;
        buttonDown.Enabled = buttonUp.Enabled = buttonStart.Enabled = true;
        buttonStop.Enabled = false;
        textViewMark.Text = "";
      };


      buttonStart.Click += async (s, a) => {
        if (working) return;
        Start();
        while (working) {
          await Task.Run(() => {
            if (waiting >= speed) {
              buttonStart.Post(() => buttonStart.Text = $"Заработано ${++result}");
              waiting = 0;
            }

            waiting += 10;
            Thread.Sleep(10);

          });
        }
      };

      buttonStop.Click += (s, a) =>  Stop();

      buttonDown.Click += (s, a) => SetTextViewSpeed(speed * 2);
 
      buttonUp.Click += (s, a) => SetTextViewSpeed(speed / 2);

      buttonQuit.Click += (s, a) => Process.KillProcess(Process.MyPid());

      var grid = FindViewById<GridLayout>(Resource.Id.gridLayout);

      Init();
		}


    


	}
}


