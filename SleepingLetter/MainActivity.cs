﻿using System.Net;
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
    [Activity(Label = "자니?", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        EditText Letter;
        Button Sendbutton;
        Button Resultbutton;
        TextView ResultView;
        letter letter;
        
        Toast toast;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);
            // Set our view from the "main" layout resource
            Letter = FindViewById<EditText>(Resource.Id.textInputEditText1);
            Sendbutton = FindViewById<Button>(Resource.Id.sendLetter);
            Resultbutton = FindViewById<Button>(Resource.Id.Resultbutton);
            ResultView = FindViewById<TextView>(Resource.Id.ResultView);
            Sendbutton.Click += Sendbutton_ClickAsync;
            Resultbutton.Click += Resultbutton_ClickAsync;
            letter = new letter();

        }

        private async void Resultbutton_ClickAsync(object sender, EventArgs e)
        {
            try
            {
                letter.Message = Letter.Text;
                var req = new HttpRequestMessage();
                req.RequestUri = new Uri("http://61.82.138.72:12358/accept");



                
                var client = new HttpClient();
                var res = await client.GetAsync(req.RequestUri);
              
                req.RequestUri = new Uri("http://61.82.138.72:12358/2");
                ResultView.Text = "메세지를 보냈습니다.";
                Log.Debug("HTTP", res.Content.ReadAsStringAsync().ToString());
                if (res.StatusCode == HttpStatusCode.OK)
                {
                    toast = Toast.MakeText(this, "까였네? ^^", ToastLength.Short);
                    toast.Show();
                    res = await client.PutAsync(req.RequestUri, req.Content);
                }
                else if (res.StatusCode == HttpStatusCode.Created)
                {
                    toast = Toast.MakeText(this, "수락되었습니다.", ToastLength.Short);
                    toast.Show();
                    res = await client.PutAsync(req.RequestUri, req.Content);
                }
                else
                {
                    toast = Toast.MakeText(this, "들어온결과가 없습니다.", ToastLength.Short);
                    toast.Show();
                }
                /*switch (str)
                {
                    case 0:
                        toast = Toast.MakeText(this, "까였네? 병신", ToastLength.Long);
                        toast.Show();
                        res = await client.PutAsync(req.RequestUri, req.Content);
                        break;
                            case 1:
                        toast = Toast.MakeText(this, "수락되었습니다.", ToastLength.Long);
                        toast.Show();
                        res = await client.PutAsync(req.RequestUri, req.Content);
                        break;
                    default:
                        toast = Toast.MakeText(this, "들어온결과가 없습니다.", ToastLength.Long);
                        toast.Show();
                        break;
                }*/
            }
            catch (Exception ex)
            {
                toast = Toast.MakeText(this, "뭔가 문제가 생긴듯ㅎㅎ", ToastLength.Long);
                Log.Debug("Http", ex.Message);
            }

        }

        async void Sendbutton_ClickAsync(object sender, EventArgs e)
        {
            try
            {
                letter.Message = Letter.Text;
                var req = new HttpRequestMessage
                {
                    RequestUri = new Uri("http://61.82.138.72:12358"),
                    Content = new StringContent(JsonConvert.SerializeObject(letter), Encoding.UTF8, "application/json")
                };

                var client = new HttpClient();
                var res = await client.PutAsync(req.RequestUri, req.Content);
                ResultView.Text = "메세지를 보냈습니다.";
                Log.Debug("HTTP", res.Content.ReadAsStringAsync().ToString());
                toast = Toast.MakeText(this, "자니? 두근두근~", ToastLength.Long);
                toast.Show();
            }catch(Exception ex)
            {
                toast = Toast.MakeText(this, "뭔가 문제가 생긴듯ㅎㅎ", ToastLength.Long);
                Log.Debug("Http", ex.Message);
            }
              
          
        }
    }
}

