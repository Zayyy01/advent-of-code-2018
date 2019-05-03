using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day9
{
    class Program
    {
        static List<int> circle = new List<int>();
        static Dictionary<int, int> scores = new Dictionary<int, int>();
        static int ptr = 0;


        static void Next()
        {
            ptr++;
            if (ptr >= circle.Count)
                ptr = 0;
        }

        static void Previous()
        {
            ptr--;
            if (ptr < 0)
                ptr = circle.Count - 1;
        }

        static void Main(string[] args)
        {
            int players = int.Parse(args[0]);
            int marbles = int.Parse(args[1]);
            int actPlayer;

            circle.Add(0);

            for (int i = 1; i <= marbles; i++)
            {
                actPlayer = (i - 1) % players;

                if(i % 23 != 0)
                {
                    Next();
                    Next();

                    circle.Insert(ptr, i);
                }
                else
                {
                    if(!scores.ContainsKey(actPlayer))
                        scores.Add(actPlayer, i);
                    else
                        scores[actPlayer] += i;

                    for (int j = 0; j < 7; j++) Previous();

                    scores[actPlayer] += circle.ElementAt(ptr);

                    circle.RemoveAt(ptr);

                    if(ptr >= circle.Count)
                        ptr = 0;
                }
            }

            Console.WriteLine(scores.Max(s => s.Value));
            Console.ReadKey();
        }
    }
}
