using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace MasterMind
{
    [Activity(Label = "Level")]
    public class Level : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.LayoutLevel);

            RadioButton buttonEasy = FindViewById<RadioButton>(Resource.Id.RadioEasy);
            RadioButton buttonHard = FindViewById<RadioButton>(Resource.Id.RadioHard);

            buttonEasy.Click += onRadioButtonEasyClicked;
            buttonHard.Click += onRadioButtonHardClicked;
        }

        public void onRadioButtonEasyClicked(object sender, EventArgs args) 
        {
            Intent PlayGame = new Intent(this, typeof(Game));
            PlayGame.PutExtra("Level", "easy");          
            this.StartActivity(PlayGame);
        }

        public void onRadioButtonHardClicked(object sender, EventArgs args)
        {
            Intent PlayGame = new Intent(this, typeof(Game));
            PlayGame.PutExtra("Level", "hard");
            this.StartActivity(PlayGame);
        }

    }
}