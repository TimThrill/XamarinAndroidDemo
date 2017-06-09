using Android.App;
using Android.Widget;
using Android.OS;
using System;

namespace XamarinAndroidDemo
{
    [Activity(Label = "XamarinAndroidDemo", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);

            EditText username = FindViewById<EditText>(Resource.Id.et_username);
            EditText passwd = FindViewById<EditText>(Resource.Id.et_passwd);

            Button login = FindViewById<Button>(Resource.Id.bt_login);
            Button signup = FindViewById<Button>(Resource.Id.bt_sign_up);

            login.Click += Login_Click;
        }

        private void Login_Click(object sender, System.EventArgs e)
        {
            AlertDialog.Builder alert = new AlertDialog.Builder(this);
            alert.SetTitle("Hi, how are you");
            alert.Show();
        }
    }
}

