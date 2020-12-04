using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using TheTeamTracker.Mobile.DataLayer.Classes;

namespace TheTeamTracker.Mobile
{
    public class Config
    {
        public static Configuration Instance { get; set; }

        public static void LoadConfig(string configString)
        {
            Instance = JsonConvert.DeserializeObject<Configuration>(configString);
        }
    }
}
