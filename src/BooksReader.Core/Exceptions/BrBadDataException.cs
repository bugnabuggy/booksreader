using System;
using System.Collections.Generic;
using System.Text;

namespace BooksReader.Core.Exceptions
{
	public class BrBadDataException : Exception
	{
		public BrBadDataException()
		{
		}

		public BrBadDataException(string message) :base (message)
		{
		}
	}
}
