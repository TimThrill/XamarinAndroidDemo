using Android.App;
using Android.Widget;
using Android.OS;
using System;
using Android.Gms.Common.Apis;
using Android.Gms.Common;
using Android.Gms.Plus;
using Android.Gms.Auth.Api.SignIn;
using Android.Gms.Auth.Api;
using Android.Support.V4.App;
using Android.Content;
using Android.Runtime;
using Android.Util;
using Android.Views;
using XamarinAndroidDemo.src.Model;

namespace XamarinAndroidDemo
{
    [Activity(Label = "XamarinAndroidDemo", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : FragmentActivity, GoogleApiClient.IOnConnectionFailedListener, View.IOnClickListener
    {
        private string TAG = "MainActivity";
        GoogleApiClient mGoogleApiClient;

        EditText et_username, et_passwd;
        Button bt_simpleLogin, bt_googleLogin;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);

            et_username = FindViewById<EditText>(Resource.Id.et_username);
            et_passwd = FindViewById<EditText>(Resource.Id.et_passwd);

            bt_simpleLogin = FindViewById<Button>(Resource.Id.bt_login);
            bt_googleLogin = FindViewById<Button>(Resource.Id.bt_Google_login);

            bt_simpleLogin.SetOnClickListener(this);
            bt_googleLogin.SetOnClickListener(this);

            // Configure sign-in to request the user's ID, email address, and basic
            // profile. ID and basic profile are included in DEFAULT_SIGN_IN.
            GoogleSignInOptions gso = new GoogleSignInOptions.Builder(GoogleSignInOptions.DefaultSignIn)
                                        .RequestIdToken(Resources.GetString(Resource.String.GoogleClientKey))
                                        .RequestEmail()
                                        .Build();
            // Build a GoogleApiClient with access to the Google Sign-In API and the
            // options specified by gso.
            mGoogleApiClient = new GoogleApiClient.Builder(this)
                .EnableAutoManage(this, this)
                .AddApi(Auth.GOOGLE_SIGN_IN_API, gso)
                .Build();
        }

        private void googleSignIn()
        {
            Intent intentSignIn = Auth.GoogleSignInApi.GetSignInIntent(mGoogleApiClient);
            StartActivityForResult(intentSignIn, 1);
        }

        public void OnConnectionFailed(ConnectionResult result)
        {
            AlertDialog.Builder alert = new AlertDialog.Builder(this);
            alert.SetTitle("Connection failed");
            alert.Show();
        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            Log.Debug(TAG, "Sign in result with result code: " + resultCode);
            base.OnActivityResult(requestCode, resultCode, data);
            if(requestCode == 1)
            {
                if(resultCode == Result.Ok)
                {
                    GoogleSignInResult res = Auth.GoogleSignInApi.GetSignInResultFromIntent(data);
                    if(res.IsSuccess)
                    {
                        GoogleSignInAccount acct = res.SignInAccount;
                        Log.Debug(TAG, "id:" + acct.Id + " tokenId:" + acct.IdToken);
                    } else
                    {
                        AlertDialog.Builder alg = new AlertDialog.Builder(this);
                        alg.SetTitle("Sign in failed");
                        alg.Show();
                    }
                } else
                {
                    AlertDialog.Builder alg = new AlertDialog.Builder(this);
                    alg.SetTitle("Sign in failed");
                    alg.Show();
                }
            }
        }

        public void OnClick(View v)
        {
            switch(v.Id)
            {
                case Resource.Id.bt_login:
                    SignIn simpleSignIn = new SignIn();
                    string username = et_username.Text;
                    string passwd = et_passwd.Text;
                    simpleSignIn.simpleSignIn(username, passwd);
                    break;
                case Resource.Id.bt_Google_login:
                    googleSignIn();
                    break;
                default:
                    break;
            }
        }
    }
}

