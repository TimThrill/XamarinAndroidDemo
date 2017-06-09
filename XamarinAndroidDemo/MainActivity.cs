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

namespace XamarinAndroidDemo
{
    [Activity(Label = "XamarinAndroidDemo", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : FragmentActivity, GoogleApiClient.IOnConnectionFailedListener
    {
        GoogleApiClient mGoogleApiClient;

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

            GoogleSignInOptions gso = new GoogleSignInOptions.Builder(GoogleSignInOptions.DefaultSignIn)
                                        .RequestIdToken("366818564701-1a3lcebf7g52t0fh23uumrppe0jn2dbu.apps.googleusercontent.com")
                                        .Build();
            mGoogleApiClient = new GoogleApiClient.Builder(this)
                .EnableAutoManage(this, this)
                .AddApi(Auth.GOOGLE_SIGN_IN_API, gso)
                .Build();
        }

        private void Login_Click(object sender, System.EventArgs e)
        {
            signIn();
        }

        private void signIn()
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
            base.OnActivityResult(requestCode, resultCode, data);
            if(requestCode == 1)
            {
                if(resultCode == Result.Ok)
                {
                    GoogleSignInResult res = Auth.GoogleSignInApi.GetSignInResultFromIntent(data);
                    if(res.IsSuccess)
                    {
                        GoogleSignInAccount acct = res.SignInAccount;
                        AlertDialog.Builder alg = new AlertDialog.Builder(this);
                        alg.SetTitle(acct.Email);
                        alg.Show();
                    } else
                    {
                        AlertDialog.Builder alg = new AlertDialog.Builder(this);
                        alg.SetTitle("Sign in failed");
                        alg.Show();
                    }
                } else
                {

                }
            }
        }
    }
}

