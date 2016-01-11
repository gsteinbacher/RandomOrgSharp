using System;
using Obacher.RandomOrgSharp.Core.Parameter;

namespace Obacher.RandomOrgSharp.Core.Request
{
    public interface IRequestCommandFactory
    {
        bool Execute(IParameters parameters);
    }
}
