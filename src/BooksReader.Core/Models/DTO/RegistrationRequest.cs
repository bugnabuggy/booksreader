using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksReader.Infrastructure.Models.DTO
{
	public class RegistrationRequest
	{
		public string AntiforgeryKey { get; set; }
		public string Username { get; set; }
		public string Password { get; set; }
	}
}
