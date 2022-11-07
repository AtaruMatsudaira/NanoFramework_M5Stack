using nanoFramework.M5Stack;
using System.Threading;
using Console = nanoFramework.M5Stack.Console;
using nanoFramework.Networking;
using System.Diagnostics;
using System.Text;
using System.Net.Http;
using System.Net;
using System;
using nanoFramework.Json;
using System.Numerics;

namespace HelloWorld
{
    public class Program
    {
        const string uri = "http://192.168.151.82:5000/post";
        const string ssid = "aterm-dd6f6c-g";
        const string password = "06e2e7e4221d5";
        public static void Main()
        {
           
            Setup();
            ConnectWifi();

            while (true)
            {
                Loop();
            }

        }

        [Serializable]
        public class NineAxisData
        {
            public float[] acceleromet { get; set; }
            public float[] gyroscope { get; set; }
        }

        private static void Setup()
        {
            M5Core2.InitializeScreen();
            Console.Clear();
            Console.ForegroundColor = nanoFramework.Presentation.Media.Color.White;
        }

        private static void Loop()
        {
            var ag = M5Core2.AccelerometerGyroscope;
            ag.Calibrate(100);
            var acc = ag.GetAccelerometer();
            var gyr = ag.GetGyroscope();
            Console.Clear();
            Console.WriteLine($"Accelerometer data x:{acc.X} y:{acc.Y} z:{acc.Z}");
            Console.WriteLine($"Gyroscope data x:{gyr.X} y:{gyr.Y} z:{gyr.Z}\n");
            var data = new NineAxisData();
            data.acceleromet = Vec3toFloatArray(acc);
            data.gyroscope = Vec3toFloatArray(gyr);
            var postDataJson = JsonSerializer.SerializeObject(data,false);
            Debug.WriteLine(postDataJson);
            var content = new StringContent(postDataJson, Encoding.UTF8, "application/json");
            try
            {
                var request = new HttpClient().Post(uri, content);
            }catch(Exception e)
            {
                Console.WriteLine("Error!!!");
            }
        }
        private static float[] Vec3toFloatArray(Vector3 vector)
        {
            float[] returnValue = { (float)vector.X, (float)vector.Y, (float)vector.Z };
            return returnValue;
        }
        private static void ConnectWifi()
        {
            CancellationTokenSource cs = new(60000);

            //WifiÇ≈DHCPê⁄ë±
            var success = WifiNetworkHelper.ConnectDhcp(ssid, password, requiresDateTime: true, token: cs.Token);
            if (!success)
            {
                Debug.WriteLine($"Can't connect to the network, error: {WifiNetworkHelper.Status}");
                if (WifiNetworkHelper.HelperException != null)
                {
                    Debug.WriteLine($"ex: {WifiNetworkHelper.HelperException}");
                }
            }
            else
            {
                Debug.WriteLine("Wifi Connected!");
            
            }
            Console.WriteLine(success+"Yeah");
        }
    }
}