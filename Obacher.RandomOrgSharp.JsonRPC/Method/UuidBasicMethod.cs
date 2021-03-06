﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Obacher.RandomOrgSharp.Core;
using Obacher.RandomOrgSharp.Core.Parameter;
using Obacher.RandomOrgSharp.Core.Request;
using Obacher.RandomOrgSharp.Core.Response;
using Obacher.RandomOrgSharp.Core.Service;
using Obacher.RandomOrgSharp.JsonRPC.Request;
using Obacher.RandomOrgSharp.JsonRPC.Response;

namespace Obacher.RandomOrgSharp.JsonRPC.Method
{
    /// <summary>
    /// This class is a wrapper class the <see cref="MethodCallBroker"/>.  It setups a default set of classes to be called when making a call to random.org
    /// </summary>
    /// <remarks>
    /// The following is the sequence of actions that will occur during the call...
    ///     Build the necessary JSON-RPC request string (the package that is used to retrieve a list of blob values)
    ///     Execute the <see cref="AdvisoryDelayHandler"/> to delay the request the time recommended by random.org (based on the value in the previous response)
    ///     Retrieve the random blob values from random.org
    ///     Determine if an error occurred during the call and throw an exception if one did occur
    ///     Store the advisory delay value to ensure the next request to random.org does not occur before the time requested by random.org.  This will help prevent you from
    ///     being banned from making requests to random.org
    ///     Verify the Id returned in the response matches the id passed into the request
    ///     Parse the response into a <see cref="DataResponseInfo{T}"/> so the blob values can be extracted
    /// </remarks>
    public class UuidBasicMethod
    {
        protected IRandomService RandomService;
        protected IRequestBuilder RequestBuilder;
        protected IBeforeRequestCommandFactory BeforeRequestCommandFactory;
        protected IResponseHandlerFactory ResponseHandlerFactory;
        protected JsonResponseParserFactory ResponseParser;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="advisoryDelayHandler">
        /// Class which handles the apprioriate delay before the request is called.
        /// It is required that this class be passed into the method because the same instance of the <see cref="AdvisoryDelayHandler"/> must be passed in on every request.
        /// </param>
        /// <param name="randomService"><see cref="IRandomService"/> to use to get random values.  Defaults to <see cref="RandomOrgApiService"/></param>
        public UuidBasicMethod(AdvisoryDelayHandler advisoryDelayHandler, IRandomService randomService = null)
        {
            RandomService = randomService ?? new RandomOrgApiService();
            RequestBuilder = new JsonRequestBuilder(new UuidJsonRequestBuilder());

            BeforeRequestCommandFactory = new BeforeRequestCommandFactory(advisoryDelayHandler);

            // We need to keep this separate so we can retrieve the list of values that are returned from to the caller
            ResponseParser = new JsonResponseParserFactory(new UuidResponseParser());

            ResponseHandlerFactory = new ResponseHandlerFactory(
                new ErrorHandlerThrowException(new ErrorParser()),
                advisoryDelayHandler,
                new VerifyIdResponseHandler(),
                ResponseParser
            );
        }

        /// <summary>
        /// Retrieves a list of random blob values
        /// </summary>
        /// <param name="numberOfItemsToReturn">How many random decimal fractions you need. Must be between 1 and 10,000.</param>

        /// <returns>List of random blob values</returns>
        public virtual IEnumerable<Guid> GenerateUuids(int numberOfItemsToReturn)
        {
            IParameters requestParameters = UuidParameters.Create(numberOfItemsToReturn);
            IMethodCallBroker broker = new MethodCallBroker(RequestBuilder, RandomService, BeforeRequestCommandFactory, ResponseHandlerFactory);
            broker.Generate(requestParameters);

            return (ResponseParser.ResponseInfo as DataResponseInfo<Guid>)?.Data;
        }

        /// <summary>
        /// Retrieves a list of random blob values in an asynchronous manners
        /// </summary>
        /// <param name="numberOfItemsToReturn">How many random decimal fractions you need. Must be between 1 and 10,000.</param>
        /// <returns>List of random blob values</returns>
        public virtual async Task<IEnumerable<Guid>> GenerateUuidsAsync(int numberOfItemsToReturn)
        {
            IParameters requestParameters = UuidParameters.Create(numberOfItemsToReturn);
            MethodCallBroker broker = new MethodCallBroker(RequestBuilder, RandomService, BeforeRequestCommandFactory, ResponseHandlerFactory);
            await broker.GenerateAsync(requestParameters);

            return (ResponseParser.ResponseInfo as DataResponseInfo<Guid>)?.Data;
        }
    }
}