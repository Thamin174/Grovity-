using Grovity.Database;
using Grovity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grovity.Services
{
    public class ConfigurationService
    {
        #region Singleton
        public static ConfigurationService Instance
        {
            get
            {
                if (privateInMemoryObject == null) privateInMemoryObject = new ConfigurationService();

                return privateInMemoryObject;
            }
        }
        private static ConfigurationService privateInMemoryObject { get; set; }

        private ConfigurationService()
        {
        }
        #endregion
        public Config GetConfig(string Key)
        {
            using(var context = new GrovityContext())
            {
                return context.Configurations.Find(Key);
            }
        }
    }
}
