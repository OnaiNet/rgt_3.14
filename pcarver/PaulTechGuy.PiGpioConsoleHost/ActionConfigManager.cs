using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;

namespace PaulTechGuy.PiGpioConsoleHost
{
	public class ActionConfigManager
	{
		private readonly string _configDirectory;
		private readonly Dictionary<string, dynamic> _configItems = new Dictionary<string, dynamic>();

		public ActionConfigManager(string configFileDirectory)
		{
			_configDirectory = configFileDirectory;
		}

		private void LoadFiles()
		{
			_configItems.Clear(); // just in case...be defensive
			foreach(var filename in Directory.EnumerateFiles(_configDirectory, "*Config.json"))
			{
				dynamic config = JsonConvert.DeserializeObject<dynamic>(File.ReadAllText(filename));
				var typeName = Path.GetFileNameWithoutExtension(filename);
				typeName = typeName.Substring(0, typeName.Length - "Config".Length);
				_configItems.Add(typeName, config);

				Console.WriteLine($"{this.GetType().Name} added configuration: {typeName}");
			}
		}

		public void Start()
		{
			LoadFiles();
			StartFileWatcher();
		}

		public void Stop()
		{
			StopFileWatcher();
			_configItems.Clear();
		}

		private void StartFileWatcher()
		{
		}

		private void StopFileWatcher()
		{
		}

		public dynamic this[string typeName]
		{
			get
			{
				return _configItems[typeName];
			}
		}

		public bool Exists(string typeName)
		{
			return _configItems.ContainsKey(typeName);
		}
	}
}
