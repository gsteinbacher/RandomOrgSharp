using System;

namespace Obacher.RandomOrgSharp.Core
{
    public interface IRandom
    {
        int Next();
    }

    public sealed class RandomNumberGenerator : IRandom
    {
        private static readonly Random _random;

        /// <summary>
        /// Singleton reference.  We want the reference to <see cref="RandomNumberGenerator" /> to be a Singleton so that the <see cref="Random"/> 
        /// class is only instantiated once.
        /// <remarks>
        /// An instance of the <see cref="RandomNumberGenerator"/> is always called, even if the Instance variable it set to a different class.
        /// I decided to take this approach to make the singleton thread safe, since the only time the Instance variable will be set to a different
        /// value is during unit testing.
        /// </remarks>
        /// </summary>
        public static IRandom Instance { get; set; } = new RandomNumberGenerator();

        static RandomNumberGenerator()
        {
            _random = new Random();
        }

        /// <summary>
        /// Return a random integer number
        /// </summary>
        /// <returns></returns>
        public int Next()
        {
            return _random.Next();
        }
    }
}
