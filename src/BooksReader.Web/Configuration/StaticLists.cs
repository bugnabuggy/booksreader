using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksReader.Web.Configuration
{
    public class StaticLists
    {
        public static Dictionary<Guid, DateTime> AntiforgeryKeys = new Dictionary<Guid, DateTime>();
    }
}
