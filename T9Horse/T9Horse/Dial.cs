namespace T9Horse
{
    public class Dial
    {
        public int height { get; }
        public int width { get; }
        public int numberOfHops { get; }
        public KeyValuePair<int, int> startingLocation { get; }
        public int[,] matrix { get; }
        public static HashSet<string> s_uniqueNumbers { get; }
        public static KeyValuePair<int, int>[] s_jumps { get; }
        public bool showMatrix { get; private set; }
        public bool showUniqueNumbers { get; private set; }
        public Dial(KeyValuePair<int, int> startingLocation, int numberOfHops, bool showUniqueNumbers = false, bool showMatrix = false)
        {
            matrix = new int[,] { { 1, 2, 3 },
                                  { 4, 5, 6 },
                                  { 7, 8, 9 },
                                  { -1, 0, -1 } };
            this.startingLocation = startingLocation;
            this.numberOfHops = numberOfHops;
            this.height = matrix.GetLength(0);
            this.width = matrix.GetLength(1);
            this.showMatrix = showMatrix;
            this.showUniqueNumbers = showUniqueNumbers;
        }
        public Dial(int[,] matrix, KeyValuePair<int, int> startingLocation, int numberOfHops, bool showUniqueNumbers = false, bool showMatrix = false)
        {
            this.matrix = matrix;
            this.numberOfHops = numberOfHops;
            this.height = matrix.GetLength(0);
            this.width = matrix.GetLength(1);
            this.showMatrix = showMatrix;
            this.showUniqueNumbers = showUniqueNumbers;
            this.startingLocation = startingLocation;

        }
        public Dial(int height, int width, KeyValuePair<int, int> startingLocation, int numberOfHops, bool showUniqueNumbers = false, bool showMatrix = false)
        {
            this.height = height;
            this.width = width;
            this.startingLocation = startingLocation;
            this.numberOfHops = numberOfHops;
            matrix = new int[height, width];
            randomMatrix();
            this.showMatrix = showMatrix;
            this.showUniqueNumbers = showUniqueNumbers;
        }
        static Dial()
        {
            s_uniqueNumbers = new HashSet<string>();
            s_jumps = new KeyValuePair<int, int>[] { new KeyValuePair<int, int> ( 1, 2),
                                                                   new KeyValuePair<int, int> ( 2, 1),
                                                                   new KeyValuePair<int, int> ( -1, 2),
                                                                   new KeyValuePair<int, int> ( 2, -1),
                                                                   new KeyValuePair<int, int> ( 1, -2),
                                                                   new KeyValuePair<int, int> ( -2, 1),
                                                                   new KeyValuePair<int, int> ( -1, -2),
                                                                   new KeyValuePair<int, int> ( -2, -1)};
        }
        private void randomMatrix()
        {
            Random random = new Random();
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    matrix[i, j] = random.Next(-1, 10);
                }
            }
        }
        public void output()
        {
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    Console.Write(matrix[i, j] + "    ");

                }
                Console.WriteLine();
            }
        }
        public int countDistinctNumbers(KeyValuePair<int, int> currPoint, int hopsRemaining, string prevNumber = "")
        {

            if (prevNumber == "")
            {
                prevNumber = matrix[currPoint.Key, currPoint.Value].ToString();
            }
            else
            {
                prevNumber = prevNumber + matrix[currPoint.Key, currPoint.Value];
            }

            if (hopsRemaining == 0)
            {

                if (s_uniqueNumbers.Contains(prevNumber))
                {
                    return 0;
                }
                else
                {
                    s_uniqueNumbers.Add(prevNumber);
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
        public int countDistinctNumbers()
        {
            if (this.matrix[this.startingLocation.Key, this.startingLocation.Value] < 0 || this.matrix[this.startingLocation.Key, this.startingLocation.Value] > 9)
            {
                throw new Exception(message: $"Staring location number must be between 0 and 9. Currently is {this.matrix[this.startingLocation.Key, this.startingLocation.Value]}");
            }
            int result;
            if (calculateLegalMoves(this.startingLocation).Count == 0)
            {
                result = 1;
                s_uniqueNumbers.Add(matrix[this.startingLocation.Key, this.startingLocation.Value].ToString());
            }
            else
            {
                result = countDistinctNumbers(this.startingLocation, this.numberOfHops);

            }
            return result;
        }
        public void displayResult()
        {
            if (showMatrix)
            {
                Console.WriteLine("This is matrix we are work on:");
                output();
            }
            if (showUniqueNumbers)
            {
                Console.WriteLine("Theese are all of unique numbers:");
                foreach (var item in Dial.s_uniqueNumbers)
                {
                    Console.WriteLine(item);
                }
            }
            Console.WriteLine("Answer is: " + Dial.s_uniqueNumbers.Count);
        }
        bool isLegalMove(KeyValuePair<int, int> currPoint, KeyValuePair<int, int> jump)
        {
            // this is an existing possition inside matrix
            if (currPoint.Key + jump.Key >= 0 && currPoint.Key + jump.Key < height && currPoint.Value + jump.Value >= 0 && currPoint.Value + jump.Value < width)
            {

                // if the number in this cell is between 0 and 9 we will run into next if()
                if (matrix[currPoint.Key + jump.Key, currPoint.Value + jump.Value] >= 0 && matrix[currPoint.Key + jump.Key, currPoint.Value + jump.Value] <= 9)
                {
                    return true;
                }
            }
            return false;
        }
        HashSet<KeyValuePair<int, int>> calculateLegalMoves(KeyValuePair<int, int> currPoint)
        {
            HashSet<KeyValuePair<int, int>> result = new HashSet<KeyValuePair<int, int>>();
            foreach (var item in s_jumps)
            {
                if (isLegalMove(currPoint, item))
                {
                    result.Add(new KeyValuePair<int, int>(currPoint.Key + item.Key, currPoint.Value + item.Value));
                }
            }

            return result;
        }
    }
}
