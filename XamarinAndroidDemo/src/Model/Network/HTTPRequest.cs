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
using Java.Net;
using Java.IO;
using Android.Util;

namespace XamarinAndroidDemo.src.Model.Network
{
    class HTTPRequest
    {
        public JSONObject postJson(string requestURL, JSONObject param)
        {
            string payload = "";
            try
            {
                URL url = new URL(requestURL);
                HttpURLConnection connection = (HttpURLConnection)url.OpenConnection();
                connection.DoInput = true;
                connection.DoOutput = true;
                connection.RequestMethod = "POST";
                connection.SetRequestProperty("Content-Type", "application/json");

                // Post data
                OutputStreamWriter os = new OutputStreamWriter(connection.OutputStream);
                Log.Debug("POST Conetent: ", param.ToString());
                os.Write(param.ToString());
                os.Flush();
                os.Close();

                // Read response
                BufferedReader br = new BufferedReader(new InputStreamReader(connection.InputStream));
                string line = "";
                while ((line = br.ReadLine()) != null)
                {
                    System.Console.WriteLine(line);
                    payload += line;
                }
                br.Close();
                connection.Disconnect();
            }  catch (Exception e)
            {
                Log.Debug("HTTPRequeset Exception: ", e.ToString());
            }

            return new JSONObject(payload);
        }
    }
}