﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using OAuthXFDemo.Droid.Services;
using OAuthXFDemo.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(ActivityService))]
namespace OAuthXFDemo.Droid.Services
{
    public class ActivityService : IActivityService
    {
        public void StartActivity()
        {
            //Forms.Context.StartActivity()
            //MainActivity activity = (MainActivity)Forms.Context;

            var intent = new Intent(Forms.Context, typeof(MainActivity));
            intent.SetFlags(ActivityFlags.ClearTop | ActivityFlags.SingleTop);
            Forms.Context.StartActivity(intent);
        }
    }
}