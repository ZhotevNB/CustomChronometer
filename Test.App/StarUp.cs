using System;
using System.Threading;

namespace Chronometer
{
    class StarUp
    {
        static void Main(string[] args)
        {
            var chronometer = new Chronometer();

            while (true)
            {
                var userInput =Console.ReadLine().ToLower();

                if (userInput =="exit")
                {
                    break;
                }

                switch (userInput)
                {

                    case "start":

                        chronometer.Start();
                       // TreadingClock.TreadingShowClock(chronometer);
                        break;

                    case "stop":

                        chronometer.Stop();
                        break;

                    case "reset":

                        chronometer.Reset();
                        chronometer.Stop();
                        break;

                    case "time":

                        Console.WriteLine(chronometer.GetTime);
                        break;
                    case "lap":

                        Console.WriteLine(chronometer.Lap());
                        break;
                    case "laps":

                        var laps = chronometer.Laps;

                        Console.Write("Laps: ");
                        if (laps.Count == 0)
                        {
                            Console.WriteLine("no laps");
                        }
                        else
                        {
                            Console.WriteLine();
                            var i = 0;
                            foreach (var item in laps)
                            {
                                Console.WriteLine($"{i++}. {item}");

                            }
                        }
                            break;
                    default: Console.WriteLine("Invalid Comand");
                        break;
                }
                
            }
        }
        public static class TreadingClock
        {
            public static void TreadingShowClock(Chronometer chronometer)
            {
                while (true)
                {
                    int currentLineCursor = Console.CursorTop;
                    Console.WriteLine(chronometer.GetTime);
                    Console.SetCursorPosition(0, Console.CursorTop);
                    Console.WriteLine(new string(' ',Console.WindowWidth));
                    Console.SetCursorPosition(0, currentLineCursor);


                }
            }
        }
    }
}
