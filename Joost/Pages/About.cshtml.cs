using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Joost.Pages
{
	public class AboutModel : PageModel
	{
		public string Message { get; set; }
		public DateTime Today = DateTime.Now;

		public void OnGet()
		{
			Message = "Your application description page.";
		}
	}
}
