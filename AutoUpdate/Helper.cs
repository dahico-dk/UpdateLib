using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace AutoUpdate
{
	public static class Helper
	{
		public static void LogError(Exception ex)
		{
			WFException ext = new WFException();
			ext.innerexception = ex.InnerException != null ? ex.InnerException.ToString() : "";
			ext.msg = ex.Message != null ? ex.Message.ToString() : "";
			ext.src = ex.Source != null ? ex.Source.ToString() : "";
			ext.stacktrace = ex.StackTrace != null ? ex.StackTrace.ToString() : "";
			ext.targetsite = ex.TargetSite != null ? ex.TargetSite.ToString() : "";
			ext.MachineName = Environment.MachineName != null ? Environment.MachineName.ToString() : "";
			ext.UserName = Environment.UserName != null ? Environment.UserName.ToString() : "";
			MailAt("Güncelleme Servisi hata verdi", bodyYarat(ext));

		}

		public static void MailAt(string konu, string bodyz)
		{
			try
			{
				var fromAddress = new MailAddress("[MAIL-ADRESS]", "Update Service");
				var toAddress = new MailAddress("[TO-MAIL-ADRESS]");
				 string fromPassword = "[MAIL-PASS]";
				 string subject = "Auto Update Hata Mesajı";
				 string body = bodyz;
				var smtp = new SmtpClient
				{
					Host = "smtp.gmail.com",
					Port = 587,
					EnableSsl = true,
					DeliveryMethod = SmtpDeliveryMethod.Network,
					UseDefaultCredentials = false,
					Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
				};
				using (var message = new MailMessage(fromAddress, toAddress)
				{
					Subject = subject,
					Body = body,
					IsBodyHtml=true
				})
				{
					smtp.Send(message);
				}
			}
			catch (Exception ex)
			{
				
			}
		}

		public static string bodyYarat( WFException ex)
		{
			string p0 = "Bir update servisi hata verdi <br/>";
			string p1 = "Kullanıcı Adı: " + ex.UserName + "<br/>";
			string p2 = " Makina Adı:" + ex.MachineName + "<br/>";
			string p3 = " Inner Exception: " + ex.innerexception + "<br/>";
			string p4 = " HelpLink: " + ex.helplink + "<br/>";
			string p5 = " HREsult: " + ex.hresult + "<br/>";
			string p6 = " MSG: " + ex.msg + "<br/>";
			string p7 = "LogDate: " + DateTime.Now.ToString() + "<br/>";
			string p8 = "StackTrace: " + ex.stacktrace + "<br/>";
			return p0 + p1 + p2 + p3 + p4 + p5 + p6 + p7 + p8;
		}



	}
}
