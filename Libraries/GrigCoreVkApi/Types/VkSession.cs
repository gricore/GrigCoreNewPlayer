using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows;

namespace GrigCoreVkApi.Types
{
    public class VkSession
    {
        #region Fields

        private string access_token = string.Empty;
        /// <summary>
        /// Get access_token
        /// </summary>
        public string Access_token
        {
            get
            {
                if (string.IsNullOrWhiteSpace(access_token))
                {
                    access_token = GetAccessToken("audio,offline");
                }

                return access_token;
            }
            private set { access_token = value; }
        }

        /// <summary>
        /// Gets app_id
        /// </summary>
        public string APP_ID { get; private set; }

        #endregion

        #region Methods

        public string GetAccessToken(string settings)
        {
            string accesstoken = string.Empty;

            Parameters Params = new Parameters();

            Params.AddParams("client_id", APP_ID);
            Params.AddParams("scope", settings);
            Params.AddParams("redirect_uri", "http://oauth.vk.com/blank.html");
            Params.AddParams("display", "page");
            Params.AddParams("response_type", "token");

            Request req = new Request(Params);           

            return req.RequestURL;
        }

        #endregion


        #region Constructors

        public VkSession(string app_id)
        {
            this.APP_ID = app_id;
        }

        public VkSession(string app_id, string _access_token)
        {
            this.APP_ID = app_id;
            Access_token = _access_token;
        }

        #endregion
    }
}
