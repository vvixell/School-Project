using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace SchoolProject
{
    public class SkillCheck
    {
        #region SkillCheck Properties
        public enum SkillCheckType 
        {
            Normal,
            Reaction
        }
        SkillCheckType skillCheckType;

        /// The size of in characters of the skillcheck
        int SkillCheckCellSize = 50;

        /// The Size of the hit point in characters
        int SkillCheckSize = 5;

        #endregion SkillCheck Properties

        #region Slider Properties

        /// Amount of cells that shrinks the slider hit possible postion from each side
        int SliderSideBuffer = 4;

        /// Milliseconds to wait before moving the slider
        int MoveDelay;

        /// -1 for right, 1 for left
        int MoveDirection;

        #endregion

        #region Vital Variables
        /// Where the part of the skillcheck you have to hit is located
        int SliderPosition;

        /// Values of each character in the skillcheck
        int[] Values;

        /// The Visuals of the skill check
        string SkillCheckString;

        /// What line the visuals are on
        int SkillCheckStringLine;

        #endregion Vital Variables

        #region Return Variables

        // True if SkillCheck has been passed
        bool passed = false;

        public bool Passed { get { return passed; } }

        #endregion Return Variables

        public SkillCheck(float SliderSpeed, int HitSize, SkillCheckType type = SkillCheckType.Normal, bool RandomSide = false)
        {
            skillCheckType = type;

            SetSkillCheckValues(SliderSpeed, HitSize, RandomSide);

            CreateSkillCheck();

            DoSkillCheck();
        }

        void DrawSkillCheck()
        {
            string currentMiddleString = Util.ReplaceIndex(SkillCheckString, SliderPosition + 2, OtherAscii.SkillCheckBoxLeft);
            currentMiddleString = Util.ReplaceIndex(currentMiddleString, SliderPosition + 4, OtherAscii.SkillCheckBoxRight);
            Console.SetCursorPosition(0, SkillCheckStringLine);
            Console.WriteLine(currentMiddleString);
        }

        void DoSkillCheck()
        {
            while (Console.KeyAvailable == false) //Loops untill Key is pressed
            {
                DrawSkillCheck();
                Thread.Sleep(MoveDelay);

                SliderPosition += MoveDirection;
                if (skillCheckType == SkillCheckType.Reaction) 
                {
                    if(SliderPosition == SkillCheckCellSize - 1)
                        break;
                    if (SliderPosition == 0)
                        break;
                } 
                if (SliderPosition == SkillCheckCellSize - 1 || SliderPosition == 0) MoveDirection = -MoveDirection; //Reverses Movedirection when the slider is at the edge of the skillcheck

            }
            if(Console.KeyAvailable == true)
            {
                Console.ReadKey(true);
                passed = Values[SliderPosition] != 4;
                Console.SetCursorPosition(0, SkillCheckStringLine + 2);
            }
            else
            {
                passed = false;
            }
            DrawFinal();
        } 

        string GetSkillCheckString()
        {
            string Line = string.Empty;

            for (int i = 0; i < SkillCheckCellSize; i++)
                Line = Line + Shades.ShadesList[Values[i]];

            return Line;
        }

        void SetSkillCheckValues(float SliderSpeed, int HitSize, bool RandomSide)
        {
            SkillCheckSize = HitSize;
            SliderPosition = Game.rand.Next(SkillCheckSize + SliderSideBuffer + 1 + SkillCheckSize, SkillCheckCellSize - 1 - SkillCheckSize - SliderSideBuffer);
            MoveDelay = (int)(1000 / SkillCheckCellSize / SliderSpeed);

            Values = new int[SkillCheckCellSize];
            for (int i = 0; i < SkillCheckCellSize; i++)
            {
                int Distance = Math.Abs(i - SliderPosition);
                Values[i] = 
                    Distance <= SkillCheckSize ? 0 : 
                    Distance <= SkillCheckSize + 1 ? 1 : 
                    Distance <= SkillCheckSize + 2 ? 2 : 
                    Distance <= SkillCheckSize + 3 ? 3 : 4;
            }

            SliderPosition = 0;
            MoveDirection = 1;
            if (RandomSide)
            {
                MoveDirection = Game.rand.Next(0,2) * 2 - 1;
                if (MoveDirection == -1) SliderPosition = SkillCheckCellSize - 1;
            }

        }

        void CreateSkillCheck()
        {
            string enclosingString = " " + Box.Corner + Util.RepeatChar(Box.Horizontal, SkillCheckCellSize + 2) + Box.Corner + " ";
            SkillCheckString = $"{Box.Left}{Box.Right} {GetSkillCheckString()} {Box.Left}{Box.Right}";

            Console.WriteLine();
            Console.WriteLine(enclosingString);
            SkillCheckStringLine = Console.CursorTop;
            Console.WriteLine(SkillCheckString);
            Console.WriteLine(enclosingString);
            Console.WriteLine("\n");
        }

        void DrawFinal()
        {
            Console.SetCursorPosition(0, SkillCheckStringLine);
            Console.Write($"{Box.Left}{Box.Right}");
            Console.ForegroundColor = passed ? ConsoleColor.Green : ConsoleColor.Red;
            string currentMiddleString = Util.ReplaceIndex($" {GetSkillCheckString()} ", SliderPosition, OtherAscii.SkillCheckBoxLeft);
            currentMiddleString = Util.ReplaceIndex(currentMiddleString, SliderPosition + 2, OtherAscii.SkillCheckBoxRight);
            Console.Write(currentMiddleString);
            Console.ResetColor();
            Console.Write($"{Box.Left}{Box.Right}");
            Console.SetCursorPosition(0, SkillCheckStringLine + 3);
        }
    }
}
