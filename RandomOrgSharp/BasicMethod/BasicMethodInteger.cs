using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Obacher.RandomOrgSharp.RequestParameters;

namespace Obacher.RandomOrgSharp.BasicMethod
{
    public class BasicMethodInteger : IBasicMethod
    {

        private readonly IRandomOrgService _serviceService;

        public BasicMethodInteger(IRandomOrgService serviceService)
        {
            _serviceService = serviceService;
        }

        public IResponse Execute(IRequestParameters requestParameters)
        {
            JObject jsonRequest = requestParameters.CreateJsonRequest();
            JObject jsonReponse = _serviceService.SendRequest(jsonRequest);

            var response = RandomOrgIntegerResponse.Parse(jsonReponse);

            return response;
        }


        public async Task<IResponse> ExecuteAsync(IRequestParameters requestParameters)
        {
            JObject jsonRequest = requestParameters.CreateJsonRequest();
            JObject jsonReponse = await _serviceService.SendRequestAsync(jsonRequest);

            var response = RandomOrgIntegerResponse.Parse(jsonReponse);

            return response;
        }

        public bool CanExecute(MethodType methodType)
        {
            return methodType == MethodType.Integer;
        }
    }
}
