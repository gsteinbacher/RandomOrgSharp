using System;
using Obacher.RandomOrgSharp.Core.Parameter;

namespace Obacher.RandomOrgSharp.Core.Response
{
    public interface IResponseHandlerFactory
    {
        /// <summary>
        /// Execute code against the response returned from <see cref="IRandomService"/>
        /// </summary>
        /// <param name="parameters">Parameters passed into <see cref="IRandomService"/></param>
        /// <param name="response">Response returnred from <see cref="IRandomService"/></param>
        /// <returns>True is the process should continue to the next <see cref="IResponseHandler"/> in the list, false to stop processing response handlers</returns>
        bool Execute(IParameters parameters, string response);

        /// <summary>
        /// Get an instance of a <see cref="IResponseHandler"/>
        /// </summary>
        /// <param name="responseHandler">Type of <see cref="IResponseHandler"/> to return</param>
        /// <returns>Instance of <see cref="IResponseHandler"/>. Returns null if a <see cref="IResponseHandler"/> of specified type is not found</returns>
        IResponseHandler GetHandler(Type responseHandler);
    }
}