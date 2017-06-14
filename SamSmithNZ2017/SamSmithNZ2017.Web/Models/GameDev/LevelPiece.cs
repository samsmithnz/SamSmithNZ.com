//using UnityEngine;
using System.Collections;

namespace SamSmithNZ2017.Models.GameDev
{
    public class LevelPiece
    {
        public int PieceNumber { get; set; }
        public string PieceName { get; set; }
        public string NorthSide { get; set; }
        public string EastSide { get; set; }
        public string SouthSide { get; set; }
        public string WestSide { get; set; }

        public LevelPiece() { }

        public LevelPiece(int pieceNumber, string pieceName, string northSide, string eastSide, string southSide, string westSide)
        {
            this.PieceNumber = pieceNumber;
            this.PieceName = pieceName;
            this.NorthSide = northSide;
            this.EastSide = eastSide;
            this.SouthSide = southSide;
            this.WestSide = westSide;
        }
    }
}
