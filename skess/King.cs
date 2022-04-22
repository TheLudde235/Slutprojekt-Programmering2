using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skess
{
    class King : Piece
    {
        public King(string color, (int, int) pos) : base(color, pos)
        {
            if (color == "w")
            {
                base.Image = Properties.Resources.wKing;
            }
            else
            {
                base.Image = Properties.Resources.bKing;
            }
        }


        public override (int, int)[] GetMoves()
        {
            (int, int) pos = this.GetPos();
            List<(int, int)> list = new List<(int, int)>();

            (int, int) temp = pos;
            temp.Item1--;
            temp.Item2--;
            list = CheckMove(list, temp);
            temp = pos;
            temp.Item1--;
            temp.Item2++;
            list = CheckMove(list, temp);
            temp = pos;
            temp.Item1--;
            list = CheckMove(list, temp);
            temp = pos;
            temp.Item1++;
            temp.Item2--;
            list = CheckMove(list, temp);
            temp = pos;
            temp.Item1++;
            temp.Item2++;
            list = CheckMove(list, temp);
            temp = pos;
            temp.Item1++;
            list = CheckMove(list, temp);
            temp = pos;
            temp.Item2--;
            list = CheckMove(list, temp);
            temp = pos;
            temp.Item2++;
            list = CheckMove(list, temp);

            // Code for casteling
            if (this.GetColor() == "w" && !Globals.InCheck(this.GetPos(), this.GetColor()))
            {
                if (Globals.CastleArray[0] == "K" && Globals.BoardDict[(5, 1)].GetPiece() == null && Globals.BoardDict[(6, 1)].GetPiece() == null && !Globals.InCheck((5, 1), "w") && !Globals.InCheck((6, 1), "w") && Globals.BoardDict[(7, 1)].GetPiece() is Rook && Globals.BoardDict[(7, 1)].GetPiece().GetColor() == this.GetColor())
                {
                    list.Add((6, 1));
                }
                if (Globals.CastleArray[1] == "Q" && Globals.BoardDict[(3, 1)].GetPiece() == null && Globals.BoardDict[(2, 1)].GetPiece() == null && Globals.BoardDict[(1, 1)].GetPiece() == null && !Globals.InCheck((3, 1), "w") && !Globals.InCheck((2, 1), "w") && Globals.BoardDict[(0, 1)].GetPiece() is Rook && Globals.BoardDict[(0, 1)].GetPiece().GetColor() == this.GetColor())
                {
                    list.Add((2, 1));
                }
            }
            else if (!Globals.InCheck(this.GetPos(), this.GetColor()))
            {
                if (Globals.CastleArray[2] == "k" && Globals.BoardDict[(5, 8)].GetPiece() == null && Globals.BoardDict[(6, 8)].GetPiece() == null && !Globals.InCheck((5, 8), "b") && !Globals.InCheck((6, 8), "b") && Globals.BoardDict[(7, 8)].GetPiece() is Rook && Globals.BoardDict[(7, 8)].GetPiece().GetColor() == this.GetColor())
                {
                    list.Add((6, 8));
                }
                if (Globals.CastleArray[3] == "q" && Globals.BoardDict[(3, 8)].GetPiece() == null && Globals.BoardDict[(2, 8)].GetPiece() == null && Globals.BoardDict[(1, 8)].GetPiece() == null && !Globals.InCheck((3, 8), "b") && !Globals.InCheck((2, 8), "b") && Globals.BoardDict[(0, 8)].GetPiece() is Rook && Globals.BoardDict[(0, 8)].GetPiece().GetColor() == this.GetColor())
                {
                    list.Add((2, 8));
                }
            }
            
            return list.ToArray();
        }
        public override (int, int)[] GetCheckableMoves()
        {
            (int, int) pos = this.GetPos();
            List<(int, int)> list = new List<(int, int)>();

            (int, int) temp = pos;
            temp.Item1--;
            temp.Item2--;

            if (temp.Item1 >= 0 && temp.Item2 <= 8 && temp.Item1 <= 7 && temp.Item2 >= 0) list.Add(temp);

            temp = pos;
            temp.Item1--;
            temp.Item2++;

            if (temp.Item1 >= 0 && temp.Item2 <= 8 && temp.Item1 <= 7 && temp.Item2 >= 0) list.Add(temp);

            temp = pos;
            temp.Item1--;

            if (temp.Item1 >= 0 && temp.Item2 <= 8 && temp.Item1 <= 7 && temp.Item2 >= 0) list.Add(temp);

            temp = pos;
            temp.Item1++;
            temp.Item2--;

            if (temp.Item1 >= 0 && temp.Item2 <= 8 && temp.Item1 <= 7 && temp.Item2 >= 0) list.Add(temp);

            temp = pos;
            temp.Item1++;
            temp.Item2++;

            if (temp.Item1 >= 0 && temp.Item2 <= 8 && temp.Item1 <= 7 && temp.Item2 >= 0) list.Add(temp);

            temp = pos;
            temp.Item1++;

            if (temp.Item1 >= 0 && temp.Item2 <= 8 && temp.Item1 <= 7 && temp.Item2 >= 0) list.Add(temp);

            temp = pos;
            temp.Item2--;

            if (temp.Item1 >= 0 && temp.Item2 <= 8 && temp.Item1 <= 7 && temp.Item2 >= 0) list.Add(temp);

            temp = pos;
            temp.Item2++;

            if (temp.Item1 >= 0 && temp.Item2 <= 8 && temp.Item1 <= 7 && temp.Item2 >= 0) list.Add(temp);

            return list.ToArray();
        }
        public override void Moved()
        {
            base.Moved();
            if (this.GetColor() == "w")
            {
                Globals.CastleArray[0] = "";
                Globals.CastleArray[1] = "";
            }
            else
            {
                Globals.CastleArray[2] = "";
                Globals.CastleArray[3] = "";
            }
        }

        private List<(int, int)> CheckMove(List<(int, int)> list, (int, int) temp)
        {
            if (temp.Item1 >= 0 && temp.Item2 <= 8 && temp.Item1 <= 7 && temp.Item2 > 0 && !Globals.InCheck(temp, this.GetColor()))
            {
                if (Globals.BoardDict[temp].GetPiece() != null)
                {
                    if (Globals.BoardDict[temp].GetPiece().GetColor() != this.GetColor())
                    {
                        list = ValidateMove(list, temp);
                    }
                }
                else
                {
                    list = ValidateMove(list, temp);
                }
            }
            return list;
        }



        public override string ToString()
        {
            return base.ToString() + " king";
        }
    }
}
