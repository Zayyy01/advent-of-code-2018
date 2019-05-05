using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Day10
{
    class Program
    {
        static List<(int x, int y, int vx, int vy)> points = new List<(int x, int y, int vx, int vy)>();

        static void HandleInputFile(string path)
        {

            using (var sr = File.OpenText(path))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    MatchCollection t = Regex.Matches(line, @"-?\d+");
                    string[] nbrs = Regex.Matches(line, @"-?\d+").OfType<Match>().Select(m => m.Value).ToArray();
                    var x = int.Parse(nbrs[0]);
                    var y = int.Parse(nbrs[1]);
                    var vx = int.Parse(nbrs[2]);
                    var vy = int.Parse(nbrs[3]);

                    points.Add((x, y, vx, vy));
                }

            }
        }

        static int YRange_t(List<(int x, int y, int vx, int vy)> points, int t)
        {
            var newP = points.Select(p => (p.y + p.vy * t));
            return newP.Max() - newP.Min();
        }


        static void Main(string[] args)
        {
            HandleInputFile(args[0]);
            int bestTime = 0;
            Stopwatch sw = new Stopwatch();
            sw.Start();

            bestTime = Enumerable.Range(0, YRange_t(points, 0))
                       .SkipWhile(i => YRange_t(points, i) > YRange_t(points, i + 1))
                       .First();

            var newPoints = points.Select(origpt => 
                            (x: origpt.x + bestTime * origpt.vx,
                             y: origpt.y + bestTime * origpt.vy));

            var minx = newPoints.Min(p => p.x);
            var maxx = newPoints.Max(p => p.x);
            var miny = newPoints.Min(p => p.y);
            var maxy = newPoints.Max(p => p.y);

            foreach (var y in Enumerable.Range(miny, maxy - miny + 1))
            {
                foreach (var x in Enumerable.Range(minx, maxx - minx + 1))
                    Console.Write(newPoints.Contains((x, y))? '#' : '.');

                Console.WriteLine();
            }
            sw.Stop();

            Console.WriteLine(bestTime);
            Console.WriteLine($"time: {sw.ElapsedMilliseconds}");
            Console.ReadLine();
        }
    }
}
