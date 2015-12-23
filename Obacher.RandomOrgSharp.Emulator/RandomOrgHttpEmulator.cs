using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Obacher.RandomOrgSharp.Emulator
{
    public class RandomOrgServiceEmulator : IRandomOrgService
    {
        private readonly Random _random;
        private readonly DateTime rollOverTime;
        private int _code;
        private object[] _params;

        private int _bitsUsed;
        private int _initialBitsLeft;
        private int _bitsLeft;
        private int _initialRequestsLeft;
        private int _requestsLeft;
        private int _advistoryDelay;

        /// <summary>
        /// Number of bits remaining for the daily limit.  If value is never set then a random value between 100000 and 100000000 will be generated.
        /// </summary>
        public int BitsLeft
        {
            private get
            {
                return _bitsLeft;
            }
            set
            {
                _initialBitsLeft = value;
                _bitsLeft = value;
            }
        }

        // Number of requests remaining for the daily limit.  If value is never set then a random value between 100 and 1000 will be generated.
        public int RequestsLeft
        {
            private get
            {
                return _requestsLeft;
            }
            set
            {
                _initialRequestsLeft = value;
                _requestsLeft = value;
            }
        }

        // Advisory Delay return in the response.  If value is never set then a random value between 0 and 100 milliseconds will be generated on each request.
        public int AdvisoryDelay
        {
            set
            {
                _advistoryDelay = value;
            }
        }

        /// <summary>
        /// This class emulates the random.org web service.
        /// One use of this class is for testing.  Your code can be tested without using up the daily bits allowed by random.org
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
        /// <param name="requestsLeft">Number of requests remaining for the daily limit.  Default value is a random value between 100 and 1000</param>
        /// <param name="advisoryDelay">Advisory Delay returned in the response.  Default value is a value between 0 and 100 milliseconds on each request</param>
        public RandomOrgServiceEmulator(int bitsLeft = -1, int requestsLeft = -1, int advisoryDelay = -1)
        {
            _random = new Random();
            rollOverTime = DateTime.UtcNow;

            _bitsUsed = 0;
            BitsLeft = bitsLeft < 0 ? _random.Next(100000, 100000000) : bitsLeft;
            RequestsLeft = requestsLeft < 0 ? _random.Next(100, 1000) : requestsLeft;
            AdvisoryDelay = advisoryDelay;

            _initialBitsLeft = BitsLeft;
            _initialRequestsLeft = RequestsLeft;
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
        /// <param name="jsonRequest">Request that would be sent to random.org</param>
        /// <returns>Response that looks the same as a response from the random.org web site</returns>
        public JObject SendRequest(JObject jsonRequest)
        {
            JObject response = SentRequestInternal(jsonRequest);
            return response;
        }

        /// <summary>
        /// Emulate the call to random.org web api in an asynchronous manner
        /// </summary>
        /// <param name="jsonRequest">Request that would be sent to random.org</param>
        /// <returns>A task which will return a response that simulates as a response from the random.org web site</returns>
        public Task<JObject> SendRequestAsync(JObject jsonRequest)
        {
            var task = Task.Factory.StartNew(() => SentRequestInternal(jsonRequest));
            return task;
        }

        /// <summary>
        /// Creates the response object based on the parameters in the request object
        /// </summary>
        /// <param name="jsonRequest">Request object</param>
        /// <returns>Response object</returns>
        private JObject SentRequestInternal(JObject jsonRequest)
        {
            JObject response;

            if (_code > 0)
            {
                response = CreateErrorResponse(_code, _params);
                return response;
            }

            string method = JsonHelper.JsonToString(jsonRequest.GetValue(RandomOrgConstants.JSON_METHOD_PARAMETER_NAME));
            int id = JsonHelper.JsonToInt(jsonRequest.GetValue(RandomOrgConstants.JSON_ID_PARAMETER_NAME));

            var parameter = jsonRequest.GetValue(RandomOrgConstants.JSON_PARAMETERS_PARAMETER_NAME) as JObject;
            if (parameter == null)
                return CreateErrorResponse(200, "parameter");

            int numberOfItemsToReturn = JsonHelper.JsonToInt(parameter.GetValue(RandomOrgConstants.JSON_NUMBER_ITEMS_RETURNED_PARAMETER_NAME));


            switch (method)
            {
                case "generateIntegers":
                    {
                        int minValue =
                            JsonHelper.JsonToInt(parameter.GetValue(RandomOrgConstants.JSON_MINIMUM_VALUE_PARAMETER_NAME));
                        int maxValue =
                            JsonHelper.JsonToInt(parameter.GetValue(RandomOrgConstants.JSON_MAXIMUM_VALUE_PARAMETER_NAME));
                        int baseNumber =
                            JsonHelper.JsonToInt(parameter.GetValue(RandomOrgConstants.JSON_BASE_NUMBER_PARAMETER_NAME));
                        IEnumerable<int> values = GenerateInteger(numberOfItemsToReturn, minValue, maxValue, baseNumber);
                        response = CreateResponse(values, id);
                    }
                    break;

                case "generateDecimalFractions":
                    {
                        int decimalPlaces =
                            JsonHelper.JsonToInt(parameter.GetValue(RandomOrgConstants.JSON_DECIMAL_PLACES_PARAMETER_NAME));
                        IEnumerable<decimal> values = GenerateDecimalFractions(numberOfItemsToReturn, decimalPlaces);
                        response = CreateResponse(values, id);
                    }
                    break;

                case "generateGaussians":
                    {
                        int mean = JsonHelper.JsonToInt(parameter.GetValue(RandomOrgConstants.JSON_MEAN_PARAMETER_NAME));
                        int standardDeviation =
                            JsonHelper.JsonToInt(parameter.GetValue(RandomOrgConstants.JSON_STANDARD_DEVIATION_PARAMETER_NAME));
                        int significantDigits =
                            JsonHelper.JsonToInt(parameter.GetValue(RandomOrgConstants.JSON_SIGNIFICANT_DIGITS_PARAMETER_NAME));
                        IEnumerable<decimal> values = GenerateGaussians(numberOfItemsToReturn, mean, standardDeviation,
                            significantDigits);
                        response = CreateResponse(values, id);
                    }
                    break;

                case "generateStrings":
                    {
                        int length = JsonHelper.JsonToInt(parameter.GetValue(RandomOrgConstants.JSON_LENGTH_PARAMETER_NAME));
                        string characters =
                            JsonHelper.JsonToString(parameter.GetValue(RandomOrgConstants.JSON_CHARACTERS_ALLOWED_PARAMETER_NAME));
                        IEnumerable<string> values = GenerateStrings(numberOfItemsToReturn, length, characters);
                        response = CreateResponse(values, id);
                    }
                    break;

                case "generateUUIDs":
                    {
                        IEnumerable<Guid> values = GenerateUUIDs(numberOfItemsToReturn);
                        response = CreateResponse(values, id);
                    }
                    break;

                case "generateBlobs":
                    {
                        int size = JsonHelper.JsonToInt(parameter.GetValue(RandomOrgConstants.JSON_SIZE_PARAMETER_NAME));
                        string format = JsonHelper.JsonToString(parameter.GetValue(RandomOrgConstants.JSON_FORMAT_PARAMETER_NAME));
                        IEnumerable<string> values = GenerateBlobs(numberOfItemsToReturn, size, format);
                        response = CreateResponse(values, id);
                    }
                    break;

                default:
                    response = CreateErrorResponse(201, method);
                    break;
            }

            return response;
        }

        private IEnumerable<int> GenerateInteger(int numberOfItemsToReturn, int minimumValue, int maximumValue, int baseNumber)
        {
            for (int x = 0; x < numberOfItemsToReturn; x++)
            {
                var value = Convert.ToString(_random.Next(minimumValue, maximumValue), baseNumber);
                yield return Convert.ToInt32(value);
            }
        }

        private IEnumerable<decimal> GenerateDecimalFractions(int numberOfItemsToReturn, int decimalPlaces)
        {
            for (int x = 0; x < numberOfItemsToReturn; x++)
            {
                string decimalString = "0.";
                for (int i = 0; i < decimalPlaces; i++)
                    decimalString += _random.Next(0, 9);

                yield return Convert.ToDecimal(decimalString);
            }
        }

        private IEnumerable<decimal> GenerateGaussians(int numberOfItemsToReturn, int mean, int standardDeviation, int significantDigits)
        {
            for (int x = 0; x < numberOfItemsToReturn; x++)
            {
                int startNumber = mean - standardDeviation;
                int endNumber = mean + standardDeviation;
                int decimalPlaces = _random.Next(0, significantDigits);

                string value = _random.Next(startNumber, endNumber).ToString();

                if (decimalPlaces > 0)
                {
                    value += ".";
                    for (int i = 0; i < decimalPlaces; i++)
                        value += _random.Next(0, 9);
                }

                yield return Convert.ToDecimal(value);
            }
        }

        private IEnumerable<string> GenerateStrings(int numberOfItemsToReturn, int length, string characters)
        {
            for (int x = 0; x < numberOfItemsToReturn; x++)
            {
                string value = string.Empty;
                for (int i = 0; i < length; i++)
                {
                    int startingIndex = _random.Next(1, characters.Length);
                    value += characters.Substring(startingIndex, 1);
                }

                yield return value;
            }
        }

        private IEnumerable<Guid> GenerateUUIDs(int numberOfItemsToReturn)
        {
            for (int x = 0; x < numberOfItemsToReturn; x++)
            {
                yield return Guid.NewGuid();
            }
        }


        private IEnumerable<string> GenerateBlobs(int numberOfItemsToReturn, int size, string format)
        {
            if (format == "base64")
                return GenerateStrings(numberOfItemsToReturn, size, "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789");

            return GenerateStrings(numberOfItemsToReturn, size, "abcdef0123456789");
        }

        private JObject CreateResponse<T>(IEnumerable<T> values, int id)
        {
            if (rollOverTime != DateTime.UtcNow)
            {
                BitsLeft = _initialBitsLeft;
                RequestsLeft = _initialRequestsLeft;
            }

            _bitsUsed = values.Sum(s => s.ToString().Length);
            BitsLeft -= _bitsUsed;
            RequestsLeft -= 1;

            int advisoryDelay = _advistoryDelay;
            if (advisoryDelay < 0)
                advisoryDelay = _random.Next(0, 100);

            JObject response = new JObject(
                new JProperty("json-rpc", "2.0"),
                new JProperty("result",
                    new JObject(
                        new JProperty("random",
                            new JObject(
                                new JProperty("data", new JArray(values)),
                                new JProperty("completionTime", DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ssZ"))
                            )
                        ),
                        new JProperty("bitsUsed", _bitsUsed),
                        new JProperty("bitsLeft", BitsLeft),
                        new JProperty("requestsLeft", RequestsLeft),
                        new JProperty("advisoryDelay", advisoryDelay)
                    )
                ),
                new JProperty("id", id)
            );

            // JObject response = new JObject(
            //     new JProperty(RandomOrgConstants.JSON_RPC_PARAMETER_NAME, RandomOrgConstants.JSON_RPC_VALUE),
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

        private JObject CreateErrorResponse(int code, params object[] data)
        {
            int id = _random.Next();
            string message = ResourceHelper.GetString(StringsConstants.ERROR_CODE_KEY + code, data);

            JObject response = new JObject(
                new JProperty(RandomOrgConstants.JSON_RPC_PARAMETER_NAME, RandomOrgConstants.JSON_RPC_VALUE),
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
