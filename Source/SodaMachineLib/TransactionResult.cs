namespace SodaMachineLib
{
	public enum Result { Success, Failure }
	public enum FailureReason { OutOfStock, InsufficientFunds }

	public class TransactionResult
	{
		public Result Result { get; set; }
		public FailureReason FailureReason { get; set; }
		public string Receipt { get; set; }
	}
}