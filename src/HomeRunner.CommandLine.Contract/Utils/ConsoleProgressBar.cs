
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
			int maxCharacterWidth = Console.WindowWidth - 1;
			if (message.Length > maxCharacterWidth)
			{
				// If message is longer than the console window truncate it.
				message = message.Substring(0, maxCharacterWidth - 3) + "...";
			}

            // Create a new message string with the end padded out with blank spaces.
            message = message.PadRight(maxCharacterWidth - message.Length - 2, ' ');

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
            char progressBarCharacter = '=';
            int percentage = (int)Math.Round((current * 100d) / total);
            bool isFinished = current == total;

            Console.CursorVisible = false;

            // Move the cursor to the left of the console.
            Console.CursorLeft = 0;

			// Determine the maximum width of the console window.
			int width = Math.Min(Console.WindowWidth - 1, 100);

			// Calculate the number of character required to create the progress bar.
			int newWidth = (int)((width * percentage) / 100d);

            ConsoleColor foregroundColor = Console.ForegroundColor;
            ConsoleColor backgroundColor = Console.BackgroundColor;

            Console.ForegroundColor = backgroundColor;
            Console.BackgroundColor = color;

            // Create the progress bar text to be displayed.
            string progress = "|" + string.Empty.PadLeft(Math.Max(newWidth - (isFinished ? 0 : 1), 0), progressBarCharacter);
            Console.Write(progress);

            char spinner = '|';

            if (!isFinished)
            {
                switch (current % 4)
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

            Console.ForegroundColor = color;
            Console.BackgroundColor = ConsoleColor.DarkGreen;

            string filler = isFinished ? string.Empty : string.Empty.PadLeft(width - (newWidth == 0 ? 1 : newWidth), '-') + "|";
            Console.Write(filler);

            if (string.IsNullOrEmpty(message)) message = "";

			// Move the cursor up one line to display the message.
			Console.CursorTop--;

            Console.ForegroundColor = color;
            Console.BackgroundColor = backgroundColor;

            // Render the message below the progress bar.
            OverwriteConsoleMessage(message);

			// Reset the cursor down one line.
			Console.CursorTop++;

            // Reset the console color back to the original color.
            Console.ForegroundColor = foregroundColor;
            Console.BackgroundColor = backgroundColor;

            Console.CursorVisible = true;
		}
	}
}
