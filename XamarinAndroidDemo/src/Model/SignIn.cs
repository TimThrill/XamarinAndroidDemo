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
using Org.Json;
using XamarinAndroidDemo.src.Model.Network;

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
            JSONObject postParam = new JSONObject();
            postParam.Put("username", username);
            postParam.Put("password", passwd);

            HTTPRequest rq = new HTTPRequest();
            rq.postJson("http://localhost:11618/api/values/signIn/", postParam);
            return false;
        }
    }
}