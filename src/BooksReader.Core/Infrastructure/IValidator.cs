using System;
using System.Collections.Generic;
using System.Text;

namespace BooksReader.Core.Infrastructure
{
    public interface IValidator<T>
    {
        IEnumerable<string> Validate(T item);
    }
}
