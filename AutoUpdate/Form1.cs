using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoUpdate
{
	public partial class Form1 : Form
	{
		//update path
		string url = ConfigurationManager.AppSettings["updatepath"];
		string process = ConfigurationManager.AppSettings["processpath"];
		string processname = ConfigurationManager.AppSettings["processname"];
		string installpath = ConfigurationManager.AppSettings["installpath"];
		string temppath = ConfigurationManager.AppSettings["temppath"];
		WebClient wc = new WebClient();
		public Form1()
		{
			//form gizle gorsele gerek yok
			this.Visible = false;
			//program açık ise processi yoket
			foreach (var process in Process.GetProcessesByName(processname))
			{
				process.Kill();
			}
			InitializeComponent();

		}		

		/// <summary>
		/// Mevcut versiyonun okunması ver karşılaştırılması
		/// </summary>
		/// <param name="path">versiyon dosyası yolu</param>
		///<param name="link">Server dosyası yolu</param>			
		bool readVersion(string path, string link)
		{
			try
			{  
				if (File.Exists(path))
				{
					string text = System.IO.File.ReadAllText(path);
					string[] lines = System.IO.File.ReadAllLines(path);
					string content = "";
					foreach (string line in lines) { content += line; }
					if (Convert.ToInt32(content) >= Convert.ToInt32(wc.DownloadString(link)))
					{
						return false;
					}
					else { return true; }
				}
				else { return true; }
			}
			catch (Exception)
			{
				return true;
			}
		}
		/// <summary>
		/// Verilen klasörün içeriğinin silinmesi
		/// </summary>
		/// <param name="path">Temizlenecek klasörün yolu</param>				
		/// <returns></returns>
		void Temizle(string path)
		{
			//dosyanın acılcagı yerı temızleme
			System.IO.DirectoryInfo di = new DirectoryInfo(path);
			foreach (FileInfo file in di.GetFiles())
			{
				file.Delete();
			}
			foreach (DirectoryInfo dir in di.GetDirectories())
			{
				dir.Delete(true);
			}
		}
		/// <summary>
		/// Klasör içeriğinin taşınması
		/// </summary>
		/// <param name="path">Eski klasör yolu</param>	
		/// <param name="newpath">Yeni klasör yolu</param>	
		/// <returns></returns>
		void MoveFiles(string path, string newpath)
		{
			try
			{
				//izinlerle ilgili sıkıngı yasamamak adına varolan klasoru sılıp yenıden yaratmak yerıne dosyaları tasıyorum
				System.IO.DirectoryInfo di = new DirectoryInfo(path);
				foreach (FileInfo file in di.GetFiles())
				{
					file.MoveTo(newpath);
				}
				if (Directory.Exists(path)) { Directory.Delete(path); }
			}
			catch (Exception ex)
			{
				Helper.LogError(ex);
				throw;
			}
		}
		//update zipinin açılması
		bool unZipFiles()
		{
			try
			{
				if (File.Exists(temppath)) { File.Delete(temppath + "\\update.zip"); }
				Temizle(installpath);
				ZipFile.ExtractToDirectory(temppath + "\\update.zip", installpath);
				//bütün dosyalar tasındı
				return true;
			}
			catch (Exception ex)
			{
				Helper.LogError(ex);
				return false;
			}
		}
		void ProgramBaslat()
		{
			try
			{
				Process.Start(process);				
				Application.Exit();
			}
			catch (Exception ex)
			{
				//bir kereye mahsus programın kendısı sılınmıs ama update.txtsilinmemis olabilir diye. Methodception
				try
				{
					Helper.LogError(ex);					
					Temizle(installpath);
					LoadMethod();
				}
				catch (Exception exz)
				{
					Helper.LogError(exz);
					throw;
				}
			}
		}		
		void LoadMethod()
		{
			try
			{												
				//bizdeki versiyon düşük mü? kurmaya gerek var mı?
				string uptext = installpath+@"\update.txt";
				string uptextserv = url + "/updatetext.txt";
				if (readVersion(uptext, uptextserv))//versiyon kaşılaştırması
				{
					//zip dosyası indirilmesi
					wc.DownloadFile(url + "update.zip", installpath+@"\update.zip");
					if (unZipFiles())
					{
						//program yuklendı.Update edildi. Versiyon yazılması
						wc.DownloadFile(url + "updatetext.txt", installpath);
						ProgramBaslat();
					}
					else
					{
						MessageBox.Show("Güncelleme sırasında hata oluştu.");
					}
				}
				else
				{
					//Program versiyonu dogru
					ProgramBaslat();
				}
			}
			catch (Exception ex)
			{
				Helper.LogError(ex);
				MessageBox.Show("Güncelleme sırasında hata oluştu.Lütfen bilgi işleme haber verin.");
			}
		}
		private void Form1_Load(object sender, EventArgs e)
		{			
			LoadMethod();
		}
	}
}
