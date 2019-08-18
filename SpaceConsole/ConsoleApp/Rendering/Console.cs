using System;
using System.Threading.Tasks;

namespace SpaceConsole.ConsoleApp.Rendering
{
    public sealed class Console
    {
        public int Width => System.Console.WindowWidth;

        public int Height => System.Console.WindowHeight;

        public ConsoleColor BackgroundColor { get; set; } = ConsoleColor.Black;

        public ConsoleColor ForegroundColor { get; set; } = ConsoleColor.DarkGreen;

        public Console(int width, int height, string title)
        {
            System.Console.SetWindowSize(width, height);
            System.Console.Title = title;
            // System.Console.TreatControlCAsInput = true;
        }

        public void Write(ConsoleBitmap bitmap, TimeSpan rowDelay, TimeSpan characterDelay)
        {
            bitmap = bitmap.WithFilledBackground(BackgroundColor);

            System.Console.CursorVisible = false;
            System.Console.Clear();

            foreach (var row in bitmap.GetRows())
            {
                foreach (var bit in row)
                {
                    System.Console.ForegroundColor = bit.ForegroundColor;
                    System.Console.BackgroundColor = bit.BackgroundColor;
                    System.Console.Write(bit.Character);

                    if (characterDelay != TimeSpan.Zero)
                        Task.Delay(characterDelay).Wait();
                }

                System.Console.WriteLine();

                if (rowDelay != TimeSpan.Zero)
                    Task.Delay(rowDelay).Wait();
            }

            System.Console.BackgroundColor = BackgroundColor;
            System.Console.ForegroundColor = ForegroundColor;
            System.Console.Write(" ");
            System.Console.CursorVisible = true;
        }

        public void Write(string text)
        {
            System.Console.Write(text);
        }

        public void WriteLine(string line)
        {
            System.Console.WriteLine(line);
        }

        public string ReadLine()
        {
            return System.Console.ReadLine();
        }

        public ConsoleKeyInfo ReadKey()
        {
            return System.Console.ReadKey();
        }

        public int Read()
        {
            return System.Console.Read();
        }

        public void Beep()
        {
            const int min = 37;
            const int max = 32767;
            const double factor = 0.008;
            const int frequency = (int) (factor * (max - min) + min);
            System.Console.Beep(frequency, 500);
        }
    }
}
