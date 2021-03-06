﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace org.random.JSONRPC
{
    public class RandomJSONRPC
    {
        /** The URL to send the remote procedure calls to */
        private readonly string URL = "https://api.random.org/json-rpc/1/invoke";
        /** The following members are the names of the basic methods available in the random.org api */
        private readonly string INTEGER_METHOD = "generateIntegers";
        private readonly string DECIMALFRACTION_METHOD = "generateDecimalFractions";
        private readonly string GAUSSIAN_METHOD = "generateGaussians";
        private readonly string STRING_METHOD = "generateStrings";
        private readonly string UUID_METHOD = "generateUUIDs";
        // Signed method calls
        private readonly string SIGNED_INTEGER_METHOD = "generateSignedIntegers";
        private readonly string SIGNED_DECIMALFRACTION_METHOD = "generateSignedDecimalFractions";
        private readonly string SIGNED_GAUSSIAN_METHOD = "generateSignedGaussians";
        private readonly string SIGNED_STRING_METHOD = "generateSignedStrings";
        private readonly string SIGNED_UUID_METHOD = "generateSignedUUIDs";
        private readonly string VERIFY_SIGNATURE = "verifySignature";
        private readonly string GET_USAGE_METHOD = "getUsage";
        /** The HTTP content type for the requests */
        private readonly string CONTENT_TYPE = "application/json";
        /** The default value for the optional replacement parameter */
        private bool REPLACEMENT_DEFAULT = true;
        private readonly int ONE_HOUR_IN_MILLIS = 3600000;
        private string mApiKey;
        private long mMaxBlockingTime = 3000;
        /** The request object to be sent to the server */
        private JObject mJSONRequest;
        /** The response object received from the server */
        private JObject mJSONResponse;
        /** The parameters supplied with the request object */
        private JObject mJSONParams;
        /** The time of the last received response */
        private long mLastResponseReceived;
        /** The advisory delay given by the random.org server */
        private long mAdvisoryDelay = 0;
        // Signed Keys and Signature Authenticity requirements
        private readonly string SIGNATURE = "signature";
        private readonly string AUTHENTICITY = "authenticity";

        // Other hard coding required
        private readonly string RESULT = "result";
        private readonly string RANDOM = "random";
        private readonly string APIKEY = "apiKey";


        /// <summary>
        /// Creates a new RandomJSONRPC object with the given api key
        /// </summary>
        /// <param name="apiKey">The api Key from random.org</param>
        public RandomJSONRPC(String apiKey)
        {
            mApiKey = apiKey;
        }

        /// <summary>
        /// Creates a new RandomJSONRPC object with the given api key and the maximum time the user wants to wait for the server.
        /// </summary>
        /// <param name="apiKey">The api key from random.org</param>
        /// <param name="maxBlockingTime">The longest amount of time (in milliseconds) that the user wants to wait for the server (default is 3 seconds).
        /// This does not take into account the time it takes to send the request over the network.
        /// Only the advisory delay given by the server is used. If the maxBlockingTime value is exceeded a RuntimeException will be thrown.
        /// </param>
        public RandomJSONRPC(String apiKey, long maxBlockingTime)
        {
            mApiKey = apiKey;
            mMaxBlockingTime = maxBlockingTime;
        }

        /// <summary>
        /// Calls <code> GenerateIntegers(int n, int min, int max, true, 10) </code>
        /// </summary>
        /// <param name="n">How many random integers are needed. Must be within the [1,1e4] range.</param>
        /// <param name="min">The lower boundary for the range from which the random numbers will be picked. Must be within the [-1e9,1e9] range.</param>
        /// <param name="max">The upper boundary for the range from which the random numbers will be picked. Must be within the [-1e9,1e9] range.</param>
        /// <returns>a set of random integers limited by the parameters listed above and generated by random.org</returns>
        public int[] GenerateIntegers(int n, int min, int max)
        {
            return GenerateIntegers(n, min, max, REPLACEMENT_DEFAULT);
        }

        /// <summary>
        /// Calls <code> GenerateIntegers(int n, int min, int max, bool replacement, 10) </code>
        /// </summary>
        /// <param name="n">How many random integers are needed. Must be within the [1,1e4] range.</param>
        /// <param name="min">The lower boundary for the range from which the random numbers will be picked. Must be within the [-1e9,1e9] range.</param>
        /// <param name="max">The upper boundary for the range from which the random numbers will be picked. Must be within the [-1e9,1e9] range.</param>
        /// <param name="replacement">(default value true) Specifies whether the random numbers should be picked with replacement.</param>
        /// <returns>a set of random integers limited by the parameters listed above and generated by random.org</returns>
        public int[] GenerateIntegers(int n, int min, int max, bool replacement)
        {
            return Array.ConvertAll(GenerateIntegers(n, min, max, replacement, 10), int.Parse);
        }

        /// <summary>
        /// Generates true random integers within a user-defined range
        /// </summary>
        /// <param name="n">How many random integers are needed. Must be within the [1,1e4] range.</param>
        /// <param name="min">The lower boundary for the range from which the random numbers will be picked. Must be within the [-1e9,1e9] range.</param>
        /// <param name="max">The upper boundary for the range from which the random numbers will be picked. Must be within the [-1e9,1e9] range.</param>
        /// <param name="replacement">(default value true) Specifies whether the random numbers should be picked with replacement.</param>
        /// <param name="baseNum">Specifies the base that will be used to display the numbers. Values allowed are 2, 8, 10 and 16.</param>
        /// <returns>a set of random integers limited by the parameters listed above and generated by random.org</returns>
        public string[] GenerateIntegers(int n, int min, int max, bool replacement, int baseNum)
        {
            mJSONParams = InitIntegerParams(n, min, max, replacement, baseNum);
            mJSONRequest = InitMethod(INTEGER_METHOD);
            SendRequest();
            return ExtractStrings();
        }

        /// <summary>
        /// Calls <code> GenerateDecimalFractions(int n, int decimalPlaces, true) </code>
        /// </summary>
        /// <param name="n">How many random decimal fractions are needed. Must be within the [1,1e4] range.</param>
        /// <param name="decimalPlaces">The number of decimal places to use. Must be within the [1,20] range.</param>
        /// <returns>a set of random integers limited by the parameters listed above and generated by random.org</returns>
        public double[] GenerateDecimalFractions(int n, int decimalPlaces)
        {
            return GenerateDecimalFractions(n, decimalPlaces, true);
        }

        /// <summary>
        /// Generates true random decimal fractions from a uniform distribution across the [0,1] interval with a user-defined number of decimal places.
        /// </summary>
        /// <param name="n">How many random decimal fractions are needed. Must be within the [1,1e4] range.</param>
        /// <param name="decimalPlaces">The number of decimal places to use. Must be within the [1,20] range.</param>
        /// <param name="replacement">(default value true) Specifies whether the random numbers should be picked with replacement.</param>
        /// <returns>a set of random integers limited by the parameters listed above and generated by random.org</returns>
        public double[] GenerateDecimalFractions(int n, int decimalPlaces, bool replacement)
        {
            mJSONParams = InitDecimalFractionParams(n, decimalPlaces, replacement);
            mJSONRequest = InitMethod(DECIMALFRACTION_METHOD);
            SendRequest();
            return ExtractDoubles();
        }

        /// <summary>
        /// Generates true random numbers from a Gaussian distribution (also known as a normal distribution).
        /// </summary>
        /// <param name="n">How many random Gaussian numbers are needed. Must be within the [1,1e4] range.</param>
        /// <param name="mean">The distribution's mean. Must be within the [-1e6,1e6] range.</param>
        /// <param name="standardDeviation">The distribution's standard deviation. Must be within the [-1e6,1e6] range.</param>
        /// <param name="significantDigits">The number of significant digits to use. Must be within the [2,20] range.</param>
        /// <returns>a set of random Gaussians limited by the parameters listed above and generated by random.org</returns>
        public double[] GenerateGaussians(int n, double mean, double standardDeviation, int significantDigits)
        {
            mJSONParams = InitGaussiansParams(n, mean, standardDeviation, significantDigits);
            mJSONRequest = InitMethod(GAUSSIAN_METHOD);
            SendRequest();
            return ExtractDoubles();
        }

        /// <summary>
        /// Generates true random strings. 
        /// </summary>
        /// <param name="n">How many strings are needed. Must be within the [1,1e4] range. </param>
        /// <param name="length">The length of each string. Must be within the [1,20] range. All strings will be of the same length.</param>
        /// <param name="characters">A string that contains the set of characters that are allowed to occur in the random strings. The maximum number of characters is 80.</param>
        /// <returns>a set of random strings limited by the parameters listed above and generated by random.org</returns>
        public String[] GenerateStrings(int n, int length, String characters)
        {
            return GenerateStrings(n, length, characters, true);
        }

        /// <summary>
        /// Generates true random strings. 
        /// </summary>
        /// <param name="n">How many strings are needed. Must be within the [1,1e4] range. </param>
        /// <param name="length">The length of each string. Must be within the [1,20] range. All strings will be of the same length.</param>
        /// <param name="characters">A string that contains the set of characters that are allowed to occur in the random strings. The maximum number of characters is 80.</param>
        /// <param name="replacement">(default value true) Specifies whether the random strings should be picked with replacement</param>
        /// <returns>a set of random strings limited by the parameters listed above and generated by random.org</returns>
        public String[] GenerateStrings(int n, int length, String characters, bool replacement)
        {
            mJSONParams = InitStringParams(n, length, characters, replacement);
            mJSONRequest = InitMethod(STRING_METHOD);
            SendRequest();
            return ExtractStrings();
        }

        /// <summary>
        /// Generates version 4 true random Universally Unique IDentifiers (UUIDs) in accordance with section 4.4 of RFC 4122. 
        /// </summary>
        /// <param name="n">How many UUIDs are needed. Must be within the [1,1e3] range.</param>
        /// <returns>a set of random UUIDS limited by the parameters listed above and generated by random.org</returns>
        public Guid[] GenerateUUIDs(int n)
        {
            mJSONParams = InitUUIDParams(n);
            mJSONRequest = InitMethod(UUID_METHOD);
            SendRequest();
            return ExtractUUIDs();
        }

        /// <summary>
        /// calls <code>GenerateSignedIntegers(n, min, max, true, 10)</code>
        /// </summary>
        /// <param name="n">How many random integers you need. Must be within the [1,1e4] range.</param>
        /// <param name="min">The lower boundary for the range from which the random numbers will be picked. Must be within the [-1e9,1e9] range.</param>
        /// <param name="max">The upper boundary for the range from which the random numbers will be picked. Must be within the [-1e9,1e9] range.</param>
        /// <returns>a set of random integers limited by the parameters listed above and generated by random.org</returns>
        public int[] GenerateSignedIntegers(int n, int min, int max)
        {
            return GenerateSignedIntegers(n, min, max, REPLACEMENT_DEFAULT);
        }

        /// <summary>
        /// Calls <code>GenerateSignedIntegers(n, min, max, replacement, 10)</code>
        /// </summary>
        /// <param name="n">How many random integers you need. Must be within the [1,1e4] range.</param>
        /// <param name="min">The lower boundary for the range from which the random numbers will be picked. Must be within the [-1e9,1e9] range.</param>
        /// <param name="max">The upper boundary for the range from which the random numbers will be picked. Must be within the [-1e9,1e9] range.</param>
        /// <param name="replacement">(default value true) Specifies whether the random numbers should be picked with replacement</param>
        /// <returns>a set of random integers limited by the parameters listed above and generated by random.org</returns>
        public int[] GenerateSignedIntegers(int n, int min, int max, bool replacement)
        {
            return Array.ConvertAll(GenerateSignedIntegers(n, min, max, replacement, 10), int.Parse);
        }

        /// <summary>
        /// Generates true random integers within a user-defined range
        /// </summary>
        /// <param name="n">How many random integers you need. Must be within the [1,1e4] range.</param>
        /// <param name="min">The lower boundary for the range from which the random numbers will be picked. Must be within the [-1e9,1e9] range.</param>
        /// <param name="max">The upper boundary for the range from which the random numbers will be picked. Must be within the [-1e9,1e9] range.</param>
        /// <param name="replacement">(default value true) Specifies whether the random numbers should be picked with replacement</param>
        /// <param name="baseValue">(default value 10) Specifies the base that will be used to display the numbers. Values allowed are 2, 8, 10 and 16. </param>
        /// <returns>a set of random integers limited by the parameters listed above and generated by random.org
        /// Since bases other than 10 will require alphe numeric or padding, returns as a string array</returns>
        public string[] GenerateSignedIntegers(int n, int min, int max, bool replacement, int baseValue)
        {
            mJSONParams = InitIntegerParams(n, min, max, replacement, baseValue);
            mJSONRequest = InitMethod(SIGNED_INTEGER_METHOD);
            SendRequest();
            return ExtractStrings();
        }

        /// <summary>
        /// Calls <code> GenerateSignedDecimalFractions(int n, int decimalPlaces, true) </code>
        /// </summary>
        /// <param name="n">How many random decimal fractions are needed. Must be within the [1,1e4] range.</param>
        /// <param name="decimalPlaces">The number of decimal places to use. Must be within the [1,20] range.</param>
        /// <returns>a set of random integers limited by the parameters listed above and generated by random.org</returns>
        public double[] GenerateSignedDecimalFractions(int n, int decimalPlaces)
        {
            return GenerateSignedDecimalFractions(n, decimalPlaces, true);
        }

        /// <summary>
        /// Generates true random decimal fractions from a uniform distribution across the [0,1] interval with a user-defined number of decimal places.
        /// </summary>
        /// <param name="n">How many random decimal fractions are needed. Must be within the [1,1e4] range.</param>
        /// <param name="decimalPlaces">The number of decimal places to use. Must be within the [1,20] range.</param>
        /// <param name="replacement">(default value true) Specifies whether the random numbers should be picked with replacement.</param>
        /// <returns>a set of random integers limited by the parameters listed above and generated by random.org</returns>
        public double[] GenerateSignedDecimalFractions(int n, int decimalPlaces, bool replacement)
        {
            mJSONParams = InitDecimalFractionParams(n, decimalPlaces, replacement);
            mJSONRequest = InitMethod(SIGNED_DECIMALFRACTION_METHOD);
            SendRequest();
            return ExtractDoubles();
        }

        /// <summary>
        /// Generates true random numbers from a Gaussian distribution (also known as a normal distribution).
        /// </summary>
        /// <param name="n">How many random Gaussian numbers are needed. Must be within the [1,1e4] range.</param>
        /// <param name="mean">The distribution's mean. Must be within the [-1e6,1e6] range.</param>
        /// <param name="standardDeviation">The distribution's standard deviation. Must be within the [-1e6,1e6] range.</param>
        /// <param name="significantDigits">The number of significant digits to use. Must be within the [2,20] range.</param>
        /// <returns>a set of random Gaussians limited by the parameters listed above and generated by random.org</returns>
        public double[] GenerateSignedGaussians(int n, double mean, double standardDeviation, int significantDigits)
        {
            mJSONParams = InitGaussiansParams(n, mean, standardDeviation, significantDigits);
            mJSONRequest = InitMethod(SIGNED_GAUSSIAN_METHOD);
            SendRequest();
            return ExtractDoubles();
        }

        /// <summary>
        /// Calls <code>GenerateSignedStrings(n, length, characters, true)</code> 
        /// </summary>
        /// <param name="n">How many strings are needed. Must be within the [1,1e4] range. </param>
        /// <param name="length">The length of each string. Must be within the [1,20] range. All strings will be of the same length.</param>
        /// <param name="characters">A string that contains the set of characters that are allowed to occur in the random strings. The maximum number of characters is 80.</param>
        /// <returns>a set of random strings limited by the parameters listed above and generated by random.org</returns>
        public String[] GenerateSignedStrings(int n, int length, String characters)
        {
            return GenerateSignedStrings(n, length, characters, true);
        }

        /// <summary>
        /// Generates true random strings. 
        /// </summary>
        /// <param name="n">How many strings are needed. Must be within the [1,1e4] range. </param>
        /// <param name="length">The length of each string. Must be within the [1,20] range. All strings will be of the same length.</param>
        /// <param name="characters">A string that contains the set of characters that are allowed to occur in the random strings. The maximum number of characters is 80.</param>
        /// <param name="replacement">(default value true) Specifies whether the random strings should be picked with replacement</param>
        /// <returns>a set of random strings limited by the parameters listed above and generated by random.org</returns>
        public String[] GenerateSignedStrings(int n, int length, String characters, bool replacement)
        {
            mJSONParams = InitStringParams(n, length, characters, replacement);
            mJSONRequest = InitMethod(SIGNED_STRING_METHOD);
            SendRequest();
            return ExtractStrings();
        }

        /// <summary>
        /// Generates version 4 true random Universally Unique IDentifiers (UUIDs) in accordance with section 4.4 of RFC 4122. 
        /// </summary>
        /// <param name="n">How many UUIDs are needed. Must be within the [1,1e3] range.</param>
        /// <returns>a set of random UUIDS limited by the parameters listed above and generated by random.org</returns>
        public Guid[] GenerateSignedUUIDs(int n)
        {
            mJSONParams = InitUUIDParams(n);
            mJSONRequest = InitMethod(SIGNED_UUID_METHOD);
            SendRequest();
            return ExtractUUIDs();
        }


        /// <summary>
        /// Returns the number of requests left on the quota
        /// </summary>
        /// <returns>The number of remaining requests</returns>
        public int GetRequestsLeft()
        {
            return GetObjectsLeft(RESULT, "requestsLeft");
        }

        /// <summary>
        /// Returns the number of bits left on the quota
        /// </summary>
        /// <returns>The number of remaining bits</returns>
        public int GetBitsLeft()
        {
            return GetObjectsLeft(RESULT, "bitsLeft");
        }
        /// <summary>
        /// Verifies the signature of a response previously received from one of the methods in the Signed API. This is used to examine the authenticity of numbers.
        /// This must be run therefore after a valisd request to retrive signed objects
        /// </summary>
        /// <returns>True or False</returns>
        public bool VerifySignature()
        {
            mJSONParams.RemoveAll();
            mJSONParams.Add(RANDOM, ResultObject.GetValue(RANDOM));
            mJSONParams.Add(SIGNATURE, ResultObject.GetValue(SIGNATURE));
            mJSONRequest = InitMethod(VERIFY_SIGNATURE);
            SendRequest();
            //RandomObject = ((JObject)mJSONResponse.GetValue(RESULT));
            return (bool)((JObject)mJSONResponse.GetValue(RESULT)).GetValue(AUTHENTICITY);
        }

        /// <summary>
        /// Initialise the parameters to put in the JSON request object for integer generation
        /// </summary>
        /// <param name="n"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="replacement"></param>
        /// <returns>An initialised JSON object holding the parameters necessary to generate integers</returns>
        private JObject InitIntegerParams(int n, int min, int max, bool replacement, int baseNum)
        {
            mJSONParams = new JObject(
                    new JProperty(APIKEY, mApiKey),
                    new JProperty("n", n),
                    new JProperty("min", min),
                    new JProperty("max", max),
                    new JProperty("replacement", replacement),
                    new JProperty("base", baseNum)
                    );
            return mJSONParams;
        }

        /// <summary>
        /// Generic GetUsage for quota lookups
        /// </summary>
        /// <returns>JOBject with method params</returns>
        private JObject GetUsage()
        {
            mJSONParams = new JObject(
                new JProperty(APIKEY, mApiKey),
                new JProperty(GET_USAGE_METHOD));
            SendRequest();
            return mJSONResponse;
        }

        /// <summary>
        /// Initialise the parameters to put in the JSON request object for decimal fraction generation
        /// </summary>
        /// <param name="n"></param>
        /// <param name="decimalPlaces"></param>
        /// <param name="replacement"></param>
        /// <returns>An initialised JSON object holding the parameters necessary to generate decimal fractions</returns>
        private JObject InitDecimalFractionParams(int n, int decimalPlaces, bool replacement)
        {
            mJSONParams = new JObject(
                new JProperty(APIKEY, mApiKey),
                new JProperty("n", n),
                new JProperty("decimalPlaces", decimalPlaces),
                new JProperty("replacement", replacement));
            return mJSONParams;
        }


        /// <summary>
        /// Initialise the parameters to put in the JSON request object for Gaussian generation
        /// </summary>
        /// <param name="n"></param>
        /// <param name="mean"></param>
        /// <param name="standardDeviation"></param>
        /// <param name="significantDigits"></param>
        /// <returns>An initialised JSON object holding the parameters necessary to generate Gaussians</returns>
        private JObject InitGaussiansParams(int n, double mean, double standardDeviation, int significantDigits)
        {
            mJSONParams = new JObject(
                new JProperty(APIKEY, mApiKey),
                new JProperty("n", n),
                new JProperty("mean", mean),
                new JProperty("standardDeviation", standardDeviation),
                new JProperty("significantDigits", significantDigits));
            return mJSONParams;
        }

        /// <summary>
        /// Initialise the parameters to put in the JSON request object for string generation
        /// </summary>
        /// <param name="n"></param>
        /// <param name="length"></param>
        /// <param name="characters"></param>
        /// <param name="replacement"></param>
        /// <returns>An initialised JSON object holding the parameters necessary to generate strings</returns>
        private JObject InitStringParams(int n, int length, String characters, bool replacement)
        {
            mJSONParams = new JObject(
                new JProperty(APIKEY, mApiKey),
                new JProperty("n", n),
                new JProperty("length", length),
                new JProperty("characters", characters),
                new JProperty("replacement", replacement));
            return mJSONParams;
        }

        /// <summary>
        /// Initialise the JSON object representing the request to be sent over the network
        /// </summary>
        /// <param name="n">method The name of the method to be invoked on the server</param>
        /// <returns>An initialised JSON object holding the fields that the api methods use</returns>
        private JObject InitUUIDParams(int n)
        {
            mJSONParams = new JObject(
                new JProperty(APIKEY, mApiKey),
                new JProperty("n", n));
            return mJSONParams;
        }

        /// <summary>
        /// Initialise the JSON object representing the request to be sent over the network
        /// </summary>
        /// <param name="method">The name of the method to be invoked on the server</param>
        /// <returns>An initialised JSON object holding the fields that the api methods use</returns>
        private JObject InitMethod(string method)
        {

            mJSONRequest = new JObject(
                new JProperty("jsonrpc", "2.0"),
                new JProperty("method", method),
                new JProperty("params", mJSONParams),
                new JProperty("id", new Random().Next()));
            return mJSONRequest;
        }

        /// <summary>
        /// Wait for advisory delay and make the call to the method that does the actual networking.
        /// </summary>
        private void SendRequest()
        {
            mJSONResponse = null;
            long timeSinceLastRequest = DateTime.Now.Ticks - mLastResponseReceived;
            long waitingTime = mAdvisoryDelay - timeSinceLastRequest;

            if (waitingTime > 0)
            {
                if (waitingTime > mMaxBlockingTime)
                {
                    //if the waiting time advised by random.org is larger than the time the user wants to wait, throw an exception
                    throw new RandomJSONRPCRunTimeException("The advised waiting is higher than the max accepted value");
                }
                else
                {
                    System.Threading.Thread.Sleep(TimeSpan.FromTicks(waitingTime * 10000));
                }
            }

            DoPost();
            ErrorCheck();

            //store the time when the response is received (unless the response is an error or the response of a getUsage request)
            //set the delay if needed
            if (mJSONResponse.GetValue("result").SelectToken("advisoryDelay", false) != null)
            {
                mLastResponseReceived = DateTime.Now.Ticks;
                mAdvisoryDelay = Convert.ToInt64(mJSONResponse.GetValue("result").SelectToken("advisoryDelay"));
            }
        }

        /// <summary>
        /// Do the actual connect() call to to open the connection and send the data over the network
        /// </summary>
	    private void DoPost()
        {
            Stream requestStream = null; ;
            StreamReader reader = null; ;
            WebResponse response = null;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
            //set HTTP properties
            request.Method = "POST";
            request.ContentType = CONTENT_TYPE;

            byte[] bytes = Encoding.UTF8.GetBytes(mJSONRequest.ToString());

            try
            {
                using (requestStream = request.GetRequestStream())
                {
                    requestStream.Write(bytes, 0, bytes.Length);

                    using (response = request.GetResponse())
                    {
                        using (reader = new StreamReader(response.GetResponseStream()))
                        {
                            string content = reader.ReadToEnd();
                            mJSONResponse = JObject.Parse(content);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw new RandomJSONRPCRunTimeException("Code: 9999", e);
            }
            finally
            {
                // Clean up resources
                requestStream.Flush();
                requestStream.Close();
                reader.Close();
                response.Close();
            }
        }

        /// <summary>
        /// Check if an error occurred and in that case throw the appropriate exception
        /// </summary>
        private void ErrorCheck()
        {
            JObject error;
            if (!(mJSONResponse.GetValue("error") != null))
                return;
            else
            {
                error = (JObject)mJSONResponse.GetValue("error");
                int errorCode = (int)error.SelectToken("code");
                string message = (string)error.SelectToken("message");
                //the cases where an illegal argument has been supplied by the user
                if (errorCode == 200 || errorCode == 201 || errorCode == 202 || errorCode == 203 || errorCode == 300 || errorCode == 301 || errorCode == 301 || errorCode == 400 || errorCode == 401)
                    throw new RandomJSONRPCException("Code: " + Convert.ToString(errorCode) + ". Message: " + message);
                //the case where an unknown error occurred, or an error that has nothing to do with the parameters supplied by the client occurred
                throw new RandomJSONRPCRunTimeException("Code: " + Convert.ToString(errorCode) + ". Message: " + message);
            }
        }

        /// <summary>
        /// Returns the number of objects left on the quota
        /// </summary>
        /// <returns></returns>
        private int GetObjectsLeft(string value, string token)
        {
            if (mJSONResponse == null || DateTime.Now.Ticks > mLastResponseReceived + ONE_HOUR_IN_MILLIS)
                GetUsage();
            JObject resultObject = (JObject)mJSONResponse.GetValue(value);
            return (int)resultObject.GetValue(token);
        }

        /// <summary>
        /// Extract integers from the JSON response object
        /// </summary>
        /// <returns>An array containing the integers</returns>
        private int[] ExtractInts()
        {
            JArray dataArray = UnwrapJSONResponse();
            int length = dataArray.Count();
            int i = 0;
            int[] result = new int[length];
            while (i < length)
            {
                result[i] = (int)dataArray[i];
                i++;
            }
            return result;
        }

        /// <summary>
        /// Extract doubles from the JSON response object
        /// </summary>
        /// <returns>An array containing the doubles</returns>
        private double[] ExtractDoubles()
        {
            JArray dataArray = UnwrapJSONResponse();
            int length = dataArray.Count();
            int i = 0;
            double[] result = new double[length];
            while (i < length)
            {
                result[i] = (Double)dataArray[i];
                i++;
            }
            return result;
        }

        /// <summary>
        /// Extract strings from the JSON response object
        /// </summary>
        /// <returns>An array containing the strings</returns>
        private String[] ExtractStrings()
        {
            JArray dataArray = UnwrapJSONResponse();
            int length = dataArray.Count();
            int i = 0;
            string[] result = new string[length];
            while (i < length)
            {
                result[i] = (string)dataArray[i];
                i++;
            }
            return result;
        }

        /// <summary>
        /// Extract UUIDs from the JSON response object
        /// </summary>
        /// <returns>An array containing the UUIDs</returns>
        private Guid[] ExtractUUIDs()
        {
            JArray dataArray = UnwrapJSONResponse();
            int length = dataArray.Count();
            int i = 0;
            Guid[] result = new Guid[length];
            while (i < length)
            {
                result[i] = (Guid)dataArray[i];
                i++;
            }
            return result;
        }

        /// <summary>
        /// Unwrap the data from inside the result and random fields
        /// </summary>
        /// <returns>The JSON object with the data</returns>
        private JArray UnwrapJSONResponse()
        {
            if (!mJSONResponse.GetValue("result").HasValues)
            {
                throw new RandomJSONRPCException("Code: 9999" + ". Message: Request is valid but Response result payload is null");
            }
            // allows later interrogation for signed methods
            ResultObject = (JObject)mJSONResponse.GetValue(RESULT);
            RandomObject = (JObject)ResultObject.GetValue(RANDOM);
            return (JArray)RandomObject.GetValue("data");
        }

        /// <summary>
        /// Getters/Setters for the JSON responses
        /// </summary>
        private JObject ResultObject { get; set; }

        private JObject RandomObject { get; set; }


    }
}

