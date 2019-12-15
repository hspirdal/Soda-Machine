using System;

namespace SodaMachineLib
{
	public interface ISodaMachine
	{
		void InsertCoin(int money);
		void RecallAllCoin();
		int StoredCoin { get; }
	}

	public class SodaMachine : ISodaMachine
	{
		public int StoredCoin { get; private set; }

		public void InsertCoin(int money)
		{
			StoredCoin += money;
		}

		public void RecallAllCoin()
		{
			throw new NotImplementedException();
		}
	}
}
