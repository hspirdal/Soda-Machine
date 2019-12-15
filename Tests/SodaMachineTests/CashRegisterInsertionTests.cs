using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SodaMachineLib;

namespace SodaMachineTests
{
	[TestClass]
	public class CashRegisterInsertionTests
	{
		private const int MaxMoneyThatCanBeStoredInRegister = 5000;

		[TestMethod]
		public void WhenAddingTwentyCoinsOfMoney_ThenSodaMachineStoresTwentyCoinsOfMoney()
		{
			var cashRegister = new CoinBasedCashRegister();

			var amountToStore = 20;

			cashRegister.StoreMoney(amountToStore);
			var storedAmount = cashRegister.StoredMoney;

			Assert.IsTrue(storedAmount == amountToStore);
		}

		[TestMethod]
		public void WhenAddingNegativeAmountOfMoney_ThenSodaMachineThrowsArgumentException()
		{
			var cashRegister = new CoinBasedCashRegister();

			var amountToStore = -1;

			Assert.ThrowsException<ArgumentException>(() => { cashRegister.StoreMoney(amountToStore); });
		}

		[TestMethod]
		public void WhenAddingZeroAmountOfMoney_ThenSodaMachineThrowsArgumentException()
		{
			var cashRegister = new CoinBasedCashRegister();

			var amountToStore = 0;

			Assert.ThrowsException<ArgumentException>(() => { cashRegister.StoreMoney(amountToStore); });
		}

		[TestMethod]
		public void WhenInsertingMoreMoneyThanCashRegisterCanStore_ThenTransactionFails()
		{
			const int maxAmountOfMoneyThatCanBeStored = 2000;
			var cashRegister = new CoinBasedCashRegister(maxAmountOfMoneyThatCanBeStored);

			var amountToStore = 2001;

			Assert.ThrowsException<ArgumentException>(() => { cashRegister.StoreMoney(amountToStore); });
		}
	}
}