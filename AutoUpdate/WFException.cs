using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoUpdate
{
	public partial class WFException
	{
		public int ID { get; set; }
		public string msg { get; set; }
		public string stacktrace { get; set; }
		public string hresult { get; set; }
		public string innerexception { get; set; }
		public string src { get; set; }
		public string targetsite { get; set; }
		public string dta { get; set; }
		public string helplink { get; set; }
		public string MachineName { get; set; }
		public string UserName { get; set; }
	}
}
