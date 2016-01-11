using System.Linq;
using Newtonsoft.Json.Linq;
using Obacher.RandomOrgSharp.Core;
using Obacher.RandomOrgSharp.Core.Response;

namespace Obacher.RandomOrgSharp.JsonRPC.Response
{
    public class ErrorHandlerThrowException : IErrorHandler
    {
        public int Code { get; private set; }
        public string Message { get; private set; }
        public int Id { get; private set; }

        private bool _hasError;
        public bool HasError()
        {
            return _hasError;
        }


        public void Process(string response)
        {
            _hasError = false;

            JObject json = JObject.Parse(response);

            Id = JsonHelper.JsonToInt(json.GetValue("id"));

            var result = json.GetValue(JsonRpcConstants.ERROR_PARAMETER_NAME) as JObject;
            if (result != null)
            {
                _hasError = true;
                Code = JsonHelper.JsonToInt(result.GetValue(JsonRpcConstants.CODE_PARAMETER_NAME));
                var data = result.GetValue(JsonRpcConstants.DATA_PARAMETER_NAME);

                if (!data.HasValues)
                    Message = ResourceHelper.GetString(StringsConstants.ERROR_CODE_KEY + Code);
                else
                {
                    var unformattedMessage = ResourceHelper.GetString(StringsConstants.ERROR_CODE_KEY + Code);
                    if (!string.IsNullOrWhiteSpace(unformattedMessage))
                        Message = string.Format(unformattedMessage, data.Values<object>().ToArray());
                }

                if (string.IsNullOrWhiteSpace(Message))
                    Message = JsonHelper.JsonToString(result.GetValue(JsonRpcConstants.MESSAGE_PARAMETER_NAME));

                throw new RandomOrgException(Code, Message);
            }
        }
    }
}