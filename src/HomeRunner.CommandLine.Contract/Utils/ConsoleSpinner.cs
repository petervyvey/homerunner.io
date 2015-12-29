
using System;

namespace HomeRunner.CommandLine.Utils
{
	public static class ConsoleSpinner
	{
		#region - Private fields -

		private static int counter;

		#endregion

		public static void Spin()
		{
			ConsoleSpinner.counter++;

			switch (ConsoleSpinner.counter % 4)
			{
				case 0: 
					Console.Write("/"); 
					break;

				case 1: 
					Console.Write("-"); 
					break;

				case 2: 
					Console.Write("\\"); 
					break;

				case 3: 
					Console.Write("|"); 
					break;
			}

			Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
		}
	}
}
