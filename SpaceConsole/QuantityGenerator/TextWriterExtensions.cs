using System.IO;
using System.Text;

namespace QuantityGenerator
{
    public static class TextWriterExtensions
    {
        public static void WriteLine(this TextWriter writer, int indent, string line)
        {
            writer.WriteLine(new StringBuilder(new string(' ', indent * 3)).Append(line));
        }
    }
}