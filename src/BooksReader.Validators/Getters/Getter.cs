using System;
using System.Linq;
using BooksReader.Core.Infrastructure;
using BooksReader.Core.Models;
using Microsoft.Extensions.DependencyInjection;

namespace BooksReader.Validators.Getters
{
    public class Getter<T> : IGetter where T : IIdentified
    {

        public Getter()
        {
        }

        public object Get(Guid id, IServiceProvider provider)
        {
            var repo = provider.GetRequiredService<IRepository<T>>();
            var item = repo.Data.FirstOrDefault(x => x.Id.Equals(id));
            return item;
        }
    }
}