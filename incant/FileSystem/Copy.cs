using System;
using incant;

namespace incant.FileSystem
{
	public class CopyParams 
	{
		private File source { public get; set; }
		private File destination { public get; set; }
		
		public CopyParams(File src, File dst) {
			this.source = src;
			this.destination = dst;
		}
	}
	
	public class Copy: ICommand<CopyParams, bool>
	{
		public Copy ()
		{
		}
		
		public bool execute(CopyParams args) {
			try {
				System.IO.File.Copy(args.source.path, args.destination.path);
			} catch (exception) {
				return false;
			}
			
			return true;
		}
	}
}

