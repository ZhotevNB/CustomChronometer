using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Clock
{

    public class Program
    {

        static bool isTimerRuning;
        private static Stopwatch watch = new Stopwatch();
        public static Stopwatch lapWatch = new Stopwatch();
        private const int textRow = 9;
        private const int stopWatchRow = 11;
        private static int lap = 1;
        static void Main(string[] args)
        {
            ShowMenu();

            // MONITORING AND SHOW THE WATCH USING OTHER TASK

            CancellationTokenSource cts = new CancellationTokenSource();
            var task = new Task(() => ShowTheWatchInLineNumber(stopWatchRow, cts));
            task.Start();

            // DRAW MENU AND MONITORING USER INPUT
            while (!task.IsCompleted)
            {

                var keyInput = Console.ReadKey(true);
                if (!Console.KeyAvailable)
                {
                    if (keyInput.Key == ConsoleKey.Spacebar)
                    {
                        if (isTimerRuning)
                        {
                            watch.Stop();
                            lapWatch.Stop();
                            CleanTheRow(textRow);
                            Console.ForegroundColor = ConsoleColor.Red;
                            ShowMessageInLineNumber(textRow, "Spacebar pressed: Timer is stoped.");
                        }
                        else
                        {
                            watch.Start();
                            lapWatch.Start();
                            CleanTheRow(textRow);
                            Console.ForegroundColor = ConsoleColor.Green;
                            ShowMessageInLineNumber(textRow, "Spacebar pressed: Timer is running.");
                        }
                        isTimerRuning = !isTimerRuning;
                    }

                    else if (keyInput.Key == ConsoleKey.R)
                    {
                        isTimerRuning = false;
                        watch.Reset();
                        lapWatch.Reset();
                        CleanTheRow(stopWatchRow);
                        CleanTheRow(textRow);
                        Console.ForegroundColor = ConsoleColor.Green;
                        ShowMessageInLineNumber(textRow, "R pressed: Reset timer in 0.25s.");
                        Task.Delay(250).Wait();
                        for (int i = textRow; i < stopWatchRow + 3 + lap; i++)
                        {
                            CleanTheRow(i);
                        }
                        lap = 1;
                    }

                    else if (keyInput.Key == ConsoleKey.L)
                    {

                        var lapTime = lapWatch.Elapsed.ToString(@"mm\:ss\.ffff");
                        Console.ForegroundColor = ConsoleColor.Green;
                        ShowMessageInLineNumber(stopWatchRow + 3 + lap, $"{lap++}. {lapTime}");
                        lapWatch.Reset();
                        if (isTimerRuning)
                        {
                            lapWatch.Start();
                        }
                         
                    }
                    else if (keyInput.Key == ConsoleKey.Escape)
                    {
                        cts.Cancel();
                        Console.ForegroundColor = ConsoleColor.White;
                        ShowMessageInLineNumber(textRow, "ESC pressed: Exiting program in 0.25s.");
                        Task.Delay(250).Wait();
                        break;
                    }

                    //wait for Console to draw UI. 
                    Task.Delay(85).Wait();
                }
            }
        }

        private static void CleanTheRow(int row)
        {
            Console.SetCursorPosition(0, row);
            Console.WriteLine(new string(' ', Console.WindowWidth));
        }

        private static void ShowMenu()
        {
            Console.WriteLine("==============SIMPLE STOPWATCH===================");
            Console.WriteLine("*                                               *");
            Console.WriteLine("* {0,-10} to reset timer in 1s.              *", "[R]");
            Console.WriteLine("* {0,-10} to mark a lap time                 *", "[L]");
            Console.WriteLine("*                                               *");
            Console.WriteLine("* {0,-10} to start/pause the watch.          *", "[SPACE]");
            Console.WriteLine("* {0,-10} to quit the program in 1s.         *", "[ESC]");
            Console.WriteLine("*                                               *");
            Console.WriteLine("=================================================");


        }


        private static void ShowMessageInLineNumber(int row, string mess)
        {
            Console.SetCursorPosition(0, row);
            Console.WriteLine(mess);
        }

        static void ShowTheWatchInLineNumber(int row, CancellationTokenSource _cts)
        {

            Task.Delay(1).Wait();
            while (!_cts.IsCancellationRequested)
            {
                Console.SetCursorPosition(0, row);
                if (isTimerRuning && watch.ElapsedMilliseconds != 0)
                {
                    var time = watch.Elapsed.ToString(@"mm\:ss\.ffff");
                    Console.WriteLine(time);
                }
                Task.Delay(1).Wait();

            }
        }
    }

}