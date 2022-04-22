using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace skess
{
    public partial class PromotionDialogBox : Form
    {
        readonly string Color;
        public PromotionDialogBox(string color)
        {
            InitializeComponent();
            this.Color = color;
            RenderSquares();
        }

        private void RenderSquares()
        {
            // DialogResults meaning
            // NO = Queen
            // OK = Rook
            // YES = Knight
            // Abort = Bishop

            for (int i = 0; i < 4; i++)
            {
                (int, int) pos = (0, i);
                Square s = new Square(null, pos)
                {
                    Name = "square" + i,
                    Location = new Point(i * 100, 100),
                    Size = new Size(100, 100),
                    ImageAlign = ContentAlignment.MiddleCenter,
                    BackgroundImageLayout = ImageLayout.Zoom,
                };
                if (i % 2 == 0) s.BackColor = System.Drawing.Color.White;
                else s.BackColor = System.Drawing.Color.LightSteelBlue;

                if (i == 0)
                {
                    s.SetPiece(new Queen(this.Color, pos));
                    s.DialogResult = DialogResult.No;
                }
                else if (i == 1)
                {
                    s.SetPiece(new Rook(this.Color, pos));
                    s.DialogResult = DialogResult.OK;
                }
                else if (i == 2)
                {
                    s.SetPiece(new Knight(this.Color, pos));
                    s.DialogResult = DialogResult.Yes;
                }
                else
                {
                    s.SetPiece(new Bishop(this.Color, pos));
                    s.DialogResult = DialogResult.Abort;
                }
                Controls.Add(s);
            }
        }
    }
}
