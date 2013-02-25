using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Xml;
using GrigCoreVkApi.Utilities;

namespace GrigCoreVkApi.Types
{
    public class Audio
    {
        #region Fields

        VkSession vk_session;

        private Track[] Tracks { get; set; }

        #endregion

        #region Methods

        public Track[] GetMp3UrlList(string title)
        {
            List<Track> tracks = new List<Track>();

            Parameters Params = new Parameters();
            Params.AddParams("q", title);

            Request req = new Request(Params, "audio.search.xml?", vk_session);

            XmlDocument doc = new XmlDocument();
            doc.Load(req.RequestURL);
            foreach(XmlNode node in doc.GetElementsByTagName("response").Item(0))
            {
                if (node.Name == "count") continue;
              
                XmlParser parser = new XmlParser(node);

                tracks.Add(new Track()
                {
                    Artist = parser.GetArtist(),
                    Title = parser.GetTitle(),
                    AID = parser.GetAid(),
                    Duration = parser.GetDuration(),
                    Owner_id = parser.GetOwnerId(),
                    Url = parser.GetUrl()
                });
            }

            this.Tracks = tracks.ToArray();

            return tracks.ToArray();
        }

        #endregion


        #region Constructors

        public Audio(VkSession session)
        {
            this.vk_session = session;
        }

        #endregion
    }
}
