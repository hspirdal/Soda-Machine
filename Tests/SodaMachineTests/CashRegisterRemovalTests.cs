using Microsoft.VisualStudio.TestTools.UnitTesting;
using SodaMachineLib;

namespace SodaMachineTests
{
	[TestClass]
	public class CashRegisterRemovalTests
	{
		[TestMethod]
		public void WhenRecallingCashRegisterOfThirtyCoins_ThenCashRegisterStoresZeroCoinsAfter()
		{
			var cashRegister = new CoinBasedCashRegister();
			cashRegister.StoreMoney(30);
			var initiallyStoredMoney = cashRegister.StoredMoney;

			cashRegister.RecallStoredMoney();
			var currentlyStoredMoney = cashRegister.StoredMoney;

			Assert.AreEqual(0, currentlyStoredMoney);
		}

		[TestMethod]
		public void WhenRecallingThirtyStoredCoins_ThenThirtyCoinsIsReturned()
		{
			var cashRegister = new CoinBasedCashRegister();
			const int initiallyStoredMoney = 30;
			cashRegister.StoreMoney(initiallyStoredMoney);

			var returnedMoney = cashRegister.RecallStoredMoney();

			Assert.AreEqual(initiallyStoredMoney, returnedMoney);
		}
	}
}