
using System;

namespace HomeRunner.CommandLine.Utils
{
    public static class ConsoleProgressBar
	{

		/// <summary>
		/// Renders a message in the console window that overwrites any previous message in the same location
		/// </summary>
		/// <param name="message">Message to be displayed in the console</param>
		public static void OverwriteConsoleMessage(string message)
		{
			// Move the cursor to the beginning of the console.
			Console.CursorLeft = 0;

			// Get size of console.
			int maxWidth = Math.Min(Console.WindowWidth, 100) - 3;
			if (message.Length > maxWidth)
			{
				// If message is longer than the console window truncate it.
				message = message.Substring(0, maxWidth) + "...";
			}

            // Create a new message string with the end padded out with blank spaces.
			message = message + new string(' ', maxWidth - message.Length);

			Console.Write(message);
		}

		/// <summary>
		/// Draws a progress bar in a console window using a selected character to make up the progres bar elements. A message can be displayed below the console at the same time.
		/// </summary>
		/// <param name="percentage">Percentage to be displayed on console</param>
		/// <param name="progressBarCharacter">Character used to build progress bar</param>
		/// <param name="color">Color of progress bar</param>
		/// <param name="message">Message to be displayed below console</param>
		public static void RenderConsoleProgress(int current, int total, ConsoleColor color, string message)
		{
			Console.CursorVisible = false;

            char progressBarCharacter = '=';
            int percentage = (int)Math.Round((current * 100d) / total);
            bool isFinished = current == total;

            // Move the cursor to the left of the console.
            Console.CursorLeft = 0;

			// Determine the maximum width of the console window.
			int maxWidth = Math.Min(Console.WindowWidth, 100) - 3;

			// Calculate the number of character required to create the progress bar.
			int width = (int)((maxWidth * percentage) / 100d);

            ConsoleColor foregroundColor = Console.ForegroundColor;

			// Write start character.
			Console.ForegroundColor = ConsoleColor.Green;
			Console.Write("|");

            // Create the progress bar text to be displayed.
			string progress = new string(progressBarCharacter, width == 0 ? width: width - 1);
            Console.Write(progress);

            char spinner = '=';
			if(!isFinished)
			{
				switch(current % 4)
				{
				case 0:
					spinner = '/';
					break;

				case 1:
					spinner = '-';
					break;

				case 2:
					spinner = '\\';
					break;

				case 3:
					spinner = '|';
					break;
				}
			}

            Console.Write(spinner);

			Console.ForegroundColor = ConsoleColor.DarkYellow;
			string filler = new string ('-', maxWidth - (width == 0 ? width: width - 1) - 1);
            Console.Write(filler);

			// Write end character.
			Console.ForegroundColor = ConsoleColor.Green;
			Console.Write("|");

			if (string.IsNullOrEmpty(message)) message = string.Empty;

			// Move the cursor up one line to display the message.
			Console.CursorTop--;

			Console.ForegroundColor = ConsoleColor.Green;

            // Render the message below the progress bar.
            OverwriteConsoleMessage(message);

			// Reset the cursor down one line.
			Console.CursorTop++;

            // Reset the console color back to the original color.
            Console.ForegroundColor = foregroundColor;
            //Console.BackgroundColor = backgroundColor;

			if(isFinished)
			{
				Console.WriteLine();
			}

            Console.CursorVisible = true;
		}
	}
}
