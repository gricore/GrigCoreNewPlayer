using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GrigCoreVkApi.Types
{
    public class Request
    {
        private const string BaseAuthUri = "https://oauth.vk.com/authorize?";
        private const string VkBaseUri = "https://api.vk.com/method/";

        // Parametres
        private Dictionary<string, string> DParamters = new Dictionary<string, string>();

        private string requestURL = string.Empty;
        internal string RequestURL { get { return requestURL; } private set { requestURL = value; } }

        internal Request(Parameters Paramters)
        {
            RequestURL = BaseAuthUri;

            DParamters = Paramters.GetParametres();

            if (DParamters.Count > 0)
            {
                foreach (KeyValuePair<string, string> pair in DParamters)
                {
                    RequestURL += (pair.Key + "=" + pair.Value);
                }
            }
            //RequestURL += "&api_key=" + Session.Api_key;
        }

        internal Request(Parameters Paramters, string method, VkSession vk_session)
        {
            RequestURL = VkBaseUri+method;

            DParamters = Paramters.GetParametres();

            if (DParamters.Count > 0)
            {
                foreach (KeyValuePair<string, string> pair in DParamters)
                {
                    RequestURL += (pair.Key + "=" + pair.Value);
                }
            }
            RequestURL += "&access_token=" + vk_session.Access_token;
        }
    }
}
