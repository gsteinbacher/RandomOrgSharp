﻿using System.ComponentModel;
using System.Linq;
using Newtonsoft.Json.Linq;
using Obacher.RandomOrgSharp.Core;
using Obacher.RandomOrgSharp.Core.Parameter;
using Obacher.RandomOrgSharp.Core.Response;

namespace Obacher.RandomOrgSharp.JsonRPC.Response
{
    /// <summary>
    /// Error handler which throws an exception when an error occurs
    /// </summary>
    public class ErrorHandler : IErrorHandler, IResponseHandler
    {
        private readonly IResponseParser _errorParser;

        public ErrorResponseInfo ErrorInfo { get; private set; }

        private bool _hasError;

        /// <summary>
        /// Constructor for <see cref="ErrorHandlerThrowException"/>
        /// </summary>
        /// <param name="errorParser">Parser to parse the json containing the error information</param>
        public ErrorHandler(IResponseParser errorParser)
        {
            _errorParser = errorParser;
        }

        public bool HasError()
        {
            return _hasError;
        }


        public bool Handle(IParameters parameters, string response)
        {
            _hasError = false;

            if (_errorParser.CanParse(parameters))
            {
                ErrorInfo = _errorParser.Parse(response) as ErrorResponseInfo;

                if (ErrorInfo?.Code > 0)
                {
                    _hasError = true;
                }
            }

            // This return value is indicating whether errors occurred while running the error parsing process, 
            // not whether errors were returned from the IRandomService call, 
            return true;
        }

        /// <summary>
        /// Every method call will execute the error handler so this always returns true.
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public bool CanHandle(IParameters parameters)
        {
            return true;
        }
    }
}