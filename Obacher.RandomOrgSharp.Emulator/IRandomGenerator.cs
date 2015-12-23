using System.Collections.Generic;

namespace Obacher.RandomOrgSharp.Emulator
{
    public interface IRandomGenerator<T>
    {
        IEnumerable<T> Generate();
    }
}