using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoostApp.Models
{
	class LogEvent
	{
		public int Id { get; set; }

		public DateTime Date { get; set; }

		public string Text { get; set; }

		public string Event { get; set; }
	}
}
