﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Obacher.RandomOrgSharp.Core;
using Obacher.RandomOrgSharp.JsonRPC;

namespace Obacher.RandomOrgSharp.Emulator
{
    /// <summary>
    /// Class to emulate the functionality of random.org
    /// </summary>
    /// <remarks>
    /// 
    /// ---------------------------------------------------------
    /// NOTE:  THIS CLASS SHOULD ONLY BE USED FOR TESTING. 
    /// ---------------------------------------------------------
    /// 
    /// This class does not make any attempt to, nor does it come anywhere near the randomness generated by random.org.  The class is intended to 
    /// be used for testing purposes only, to give developers the ability to repeatedly test their code without using up the daily limits at random.org
    /// </remarks>
    public class RandomOrgApiEmulator : IRandomService
    {
        private readonly Random _random;
        private readonly DateTime _rollOverTime;
        private int _code;
        private object[] _params;

        private int _initialBitsLeft;
        private int _bitsLeft;
        private int _initialRequestCount;
        private int _requestsLeft;
        private int _advistoryDelay;

        /// <summary>
        /// This class emulates the random.org web service.
        /// One use of this class is for testing.  Your code can be tested without using up the daily limits at random.org
        /// A few things to note about this class.
        /// 1) The values returned from this class are NOT as random as random.org.  Using this class in production is not recommended.
        /// 2) Since the class uses the pseudo-random generator, duplicate values will never be specified, therefore the replacement value has no effect
        /// 3) This class assumes that all parameters are valid.
        /// 3) The Bits Used and Requests Left are reset to their initial values when the UTC Date rolls over to the next day.
        /// </summary>
        /// <remarks>
        /// You can specify the starting values for some of the values returned within the response of random.org.  This will allow you to 
        /// more easily test the edge cases of your application.  For example, if you want to test to ensure your code properly handles
        /// the exception when there are no more bits left you can set the bitsLeft a very low value, causing the exception to be thrown.
        /// Leaving these at their default will set higher values.
        /// </remarks>
        /// <param name="bitsLeft">Number of bits remaining for the daily limit.  Default value is a random value between 100000 and 100000000</param>
        /// <param name="requestsLeft">Number of requests remaining for the daily limit.  Default value is a random value between 10000 and minimum of 100000 or BitsLeft</param>
        /// <param name="advisoryDelay">Advisory Delay returned in the response.  Default value is a value between 0 and 100 milliseconds on each request</param>
        public RandomOrgApiEmulator(int bitsLeft = -1, int requestsLeft = -1, int advisoryDelay = -1)
        {
            _random = new Random();
            _rollOverTime = DateTime.UtcNow;

            SetInitialBitsLeft(bitsLeft < 0 ? _random.Next(100000, 100000000) : bitsLeft);
            SetInitialRequestsLeft(requestsLeft < 0 ? _random.Next(10000, Math.Min(100000, _initialBitsLeft)) : requestsLeft);
            SetAdvisoryDelay(advisoryDelay);
        }

        /// <summary>
        /// Number of bits remaining for the daily limit.
        /// </summary>
        /// <remarks>
        /// Set this value to a low number to verify your code properly handles the scenerio where the daily limit is exceeded
        /// </remarks>
        public void SetInitialBitsLeft(int bitsLeft)
        {
            _initialBitsLeft = bitsLeft;
            _bitsLeft = bitsLeft;
        }

        /// <summary>
        /// Number of requests remaining for the daily limit.
        /// </summary>
        /// <remarks>
        /// Set this value to a low number to verify your code properly handles the scenerio where the daily limit is exceeded
        /// </remarks>
        public void SetInitialRequestsLeft(int requestsLeft)
        {
            _initialRequestCount = requestsLeft;
            _requestsLeft = requestsLeft;
        }


        /// <summary>
        /// Advisory Delay to return in the response.
        /// </summary>
        /// <remarks>
        /// By default the advisory delay is between 0 and 100 milliseconds.  You can set this value if you wish to control the advisory delay.
        /// </remarks>
        public void SetAdvisoryDelay(int advisoryDelay)
        {
            _advistoryDelay = advisoryDelay;
        }

        /// <summary>
        /// Force a <see cref="RandomOrgException"/> to occur on the next call to SendRequest.
        /// </summary>
        /// <param name="code">Error code to be returned in response</param>
        /// <param name="parameters">Parameters that will be passed to the message.  The number of parameters passed here must match the expected number of parameters in the message</param>
        public void ForceException(int code, params object[] parameters)
        {
            _code = code;
            _params = parameters;
        }

        /// <summary>
        /// Emulate the call to the random.org web api
        /// </summary>
        /// <param name="request">Request that would be sent to random.org</param>
        /// <returns>ResponseInfo that looks the same as a response from the random.org web site</returns>
        public string SendRequest(string request)
        {
            var response = SendRequestInternal(request);
            return response;
        }

        /// <summary>
        /// Emulate the call to random.org web api in an asynchronous manner
        /// </summary>
        /// <param name="request">Request that would be sent to random.org</param>
        /// <returns>A task which will return a response that simulates as a response from the random.org web site</returns>
        public Task<string> SendRequestAsync(string request)
        {
            var task = Task.Factory.StartNew(() => SendRequestInternal(request));
            return task;
        }

        /// <summary>
        /// Creates the response object based on the parameters in the request object
        /// </summary>
        /// <param name="request">Request object</param>
        /// <returns>ResponseInfo object</returns>
        private string SendRequestInternal(string request)
        {
            JObject response;
            JObject jsonRequest = JObject.Parse(request);

            // The UTC time has rolled to a new day, reset the daily limits
            if (_rollOverTime != DateTime.UtcNow)
            {
                _bitsLeft = _initialBitsLeft;
                _requestsLeft = _initialRequestCount;
            }

            // If caller requested emulating an error returned from random.org then go ahead and respond with the error. 
            if (_code > 0)
            {
                response = CreateErrorResponse(_code, _params);
                return response.ToString();
            }

            // Extract information from json needed to emulate the random.org request
            var method = JsonHelper.JsonToString(jsonRequest.GetValue(JsonRpcConstants.METHOD_PARAMETER_NAME));
            var id = JsonHelper.JsonToInt(jsonRequest.GetValue(JsonRpcConstants.ID_PARAMETER_NAME));

            var parameter = jsonRequest.GetValue(JsonRpcConstants.PARAMETERS_PARAMETER_NAME) as JObject;
            if (parameter == null)
                return CreateErrorResponse(200, "parameter").ToString();

            var numberOfItemsToReturn = JsonHelper.JsonToInt(parameter.GetValue(JsonRpcConstants.NUMBER_ITEMS_RETURNED_PARAMETER_NAME));

            // Extract specific parameters for each method and call the correct method to emulate the random.org method.
            switch (method)
            {
                case "generateIntegers":
                    {
                        var minValue = JsonHelper.JsonToInt(parameter.GetValue(JsonRpcConstants.MINIMUM_VALUE_PARAMETER_NAME));
                        var maxValue = JsonHelper.JsonToInt(parameter.GetValue(JsonRpcConstants.MAXIMUM_VALUE_PARAMETER_NAME));
                        var baseNumber = JsonHelper.JsonToInt(parameter.GetValue(JsonRpcConstants.BASE_NUMBER_PARAMETER_NAME));
                        var values = GenerateInteger(numberOfItemsToReturn, minValue, maxValue, baseNumber).ToList();
                        response = CreateResponse(values, id, values.Sum(x => x.ToString().Length * 3));
                    }
                    break;

                case "generateDecimalFractions":
                    {
                        var decimalPlaces =
                            JsonHelper.JsonToInt(parameter.GetValue(JsonRpcConstants.DECIMAL_PLACES_PARAMETER_NAME));
                        var values = GenerateDecimalFractions(numberOfItemsToReturn, decimalPlaces).ToList();
                        response = CreateResponse(values, id, values.Sum(x => (x.ToString().Length - 2) * 3));
                    }
                    break;

                case "generateGaussians":
                    {
                        var mean = JsonHelper.JsonToInt(parameter.GetValue(JsonRpcConstants.MEAN_PARAMETER_NAME));
                        var standardDeviation = JsonHelper.JsonToInt(parameter.GetValue(JsonRpcConstants.STANDARD_DEVIATION_PARAMETER_NAME));
                        var significantDigits = JsonHelper.JsonToInt(parameter.GetValue(JsonRpcConstants.SIGNIFICANT_DIGITS_PARAMETER_NAME));
                        var values = GenerateGaussians(numberOfItemsToReturn, mean, standardDeviation, significantDigits).ToList();
                        response = CreateResponse(values, id, values.Sum(x => (x.ToString().Length - 2) * 3));
                    }
                    break;

                case "generateStrings":
                    {
                        var length = JsonHelper.JsonToInt(parameter.GetValue(JsonRpcConstants.LENGTH_PARAMETER_NAME));
                        var characters = JsonHelper.JsonToString(parameter.GetValue(JsonRpcConstants.CHARACTERS_ALLOWED_PARAMETER_NAME));
                        List<string> values = GenerateStrings(numberOfItemsToReturn, length, characters).ToList();
                        response = CreateResponse(values, id, values.Sum(s => s.Length) * 5);
                    }
                    break;

                case "generateUUIDs":
                    {
                        var values = GenerateUUIDs(numberOfItemsToReturn);
                        response = CreateResponse(values, id, numberOfItemsToReturn * 122);
                    }
                    break;

                case "generateBlobs":
                    {
                        var size = JsonHelper.JsonToInt(parameter.GetValue(JsonRpcConstants.SIZE_PARAMETER_NAME));
                        var format = JsonHelper.JsonToString(parameter.GetValue(JsonRpcConstants.FORMAT_PARAMETER_NAME));
                        var values = GenerateBlobs(numberOfItemsToReturn, size, format).ToList();
                        response = CreateResponse(values, id, numberOfItemsToReturn * size);
                    }
                    break;

                case "getUsage":
                    {
                        response = CreateUsageResponse(id);
                    }
                    break;

                default:
                    response = CreateErrorResponse(201, method);
                    break;
            }

            return response.ToString();
        }

        private IEnumerable<int> GenerateInteger(int numberOfItemsToReturn, int minimumValue, int maximumValue, int baseNumber)
        {
            for (var x = 0; x < numberOfItemsToReturn; x++)
            {
                var value = Convert.ToString(_random.Next(minimumValue, maximumValue), baseNumber);
                yield return Convert.ToInt32(value);
            }
        }

        private IEnumerable<decimal> GenerateDecimalFractions(int numberOfItemsToReturn, int decimalPlaces)
        {
            for (var x = 0; x < numberOfItemsToReturn; x++)
            {
                var decimalString = "0.";
                for (var i = 0; i < decimalPlaces; i++)
                    decimalString += _random.Next(0, 9);

                yield return Convert.ToDecimal(decimalString);
            }
        }

        private IEnumerable<decimal> GenerateGaussians(int numberOfItemsToReturn, int mean, int standardDeviation, int significantDigits)
        {
            for (var x = 0; x < numberOfItemsToReturn; x++)
            {
                var startNumber = mean - Math.Abs(standardDeviation);
                var endNumber = mean + Math.Abs(standardDeviation);
                var decimalPlaces = _random.Next(0, significantDigits);

                var value = _random.Next(startNumber, endNumber).ToString();

                if (decimalPlaces > 0)
                {
                    value += ".";
                    for (var i = 0; i < decimalPlaces; i++)
                        value += _random.Next(0, 9);
                }

                yield return Convert.ToDecimal(value);
            }
        }

        private IEnumerable<string> GenerateStrings(int numberOfItemsToReturn, int length, string characters)
        {
            for (var x = 0; x < numberOfItemsToReturn; x++)
            {
                var value = string.Empty;
                for (var i = 0; i < length; i++)
                {
                    var startingIndex = _random.Next(1, characters.Length);
                    value += characters.Substring(startingIndex, 1);
                }

                yield return value;
            }
        }

        private IEnumerable<Guid> GenerateUUIDs(int numberOfItemsToReturn)
        {
            for (var x = 0; x < numberOfItemsToReturn; x++)
                yield return Guid.NewGuid();
        }


        private IEnumerable<string> GenerateBlobs(int numberOfItemsToReturn, int size, string format)
        {
            var charactersUsed = format == "base64" ? "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789" : "abcdef0123456789";
            return GenerateStrings(numberOfItemsToReturn, size, charactersUsed);
        }

        /// <summary>
        /// Build the json response
        /// </summary>
        /// <typeparam name="T">Type of values in the data node</typeparam>
        /// <param name="values">List of values in the data node</param>
        /// <param name="id">Identifier in the request object</param>
        /// <param name="bitsUsed">Bits used in the request. 
        /// Note: Due to the lack of knowledge as to how random.org computes the bits used this is a close approximation of the number of bits used.</param>
        /// <returns>Json representing a random.org response</returns>
        private JObject CreateResponse<T>(IEnumerable<T> values, int id, int bitsUsed)
        {
            if (_bitsLeft - bitsUsed <= 0)
                return CreateErrorResponse(403, bitsUsed, _bitsLeft);

            if (_requestsLeft <= 0)
                return CreateErrorResponse(402, _requestsLeft, 0);

            _bitsLeft -= bitsUsed;
            _requestsLeft -= 1;

            var advisoryDelay = _advistoryDelay;
            if (advisoryDelay < 0)
                advisoryDelay = _random.Next(0, 100);

            var response = new JObject(
                new JProperty("jsonrpc", "2.0"),
                new JProperty("result",
                    new JObject(
                        new JProperty("random",
                            new JObject(
                                new JProperty("data", new JArray(values)),
                                new JProperty("completionTime", DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ssZ"))
                                )
                            ),
                        new JProperty("bitsUsed", bitsUsed),
                        new JProperty("bitsLeft", _bitsLeft),
                        new JProperty("requestsLeft", _requestsLeft),
                        new JProperty("advisoryDelay", advisoryDelay)
                        )
                    ),
                new JProperty("id", id)
                );

            // JObject response = new JObject(
            //     new JProperty(RandomOrgConstants.RPC_PARAMETER_NAME, RandomOrgConstants.RPC_VALUE),
            //         new JObject("result",
            //             new JObject("random",
            //                 new JObject("data", new JArray(values)),
            //                 new JProperty("completionTime", XmlConvert.ToString(DateTime.UtcNow, XmlDateTimeSerializationMode.Utc))\
            //             ),
            //             new JProperty("bitsUsed", _bitsUsed),
            //             new JProperty("bitsLeft", BitsLeft),
            //             new JProperty("requestsLeft", RequestsLeft),
            //             new JProperty("advisoryDelay", advisoryDelay)
            //         ),
            //     new JProperty("id", id)
            //);

            return response;
        }

        /// <summary>
        /// Build the usage response object in the same format as returned by random.org
        /// </summary>
        /// <param name="id">Identifier from request object</param>
        /// <returns>Json respresenting a usage response from random.org</returns>
        private JObject CreateUsageResponse(int id)
        {
            var response =
                new JObject(
                    new JProperty("jsonrpc", "2.0"),
                    new JProperty("result",
                        new JObject(
                            new JProperty("status", "running"),
                            new JProperty("creationTime", DateTime.UtcNow.AddDays(_random.Next(-1825, -1)).ToString("yyyy-MM-dd HH:mm:ssZ")),
                            new JProperty("bitsLeft", _bitsLeft),
                            new JProperty("requestsLeft", _requestsLeft),
                            new JProperty("totalBits", _initialBitsLeft - _bitsLeft),
                            new JProperty("totalRequests", _initialRequestCount - _requestsLeft)
                        )
                    ),
                    new JProperty("id", id)
                    );

            // {
            //   "jsonrpc": "2.0",
            //   "result": {
            //     "status": "running",
            //     "creationTime": "2013-02-01 17:53:40Z",
            //     "bitsLeft": 998532,
            //     "requestsLeft": 199996,
            //     "totalBits": 1646421,
            //     "totalRequests": 65036
            //   },
            //   "id": 15998
            // }

            return response;

        }

        private JObject CreateErrorResponse(int code, params object[] data)
        {
            var id = _random.Next();
            var message = ResourceHelper.GetString(StringsConstants.ERROR_CODE_KEY + code, data);

            var response = new JObject(
                new JProperty(JsonRpcConstants.RPC_PARAMETER_NAME, JsonRpcConstants.RPC_VALUE),
                new JObject("error",
                    new JProperty("code", code.ToString()),
                    new JProperty("message", message),
                    new JArray("data", data)
                    ),
                new JProperty("id", id)
                );

            return response;
        }
    }
}