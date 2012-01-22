using System;
using System.Linq;
using incant.FileSystem;

namespace incant
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			Console.WriteLine ("Hello World!");

            Copy c = new Copy();

            Console.WriteLine(c.Satisfied);

            c.Source = new File( "foo" );
            c.Destination = new File( "bar" );
            c.Overwrite = true;

            Console.WriteLine(c.Satisfied);
            
            Console.ReadLine();
		}
	}
}
