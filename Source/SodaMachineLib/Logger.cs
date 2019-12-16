using System;

namespace SodaMachineLib
{
	public interface ILogger
	{
		void Trace(string message);
		void Warning(string message);
		void Fail(Exception exception);
	}
	public class OperatorLogger : ILogger
	{
		public void Trace(string message)
		{

		}

		public void Warning(string message)
		{

		}

		public void Fail(Exception exception)
		{

		}
	}
}