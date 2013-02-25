using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GrigCoreLastfm.API.Types;
using GrigCorePlayer.Annotations;
using GrigCoreVkApi.Types;

namespace GrigCorePlayer.Services
{
    [UsedImplicitly]
    public class DataService : IDataService
    {
        /// <summary>
        /// Gets a last.fm session from settings file.
        /// </summary>
        /// <returns></returns>
        public Session GetLfmSessionFromSettings()
        {
            return new Session
                {
                    ApiKey = Properties.Settings.Default.api_key,
                    ApiSec = Properties.Settings.Default.api_sec,
                    ApiSig = Properties.Settings.Default.lfm_apisig,
                    Key = Properties.Settings.Default.lfm_sessionKey,
                    Token = Properties.Settings.Default.lfm_token,
                    Name = Properties.Settings.Default.lfm_username
                };
        }

        /// <summary>
        /// Gets a vk session from settings file
        /// </summary>
        /// <returns></returns>
        public VkSession GetVkSessionFromSettings()
        {
            var session = new VkSession(Properties.Settings.Default.vk_id, Properties.Settings.Default.vk_access_token);
            return session;
        }

        /// <summary>
        /// Set lfm session data to settings
        /// </summary>
        /// <param name="session"></param>
        public void WriteLfmSessionToSettings(Session session)
        {
            Properties.Settings.Default.lfm_apisig = session.ApiSig;
            Properties.Settings.Default.Save();
            Properties.Settings.Default.Reload();

            Properties.Settings.Default.lfm_sessionKey = session.Key;
            Properties.Settings.Default.Save();
            Properties.Settings.Default.Reload();

            Properties.Settings.Default.lfm_token = session.Token;
            Properties.Settings.Default.Save();
            Properties.Settings.Default.Reload();

            Properties.Settings.Default.lfm_username = session.Name;
            Properties.Settings.Default.Save();
            Properties.Settings.Default.Reload();
        }

        /// <summary>
        /// Clear last.fm login data from settings
        /// </summary>
        public void ClearLfmSessionDataFromSettings()
        {
            var session = new Session();
            WriteLfmSessionToSettings(session);
        }
    }
}
