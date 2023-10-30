using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Threading;

namespace SchoolProject
{
    public static class Util
    {
        public static string RepeatChar(char c, int count)
        {
            return new string(c, count);
        }

        public static string ReplaceIndex(string Input ,int Index, char c)
        {
            string String1 = Input.Substring(0, Index);
            string String2 = Input.Substring(Index + 1, Input.Length - Index - 1);
            return String1 + c + String2;
        }

        public static void ClearInputBuffer()
        {
            while (Console.KeyAvailable)
            {
                Console.ReadKey(intercept: true);
            }
            /*
            while (Console.KeyAvailable)
            {
                Console.ReadKey(false);
                Console.SetCursorPosition(0, Console.CursorTop);
            }
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(" ");
            Console.SetCursorPosition(0, Console.CursorTop);*/
        }

        public static ConsoleKey GetInput(bool Intercept = false, params ConsoleKey[] ValidInputs)
        {
            int L = Console.CursorLeft;
            int T = Console.CursorTop;

            ConsoleKey Input = Console.ReadKey(Intercept).Key;
            while (!ValidInputs.Contains(Input))
            {
                Console.SetCursorPosition(L, T);
                Console.Write(' ');
                Console.SetCursorPosition(L, T);
                Input = Console.ReadKey().Key;
            }
            return Input;
        }
 
        public static void WipeScreen(int StartLine = 0)
        {
            Console.CursorVisible = false;
            int height = Console.CursorTop - StartLine;
            int width = Console.WindowWidth;

            int TotalPasses = width + height - 1;

            int xFrom = 0, xTo = 0;
            int yFrom = 0, yTo = 0;
            for (int i = 0; i < TotalPasses; i++)
            {
                int x = xFrom;
                int y = yFrom;
                int TotalSteps = (xTo - xFrom + yFrom - yTo + 2) / 2;

                for (int s = 1; s <= TotalSteps; s++)
                {
                    Console.SetCursorPosition(x, y + StartLine);
                    Console.Write(" ");

                    x++;
                    y--;
                }

                if (xTo < width - 1)
                    xTo++;
                else
                    yTo++;

                if (yFrom < height)
                    yFrom++;
                else
                    xFrom++;

                if (i % 2 == 0)
                    Thread.Sleep(1);
                Console.SetCursorPosition(0, StartLine);
                Console.CursorVisible = true;
            }
        }

        public static void ClearFromLine(int StartLine)
        {
            int Height = Console.CursorTop - StartLine;
            for (int i = 0; i <= Height; i++)
            {
                Console.SetCursorPosition(0, i + StartLine);
                Console.Write(new string(' ', Console.WindowWidth));
            }
            Console.SetCursorPosition(0, StartLine);
        }

        public static int TrueMod(int a, int b)
        {
            return a - b * (int)Math.Floor((float)a / (float)b);
        }

        public static float Clamp(float t, float From, float To)
        {
            float T = t;
            if (T <= From) T = From;
            else if (T >= To) T = To;
            return T;
        }

        public static int Wrap(int t, int To)
        {
            return TrueMod(t, To);
        }

        public static bool CTSDisposed = true;
        static DateTime LastTimePressed;
        public static void ListenForKeyPress()
        {
            System.Timers.Timer timer = new System.Timers.Timer(100);
            timer.Enabled = true;
            LastTimePressed = DateTime.Now;

            while (true)
            {
                if(!CTSDisposed)
                {
                    while (!Console.KeyAvailable)
                    {
                        if (CTSDisposed) break;
                    }
                    if(!CTSDisposed)
                    {
                        if (DateTime.Now.Subtract(LastTimePressed) > TimeSpan.FromMilliseconds(100)) //Last time pressed a key is more than 100 ms ago
                        {
                            LastTimePressed = DateTime.Now;
                            if (!CTSDisposed)
                                Game.cts.Cancel();
                        }
                    }
                }
            }
        }
    }
}
