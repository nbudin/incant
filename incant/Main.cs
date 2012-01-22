using System;
using System.Linq;
using incant.FileSystem;
using incant.HTTP;
using System.Collections.Generic;
using System.IO;

namespace incant
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			Console.WriteLine ("Hello World!");

            Resource google = new Resource(new Uri("http://google.com"));
			System.Net.WebResponse resp = google.Request("GET", "/", new Dictionary<string, string>(), new Dictionary<string, string>());
			Console.Write(new StreamReader(resp.GetResponseStream()).ReadToEnd());
			
            Console.ReadLine();
		}
	}
}
