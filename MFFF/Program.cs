using MFFF;

List<Line> lines = [new Line([new TextBox(text: "Hello", color: new Color(bgColor: Colors.Blue, textColor: Colors.Gray), effect: new TypeWriter(delay: 250)), 
    new TextBox(text: " World!", color: new Color(bgColor: Colors.Gray, textColor: Colors.Blue))]), new Line([new TextBox(text: "This somehow works...", color: 
    new Color(bgColor: Colors.DarkRed, textColor: Colors.Yellow), effect: new TypeWriter(100))])];
Display.Print(lines);

namespace MFFF
{
    public static class Display
    {
        // Class for printing lines and/or blocks of text to the Console
        
        public static void Print(List<Line> lines)
        {
            // Print a series of Lines to the Console, each as its own line of text

            foreach (var line in lines)
            {
                line.PrintText();
            }
        }

        public static void Print (Line line)
        {
            // Print a single Line to the Console as its own line of text

            line.PrintText();
        }

        public static void Print(TextBox text)
        {
            // Print a single block of text to the Console

            text.PrintText();
        }

        public static void Print()
        {
            // Print an empty line

            Print(new Line());
        }
    }

    public class Line
    {
        // Contains a series of blocks of text with different colors and effects constituting one overall line of text

        public List<TextBox> Texts { get; set; } = new List<TextBox>();

        public void PrintText()
        {
            // Print the stored blocks of text as a single line in the given order

            foreach (var text in Texts)
            {
                text.PrintText();
            }
            Console.WriteLine();
        }

        public Line() { }

        public Line(TextBox text)
        {
            Texts.Add(text);
        }

        public Line(List<TextBox> texts)
        {
            Texts.AddRange(texts);
        }
    }

    public class TextBox
    {
        // Container class for storing and printing a homogenous section of text with a shared Color and Effect

        public static Effect DefaultEffect { get; set; } = new NoEffect();
        public string Text { get; set; } = "";
        public Color Color { get; set; } = new Color();
        public Effect Effect { get; set; }

        public void PrintText()
        {
            // Print the given text with the given Color and Effect applied

            var defBGColor = Console.BackgroundColor;
            var defTextColor = Console.ForegroundColor;

            Console.BackgroundColor = Color.BGColor;
            Console.ForegroundColor = Color.TextColor;

            Effect.PrintEffect(Text);

            Console.BackgroundColor = defBGColor;
            Console.ForegroundColor = defTextColor;
        }

        
        public TextBox(string? text = null, Color? color = null, Effect? effect = null)
        {
            Text = text ?? Text;
            Color = color ?? Color;
            Effect = effect ?? DefaultEffect;
        }
    }

    public class Color
    {
        // Class for storing the color data for a TextBox

        public static ConsoleColor DefaultBGColor { get; set; } = Colors.Black;
        public static ConsoleColor DefaultTextColor { get; set; } = Colors.White;
        public ConsoleColor BGColor { get; set; }
        public ConsoleColor TextColor { get; set; }

        public Color(ConsoleColor? bgColor = null, ConsoleColor? textColor = null)
        {
            BGColor = bgColor ?? DefaultBGColor;
            TextColor = textColor ?? DefaultTextColor;
        }
    }

    public class Effect
    {
        // Parent class for all text effects

        public virtual void PrintEffect(string text)
        {
            // PrintEffect() must be implemented for each Effect

            throw new NotImplementedException();
        }
    }

    public class NoEffect : Effect
    {
        // Default Effect: does not apply any effect

        public override void PrintEffect(string text)
        {
            // Print text without any effect applied

            Console.Write(text);
        }
    }

    public class TypeWriter : Effect
    {
        // Effect for printing text with delays between each character

        public int Delay { get; set; } // Time between characters in milliseconds

        public override void PrintEffect(string text)
        {
            // Print each character after a delay

            var chars = Utils.ParseString(text);
            foreach (var chara in chars)
            {
                Console.Write(chara);
                Thread.Sleep(Delay);
            }
        }

        public TypeWriter(int delay = 500)
        {
            Delay = delay;
        }
    }

    public static class Colors
    {
        // Class for storing references to all possible Console colors

        public static readonly ConsoleColor Black = ConsoleColor.Black;
        public static readonly ConsoleColor DarkBlue = ConsoleColor.DarkBlue;
        public static readonly ConsoleColor DarkGreen = ConsoleColor.DarkGreen;
        public static readonly ConsoleColor DarkCyan = ConsoleColor.DarkCyan;
        public static readonly ConsoleColor DarkRed = ConsoleColor.DarkRed;
        public static readonly ConsoleColor DarkMagenta = ConsoleColor.DarkMagenta;
        public static readonly ConsoleColor DarkYellow = ConsoleColor.DarkYellow;
        public static readonly ConsoleColor Gray = ConsoleColor.Gray;
        public static readonly ConsoleColor DarkGray = ConsoleColor.DarkGray;
        public static readonly ConsoleColor Blue = ConsoleColor.Blue;
        public static readonly ConsoleColor Green = ConsoleColor.Green;
        public static readonly ConsoleColor Cyan = ConsoleColor.Cyan;
        public static readonly ConsoleColor Red = ConsoleColor.Red;
        public static readonly ConsoleColor Magenta = ConsoleColor.Magenta;
        public static readonly ConsoleColor Yellow = ConsoleColor.Yellow;
        public static readonly ConsoleColor White = ConsoleColor.White;
    }

    public class Utils
    {
        // Utility functions for use by other classes

        public static List<string> ParseString(string input)
        {
            // Split a string into a List of its individual characters

            var chars = input.ToCharArray();
            var parsedString = new List<string>();
            foreach (var chara in chars)
            {
                parsedString.Add(chara.ToString());
            }
            return parsedString.ToList();
        }
    }
}