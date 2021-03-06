﻿using System;
using System.ComponentModel.DataAnnotations.Schema;
using BooksReader.Core.Models;

namespace BooksReader.Core.Entities
{
	public class LoginHistory: IIdentified
	{
		public Guid Id { get; set; }
		public Guid UserId { get; set; }
		public DateTimeOffset DateTimeOffset { get; set; }
		public string IpAddress { get; set; }
		public string Browser { get; set; }
		public string Screen { get; set; }
		public string Geolocation { get; set; }

        [ForeignKey("UserId")]
        public BrUser User { get; set; }
	}
}