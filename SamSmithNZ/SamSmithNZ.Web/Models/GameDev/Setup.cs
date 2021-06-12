using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamSmithNZ.Web.Models.GameDev
{
    public class Setup
    {
        /// <summary>
        /// Create random levels to display
        /// </summary>
        /// <param name="levelPiecePool">Pool of tile pieces to build the level with</param>
        /// <param name="levelWidth">level width</param>
        /// <param name="levelHeight">level hieght</param>
        /// <returns></returns>
        public static List<LevelPiece> CreateLevelFromPiecePool(List<LevelPiece> levelPiecePool, int levelWidth, int levelHeight, bool allowRivers, bool isCampaign)
        {
            //1. randomly decide if there is going to be a river (1/3, no, yes: north, yes: west)
            int isThereARoad = Utility.GenerateRandomNumber(2, 3);
            int rowEntryPoint = 0;
            int colEntryPoint = 0;
            string originDirection = "";
            List<Location> roadPath = new();

            List<LevelPiece> levelPiecePoolWithoutRivers = new();
            foreach (LevelPiece item in levelPiecePool)
            {
                if (item.NorthSide == "" && item.SouthSide == "" && item.EastSide == "" && item.WestSide == "")
                {
                    levelPiecePoolWithoutRivers.Add(item);
                }
            }

            //Only add the river/road paths if the level is at least 3x3
            if (allowRivers == true && levelWidth > 3 && levelHeight > 3)
            {
                //2. If there is a river, randomly choose an entry point random(2, width - 1) // (don't start on a corner))
                if (isThereARoad == 2) //starting horizontally
                {
                    originDirection = "W";
                    colEntryPoint = 1;
                    rowEntryPoint = Utility.GenerateRandomNumber(2, levelHeight - 1);
                    roadPath.Add(new Location(colEntryPoint, rowEntryPoint, "W", GetRandomDirection("W", originDirection)));
                }
                else if (isThereARoad == 3)//starting vertically
                {
                    originDirection = "N";
                    colEntryPoint = Utility.GenerateRandomNumber(2, levelWidth - 1);
                    rowEntryPoint = 1;
                    roadPath.Add(new Location(colEntryPoint, rowEntryPoint, "N", GetRandomDirection("N", originDirection)));
                }

                //roadPath.Add(new Location(1, 2, "W", "E"));
                //roadPath.Add(new Location(2, 2, "W", "E"));
                //roadPath.Add(new Location(3, 2, "W", "E"));

                int safeCounter = 0;
                do
                {
                    safeCounter++;
                    int newRowEntryPoint = rowEntryPoint;
                    int newColEntryPoint = colEntryPoint;
                    //3. randomly choose a direction for the next river piece, until we reach the end. 
                    switch (roadPath[^1].Exit)
                    {
                        case "N":
                            newRowEntryPoint--;
                            break;
                        case "S":
                            newRowEntryPoint++;
                            break;
                        case "W":
                            newColEntryPoint--;
                            break;
                        case "E":
                            newColEntryPoint++;
                            break;
                    }
                    string newEntry = GetOppositeDirection(roadPath[^1].Exit);
                    string newExit = GetRandomDirection(newEntry, originDirection);
                    bool blnFoundExistingRoundAtThisLocation = false;
                    foreach (Location newLoc in roadPath)
                    {
                        if (newLoc.Col == newColEntryPoint && newLoc.Row == newRowEntryPoint)
                        {
                            blnFoundExistingRoundAtThisLocation = true;
                            break;
                        }
                    }
                    if (blnFoundExistingRoundAtThisLocation == false)
                    {
                        rowEntryPoint = newRowEntryPoint;
                        colEntryPoint = newColEntryPoint;
                        if (rowEntryPoint > 0 && colEntryPoint > 0 && rowEntryPoint <= levelHeight && colEntryPoint <= levelWidth)
                        {
                            safeCounter = 0;
                            roadPath.Add(new Location(colEntryPoint, rowEntryPoint, newEntry, newExit));
                        }
                    }
                    //Make sure bad stuff doesn't happen.
                    if (safeCounter > 100)
                    {
                        Debug.WriteLine("uh oh: breaking, more than 100 loops and we are stuck");
                        break;
                    }
                }
                while (rowEntryPoint > 0 && colEntryPoint > 0 && rowEntryPoint <= levelHeight && colEntryPoint <= levelWidth);
            }

            //4. with this collection of an entry and exit, for each tile, select the relevant pieces from the piece list.
            List<LevelPiece> levelPieces = new();

            //4.1. Add campaign tiles if required
            if (isCampaign == true)
            {
                int blueHeight = 0;
                int redHeight = 0;
                if (levelHeight % 2 == 0)
                {
                    blueHeight = levelHeight / 2;
                    redHeight = blueHeight;
                }
                else
                {
                    blueHeight = (int)(((float)levelHeight / 2f) - 0.5f);
                    redHeight = (int)(((float)levelHeight / 2f) + 0.5f);
                }
                LevelPiece bluePiece = null;
                foreach (LevelPiece item in levelPiecePoolWithoutRivers)
                {
                    if (item.PieceName == "TileBlueCampaign")
                    {
                        bluePiece = item;
                        break;
                    }
                }
                LevelPiece redPiece = null;
                foreach (LevelPiece item in levelPiecePoolWithoutRivers)
                {
                    if (item.PieceName == "TileRedCampaign")
                    {
                        redPiece = item;
                        break;
                    }
                }
                for (int iBlue = 0; iBlue < blueHeight * levelWidth; iBlue++)
                {
                    levelPieces.Add(bluePiece);
                }
                for (int iRed = blueHeight * levelWidth; iRed < levelHeight * levelWidth; iRed++)
                {
                    levelPieces.Add(redPiece);
                }
            }
            else
            {
                for (int i = 1; i <= levelWidth * levelHeight; i++)
                {
                    Location foundItem = null;
                    foreach (Location item in roadPath)
                    {
                        if (i == GetIndexFromLocation(item, levelWidth))
                        {
                            foundItem = item;
                            //Debug.Log("Road (" + item.Col + "," + item.Row + ") from: " + item.Entry + " to " + item.Exit);
                            break;
                        }
                    }
                    if (foundItem != null)
                    {
                        List<LevelPiece> subsetPieces = new();
                        //there can be multiple pieces with the same direction/path.
                        foreach (LevelPiece piece in levelPiecePool)
                        {
                            if (foundItem.Entry == "N" && piece.NorthSide != "")
                            {
                                subsetPieces.Add(piece);
                            }
                            else if (foundItem.Entry == "E" && piece.EastSide != "")
                            {
                                subsetPieces.Add(piece);
                            }
                            else if (foundItem.Entry == "S" && piece.SouthSide != "")
                            {
                                subsetPieces.Add(piece);
                            }
                            else if (foundItem.Entry == "W" && piece.WestSide != "")
                            {
                                subsetPieces.Add(piece);
                            }
                        }
                        foreach (LevelPiece piece in subsetPieces)
                        {
                            if (foundItem.Exit == "N" && piece.NorthSide != "")
                            {
                                levelPieces.Add(piece);
                                break;
                            }
                            else if (foundItem.Exit == "E" && piece.EastSide != "")
                            {
                                levelPieces.Add(piece);
                                break;
                            }
                            else if (foundItem.Exit == "S" && piece.SouthSide != "")
                            {
                                levelPieces.Add(piece);
                                break;
                            }
                            else if (foundItem.Exit == "W" && piece.WestSide != "")
                            {
                                levelPieces.Add(piece);
                                break;
                            }
                        }
                    }
                    else
                    {
                        int r = Utility.GenerateRandomNumber(0, levelPiecePoolWithoutRivers.Count - 1);
                        levelPieces.Add(levelPiecePoolWithoutRivers[r]);
                    }
                }
            }
            return levelPieces;
        }

        private static int GetIndexFromLocation(Location item, int levelWidth)
        {
            int index = (item.Row - 1) * levelWidth + item.Col;

            return index;
        }

        private static string GetRandomDirection(string origin, string longTermOrigin)
        {
            string exit = "";
            do
            {
                exit = GetDirection(Utility.GenerateRandomNumber(1, 4));
            } while (exit == longTermOrigin || exit == origin);

            return exit;
        }

        private static string GetOppositeDirection(string direction)
        {
            switch (direction)
            {
                case "N":
                    return "S";
                case "S":
                    return "N";
                case "W":
                    return "E";
                case "E":
                    return "W";
                default:
                    return "";
            }
        }

        private static string GetDirection(int direction)
        {
            switch (direction)
            {
                case 1:
                    return "N";
                case 2:
                    return "E";
                case 3:
                    return "S";
                case 4:
                    return "W";
                default:
                    return "";
            }
        }

    }
}