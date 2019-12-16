using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SodaMachineLib;

namespace SodaMachineTests
{
	[TestClass]
	public class SodaContainerRemovalTests
	{
		[TestMethod]
		public void WhenRemovingSodaBottleOfSpecificType_ThenBottleCollectionOfThatTypeIsReducedByOne()
		{
			var sodaContainer = new SodaContainer();
			sodaContainer.Append(new Cola());
			var initialAmountOfColaStored = sodaContainer.GetAmountForType(Cola.ProductName);

			sodaContainer.Remove(Cola.ProductName);
			var currentAmountOfColaStored = sodaContainer.GetAmountForType(Cola.ProductName);

			Assert.AreEqual(initialAmountOfColaStored - 1, currentAmountOfColaStored);
		}

		[TestMethod]
		public void WhenContainerIsEmptyOfSpecificType_ThenRemovingBottleOfThatTypeThrowsException()
		{
			var zeroSodasToStoreInitially = new List<Soda>();
			var sodaContainer = new SodaContainer(zeroSodasToStoreInitially);

			Assert.ThrowsException<ArgumentException>(() => sodaContainer.Remove(Cola.ProductName));
		}

		[TestMethod]
		public void WhenContainerHasOneColaStored_ThenRemovingTwoResultsInArgumentException()
		{
			var initallyStoredSodas = new List<Soda>() { new Cola() };
			var sodaContainer = new SodaContainer(initallyStoredSodas);

			sodaContainer.Remove(Cola.ProductName);

			Assert.ThrowsException<ArgumentException>(() => sodaContainer.Remove(Cola.ProductName));
		}
	}
}