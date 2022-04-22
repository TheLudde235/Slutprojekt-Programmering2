using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace skess
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            CreateBoard();
            UpdateBoardDict();
            timerWhite.Interval = 1000;
            timerWhite.Start();
            timerBlack.Interval = 1000;
        }

        readonly Color light = Globals.ColorScheme.Item1;
        readonly Color dark = Globals.ColorScheme.Item2;
        readonly List<string> fenCodeList = new List<string>();



        (int, int) lastSelectedPosition;
        (int, int)[] moveList;
        int fullMoveClock = 1;
        int whiteTime = 600;
        int blackTime = 600;
        int priority = 0;
        bool gameIsOver = false;
        string notation = "";
        string colorToAct = "w";
        string clientColor = "w";
        readonly Random random = new Random();
        readonly string startingPositionFen = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1";
        


        private void Timer1_Tick(object sender, EventArgs e)
        {
            if (gameIsOver) return;
            if (whiteTime > 0)
            {
                string minutes = (whiteTime / 60).ToString();
                string seconds = (whiteTime % 60).ToString();
                if (seconds.Length == 1)
                {
                    tbxWhiteTime.Text = minutes + ":0" + seconds;
                }
                else
                {
                    tbxWhiteTime.Text = minutes + ":" + seconds;
                }
                whiteTime--;
            }
            else
            {
                timerWhite.Stop();
                tbxWhiteTime.Text = "0:00";
                MessageBox.Show("Black wins on time");
            }
        }
        private void Timer2_Tick(object sender, EventArgs e)
        {
            if (gameIsOver) return;
            if (blackTime > 0)
            {
                string minutes = (blackTime / 60).ToString();
                string seconds = (blackTime % 60).ToString();
                if (seconds.Length == 1)
                {
                    this.Invoke(new MethodInvoker(delegate () 
                    {
                        tbxBlackTime.Text = minutes + ":0" + seconds;
                    }));
                }
                else
                {
                    this.Invoke(new MethodInvoker(delegate () 
                    {
                        tbxBlackTime.Text = minutes + ":" + seconds;
                    }));
                }
                blackTime--;
            }
            else
            {
                timerBlack.Stop();
                tbxBlackTime.Text = "0:00";
                MessageBox.Show("White wins on time");
            }

        }
        private void BtnShowFen_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(BoardDictToFen() + "\r\n Do you want to copy it?", "Fencode", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes) Clipboard.SetText(BoardDictToFen());
        }
        private void BtnGiveUp_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("Are you sure you want to give up?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
            {
                gameIsOver = true;
                SendMessage('g');
                MessageBox.Show("You gave up. Opponent won");
                btnConnect.Enabled = true;
            }
        }
        private void BtnStalemate_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("Are you sure you want to ask for stalemate?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
            {
                if (socket.Connected)
                {
                    SendMessage('r', "Stalemate");
                }
                else
                {
                    timerWhite.Stop();
                    timerBlack.Stop();
                    gameIsOver = true;
                    lbxNotationList.Items.Add(fullMoveClock.ToString() + ". 1/2 - 1/2");
                    fenCodeList.Add(BoardDictToFen());
                    MessageBox.Show("Stalemate");
                    btnConnect.Enabled = true;
                }
            }

        }
        private void BtnSwitchPorts_Click(object sender, EventArgs e)
        {
            string temp = tbxLocalPort.Text;
            tbxLocalPort.Text = tbxRemotePort.Text;
            tbxRemotePort.Text = temp;
        }
        private void BtnTakeback_Click(object sender, EventArgs e)
        {
            if (socket.Connected)
            {
                DialogResult res = MessageBox.Show("Are you sure you want to ask for takeback?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res == DialogResult.Yes)
                {
                    SendMessage('r', "Takeback");
                }
            }
            else
            {
                Takeback();
            }
        }
        private void BtnFlip_Click(object sender, EventArgs e)
        {
            FlipBoard();
        }
        private void BtnRestart_Click(object sender, EventArgs e)
        {
            if (socket.Connected)
            {
                socket.Disconnect(true);
            }
            ResetGame();

            if (!cbxPlayAsWhite.Checked) CCCPMove();
        }
        private void BtnSaveGame_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Chess files (*.txt)|*.txt",
                Title = "Select where you want to save game"
            };

            string str = "";

            for (int i = 0; i < lbxNotationList.Items.Count; i++)
            {
                str += lbxNotationList.Items[i] + "*";
                if (i < fenCodeList.Count)
                {
                    str += fenCodeList[i];
                }
                str += "\r";
            }

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                if (saveFileDialog.FileName != "")
                {
                    FileStream fs = (FileStream)saveFileDialog.OpenFile();
                    AddText(fs, str);
                    fs.Dispose();
                }
            }
        }
        private void BtnOpenGame_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Chess files (*.txt)|*.txt",
                Title = "Select where you want to save game"
            };
            byte[] b = new byte[10240];

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                gameIsOver = true;
                btnConnect.Enabled = true;
                FileStream fs = File.OpenRead(openFileDialog.FileName);
                UTF8Encoding temp = new UTF8Encoding(true);
                while (fs.Read(b, 0, b.Length) > 0)
                {
                    string file = temp.GetString(b);
                    string[] lines = file.Split('\r');
                        
                    for (int i = 0; i < lines.Length - 1; i++)
                    {
                        string line = lines[i];
                        string notation = line.Split('*')[0];
                        if (line.Split('*').Length > 1)
                        {
                            string fenCode = line.Split('*')[1];
                            if (fenCode.Length > 5)
                            fenCodeList.Add(fenCode);
                        }
                        lbxNotationList.Items.Add(notation);
                    }
                }
                lbxNotationList.SelectedIndex = 0;
            }
        }
        private void BtnNotationLeftArrow_Click(object sender, EventArgs e)
        {
            if (lbxNotationList.SelectedIndex >= 1)
            {
                lbxNotationList.SelectedIndex--;
                LbxNotationList_DoubleClick(sender, e);
            }
        }
        private void BtnNotationCircle_Click(object sender, EventArgs e)
        {
            if (lbxNotationList.SelectedIndex >= 0)
            {
                LbxNotationList_DoubleClick(sender, e);
            }
            else
            {
                while (!gameIsOver)
                {
                    if (colorToAct == "w")
                    {
                        CCCPMove();
                    }
                    else RandomMove();
                }
            }
        }
        private void BtnNotationRightArrow_Click(object sender, EventArgs e)
        {
            if (lbxNotationList.SelectedIndex >= 0 && lbxNotationList.SelectedIndex < lbxNotationList.Items.Count - 1)
            {
                lbxNotationList.SelectedIndex++;
                LbxNotationList_DoubleClick(sender, e);
            }
        }
        private void CbxPlayAsWhite_CheckedChanged(object sender, EventArgs e)
        {
            cbxPlayAsWhite.Enabled = false;
            blackTime = 600;
            whiteTime = 600;
            FlipBoard();
            if (!cbxPlayAsWhite.Checked)
            {
                CCCPMove();
            }

        }
        private void LbxNotationList_DoubleClick(object sender, EventArgs e)
        {
            if (lbxNotationList.SelectedIndex > -1)
            {
                gameIsOver = true;
                if (fenCodeList.Count > lbxNotationList.SelectedIndex)
                {
                    FenToBoard(fenCodeList[lbxNotationList.SelectedIndex]);
                    if (lbxNotationList.SelectedIndex > 0)
                    {
                        ShowDifference(fenCodeList[lbxNotationList.SelectedIndex - 1], fenCodeList[lbxNotationList.SelectedIndex]);
                    }
                    else if (lbxNotationList.SelectedIndex == 0)
                    {
                        ShowDifference(fenCodeList[0], startingPositionFen);
                    }
                }
                else
                {
                    FenToBoard(fenCodeList[fenCodeList.Count - 1]);
                }
                btnConnect.Enabled = true;
            }
        }
        private void SquareClicked(object sender, MouseEventArgs e)
        {
            Square square = sender as Square;

            if (e.Button == MouseButtons.Right)
            {
                if (square.BackColor != light && square.BackColor != dark)
                {
                    square.ResetColor();
                    return;
                }
                else
                {
                    square.BackColor = Color.LightCoral;
                }
                return;
            }
            else
            {
                foreach (Square s in Globals.BoardDict.Values)
                {
                    square.ResetColor();
                }
                ResetAllSquaresColor();
            }

            if (moveList != null && !Array.Exists(moveList, x => x == square.GetPos()))
            {
                DeleteSelectedDots();
                moveList = null;
                ResetAllSquaresColor();
            }

            if (gameIsOver) return;

            if (socket.Connected && square.GetPiece() != null && square.GetPiece().GetColor() != clientColor)
            {
                if (moveList != null && Array.Exists(moveList, x => x == square.GetPos()))
                {
                    cbxPlayAsWhite.Enabled = false;
                    FindSquare(lastSelectedPosition).ResetColor();
                    MovePiece(lastSelectedPosition, square.GetPos(), false);
                    DeleteSelectedDots();
                    square.GetPiece().Moved();
                    moveList = null;
                    SendMessage('b', BoardDictToFen() + '|' + fenCodeList.Last() + '|' + lbxNotationList.Items[lbxNotationList.Items.Count - 1]);
                }
                else return;
            }

            if (!square.IsEmpty() && square.GetPiece().GetColor() == colorToAct)
            {
                FindSquare(lastSelectedPosition).ResetColor();
                square.BackColor = Color.LightGray;
                moveList = square.GetPiece().GetMoves();
                lastSelectedPosition = square.GetPos();
                PrintMoves(moveList);
            }

            if (moveList != null && Array.Exists(moveList, x => x == square.GetPos()))
            {
                cbxPlayAsWhite.Enabled = false;
                FindSquare(lastSelectedPosition).ResetColor();
                MovePiece(lastSelectedPosition, square.GetPos(), false);
                DeleteSelectedDots();
                square.GetPiece().Moved();
                moveList = null;

                if (socket.Connected)
                {
                    SendMessage('b', BoardDictToFen() + '|' + fenCodeList.Last() + '|' + lbxNotationList.Items[lbxNotationList.Items.Count - 1]);
                }
                else
                {
                    if (cbxPlayVsBot.Checked) CCCPMove();
                }
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
        }
        private void LbxNotationList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Right)
            {
                BtnNotationRightArrow_Click(sender, e);
                if (lbxNotationList.Items.Count > 1) lbxNotationList.SelectedIndex--;
            }
            else if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Left)
            {
                BtnNotationLeftArrow_Click(sender, e);
                if (lbxNotationList.Items.Count > 1) lbxNotationList.SelectedIndex++;
            }
        }
        private void ResetGame()
        {
            ResetAllSquaresColor();
            DeleteSelectedDots();
            FenToBoard(startingPositionFen);
            fenCodeList.Clear();
            fenCodeList.Add(startingPositionFen);
            lbxNotationList.Items.Clear();
            notation = "";
            tbxBlackTime.Text = "10:00";
            tbxWhiteTime.Text = "10:00";
            whiteTime = 600;
            blackTime = 600;
            moveList = new (int, int)[0];
            cbxPlayAsWhite.Enabled = true;
            btnTakeback.Enabled = false;
            gameIsOver = false;
            Globals.EnPassantPos = null;
            btnConnect.Enabled = true;
        }
        private void FlipBoard()
        {
            foreach (Control c in Controls)
            {
                if (c is Square)
                {
                    Square s = c as Square;
                    s.Location = new Point(700 - s.Location.X, 700 - s.Location.Y);
                }
                else if (c is Label)
                {
                    Label l = c as Label;
                    if (l.Name == "bottomLabel")
                    {
                        l.Location = new Point(860 - l.Location.X, l.Location.Y);
                        l.BringToFront();
                    }
                    else if (l.Name == "sideLabel")
                    {
                        l.Location = new Point(l.Location.X, 720 - l.Location.Y);
                    }

                    if (l.BackColor == light) l.BackColor = dark;
                    else if (l.BackColor == dark) l.BackColor = light;

                }
            }
        }
        private void CreateBoard()
        {
            fenCodeList.Add(startingPositionFen);
            // Creates the squares
            for (int i = 0; i < 8; i++)
            {
                for (int o = 0; o < 8; o++)
                {
                    (int, int) pos = (i, 8 - o);

                    Square s = new Square(null, pos)
                    {
                        Name = "Square_" + IntToColumnString(pos.Item1) + "_" + pos.Item2.ToString(),
                        Location = new Point(i * 100, o * 100),
                        Size = new Size(100, 100),
                        ImageAlign = ContentAlignment.MiddleCenter,
                        BackgroundImageLayout = ImageLayout.Zoom,
                        Margin = new Padding(0)
                    };
                    s.MouseDown += new MouseEventHandler(SquareClicked);

                    Font font = new Font(FontFamily.GenericSansSerif, 10);

                    if (pos.Item2 == 1)
                    {
                        Label l = new Label
                        {
                            Font = font,
                            Size = new Size(15, 15),
                            Text = IntToColumnString(pos.Item1).ToUpper(),
                            Location = new Point((i * 100) + 80, (o * 100) + 80),
                            Name = "bottomLabel"
                        };
                        switch ((i + o) % 2)
                        {
                            case 0:
                                l.BackColor = light;
                                break;
                            default:
                                l.BackColor = dark;
                                break;
                        }
                        Controls.Add(l);
                    }
                    s.ResetColor();
                    if (pos.Item1 == 0)
                    {
                        Label l = new Label
                        {
                            Font = font,
                            Size = new Size(15, 15),
                            Text = pos.Item2.ToString(),
                            Location = new Point((i * 100) + 5, (o * 100) + 10),
                            Name = "sideLabel"
                        };
                        switch ((i + o) % 2)
                        {
                            case 0:
                                l.BackColor = light;
                                break;
                            default:
                                l.BackColor = dark;
                                break;
                        }
                        Controls.Add(l);
                    }
                    Controls.Add(s);
                    Globals.BoardDict.Add(pos, s);
                }
            }
            FenToBoard(startingPositionFen);
        }
        private void DeleteSelectedDots()
        {
            foreach (Control c in Controls)
            {
                if (c is Square)
                {
                    Square square = c as Square;
                    square.Image = null;
                }
            }
        }
        private void Takeback()
        {
            if (timerWhite.Enabled)
            {
                if (fenCodeList.Count >= 2) FenToBoard(fenCodeList[fenCodeList.Count - 2]);
                else FenToBoard(startingPositionFen);
                lbxNotationList.Items.RemoveAt(lbxNotationList.Items.Count - 1);
                fenCodeList.RemoveAt(fenCodeList.Count - 1);
                btnTakeback.Enabled = false;
                timerWhite.Stop();
                timerBlack.Start();
            }
            else
            {
                if (fenCodeList.Count >= 2) FenToBoard(fenCodeList[fenCodeList.Count - 2]);
                else FenToBoard(startingPositionFen);
                lbxNotationList.Items.RemoveAt(lbxNotationList.Items.Count - 1);
                fenCodeList.RemoveAt(fenCodeList.Count - 1);
                timerBlack.Stop();
                timerWhite.Start();
            }
        }
        private Square FindSquare((int, int) pos)
        {
            if (pos.Item2 >= 1 && pos.Item2 <= 8)
            {
                string squareName = "Square_" + IntToColumnString(pos.Item1) + "_" + pos.Item2;
                Control c = Controls.Find(squareName, false)[0];
                Square s = c as Square;
                return s;
            }
            else
            {
                return new Square(null, (0, 0));
            }
        }
        private void PrintMoves((int, int)[] moves)
        {
            for (int i = 0; i < moves.Length; i++)
            {
                (int, int) pos = moves[i];
                if (!FindSquare(pos).IsEmpty())
                {
                    if (colorToAct != FindSquare(pos).GetPiece().GetColor())
                    {
                        FindSquare(pos).Image = Properties.Resources.selected_dot;
                    }
                }
                else
                {
                    FindSquare(pos).Image = Properties.Resources.selected_dot;
                }
            }
        }
        private bool EnoughForCheckmate()
        {
            Square[] squares = Globals.BoardDict.Values.ToArray();

            foreach (Square s in squares)
            {
                Piece piece = s.GetPiece();
                if (piece is Pawn) return true;
                if (piece is Queen) return true;
                if (piece is Rook) return true;
            }

            Piece[] whitePieces = Globals.GetPieces("w");
            Piece[] blackPieces = Globals.GetPieces("b");
            int tempIndex;
            if (whitePieces.Length < 3 && blackPieces.Length < 3) return false;
            else
            {
                for (int i = 0; i < whitePieces.Length; i++)
                {
                    if (whitePieces[i] is Bishop)
                    {
                        tempIndex = i;
                        for (int n = whitePieces.Length - 1; n >= 0; n--)
                        {
                            if (whitePieces[n] is Bishop && tempIndex == n) return true;
                        }
                    }
                }
            }
            tempIndex = -1;
            if (blackPieces.Length > 3) return true;
            else
            {
                for (int i = 0; i < blackPieces.Length; i++)
                {
                    if (blackPieces[i] is Bishop)
                    {
                        tempIndex = i;
                        for (int n = blackPieces.Length - 1; n >= 0; n--)
                        {
                            if (blackPieces[n] is Bishop && tempIndex == n) return true;
                        }
                    }
                }
            }



            return false;
        }
        private void MovePiece((int, int) from, (int, int) to, bool autoQueen)
        {
            Square square = FindSquare(from);
            Piece piece = square.GetPiece();
            string fullMoveNotation = GetFullMoveNotation();

            if (Globals.HalfMoveClock >= 50)
            {
                MessageBox.Show("Stalemate");
                gameIsOver = true;
                fenCodeList.Add(BoardDictToFen());
                lbxNotationList.Items.Add("1/2 - 1/2");
                btnConnect.Enabled = true;
                return;
            }

            if (!(piece is Pawn))
            {
                notation += PieceToFenPiece(piece).ToUpper();
            }

            // if piece is captured
            if (!FindSquare(to).IsEmpty())
            {
                Globals.HalfMoveClock = 0;

                if (piece is Pawn)
                {
                    notation += IntToColumnString(from.Item1);
                }

                notation += "x";
            }

            notation += IntToColumnString(to.Item1) + to.Item2.ToString();

            square.RemovePiece();
            FindSquare(to).SetPiece(piece);

            //en Passant
            if (piece is Pawn && to == Globals.EnPassantPos)
            {
                if (piece.GetColor() == "w")
                {
                    square.RemovePiece();
                    square = FindSquare((to.Item1, to.Item2 - 1));
                    square.RemovePiece();
                    FindSquare(to).SetPiece(piece);
                    notation = notation.Insert(0, IntToColumnString(from.Item1) + "x"); 
                }
                else
                {
                    square.RemovePiece();
                    square = FindSquare((to.Item1, to.Item2 + 1));
                    square.RemovePiece();
                    FindSquare(to).SetPiece(piece);
                    notation = notation.Insert(notation.Length - 2, IntToColumnString(from.Item1) + "x");
                }
                notation += " ep.";
            }

            // promotion
            if (piece is Pawn && (to.Item2 == 8 || to.Item2 == 1))
            {
                // DialogResults meaning
                // NO = Queen
                // OK = Rook
                // YES = Knight
                // Abort = Bishop

                string s = "";
                
                if (autoQueen)
                {
                    Queen temp = new Queen(piece.GetColor(), piece.GetPos());
                    square.RemovePiece();
                    FindSquare(to).SetPiece(temp);
                    piece = temp;
                    s = "Q";
                }
                else
                {
                    PromotionDialogBox dialogBox = new PromotionDialogBox(piece.GetColor());
                    DialogResult res;
                    do
                    {
                        res = dialogBox.ShowDialog();
                        if (res == DialogResult.No)
                        {
                            Queen temp = new Queen(piece.GetColor(), piece.GetPos());
                            square.RemovePiece();
                            FindSquare(to).SetPiece(temp);
                            piece = temp;
                            s = "Q";
                        }
                        else if (res == DialogResult.OK)
                        {
                            Rook temp = new Rook(piece.GetColor(), piece.GetPos());
                            square.RemovePiece();
                            FindSquare(to).SetPiece(temp);
                            piece = temp;
                            s = "R";
                        }
                        else if (res == DialogResult.Yes)
                        {
                            Knight temp = new Knight(piece.GetColor(), piece.GetPos());
                            square.RemovePiece();
                            FindSquare(to).SetPiece(temp);
                            piece = temp;
                            s = "N";
                        }
                        else if (res == DialogResult.Abort)
                        {
                            Bishop temp = new Bishop(piece.GetColor(), piece.GetPos());
                            square.RemovePiece();
                            FindSquare(to).SetPiece(temp);
                            piece = temp;
                            s = "B";
                        }
                    }
                    while (res == DialogResult.Cancel);
                }
                

                Globals.HalfMoveClock = 0;

                notation += "=" + s;

            }

            // sets Globals.enPassantPos if a pawn takes two steps
            if (piece is Pawn && Math.Abs(from.Item2 - to.Item2) == 2)
            {
                if (piece.GetColor() == "w")
                {
                    Globals.EnPassantPos = (to.Item1, to.Item2 - 1);
                }
                else
                {
                    Globals.EnPassantPos = (to.Item1, to.Item2 + 1);
                }
            }
            else
            {
                Globals.EnPassantPos = null;
            }

            // Code for casteling
            if (piece is King && Math.Abs(from.Item1 - to.Item1) == 2)
            {
                if (to.Item1 < 4)
                {
                    MovePiece((0, to.Item2), (3, to.Item2), false);
                    notation = "O-O-O";
                }
                else 
                {
                    MovePiece((7, to.Item2), (5, to.Item2), false);
                    notation = "O-O";
                }
            }

            DeleteSelectedDots();

            string enemyColor;

            if (colorToAct == "w") enemyColor = "b";
            else enemyColor = "w";




            Square enemyKingsSquare = FindSquare(Globals.GetKing(enemyColor).GetPos());

            if (Globals.InCheck(enemyKingsSquare.GetPos(), enemyColor))
            {
                if (!CanMove(Globals.GetPieces(enemyKingsSquare.GetPiece().GetColor())))
                {
                    PlaySimpleSound(@"C:\Windows\Media\tada.wav");
                    timerWhite.Stop();
                    timerBlack.Stop();
                    gameIsOver = true;
                    notation += "#";
                    if (colorToAct == "b")
                    {
                        lbxNotationList.Items[lbxNotationList.Items.Count - 1] = lbxNotationList.Items[lbxNotationList.Items.Count - 1] + notation;
                    }
                    else
                    {
                        lbxNotationList.Items.Add(fullMoveNotation + notation);
                    }
                    fenCodeList.Add(BoardDictToFen());
                    if (colorToAct == "w") notation = "1 - 0";
                    else notation = "0 - 1";
                    lbxNotationList.Items.Add(notation);
                    string winner;
                    if (colorToAct == "w") winner = "White";
                    else winner = "Black";
                    MessageBox.Show("Check Mate " + winner + " Wins");
                    btnConnect.Enabled = true;
                }
                else
                {
                    PlaySimpleSound(@"C:\Windows\Media\notify.wav");
                    notation += "+";
                }
            }
            else if (!CanMove(Globals.GetPieces(enemyKingsSquare.GetPiece().GetColor())) || Globals.HalfMoveClock >= 50)
            {    
                timerWhite.Stop();
                timerBlack.Stop();
                gameIsOver = true;
                lbxNotationList.Items.Add(fullMoveNotation + notation);
                notation = "1/2 - 1/2";
                lbxNotationList.Items.Add(notation);
                MessageBox.Show("Stalemate");
                btnConnect.Enabled = true;
            }
            piece.Moved();
            piece.SetPos(to);

            if (gameIsOver) return;

            if (piece.GetColor() == "w")
            {
                timerWhite.Stop();
                colorToAct = "b";
                timerBlack.Start();
                string temp = " ";
                for (int i = 0; i < 8 - notation.Length; i++) temp += " ";
                notation += temp;
                lbxNotationList.Items.Add(fullMoveNotation + notation);
                notation = "";
            }
            else
            {
                timerBlack.Stop();
                colorToAct = "w";
                timerWhite.Start();
                lbxNotationList.Items[lbxNotationList.Items.Count - 1] = lbxNotationList.Items[lbxNotationList.Items.Count - 1] + notation;
                fenCodeList.Add(BoardDictToFen());
                notation = "";
            }

            if (piece.GetColor() == "b")
            {
                fullMoveClock++;
                Globals.HalfMoveClock++;
            }
            UpdateBoardDict();

            if (fenCodeList.Count >= 5)
            {
                int index = fenCodeList.Count - 1;
                if (fenCodeList[index].Split(' ')[0] == fenCodeList[index - 2].Split(' ')[0] && fenCodeList[index].Split(' ')[0] == fenCodeList[index - 4].Split(' ')[0])
                {
                    fenCodeList.Add(BoardDictToFen());
                    lbxNotationList.Items.Add("1/2 - 1/2");
                    MessageBox.Show("Draw: Repetition");
                    gameIsOver = true;
                    btnConnect.Enabled = true;
                } 
            }

            if (!EnoughForCheckmate())
            {
                fenCodeList.Add(BoardDictToFen());
                lbxNotationList.Items.Add("1/2 - 1/2");
                MessageBox.Show("Stalemate");
                gameIsOver = true;
                btnConnect.Enabled = true;
            }

            btnTakeback.Enabled = true;
        }
        private string GetFullMoveNotation()
        {
            string fullMoveNotation = "";
            if (fullMoveClock < 10)
            {
                fullMoveNotation += fullMoveClock + ".    ";
            }
            else if (fullMoveClock < 100)
            {
                fullMoveNotation += fullMoveClock + ".   ";
            }
            else if (fullMoveClock < 1000)
            {
                fullMoveNotation += fullMoveClock + ".  ";
            }
            else
            {
                fullMoveNotation += fullMoveClock + ". ";
            }

            return fullMoveNotation;
        }
        private void UpdateBoardDict()
        {
            foreach(Control c in Controls)
            {
                if (c is Square)
                {
                    Square s = c as Square;
                    Globals.BoardDict[s.GetPos()] = s;
                }
            }
        }
        void LoadBoardDict()
        {
            //1.4ms
            foreach (KeyValuePair<(int, int), Square> kvp in Globals.BoardDict)
            {
                FindSquare(kvp.Key).SetPiece(kvp.Value.GetPiece());
            }
        }
        private bool CanMove(Piece[] pieces)
        {
            if (gameIsOver) return false;
            foreach (Piece p in pieces)
            {
                if (p.GetMoves().Length > 0)
                {
                    return true;
                }
            }
            return false;
        }
        private Dictionary<(int, int), Square> FenToDictionary(string fenBoard)
        {
            Dictionary<(int, int), Square> Dictionary = Globals.BoardDict;


            (int, int) writingPos; 
            string[] rows = fenBoard.Split('/');
            bool isNumeric;

            for (int y = 0; y < rows.Length; y++)
            {
                writingPos = (0, 8 - y);
                foreach (char piece in rows[y])
                {
                    isNumeric = int.TryParse(piece.ToString(), out int n);
                    if (isNumeric)
                    {
                        for (int i = 0; i < n; i++)
                        {
                            Dictionary[writingPos].RemovePiece();
                            writingPos.Item1++;
                        }
                    }
                    else
                    {
                        Dictionary[writingPos].RemovePiece();
                        Dictionary[writingPos].SetPiece(FenToPiece(piece, writingPos));
                        writingPos.Item1++;
                    }

                }
            }

            return Dictionary;
        }
        private void FenToBoard(string fenString)
        {   
            string[] fenArray = fenString.Split(' ');

            Globals.BoardDict = FenToDictionary(fenArray[0]);

            colorToAct = fenArray[1];
            string possibleCastle = fenArray[2];
            string enPassant = fenArray[3];
            Globals.HalfMoveClock = int.Parse(fenArray[4]);
            fullMoveClock = int.Parse(fenArray[5]);

            if (possibleCastle != "-")
            {
                if (possibleCastle.Contains("K")) Globals.CastleArray[0] = "K";
                if (possibleCastle.Contains("Q")) Globals.CastleArray[1] = "Q";
                if (possibleCastle.Contains("k")) Globals.CastleArray[2] = "k";
                if (possibleCastle.Contains("q")) Globals.CastleArray[3] = "q";
            }

            if (enPassant != "-")
            {
                Globals.EnPassantPos = (RowStringToInt(enPassant[0].ToString()), int.Parse(enPassant[1].ToString()));
            }

            LoadBoardDict();
        }
        private Piece FenToPiece(char piece, (int, int) pos)
        {
            switch (piece)
            {
                case 'p':
                    return new Pawn("b", pos);
                case 'r':
                    return new Rook("b", pos);
                case 'n':
                    return new Knight("b", pos);
                case 'b':
                    return new Bishop("b", pos);
                case 'q':
                    return new Queen("b", pos);
                case 'k':
                    return new King("b", pos);
                case 'P':
                    return new Pawn("w", pos);
                case 'R':
                    return new Rook("w", pos);
                case 'N':
                    return new Knight("w", pos);
                case 'B':
                    return new Bishop("w", pos);
                case 'Q':
                    return new Queen("w", pos);
                case 'K':
                    return new King("w", pos);
                default:
                    return null;
            }
        }
        private int RowStringToInt(string s)
        {
            switch (s)
            {
                case "a":
                    return 0;
                case "b":
                    return 1;
                case "c":
                    return 2;
                case "d":
                    return 3;
                case "e":
                    return 4;
                case "f":
                    return 5;
                case "g":
                    return 6;
                case "h":
                    return 7;
                default:
                    return 6106415;
            }
        }
        private string IntToColumnString(int i)
        {
            switch (i)
            {
                case 0:
                    return "a";
                case 1:
                    return "b";
                case 2:
                    return "c";
                case 3:
                    return "d";
                case 4:
                    return "e";
                case 5:
                    return "f";
                case 6:
                    return "g";
                case 7:
                    return "h";
                default:
                    return "ö";
            }
        }
        private void ResetAllSquaresColor()
        {
            foreach (Control c in Controls)
            {
                if (c is Square)
                {
                    Square s = c as Square;
                    s.ResetColor();
                }
            }
        }
        private bool InMate(King king)
        {
            (int, int) pos = king.GetPos();
            if (Globals.InCheck(pos, king.GetColor()))
            {
                if (!CanMove(Globals.GetPieces(king.GetColor())))
                {
                    return true;
                }
            }
            return false;
        }
        private void CCCPMove()
        {
            //https://youtu.be/DpXy041BIlA?t=665

            if (gameIsOver) return;
            Piece[] pieces = Globals.GetPieces(colorToAct);
            King enemyKing = Globals.GetKing(colorToAct, false);
            
            (int, int) enemyKingPos = enemyKing.GetPos();
            KeyValuePair<(int, int), (int, int)> checkMateMove = new KeyValuePair<(int, int), (int, int)>();
            KeyValuePair<(int, int), (int, int)> checkMove = new KeyValuePair<(int, int), (int, int)>();
            KeyValuePair<(int, int), (int, int)> captureMove = new KeyValuePair<(int, int), (int, int)>();

            List< KeyValuePair < (int, int), (int, int) > > completeMoves = new List<KeyValuePair<(int, int), (int, int)>>();
            foreach (Piece piece in pieces)
            {
                (int, int)[] moves = piece.GetMoves();

                foreach ((int, int) move in moves)
                {
                    completeMoves.Add(new KeyValuePair<(int, int), (int, int)>(piece.GetPos(), move));
                }
            }
            foreach (KeyValuePair<(int, int), (int, int)> kvp in completeMoves)
            {
                Piece piece = Globals.BoardDict[kvp.Key].GetPiece();

                if (Globals.BoardDict[kvp.Value].GetPiece() == null)
                {
                    Globals.BoardDict[kvp.Key].RemovePiece();
                    Globals.BoardDict[kvp.Value].SetPiece(piece);

                    if (InMate(enemyKing)) checkMateMove = kvp;
                    if (Globals.InCheck(enemyKingPos, colorToAct, false)) checkMove = kvp;

                    Globals.BoardDict[kvp.Key].SetPiece(piece);
                    Globals.BoardDict[kvp.Value].RemovePiece();

                } // Temporarially removes the enemy piece and checks if king is in check
                else if (Globals.BoardDict[kvp.Value].GetPiece().GetColor() != piece.GetColor())
                {
                    Globals.BoardDict[kvp.Key].RemovePiece();
                    Piece enemyPiece = Globals.BoardDict[kvp.Value].GetPiece();
                    Globals.BoardDict[kvp.Value].RemovePiece();
                    Globals.BoardDict[kvp.Value].SetPiece(piece);

                    if (InMate(enemyKing)) checkMateMove = kvp;
                    if (Globals.InCheck(enemyKingPos, colorToAct, false)) checkMove = kvp;
                    else captureMove = kvp;

                    Globals.BoardDict[kvp.Key].SetPiece(piece);
                    Globals.BoardDict[kvp.Value].RemovePiece();
                    Globals.BoardDict[kvp.Value].SetPiece(enemyPiece);
                }
            }

            if (checkMateMove.Key != (0, 0) && checkMateMove.Value != (0, 0)) MovePiece(checkMateMove.Key, checkMateMove.Value, true); // Checkmate
            else if (checkMove.Key != (0, 0) && checkMove.Value != (0, 0)) MovePiece(checkMove.Key, checkMove.Value, true); // Check
            else if (captureMove.Key != (0, 0) && captureMove.Value != (0, 0)) MovePiece(captureMove.Key, captureMove.Value, true); // Capture
            else PushRandomPawn(); // Push
        }
        private void PushRandomPawn()
        {
            Piece[] pieces = Globals.GetPieces(colorToAct);
            List<Piece> ps = new List<Piece>();
            Random random = new Random();

            foreach (Piece p in pieces)
            {
                if (p is Pawn) ps.Add(p);
            }

            Piece[] pawns = ps.ToArray();

            while (CanMove(pawns))
            {
                int index = random.Next(0, pawns.Length);
                (int, int)[] possibleMoves = pawns[index].GetMoves();

                if (possibleMoves.Length > 0)
                {
                    (int, int) move = possibleMoves[random.Next(0, possibleMoves.Length)];
                    MovePiece(pawns[index].GetPos(), move, true);
                    return;
                }
            }
            RandomMove();

        }
        private void RandomMove()
        {
            Piece[] pieces = Globals.GetPieces(colorToAct);
            Random random = new Random();

            while (CanMove(pieces))
            {
                int index = random.Next(0, pieces.Length);
                (int, int)[] possibleMoves = pieces[index].GetMoves();
                if (possibleMoves.Length > 0)
                {
                    (int, int) move = possibleMoves[random.Next(0, possibleMoves.Length)];
                    MovePiece(pieces[index].GetPos(), move, true);
                    return;
                }
            }
        }
        private void ShowDifference(string fen1, string fen2)
        {
            ResetAllSquaresColor();

            string board1 = fen1.Split(' ')[0];
            string board2 = fen2.Split(' ')[0];

            (int, int) writingPos;
            string[] rows1 = board1.Split('/');
            string[] rows2 = board2.Split('/');

            string extendedFen1 = "";
            string extendedFen2 = "";

            bool isNumeric;

            for (int y = 0; y < rows1.Length; y++)
            {
                writingPos = (0, 8 - y);
                foreach (char piece in rows1[y])
                {
                    isNumeric = int.TryParse(piece.ToString(), out int n);
                    if (isNumeric)
                    {
                        for (int i = 0; i < n; i++)
                        {
                            extendedFen1 += "e";
                            writingPos.Item1++;
                        }
                    }
                    else
                    {
                        extendedFen1 += piece;
                        writingPos.Item1++;
                    }

                }
            }

            for (int y = 0; y < rows2.Length; y++)
            {
                writingPos = (0, 8 - y);
                foreach (char piece in rows2[y])
                {
                    isNumeric = int.TryParse(piece.ToString(), out int n);
                    if (isNumeric)
                    {
                        for (int i = 0; i < n; i++)
                        {
                            extendedFen2 += "e";
                            writingPos.Item1++;
                        }
                    }
                    else
                    {
                        extendedFen2 += piece;
                        writingPos.Item1++;
                    }

                }
            }

            for (int i = 0; i < 64; i++)
            {
                if (extendedFen1[i] != extendedFen2[i])
                {
                    (int, int) pos;

                    if (i < 8)
                    {
                        pos = (i, 8);
                    }
                    else if (i < 16)
                    {
                        pos = (i - 8, 7);
                    }
                    else if (i < 24)
                    {
                        pos = (i - 16, 6);
                    }
                    else if (i < 32)
                    {
                        pos = (i - 24, 5);
                    }
                    else if (i < 40)
                    {
                        pos = (i - 32, 4);
                    }
                    else if (i < 48)
                    {
                        pos = (i - 40, 3);
                    }
                    else if (i < 56)
                    {
                        pos = (i - 48, 2);
                    }
                    else
                    {
                        pos = (i - 56, 1);
                    }

                    Square square = FindSquare(pos);

                    square.BackColor = Color.FromArgb(square.BackColor.A - 100, square.BackColor.B, square.BackColor.B, square.BackColor.B);
                }
            }

        }
        private void PlaySimpleSound(string audioFile)
        {
            SoundPlayer simpleSound = new SoundPlayer(audioFile);
            simpleSound.Play();
        }
        private string PieceToFenPiece(Piece piece)
        {
            string s = "";
            switch (piece)
            {
                case Pawn _:
                    s = "p";
                    break;
                case Rook _:
                    s = "r";
                    break;
                case King _:
                    s = "k";
                    break;
                case Queen _:
                    s = "q";
                    break;
                case Bishop _:
                    s = "b";
                    break;
                case Knight _:
                    s = "n";
                    break;
                default:
                    break;
            }
            if (piece.GetColor() == "w")
            {
                 s = s.ToUpper();
            }
            return s;
        }
        private string BoardDictToFen()
        {
            string fenCode = "";
            int spaces = 0;

            for (int y = 8; y > 0; y--)
            {
                for (int x = 0; x < 8; x++)
                {
                    Square s = Globals.BoardDict[(x, y)];
                    if (!s.IsEmpty())
                    {
                        if (spaces > 0)
                        {
                            fenCode += spaces.ToString();
                        }
                        fenCode += PieceToFenPiece(s.GetPiece());
                        spaces = 0;
                    }
                    else
                    {
                        spaces++;
                    }
                }
                if (spaces > 0)
                {
                    fenCode += spaces.ToString();
                }
                spaces = 0;
                if (y > 1)
                {
                    fenCode += "/";
                }
            }

            fenCode += " " + colorToAct;

            string[] castle = Globals.CastleArray;
            string tempString = "";
            foreach (string ch in castle)
            {
                tempString += ch.ToString();
            }
            
            if (tempString == "")
            {
                fenCode += " -";
            }
            else
            {
                fenCode += " " + tempString;
            }

            (int, int)? enPassant = Globals.EnPassantPos;

            if (Globals.EnPassantPos != null)
            {
                (int, int) pos = ((int, int))enPassant;
                fenCode += " " + IntToColumnString(pos.Item1) + pos.Item2.ToString();
            }
            else
            {
                fenCode += " -";
            }

            fenCode += " " + Globals.HalfMoveClock.ToString();
            fenCode += " " + fullMoveClock.ToString();


            return fenCode;
        }
        private void AddText(FileStream fs, string value)
        {
            byte[] info = new UTF8Encoding(true).GetBytes(value);
            fs.Write(info, 0, info.Length);
        }

        //networking
        Socket socket;
        EndPoint epLocal, epRemote;
        byte[] buffer;
        private void BtnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                priority = random.Next(100_000_000);

                ResetGame();

                //bind local socket
                epLocal = new IPEndPoint(IPAddress.Parse(tbxLocalIP.Text), Convert.ToInt32(tbxLocalPort.Text));
                socket.Bind(epLocal);
                // connect to remote ip
                epRemote = new IPEndPoint(IPAddress.Parse(tbxRemoteIP.Text), Convert.ToInt32(tbxRemotePort.Text));
                socket.Connect(epRemote);
                //listening to specific port

                buffer = new byte[1250];
                socket.BeginReceiveFrom(buffer, 0, buffer.Length, SocketFlags.None, ref epRemote, new AsyncCallback(MessageCallback), buffer);
                SendMessage('p', priority.ToString());
                btnConnect.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void SendMessage(char header, string message)
        {
            UTF32Encoding encoding = new UTF32Encoding();
            byte[] sendingMessage = encoding.GetBytes(header.ToString() + message);
            try
            {
                socket.Send(sendingMessage);
            }
            catch { }
        }
        private void SendMessage(char header)
        {
            UTF32Encoding encoding = new UTF32Encoding();
            byte[] sendingMessage = encoding.GetBytes(header.ToString());
            try
            {
                socket.Send(sendingMessage);
            }
            catch { }
        }
        private void BtnSend_Click(object sender, EventArgs e)
        {
            if (socket.Connected)
            {
                SendMessage('m', tbxMessage.Text);
                lbxMessages.Items.Add("Me: " + tbxMessage.Text);
                tbxMessage.Text = "";
            }
            else
            {
                tbxMessage.Text = "";
                btnSend.Enabled = false;
                tbxMessage.Enabled = false;
                MessageBox.Show("Error: You are not connected");
            }
        }
        private void MessageCallback(IAsyncResult result)
        {
            //nf
            //Notation skickas inte med
            try
            {
                DeleteSelectedDots();
                // empties moveList
                moveList = new (int, int)[0];
                byte[] recieveData = (byte[])result.AsyncState;

                //converting byte[] to string 
                UTF32Encoding encoding = new UTF32Encoding();
                string recievedMessage = encoding.GetString(recieveData).ToString();
                string content = recievedMessage.Substring(1, recievedMessage.Length - 2).TrimEnd('\0');

                if (content == null)
                {
                    buffer = new byte[1250];
                    socket.BeginReceiveFrom(buffer, 0, buffer.Length, SocketFlags.None, ref epRemote, new AsyncCallback(MessageCallback), buffer);
                    return;
                }

                switch (recievedMessage[0])
                {
                    case 'd':
                        MessageBox.Show("Your opponent declined");
                        break;
                    case 'b': // "b"oard
                        FenToBoard(content.Split('|')[0]);
                        fenCodeList.Add(content.Split('|')[1]);
                        this.Invoke(new MethodInvoker(delegate ()
                        {
                            if (lbxNotationList.Items.Count > 0 && colorToAct == "w")
                            {
                                lbxNotationList.Items[lbxNotationList.Items.Count - 1] = content.Split('|')[2];
                            }
                            else
                            {
                                lbxNotationList.Items.Add(content.Split('|')[2]);
                            }
                        }));
                        if (timerWhite.Enabled)
                        {
                            timerWhite.Stop();
                            timerBlack.Start();
                        }
                        else
                        {
                            timerBlack.Stop();
                            timerWhite.Start();
                        }
                        break;
                    case 'm': // "m"essage
                        lbxMessages.Items.Add("Opponent: " + content);
                        break;
                    case 'g': // "g"ave up
                        MessageBox.Show("Opponent gave up, you win!");
                        gameIsOver = true;
                        btnConnect.Enabled = true;
                        break;
                    case 'r': // "r"equest content
                        string message = "Your opponent asked for a " + content.ToLower() + ". Do you accept?";
                        message.Trim('\0');
                        DialogResult res = MessageBox.Show(message, content, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (res == DialogResult.Yes)
                        {
                            SendMessage(content.ToLower()[0]);
                            char ch = content.ToLower()[0];
                            switch (ch)
                            {
                                case 's': // "s"talemate
                                    MessageBox.Show("Draw");
                                    gameIsOver = true;
                                    btnConnect.Enabled = true;
                                    break;
                                case 't': // "t"akeback
                                    Takeback();
                                    if (timerWhite.Enabled)
                                    {
                                        timerWhite.Stop();
                                        timerBlack.Start();
                                    }
                                    else
                                    {
                                        timerBlack.Stop();
                                        timerWhite.Start();
                                    }
                                    break;
                                default:
                                    if (recievedMessage[0] != '\0')
                                    {
                                        MessageBox.Show("Invalid Request prefix: " + recievedMessage[0].ToString());
                                    }
                                    break;
                            }
                        }
                        else
                        {
                            SendMessage('d');
                        }
                        break;
                    case 's': // "s"talemate
                        MessageBox.Show("Draw");
                        gameIsOver = true;
                        btnConnect.Enabled = true;
                        break;
                    case 't': // "t"akeback
                        Takeback();
                        if (timerWhite.Enabled)
                        {
                            timerWhite.Stop();
                            timerBlack.Start();
                        }
                        else
                        {
                            timerBlack.Stop();
                            timerWhite.Start();
                        }
                        break;
                    case 'p': //"p"riority
                        if (int.Parse(content) == priority)
                        {
                            priority = random.Next(100_000_000);
                            SendMessage('p', priority.ToString());
                        }
                        else if (int.Parse(content) > priority)
                        {
                            clientColor = "b";
                            this.Invoke(new MethodInvoker(delegate () 
                            {
                                 FlipBoard();
                            }));
                        }
                        else
                        {
                            clientColor = "w";
                        }
                        break;
                    default:
                        if (recievedMessage[0] != '\0')
                        {
                            MessageBox.Show("Invalid prefix: " + recievedMessage[0].ToString());
                        }
                        break;
                }
                buffer = new byte[1250];
                socket.BeginReceiveFrom(buffer, 0, buffer.Length, SocketFlags.None, ref epRemote, new AsyncCallback(MessageCallback), buffer);
            }
            catch 
            {
                if (!gameIsOver)
                {
                    MessageBox.Show("Your opponent exited the match, you win!");
                }
            }
        }
    }
}