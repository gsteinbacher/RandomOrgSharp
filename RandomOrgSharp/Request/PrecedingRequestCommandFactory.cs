using System;
using System.Linq;
using Obacher.RandomOrgSharp.Core.Parameter;

namespace Obacher.RandomOrgSharp.Core.Request
{
    public class PrecedingRequestCommandFactory : IPrecedingRequestCommandFactory
    {
        private readonly IRequestCommand[] _requestCommand;

        public PrecedingRequestCommandFactory(params IRequestCommand[] requestCommand)
        {
            _requestCommand = requestCommand;
        }

        public bool Execute(IParameters parameters)
        {
            if (_requestCommand != null)
            {
                foreach (IRequestCommand handlers in _requestCommand)
                {
                    if (handlers.CanHandle(parameters))
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
