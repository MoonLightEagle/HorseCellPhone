using T9Horse;

internal class Program
{
    private static void Main(string[] args)
    {
        Dial dial = new Dial(new KeyValuePair<int, int>(0, 0), 3, showUniqueNumbers: true);
        dial.countDistinctNumbers();
    }
}

