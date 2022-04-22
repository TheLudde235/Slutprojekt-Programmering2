using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace skess
{
    abstract class Piece
    {
        private string Color { get; }
        protected Image Image;
        private (int, int) Pos;
        protected bool HasMoved;
        protected Piece(string color, (int, int) pos)
        {
            this.Color = color;
            this.Pos = pos;
        }
        public Image GetImg() => this.Image;
        public (int, int) GetPos() => this.Pos;
        public void SetPos((int, int) pos) => this.Pos = pos;
        public override string ToString() => this.Color;
        public string GetColor() => this.Color;
        public virtual void Moved() => this.HasMoved = true;
        public abstract (int, int)[] GetMoves();
        public abstract (int, int)[] GetCheckableMoves();

        protected List<(int, int)> ValidateMove(List<(int, int)> previousPositions, (int, int) temp)
        {
            string color = this.Color;
            (int, int) kingPos = Globals.GetKing(color).GetPos();

            if ((!Globals.BoardDict[temp].IsEmpty() && Globals.BoardDict[temp].GetPiece().GetColor() == color) || temp.Item1 < 0 || temp.Item1 > 7 || temp.Item2 < 1 || temp.Item2 > 8)
            {
                return previousPositions;
            }

            // Previews the move and checks if the king is in check

            if (Globals.BoardDict[temp].GetPiece() == null)
            {
                Piece clone = this.MemberwiseClone() as Piece;

                Globals.BoardDict[temp].SetPiece(clone);
                Globals.BoardDict[this.Pos].RemovePiece();

                if (this is King && !Globals.InCheck(temp, color))
                {
                    previousPositions.Add(temp);
                }
                else if (!Globals.InCheck(kingPos, color))
                {
                    previousPositions.Add(temp);
                }

                Globals.BoardDict[this.Pos].SetPiece(this);
                Globals.BoardDict[temp].RemovePiece();

            } // Temporarially removes the enemy piece and checks if king is in check
            else if (Globals.BoardDict[temp].GetPiece().GetColor() != this.GetColor())
            {
                Globals.BoardDict[this.Pos].RemovePiece();
                Piece piece = Globals.BoardDict[temp].GetPiece();
                Globals.BoardDict[temp].RemovePiece();
                if (this is King && !Globals.InCheck(temp, color))
                {
                    previousPositions.Add(temp);
                }
                else if (!Globals.InCheck(kingPos, color))
                {
                    previousPositions.Add(temp);
                }
                Globals.BoardDict[this.Pos].SetPiece(this);
                Globals.BoardDict[temp].SetPiece(piece);
            }
            else if (!Globals.InCheck(kingPos, color))
            {
                previousPositions.Add(temp);
            }
            return previousPositions;
        }
    }
}
