﻿using Obacher.RandomOrgSharp.Core.Parameter;

namespace Obacher.RandomOrgSharp.Core.Response
{
    public interface IResponseHandler
    {
        /// <summary>
        /// Process the response 
        /// </summary>
        /// <param name="parameters">Parameters passed into the request object</param>
        /// <param name="responseInfo"></param>
        /// <returns>True if the process can continue to process subsequent handlers</returns>
        bool Process(IParameters parameters, IResponseInfo responseInfo);

        /// <summary>
        /// Should the <c>Process</c> method be called for the implementation of this interface
        /// </summary>
        /// <param name="parameters">Parameters passed into the request object</param>
        /// <returns>True if thie <c>Process</c> method should be called</returns>
        bool CanHandle(IParameters parameters);
    }
}