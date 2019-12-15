using System;

namespace SodaMachineLib
{
	public interface ICashRegister
	{
		void StoreMoney(int money);
		int RecallStoredMoney();
		int StoredMoney { get; }
	}

	// Can only receive coins (no paper money) and therefore has a limit to how much money can be stored at any one time.
	// To simplify we assume that any amount that is between 1 and max amount is a valid coin insertion. In a more complex project, it would make sense to 
	// introduce a coin type or somesuch.
	public class CoinBasedCashRegister : ICashRegister
	{
		public int StoredMoney { get; private set; }
		public readonly int MaxAmountOfMoneyThatCanBeStored;

		public CoinBasedCashRegister()
		{
			MaxAmountOfMoneyThatCanBeStored = 5000;
		}

		public CoinBasedCashRegister(int maxAmountOfMoneyThatCanBeStored)
		{
			if (maxAmountOfMoneyThatCanBeStored < 1 || maxAmountOfMoneyThatCanBeStored > int.MaxValue)
			{
				throw new ArgumentException("Max amount of money that can be stored must be a postive integer.");
			}

			MaxAmountOfMoneyThatCanBeStored = maxAmountOfMoneyThatCanBeStored;
		}

		public void StoreMoney(int money)
		{
			if (money < 1)
			{
				throw new ArgumentException("Must add positive amount of money.");
			}

			if (money + StoredMoney > MaxAmountOfMoneyThatCanBeStored)
			{
				throw new ArgumentException("Transaction failed because cash register is full.");
			}

			StoredMoney += money;
		}
		public int RecallStoredMoney()
		{
			var returnedMoney = StoredMoney;
			StoredMoney = 0;
			return returnedMoney;
		}

	}
}