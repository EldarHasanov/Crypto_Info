using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using CryptoInfo.Models;

namespace CryptoInfoConsole 
{

    internal class Program
    {

        private const string data_url = @"http://api.coincap.io/v2";

        static void Main(string[] args)
        {
            var client = new HttpClient();

            var resurses = client.GetAsync(data_url + "/assets").Result;

            var str = resurses.Content.ReadAsStringAsync().Result;

            AssetsRootObjectWithList rootobject = JsonSerializer.Deserialize<AssetsRootObjectWithList>(str);
            var a = rootobject.data;
            var b = a[1];

            var resurs = client.GetAsync(data_url + "/assets/" + a[1].id).Result;

            var st = resurs.Content.ReadAsStringAsync().Result;

            AssetsRootObjectById root = JsonSerializer.Deserialize<AssetsRootObjectById>(st);
            var a1 = root.data;
            //var b1 = a[0];

            //Console.WriteLine(resurses);

            Console.WriteLine("Hello World!");
        }
    }
}