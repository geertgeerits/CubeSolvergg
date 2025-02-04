using static CubeSolver.Globals;

namespace CubeSolver
{
    internal sealed class ClassCubeTurns
    {
        /// <summary>
        /// Turn the layers of the cube (CW = Clockwise, CCW = Counter clockwise, 2 = two quarter turns or 1 half turn)
        /// </summary>
        /// <param name="cTurn"></param>
        /// <returns></returns>
        public static async Task<bool> TurnCubeLayersAsync(string cTurn)
        {
            switch (cTurn)
            {
                // Face rotations
                case turnFrontCW:
                    TurnFrontFaceTo("CW");
                    break;
                case turnFrontCCW:
                    TurnFrontFaceTo("CCW");
                    break;
                case turnFront2:
                    TurnFrontFaceTo("CW");
                    TurnFrontFaceTo("CW");
                    break;
                case turnRightCW:
                    TurnRightFaceTo("CW");
                    break;
                case turnRightCCW:
                    TurnRightFaceTo("CCW");
                    break;
                case turnRight2:
                    TurnRightFaceTo("CW");
                    TurnRightFaceTo("CW");
                    break;
                case turnBackCW:
                    TurnBackFaceTo("CW");
                    break;
                case turnBackCCW:
                    TurnBackFaceTo("CCW");
                    break;
                case turnBack2:
                    TurnBackFaceTo("CW");
                    TurnBackFaceTo("CW");
                    break;
                case turnLeftCW:
                    TurnLeftFaceTo("CW");
                    break;
                case turnLeftCCW:
                    TurnLeftFaceTo("CCW");
                    break;
                case turnLeft2:
                    TurnLeftFaceTo("CW");
                    TurnLeftFaceTo("CW");
                    break;
                case turnUpCW:
                    TurnUpFaceTo("CW");
                    break;
                case turnUpCCW:
                    TurnUpFaceTo("CCW");
                    break;
                case turnUp2:
                    TurnUpFaceTo("CW");
                    TurnUpFaceTo("CW");
                    break;
                case turnDownCW:
                    TurnDownFaceTo("CW");
                    break;
                case turnDownCCW:
                    TurnDownFaceTo("CCW");
                    break;
                case turnDown2:
                    TurnDownFaceTo("CW");
                    TurnDownFaceTo("CW");
                    break;

                // Middle layer rotations
                case turnUpHorMiddleRight:
                    TurnUpHorMiddleTo("CW");
                    break;
                case turnUpHorMiddleLeft:
                    TurnUpHorMiddleTo("CCW");
                    break;
                case turnUpHorMiddle2:
                    TurnUpHorMiddleTo("CW");
                    TurnUpHorMiddleTo("CW");
                    break;

                case turnUpVerMiddleBack:
                    TurnUpVerMiddleTo("CW");
                    break;
                case turnUpVerMiddleFront:
                    TurnUpVerMiddleTo("CCW");
                    break;
                case turnUpVerMiddle2:
                    TurnUpVerMiddleTo("CW");
                    TurnUpVerMiddleTo("CW");
                    break;

                case turnFrontHorMiddleLeft:
                    TurnFrontHorMiddleTo("CW");
                    break;
                case turnFrontHorMiddleRight:
                    TurnFrontHorMiddleTo("CCW");
                    break;
                case turnFrontHorMiddle2:
                    TurnFrontHorMiddleTo("CW");
                    TurnFrontHorMiddleTo("CW");
                    break;

                // Two layers at the same time
                case turn2LayersFrontCW:
                    TurnFrontFaceTo("CW");
                    TurnUpHorMiddleTo("CW");
                    break;
                case turn2LayersFrontCCW:
                    TurnFrontFaceTo("CCW");
                    TurnUpHorMiddleTo("CCW");
                    break;
                case turn2LayersFront2:
                    TurnFrontFaceTo("CW");
                    TurnUpHorMiddleTo("CW");
                    TurnFrontFaceTo("CW");
                    TurnUpHorMiddleTo("CW");
                    break;

                case turn2LayersRightCW:
                    TurnRightFaceTo("CW");
                    TurnUpVerMiddleTo("CW");
                    break;
                case turn2LayersRightCCW:
                    TurnRightFaceTo("CCW");
                    TurnUpVerMiddleTo("CCW");
                    break;
                case turn2LayersRight2:
                    TurnRightFaceTo("CW");
                    TurnUpVerMiddleTo("CW");
                    TurnRightFaceTo("CW");
                    TurnUpVerMiddleTo("CW");
                    break;

                case turn2LayersBackCW:
                    TurnBackFaceTo("CW");
                    TurnUpHorMiddleTo("CCW");
                    break;
                case turn2LayersBackCCW:
                    TurnBackFaceTo("CCW");
                    TurnUpHorMiddleTo("CW");
                    break;
                case turn2LayersBack2:
                    TurnBackFaceTo("CW");
                    TurnUpHorMiddleTo("CCW");
                    TurnBackFaceTo("CW");
                    TurnUpHorMiddleTo("CCW");
                    break;

                case turn2LayersLeftCW:
                    TurnLeftFaceTo("CW");
                    TurnUpVerMiddleTo("CCW");
                    break;
                case turn2LayersLeftCCW:
                    TurnLeftFaceTo("CCW");
                    TurnUpVerMiddleTo("CW");
                    break;
                case turn2LayersLeft2:
                    TurnLeftFaceTo("CW");
                    TurnUpVerMiddleTo("CCW");
                    TurnLeftFaceTo("CW");
                    TurnUpVerMiddleTo("CCW");
                    break;

                case turn2LayersUpCW:
                    TurnUpFaceTo("CW");
                    TurnFrontHorMiddleTo("CW");
                    break;
                case turn2LayersUpCCW:
                    TurnUpFaceTo("CCW");
                    TurnFrontHorMiddleTo("CCW");
                    break;
                case turn2LayersUp2:
                    TurnUpFaceTo("CW");
                    TurnFrontHorMiddleTo("CW");
                    TurnUpFaceTo("CW");
                    TurnFrontHorMiddleTo("CW");
                    break;

                case turn2LayersDownCW:
                    TurnDownFaceTo("CW");
                    TurnFrontHorMiddleTo("CCW");
                    break;
                case turn2LayersDownCCW:
                    TurnDownFaceTo("CCW");
                    TurnFrontHorMiddleTo("CW");
                    break;
                case turn2LayersDown2:
                    TurnDownFaceTo("CW");
                    TurnFrontHorMiddleTo("CCW");
                    TurnDownFaceTo("CW");
                    TurnFrontHorMiddleTo("CCW");
                    break;

                // Cube rotations
                case turnCubeFrontToRight:
                    TurnCubeFrontFaceToRightFace();
                    break;
                case turnCubeFrontToLeft:
                    TurnCubeFrontFaceToLeftFace();
                    break;
                case turnCubeFrontToLeft2:
                    TurnCubeFrontFaceToLeftFace();
                    TurnCubeFrontFaceToLeftFace();
                    break;
                case turnCubeFrontToUp:
                    TurnCubeFrontFaceToUpFace();
                    break;
                case turnCubeFrontToUp2:
                    TurnCubeFrontFaceToUpFace();
                    TurnCubeFrontFaceToUpFace();
                    break;
                case turnCubeFrontToDown:
                    TurnCubeFrontFaceToDownFace();
                    break;
                case turnCubeUpToRight:
                    TurnCubeUpFaceToRightFace();
                    break;
                case turnCubeUpToRight2:
                    TurnCubeUpFaceToRightFace();
                    TurnCubeUpFaceToRightFace();
                    break;
                case turnCubeUpToLeft:
                    TurnCubeUpFaceToLeftFace();
                    break;

                default:
                    await Application.Current!.Windows[0].Page!.DisplayAlert(CubeLang.ErrorTitle_Text, $"TurnCubeLayersAsync\ncTurn not found:\n{cTurn}", CubeLang.ButtonClose_Text);
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Turn the layers of the cube in reverse order
        /// </summary>
        /// <param name="cTurn"></param>
        /// <returns></returns>
        public static async Task<bool> TurnCubeLayersReversedAsync(string cTurn)
        {
            switch (cTurn)
            {
                // Face rotations
                case turnFrontCW:
                    TurnFrontFaceTo("CCW");
                    break;
                case turnFrontCCW:
                    TurnFrontFaceTo("CW");
                    break;
                case turnFront2:
                    TurnFrontFaceTo("CW");
                    TurnFrontFaceTo("CW");
                    break;
                case turnRightCW:
                    TurnRightFaceTo("CCW");
                    break;
                case turnRightCCW:
                    TurnRightFaceTo("CW");
                    break;
                case turnRight2:
                    TurnRightFaceTo("CW");
                    TurnRightFaceTo("CW");
                    break;
                case turnBackCW:
                    TurnBackFaceTo("CCW");
                    break;
                case turnBackCCW:
                    TurnBackFaceTo("CW");
                    break;
                case turnBack2:
                    TurnBackFaceTo("CW");
                    TurnBackFaceTo("CW");
                    break;
                case turnLeftCW:
                    TurnLeftFaceTo("CCW");
                    break;
                case turnLeftCCW:
                    TurnLeftFaceTo("CW");
                    break;
                case turnLeft2:
                    TurnLeftFaceTo("CW");
                    TurnLeftFaceTo("CW");
                    break;
                case turnUpCW:
                    TurnUpFaceTo("CCW");
                    break;
                case turnUpCCW:
                    TurnUpFaceTo("CW");
                    break;
                case turnUp2:
                    TurnUpFaceTo("CW");
                    TurnUpFaceTo("CW");
                    break;
                case turnDownCW:
                    TurnDownFaceTo("CCW");
                    break;
                case turnDownCCW:
                    TurnDownFaceTo("CW");
                    break;
                case turnDown2:
                    TurnDownFaceTo("CW");
                    TurnDownFaceTo("CW");
                    break;

                // Middle layer rotations
                case turnUpHorMiddleRight:
                    TurnUpHorMiddleTo("CCW");
                    break;
                case turnUpHorMiddleLeft:
                    TurnUpHorMiddleTo("CW");
                    break;
                case turnUpHorMiddle2:
                    TurnUpHorMiddleTo("CW");
                    TurnUpHorMiddleTo("CW");
                    break;

                case turnUpVerMiddleBack:
                    TurnUpVerMiddleTo("CCW");
                    break;
                case turnUpVerMiddleFront:
                    TurnUpVerMiddleTo("CW");
                    break;
                case turnUpVerMiddle2:
                    TurnUpVerMiddleTo("CW");
                    TurnUpVerMiddleTo("CW");
                    break;

                case turnFrontHorMiddleLeft:
                    TurnFrontHorMiddleTo("CCW");
                    break;
                case turnFrontHorMiddleRight:
                    TurnFrontHorMiddleTo("CW");
                    break;
                case turnFrontHorMiddle2:
                    TurnFrontHorMiddleTo("CW");
                    TurnFrontHorMiddleTo("CW");
                    break;

                // Two layers at the same time
                case turn2LayersFrontCW:
                    TurnFrontFaceTo("CCW");
                    TurnUpHorMiddleTo("CCW");
                    break;
                case turn2LayersFrontCCW:
                    TurnFrontFaceTo("CW");
                    TurnUpHorMiddleTo("CW");
                    break;
                case turn2LayersFront2:
                    TurnFrontFaceTo("CW");
                    TurnUpHorMiddleTo("CW");
                    TurnFrontFaceTo("CW");
                    TurnUpHorMiddleTo("CW");
                    break;

                case turn2LayersRightCW:
                    TurnRightFaceTo("CCW");
                    TurnUpVerMiddleTo("CCW");
                    break;
                case turn2LayersRightCCW:
                    TurnRightFaceTo("CW");
                    TurnUpVerMiddleTo("CW");
                    break;
                case turn2LayersRight2:
                    TurnRightFaceTo("CW");
                    TurnUpVerMiddleTo("CW");
                    TurnRightFaceTo("CW");
                    TurnUpVerMiddleTo("CW");
                    break;

                case turn2LayersBackCW:
                    TurnBackFaceTo("CCW");
                    TurnUpHorMiddleTo("CW");
                    break;
                case turn2LayersBackCCW:
                    TurnBackFaceTo("CW");
                    TurnUpHorMiddleTo("CCW");
                    break;
                case turn2LayersBack2:
                    TurnBackFaceTo("CW");
                    TurnUpHorMiddleTo("CCW");
                    TurnBackFaceTo("CW");
                    TurnUpHorMiddleTo("CCW");
                    break;

                case turn2LayersLeftCW:
                    TurnLeftFaceTo("CCW");
                    TurnUpVerMiddleTo("CW");
                    break;
                case turn2LayersLeftCCW:
                    TurnLeftFaceTo("CW");
                    TurnUpVerMiddleTo("CCW");
                    break;
                case turn2LayersLeft2:
                    TurnLeftFaceTo("CW");
                    TurnUpVerMiddleTo("CCW");
                    TurnLeftFaceTo("CW");
                    TurnUpVerMiddleTo("CCW");
                    break;

                case turn2LayersUpCW:
                    TurnUpFaceTo("CCW");
                    TurnFrontHorMiddleTo("CCW");
                    break;
                case turn2LayersUpCCW:
                    TurnUpFaceTo("CW");
                    TurnFrontHorMiddleTo("CW");
                    break;
                case turn2LayersUp2:
                    TurnUpFaceTo("CW");
                    TurnFrontHorMiddleTo("CW");
                    TurnUpFaceTo("CW");
                    TurnFrontHorMiddleTo("CW");
                    break;

                case turn2LayersDownCW:
                    TurnDownFaceTo("CCW");
                    TurnFrontHorMiddleTo("CW");
                    break;
                case turn2LayersDownCCW:
                    TurnDownFaceTo("CW");
                    TurnFrontHorMiddleTo("CCW");
                    break;
                case turn2LayersDown2:
                    TurnDownFaceTo("CW");
                    TurnFrontHorMiddleTo("CCW");
                    TurnDownFaceTo("CW");
                    TurnFrontHorMiddleTo("CCW");
                    break;

                // Cube rotations
                case turnCubeFrontToRight:
                    TurnCubeFrontFaceToLeftFace();
                    break;
                case turnCubeFrontToLeft:
                    TurnCubeFrontFaceToRightFace();
                    break;
                case turnCubeFrontToLeft2:
                    TurnCubeFrontFaceToLeftFace();
                    TurnCubeFrontFaceToLeftFace();
                    break;
                case turnCubeFrontToUp:
                    TurnCubeFrontFaceToDownFace();
                    break;
                case turnCubeFrontToUp2:
                    TurnCubeFrontFaceToUpFace();
                    TurnCubeFrontFaceToUpFace();
                    break;
                case turnCubeFrontToDown:
                    TurnCubeFrontFaceToUpFace();
                    break;
                case turnCubeUpToRight:
                    TurnCubeUpFaceToLeftFace();
                    break;
                case turnCubeUpToRight2:
                    TurnCubeUpFaceToRightFace();
                    TurnCubeUpFaceToRightFace();
                    break;
                case turnCubeUpToLeft:
                    TurnCubeUpFaceToRightFace();
                    break;

                default:
                    await Application.Current!.Windows[0].Page!.DisplayAlert(CubeLang.ErrorTitle_Text, $"TurnCubeLayersAsync\ncTurn not found:\n{cTurn}", CubeLang.ButtonClose_Text);
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Rotate the entire cube a quarter turn so that the front goes to the left face
        /// </summary>
        private static void TurnCubeFrontFaceToLeftFace()
        {
            TurnUpFaceTo("CW");
            TurnFrontHorMiddleTo("CW");
            TurnDownFaceTo("CCW");
        }

        /// <summary>
        /// Rotate the entire cube a quarter turn so that the front goes to the right face 
        /// </summary>
        private static void TurnCubeFrontFaceToRightFace()
        {
            TurnUpFaceTo("CCW");
            TurnFrontHorMiddleTo("CCW");
            TurnDownFaceTo("CW");
        }

        /// <summary>
        /// Rotate the entire cube a quarter turn so that the front goes to the upper face 
        /// </summary>
        private static void TurnCubeFrontFaceToUpFace()
        {
            TurnRightFaceTo("CW");
            TurnUpVerMiddleTo("CW");
            TurnLeftFaceTo("CCW");
        }

        /// <summary>
        /// Rotate the entire cube a quarter turn so that the front goes to the down face 
        /// </summary>
        private static void TurnCubeFrontFaceToDownFace()
        {
            TurnRightFaceTo("CCW");
            TurnUpVerMiddleTo("CCW");
            TurnLeftFaceTo("CW");
        }

        /// <summary>
        /// Rotate the entire cube a quarter turn so that the upper face goes to the right face 
        /// </summary>
        private static void TurnCubeUpFaceToRightFace()
        {
            TurnFrontFaceTo("CW");
            TurnUpHorMiddleTo("CW");
            TurnBackFaceTo("CCW");
        }

        /// <summary>
        /// Rotate the entire cube a quarter turn so that the upper face goes to the left face 
        /// </summary>
        private static void TurnCubeUpFaceToLeftFace()
        {
            TurnFrontFaceTo("CCW");
            TurnUpHorMiddleTo("CCW");
            TurnBackFaceTo("CW");
        }

        /// <summary>
        /// Turn the entire front face clockwise or counter clockwise
        /// </summary>
        /// <param name="cDirection"></param>
        public static void TurnFrontFaceTo(string cDirection)
        {
            // In this case are regular arrays faster than span arrays

            // Create and start a stopwatch instance
            //long startTime = Stopwatch.GetTimestamp();

            // Create a span for the arrays
            //Span<string> aPiecesSpan = aPieces.AsSpan();
            //Span<string> aPiecesTempSpan = aPiecesTemp.AsSpan();

            // Copy the pieces to the temporary array
            //aPiecesSpan[..54].CopyTo(aPiecesTempSpan);
            Array.Copy(aPieces, aPiecesTemp, 54);

            if (cDirection == "CW")
            {
                aPieces[0] = aPiecesTemp[6];
                aPieces[1] = aPiecesTemp[3];
                aPieces[2] = aPiecesTemp[0];
                aPieces[3] = aPiecesTemp[7];
                aPieces[5] = aPiecesTemp[1];
                aPieces[6] = aPiecesTemp[8];
                aPieces[7] = aPiecesTemp[5];
                aPieces[8] = aPiecesTemp[2];

                aPieces[42] = aPiecesTemp[35];
                aPieces[43] = aPiecesTemp[32];
                aPieces[44] = aPiecesTemp[29];

                aPieces[9] = aPiecesTemp[42];
                aPieces[12] = aPiecesTemp[43];
                aPieces[15] = aPiecesTemp[44];

                aPieces[45] = aPiecesTemp[15];
                aPieces[46] = aPiecesTemp[12];
                aPieces[47] = aPiecesTemp[9];

                aPieces[29] = aPiecesTemp[45];
                aPieces[32] = aPiecesTemp[46];
                aPieces[35] = aPiecesTemp[47];
            }

            if (cDirection == "CCW")
            {
                aPieces[0] = aPiecesTemp[2];
                aPieces[1] = aPiecesTemp[5];
                aPieces[2] = aPiecesTemp[8];
                aPieces[3] = aPiecesTemp[1];
                aPieces[5] = aPiecesTemp[7];
                aPieces[6] = aPiecesTemp[0];
                aPieces[7] = aPiecesTemp[3];
                aPieces[8] = aPiecesTemp[6];

                aPieces[42] = aPiecesTemp[9];
                aPieces[43] = aPiecesTemp[12];
                aPieces[44] = aPiecesTemp[15];

                aPieces[9] = aPiecesTemp[47];
                aPieces[12] = aPiecesTemp[46];
                aPieces[15] = aPiecesTemp[45];

                aPieces[45] = aPiecesTemp[29];
                aPieces[46] = aPiecesTemp[32];
                aPieces[47] = aPiecesTemp[35];

                aPieces[29] = aPiecesTemp[44];
                aPieces[32] = aPiecesTemp[43];
                aPieces[35] = aPiecesTemp[42];
            }

            // Stop the stopwatch and get the elapsed time
            //TimeSpan delta = Stopwatch.GetElapsedTime(startTime);
            //_ = Application.Current!.Windows[0].Page!.DisplayAlert("TurnFrontFaceTo", $"Time elapsed (hh:mm:ss.xxxxxxx): {delta}", "OK");
        }

        /// <summary>
        /// Turn the top horizontal middle layer to the right or left
        /// </summary>
        /// <param name="cDirection"></param>
        public static void TurnUpHorMiddleTo(string cDirection)
        {
            // Copy the pieces to the temporary array
            Array.Copy(aPieces, aPiecesTemp, 54);

            if (cDirection == "CW")
            {
                aPieces[39] = aPiecesTemp[34];
                aPieces[40] = aPiecesTemp[31];
                aPieces[41] = aPiecesTemp[28];

                aPieces[10] = aPiecesTemp[39];
                aPieces[13] = aPiecesTemp[40];
                aPieces[16] = aPiecesTemp[41];

                aPieces[48] = aPiecesTemp[16];
                aPieces[49] = aPiecesTemp[13];
                aPieces[50] = aPiecesTemp[10];

                aPieces[28] = aPiecesTemp[48];
                aPieces[31] = aPiecesTemp[49];
                aPieces[34] = aPiecesTemp[50];
            }

            if (cDirection == "CCW")
            {
                aPieces[39] = aPiecesTemp[10];
                aPieces[40] = aPiecesTemp[13];
                aPieces[41] = aPiecesTemp[16];

                aPieces[10] = aPiecesTemp[50];
                aPieces[13] = aPiecesTemp[49];
                aPieces[16] = aPiecesTemp[48];

                aPieces[48] = aPiecesTemp[28];
                aPieces[49] = aPiecesTemp[31];
                aPieces[50] = aPiecesTemp[34];

                aPieces[28] = aPiecesTemp[41];
                aPieces[31] = aPiecesTemp[40];
                aPieces[34] = aPiecesTemp[39];
            }
        }

        /// <summary>
        /// Turn the entire back face clockwise or counter clockwise
        /// </summary>
        /// <param name="cDirection"></param>
        public static void TurnBackFaceTo(string cDirection)
        {
            // Copy the pieces to the temporary array
            Array.Copy(aPieces, aPiecesTemp, 54);

            if (cDirection == "CW")
            {
                aPieces[18] = aPiecesTemp[24];
                aPieces[19] = aPiecesTemp[21];
                aPieces[20] = aPiecesTemp[18];
                aPieces[21] = aPiecesTemp[25];
                aPieces[23] = aPiecesTemp[19];
                aPieces[24] = aPiecesTemp[26];
                aPieces[25] = aPiecesTemp[23];
                aPieces[26] = aPiecesTemp[20];

                aPieces[36] = aPiecesTemp[11];
                aPieces[37] = aPiecesTemp[14];
                aPieces[38] = aPiecesTemp[17];

                aPieces[11] = aPiecesTemp[53];
                aPieces[14] = aPiecesTemp[52];
                aPieces[17] = aPiecesTemp[51];

                aPieces[51] = aPiecesTemp[27];
                aPieces[52] = aPiecesTemp[30];
                aPieces[53] = aPiecesTemp[33];

                aPieces[27] = aPiecesTemp[38];
                aPieces[30] = aPiecesTemp[37];
                aPieces[33] = aPiecesTemp[36];
            }

            if (cDirection == "CCW")
            {
                aPieces[18] = aPiecesTemp[20];
                aPieces[19] = aPiecesTemp[23];
                aPieces[20] = aPiecesTemp[26];
                aPieces[21] = aPiecesTemp[19];
                aPieces[23] = aPiecesTemp[25];
                aPieces[24] = aPiecesTemp[18];
                aPieces[25] = aPiecesTemp[21];
                aPieces[26] = aPiecesTemp[24];

                aPieces[36] = aPiecesTemp[33];
                aPieces[37] = aPiecesTemp[30];
                aPieces[38] = aPiecesTemp[27];

                aPieces[11] = aPiecesTemp[36];
                aPieces[14] = aPiecesTemp[37];
                aPieces[17] = aPiecesTemp[38];

                aPieces[51] = aPiecesTemp[17];
                aPieces[52] = aPiecesTemp[14];
                aPieces[53] = aPiecesTemp[11];

                aPieces[27] = aPiecesTemp[51];
                aPieces[30] = aPiecesTemp[52];
                aPieces[33] = aPiecesTemp[53];
            }
        }

        /// <summary>
        /// Turn the entire left face clockwise or counter clockwise
        /// </summary>
        /// <param name="cDirection"></param>
        public static void TurnLeftFaceTo(string cDirection)
        {
            // Copy the pieces to the temporary array
            Array.Copy(aPieces, aPiecesTemp, 54);

            if (cDirection == "CW")
            {
                aPieces[27] = aPiecesTemp[33];
                aPieces[28] = aPiecesTemp[30];
                aPieces[29] = aPiecesTemp[27];
                aPieces[30] = aPiecesTemp[34];
                aPieces[32] = aPiecesTemp[28];
                aPieces[33] = aPiecesTemp[35];
                aPieces[34] = aPiecesTemp[32];
                aPieces[35] = aPiecesTemp[29];

                aPieces[36] = aPiecesTemp[26];
                aPieces[39] = aPiecesTemp[23];
                aPieces[42] = aPiecesTemp[20];

                aPieces[0] = aPiecesTemp[36];
                aPieces[3] = aPiecesTemp[39];
                aPieces[6] = aPiecesTemp[42];

                aPieces[45] = aPiecesTemp[0];
                aPieces[48] = aPiecesTemp[3];
                aPieces[51] = aPiecesTemp[6];

                aPieces[20] = aPiecesTemp[51];
                aPieces[23] = aPiecesTemp[48];
                aPieces[26] = aPiecesTemp[45];
            }

            if (cDirection == "CCW")
            {
                aPieces[27] = aPiecesTemp[29];
                aPieces[28] = aPiecesTemp[32];
                aPieces[29] = aPiecesTemp[35];
                aPieces[30] = aPiecesTemp[28];
                aPieces[32] = aPiecesTemp[34];
                aPieces[33] = aPiecesTemp[27];
                aPieces[34] = aPiecesTemp[30];
                aPieces[35] = aPiecesTemp[33];

                aPieces[36] = aPiecesTemp[0];
                aPieces[39] = aPiecesTemp[3];
                aPieces[42] = aPiecesTemp[6];

                aPieces[0] = aPiecesTemp[45];
                aPieces[3] = aPiecesTemp[48];
                aPieces[6] = aPiecesTemp[51];

                aPieces[45] = aPiecesTemp[26];
                aPieces[48] = aPiecesTemp[23];
                aPieces[51] = aPiecesTemp[20];

                aPieces[20] = aPiecesTemp[42];
                aPieces[23] = aPiecesTemp[39];
                aPieces[26] = aPiecesTemp[36];
            }
        }

        /// <summary>
        /// Turn the top vertical middle layer to back or front
        /// </summary>
        /// <param name="cDirection"></param>
        public static void TurnUpVerMiddleTo(string cDirection)
        {
            // Copy the pieces to the temporary array
            Array.Copy(aPieces, aPiecesTemp, 54);

            if (cDirection == "CW")
            {
                aPieces[37] = aPiecesTemp[1];
                aPieces[40] = aPiecesTemp[4];
                aPieces[43] = aPiecesTemp[7];

                aPieces[1] = aPiecesTemp[46];
                aPieces[4] = aPiecesTemp[49];
                aPieces[7] = aPiecesTemp[52];

                aPieces[46] = aPiecesTemp[25];
                aPieces[49] = aPiecesTemp[22];
                aPieces[52] = aPiecesTemp[19];

                aPieces[19] = aPiecesTemp[43];
                aPieces[22] = aPiecesTemp[40];
                aPieces[25] = aPiecesTemp[37];
            }

            if (cDirection == "CCW")
            {
                aPieces[37] = aPiecesTemp[25];
                aPieces[40] = aPiecesTemp[22];
                aPieces[43] = aPiecesTemp[19];

                aPieces[1] = aPiecesTemp[37];
                aPieces[4] = aPiecesTemp[40];
                aPieces[7] = aPiecesTemp[43];

                aPieces[46] = aPiecesTemp[1];
                aPieces[49] = aPiecesTemp[4];
                aPieces[52] = aPiecesTemp[7];

                aPieces[19] = aPiecesTemp[52];
                aPieces[22] = aPiecesTemp[49];
                aPieces[25] = aPiecesTemp[46];
            }
        }

        /// <summary>
        /// Turn the entire right face clockwise or counter clockwise
        /// </summary>
        /// <param name="cDirection"></param>
        public static void TurnRightFaceTo(string cDirection)
        {
            // Copy the pieces to the temporary array
            Array.Copy(aPieces, aPiecesTemp, 54);

            if (cDirection == "CW")
            {
                aPieces[9] = aPiecesTemp[15];
                aPieces[10] = aPiecesTemp[12];
                aPieces[11] = aPiecesTemp[9];
                aPieces[12] = aPiecesTemp[16];
                aPieces[14] = aPiecesTemp[10];
                aPieces[15] = aPiecesTemp[17];
                aPieces[16] = aPiecesTemp[14];
                aPieces[17] = aPiecesTemp[11];

                aPieces[38] = aPiecesTemp[2];
                aPieces[41] = aPiecesTemp[5];
                aPieces[44] = aPiecesTemp[8];

                aPieces[2] = aPiecesTemp[47];
                aPieces[5] = aPiecesTemp[50];
                aPieces[8] = aPiecesTemp[53];

                aPieces[47] = aPiecesTemp[24];
                aPieces[50] = aPiecesTemp[21];
                aPieces[53] = aPiecesTemp[18];

                aPieces[18] = aPiecesTemp[44];
                aPieces[21] = aPiecesTemp[41];
                aPieces[24] = aPiecesTemp[38];
            }

            if (cDirection == "CCW")
            {
                aPieces[9] = aPiecesTemp[11];
                aPieces[10] = aPiecesTemp[14];
                aPieces[11] = aPiecesTemp[17];
                aPieces[12] = aPiecesTemp[10];
                aPieces[14] = aPiecesTemp[16];
                aPieces[15] = aPiecesTemp[9];
                aPieces[16] = aPiecesTemp[12];
                aPieces[17] = aPiecesTemp[15];

                aPieces[38] = aPiecesTemp[24];
                aPieces[41] = aPiecesTemp[21];
                aPieces[44] = aPiecesTemp[18];

                aPieces[2] = aPiecesTemp[38];
                aPieces[5] = aPiecesTemp[41];
                aPieces[8] = aPiecesTemp[44];

                aPieces[47] = aPiecesTemp[2];
                aPieces[50] = aPiecesTemp[5];
                aPieces[53] = aPiecesTemp[8];

                aPieces[18] = aPiecesTemp[53];
                aPieces[21] = aPiecesTemp[50];
                aPieces[24] = aPiecesTemp[47];
            }
        }

        /// <summary>
        /// Turn the entire upper face clockwise or counter clockwise
        /// </summary>
        /// <param name="cDirection"></param>
        public static void TurnUpFaceTo(string cDirection)
        {
            // Copy the pieces to the temporary array
            Array.Copy(aPieces, aPiecesTemp, 54);

            if (cDirection == "CW")
            {
                aPieces[36] = aPiecesTemp[42];
                aPieces[37] = aPiecesTemp[39];
                aPieces[38] = aPiecesTemp[36];
                aPieces[39] = aPiecesTemp[43];
                aPieces[41] = aPiecesTemp[37];
                aPieces[42] = aPiecesTemp[44];
                aPieces[43] = aPiecesTemp[41];
                aPieces[44] = aPiecesTemp[38];

                aPieces[27] = aPiecesTemp[0];
                aPieces[28] = aPiecesTemp[1];
                aPieces[29] = aPiecesTemp[2];

                aPieces[0] = aPiecesTemp[9];
                aPieces[1] = aPiecesTemp[10];
                aPieces[2] = aPiecesTemp[11];

                aPieces[9] = aPiecesTemp[18];
                aPieces[10] = aPiecesTemp[19];
                aPieces[11] = aPiecesTemp[20];

                aPieces[18] = aPiecesTemp[27];
                aPieces[19] = aPiecesTemp[28];
                aPieces[20] = aPiecesTemp[29];
            }

            if (cDirection == "CCW")
            {
                aPieces[36] = aPiecesTemp[38];
                aPieces[37] = aPiecesTemp[41];
                aPieces[38] = aPiecesTemp[44];
                aPieces[39] = aPiecesTemp[37];
                aPieces[41] = aPiecesTemp[43];
                aPieces[42] = aPiecesTemp[36];
                aPieces[43] = aPiecesTemp[39];
                aPieces[44] = aPiecesTemp[42];

                aPieces[27] = aPiecesTemp[18];
                aPieces[28] = aPiecesTemp[19];
                aPieces[29] = aPiecesTemp[20];

                aPieces[0] = aPiecesTemp[27];
                aPieces[1] = aPiecesTemp[28];
                aPieces[2] = aPiecesTemp[29];

                aPieces[9] = aPiecesTemp[0];
                aPieces[10] = aPiecesTemp[1];
                aPieces[11] = aPiecesTemp[2];

                aPieces[18] = aPiecesTemp[9];
                aPieces[19] = aPiecesTemp[10];
                aPieces[20] = aPiecesTemp[11];
            }
        }

        /// <summary>
        /// Turn the front horizontal middle layer to right or left
        /// </summary>
        /// <param name="cDirection"></param>
        public static void TurnFrontHorMiddleTo(string cDirection)
        {
            // Copy the pieces to the temporary array
            Array.Copy(aPieces, aPiecesTemp, 54);

            if (cDirection == "CW")
            {
                aPieces[3] = aPiecesTemp[12];
                aPieces[4] = aPiecesTemp[13];
                aPieces[5] = aPiecesTemp[14];

                aPieces[12] = aPiecesTemp[21];
                aPieces[13] = aPiecesTemp[22];
                aPieces[14] = aPiecesTemp[23];

                aPieces[21] = aPiecesTemp[30];
                aPieces[22] = aPiecesTemp[31];
                aPieces[23] = aPiecesTemp[32];

                aPieces[30] = aPiecesTemp[3];
                aPieces[31] = aPiecesTemp[4];
                aPieces[32] = aPiecesTemp[5];
            }

            if (cDirection == "CCW")
            {
                aPieces[3] = aPiecesTemp[30];
                aPieces[4] = aPiecesTemp[31];
                aPieces[5] = aPiecesTemp[32];

                aPieces[12] = aPiecesTemp[3];
                aPieces[13] = aPiecesTemp[4];
                aPieces[14] = aPiecesTemp[5];

                aPieces[21] = aPiecesTemp[12];
                aPieces[22] = aPiecesTemp[13];
                aPieces[23] = aPiecesTemp[14];

                aPieces[30] = aPiecesTemp[21];
                aPieces[31] = aPiecesTemp[22];
                aPieces[32] = aPiecesTemp[23];
            }
        }

        /// <summary>
        /// Turn the entire down face clockwise or counter clockwise
        /// </summary>
        /// <param name="cDirection"></param>
        public static void TurnDownFaceTo(string cDirection)
        {
            // Copy the pieces to the temporary array
            Array.Copy(aPieces, aPiecesTemp, 54);

            if (cDirection == "CW")
            {
                aPieces[45] = aPiecesTemp[51];
                aPieces[46] = aPiecesTemp[48];
                aPieces[47] = aPiecesTemp[45];
                aPieces[48] = aPiecesTemp[52];
                aPieces[50] = aPiecesTemp[46];
                aPieces[51] = aPiecesTemp[53];
                aPieces[52] = aPiecesTemp[50];
                aPieces[53] = aPiecesTemp[47];

                aPieces[33] = aPiecesTemp[24];
                aPieces[34] = aPiecesTemp[25];
                aPieces[35] = aPiecesTemp[26];

                aPieces[6] = aPiecesTemp[33];
                aPieces[7] = aPiecesTemp[34];
                aPieces[8] = aPiecesTemp[35];

                aPieces[15] = aPiecesTemp[6];
                aPieces[16] = aPiecesTemp[7];
                aPieces[17] = aPiecesTemp[8];

                aPieces[24] = aPiecesTemp[15];
                aPieces[25] = aPiecesTemp[16];
                aPieces[26] = aPiecesTemp[17];
            }

            if (cDirection == "CCW")
            {
                aPieces[45] = aPiecesTemp[47];
                aPieces[46] = aPiecesTemp[50];
                aPieces[47] = aPiecesTemp[53];
                aPieces[48] = aPiecesTemp[46];
                aPieces[50] = aPiecesTemp[52];
                aPieces[51] = aPiecesTemp[45];
                aPieces[52] = aPiecesTemp[48];
                aPieces[53] = aPiecesTemp[51];

                aPieces[33] = aPiecesTemp[6];
                aPieces[34] = aPiecesTemp[7];
                aPieces[35] = aPiecesTemp[8];

                aPieces[6] = aPiecesTemp[15];
                aPieces[7] = aPiecesTemp[16];
                aPieces[8] = aPiecesTemp[17];

                aPieces[15] = aPiecesTemp[24];
                aPieces[16] = aPiecesTemp[25];
                aPieces[17] = aPiecesTemp[26];

                aPieces[24] = aPiecesTemp[33];
                aPieces[25] = aPiecesTemp[34];
                aPieces[26] = aPiecesTemp[35];
            }
        }
    }
}
