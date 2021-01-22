using System;
using System.IO;
using Microsoft.CodeAnalysis;
namespace TextTemplating
{
    public class ttConsole
    {

        public virtual void Clear() => Console.Clear();

        public static void WriteError(string value)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(value.TrimEnd(Environment.NewLine.ToCharArray()));
            Console.ResetColor();
        }

        public static void WriteSuccess(string value)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(value.TrimEnd(Environment.NewLine.ToCharArray()));
            Console.ResetColor();
        }

        public static void WriteHighlighted(string value)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(value.TrimEnd(Environment.NewLine.ToCharArray()));
            Console.ResetColor();
        }

        public static void WriteNormal(string value)
        {
            Console.WriteLine(value.TrimEnd(Environment.NewLine.ToCharArray()));
        }
    }
}