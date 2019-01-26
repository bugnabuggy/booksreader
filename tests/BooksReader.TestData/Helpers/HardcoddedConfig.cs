using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksReader.TestData.Helpers
{
    public class HardcoddedConfig
    {
        public const string ConnectionString = "Server=(localdb)\\mssqllocaldb;Database=br-test;Trusted_Connection=True;MultipleActiveResultSets=true";
        public const int AsyncOperationWaitTime = 10_000;
        public const string DefaultPassword = "Password@123";
    }
}
