using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SodaMachineLib;

namespace SodaMachineTests
{
	[TestClass]
	public class CashRegisterRemovalTests
	{
		[TestMethod]
		public void WhenRecallingCashRegisterOfCoins_ThenCashRegisterStoresZeroCoinsAfter()
		{
			var cashRegister = new CoinBasedCashRegister();
			cashRegister.StoreMoney(20);
			var initiallyStoredMoney = cashRegister.StoredMoney;

			cashRegister.RecallStoredMoney();
			var currentlyStoredMoney = cashRegister.StoredMoney;

			Assert.AreEqual(0, currentlyStoredMoney);
		}

		[TestMethod]
		public void WhenRecallingStoredCoins_ThenAllCoinsIsReturned()
		{
			var cashRegister = new CoinBasedCashRegister();
			const int initiallyStoredMoney = 14;
			cashRegister.StoreMoney(initiallyStoredMoney);

			var returnedMoney = cashRegister.RecallStoredMoney();

			Assert.AreEqual(initiallyStoredMoney, returnedMoney);
		}

		[TestMethod]
		public void WhenWithdrawingTwoMoneyFromFour_ThenStoredMoneyIsTwo()
		{
			var cashRegister = new CoinBasedCashRegister();
			const int initiallyStoredMoney = 4;
			cashRegister.StoreMoney(initiallyStoredMoney);

			const int moneyToWithdraw = 2;
			cashRegister.WithdrawMoney(moneyToWithdraw);

			var storedMoney = cashRegister.StoredMoney;
			var expectedMoneyBalance = initiallyStoredMoney - moneyToWithdraw;
			Assert.AreEqual(expectedMoneyBalance, storedMoney);
		}

		[TestMethod]
		public void WhenWithdrawingMoreMoneyThanIsStored_ThenArgumentExceptionIsThrown()
		{
			var cashRegister = new CoinBasedCashRegister();
			const int initiallyStoredMoney = 14;
			cashRegister.StoreMoney(initiallyStoredMoney);

			const int moneyToWithdraw = 15;
			Assert.ThrowsException<ArgumentException>(() => cashRegister.WithdrawMoney(moneyToWithdraw));
		}
	}
}