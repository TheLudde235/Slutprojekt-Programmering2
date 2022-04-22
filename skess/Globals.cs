using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace skess
{
    class Globals
    {
        public static Dictionary<(int, int), Square> BoardDict = new Dictionary<(int, int), Square>();
        public static (int, int)? EnPassantPos;
        public static string[] CastleArray = { "", "", "", "" };
        public static int HalfMoveClock = 0;
        public static (Color, Color) ColorScheme = (Color.White, Color.LightSteelBlue);
        public static King GetKing(string color)
        {
            Piece[] pieces = GetPieces(color);

            foreach (Piece p in pieces)
            {
                if (p is King)
                {
                    return p as King;
                }
            }
            return null;
        }
        public static King GetKing(string color, bool returnPieceOfSameColor)
        {
            Piece[] pieces = GetPieces(color, returnPieceOfSameColor);

            foreach (Piece p in pieces)
            {
                if (p is King)
                {
                    return p as King;
                }
            }
            return null;
        }
        public static Piece[] GetPieces()
        {
            List<Piece> list = new List<Piece>();

            foreach (Square s in BoardDict.Values)
            {
                Piece piece = s.GetPiece();

                if (piece != null)
                {
                    list.Add(piece);                    
                }
            }
            
            return list.ToArray();
        }
        public static Piece[] GetPieces(string color)
        {
            List<Piece> list = new List<Piece>();

            foreach (Square s in BoardDict.Values)
            {
                Piece piece = s.GetPiece();

                if (piece != null)
                {
                    if (piece.GetColor() == color)
                    {
                        list.Add(piece);
                    }
                }
            }

            return list.ToArray();
        }
        public static Piece[] GetPieces(string color, bool returnPiecesOfTheSameColor)
        {
            List<Piece> list = new List<Piece>();

            foreach (Square s in BoardDict.Values)
            {
                Piece piece = s.GetPiece();

                if (piece != null)
                {
                    if (piece.GetColor() == color && returnPiecesOfTheSameColor)
                    {
                        list.Add(piece);
                    }
                    else if (piece.GetColor() != color && !returnPiecesOfTheSameColor)
                    {
                        list.Add(piece);
                    }

                }
            }

            return list.ToArray();
        }

        public static bool InCheck((int, int) pos, string friendlyColor)
        {
            // gets an array with the enemies pieces
            Piece[] enemyPieces = GetPieces(friendlyColor, false);

            // loops through the array and returns true when it finds a piece that can make a legal move
            foreach (Piece p in enemyPieces)
            {
                if (p.GetCheckableMoves().Contains(pos)) return true;
            }
            return false;
        }
        public static bool InCheck((int, int) pos, string color, bool colorIsEnemy)
        {
            Piece[] enemyPieces = GetPieces(color, !colorIsEnemy);

            // loops through the array and returns true when it finds a piece that can make a legal move
            foreach (Piece p in enemyPieces)
            {
                if (p.GetCheckableMoves().Contains(pos)) return true;
            }
            return false;
        }
    }
}
