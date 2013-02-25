using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GrigCoreVkApi.Types
{
    public class Parameters
    {
        #region Fields

        Dictionary<string, string> Params = new Dictionary<string, string>();

        #endregion

        #region Methods

        public void AddParams(string Key, string Value)
        {
            if (Params.Count == 0)
            {

            }
            else
            {
                Key = "&" + Key;
            }

            Params.Add(Key, Value);
        }

        internal Dictionary<string, string> GetParametres()
        {
            return Params;
        }      

        #endregion
    }
}
