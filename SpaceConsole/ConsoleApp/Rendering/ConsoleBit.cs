using System;

namespace SpaceConsole.ConsoleApp.Rendering
{
    public struct ConsoleBit
    {
        public static ConsoleBit Empty { get; } = new ConsoleBit(' ', ConsoleColor.Black, true, ConsoleColor.Black);

        public char Character { get; }

        public ConsoleColor ForegroundColor { get; }

        public bool IsBackgroundTransparent { get; }

        public ConsoleColor BackgroundColor { get; }

        public ConsoleBit(char character, ConsoleColor foregroundColor)
            : this(character, foregroundColor, true, ConsoleColor.Black)
        {
        }

        public ConsoleBit(char character, ConsoleColor foregroundColor, ConsoleColor backgroundColor)
            : this(character, foregroundColor, false, backgroundColor)
        {
        }

        public ConsoleBit(ConsoleColor backgroundColor)
            : this(' ', ConsoleColor.Black, false, backgroundColor)
        {
        }

        private ConsoleBit(char character, ConsoleColor foregroundColor, bool isBackgroundTransparent, ConsoleColor backgroundColor)
        {
            Character = character;
            ForegroundColor = foregroundColor;
            IsBackgroundTransparent = isBackgroundTransparent;
            BackgroundColor = backgroundColor;
        }

        public ConsoleBit WithBackgroundColor(ConsoleColor backgroundColor)
        {
            return new ConsoleBit(Character, ForegroundColor, false, backgroundColor);
        }
    }
}