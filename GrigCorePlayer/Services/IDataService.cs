using GrigCoreLastfm.API.Types;
using GrigCoreVkApi.Types;

namespace GrigCorePlayer.Services
{
    public interface IDataService
    {
        /// <summary>
        /// Get last.fm session from settings
        /// </summary>
        /// <returns></returns>
        Session GetLfmSessionFromSettings();

        /// <summary>
        /// Get vk session from settings
        /// </summary>
        /// <returns></returns>
        VkSession GetVkSessionFromSettings();

        /// <summary>
        /// Set last.fm session to settings
        /// </summary>
        /// <param name="session"></param>
        void WriteLfmSessionToSettings(Session session);

        /// <summary>
        /// Clear last.fm login data from settings
        /// </summary>
        void ClearLfmSessionDataFromSettings();
    }
}
