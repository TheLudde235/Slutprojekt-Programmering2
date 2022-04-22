using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace skess
{
    class Square : System.Windows.Forms.Button
    {
        private Piece Piece { get; set; }
        private (int, int) Pos { get; }

        public Square(Piece piece, (int, int) pos)
        {
            this.Piece = piece;
            this.Pos = pos;
            base.FlatStyle = FlatStyle.Flat;
            this.FlatAppearance.BorderSize = 0;
        }
        public Piece GetPiece() => this.Piece;
        public (int, int) GetPos() => this.Pos;
        public bool IsEmpty()
        {
            if (this.Piece == null)
            {
                return true;
            }
            return false;
        }
        public void SetPiece(Piece piece)
        {
            if (piece != null)
            {
                piece.SetPos(this.Pos);
                this.Piece = piece;
                this.BackgroundImage = piece.GetImg();
            }
        }
        public void RemovePiece()
        {
            this.Piece = null;
            this.BackgroundImage = null;
        }
        public void ResetColor()
        {
            switch ((this.GetPos().Item1 + this.GetPos().Item2) % 2)
            {
                case 0:
                    this.BackColor = Globals.ColorScheme.Item1;
                    break;
                default:
                    this.BackColor = Globals.ColorScheme.Item2;
                    break;
            }
        }
        public object Clone()
        { 
            return this.MemberwiseClone();
        }
        public override string ToString()
        {
            return "Pos: " + this.Pos + ", Piece: " + this.Piece.ToString();
        }
    }
}
