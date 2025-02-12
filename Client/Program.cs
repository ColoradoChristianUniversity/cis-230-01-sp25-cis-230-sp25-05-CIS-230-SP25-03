using System.Drawing;

using Client.Library;

while (true)
{
    Console.Clear();

    var menuItems = await Api.GetStringArrayAsync();

    var highlightedIndex = 0;

    while (true)
    {
        PrintMenu(menuItems, highlightedIndex);

        var key = Screen.Listen(ConsoleKey.UpArrow, ConsoleKey.DownArrow, ConsoleKey.Enter, ConsoleKey.Escape);

        if (key == ConsoleKey.UpArrow)
        {
            highlightedIndex = Math.Max(0, highlightedIndex - 1);
        }
        else if (key == ConsoleKey.DownArrow)
        {
            highlightedIndex = Math.Min(menuItems.Length - 1, highlightedIndex + 1);
        }
        else
        {
            if (key == ConsoleKey.Enter)
            {
                PrintSelection(menuItems, highlightedIndex);
            }

            break;
        }
    }

}

static void PrintMenu(string[] menuItems, int highlightedIndex)
{
    for (var i = 0; i < menuItems.Length; i++)
    {
        var item = menuItems[i];

        if (i == highlightedIndex)
        {
            Screen.Print($"{i + 1}. {item}", 1, i + 1, ConsoleColor.Black, ConsoleColor.White);
        }
        else
        {
            Screen.Print($"{i + 1}. {item}", 1, i + 1, ConsoleColor.White, ConsoleColor.Black);
        }
    }
}

static void PrintSelection(string[] menuItems, int highlightedIndex)
{
    Console.Clear();

    var message = $"You selected: {menuItems[highlightedIndex]}";

    var topLeft = new Point(1, 1);
    var size = new Size(message.Length, 1);

    Screen.Print(message, topLeft, ConsoleColor.Black, ConsoleColor.White);
    Screen.SurroundWithBorder(topLeft, size, Screen.BorderStyle.@single);

    message = "Press any key to continue.";
    Screen.Print(message, new Point(1, Console.WindowHeight - 3), ConsoleColor.Yellow, ConsoleColor.Blue);

    Console.ReadKey();
}