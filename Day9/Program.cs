using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day9
{
    class Program
    {
        static LinkedList<int> circle = new LinkedList<int>();
        static Dictionary<int, double> scores = new Dictionary<int, double>();
        static LinkedListNode<int> ptr;


        static void Next()
        {
            if (ptr == circle.Last)
                ptr = circle.First;
            else
                ptr = ptr.Next;
        }

        static void Previous()
        {
            if (ptr == circle.First)
                ptr = circle.Last;
            else
                ptr = ptr.Previous;
        }

        static void Main(string[] args)
        {
            int players = int.Parse(args[0]);
            int marbles = int.Parse(args[1]);
            int actPlayer;

            circle.AddFirst(0);
            ptr = circle.First;

            for (int i = 1; i <= marbles; i++)
            {
                actPlayer = (i - 1) % players;

                if (i % 23 != 0)
                {
                    Next();
                    Next();


                    circle.AddBefore(ptr, i);
                    Previous();
                }
                else
                {
                    if (!scores.ContainsKey(actPlayer))
                        scores.Add(actPlayer, i);
                    else
                        scores[actPlayer] += i;

                    for (int j = 0; j < 7; j++) Previous();

                    scores[actPlayer] += ptr.Value;

                    var removable = ptr;
                    if (ptr == circle.Last)
                        ptr = circle.First;
                    else
                        ptr = ptr.Next;

                    circle.Remove(removable);

                }
            }

            Console.WriteLine(scores.Max(s => s.Value));
            Console.ReadKey();
        }
    }
}
