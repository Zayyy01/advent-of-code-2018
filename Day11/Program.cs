using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day11
{
    class Program
    {
        static List<List<int>> grid = new List<List<int>>();

        static void Main(string[] args)
        {
            var sNbr = int.Parse(args[0]);
            for (int y = 1; y < 301; y++)
            {
                var row = new List<int>();
                for (int x = 1; x < 301; x++)
                {
                    var rackID = x + 10;
                    var pwrLvl = (((rackID * y + sNbr) * rackID) / 100) % 10 - 5;
                    row.Add(pwrLvl);
                }
                grid.Add(row);
            }

            var maxVal = int.MinValue;
            (int, int, int) coord = (-1, -1, -1);


            for (int y = 1; y < 301; y++)
            {
                for (int x = 1; x < 301; x++)
                {
                    var val = 0;
                    for (int size = 1; size < Math.Min(301 - y, 301 - x); size++)
                    {
                        for (int i = 0; i < size; i++)
                        {
                            val += grid[y + i - 1][x + size - 2];
                        }
                        for (int j = 0; j < size - 1; j++)
                        {
                            val += grid[y + size - 2][x + j - 1];
                        }

                        if (val > maxVal)
                        {
                            maxVal = val;
                            coord = (x, y, size);
                        }
                    }
                }
            }
            Console.WriteLine(coord);
            Console.ReadKey();
        }
    }
}
