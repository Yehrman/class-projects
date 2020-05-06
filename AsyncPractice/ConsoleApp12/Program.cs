using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp12
{
    class Program
    {
        static void down1()
        {
            HttpClient client = new HttpClient();
            var a = client.GetStringAsync("http://classroom.compuskills.org/course/view.php?id=67").Result.Length;
            Console.WriteLine(a);
        }
        static void down2()
        {
            HttpClient http = new HttpClient();
            var b = http.GetStringAsync("https://www.torahanytime.com").Result.Length;
            Console.WriteLine(b);
        }
        public static async void Both()
        {
            Task a = Task.Run(() => down1());
            Task b = Task.Run(() => down2());
            await a;
            Console.WriteLine("compuskills downloaded");
            await b;
            Console.WriteLine("Torah Anytime downloaded");
        }
        static void Main(string[] args)
        {
           // down1();
            //down2();
           Both();
            Console.ReadKey();
        }
    }
}
