using System;

namespace BooksReader.Core.Models.DTO
{
	public class LoginHistoryResult
	{
		public DateTime DateTime { get; set; }
		public string IpAddress { get; set; }
		public string Browser { get; set; }
		public string Geolocation { get; set; }
	}
}