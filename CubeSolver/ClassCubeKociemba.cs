using System.Diagnostics;

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
                B1 = 18, B2 = 19, B3 = 20, B4 = 21, B5 = 22, B6 = 23, B7 = 24, B8 = 25, B9 = 26
             */

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
            //Globals.aPieces[0] = Globals.aPiecesTemp[36];
            //Globals.aPieces[1] = Globals.aPiecesTemp[37];
            //Globals.aPieces[2] = Globals.aPiecesTemp[38];
            //Globals.aPieces[3] = Globals.aPiecesTemp[39];
            //Globals.aPieces[4] = Globals.aPiecesTemp[40];
            //Globals.aPieces[5] = Globals.aPiecesTemp[41];
            //Globals.aPieces[6] = Globals.aPiecesTemp[42];
            //Globals.aPieces[7] = Globals.aPiecesTemp[43];
            //Globals.aPieces[8] = Globals.aPiecesTemp[44];

            // Right face
            for (nRow = 9; nRow < 18; nRow++)
            {
                Globals.aPieces[nRow] = Globals.aPiecesTemp[nRow];
            }
            //Globals.aPieces[9] = Globals.aPiecesTemp[9];
            //Globals.aPieces[10] = Globals.aPiecesTemp[10];
            //Globals.aPieces[11] = Globals.aPiecesTemp[11];
            //Globals.aPieces[12] = Globals.aPiecesTemp[12];
            //Globals.aPieces[13] = Globals.aPiecesTemp[13];
            //Globals.aPieces[14] = Globals.aPiecesTemp[14];
            //Globals.aPieces[15] = Globals.aPiecesTemp[15];
            //Globals.aPieces[16] = Globals.aPiecesTemp[16];
            //Globals.aPieces[17] = Globals.aPiecesTemp[17];

            // Front face
            for (nRow = 18; nRow < 27; nRow++)
            {
                Globals.aPieces[nRow] = Globals.aPiecesTemp[nRow - 18];
            }
            //Globals.aPieces[18] = Globals.aPiecesTemp[0];
            //Globals.aPieces[19] = Globals.aPiecesTemp[1];
            //Globals.aPieces[20] = Globals.aPiecesTemp[2];
            //Globals.aPieces[21] = Globals.aPiecesTemp[3];
            //Globals.aPieces[22] = Globals.aPiecesTemp[4];
            //Globals.aPieces[23] = Globals.aPiecesTemp[5];
            //Globals.aPieces[24] = Globals.aPiecesTemp[6];
            //Globals.aPieces[25] = Globals.aPiecesTemp[7];
            //Globals.aPieces[26] = Globals.aPiecesTemp[8];

            // Down face
            for (nRow = 27; nRow < 36; nRow++)
            {
                Globals.aPieces[nRow] = Globals.aPiecesTemp[nRow + 18];
            }
            //Globals.aPieces[27] = Globals.aPiecesTemp[45];
            //Globals.aPieces[28] = Globals.aPiecesTemp[46];
            //Globals.aPieces[29] = Globals.aPiecesTemp[47];
            //Globals.aPieces[30] = Globals.aPiecesTemp[48];
            //Globals.aPieces[31] = Globals.aPiecesTemp[49];
            //Globals.aPieces[32] = Globals.aPiecesTemp[50];
            //Globals.aPieces[33] = Globals.aPiecesTemp[51];
            //Globals.aPieces[34] = Globals.aPiecesTemp[52];
            //Globals.aPieces[35] = Globals.aPiecesTemp[53];

            // Left face
            for (nRow = 36; nRow < 45; nRow++)
            {
                Globals.aPieces[nRow] = Globals.aPiecesTemp[nRow - 9];
            }
            //Globals.aPieces[36] = Globals.aPiecesTemp[27];
            //Globals.aPieces[37] = Globals.aPiecesTemp[28];
            //Globals.aPieces[38] = Globals.aPiecesTemp[29];
            //Globals.aPieces[39] = Globals.aPiecesTemp[30];
            //Globals.aPieces[40] = Globals.aPiecesTemp[31];
            //Globals.aPieces[41] = Globals.aPiecesTemp[32];
            //Globals.aPieces[42] = Globals.aPiecesTemp[33];
            //Globals.aPieces[43] = Globals.aPiecesTemp[34];
            //Globals.aPieces[44] = Globals.aPiecesTemp[35];

            // Back face
            for (nRow = 45; nRow < 54; nRow++)
            {
                Globals.aPieces[nRow] = Globals.aPiecesTemp[nRow - 27];
            }
            //Globals.aPieces[45] = Globals.aPiecesTemp[18];
            //Globals.aPieces[46] = Globals.aPiecesTemp[19];
            //Globals.aPieces[47] = Globals.aPiecesTemp[20];
            //Globals.aPieces[48] = Globals.aPiecesTemp[21];
            //Globals.aPieces[49] = Globals.aPiecesTemp[22];
            //Globals.aPieces[50] = Globals.aPiecesTemp[23];
            //Globals.aPieces[51] = Globals.aPiecesTemp[24];
            //Globals.aPieces[52] = Globals.aPiecesTemp[25];
            //Globals.aPieces[53] = Globals.aPiecesTemp[26];

            // Convert the colors of the cube to Kociemba colors
            for (nRow = 0; nRow < 54; nRow++)
            {
                if (Globals.aPieces[nRow] == Globals.aFaceColors[5])        // Up face: White
                {
                    Globals.aPieces[nRow] = "U";
                }
                else if (Globals.aPieces[nRow] == Globals.aFaceColors[2])   // Right face: Blue
                {
                    Globals.aPieces[nRow] = "R";
                }
                else if (Globals.aPieces[nRow] == Globals.aFaceColors[1])   // Front face: Red
                {
                    Globals.aPieces[nRow] = "F";
                }
                else if (Globals.aPieces[nRow] == Globals.aFaceColors[6])   // Down face: Yellow
                {
                    Globals.aPieces[nRow] = "D";
                }
                else if (Globals.aPieces[nRow] == Globals.aFaceColors[4])   // Left face: Green
                {
                    Globals.aPieces[nRow] = "L";
                }
                else if (Globals.aPieces[nRow] == Globals.aFaceColors[3])   // Back face: Orange
                {
                    Globals.aPieces[nRow] = "B";
                }

                cResult = cResult + Globals.aPieces[nRow];
                //Debug.WriteLine("cResult: " + cResult);
            }

            // Restore the start colors of the cube to array aPieces[]
            Array.Copy(Globals.aPiecesTemp, Globals.aPieces, 54);

            return cResult;


            // searchString:
           
            // F D L U U D R F L F L F R R F D R D U U B U F B B B F R U U R D L L L D B F R F L R R L B L B U D B D D B U

            // U:  9
            // R:  9
            // F:  9
            // D:  9
            // L:  9
            // B:  9

            // T: 54
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
