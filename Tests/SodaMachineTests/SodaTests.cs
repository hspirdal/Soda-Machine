using Microsoft.VisualStudio.TestTools.UnitTesting;
using SodaMachineLib;

namespace SodaMachineTests
{
	[TestClass]
	public class SodaTests
	{
		[TestMethod]
		public void WhenInsertingTwentyCoins_ThenSodaMachineStoresTwentyCoins()
		{
			var sodaMachine = new SodaMachine();

			var amountToStore = 20;

			sodaMachine.InsertCoin(amountToStore);
			var storedAmount = sodaMachine.StoredCoin;

			Assert.IsTrue(storedAmount == amountToStore);
		}
	}
}
