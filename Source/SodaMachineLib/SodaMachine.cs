using System;
using System.Collections.Generic;

namespace SodaMachineLib
{
	public interface ISodaMachine
	{
		TransactionResult InsertCoin(int money);
		TransactionResult RecallAllCoin();
		TransactionResult Order(string sodaType);
		TransactionResult OrderWithSmsReceipt(string sodaType);
		int StoredCoin { get; }
		bool IsValidType(string sodaType);
	}

	public class SodaMachine : ISodaMachine
	{
		private readonly ICashRegister _cashRegister;
		private readonly ISodaContainer _sodaContainer;
		private readonly ISodaRegistry _sodaRegistry;

		private static List<string> AllowedSodaTypes = new List<string>() { Cola.ProductName, Fanta.ProductName, Sprite.ProductName };

		public SodaMachine(ICashRegister cashRegister, ISodaContainer sodaContainer, ISodaRegistry sodaRegistry)
		{
			_cashRegister = cashRegister;
			_sodaContainer = sodaContainer;
			_sodaRegistry = sodaRegistry;
		}

		public int StoredCoin => _cashRegister.StoredMoney;

		public TransactionResult InsertCoin(int money)
		{
			_cashRegister.StoreMoney(money);
			return new TransactionResult { Result = Result.Success, Receipt = $"Adding {money} to credit." };
		}

		public TransactionResult RecallAllCoin()
		{
			var recalledMoney = _cashRegister.RecallStoredMoney();
			return new TransactionResult { Result = Result.Success, Receipt = $"Returning {recalledMoney} coins to customer." };
		}

		public TransactionResult Order(string sodaType)
		{
			if (!_sodaRegistry.ValidSodaTypes.Contains(sodaType))
			{
				throw new ArgumentException("Invalid soda type.");
			}

			var sodaToPurchase = _sodaRegistry.Lookup(sodaType);

			if (_cashRegister.StoredMoney < sodaToPurchase.Price)
			{
				var missingCoins = sodaToPurchase.Price - _cashRegister.StoredMoney;
				return new TransactionResult { Result = Result.Failure, FailureReason = FailureReason.InsufficientFunds, Receipt = $"Insufficient funds. Need an additional {missingCoins} coins." };
			}

			if (_sodaContainer.GetAmountForType(sodaType) < 1)
			{
				return new TransactionResult { Result = Result.Failure, FailureReason = FailureReason.OutOfStock, Receipt = $"No {sodaToPurchase.Name} left." };
			}

			_sodaContainer.Remove(sodaType);
			_cashRegister.WithdrawMoney(sodaToPurchase.Price);
			var recalledMoney = RecallAllCoin();
			return new TransactionResult { Result = Result.Success, Receipt = $"Purchased {sodaToPurchase.Name} for {sodaToPurchase.Price}. {recalledMoney.Receipt}" };
		}

		public TransactionResult OrderWithSmsReceipt(string sodaType)
		{
			var transactionResult = Order(sodaType);
			if (transactionResult.Result == Result.Success)
			{
				SendReceiptBySms(transactionResult.Receipt);
				transactionResult.Receipt += $"\nReceipt sent by SMS.";
			}

			return transactionResult;
		}

		private void SendReceiptBySms(string receipt)
		{
			// Stub method. Assuming we magically had the correct phone number instead of asking for it in the UI.
		}

		public bool IsValidType(string sodaType)
		{
			return _sodaRegistry.ValidType(sodaType);
		}
	}
}
