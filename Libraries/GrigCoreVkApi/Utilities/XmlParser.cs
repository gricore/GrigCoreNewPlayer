using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Xml;

namespace GrigCoreVkApi.Utilities
{
    public class XmlParser
    {
        #region Fields

        /// <summary>
        /// Gets or set req url
        /// </summary>
        public string Req_url { get; set; }

        private XmlDocument doc = new XmlDocument();

        private XmlNode sup_nod;

        #endregion

        #region Methods

        public XmlDocument GetDocument()
        {
            return this.doc;
        }

        public string GetAid()
        {
            try { return sup_nod.GetNodeByTheTagName("aid").InnerText; }
            catch { return string.Empty; }
        }

        public string GetUrl()
        {
            try { return sup_nod.GetNodeByTheTagName("url").InnerText; }
            catch { return string.Empty; }
        }

        public string GetOwnerId()
        {
            try { return sup_nod.GetNodeByTheTagName("owner_id").InnerText; }
            catch { return string.Empty; }
        }

        public string GetArtist()
        {
            try { return sup_nod.GetNodeByTheTagName("artist").InnerText; }
            catch { return string.Empty; }
        }

        public string GetTitle()
        {
            try { return sup_nod.GetNodeByTheTagName("title").InnerText; }
            catch { return string.Empty; }
        }

        public string GetDuration()
        {
            try { return sup_nod.GetNodeByTheTagName("duration").InnerText; }
            catch { return string.Empty; }
        }

        public XmlNode GetNodeByTagName(string tag_name)
        {
            MessageBox.Show(doc.InnerXml);
            XmlNode nodeList = doc.GetElementsByTagName("response").Item(0);
         
            XmlNode retNode = null;

            foreach (XmlNode node in nodeList)
            {
                if (node.Name == tag_name)
                    retNode = node;
            }

            return retNode;
        }

        public void LoadDocument()
        {
            doc = new XmlDocument();
            try { doc.Load(this.Req_url); }
            catch { doc = new XmlDocument(); }
        }

        #endregion


        #region Constr

        public XmlParser(string req_url)
        {
            this.Req_url = req_url;
        }

        public XmlParser(XmlNode _sup_node)
        {
            this.sup_nod = _sup_node;
        }

        #endregion
    }

    public static class xmlTools
    {
        internal static XmlNode GetNodeByTheTagName(this XmlNode currentNode, string tagName)
        {
            XmlNode retNode = currentNode.Clone();

            bool oneFinded = false;

            if (currentNode.HasChildNodes)
            {
                foreach (XmlNode node in currentNode)
                {
                    if (node.Name == tagName)
                    {
                        retNode = node;
                        oneFinded = true;
                        break;
                    }
                }
            }

            if (!oneFinded)
            {
                retNode.InnerXml = string.Empty;
            }

            return retNode;
        }
    }
}
