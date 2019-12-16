using System;
using System.Collections.Generic;

namespace SodaMachineLib
{
	public interface ISodaContainer
	{
		void Append(Soda soda);
		Soda Remove(string sodaTypeToRemove);
		int GetAmountForType(string sodaType);
	}
	public class SodaContainer : ISodaContainer
	{
		private readonly int MaxStoredBottlesPerType;
		private Dictionary<string, Queue<Soda>> _storedSodaMap = new Dictionary<string, Queue<Soda>>();

		public SodaContainer()
		{
			MaxStoredBottlesPerType = 50;
			var sodasToStore = new List<Soda> { new Cola(), new Cola() };
			StoreSodas(sodasToStore);
		}

		public SodaContainer(List<Soda> sodasToStore)
		{
			MaxStoredBottlesPerType = 50;
			StoreSodas(sodasToStore);
		}

		public SodaContainer(int maxStoredBottlesPerType, List<Soda> sodasToStore)
		{
			if (maxStoredBottlesPerType < 1 || maxStoredBottlesPerType > int.MaxValue)
			{
				throw new ArgumentException("Max amount of bottles to be stored per type must be a postive integer.");
			}

			MaxStoredBottlesPerType = maxStoredBottlesPerType;
			StoreSodas(sodasToStore);
		}

		public void Append(Soda soda)
		{
			var sodaType = soda.Name.ToLower();
			if (!_storedSodaMap.ContainsKey(sodaType))
			{
				_storedSodaMap.Add(sodaType, new Queue<Soda>());
			}

			if (_storedSodaMap[sodaType].Count >= MaxStoredBottlesPerType)
			{
				throw new ArgumentException($"Cannot add soda as container is full for soda of type {sodaType}.");
			}

			_storedSodaMap[sodaType].Enqueue(soda);
		}

		public Soda Remove(string sodaTypeToRemove)
		{
			sodaTypeToRemove = sodaTypeToRemove.ToLower();
			if (!_storedSodaMap.ContainsKey(sodaTypeToRemove))
			{
				throw new ArgumentException($"Soda of type {sodaTypeToRemove} is not stored.");
			}

			if (_storedSodaMap[sodaTypeToRemove].Count == 0)
			{
				throw new ArgumentException($"Out of sodas for type {sodaTypeToRemove}.");
			}

			return _storedSodaMap[sodaTypeToRemove].Dequeue();
		}

		public int GetAmountForType(string sodaType)
		{
			sodaType = sodaType.ToLower();
			if (!_storedSodaMap.ContainsKey(sodaType.ToLower()))
			{
				return 0;
			}

			return _storedSodaMap[sodaType].Count;
		}

		private void StoreSodas(List<Soda> sodasToStore)
		{
			foreach (var soda in sodasToStore)
			{
				Append(soda);
			}
		}
	}
}