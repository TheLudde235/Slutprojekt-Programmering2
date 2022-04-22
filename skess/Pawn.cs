using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace skess
{
    class Pawn : Piece
    {
        public Pawn(string color, (int, int) pos) : base(color, pos)
        {
            if (color == "w")
            {
                base.Image = Properties.Resources.wPawn;
            }
            else
            {
                base.Image = Properties.Resources.bPawn;
            }
        }

        public override (int, int)[] GetMoves()
        {
            (int, int) pos = this.GetPos();
            List<(int, int)> list = new List<(int, int)>();

            int steps = 1;

            if (!base.HasMoved)
            {
                if (this.GetColor() == "w" && pos.Item2 == 2)
                {
                    steps = 2;
                }
                else if (this.GetColor() == "b" && pos.Item2 == 7)
                {
                    steps = 2;
                }
            }
            (int, int) temp = pos;
            while ( this.GetColor() == "w" && temp.Item2 < pos.Item2 + steps)
            {
                temp.Item2++;

                if (pos.Item2 < 8)
                {
                    if (pos.Item1 < 7)
                    {
                        if (Globals.BoardDict[(pos.Item1 + 1, pos.Item2 + 1)].GetPiece() != null || Globals.EnPassantPos == (pos.Item1 + 1, pos.Item2 + 1)) list = ValidateMove(list, (pos.Item1 + 1, pos.Item2 + 1));
                    }
                    if (pos.Item1 > 0)
                    {
                        if (Globals.BoardDict[(pos.Item1 - 1, pos.Item2 + 1)].GetPiece() != null || Globals.EnPassantPos == (pos.Item1 - 1, pos.Item2 + 1)) list = ValidateMove(list, (pos.Item1 - 1, pos.Item2 + 1));
                    }
                    if (Globals.BoardDict[temp].GetPiece() == null) list = ValidateMove(list, temp);
                    else break;
                }
            }           
            while (this.GetColor() == "b" && temp.Item2 > pos.Item2 - steps)
            {
                temp.Item2--;

                if  (pos.Item2 > 1)
                {
                    if (pos.Item1 < 7)
                    {
                        if (Globals.BoardDict[(pos.Item1 + 1, pos.Item2 - 1)].GetPiece() != null || Globals.EnPassantPos == (pos.Item1 + 1, pos.Item2 - 1)) list = ValidateMove(list, (pos.Item1 + 1, pos.Item2 - 1));
                    }
                    if (pos.Item1 > 0)
                    {
                        if (Globals.BoardDict[(pos.Item1 - 1, pos.Item2 - 1)].GetPiece() != null || Globals.EnPassantPos == (pos.Item1 - 1, pos.Item2 - 1)) list = ValidateMove(list, (pos.Item1 - 1, pos.Item2 -1));
                    }
                    if (Globals.BoardDict[temp].GetPiece() == null) list = ValidateMove(list, temp);
                    else break;
                }
            }
            return list.ToArray();
        }
        public override (int, int)[] GetCheckableMoves()
        {
            List<(int, int)> list = new List<(int, int)>();
            (int, int) pos = this.GetPos();
            if (this.GetColor() == "w")
            {
                if (pos.Item1 > 0) list.Add((pos.Item1 - 1, pos.Item2 + 1));
                if (pos.Item1 < 7) list.Add((pos.Item1 + 1, pos.Item2 + 1));
            }
            else
            { 
                if (pos.Item1 > 0) list.Add((pos.Item1 - 1, pos.Item2 - 1));
                if (pos.Item1 < 7) list.Add((pos.Item1 + 1, pos.Item2 - 1));
            }
            return list.ToArray();
        }
        public override void Moved()
        {
            Globals.HalfMoveClock = 0;
            base.Moved();
        }
        public override string ToString()
        {
            return base.ToString() + " pawn";
        }
    }
}