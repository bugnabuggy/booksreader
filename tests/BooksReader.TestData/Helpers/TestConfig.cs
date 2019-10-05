using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace BooksReader.TestData.Helpers
{
    public class TestConfig
    {
        public static string GetConfig(string key)
        {
            string val = ConfigurationManager.AppSettings["connection"];
            return val;
        }
    }
}
