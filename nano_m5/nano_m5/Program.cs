using nanoFramework.M5Stack;
using System.Diagnostics;
using Console = nanoFramework.M5Stack.Console;

namespace HelloWorld
{
    public class Program
    {
        public static void Main()
        {
            Setup();
            Console.WriteLine("HelloWorld");
            while (true)
            {
                Loop();
                System.Threading.Thread.Sleep(50);
            }
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
        }
    }
}