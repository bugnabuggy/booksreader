using System;
using System.Collections.Generic;
using System.Text;

namespace BooksReader.Validators.Getters
{
    public interface IGetter
    {
        object Get(Guid id, IServiceProvider provider);
    }
}
