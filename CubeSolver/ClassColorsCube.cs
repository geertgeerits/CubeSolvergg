using static CubeSolver.Globals;

namespace CubeSolver
{
    internal sealed class ClassColorsCube
    {
        /// <summary>
        /// Check the number of colors of the cube
        /// </summary>
        /// <returns></returns>
        public static bool CheckNumberColors()
        {
            // Create and start a stopwatch instance
            //long startTime = Stopwatch.GetTimestamp();

            int nNumberOfColors1 = 0;
            int nNumberOfColors2 = 0;
            int nNumberOfColors3 = 0;
            int nNumberOfColors4 = 0;
            int nNumberOfColors5 = 0;
            int nNumberOfColors6 = 0;

            int nItem;

            //// Check the number of colors of the cube
            // Front face
            for (nItem = 0; nItem < 9; nItem++)
            {
                if (aPieces[nItem] == aFaceColors[1])
                {
                    nNumberOfColors1++;
                }
            }

            for (nItem = 0; nItem < 9; nItem++)
            {
                if (aPieces[nItem] == aFaceColors[2])
                {
                    nNumberOfColors2++;
                }
            }

            for (nItem = 0; nItem < 9; nItem++)
            {
                if (aPieces[nItem] == aFaceColors[3])
                {
                    nNumberOfColors3++;
                }
            }

            for (nItem = 0; nItem < 9; nItem++)
            {
                if (aPieces[nItem] == aFaceColors[4])
                {
                    nNumberOfColors4++;
                }
            }

            for (nItem = 0; nItem < 9; nItem++)
            {
                if (aPieces[nItem] == aFaceColors[5])
                {
                    nNumberOfColors5++;
                }
            }

            for (nItem = 0; nItem < 9; nItem++)
            {
                if (aPieces[nItem] == aFaceColors[6])
                {
                    nNumberOfColors6++;
                }
            }

            // Right face
            for (nItem = 9; nItem < 18; nItem++)
            {
                if (aPieces[nItem] == aFaceColors[1])
                {
                    nNumberOfColors1++;
                }
            }

            for (nItem = 9; nItem < 18; nItem++)
            {
                if (aPieces[nItem] == aFaceColors[2])
                {
                    nNumberOfColors2++;
                }
            }

            for (nItem = 9; nItem < 18; nItem++)
            {
                if (aPieces[nItem] == aFaceColors[3])
                {
                    nNumberOfColors3++;
                }
            }

            for (nItem = 9; nItem < 18; nItem++)
            {
                if (aPieces[nItem] == aFaceColors[4])
                {
                    nNumberOfColors4++;
                }
            }

            for (nItem = 9; nItem < 18; nItem++)
            {
                if (aPieces[nItem] == aFaceColors[5])
                {
                    nNumberOfColors5++;
                }
            }

            for (nItem = 9; nItem < 18; nItem++)
            {
                if (aPieces[nItem] == aFaceColors[6])
                {
                    nNumberOfColors6++;
                }
            }

            // Back face
            for (nItem = 18; nItem < 27; nItem++)
            {
                if (aPieces[nItem] == aFaceColors[1])
                {
                    nNumberOfColors1++;
                }
            }

            for (nItem = 18; nItem < 27; nItem++)
            {
                if (aPieces[nItem] == aFaceColors[2])
                {
                    nNumberOfColors2++;
                }
            }

            for (nItem = 18; nItem < 27; nItem++)
            {
                if (aPieces[nItem] == aFaceColors[3])
                {
                    nNumberOfColors3++;
                }
            }

            for (nItem = 18; nItem < 27; nItem++)
            {
                if (aPieces[nItem] == aFaceColors[4])
                {
                    nNumberOfColors4++;
                }
            }

            for (nItem = 18; nItem < 27; nItem++)
            {
                if (aPieces[nItem] == aFaceColors[5])
                {
                    nNumberOfColors5++;
                }
            }

            for (nItem = 18; nItem < 27; nItem++)
            {
                if (aPieces[nItem] == aFaceColors[6])
                {
                    nNumberOfColors6++;
                }
            }

            // Left face
            for (nItem = 27; nItem < 36; nItem++)
            {
                if (aPieces[nItem] == aFaceColors[1])
                {
                    nNumberOfColors1++;
                }
            }

            for (nItem = 27; nItem < 36; nItem++)
            {
                if (aPieces[nItem] == aFaceColors[2])
                {
                    nNumberOfColors2++;
                }
            }

            for (nItem = 27; nItem < 36; nItem++)
            {
                if (aPieces[nItem] == aFaceColors[3])
                {
                    nNumberOfColors3++;
                }
            }

            for (nItem = 27; nItem < 36; nItem++)
            {
                if (aPieces[nItem] == aFaceColors[4])
                {
                    nNumberOfColors4++;
                }
            }

            for (nItem = 27; nItem < 36; nItem++)
            {
                if (aPieces[nItem] == aFaceColors[5])
                {
                    nNumberOfColors5++;
                }
            }

            for (nItem = 27; nItem < 36; nItem++)
            {
                if (aPieces[nItem] == aFaceColors[6])
                {
                    nNumberOfColors6++;
                }
            }

            // Top layer
            for (nItem = 36; nItem < 45; nItem++)
            {
                if (aPieces[nItem] == aFaceColors[1])
                {
                    nNumberOfColors1++;
                }
            }

            for (nItem = 36; nItem < 45; nItem++)
            {
                if (aPieces[nItem] == aFaceColors[2])
                {
                    nNumberOfColors2++;
                }
            }

            for (nItem = 36; nItem < 45; nItem++)
            {
                if (aPieces[nItem] == aFaceColors[3])
                {
                    nNumberOfColors3++;
                }
            }

            for (nItem = 36; nItem < 45; nItem++)
            {
                if (aPieces[nItem] == aFaceColors[4])
                {
                    nNumberOfColors4++;
                }
            }

            for (nItem = 36; nItem < 45; nItem++)
            {
                if (aPieces[nItem] == aFaceColors[5])
                {
                    nNumberOfColors5++;
                }
            }

            for (nItem = 36; nItem < 45; nItem++)
            {
                if (aPieces[nItem] == aFaceColors[6])
                {
                    nNumberOfColors6++;
                }
            }

            // Bottom layer
            for (nItem = 45; nItem < 54; nItem++)
            {
                if (aPieces[nItem] == aFaceColors[1])
                {
                    nNumberOfColors1++;
                }
            }

            for (nItem = 45; nItem < 54; nItem++)
            {
                if (aPieces[nItem] == aFaceColors[2])
                {
                    nNumberOfColors2++;
                }
            }

            for (nItem = 45; nItem < 54; nItem++)
            {
                if (aPieces[nItem] == aFaceColors[3])
                {
                    nNumberOfColors3++;
                }
            }

            for (nItem = 45; nItem < 54; nItem++)
            {
                if (aPieces[nItem] == aFaceColors[4])
                {
                    nNumberOfColors4++;
                }
            }

            for (nItem = 45; nItem < 54; nItem++)
            {
                if (aPieces[nItem] == aFaceColors[5])
                {
                    nNumberOfColors5++;
                }
            }

            for (nItem = 45; nItem < 54; nItem++)
            {
                if (aPieces[nItem] == aFaceColors[6])
                {
                    nNumberOfColors6++;
                }
            }

            if (nNumberOfColors1 != 9 || nNumberOfColors2 != 9 || nNumberOfColors3 != 9 || nNumberOfColors4 != 9 || nNumberOfColors5 != 9 || nNumberOfColors6 != 9)
            {
                _ = Application.Current!.Windows[0].Page!.DisplayAlert(CubeLang.ErrorTitle_Text, CubeLang.MessageNineSameColor_Text, CubeLang.ButtonClose_Text);
                return false;
            }

            //// Check the number of colors of the central square of the cube
            bool bColorCenterCube = true;

            if (aPieces[40] == aPieces[4] || aPieces[40] == aPieces[13] || aPieces[40] == aPieces[31] || aPieces[40] == aPieces[22] || aPieces[40] == aPieces[49])
            {
                bColorCenterCube = false;
            }

            if (aPieces[4] == aPieces[40] || aPieces[4] == aPieces[13] || aPieces[4] == aPieces[31] || aPieces[4] == aPieces[22] || aPieces[4] == aPieces[49])
            {
                bColorCenterCube = false;
            }

            if (aPieces[13] == aPieces[4] || aPieces[13] == aPieces[40] || aPieces[13] == aPieces[31] || aPieces[13] == aPieces[22] || aPieces[13] == aPieces[49])
            {
                bColorCenterCube = false;
            }

            if (aPieces[31] == aPieces[4] || aPieces[31] == aPieces[13] || aPieces[31] == aPieces[40] || aPieces[31] == aPieces[22] || aPieces[31] == aPieces[49])
            {
                bColorCenterCube = false;
            }

            if (aPieces[22] == aPieces[4] || aPieces[22] == aPieces[13] || aPieces[22] == aPieces[31] || aPieces[22] == aPieces[40] || aPieces[22] == aPieces[49])
            {
                bColorCenterCube = false;
            }

            if (aPieces[49] == aPieces[4] || aPieces[49] == aPieces[13] || aPieces[49] == aPieces[31] || aPieces[49] == aPieces[22] || aPieces[49] == aPieces[40])
            {
                bColorCenterCube = false;
            }

            if (!bColorCenterCube)
            {
                _ = Application.Current!.Windows[0].Page!.DisplayAlert(CubeLang.ErrorTitle_Text, CubeLang.MessageColorCentralCube_Text, CubeLang.ButtonClose_Text);
                return false;
            }

            //// Check the number of colors of the corner cubes of the cube if there are no corner cubes with the same color
            bool bColorCornerCube = true;

            if (aPieces[42] == aPieces[29] || aPieces[42] == aPieces[0] || aPieces[0] == aPieces[29])
            {
                bColorCornerCube = false;
            }

            if (aPieces[36] == aPieces[27] || aPieces[36] == aPieces[20] || aPieces[27] == aPieces[20])
            {
                bColorCornerCube = false;
            }

            if (aPieces[38] == aPieces[11] || aPieces[38] == aPieces[18] || aPieces[11] == aPieces[18])
            {
                bColorCornerCube = false;
            }

            if (aPieces[44] == aPieces[2] || aPieces[44] == aPieces[9] || aPieces[2] == aPieces[9])
            {
                bColorCornerCube = false;
            }

            if (aPieces[45] == aPieces[35] || aPieces[45] == aPieces[6] || aPieces[6] == aPieces[35])
            {
                bColorCornerCube = false;
            }

            if (aPieces[51] == aPieces[33] || aPieces[51] == aPieces[26] || aPieces[26] == aPieces[33])
            {
                bColorCornerCube = false;
            }

            if (aPieces[53] == aPieces[17] || aPieces[53] == aPieces[24] || aPieces[24] == aPieces[17])
            {
                bColorCornerCube = false;
            }

            if (aPieces[47] == aPieces[15] || aPieces[47] == aPieces[8] || aPieces[8] == aPieces[15])
            {
                bColorCornerCube = false;
            }

            if (!bColorCornerCube)
            {
                _ = Application.Current!.Windows[0].Page!.DisplayAlert(CubeLang.ErrorTitle_Text, CubeLang.MessageColorCornerCube_Text, CubeLang.ButtonClose_Text);
                return false;
            }

            //// Check the number of colors of the corner cubes if there are no more than 4 of the same colors on an corner
            int[] aNumberOfColors = [0, 0, 0, 0, 0, 0, 0];

            CheckNumberColorsCornerCube(aNumberOfColors, 1);
            CheckNumberColorsCornerCube(aNumberOfColors, 2);
            CheckNumberColorsCornerCube(aNumberOfColors, 3);
            CheckNumberColorsCornerCube(aNumberOfColors, 4);
            CheckNumberColorsCornerCube(aNumberOfColors, 5);
            CheckNumberColorsCornerCube(aNumberOfColors, 6);

            if (aNumberOfColors[1] > 4 || aNumberOfColors[2] > 4 || aNumberOfColors[3] > 4 || aNumberOfColors[4] > 4 || aNumberOfColors[5] > 4 || aNumberOfColors[6] > 4)
            {
                _ = Application.Current!.Windows[0].Page!.DisplayAlert(CubeLang.ErrorTitle_Text, CubeLang.MessageFourSameColorCornerCube_Text, CubeLang.ButtonClose_Text);
                return false;
            }

            //// Check the number of colors of the edge cubes if there are no edge cubes with the same color
            bColorCornerCube = true;

            if (aPieces[37] == aPieces[19] || aPieces[39] == aPieces[28] || aPieces[41] == aPieces[10] || aPieces[43] == aPieces[1])
            {
                bColorCornerCube = false;
            }

            if (aPieces[46] == aPieces[7] || aPieces[48] == aPieces[34] || aPieces[50] == aPieces[16] || aPieces[52] == aPieces[25])
            {
                bColorCornerCube = false;
            }

            if (!bColorCornerCube)
            {
                _ = Application.Current!.Windows[0].Page!.DisplayAlert(CubeLang.ErrorTitle_Text, CubeLang.MessageColorEdgeCube_Text, CubeLang.ButtonClose_Text);
                return false;
            }

            //// Check the number of colors of the edge cubes if there are no more than 4 of the same colors on an edge
            aNumberOfColors = [0, 0, 0, 0, 0, 0, 0];

            CheckNumberColorsEdgeCube(aNumberOfColors, 1);
            CheckNumberColorsEdgeCube(aNumberOfColors, 2);
            CheckNumberColorsEdgeCube(aNumberOfColors, 3);
            CheckNumberColorsEdgeCube(aNumberOfColors, 4);
            CheckNumberColorsEdgeCube(aNumberOfColors, 5);
            CheckNumberColorsEdgeCube(aNumberOfColors, 6);

            if (aNumberOfColors[1] > 4 || aNumberOfColors[2] > 4 || aNumberOfColors[3] > 4 || aNumberOfColors[4] > 4 || aNumberOfColors[5] > 4 || aNumberOfColors[6] > 4)
            {
                _ = Application.Current!.Windows[0].Page!.DisplayAlert(CubeLang.ErrorTitle_Text, CubeLang.MessageFourSameColorEdgeCube_Text, CubeLang.ButtonClose_Text);
                return false;
            }

            //// Check the opposite center pieces of the cube
            // Colors: 1= red, 2= blue, 3= orange, 4= green, 5= white, 6= yellow
            // Center pieces: 4= front/red, 13= right/blue, 22= back/orange, 31= left/green, 40= up/white, 49= down/yellow
            // Opposite colors: red-orange 1-3, blue-green 2-4, white-yellow 5-6
            // Opposite center pieces: front-back 4-22, right-left 13-31, up-down 40-49
            bool bResult1 = CheckOppositeCenterPieces(4, 1, 22, 3);
            bool bResult2 = CheckOppositeCenterPieces(4, 2, 22, 4);
            bool bResult3 = CheckOppositeCenterPieces(4, 3, 22, 1);
            bool bResult4 = CheckOppositeCenterPieces(4, 4, 22, 2);
            bool bResult5 = CheckOppositeCenterPieces(4, 5, 22, 6);
            bool bResult6 = CheckOppositeCenterPieces(4, 6, 22, 5);

            if (!bResult1 || !bResult2 || !bResult3 || !bResult4 || !bResult5 || !bResult6)
            {
                _ = Application.Current!.Windows[0].Page!.DisplayAlert(CubeLang.ErrorTitle_Text, CubeLang.MessageColorCenterPiece_Text, CubeLang.ButtonClose_Text);
                return false;
            }

            bResult1 = CheckOppositeCenterPieces(13, 1, 31, 3);
            bResult2 = CheckOppositeCenterPieces(13, 2, 31, 4);
            bResult3 = CheckOppositeCenterPieces(13, 3, 31, 1);
            bResult4 = CheckOppositeCenterPieces(13, 4, 31, 2);
            bResult5 = CheckOppositeCenterPieces(13, 5, 31, 6);
            bResult6 = CheckOppositeCenterPieces(13, 6, 31, 5);

            if (!bResult1 || !bResult2 || !bResult3 || !bResult4 || !bResult5 || !bResult6)
            {
                _ = Application.Current!.Windows[0].Page!.DisplayAlert(CubeLang.ErrorTitle_Text, CubeLang.MessageColorCenterPiece_Text, CubeLang.ButtonClose_Text);
                return false;
            }

            bResult1 = CheckOppositeCenterPieces(22, 1, 4, 3);
            bResult2 = CheckOppositeCenterPieces(22, 2, 4, 4);
            bResult3 = CheckOppositeCenterPieces(22, 3, 4, 1);
            bResult4 = CheckOppositeCenterPieces(22, 4, 4, 2);
            bResult5 = CheckOppositeCenterPieces(22, 5, 4, 6);
            bResult6 = CheckOppositeCenterPieces(22, 6, 4, 5);

            if (!bResult1 || !bResult2 || !bResult3 || !bResult4 || !bResult5 || !bResult6)
            {
                _ = Application.Current!.Windows[0].Page!.DisplayAlert(CubeLang.ErrorTitle_Text, CubeLang.MessageColorCenterPiece_Text, CubeLang.ButtonClose_Text);
                return false;
            }

            bResult1 = CheckOppositeCenterPieces(31, 1, 13, 3);
            bResult2 = CheckOppositeCenterPieces(31, 2, 13, 4);
            bResult3 = CheckOppositeCenterPieces(31, 3, 13, 1);
            bResult4 = CheckOppositeCenterPieces(31, 4, 13, 2);
            bResult5 = CheckOppositeCenterPieces(31, 5, 13, 6);
            bResult6 = CheckOppositeCenterPieces(31, 6, 13, 5);

            if (!bResult1 || !bResult2 || !bResult3 || !bResult4 || !bResult5 || !bResult6)
            {
                _ = Application.Current!.Windows[0].Page!.DisplayAlert(CubeLang.ErrorTitle_Text, CubeLang.MessageColorCenterPiece_Text, CubeLang.ButtonClose_Text);
                return false;
            }

            bResult1 = CheckOppositeCenterPieces(40, 1, 49, 3);
            bResult2 = CheckOppositeCenterPieces(40, 2, 49, 4);
            bResult3 = CheckOppositeCenterPieces(40, 3, 49, 1);
            bResult4 = CheckOppositeCenterPieces(40, 4, 49, 2);
            bResult5 = CheckOppositeCenterPieces(40, 5, 49, 6);
            bResult6 = CheckOppositeCenterPieces(40, 6, 49, 5);

            if (!bResult1 || !bResult2 || !bResult3 || !bResult4 || !bResult5 || !bResult6)
            {
                _ = Application.Current!.Windows[0].Page!.DisplayAlert(CubeLang.ErrorTitle_Text, CubeLang.MessageColorCenterPiece_Text, CubeLang.ButtonClose_Text);
                return false;
            }

            bResult1 = CheckOppositeCenterPieces(49, 1, 40, 3);
            bResult2 = CheckOppositeCenterPieces(49, 2, 40, 4);
            bResult3 = CheckOppositeCenterPieces(49, 3, 40, 1);
            bResult4 = CheckOppositeCenterPieces(49, 4, 40, 2);
            bResult5 = CheckOppositeCenterPieces(49, 5, 40, 6);
            bResult6 = CheckOppositeCenterPieces(49, 6, 40, 5);

            if (!bResult1 || !bResult2 || !bResult3 || !bResult4 || !bResult5 || !bResult6)
            {
                _ = Application.Current!.Windows[0].Page!.DisplayAlert(CubeLang.ErrorTitle_Text, CubeLang.MessageColorCenterPiece_Text, CubeLang.ButtonClose_Text);
                return false;
            }

            // Check the neighbors and opposite center pieces of the cube
            if (!CheckNeighborsCenterPieces())
            {
                _ = Application.Current!.Windows[0].Page!.DisplayAlert(CubeLang.ErrorTitle_Text, CubeLang.MessageColorCenterPiece_Text, CubeLang.ButtonClose_Text);
                return false;
            }

            // Stop the stopwatch and get the elapsed time
            //TimeSpan delta = Stopwatch.GetElapsedTime(startTime);
            //_ = Application.Current!.Windows[0].Page!.DisplayAlert("CheckNumberColors", $"Time elapsed (hh:mm:ss.xxxxxxx): {delta}", "OK");

            return true;
        }

        /// <summary>
        /// Check the opposite center pieces of the cube
        /// </summary>
        /// <param name="nPiece1"></param>
        /// <param name="nColor1"></param>
        /// <param name="nPiece2"></param>
        /// <param name="nColor2"></param>
        /// <returns></returns>
        private static bool CheckOppositeCenterPieces(int nPiece1, int nColor1, int nPiece2, int nColor2)
        {
            if (aPieces[nPiece1] == aFaceColors[nColor1] && aPieces[nPiece2] != aFaceColors[nColor2])
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Check the neighbors and opposite center pieces of the cube
        /// </summary>
        /// <returns></returns>
        private static bool CheckNeighborsCenterPieces()
        {
            /* aFaceColors[1] -> Front face: Red 
               aFaceColors[2] -> Right face: Blue 
               aFaceColors[3] -> Back face: Orange 
               aFaceColors[4] -> Left face: Green 
               aFaceColors[5] -> Up face: White 
               aFaceColors[6] -> Down face: Yellow */

            // Front face Red and Up face White
            if (aPieces[4] == aFaceColors[1] && aPieces[40] == aFaceColors[5])
            {
                if (aPieces[13] != aFaceColors[2] || aPieces[22] != aFaceColors[3] || aPieces[31] != aFaceColors[4] || aPieces[49] != aFaceColors[6])
                {
                    return false;
                }
            }

            // Front face Blue and Up face White
            if (aPieces[4] == aFaceColors[2] && aPieces[40] == aFaceColors[5])
            {
                if (aPieces[13] != aFaceColors[3] || aPieces[22] != aFaceColors[4] || aPieces[31] != aFaceColors[1] || aPieces[49] != aFaceColors[6])
                {
                    return false;
                }
            }

            // Front face Orange and Up face White
            if (aPieces[4] == aFaceColors[3] && aPieces[40] == aFaceColors[5])
            {
                if (aPieces[13] != aFaceColors[4] || aPieces[22] != aFaceColors[1] || aPieces[31] != aFaceColors[2] || aPieces[49] != aFaceColors[6])
                {
                    return false;
                }
            }

            // Front face Green and Up face White
            if (aPieces[4] == aFaceColors[4] && aPieces[40] == aFaceColors[5])
            {
                if (aPieces[13] != aFaceColors[1] || aPieces[22] != aFaceColors[2] || aPieces[31] != aFaceColors[3] || aPieces[49] != aFaceColors[6])
                {
                    return false;
                }
            }

            // Front face White and Up face Red
            if (aPieces[4] == aFaceColors[5] && aPieces[40] == aFaceColors[1])
            {
                if (aPieces[13] != aFaceColors[4] || aPieces[22] != aFaceColors[6] || aPieces[31] != aFaceColors[2] || aPieces[49] != aFaceColors[3])
                {
                    return false;
                }
            }

            // Front face Green and Up face Red
            if (aPieces[4] == aFaceColors[4] && aPieces[40] == aFaceColors[1])
            {
                if (aPieces[13] != aFaceColors[6] || aPieces[22] != aFaceColors[2] || aPieces[31] != aFaceColors[5] || aPieces[49] != aFaceColors[3])
                {
                    return false;
                }
            }

            // Front face Yellow and Up face Red
            if (aPieces[4] == aFaceColors[6] && aPieces[40] == aFaceColors[1])
            {
                if (aPieces[13] != aFaceColors[2] || aPieces[22] != aFaceColors[5] || aPieces[31] != aFaceColors[4] || aPieces[49] != aFaceColors[3])
                {
                    return false;
                }
            }

            // Front face Blue and Up face Red
            if (aPieces[4] == aFaceColors[2] && aPieces[40] == aFaceColors[1])
            {
                if (aPieces[13] != aFaceColors[5] || aPieces[22] != aFaceColors[4] || aPieces[31] != aFaceColors[6] || aPieces[49] != aFaceColors[3])
                {
                    return false;
                }
            }

            // Front face White and Up face Green
            if (aPieces[4] == aFaceColors[5] && aPieces[40] == aFaceColors[4])
            {
                if (aPieces[13] != aFaceColors[3] || aPieces[22] != aFaceColors[6] || aPieces[31] != aFaceColors[1] || aPieces[49] != aFaceColors[2])
                {
                    return false;
                }
            }

            // Front face Orange and Up face Green
            if (aPieces[4] == aFaceColors[3] && aPieces[40] == aFaceColors[4])
            {
                if (aPieces[13] != aFaceColors[6] || aPieces[22] != aFaceColors[1] || aPieces[31] != aFaceColors[5] || aPieces[49] != aFaceColors[2])
                {
                    return false;
                }
            }

            // Front face Yellow and Up face Green
            if (aPieces[4] == aFaceColors[6] && aPieces[40] == aFaceColors[4])
            {
                if (aPieces[13] != aFaceColors[1] || aPieces[22] != aFaceColors[5] || aPieces[31] != aFaceColors[3] || aPieces[49] != aFaceColors[2])
                {
                    return false;
                }
            }

            // Front face Red and Up face Green
            if (aPieces[4] == aFaceColors[1] && aPieces[40] == aFaceColors[4])
            {
                if (aPieces[13] != aFaceColors[5] || aPieces[22] != aFaceColors[3] || aPieces[31] != aFaceColors[6] || aPieces[49] != aFaceColors[2])
                {
                    return false;
                }
            }

            // Front face White and Up face Orange
            if (aPieces[4] == aFaceColors[5] && aPieces[40] == aFaceColors[3])
            {
                if (aPieces[13] != aFaceColors[2] || aPieces[22] != aFaceColors[6] || aPieces[31] != aFaceColors[4] || aPieces[49] != aFaceColors[1])
                {
                    return false;
                }
            }

            // Front face Blue and Up face Orange
            if (aPieces[4] == aFaceColors[2] && aPieces[40] == aFaceColors[3])
            {
                if (aPieces[13] != aFaceColors[6] || aPieces[22] != aFaceColors[4] || aPieces[31] != aFaceColors[5] || aPieces[49] != aFaceColors[1])
                {
                    return false;
                }
            }

            // Front face Yellow and Up face Orange
            if (aPieces[4] == aFaceColors[6] && aPieces[40] == aFaceColors[3])
            {
                if (aPieces[13] != aFaceColors[4] || aPieces[22] != aFaceColors[5] || aPieces[31] != aFaceColors[2] || aPieces[49] != aFaceColors[1])
                {
                    return false;
                }
            }

            // Front face Green and Up face Orange
            if (aPieces[4] == aFaceColors[4] && aPieces[40] == aFaceColors[3])
            {
                if (aPieces[13] != aFaceColors[5] || aPieces[22] != aFaceColors[2] || aPieces[31] != aFaceColors[6] || aPieces[49] != aFaceColors[1])
                {
                    return false;
                }
            }

            // Front face White and Up face Blue
            if (aPieces[4] == aFaceColors[5] && aPieces[40] == aFaceColors[2])
            {
                if (aPieces[13] != aFaceColors[1] || aPieces[22] != aFaceColors[6] || aPieces[31] != aFaceColors[3] || aPieces[49] != aFaceColors[4])
                {
                    return false;
                }
            }

            // Front face Red and Up face Blue
            if (aPieces[4] == aFaceColors[1] && aPieces[40] == aFaceColors[2])
            {
                if (aPieces[13] != aFaceColors[6] || aPieces[22] != aFaceColors[3] || aPieces[31] != aFaceColors[5] || aPieces[49] != aFaceColors[4])
                {
                    return false;
                }
            }

            // Front face Yellow and Up face Blue
            if (aPieces[4] == aFaceColors[6] && aPieces[40] == aFaceColors[2])
            {
                if (aPieces[13] != aFaceColors[3] || aPieces[22] != aFaceColors[5] || aPieces[31] != aFaceColors[1] || aPieces[49] != aFaceColors[4])
                {
                    return false;
                }
            }

            // Front face Orange and Up face Blue
            if (aPieces[4] == aFaceColors[3] && aPieces[40] == aFaceColors[2])
            {
                if (aPieces[13] != aFaceColors[5] || aPieces[22] != aFaceColors[1] || aPieces[31] != aFaceColors[6] || aPieces[49] != aFaceColors[4])
                {
                    return false;
                }
            }

            // Front face Red and Up face Yellow
            if (aPieces[4] == aFaceColors[1] && aPieces[40] == aFaceColors[6])
            {
                if (aPieces[13] != aFaceColors[4] || aPieces[22] != aFaceColors[3] || aPieces[31] != aFaceColors[2] || aPieces[49] != aFaceColors[5])
                {
                    return false;
                }
            }

            // Front face Green and Up face Yellow
            if (aPieces[4] == aFaceColors[4] && aPieces[40] == aFaceColors[6])
            {
                if (aPieces[13] != aFaceColors[3] || aPieces[22] != aFaceColors[2] || aPieces[31] != aFaceColors[1] || aPieces[49] != aFaceColors[5])
                {
                    return false;
                }
            }

            // Front face Orange and Up face Yellow
            if (aPieces[4] == aFaceColors[3] && aPieces[40] == aFaceColors[6])
            {
                if (aPieces[13] != aFaceColors[2] || aPieces[22] != aFaceColors[1] || aPieces[31] != aFaceColors[4] || aPieces[49] != aFaceColors[5])
                {
                    return false;
                }
            }

            // Front face Blue and Up face Yellow
            if (aPieces[4] == aFaceColors[2] && aPieces[40] == aFaceColors[6])
            {
                if (aPieces[13] != aFaceColors[1] || aPieces[22] != aFaceColors[4] || aPieces[31] != aFaceColors[3] || aPieces[49] != aFaceColors[5])
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Check the number of colors on the corner cubes
        /// </summary>
        /// <param name="aNumberOfColors"></param>
        /// <param name="nColor"></param>
        private static void CheckNumberColorsCornerCube(int[] aNumberOfColors, int nColor)
        {
            // Up face
            if (aPieces[0] == aFaceColors[nColor])
            {
                aNumberOfColors[nColor]++;
            }

            if (aPieces[2] == aFaceColors[nColor])
            {
                aNumberOfColors[nColor]++;
            }

            if (aPieces[6] == aFaceColors[nColor])
            {
                aNumberOfColors[nColor]++;
            }

            if (aPieces[8] == aFaceColors[nColor])
            {
                aNumberOfColors[nColor]++;
            }

            // Right face
            if (aPieces[9] == aFaceColors[nColor])
            {
                aNumberOfColors[nColor]++;
            }

            if (aPieces[11] == aFaceColors[nColor])
            {
                aNumberOfColors[nColor]++;
            }

            if (aPieces[15] == aFaceColors[nColor])
            {
                aNumberOfColors[nColor]++;
            }

            if (aPieces[17] == aFaceColors[nColor])
            {
                aNumberOfColors[nColor]++;
            }

            // Back face
            if (aPieces[18] == aFaceColors[nColor])
            {
                aNumberOfColors[nColor]++;
            }

            if (aPieces[20] == aFaceColors[nColor])
            {
                aNumberOfColors[nColor]++;
            }

            if (aPieces[24] == aFaceColors[nColor])
            {
                aNumberOfColors[nColor]++;
            }

            if (aPieces[26] == aFaceColors[nColor])
            {
                aNumberOfColors[nColor]++;
            }

            // Left face
            if (aPieces[27] == aFaceColors[nColor])
            {
                aNumberOfColors[nColor]++;
            }

            if (aPieces[29] == aFaceColors[nColor])
            {
                aNumberOfColors[nColor]++;
            }

            if (aPieces[33] == aFaceColors[nColor])
            {
                aNumberOfColors[nColor]++;
            }

            if (aPieces[35] == aFaceColors[nColor])
            {
                aNumberOfColors[nColor]++;
            }

            // Up face
            if (aPieces[36] == aFaceColors[nColor])
            {
                aNumberOfColors[nColor]++;
            }

            if (aPieces[38] == aFaceColors[nColor])
            {
                aNumberOfColors[nColor]++;
            }

            if (aPieces[42] == aFaceColors[nColor])
            {
                aNumberOfColors[nColor]++;
            }

            if (aPieces[44] == aFaceColors[nColor])
            {
                aNumberOfColors[nColor]++;
            }

            // Down face
            if (aPieces[45] == aFaceColors[nColor])
            {
                aNumberOfColors[nColor]++;
            }

            if (aPieces[47] == aFaceColors[nColor])
            {
                aNumberOfColors[nColor]++;
            }

            if (aPieces[51] == aFaceColors[nColor])
            {
                aNumberOfColors[nColor]++;
            }

            if (aPieces[53] == aFaceColors[nColor])
            {
                aNumberOfColors[nColor]++;
            }
        }

        /// <summary>
        /// Check the number of colors on the edge cubes
        /// </summary>
        /// <param name="aNumberOfColors"></param>
        /// <param name="nColor"></param>
        private static void CheckNumberColorsEdgeCube(int[] aNumberOfColors, int nColor)
        {
            // Up face
            if (aPieces[1] == aFaceColors[nColor])
            {
                aNumberOfColors[nColor]++;
            }

            if (aPieces[3] == aFaceColors[nColor])
            {
                aNumberOfColors[nColor]++;
            }

            if (aPieces[5] == aFaceColors[nColor])
            {
                aNumberOfColors[nColor]++;
            }

            if (aPieces[7] == aFaceColors[nColor])
            {
                aNumberOfColors[nColor]++;
            }

            // Right face
            if (aPieces[10] == aFaceColors[nColor])
            {
                aNumberOfColors[nColor]++;
            }

            if (aPieces[12] == aFaceColors[nColor])
            {
                aNumberOfColors[nColor]++;
            }

            if (aPieces[14] == aFaceColors[nColor])
            {
                aNumberOfColors[nColor]++;
            }

            if (aPieces[16] == aFaceColors[nColor])
            {
                aNumberOfColors[nColor]++;
            }

            // Back face
            if (aPieces[19] == aFaceColors[nColor])
            {
                aNumberOfColors[nColor]++;
            }

            if (aPieces[21] == aFaceColors[nColor])
            {
                aNumberOfColors[nColor]++;
            }

            if (aPieces[23] == aFaceColors[nColor])
            {
                aNumberOfColors[nColor]++;
            }

            if (aPieces[25] == aFaceColors[nColor])
            {
                aNumberOfColors[nColor]++;
            }

            // Left face
            if (aPieces[28] == aFaceColors[nColor])
            {
                aNumberOfColors[nColor]++;
            }

            if (aPieces[30] == aFaceColors[nColor])
            {
                aNumberOfColors[nColor]++;
            }

            if (aPieces[32] == aFaceColors[nColor])
            {
                aNumberOfColors[nColor]++;
            }

            if (aPieces[34] == aFaceColors[nColor])
            {
                aNumberOfColors[nColor]++;
            }

            // Up face
            if (aPieces[37] == aFaceColors[nColor])
            {
                aNumberOfColors[nColor]++;
            }

            if (aPieces[39] == aFaceColors[nColor])
            {
                aNumberOfColors[nColor]++;
            }

            if (aPieces[41] == aFaceColors[nColor])
            {
                aNumberOfColors[nColor]++;
            }

            if (aPieces[43] == aFaceColors[nColor])
            {
                aNumberOfColors[nColor]++;
            }

            // Down face
            if (aPieces[46] == aFaceColors[nColor])
            {
                aNumberOfColors[nColor]++;
            }

            if (aPieces[48] == aFaceColors[nColor])
            {
                aNumberOfColors[nColor]++;
            }

            if (aPieces[50] == aFaceColors[nColor])
            {
                aNumberOfColors[nColor]++;
            }

            if (aPieces[52] == aFaceColors[nColor])
            {
                aNumberOfColors[nColor]++;
            }
        }

        /// <summary>
        /// Check if the cube is solved
        /// </summary>
        /// <returns></returns>
        public static bool CheckIfSolved()
        {
            bool bColorsUp = false;
            bool bColorsFront = false;
            bool bColorsRight = false;
            bool bColorsLeft = false;
            bool bColorsBack = false;
            bool bColorsDown = false;

            if (aPieces[36] == aPieces[37] && aPieces[36] == aPieces[38] && aPieces[36] == aPieces[39] && aPieces[36] == aPieces[40] && aPieces[36] == aPieces[41] && aPieces[36] == aPieces[42] && aPieces[36] == aPieces[43] && aPieces[36] == aPieces[44])
            {
                bColorsUp = true;
            }

            if (aPieces[0] == aPieces[1] && aPieces[0] == aPieces[2] && aPieces[0] == aPieces[3] && aPieces[0] == aPieces[4] && aPieces[0] == aPieces[5] && aPieces[0] == aPieces[6] && aPieces[0] == aPieces[7] && aPieces[0] == aPieces[8])
            {
                bColorsFront = true;
            }

            if (aPieces[9] == aPieces[10] && aPieces[9] == aPieces[11] && aPieces[9] == aPieces[12] && aPieces[9] == aPieces[13] && aPieces[9] == aPieces[14] && aPieces[9] == aPieces[15] && aPieces[9] == aPieces[16] && aPieces[9] == aPieces[17])
            {
                bColorsRight = true;
            }

            if (aPieces[27] == aPieces[28] && aPieces[27] == aPieces[29] && aPieces[27] == aPieces[30] && aPieces[27] == aPieces[31] && aPieces[27] == aPieces[32] && aPieces[27] == aPieces[33] && aPieces[27] == aPieces[34] && aPieces[27] == aPieces[35])
            {
                bColorsLeft = true;
            }

            if (aPieces[18] == aPieces[19] && aPieces[18] == aPieces[20] && aPieces[18] == aPieces[21] && aPieces[18] == aPieces[22] && aPieces[18] == aPieces[23] && aPieces[18] == aPieces[24] && aPieces[18] == aPieces[25] && aPieces[18] == aPieces[26])
            {
                bColorsBack = true;
            }

            if (aPieces[45] == aPieces[46] && aPieces[45] == aPieces[47] && aPieces[45] == aPieces[48] && aPieces[45] == aPieces[49] && aPieces[45] == aPieces[50] && aPieces[45] == aPieces[51] && aPieces[45] == aPieces[52] && aPieces[45] == aPieces[53])
            {
                bColorsDown = true;
            }

            if (!bColorsUp || !bColorsFront || !bColorsRight || !bColorsLeft || !bColorsBack || !bColorsDown)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Reset the colors of the cube
        /// </summary>
        public static void ResetCube()
        {
            int nItem;

            for (nItem = 0; nItem < 9; nItem++)
            {
                aPieces[nItem] = aFaceColors[1];
            }

            for (nItem = 9; nItem < 18; nItem++)
            {
                aPieces[nItem] = aFaceColors[2];
            }

            for (nItem = 18; nItem < 27; nItem++)
            {
                aPieces[nItem] = aFaceColors[3];
            }

            for (nItem = 27; nItem < 36; nItem++)
            {
                aPieces[nItem] = aFaceColors[4];
            }

            for (nItem = 36; nItem < 45; nItem++)
            {
                aPieces[nItem] = aFaceColors[5];
            }

            for (nItem = 45; nItem < 54; nItem++)
            {
                aPieces[nItem] = aFaceColors[6];
            }
        }
    }
}
