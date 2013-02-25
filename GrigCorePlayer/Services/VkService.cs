using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GrigCorePlayer.Annotations;
using GrigCoreVkApi.Types;

namespace GrigCorePlayer.Services
{
    [UsedImplicitly]
    public class VkService : IVkService
    {
        private readonly IDataService _dataService;

        public VkService(IDataService dataService)
        {
            _dataService = dataService;
        }

        /// <summary>
        /// Get mp3 url by track and artist.
        /// </summary>
        /// <param name="track"></param>
        /// <returns></returns>
        public string GetUrlByTrackAndArtist(string track)
        {
            try
            {
                var session = _dataService.GetVkSessionFromSettings();
                var audio = new Audio(session);
                return audio.GetMp3UrlList(track).First().Url;
            }
            catch (Exception)
            {
                return string.Empty;
            }

        }
    }
}
