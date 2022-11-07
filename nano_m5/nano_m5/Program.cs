using nanoFramework.M5Stack;
using Console = nanoFramework.M5Stack.Console;

namespace HelloWorld
{
    public class Program
    {
        public static void Main()
        {
            Setup();

            Console.WriteLine("HelloWorld");
        }

        private static void Setup()
        {
            M5Core2.InitializeScreen();
            Console.Clear();
            Console.ForegroundColor = nanoFramework.Presentation.Media.Color.White;
        }
    }
}