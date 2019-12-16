using System;
using System.Text;

namespace SodaMachineLib
{
	public interface IFeedbackWriter
	{
		void WriteLine(string message);
		void WriteLineToBuffer(string message);
		void WriteBuffer();
	}
	public class ConsoleFeedbackWriter : IFeedbackWriter
	{
		private StringBuilder _buffer = new StringBuilder();

		public void WriteLine(string message)
		{
			Console.WriteLine(message);
		}

		public void WriteLineToBuffer(string message)
		{
			_buffer.AppendLine(message);
		}

		public void WriteBuffer()
		{
			Console.WriteLine(_buffer.ToString());
			_buffer.Clear();
		}
	}
}