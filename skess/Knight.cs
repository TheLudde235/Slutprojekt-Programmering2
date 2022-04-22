using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skess
{
    class Knight : Piece
    {
        public Knight(string color, (int, int) pos) : base(color, pos)
        {
            if (color == "w")
            {
                base.Image = Properties.Resources.wKnight;
            }
            else
            {
                base.Image = Properties.Resources.bKnight;
            }
        }

        public override (int, int)[] GetMoves()
        {
            (int, int) pos = this.GetPos();
            List<(int, int)> list = new List<(int, int)>();
            (int, int) temp = pos;

            temp.Item1 -= 2;
            temp.Item2++;
            if (temp.Item1 >= 0 && temp.Item2 <= 8 && temp.Item1 <= 7 && temp.Item2 > 0) list = ValidateMove(list, temp);
            temp.Item2 -= 2;
            if (temp.Item1 >= 0 && temp.Item2 <= 8 && temp.Item1 <= 7 && temp.Item2 > 0) list = ValidateMove(list, temp);
            temp.Item1 += 4;
            if (temp.Item1 >= 0 && temp.Item2 <= 8 && temp.Item1 <= 7 && temp.Item2 > 0) list = ValidateMove(list, temp);
            temp.Item2 += 2;
            if (temp.Item1 >= 0 && temp.Item2 <= 8 && temp.Item1 <= 7 && temp.Item2 > 0) list = ValidateMove(list, temp);
            temp = pos;
            temp.Item2 += 2;
            temp.Item1--;
            if (temp.Item1 >= 0 && temp.Item2 <= 8 && temp.Item1 <= 7 && temp.Item2 > 0) list = ValidateMove(list, temp);
            temp.Item1 += 2;
            if (temp.Item1 >= 0 && temp.Item2 <= 8 && temp.Item1 <= 7 && temp.Item2 > 0) list = ValidateMove(list, temp);
            temp.Item2 -= 4;
            if (temp.Item1 >= 0 && temp.Item2 <= 8 && temp.Item1 <= 7 && temp.Item2 > 0) list = ValidateMove(list, temp);
            temp.Item1 -= 2;
            if (temp.Item1 >= 0 && temp.Item2 <= 8 && temp.Item1 <= 7 && temp.Item2 > 0) list = ValidateMove(list, temp);
            return list.ToArray();
        }
        public override (int, int)[] GetCheckableMoves()
        {
            (int, int) pos = this.GetPos();
            List<(int, int)> list = new List<(int, int)>();
            (int, int) temp = pos;

            temp.Item1 -= 2;
            temp.Item2++;
            if (temp.Item1 >= 0 && temp.Item2 <= 8 && temp.Item1 <= 7 && temp.Item2 > 0) list.Add(temp);
            temp.Item2 -= 2;
            if (temp.Item1 >= 0 && temp.Item2 <= 8 && temp.Item1 <= 7 && temp.Item2 > 0) list.Add(temp);
            temp.Item1 += 4;
            if (temp.Item1 >= 0 && temp.Item2 <= 8 && temp.Item1 <= 7 && temp.Item2 > 0) list.Add(temp);
            temp.Item2 += 2;
            if (temp.Item1 >= 0 && temp.Item2 <= 8 && temp.Item1 <= 7 && temp.Item2 > 0) list.Add(temp);
            temp = pos;
            temp.Item2 += 2;
            temp.Item1--;
            if (temp.Item1 >= 0 && temp.Item2 <= 8 && temp.Item1 <= 7 && temp.Item2 > 0) list.Add(temp);
            temp.Item1 += 2;
            if (temp.Item1 >= 0 && temp.Item2 <= 8 && temp.Item1 <= 7 && temp.Item2 > 0) list.Add(temp);
            temp.Item2 -= 4;
            if (temp.Item1 >= 0 && temp.Item2 <= 8 && temp.Item1 <= 7 && temp.Item2 >= 0) list.Add(temp);
            temp.Item1 -= 2;
            if (temp.Item1 >= 0 && temp.Item2 <= 8 && temp.Item1 <= 7 && temp.Item2 > 0) list.Add(temp);
            return list.ToArray();
        }
        public override string ToString()
        {
            return base.ToString() + " knight";
        }
    }
}
