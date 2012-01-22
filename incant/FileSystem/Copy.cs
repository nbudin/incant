using System;
using incant;

namespace incant.FileSystem
{
	public class CopyParams 
	{
		public File source { get; private set; }
        public File destination { get; private set; }
		
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
			} catch (Exception) {
				return false;
			}
			
			return true;
		}
	}
}

