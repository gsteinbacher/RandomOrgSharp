using System;
using System.Linq;
using Newtonsoft.Json.Linq;
using Obacher.RandomOrgSharp.Error;
using Obacher.RandomOrgSharp.Parameter;

namespace Obacher.RandomOrgSharp.Response
{
    public class ErrorHandlerThrowException : IErrorHandler, IResponse
    {
        public int Code { get; private set; }
        public string Message { get; private set; }
        public int Id { get; private set; }
        private bool _hasError;

        #region IResponse implementation

        public bool Process(JObject json, IParameters parameters)
        {
            _hasError = false;

            Id = JsonHelper.JsonToInt(json.GetValue("id"));

            var result = json.GetValue(RandomOrgConstants.JSON_ERROR_PARAMETER_NAME) as JObject;
            if (result != null)
            {
                _hasError = true;
                Code = JsonHelper.JsonToInt(result.GetValue(RandomOrgConstants.JSON_CODE_PARAMETER_NAME));
                var data = result.GetValue(RandomOrgConstants.JSON_DATA_PARAMETER_NAME);

                if (!data.HasValues)
                    Message = ResourceHelper.GetString(StringsConstants.ERROR_CODE_KEY + Code);
                else
                {
                    var unformattedMessage = ResourceHelper.GetString(StringsConstants.ERROR_CODE_KEY + Code);
                    if (!string.IsNullOrWhiteSpace(unformattedMessage))
                        Message = string.Format(unformattedMessage, data.Values<object>().ToArray());
                }

                if (string.IsNullOrWhiteSpace(Message))
                    Message = JsonHelper.JsonToString(result.GetValue(RandomOrgConstants.JSON_MESSAGE_PARAMETER_NAME));

                throw new RandomOrgException(Code, Message);
            }

            // If it gets down to this then there are no errors so we can continue
            return true;
        }

        public bool IsHandlerOfType(string handlerType)
        {
            return string.Equals(handlerType, RandomOrgConstants.HANDLER_TYPE_ERROR, StringComparison.InvariantCultureIgnoreCase);
        }

        #endregion

        #region IErrorHandler implementation

        public bool HasError(JObject json)
        {
            return _hasError;
        }

        #endregion
    }
}