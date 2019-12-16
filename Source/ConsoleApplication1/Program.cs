using System;
using System.Collections.Generic;
using System.Text;
using SodaMachineLib;

namespace ConsoleApplication1
{
	// NOTE: UI was created a bit hastily due to time restriction. Ideally, the command actions would be extracted into its own place,
	// the error checking would be extracted into its own place, etc. For simplicity, the command 'order sms' was shortened to 'smsorder' to not
	// having to deal with multiword commands. Ideally, this driving code would be testable (integration test for the whole system would be nice!).
	// There's probably a few bugs left, but there's a decent test coverage of the actual logic and I'm confident enough that it's mostly ok.
	// The classic solution to this problem would probably have been a state machine, but I figured this approach would give some more insight about me
	// and I also think a SM might be a bit overkill for something that wouldn't expand much in state logic.
	// There's a few things I'm not too happy about, but that can be discussed later.
	public class Program
	{
		private static void Main(string[] args)
		{

			// NOTE: Would be setup with an IoC container like Autofac in an actual project (skipped due to time and simplicity)
			var validSodaTypes = new Dictionary<string, Soda>(StringComparer.OrdinalIgnoreCase)
			{
				{ Cola.ProductName.ToLower(), new Cola()},
				{ Fanta.ProductName.ToLower(), new Fanta()},
				{ Sprite.ProductName.ToLower(), new Sprite()}
			};
			var sodasToStore = new List<Soda>();
			var registry = new SodaRegistry(validSodaTypes);
			sodasToStore.AddRange(registry.Create(Cola.ProductName, 5));
			sodasToStore.AddRange(registry.Create(Sprite.ProductName, 3));
			sodasToStore.AddRange(registry.Create(Fanta.ProductName, 3));
			var sodaContainer = new SodaContainer(sodasToStore);
			var cashRegister = new CoinBasedCashRegister();
			var feedbackWriter = new ConsoleFeedbackWriter();
			var logger = new OperatorLogger();

			SodaMachine sodaMachine = new SodaMachine(cashRegister, sodaContainer, registry);
			string formattedSodaTypes = FormatValidTypes(registry);

			var allowedCommands = new Dictionary<string, Action<string, SodaMachine, IFeedbackWriter>>(StringComparer.OrdinalIgnoreCase)
			{
				{"insert", new Action<string, SodaMachine, IFeedbackWriter>(Insert) },
				{"order", new Action<string, SodaMachine, IFeedbackWriter>(Order) },
				{"smsorder", new Action<string, SodaMachine, IFeedbackWriter>(OrderSms) },
				{"recall", new Action<string, SodaMachine, IFeedbackWriter>(Recall) },
			};

			while (true)
			{
				try
				{
					feedbackWriter.WriteLine("\n\nAvailable commands:");
					feedbackWriter.WriteLine("insert (money) - Money put into money slot");
					feedbackWriter.WriteLine($"order ({formattedSodaTypes}) - Order from machines buttons");
					feedbackWriter.WriteLine($"smsorder ({formattedSodaTypes}) - Order sent by sms");
					feedbackWriter.WriteLine("recall - gives money back");
					feedbackWriter.WriteLine("-------");
					feedbackWriter.WriteLine("Inserted money: " + sodaMachine.StoredCoin);
					feedbackWriter.WriteLine("-------\n\n");
					feedbackWriter.WriteBuffer();

					var inputArgs = Console.ReadLine().ToLower().Split(" ");
					if (inputArgs.Length > 0)
					{
						var command = inputArgs[0];
						var argument = inputArgs.Length > 1 ? inputArgs[1] : string.Empty;

						if (allowedCommands.ContainsKey(command))
						{
							allowedCommands[command].Invoke(argument, sodaMachine, feedbackWriter);
						}
						else
						{
							feedbackWriter.WriteLineToBuffer("Invalid command.");
						}
					}
				}
				catch (Exception ex)
				{
					logger.Fail(ex);
					feedbackWriter.WriteLineToBuffer("An unknown error occurred and has been logged.");
				}
			}
		}

		private static void Insert(string argument, SodaMachine sodaMachine, IFeedbackWriter feedbackWriter)
		{
			if (EnsureValidInsertArguments(argument, sodaMachine, feedbackWriter, out var coinsToInsert))
			{
				var result = sodaMachine.InsertCoin(coinsToInsert);
				feedbackWriter.WriteLineToBuffer(result.Receipt);
			}
		}

		private static void Recall(string argument, SodaMachine sodaMachine, IFeedbackWriter feedbackWriter)
		{
			var result = sodaMachine.RecallAllCoin();
			feedbackWriter.WriteLineToBuffer(result.Receipt);
		}

		private static void Order(string argument, SodaMachine sodaMachine, IFeedbackWriter feedbackWriter)
		{
			if (EnsureValidOrderArguments(argument, sodaMachine, feedbackWriter))
			{
				var result = sodaMachine.Order(argument);
				feedbackWriter.WriteLineToBuffer(result.Receipt);
			}
		}

		private static void OrderSms(string argument, SodaMachine sodaMachine, IFeedbackWriter feedbackWriter)
		{
			if (EnsureValidOrderArguments(argument, sodaMachine, feedbackWriter))
			{
				var result = sodaMachine.OrderWithSmsReceipt(argument);
				feedbackWriter.WriteLineToBuffer(result.Receipt);
			}
		}

		private static bool EnsureValidInsertArguments(string argument, ISodaMachine sodaMachine, IFeedbackWriter feedbackWriter, out int coinsToInsert)
		{
			if (string.IsNullOrWhiteSpace(argument))
			{
				feedbackWriter.WriteLineToBuffer("Must specify amount of coins to insert.");
				coinsToInsert = 0;
				return false;
			}

			if (!int.TryParse(argument, out coinsToInsert))
			{
				feedbackWriter.WriteLineToBuffer("Must specify a number.");
				return false;
			}

			if (coinsToInsert < 1 || coinsToInsert > 20)
			{
				feedbackWriter.WriteLineToBuffer("Number of coins to insert must be between 1 and 20");
				return false;
			}

			return true;
		}

		private static bool EnsureValidOrderArguments(string argument, ISodaMachine sodaMachine, IFeedbackWriter feedbackWriter)
		{
			if (string.IsNullOrWhiteSpace(argument))
			{
				feedbackWriter.WriteLineToBuffer("Must specify soda to order.");
				return false;
			}

			if (!sodaMachine.IsValidType(argument))
			{
				feedbackWriter.WriteLineToBuffer($"Soda of type {argument} does not exist.");
				return false;
			}

			return true;
		}

		private static string FormatValidTypes(ISodaRegistry sodaRegistry)
		{
			var sb = new StringBuilder();
			var count = sodaRegistry.ValidSodaTypes.Count;
			for (var i = 0; i < count; ++i)
			{
				sb.Append($"{sodaRegistry.ValidSodaTypes[i]}");
				if (i < count - 1)
				{
					sb.Append(", ");
				}
			}
			return sb.ToString();
		}
	}
}