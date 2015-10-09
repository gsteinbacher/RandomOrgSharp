using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Obacher.RandomOrgSharp
{
    public interface IRandom
    {
        int Next();
    }

   
    public class RandomNumberGenerator : IRandom
    {
        private static readonly Random _random;
        private static IRandom _randomNumberGenerator;

        public static IRandom Instance
        {
            get { return _randomNumberGenerator ?? (_randomNumberGenerator = new RandomNumberGenerator()); }
            set { _randomNumberGenerator = value; }
        }

        static RandomNumberGenerator()
        {
            _random = new Random();

        }
        public int Next()
        {
            return _random.Next();
        }
    }
}
