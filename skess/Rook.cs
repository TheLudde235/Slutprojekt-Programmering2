using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skess
{
    class Rook : Piece
    {
        private readonly char RookSide;
        public Rook(string color, (int, int) pos) : base(color, pos)
        {
            if (color == "w")
            {
                base.Image = Properties.Resources.wRook;
            }
            else
            {
                base.Image = Properties.Resources.bRook;
            }

            if (pos.Item1 > 4) 
            {
                this.RookSide = 'K';
            }
            else
            {
               this. RookSide = 'Q';
            }

        }
        public override (int, int)[] GetMoves()
        {
            (int, int) pos = this.GetPos();
            List<(int, int)> list = new List<(int, int)>();
            (int, int) temp = pos;
            while (temp.Item2 < 8)
            {
                temp.Item2++;
                list = ValidateMove(list, temp);
                if (Globals.BoardDict[temp].GetPiece() != null) break;
            }
            temp = pos;
            while (temp.Item2 > 1)
            {
                temp.Item2--;
                list = ValidateMove(list, temp);
                if (Globals.BoardDict[temp].GetPiece() != null) break;
            }
            temp = pos;
            while (temp.Item1 > 0)
            {
                temp.Item1--;
                list = ValidateMove(list, temp);
                if (Globals.BoardDict[temp].GetPiece() != null) break;
            }
            temp = pos;
            while (temp.Item1 < 7)
            {
                temp.Item1++;
                list = ValidateMove(list, temp);
                if (Globals.BoardDict[temp].GetPiece() != null) break;
            }
            return list.ToArray();
        }
        public override (int, int)[] GetCheckableMoves()
        {
            (int, int) pos = this.GetPos();
            List<(int, int)> list = new List<(int, int)>();
            (int, int) temp = pos;
            while (temp.Item2 < 8)
            {
                temp.Item2++;
                list.Add(temp);
                if (Globals.BoardDict[temp].GetPiece() != null && !((Globals.BoardDict[temp].GetPiece() is King) && Globals.BoardDict[temp].GetPiece().GetColor() != this.GetColor())) break;
            }
            temp = pos;
            while (temp.Item2 > 1)
            {
                temp.Item2--;
                list.Add(temp);
                if (Globals.BoardDict[temp].GetPiece() != null && !((Globals.BoardDict[temp].GetPiece() is King) && Globals.BoardDict[temp].GetPiece().GetColor() != this.GetColor())) break;
            }
            temp = pos;
            while (temp.Item1 > 0)
            {
                temp.Item1--;
                list.Add(temp);
                if (Globals.BoardDict[temp].GetPiece() != null && !((Globals.BoardDict[temp].GetPiece() is King) && Globals.BoardDict[temp].GetPiece().GetColor() != this.GetColor())) break;
            }
            temp = pos;
            while (temp.Item1 < 7)
            {
                temp.Item1++;
                list.Add(temp);
                if (Globals.BoardDict[temp].GetPiece() != null && !((Globals.BoardDict[temp].GetPiece() is King) && Globals.BoardDict[temp].GetPiece().GetColor() != this.GetColor())) break;
            }

            return list.ToArray();
        }
        public override void Moved()
        {
            base.Moved();
            if (RookSide == 'K' && this.GetColor() == "w")
            {
                Globals.CastleArray[0] = "";
            }
            else if (RookSide == 'Q' && this.GetColor() == "w")
            {
                Globals.CastleArray[1] = "";
            }
            else if (RookSide == 'K' && this.GetColor() == "b")
            {
                Globals.CastleArray[2] = "";
            }
            else
            {
                Globals.CastleArray[3] = "";
            }
        }
        public override string ToString()
        {
            return base.ToString() + " rook";
        }
    }
}
