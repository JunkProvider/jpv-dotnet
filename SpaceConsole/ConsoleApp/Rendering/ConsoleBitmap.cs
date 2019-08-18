using System;
using System.Collections.Generic;
using System.Drawing;

namespace SpaceConsole.ConsoleApp.Rendering
{
    public sealed class ConsoleBitmap
    {
        public static readonly ConsoleBitmap Empty = new ConsoleBitmap(0, 0);

        public static ConsoleBit[,] CopyBits(int width, int height, ConsoleBit[,] bits)
        {
            var copy = new ConsoleBit[width, height];
            InsertBits(copy, 0, 0, width, height, bits);
            return copy;
        }

        public static void InsertBits(ConsoleBit[,] bits, int insertedX, int insertedY, int insertedWidth, int insertedHeight, ConsoleBit[,] insertedBits)
        {
            for (var x = 0; x < insertedWidth; x++)
            {
                for (var y = 0; y < insertedHeight; y++)
                {
                    bits[insertedX + x, insertedY + y] = insertedBits[x, y];
                }
            }
        }

        public static ConsoleBit[,] CreateBits(int width, int height)
        {
            return CreateBits(width, height, (x, y) => ConsoleBit.Empty);
        }

        public static ConsoleBit[,] CreateBits(int width, int height, Func<int, int, ConsoleBit> bitFunc)
        {
            var bits = new ConsoleBit[width, height];

            for (var x = 0; x < width; x++)
            {
                for (var y = 0; y < height; y++)
                {
                    bits[x, y] = bitFunc(x, y);
                }
            }

            return bits;
        }

        private readonly ConsoleBit[,] _bits;

        public int Width { get; }

        public int Height { get; }

        public ConsoleBitmap(string text, ConsoleColor foregroundColor)
            : this(text.Length, 1, CreateBits(text.Length, 1, (x, y) => new ConsoleBit(text[x], foregroundColor)))
        {
        }

        public ConsoleBitmap(Size size)
            : this(size.Width, size.Height)
        {
        }

        public ConsoleBitmap(int width, int height)
            : this(width, height, CreateBits(width, height))
        {
        }

        public ConsoleBitmap(int width, int height, ConsoleBit[,] bits)
        {
            Width = width;
            Height = height;
            _bits = bits;
        }

        public IEnumerable<IEnumerable<ConsoleBit>> GetRows()
        {
            for (var y = 0; y < Height; y++)
            {
                yield return GetRow(y);
            }
        }

        public IEnumerable<ConsoleBit> GetRow(int y)
        {
            for (var x = 0; x < Width; x++)
            {
                yield return _bits[x, y];
            }
        }

        public ConsoleBitmap WithFilledBackground(ConsoleColor backgroundColor)
        {
            return Map(bit => bit.IsBackgroundTransparent ? bit.WithBackgroundColor(backgroundColor) : bit);
        }

        public ConsoleBitmap WithMaxSize(Size maxSize)
        {
            if (Width <= maxSize.Width && Height <= maxSize.Height)
                return this;

            return WithSize(new Size(Math.Min(Width, maxSize.Width), Math.Min(Height, maxSize.Height)));
        }

        public ConsoleBitmap WithSize(Size desiredSize)
        {
            if (Width == desiredSize.Width && Height == desiredSize.Height)
                return this;

            var bits = CreateBits(desiredSize.Width, desiredSize.Height);

            InsertBits(bits, 0, 0, Math.Min(Width, desiredSize.Width), Math.Min(Height, desiredSize.Height), _bits);

            return new ConsoleBitmap(desiredSize.Width, desiredSize.Height, bits);
        }

        public ConsoleBitmap CombinedWith(int xOffset, int yOffset, ConsoleBitmap other)
        {
            var combinedWidth = Math.Max(Width, xOffset + other.Width);
            var combinedHeight = Math.Max(Height, yOffset + other.Height);
            var combinedBits = CreateBits(combinedWidth, combinedHeight);
            InsertBits(combinedBits, 0, 0, Width, Height, _bits);
            InsertBits(combinedBits, xOffset, yOffset, other.Width, other.Height, other._bits);
            return new ConsoleBitmap(combinedWidth, combinedHeight, combinedBits);
        }

        public ConsoleBitmap WithInserted(int xOffset, int yOffset, ConsoleBitmap insertedBitmap)
        {
            var insertedWidth = Math.Max(0, Math.Min(Width - xOffset, insertedBitmap.Width));
            var insertedHeight = Math.Max(0, Math.Min(Height -yOffset, insertedBitmap.Height));

            var bits = CopyBits(Width, Height, _bits);
            InsertBits(bits, xOffset, yOffset, insertedWidth, insertedHeight, insertedBitmap._bits);

            return new ConsoleBitmap(Width, Height, bits);
        }

        private ConsoleBitmap Map(Func<ConsoleBit, ConsoleBit> selectFunc)
        {
            return new ConsoleBitmap(Width, Height, MapBits(selectFunc));
        }

        private ConsoleBit[,] MapBits(Func<ConsoleBit, ConsoleBit> selectFunc)
        {
            return CreateBits(Width, Height, (x, y) => selectFunc(_bits[x, y]));
        }
    }
}



