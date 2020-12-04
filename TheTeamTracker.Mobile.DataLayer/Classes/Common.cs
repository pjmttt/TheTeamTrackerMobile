using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace TheTeamTracker.Mobile.DataLayer.Classes
{
	// TODO: bring in pajamacommon
	public class Common
	{
		public static T Clone<T>(T obj)
		{
			return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(obj));
		}
	}
}
