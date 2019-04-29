using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Day7
{
    class Program
    {
        private static Dictionary<char, List<char>> precedessors = new Dictionary<char, List<char>>(26);
        private static List<KeyValuePair<char, int>> workers = new List<KeyValuePair<char, int>>(5);
        private const int CharOffset = 65;

        static void HandleInputFile(string path)
        {
            using (var sr = File.OpenText(path))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    var split = Regex.Split(line, "step ", RegexOptions.IgnoreCase);
                    char precedessor = split[1].First();
                    char successor = split[2].First();

                    precedessors[successor].Add(precedessor);
                }
            }
        }

        static char getFirstAcceptedStep(Dictionary<char, List<char>> d)
        {
            return d.First(p => p.Value.Count == 0).Key;
        }

        static void deleteStepFromLists(Dictionary<char, List<char>> d, char c)
        {
            foreach (var list in d.Values)
            {
                list.Remove(c);
            }
        }

        static void deleteStep(Dictionary<char, List<char>> d, char c) => d.Remove(c);

        static void decreaseTimeForWorkers(int t)
        {
            for (int i = 0; i < workers.Count; i++)
            {
                var pair = new KeyValuePair<char, int>(workers[i].Key, workers[i].Value - t);
                workers[i] = pair;
            }
        }

        static void Main(string[] args)
        {
            InitCollections();

            HandleInputFile(args[0]);

            //part1
            WriteStepOrder();

            //part2
            CalculateTime();

            Console.ReadKey();
        }

        private static void CalculateTime()
        {
            var prec = new Dictionary<char, List<char>>(precedessors);
            int time = 0;
            do
            {
                char nextStep;

                while (workers.Any(w => w.Value <= 0))
                {
                    foreach (var doneStep in workers.Where(w => w.Value == 0))
                    {
                        deleteStepFromLists(prec, doneStep.Key);
                    }
                    int indexofFreeWorker = workers.IndexOf(workers.First(w => w.Value <= 0));

                    try
                    {
                        nextStep = getFirstAcceptedStep(prec);
                    }
                    catch (InvalidOperationException)
                    {
                        break;
                    }

                    workers[indexofFreeWorker] = new KeyValuePair<char, int>(nextStep, 61 + nextStep - CharOffset);
                    deleteStep(prec, nextStep);
                }

                int minTime = workers.Where(w => w.Value > 0)
                                       .OrderBy(w => w.Value)
                                       .FirstOrDefault().Value;

                decreaseTimeForWorkers(minTime);
                time += minTime;
            } while (precedessors.Any(pair => pair.Value.Any()));

            int RemainingTime = workers.Where(w => w.Value > 0)
                                       .OrderByDescending(w => w.Value)
                                       .FirstOrDefault().Value;

            Console.WriteLine(time + RemainingTime);
        }

        private static void InitCollections()
        {
            for (int i = 'A'; i <= 'Z'; ++i)
            {
                precedessors.Add((char)i, new List<char>());
            }

            for (int i = 0; i < 5; ++i)
            {
                workers.Add(new KeyValuePair<char, int>('!', 0));
            }
        }

        private static void WriteStepOrder()
        {
            var prec = new Dictionary<char, List<char>>();
            foreach (var list in precedessors)
            {
                prec.Add(list.Key, list.Value.ToList());
            }
            while (prec.Any(l => l.Value.Count > 0))
            {
                char nextStep = getFirstAcceptedStep(prec);
                Console.Write(nextStep);
                deleteStepFromLists(prec, nextStep);
                deleteStep(prec, nextStep);
            }
            Console.WriteLine();
        }
    }
}
