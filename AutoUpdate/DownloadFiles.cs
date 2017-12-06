using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoUpdate
{
	public static class DownloadFiles
	{

		public static void Downloader(string urler, string kayityeri)
		{
			try
			{
				WebClient wc = new WebClient();
				wc.DownloadFile(urler, kayityeri);
			}
			catch (Exception)
			{


			}
		}


		public static bool download(string url)
		{
			try
			{
				System.IO.DirectoryInfo di = new DirectoryInfo(@"path");
				
				foreach (FileInfo file in di.GetFiles())
				{
					file.Delete();
				}
				foreach (DirectoryInfo dir in di.GetDirectories())
				{
					dir.Delete(true);
				}

				WebClient wc = new WebClient();
				Downloader(url + "EntityFramework.dll", @"C:\Program Files (x86)\XMLUtilities\TempUpdate\EntityFramework.dll");
				Downloader(url + "EntityFramework.xml", @"C:\Program Files (x86)\XMLUtilities\TempUpdate\EntityFramework.xml");
				Downloader(url + "XMLMailWF.application", @"C:\Program Files (x86)\XMLUtilities\TempUpdate\XMLMailWF.application");
				Downloader(url + "XMLMailWF.exe", @"C:\Program Files (x86)\XMLUtilities\TempUpdate\XMLMailWF.exe");
				Downloader(url + "XMLMailWF.exe.config", @"C:\Program Files (x86)\XMLUtilities\TempUpdate\XMLMailWF.exe.config");
				Downloader(url + "XMLMailWF.exe.manifest", @"C:\Program Files (x86)\XMLUtilities\TempUpdate\XMLMailWF.exe.manifest");
				Downloader(url + "XMLMailWF.pdb", @"C:\Program Files (x86)\XMLUtilities\TempUpdate\XMLMailWF.pdb");
				Downloader(url + "XMLMailWF.XmlSerializers.dll", @"C:\Program Files (x86)\XMLUtilities\TempUpdate\XMLMailWF.XmlSerializers.dll");
				if (!System.IO.Directory.Exists(@"C:\Program Files (x86)\XMLUtilities\TempUpdate\app.publish"))
				{
					System.IO.Directory.CreateDirectory(@"C:\Program Files (x86)\XMLUtilities\TempUpdate\app.publish");
				}
				Downloader(url + "app.publish\\XMLMailWF.exe", @"C:\Program Files (x86)\XMLUtilities\TempUpdate\app.publish\XMLMailWF.exe");
				return true;
			}
			catch (Exception ex)
			{

				throw;
			}

		}

	}
}
