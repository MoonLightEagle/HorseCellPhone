using T9Horse;

public class Program
{
    private static void Main(string[] args)
    {
        Dial dial = new Dial(new KeyValuePair<int, int>(3, 2), 2, true, true);
        dial.countDistinctNumbers();
        dial.displayResult();
        Console.WriteLine();
    }
}

