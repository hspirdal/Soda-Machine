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

			const int amountToStore = 20;

			cashRegister.StoreMoney(amountToStore);
			var storedAmount = cashRegister.StoredMoney;

			Assert.IsTrue(storedAmount == amountToStore);
		}

		[TestMethod]
		public void WhenAddingCoinsSeveralTimes_ThenTheStoredAmountIsSummedUp()
		{
			var cashRegister = new CoinBasedCashRegister();

			const int firstAmountToStore = 14;
			const int secondAmountToStore = 20;

			cashRegister.StoreMoney(firstAmountToStore);
			cashRegister.StoreMoney(secondAmountToStore);
			var storedAmount = cashRegister.StoredMoney;

			var expectedAmountStored = firstAmountToStore + secondAmountToStore;
			Assert.AreEqual(expectedAmountStored, storedAmount);
		}

		[TestMethod]
		public void WhenAddingInvalidCoinValue_ThenSodaMachineThrowsArgumentException()
		{
			var cashRegister = new CoinBasedCashRegister();

			const int invalidCoinValue = 21;

			Assert.ThrowsException<ArgumentException>(() => { cashRegister.StoreMoney(invalidCoinValue); });
		}

		[TestMethod]
		public void WhenAddingNegativeAmountOfMoney_ThenSodaMachineThrowsArgumentException()
		{
			var cashRegister = new CoinBasedCashRegister();

			const int amountToStore = -1;

			Assert.ThrowsException<ArgumentException>(() => { cashRegister.StoreMoney(amountToStore); });
		}

		[TestMethod]
		public void WhenAddingZeroAmountOfMoney_ThenSodaMachineThrowsArgumentException()
		{
			var cashRegister = new CoinBasedCashRegister();

			const int amountToStore = 0;

			Assert.ThrowsException<ArgumentException>(() => { cashRegister.StoreMoney(amountToStore); });
		}

		[TestMethod]
		public void WhenInsertingMoreMoneyThanCashRegisterCanStore_ThenTransactionFails()
		{
			const int maxAmountOfMoneyThatCanBeStored = 2000;
			var cashRegister = new CoinBasedCashRegister(maxAmountOfMoneyThatCanBeStored);

			const int amountToStore = 2001;

			Assert.ThrowsException<ArgumentException>(() => { cashRegister.StoreMoney(amountToStore); });
		}
	}
}