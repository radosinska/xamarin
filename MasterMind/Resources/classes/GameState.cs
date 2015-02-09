using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace MasterMind
{
    class GameState : Java.Lang.Object, Java.IO.ISerializable
    {
        public LinearLayout[,] Fields { get; set; }
        public LinearLayout[,] Score { get; set; }
        public int[,] Board { get; set; }
        public int[] Puzzle { get; set; }
        public int Level { get; set; }
        public double Points { get; set; }


        public GameState(IntPtr handle, JniHandleOwnership transfer)
            : base(handle, transfer)
        { }

        public GameState(LinearLayout[,] fields, LinearLayout[,] score, int[,] board,int[]puzzle, int level, double points)
        {
            Fields = new LinearLayout[level+1, 4];
            Score = new LinearLayout[level+1, 4];
            Board = new int[level+1, 4];
            Puzzle = new int[4];
            Level = level;
            Points = points;

            for(int j=0; j<4;j++)
            {
                Puzzle[j] = puzzle[j];
                for(int i=0; i<level+1;i++)
                {
                    Board[i, j] = board[i, j];
                    Fields[i, j] = fields[i, j];
                    Score[i, j] = score[i, j];
                }
            }
        }

    }
}