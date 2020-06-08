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
        public Config GetConfig(string Key)
        {
            using(var context = new GrovityContext())
            {
                return context.Configurations.Find(Key);
            }
        }
    }
}
