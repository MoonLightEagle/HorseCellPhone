// See https://aka.ms/new-console-template for more information

internal class Program
{
    private static void Main(string[] args)
    {

        Cells cells = new Cells(5, 8);
        Console.WriteLine(cells.countDistinctNumbers(new KeyValuePair<int, int>(0, 0), 2));
        cells.output();
        foreach (var item in cells.uniqueNumbers)
        {
            Console.WriteLine(item);
        }
        Console.WriteLine("Result is: " + cells.uniqueNumbers.Count);
    }
}

// 1    2   3
// 4    5   6
// 7    8   9
//      0

class Cells
{
    static int[,] matrix = new int[,] { { 1, 2, 3 },
                                        { 4, 5, 6 },
                                        { 7, 8, 9 },
                                        { -1, 0, -1 } };
    int _height = 4;
    int _width = 3;
    public HashSet<int> uniqueNumbers = new HashSet<int>();
    static readonly KeyValuePair<int, int>[] jumpps = new KeyValuePair<int, int>[] { new KeyValuePair<int, int> ( 1, 2),
                                                                   new KeyValuePair<int, int> ( 2, 1),
                                                                   new KeyValuePair<int, int> ( -1, 2),
                                                                   new KeyValuePair<int, int> ( 2, -1),
                                                                   new KeyValuePair<int, int> ( 1, -2),
                                                                   new KeyValuePair<int, int> ( -2, 1),
                                                                   new KeyValuePair<int, int> ( -1, -2),
                                                                   new KeyValuePair<int, int> ( -2,-1)};

    public Cells(int n, int m)
    {
        _height = n;
        _width = m;
        matrix = new int[n, m];
        randomMatrix(n, m);
    }
    public void output()
    {
        for (int i = 0; i < _height; i++)
        {
            for (int j = 0; j < _width; j++)
            {
                Console.Write(matrix[i, j] + "    ");

            }
            Console.WriteLine();
        }
    }
    public Cells() { }
    void randomMatrix(int n, int m)
    {
        var rand = new Random();
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < m; j++)
            {
                matrix[i, j] = rand.Next(10);
            }
        }
    }

    public int countDistinctNumbers(KeyValuePair<int, int> currPoint, int hopsRemaining, int prevNumber = -1)
    {
        if (prevNumber == -1)
        {
            prevNumber = matrix[currPoint.Key, currPoint.Value];
        }
        else
        {
            prevNumber = prevNumber * 10 + matrix[currPoint.Key, currPoint.Value];
        }

        if (hopsRemaining == 0)
        {

            if (uniqueNumbers.Contains(prevNumber))
            {
                return 0;
            }
            else
            {
                uniqueNumbers.Add(prevNumber);
                return 1;
            }

        }

        int result = 0;
        HashSet<KeyValuePair<int, int>> moves = calculateLegalMoves(currPoint);

        foreach (var elem in moves)
        {

            result += countDistinctNumbers(elem, hopsRemaining - 1, prevNumber);
        }

        return result;
    }
    bool isLegalMove(KeyValuePair<int, int> currPoint, KeyValuePair<int, int> jump)
    {
        if (currPoint.Key + jump.Key >= 0 && currPoint.Key + jump.Key < _height && currPoint.Value + jump.Value >= 0 && currPoint.Value + jump.Value < _width)
        {
            // this is an existing possition inside matrix
            // if the number in this cell is between 1 and 9 we will run into next if()
            if (matrix[currPoint.Key + jump.Key, currPoint.Value + jump.Value] >= 0 || matrix[currPoint.Key + jump.Key, currPoint.Value + jump.Value] <= 9)
            {
                return true;
            }
        }
        return false;
    }
    HashSet<KeyValuePair<int, int>> calculateLegalMoves(KeyValuePair<int, int> currPoint)
    {
        HashSet<KeyValuePair<int, int>> result = new HashSet<KeyValuePair<int, int>>(); // always will be two legal moves
        foreach (var item in jumpps)
        {
            if (isLegalMove(currPoint, item))
            {
                result.Add(new KeyValuePair<int, int>(currPoint.Key + item.Key, currPoint.Value + item.Value));
            }
        }
        return result;
    }

}