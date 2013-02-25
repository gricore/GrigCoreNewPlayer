using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GrigCorePlayer.Services
{
    public interface IVkService
    {
        /// <summary>
        /// Get url by track and artist.
        /// </summary>
        /// <param name="track"></param>
        /// <param name="artist"></param>
        /// <returns></returns>
        string GetUrlByTrackAndArtist(string track);
    }
}
