using System;
using incant;

namespace incant.FileSystem
{
	
	public class Copy: Command<bool>
	{
        [Parameter]
        public File Source { get; set; }

        [Parameter]
        public File Destination { get; set; }

        [Parameter(false)]
        public bool Overwrite { get; set; }

        public Copy ()
		{
            Overwrite = false;
		}

		public override bool execute() {
			try {
				    System.IO.File.Copy(Source.path, Destination.path, Overwrite);
			} catch (Exception) {
				return false;
			}
			
			return true;
		}
	}
}

