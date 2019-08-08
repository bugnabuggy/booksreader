using System;

namespace BooksReader.Validators.Getters
{
    public interface IGetter
    {
        object Get(Guid id, IServiceProvider provider);
    }
}