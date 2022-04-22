using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace skess
{
    class Queen : Piece
    {
        public Queen(string color, (int, int) pos) : base(color, pos)
        {
            if (color == "w")
            {
                base.Image = Properties.Resources.wQueen;
            }
            else
            {
                base.Image = Properties.Resources.bQueen;
            }
        }

        public override (int, int)[] GetMoves()
        {
            (int, int) pos = this.GetPos();
            List<(int, int)> list = new List<(int, int)>();
            (int, int) temp = pos;
            // straights
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
            temp = pos;
            // diagonals
            while (temp.Item1 > 0 && temp.Item2 > 1)
            {
                temp.Item1--;
                temp.Item2--;
                list = ValidateMove(list, temp);
                if (Globals.BoardDict[temp].GetPiece() != null) break;
            }
            temp = pos;
            while (temp.Item1 > 0 && temp.Item2 < 8)
            {
                temp.Item1--;
                temp.Item2++;
                list = ValidateMove(list, temp);
                if (Globals.BoardDict[temp].GetPiece() != null) break;
            }
            temp = pos;
            while (temp.Item1 < 7 && temp.Item2 > 1)
            {
                temp.Item1++;
                temp.Item2--;
                list = ValidateMove(list, temp);
                if (Globals.BoardDict[temp].GetPiece() != null) break;
            }
            temp = pos;
            while (temp.Item1 < 7 && temp.Item2 < 8)
            {
                temp.Item1++;
                temp.Item2++;
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
            
            while (temp.Item1 > 0 && temp.Item2 > 1)
            {
                temp.Item1--;
                temp.Item2--;
                list.Add(temp);
                if (Globals.BoardDict[temp].GetPiece() != null && !((Globals.BoardDict[temp].GetPiece() is King) && Globals.BoardDict[temp].GetPiece().GetColor() != this.GetColor())) break;
            }
            temp = pos;
            while (temp.Item1 > 0 && temp.Item2 < 8)
            {
                temp.Item1--;
                temp.Item2++;
                list.Add(temp);
                if (Globals.BoardDict[temp].GetPiece() != null && !((Globals.BoardDict[temp].GetPiece() is King) && Globals.BoardDict[temp].GetPiece().GetColor() != this.GetColor())) break;
            }
            temp = pos;
            while (temp.Item1 < 7 && temp.Item2 > 1)
            {
                temp.Item1++;
                temp.Item2--;
                list.Add(temp);
                if (Globals.BoardDict[temp].GetPiece() != null && !((Globals.BoardDict[temp].GetPiece() is King) && Globals.BoardDict[temp].GetPiece().GetColor() != this.GetColor())) break;
            }
            temp = pos;
            while (temp.Item1 < 7 && temp.Item2 < 8)
            {
                temp.Item1++;
                temp.Item2++;
                list.Add(temp);
                if (Globals.BoardDict[temp].GetPiece() != null && !((Globals.BoardDict[temp].GetPiece() is King) && Globals.BoardDict[temp].GetPiece().GetColor() != this.GetColor())) break;
            }
            temp = pos;
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
        public override string ToString()
        {
            return base.ToString() + " queen";
        }
    }
}
