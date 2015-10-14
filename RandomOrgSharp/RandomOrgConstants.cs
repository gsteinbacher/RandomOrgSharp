using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Obacher.RandomOrgSharp
{
    public static class RandomOrgConstants
    {
        public const string APIKEY_KEY = "apiKey";
        public const string APIKEY_VALUE = "a0fc5d8d-8812-4093-851d-5bf64b4310d0";

        public const string INTEGER_METHOD = "generateIntegers";

        // Parameter names that are used in the JSON request object.
        public const string JSON_RPC_PARAMETER_NAME = "jsonrpc";
        public const string JSON_RPC_VALUE = "2.0";
        public const string JSON_METHOD_PARAMETER_NAME = "method";
        public const string JSON_PARAMETERS_PARAMETER_NAME = "params";
        public const string JSON_ID_PARAMETER_NAME = "id";

        public const string JSON_NUMBER_ITEMS_RETURNED_PARAMETER_NAME = "n";
        public const string JSON_MINIMUM_VALUE_PARAMETER_NAME = "min";
        public const string JSON_MAXIMUM_VALUE_PARAMETER_NAME = "max";
        public const string JSON_REPLACEMENT_PARAMETER_NAME = "replacement";
        public const string JSON_BASE_NUMBER_PARAMETER_NAME = "base";

        // Parameter names that are used in the JSON response object.
        public const string JSON_RESULT_PARAMETER_NAME = "result";
        public const string JSON_RANDOM_PARAMETER_NAME = "random";
        public const string JSON_DATA_PARAMETER_NAME = "data";
        public const string JSON_COMPLETION_TIME_PARAMETER_NAME = "completionTime";
        public const string JSON_BITS_USED_PARAMETER_NAME = "bitsUsed";
        public const string JSON_STATUS_PARAMETER_NAME = "status";
        public const string JSON_CREATION_TIME_PARAMETER_NAME = "creationTime";
        public const string JSON_ADVISORY_DELAY_PARAMETER_NAME = "advisoryDelay";
        public const string JSON_BITS_LEFT_PARAMETER_NAME = "bitsLeft";
        public const string JSON_REQUESTS_LEFT_PARAMETER_NAME = "requestsLeft";
        public const string JSON_TOTAL_BITS_PARAMETER_NAME = "totalBits";
        public const string JSON_TOTAL_REQUESTS_PARAMETER_NAME = "totalRequests";

        // Parameter names that are used in the JSON error response object
        public const string JSON_ERROR_PARAMETER_NAME = "error";
        public const string JSON_CODE_PARAMETER_NAME = "code";
        public const string JSON_MESSAGE_PARAMETER_NAME = "message";

        // Possible status values returned in JSON response
        public const string JSON_STATUS_STOPPED = "stopped";
        public const string JSON_STATUS_PAUSED = "paused";
        public const string JSON_STATUS_RUNNING = "running";

    }
}
