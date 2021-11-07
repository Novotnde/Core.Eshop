using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Api.Tests.Utils
{
    internal static class IdGenerator
    {
        internal static string GenerateIdForNotFound()
        {
            Random rand = new Random();
            int number = rand.Next(1000, 100000); 
            return number.ToString();
        }

       internal static string GenerateIdFromSeedData()
        {
            Random rand = new Random();
            int number = rand.Next(1, 3);
            return number.ToString();
        }
    }
}
