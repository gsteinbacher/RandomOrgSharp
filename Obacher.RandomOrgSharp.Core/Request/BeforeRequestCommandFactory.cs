using System;
using System.Linq;
using Obacher.RandomOrgSharp.Core.Parameter;

namespace Obacher.RandomOrgSharp.Core.Request
{
    public class BeforeRequestCommandFactory : IBeforeRequestCommandFactory
    {
        private readonly IRequestCommand[] _requestCommand;

        public BeforeRequestCommandFactory(params IRequestCommand[] requestCommand)
        {
            _requestCommand = requestCommand;
        }

        public bool Execute(IParameters parameters)
        {
            if (_requestCommand != null)
            {
                foreach (IRequestCommand handlers in _requestCommand)
                {
                    if (handlers.CanProcess(parameters))
                    {
                        if (!handlers.Process(parameters))
                            return false;
                    }
                }
            }

            return true;
        }
    }
}
