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

namespace XamarinAndroidDemo.src.Model
{
    class SignIn
    {
        public bool simpleSignIn(string username, string passwd)
        {
            if(String.IsNullOrEmpty(username) || String.IsNullOrEmpty(passwd))
            {
                return false;
            }

            Console.WriteLine("username:" + username + " password: " + passwd);

            return false;
        }
    }
}