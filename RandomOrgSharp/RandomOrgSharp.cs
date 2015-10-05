using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Obacher.RandomOrgSharp
{
   public class RandomOrgSharp
    {
        // Values which identifies information for random.org
       private const string INTEGER_METHOD = "generateIntegers";
       private const string DECIMALFRACTION_METHOD = "generateDecimalFractions";
       private const string GAUSSIAN_METHOD = "generateGaussians";
       private const string STRING_METHOD = "generateStrings";
       private const string UUID_METHOD = "generateUUIDs";
       private const string SIGNED_INTEGER_METHOD = "generateSignedIntegers";
       private const string SIGNED_DECIMALFRACTION_METHOD = "generateSignedDecimalFractions";
       private const string SIGNED_GAUSSIAN_METHOD = "generateSignedGaussians";
       private const string SIGNED_STRING_METHOD = "generateSignedStrings";
       private const string SIGNED_UUID_METHOD = "generateSignedUUIDs";
       private const string VERIFY_SIGNATURE = "verifySignature";
       private const string GET_USAGE_METHOD = "getUsage";

       private const string SIGNATURE = "signature";
       private const string AUTHENTICITY = "authenticity";

       private const string RESULT_KEY = "result";
       private const string RANDOM_KEY = "random";
       private const string DATA_KEY = "data";
       private const string BITSUSED_KEY = "bitsUsed";
       private const string BITSLEFT_KEY = "bitsLeft";
       private const string REQUESTSLEFT_KEY = "requestsLeft";
       private const string ADVISORYDELAY_KEY = "advisoryDelay";
       private const string APIKEY_KEY = "apiKey";
       private const string APIKEY_VALUE = "00000000-0000-0000-0000-000000000000";

       private readonly IRandomOrgWrapper _wrapper;

        public RandomOrgSharp()
        {
            _wrapper = new RandomOrgWrapper();
        }

        public RandomOrgSharp(IRandomOrgWrapper randomOrgWrapper)
       {
           _wrapper = randomOrgWrapper;
       }

        /// <summary>
        /// Generic GetUsage for quota lookups
        /// </summary>
        /// <returns>JOBject with method params</returns>
        public JObject GetUsage()
        {
            var jsonParams = new JObject(
                new JProperty(APIKEY_KEY, APIKEY_VALUE),
                new JProperty(GET_USAGE_METHOD));
            var jsonResponse = _wrapper.SendRequest(jsonRequest);
            return jsonResponse;
        }

        /// <summary>
        /// Check if an error occurred and in that case throw the appropriate exception
        /// </summary>
        private void ParseError(JObject jsonResponse)
        {
            if (jsonResponse.GetValue("error") != null)
            {
                var error = (JObject)jsonResponse.GetValue("error");
                var errorCode = (int)error.SelectToken("code");
                var message = (string)error.SelectToken("message");
                
                //the cases where an illegal argument has been supplied by the user
                if (errorCode == 200 || errorCode == 201 || errorCode == 202 || errorCode == 203 || errorCode == 300 || errorCode == 301 || errorCode == 301 || errorCode == 400 || errorCode == 401)
                    throw new RandomOrgException(errorCode, message);
                
                //the case where an unknown error occurred, or an error that has nothing to do with the parameters supplied by the client occurred
                throw new RandomOrgRunTimeException(errorCode, message);
            }
        }

        /// <summary>
        /// Unwrap the data from inside the result and random fields
        /// </summary>
        /// <returns>The JSON object with the data</returns>
        private JArray ParseResult(JObject jsonResponse)
        {
            if (jsonResponse == null)
                return new JArray();

            if (!jsonResponse.GetValue("result").HasValues)
                throw new RandomOrgException(9999, "Message: Request is valid but Response result payload is null");

            var result = jsonResponse.GetValue(RESULT_KEY) as JObject;
            var randomValue = result?.GetValue(RANDOM_KEY) as JObject;
            JArray dataValue = randomValue?.GetValue(DATA_KEY) as JArray;

            return dataValue;

        }

       private JObject ParseBitsLeft(JObject jsonResponse)
       {
 {
            if (jsonResponse == null || DateTime.Now.Ticks > _lastResponseReceived + ONE_HOUR_IN_MILLIS)
                GetUsage();
            JObject resultObject = (JObject)_jsonResponse.GetValue(value);
            return (int)resultObject.GetValue(token);
        }
    }

        ReturnMessage 
    }
}
