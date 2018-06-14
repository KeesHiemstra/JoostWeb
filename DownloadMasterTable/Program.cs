using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

/// <summary>
/// Download the event table from the database. The data is saved as an Json file on a OneDrive folder by default.
/// </summary>
namespace DownloadMasterTable
{
	class Program
	{
		public static object Journals { get; private set; }
		public static EventTable Events = new EventTable();

		static void Main(string[] args)
		{
			Console.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")} Started process");
			Events.Event = new List<string>();
			Events.ComputerName = Environment.GetEnvironmentVariable("COMPUTERNAME");
			#region Preapare folder
			string DownloadFolder = $"{Environment.GetEnvironmentVariable("OneDrive")}\\Transfer\\JoostWeb\\" +
				$"{Environment.GetEnvironmentVariable("COMPUTERNAME")}\\" +
				$"{Environment.GetEnvironmentVariable("USERNAME")}";
			if (!Directory.Exists(DownloadFolder))
			{
				Console.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")} Create folder: {DownloadFolder}");
				Directory.CreateDirectory(DownloadFolder);
			}
			#endregion

			string DownloadFile = $"{DownloadFolder}\\Events_{DateTime.Now.ToString("yyyy-MM-dd HHmmss")}.json";
			Console.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")} Download filename: {DownloadFile}");

			using (var db = new JournalDbContext())
			{
				var EventItems = db.Journals
					.Where(x => !string.IsNullOrEmpty(x.Event))
					.Select(x => x.Event)
					.Distinct()
					.OrderBy(x => x)
					.ToList();

				Console.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")} Started list events");
				foreach (var item in EventItems)
				{
					Console.WriteLine($">> {item}");
					Events.Event.Add(item);
				}
				Console.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")} Completed list events");

				var json = new DataContractJsonSerializer(typeof(EventTable));
				using (FileStream stream = new FileStream(DownloadFile, FileMode.CreateNew))
				{
					json.WriteObject(stream, Events);
				}
			}

			if (File.Exists(DownloadFile))
			{
				Console.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")} File is created successful");
			}
			else
			{
				Console.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")} File is NOT created");
			}

			Console.WriteLine();
			Console.Write("Press any key");
			Console.ReadKey();
		}
	}//Programm

	#region Events table
	public class EventTable
	{
		public string ComputerName { get; set; }
		public IList<string> Event { get; set; }
	}
	#endregion

	#region Journal table connection
	public class JournalDbContext : DbContext
	{
		public JournalDbContext() : base("Trusted_Connection=True;Data Source=(Local);Database=Joost;MultipleActiveResultSets=true") { }

		public DbSet<Journal> Journals { get; set; }

		//internal static void SeedData(JournalDbContext context)
		//{
		//}
	}
	#endregion

	#region Journal model
	[Table("Journal")]
	public class Journal
	{
		[Key]
		[Required]
		[Display(Name = "ID")]
		public int LogID { get; set; }

		[Display(Name = "Date")]
		[DataType(DataType.DateTime)]
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-ddTHH:mm:ss}", ApplyFormatInEditMode = true)]
		public DateTime? DTStart { get; set; }

		[Required]
		[StringLength(512)]
		public string Message { get; set; }

		[StringLength(40)]
		[DisplayFormat(NullDisplayText = "<No event>")]
		public string Event { get; set; }

		//[Required]
		//[Editable(false)]
		[Display(Name = "Create date")]
		[DataType(DataType.DateTime)]
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}", ApplyFormatInEditMode = false)]
		//[DefaultValue(DateTime, DateTime.Now.ToString())]
		public DateTime? DTCreation { get; set; }

		[ConcurrencyCheck]
		[Timestamp]
		public byte[] RowVersion { get; set; }
	}
	#endregion
}
