namespace CubeSolver
{
    internal sealed class ClassCubeKociemba
    {
        /// <summary>
        /// Convert the cube numbering and colors from CFOP to Kociemba numbering and colors
        /// </summary>
        public static string ConvertCubeToKociembaCube()
        {
            /* The searchString need to be ordered by sides Up Right Front Down Left Back with the order of the characters in the string, matching the order as outlined in the diagram.
                For example, a solved searchString would be:
                string searchString= "UUUUUUUUURRRRRRRRRFFFFFFFFFDDDDDDDDDLLLLLLLLLBBBBBBBBB";

               This searchString has had 90 degree clockwise rotation of the front face applied to it:
                string searchString= "UUUUUULLLURRURRURRFFFFFFFFFRRRDDDDDDLLDLLDLLDBBBBBBBBB";

               Converting piece numbering from CFOP to Kociemba:
                U1 = 36, U2 = 37, U3 = 38, U4 = 39, U5 = 40, U6 = 41, U7 = 42, U8 = 43, U9 = 44
                R1 = 9, R2 = 10, R3 = 11, R4 = 12, R5 = 13, R6 = 14, R7 = 15, R8 = 16, R9 = 17
                F1 = 0, F2 = 1, F3 = 2, F4 = 3, F5 = 4, F6 = 5, F7 = 6, F8 = 7, F9 = 8
                D1 = 45, D2 = 46, D3 = 47, D4 = 48, D5 = 49, D6 = 50, D7 = 51, D8 = 52, D9 = 53
                L1 = 27, L2 = 28, L3 = 29, L4 = 30, L5 = 31, L6 = 32, L7 = 33, L8 = 34, L9 = 35
                B1 = 18, B2 = 19, B3 = 20, B4 = 21, B5 = 22, B6 = 23, B7 = 24, B8 = 25, B9 = 26 */

            // Variables
            int nRow;
            string cResult = string.Empty;

            // Save the start colors of the cube to array aPiecesTemp[]
            Array.Copy(Globals.aPieces, Globals.aPiecesTemp, 54);

            // Reorder the pieces to match Kociemba numbering
            // Upper face
            for (nRow = 0; nRow < 9; nRow++)
            {
                Globals.aPieces[nRow] = Globals.aPiecesTemp[nRow + 36];
            }

            // Right face
            for (nRow = 9; nRow < 18; nRow++)
            {
                Globals.aPieces[nRow] = Globals.aPiecesTemp[nRow];
            }

            // Front face
            for (nRow = 18; nRow < 27; nRow++)
            {
                Globals.aPieces[nRow] = Globals.aPiecesTemp[nRow - 18];
            }

            // Down face
            for (nRow = 27; nRow < 36; nRow++)
            {
                Globals.aPieces[nRow] = Globals.aPiecesTemp[nRow + 18];
            }

            // Left face
            for (nRow = 36; nRow < 45; nRow++)
            {
                Globals.aPieces[nRow] = Globals.aPiecesTemp[nRow -9];
            }

            // Back face
            for (nRow = 45; nRow < 54; nRow++)
            {
                Globals.aPieces[nRow] = Globals.aPiecesTemp[nRow - 27];
            }

            // Convert the colors of the cube to Kociemba colors
            for (nRow = 0; nRow < 54; nRow++)
            {
                if (Globals.aPieces[nRow] == Globals.aFaceColors[5])
                {
                    Globals.aPieces[nRow] = "U";
                }
                else if (Globals.aPieces[nRow] == Globals.aFaceColors[2])
                {
                    Globals.aPieces[nRow] = "R";
                }
                else if (Globals.aPieces[nRow] == Globals.aFaceColors[1])
                {
                    Globals.aPieces[nRow] = "F";
                }
                else if (Globals.aPieces[nRow] == Globals.aFaceColors[6])
                {
                    Globals.aPieces[nRow] = "D";
                }
                else if (Globals.aPieces[nRow] == Globals.aFaceColors[4])
                {
                    Globals.aPieces[nRow] = "L";
                }
                else if (Globals.aPieces[nRow] == Globals.aFaceColors[3])
                {
                    Globals.aPieces[nRow] = "B";
                }

                cResult = cResult + Globals.aPieces[nRow];
            }

            // Restore the start colors of the cube to array aPieces[]
            Array.Copy(Globals.aPiecesTemp, Globals.aPieces, 54);

            return cResult;
        }

        /// <summary>
        /// Check if the table exists
        /// </summary>
        /// <returns></returns>
        public static bool CheckIfTableExists()
        {
            if (!File.Exists(Globals.cTablePath + "twist"))
            {
                return false;
            }

            if (!File.Exists(Globals.cTablePath + "flip"))
            {
                return false;
            }

            if (!File.Exists(Globals.cTablePath + "FRtoBR"))
            {
                return false;
            }

            if (!File.Exists(Globals.cTablePath + "URFtoDLF"))
            {
                return false;
            }

            if (!File.Exists(Globals.cTablePath + "URtoDF"))
            {
                return false;
            }

            if (!File.Exists(Globals.cTablePath + "URtoUL"))
            {
                return false;
            }

            if (!File.Exists(Globals.cTablePath + "UBtoDF"))
            {
                return false;
            }

            if (!File.Exists(Globals.cTablePath + "MergeURtoULandUBtoDF"))
            {
                return false;
            }

            if (!File.Exists(Globals.cTablePath + "Slice_URFtoDLF_Parity_Prun"))
            {
                return false;
            }

            if (!File.Exists(Globals.cTablePath + "Slice_URtoDF_Parity_Prun"))
            {
                return false;
            }

            if (!File.Exists(Globals.cTablePath + "Slice_Twist_Prun"))
            {
                return false;
            }

            if (!File.Exists(Globals.cTablePath + "Slice_Flip_Prun"))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Make a turn (with 1 letter [plus ' or 2]) of the cube/face/side
        /// </summary>
        /// <param name="cTurn"></param>
        /// <returns></returns>
        public static void SplitStringToTurns(string cTurn)
        {
            // Remove leading and trailing whitespace
            cTurn = cTurn.Trim();

            // Split the string into individual turns
            foreach (string cTurnPart in cTurn.Split(' '))
            {
                // Add the turn to the list
                Globals.lCubeTurns.Add(cTurnPart);
            }
        }
    }
}
