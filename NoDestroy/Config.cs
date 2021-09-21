using Rocket.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoDestroy
{
    public class Config : IRocketPluginConfiguration
    {
        public bool Enabled = true;
        public void LoadDefaults()
        {
        }
    }
}
