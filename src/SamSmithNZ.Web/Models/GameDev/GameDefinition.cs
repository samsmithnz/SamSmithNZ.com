using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace SamSmithNZ.Web.Models.GameDev
{
    public class GameDefinition
    {
        public List<LevelPiece> CampaignLevelPieces { get; set; }
        public List<LevelPiece> MissionLevelPieces { get; set; }

        public static GameDefinition Load(string fileContents)
        {
            //TextAsset gameDefinitionText = (TextAsset)Resources.Load("GameXML/GameDefinition");
            //XmlReader xmlReader = XmlReader.Create(new StringReader(gameDefinitionText.text));
            //XmlSerializer reader = new XmlSerializer(typeof(GameDefinition));
            //return (GameDefinition)reader.Deserialize(xmlReader);

            //string filePath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase).Replace("file:\\", "") + "\\Data\\Data.xml";
            //GameDefinition gameToLoad = new GameDefinition();
            //if (File.Exists(filePath) == true)
            //{
            //    StreamReader streamReader = new StreamReader(filePath);
            //    XmlSerializer reader = new XmlSerializer(typeof(GameDefinition));
            //    return (GameDefinition)reader.Deserialize(streamReader);
            //}
            //else
            //{
            //    return null;
            //}

            // convert string to stream
            byte[] byteArray = Encoding.UTF8.GetBytes(fileContents);
            //byte[] byteArray = Encoding.ASCII.GetBytes(contents);
            MemoryStream stream = new(byteArray);
            StreamReader streamReader = new(stream);
            XmlSerializer reader = new(typeof(GameDefinition));
            return (GameDefinition)reader.Deserialize(streamReader);

        }

        public static string OutputCampaignLevels(List<LevelPiece> levelPieces, int width, int height)
        {
            //string space = " ";
            //string nwCornerBorder = "╔";
            //string swCornerBorder = "╚";
            //string neCornerBorder = "╗";
            //string seCornerBorder = "╝";
            //string horizontalBorder = "═";
            //string verticalBorder = "║";

            //string nwCornerRoad = "┌";
            //string swCornerRoad = "└";
            //string neCornerRoad = "┐";
            //string seCornerRoad = "┘";
            //string horizontalRoad = "─";
            //string verticalRoad = "|";

            //╔═══╗  
            //║   ║
            //╚═══╝

            //╔═|═╗  
            //║ └──
            //╚═══╝

            //╔═|═╗╔═══╗  
            //║ └───────
            //╚═══╝╚═══╝

            StringBuilder sb = new();
            //sb.Append( nwCornerBorder + horizontalBorder + horizontalBorder + horizontalBorder + neCornerBorder + Environment.NewLine;
            //sb.Append( verticalBorder + space + space + space + verticalBorder + Environment.NewLine;
            //sb.Append( swCornerBorder + horizontalBorder + horizontalBorder + horizontalBorder + seCornerBorder + Environment.NewLine;

            int col = 0;
            int row = 1;
            for (int x = 0; x < levelPieces.Count - 1; x++)
            {
                LevelPiece item = levelPieces[x];
                col++;
                if (col > width)
                {
                    for (int line = 1; line <= 3; line++)
                    {
                        for (int i = width - 1; i >= 0; i--)
                        {
                            sb.Append(DrawLine(levelPieces[x - i - 1], line));
                        }
                        sb.Append(System.Environment.NewLine);
                    }
                    col = 1;
                    row++;
                }
            }
            for (int line = 1; line <= 3; line++)
            {
                for (int i = width - 1; i >= 0; i--)
                {
                    sb.Append(DrawLine(levelPieces[levelPieces.Count - 1 - i], line));
                }
                sb.Append(System.Environment.NewLine);
            }
            col = 1;
            row++;

            //Console.WriteLine(nwCornerBorder + horizontalBorder + neCornerBorder);
            //Console.WriteLine(verticalBorder + space + verticalBorder);
            //Console.WriteLine(swCornerBorder + horizontalBorder + seCornerBorder);

            return sb.ToString();

        }


        public static string OutputMissionLevels(List<LevelPiece> levelPieces, int width, int height)
        {
            //string space = " ";
            //string nwCornerBorder = "╔";
            //string swCornerBorder = "╚";
            //string neCornerBorder = "╗";
            //string seCornerBorder = "╝";
            //string horizontalBorder = "═";
            //string verticalBorder = "║";

            //string nwCornerRoad = "┌";
            //string swCornerRoad = "└";
            //string neCornerRoad = "┐";
            //string seCornerRoad = "┘";
            //string horizontalRoad = "─";
            //string verticalRoad = "|";

            //╔═══╗  
            //║   ║
            //╚═══╝

            //╔═|═╗  
            //║ └──
            //╚═══╝

            //╔═|═╗╔═══╗  
            //║ └───────
            //╚═══╝╚═══╝

            StringBuilder sb = new();
            //sb.Append( nwCornerBorder + horizontalBorder + horizontalBorder + horizontalBorder + neCornerBorder + Environment.NewLine;
            //sb.Append( verticalBorder + space + space + space + verticalBorder + Environment.NewLine;
            //sb.Append( swCornerBorder + horizontalBorder + horizontalBorder + horizontalBorder + seCornerBorder + Environment.NewLine;

            int col = 0;
            int row = 1;
            for (int x = 0; x < levelPieces.Count - 1; x++)
            {
                LevelPiece item = levelPieces[x];
                col++;
                if (col > width)
                {
                    for (int line = 1; line <= 3; line++)
                    {
                        for (int i = width - 1; i >= 0; i--)
                        {
                            sb.Append(DrawLine(levelPieces[x - i - 1], line));
                        }
                        sb.Append(System.Environment.NewLine);
                    }
                    col = 1;
                    row++;
                }
            }
            for (int line = 1; line <= 3; line++)
            {
                for (int i = width - 1; i >= 0; i--)
                {
                    sb.Append(DrawLine(levelPieces[levelPieces.Count - 1 - i], line));
                }
                sb.Append(System.Environment.NewLine);
            }
            col = 1;
            row++;

            //Console.WriteLine(nwCornerBorder + horizontalBorder + neCornerBorder);
            //Console.WriteLine(verticalBorder + space + verticalBorder);
            //Console.WriteLine(swCornerBorder + horizontalBorder + seCornerBorder);

            return sb.ToString();

        }

        private static string DrawLine(LevelPiece piece, int row)
        {
            string space = " ";
            string nwCornerBorder = "╔";
            string swCornerBorder = "╚";
            string neCornerBorder = "╗";
            string seCornerBorder = "╝";
            string horizontalBorder = "═";
            string verticalBorder = "║";
            string nwCornerRoad = "┌";
            string swCornerRoad = "└";
            string neCornerRoad = "┐";
            string seCornerRoad = "┘";
            string horizontalRoad = "─";
            string verticalRoad = "|";
            string blue = "O";
            string red = "X";

            string result = "";
            if (piece.PieceName == "TileBlueCampaign")
            {
                space = blue;
            }
            else if (piece.PieceName == "TileRedCampaign")
            {
                space = red;
            }

            switch (row)
            {
                case 1:
                    if (piece.NorthSide == "R")
                    {
                        result = nwCornerBorder + piece.PieceNumber + verticalRoad + horizontalBorder + neCornerBorder;
                    }
                    else
                    {
                        result = nwCornerBorder + piece.PieceNumber + horizontalBorder + horizontalBorder + neCornerBorder;
                    }
                    break;
                case 2:
                    if (piece.WestSide == "R" && piece.EastSide == "R")
                    {
                        result = horizontalRoad + horizontalRoad + horizontalRoad + horizontalRoad + horizontalRoad;
                    }
                    else if (piece.NorthSide == "R" && piece.SouthSide == "R")
                    {
                        result = verticalBorder + space + verticalRoad + space + verticalBorder;
                    }
                    else if (piece.NorthSide == "R" && piece.EastSide == "R")
                    {
                        result = verticalBorder + space + swCornerRoad + horizontalRoad + horizontalRoad;
                    }
                    else if (piece.NorthSide == "R" && piece.WestSide == "R")
                    {
                        result = horizontalRoad + horizontalRoad + seCornerRoad + space + verticalBorder;
                    }
                    else if (piece.SouthSide == "R" && piece.EastSide == "R")
                    {
                        result = verticalBorder + space + nwCornerRoad + horizontalRoad + horizontalRoad;
                    }
                    else if (piece.SouthSide == "R" && piece.WestSide == "R")
                    {
                        result = horizontalRoad + horizontalRoad + neCornerRoad + space + verticalBorder;
                    }
                    else
                    {
                        result = verticalBorder + space + space + space + verticalBorder;
                    }
                    break;
                case 3:
                    if (piece.SouthSide == "R")
                    {
                        result = swCornerBorder + horizontalBorder + verticalRoad + horizontalBorder + seCornerBorder;
                    }
                    else
                    {
                        result = swCornerBorder + horizontalBorder + horizontalBorder + horizontalBorder + seCornerBorder;
                    }
                    break;
            }

            return result;
        }
    }
}