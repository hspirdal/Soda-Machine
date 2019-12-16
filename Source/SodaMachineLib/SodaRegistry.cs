using System;
using System.Collections.Generic;
using System.Linq;

namespace SodaMachineLib
{
	public interface ISodaRegistry
	{
		Soda Lookup(string sodaType);
		bool ValidType(string sodaType);
		List<string> ValidSodaTypes { get; }
	}

	public class SodaRegistry : ISodaRegistry
	{
		private Dictionary<string, Soda> _sodaMap;

		public SodaRegistry(Dictionary<string, Soda> sodaMap)
		{
			_sodaMap = sodaMap;
		}

		public Soda Lookup(string sodaType)
		{
			if (!_sodaMap.ContainsKey(sodaType))
			{
				throw new ArgumentException($"Missing soda of type {sodaType}.");
			}

			return _sodaMap[sodaType];
		}

		public bool ValidType(string sodaType)
		{
			return _sodaMap.ContainsKey(sodaType);
		}

		public List<Soda> Create(string sodaType, int amount)
		{
			if (!ValidType(sodaType))
			{
				throw new ArgumentException($"{sodaType} is not a valid soda");
			}

			var sodasToCreate = new List<Soda>();
			for (var i = 0; i < amount; ++i)
			{
				var createdSoda = _sodaMap[sodaType].Create();
				sodasToCreate.Add(createdSoda);
			}

			return sodasToCreate;
		}

		public List<string> ValidSodaTypes => _sodaMap.Keys.ToList();
	}
}