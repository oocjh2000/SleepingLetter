using System.Net;
using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using Newtonsoft.Json;
using System.Net.Http;
using System;
using System.Text;
using Android.Util;
using System.Threading;

namespace SleepingLetter
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        EditText Letter;
        Button Sendbutton;
        letter letter;
        
        Toast toast;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);
            // Set our view from the "main" layout resource
            Letter = FindViewById<EditText>(Resource.Id.textInputEditText1);
            Sendbutton = FindViewById<Button>(Resource.Id.sendLetter);
            Sendbutton.Click += Sendbutton_ClickAsync;
            letter = new letter();

        }

        

        async void Sendbutton_ClickAsync(object sender, EventArgs e)
        {
            letter.Message = Letter.Text;
            var req = new HttpRequestMessage
            {
                RequestUri = new Uri("http://192.168.56.1:12358"),
                Content = new StringContent(JsonConvert.SerializeObject(letter), Encoding.UTF8, "application/json")
            };

            var client = new HttpClient();
            var res = await client.PutAsync(req.RequestUri, req.Content);
            Log.Debug("HTTP", res.Content.ReadAsStringAsync().ToString());
              
          
        }
    }
}

