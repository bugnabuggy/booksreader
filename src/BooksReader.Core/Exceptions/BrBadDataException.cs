using System;
using System.Collections.Generic;
using System.Text;

namespace BooksReader.Core.Exceptions
{
	public class BrBadDataException : BaseBrException
    {
		public BrBadDataException()
		{
		}

		public BrBadDataException(string message) :base (message)
		{
		}
	}
}
