using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SodaMachineLib;

namespace SodaMachineTests
{
	[TestClass]
	public class SodaContainerInsertionTests
	{
		[TestMethod]
		public void WhenAppendingMoreBottlesThanContainerCanHandle_ThenArgumentExceptionIsThrown()
		{
			const int maxBottlesStoredPerType = 1;
			var bottlesToInsert = new List<Soda>() { };
			var sodaContainer = new SodaContainer(maxBottlesStoredPerType, bottlesToInsert);

			Assert.ThrowsException<ArgumentException>(() =>
			{
				sodaContainer.Append(new Cola());
				sodaContainer.Append(new Cola());
			});
		}

		[TestMethod]
		public void WhenAppendingOneSodaOfSpecificType_ThenAmountOfSodaForThatTypeIsIncreasedByOne()
		{
			var sodaContainer = new SodaContainer();
			var initialAmountOfColaStored = sodaContainer.GetAmountForType(Cola.ProductName);

			sodaContainer.Append(new Cola());
			var currentAmountOfColaStored = sodaContainer.GetAmountForType(Cola.ProductName);

			Assert.AreEqual(initialAmountOfColaStored + 1, currentAmountOfColaStored);
		}
	}
}