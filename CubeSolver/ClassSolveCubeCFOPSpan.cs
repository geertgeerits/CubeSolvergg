using System.Diagnostics;
using static CubeSolver.Globals;

namespace CubeSolver
{
    internal sealed class ClassSolveCubeCFOPSpan
    {
        //// Declare variables
        private const int nLoopTimesMax = 200;

        /// <summary>
        /// Solve the cube using the CFOP method
        /// </summary>
        /// <returns></returns>
        public static async Task<bool> SolveTheCubeCFOPAsync()
        {
            // Create and start a stopwatch instance
            //long startTime = Stopwatch.GetTimestamp();

            // Cross part 1 (Solving the first layer 4 edge pieces completely
            if (!await ClassSolveCubeCommon.SolveTopLayerEdgesAsync())
            {
                return false;
            }

            // Cross part 2
            if (!await ClassSolveCubeCommon.SolveTopLayerEdges2Async())
            {
                return false;
            }

            // F2L (Solving the first two layers completely)
            if (!await SolveFirstTwoLayers())
            {
                return false;
            }

            // OLL (Orientation of last layer) Bottom layer
            if (!await SolveBottomLayerOrientation())
            {
                return false;
            }

            // OLL (Permutation of last layer) Bottom layer
            if (!await SolveBottomLayerPermutation())
            {
                return false;
            }

            // Check if the cube is solved
            if (ClassColorsCube.CheckIfSolved())
            {
                // Stop the stopwatch and get the elapsed time
                //TimeSpan delta = Stopwatch.GetElapsedTime(startTime);
                //_ = Application.Current!.Windows[0].Page!.DisplayAlert("SolveTheCubeCFOPAsync", $"Time elapsed (hh:mm:ss.xxxxxxx): {delta}", "OK");

                return true;
            }

            return false;
        }

        /// <summary>
        /// Solve the first two layers (F2L)
        /// </summary>
        /// <returns></returns>
        private static Task<bool> SolveFirstTwoLayers()
        {
            //ClassProgramLogging.LogExecutedLine();

            // Create a span for the array aPieces
            ReadOnlySpan<string> aPiecesSpan = aPieces;

            string cT;
            int nLoopTimes = 0;

            while (true)
            {
                nLoopTimes++;
                if (nLoopTimes > nLoopTimesMax)
                {
                    Debug.WriteLine("CFOP: nLoopTimes first two layers: " + nLoopTimes);
                    return Task.FromResult(false);
                }

                // If solved, break the loop
                cT = aPiecesSpan[49];
                if (cT == aPiecesSpan[45] && cT == aPiecesSpan[46] && cT == aPiecesSpan[47] && cT == aPiecesSpan[48] && cT == aPiecesSpan[50] && cT == aPiecesSpan[51] && cT == aPiecesSpan[52] && cT == aPiecesSpan[53])
                {
                    cT = aPiecesSpan[4];
                    if (cT == aPiecesSpan[3] && cT == aPiecesSpan[5] && cT == aPiecesSpan[6] && cT == aPiecesSpan[7] && cT == aPiecesSpan[8])
                    {
                        cT = aPiecesSpan[13];
                        if (cT == aPiecesSpan[12] && cT == aPiecesSpan[14] && cT == aPiecesSpan[15] && cT == aPiecesSpan[16] && cT == aPiecesSpan[17])
                        {
                            cT = aPiecesSpan[22];
                            if (cT == aPiecesSpan[21] && cT == aPiecesSpan[23] && cT == aPiecesSpan[24] && cT == aPiecesSpan[25] && cT == aPiecesSpan[26])
                            {
                                cT = aPiecesSpan[31];
                                if (cT == aPiecesSpan[30] && cT == aPiecesSpan[32] && cT == aPiecesSpan[33] && cT == aPiecesSpan[34] && cT == aPiecesSpan[35])
                                {
                                    Debug.WriteLine("CFOP: number of turns first two layers: " + lCubeTurns.Count);
                                    break;
                                }
                            }
                        }
                    }
                }

                // Turn the cube
                if (nLoopTimes > 1)
                {
                    _ = MakeTurnAsync("y");
                }

                // When two adjacent faces (front and right) have been solved, turn the cube
                if (aPiecesSpan[49] == aPiecesSpan[46] && aPiecesSpan[49] == aPiecesSpan[47] && aPiecesSpan[49] == aPiecesSpan[50])
                {
                    if (aPiecesSpan[4] == aPiecesSpan[5] && aPiecesSpan[4] == aPiecesSpan[7] && aPiecesSpan[4] == aPiecesSpan[8])
                    {
                        if (aPiecesSpan[13] == aPiecesSpan[12] && aPiecesSpan[13] == aPiecesSpan[15] && aPiecesSpan[13] == aPiecesSpan[16])
                        {
                            _ = MakeTurnAsync("y");
                        }
                    }
                }

                // Bring the corner piece at the top to the right position on the top layer
                if (aPiecesSpan[49] == aPiecesSpan[11] || aPiecesSpan[49] == aPiecesSpan[18] || aPiecesSpan[49] == aPiecesSpan[38])
                {
                    if (aPiecesSpan[4] == aPiecesSpan[11] || aPiecesSpan[4] == aPiecesSpan[18] || aPiecesSpan[4] == aPiecesSpan[38])
                    {
                        if (aPiecesSpan[13] == aPiecesSpan[11] || aPiecesSpan[13] == aPiecesSpan[18] || aPiecesSpan[13] == aPiecesSpan[38])
                        {
                            _ = MakeTurnAsync("U");
                        }
                    }
                }

                if (aPiecesSpan[49] == aPiecesSpan[20] || aPiecesSpan[49] == aPiecesSpan[27] || aPiecesSpan[49] == aPiecesSpan[36])
                {
                    if (aPiecesSpan[4] == aPiecesSpan[20] || aPiecesSpan[4] == aPiecesSpan[27] || aPiecesSpan[4] == aPiecesSpan[36])
                    {
                        if (aPiecesSpan[13] == aPiecesSpan[20] || aPiecesSpan[13] == aPiecesSpan[27] || aPiecesSpan[13] == aPiecesSpan[36])
                        {
                            _ = MakeTurnAsync("U2");
                        }
                    }
                }

                if (aPiecesSpan[49] == aPiecesSpan[0] || aPiecesSpan[49] == aPiecesSpan[29] || aPiecesSpan[49] == aPiecesSpan[42])
                {
                    if (aPiecesSpan[4] == aPiecesSpan[0] || aPiecesSpan[4] == aPiecesSpan[29] || aPiecesSpan[4] == aPiecesSpan[42])
                    {
                        if (aPiecesSpan[13] == aPiecesSpan[0] || aPiecesSpan[13] == aPiecesSpan[29] || aPiecesSpan[13] == aPiecesSpan[42])
                        {
                            _ = MakeTurnAsync("U'");
                        }
                    }
                }

                //--------------------------------------------------------------------------------------------------------------

                // https://kubuspuzzel.nl/3x3-kubus-oplossen/f2l/
                // Split
                // Piece 4 = 3, 6, 7 and piece 13 = 14, 16, 17
                if (aPiecesSpan[4] == aPiecesSpan[3] && aPiecesSpan[4] == aPiecesSpan[6] && aPiecesSpan[4] == aPiecesSpan[7] && aPiecesSpan[13] == aPiecesSpan[14] && aPiecesSpan[13] == aPiecesSpan[16] && aPiecesSpan[13] == aPiecesSpan[17])
                {
                    // Situation 1
                    if (aPiecesSpan[49] == aPiecesSpan[44] && aPiecesSpan[4] == aPiecesSpan[9] && aPiecesSpan[4] == aPiecesSpan[19] && aPiecesSpan[13] == aPiecesSpan[2] && aPiecesSpan[13] == aPiecesSpan[37])
                    {
                        _ = MakeTurnAsync("U2 F' U' F");
                        continue;
                    }

                    // Situation 2
                    if (aPiecesSpan[49] == aPiecesSpan[2] && aPiecesSpan[4] == aPiecesSpan[37] && aPiecesSpan[4] == aPiecesSpan[44] && aPiecesSpan[13] == aPiecesSpan[9] && aPiecesSpan[13] == aPiecesSpan[19])
                    {
                        _ = MakeTurnAsync("U' R U R'");
                        continue;
                    }

                    // Situation 3.1
                    if (aPiecesSpan[49] == aPiecesSpan[2] && aPiecesSpan[4] == aPiecesSpan[28] && aPiecesSpan[4] == aPiecesSpan[44] && aPiecesSpan[13] == aPiecesSpan[9] && aPiecesSpan[13] == aPiecesSpan[39])
                    {
                        _ = MakeTurnAsync("F' U F");
                        continue;
                    }

                    // Situation 3.2
                    if (aPiecesSpan[49] == aPiecesSpan[2] && aPiecesSpan[4] == aPiecesSpan[19] && aPiecesSpan[4] == aPiecesSpan[44] && aPiecesSpan[13] == aPiecesSpan[9] && aPiecesSpan[13] == aPiecesSpan[37])
                    {
                        _ = MakeTurnAsync("U' R U");
                        continue;
                    }
                }

                // Insert
                // Piece 4 = 7 and piece 13 = 14, 16, 17
                if (aPiecesSpan[4] == aPiecesSpan[7] && aPiecesSpan[13] == aPiecesSpan[14] && aPiecesSpan[13] == aPiecesSpan[16] && aPiecesSpan[13] == aPiecesSpan[17])
                {
                    if (aPiecesSpan[49] == aPiecesSpan[2] && aPiecesSpan[4] == aPiecesSpan[3] && aPiecesSpan[4] == aPiecesSpan[6] && aPiecesSpan[4] == aPiecesSpan[41] && aPiecesSpan[4] == aPiecesSpan[44])
                    {
                        if (aPiecesSpan[13] == aPiecesSpan[9] && aPiecesSpan[13] == aPiecesSpan[10])
                        {
                            _ = MakeTurnAsync("U R U' R'");
                            continue;
                        }
                    }

                    if (aPiecesSpan[49] == aPiecesSpan[0] && aPiecesSpan[4] == aPiecesSpan[5] && aPiecesSpan[4] == aPiecesSpan[8] && aPiecesSpan[4] == aPiecesSpan[39] && aPiecesSpan[4] == aPiecesSpan[42])
                    {
                        if (aPiecesSpan[13] == aPiecesSpan[12] && aPiecesSpan[13] == aPiecesSpan[15])
                        {
                            _ = MakeTurnAsync("U' L' U L");
                            continue;
                        }
                    }
                }

                //--------------------------------------------------------------------------------------------------------------

                // https://www.youtube.com/watch?v=Hx9ZbPdX8zM
                // Part 1. Converting cases. Convert any F2L case into one of five using a 3-step process
                // Piece 4 = 7 and piece 13 = 16
                if (aPiecesSpan[4] == aPiecesSpan[7] && aPiecesSpan[13] == aPiecesSpan[16])
                {
                    if (aPiecesSpan[49] == aPiecesSpan[15] && aPiecesSpan[4] == aPiecesSpan[14] && aPiecesSpan[4] == aPiecesSpan[47] && aPiecesSpan[13] == aPiecesSpan[8] && aPiecesSpan[13] == aPiecesSpan[21])
                    {
                        _ = MakeTurnAsync("R U R' y R U' R'");
                        continue;
                    }

                    if (aPiecesSpan[49] == aPiecesSpan[44] && aPiecesSpan[4] == aPiecesSpan[1] && aPiecesSpan[4] == aPiecesSpan[2] && aPiecesSpan[31] == aPiecesSpan[9] && aPiecesSpan[31] == aPiecesSpan[34] && aPiecesSpan[31] == aPiecesSpan[43])
                    {
                        _ = MakeTurnAsync("R' U R");
                        continue;
                    }

                    if (aPiecesSpan[49] == aPiecesSpan[44] && aPiecesSpan[4] == aPiecesSpan[9] && aPiecesSpan[4] == aPiecesSpan[41] && aPiecesSpan[13] == aPiecesSpan[2] && aPiecesSpan[13] == aPiecesSpan[10])
                    {
                        _ = MakeTurnAsync("U' R U' R'");
                        continue;
                    }

                    // Part 2. Five fundamental cases
                    // Case 1. Matching
                    if (aPiecesSpan[49] == aPiecesSpan[9] && aPiecesSpan[4] == aPiecesSpan[2] && aPiecesSpan[4] == aPiecesSpan[19] && aPiecesSpan[13] == aPiecesSpan[37] && aPiecesSpan[13] == aPiecesSpan[44])
                    {
                        _ = MakeTurnAsync("R' U2 R y U' L' U L");
                        continue;
                    }

                    // Case 2. Non-matching  
                    if (aPiecesSpan[49] == aPiecesSpan[9] && aPiecesSpan[4] == aPiecesSpan[2] && aPiecesSpan[4] == aPiecesSpan[37] && aPiecesSpan[13] == aPiecesSpan[19] && aPiecesSpan[13] == aPiecesSpan[44])
                    {
                        _ = MakeTurnAsync("R U R'");
                        continue;
                    }

                    // Case 3. White on top - edge at the left
                    //if (aPiecesSpan[49] == aPiecesSpan[44] && aPiecesSpan[4] == aPiecesSpan[9] && aPiecesSpan[4] == aPiecesSpan[28] && aPiecesSpan[13] == aPiecesSpan[2] && aPiecesSpan[13] == aPiecesSpan[39])
                    //{
                    //    _ = MakeTurnAsync("U' F' U' R y U' L' U L"); 
                    //    continue;
                    //}

                    // Case 3. White on top - edge at the back
                    if (aPiecesSpan[49] == aPiecesSpan[44] && aPiecesSpan[4] == aPiecesSpan[9] && aPiecesSpan[4] == aPiecesSpan[19] && aPiecesSpan[13] == aPiecesSpan[2] && aPiecesSpan[13] == aPiecesSpan[37])
                    {
                        _ = MakeTurnAsync("U2 F' U' R y U' L' U L");
                        continue;
                    }

                    // Case 4. Mirrored matching
                    if (aPiecesSpan[49] == aPiecesSpan[2] && aPiecesSpan[4] == aPiecesSpan[39] && aPiecesSpan[4] == aPiecesSpan[44] && aPiecesSpan[13] == aPiecesSpan[9] && aPiecesSpan[13] == aPiecesSpan[28])
                    {
                        _ = MakeTurnAsync("U' R U2 R' U2 R U' R'");
                        continue;
                    }

                    // Case 5. Mirrored non-matching
                    if (aPiecesSpan[49] == aPiecesSpan[2] && aPiecesSpan[4] == aPiecesSpan[28] && aPiecesSpan[4] == aPiecesSpan[44] && aPiecesSpan[13] == aPiecesSpan[9] && aPiecesSpan[13] == aPiecesSpan[39])
                    {
                        _ = MakeTurnAsync("y L' U' L");
                        continue;
                    }

                    // Part 3. Solving the last pair
                    if (aPiecesSpan[49] == aPiecesSpan[9] && aPiecesSpan[4] == aPiecesSpan[2] && aPiecesSpan[4] == aPiecesSpan[3] && aPiecesSpan[4] == aPiecesSpan[6] && aPiecesSpan[4] == aPiecesSpan[43] && aPiecesSpan[13] == aPiecesSpan[1] && aPiecesSpan[13] == aPiecesSpan[14] && aPiecesSpan[13] == aPiecesSpan[17] && aPiecesSpan[13] == aPiecesSpan[44])
                    {
                        _ = MakeTurnAsync("y' U R' U2 R y U' R U R'");
                        continue;
                    }
                }

                //--------------------------------------------------------------------------------------------------------------

                // https://solvethecube.com/algorithms
                // Piece 4 = 7 and piece 13 = 16
                if (aPiecesSpan[4] == aPiecesSpan[7] && aPiecesSpan[13] == aPiecesSpan[16])
                {
                    // 1. Basic cases
                    // 1.1   R U R'
                    if (aPiecesSpan[49] == aPiecesSpan[9] && aPiecesSpan[4] == aPiecesSpan[2] && aPiecesSpan[4] == aPiecesSpan[37] && aPiecesSpan[13] == aPiecesSpan[19] && aPiecesSpan[13] == aPiecesSpan[44])
                    {
                        _ = MakeTurnAsync("R U R'");
                        continue;
                    }

                    // 1.2   F' U' F
                    if (aPiecesSpan[49] == aPiecesSpan[2] && aPiecesSpan[4] == aPiecesSpan[28] && aPiecesSpan[4] == aPiecesSpan[44] && aPiecesSpan[13] == aPiecesSpan[9] && aPiecesSpan[13] == aPiecesSpan[39])
                    {
                        _ = MakeTurnAsync("F' U' F");
                        continue;
                    }

                    // 1.3   U R U' R'
                    if (aPiecesSpan[49] == aPiecesSpan[2] && aPiecesSpan[4] == aPiecesSpan[41] && aPiecesSpan[4] == aPiecesSpan[44] && aPiecesSpan[13] == aPiecesSpan[9] && aPiecesSpan[13] == aPiecesSpan[10])
                    {
                        _ = MakeTurnAsync("U R U' R'");
                        continue;
                    }

                    // 1.4   U' F' U F
                    if (aPiecesSpan[49] == aPiecesSpan[9] && aPiecesSpan[4] == aPiecesSpan[1] && aPiecesSpan[4] == aPiecesSpan[2] && aPiecesSpan[13] == aPiecesSpan[43] && aPiecesSpan[13] == aPiecesSpan[44])
                    {
                        _ = MakeTurnAsync("U' F' U F");
                        continue;
                    }

                    // 2. Corner and edge in top
                    // 2.1   (U' R U') (R' U R) U R'
                    if (aPiecesSpan[49] == aPiecesSpan[9] && aPiecesSpan[4] == aPiecesSpan[2] && aPiecesSpan[4] == aPiecesSpan[41] && aPiecesSpan[13] == aPiecesSpan[10] && aPiecesSpan[13] == aPiecesSpan[44])
                    {
                        _ = MakeTurnAsync("U' R U' R' U R U R'");
                        continue;
                    }

                    // 2.2   (U F' U) (F U' F') U' F
                    if (aPiecesSpan[49] == aPiecesSpan[2] && aPiecesSpan[4] == aPiecesSpan[1] && aPiecesSpan[4] == aPiecesSpan[44] && aPiecesSpan[13] == aPiecesSpan[9] && aPiecesSpan[13] == aPiecesSpan[43])
                    {
                        _ = MakeTurnAsync("U F' U F U' F' U' F");
                        continue;
                    }

                    // 2.3   (U' R U) (R' U R) U R'
                    if (aPiecesSpan[49] == aPiecesSpan[9] && aPiecesSpan[4] == aPiecesSpan[2] && aPiecesSpan[4] == aPiecesSpan[39] && aPiecesSpan[13] == aPiecesSpan[28] && aPiecesSpan[13] == aPiecesSpan[44])
                    {
                        _ = MakeTurnAsync("U' R U R' U R U R'");
                        continue;
                    }

                    // 2.4   (U F' U') (F U' F') U' F
                    if (aPiecesSpan[49] == aPiecesSpan[2] && aPiecesSpan[4] == aPiecesSpan[19] && aPiecesSpan[4] == aPiecesSpan[44] && aPiecesSpan[13] == aPiecesSpan[9] && aPiecesSpan[13] == aPiecesSpan[37])
                    {
                        _ = MakeTurnAsync("U F' U' F U' F' U' F");
                        continue;
                    }

                    // 2.5   d (R' U2 R) d' (R U R')
                    if (aPiecesSpan[49] == aPiecesSpan[9] && aPiecesSpan[4] == aPiecesSpan[2] && aPiecesSpan[4] == aPiecesSpan[43] && aPiecesSpan[13] == aPiecesSpan[1] && aPiecesSpan[13] == aPiecesSpan[44])
                    {
                        _ = MakeTurnAsync("d R' U2 R d' R U R'");
                        continue;
                    }

                    // 2.6   U' (R U2 R') d (R' U' R)
                    if (aPiecesSpan[49] == aPiecesSpan[2] && aPiecesSpan[4] == aPiecesSpan[10] && aPiecesSpan[4] == aPiecesSpan[44] && aPiecesSpan[13] == aPiecesSpan[9] && aPiecesSpan[13] == aPiecesSpan[41])
                    {
                        _ = MakeTurnAsync("U' R U2 R' d R' U' R");
                        continue;
                    }

                    // 2.7   (R U' R' U) d (R' U' R)
                    if (aPiecesSpan[49] == aPiecesSpan[9] && aPiecesSpan[4] == aPiecesSpan[2] && aPiecesSpan[4] == aPiecesSpan[10] && aPiecesSpan[13] == aPiecesSpan[41] && aPiecesSpan[13] == aPiecesSpan[44])
                    {
                        _ = MakeTurnAsync("R U' R' U d R' U' R");
                        continue;
                    }

                    // 2.8   (F' U F U') d' (F U F')
                    if (aPiecesSpan[49] == aPiecesSpan[2] && aPiecesSpan[4] == aPiecesSpan[43] && aPiecesSpan[4] == aPiecesSpan[44] && aPiecesSpan[13] == aPiecesSpan[1] && aPiecesSpan[13] == aPiecesSpan[9])
                    {
                        _ = MakeTurnAsync("F' U F U' d' F U F'");
                        continue;
                    }

                    // 2.9   (U F' U2 F) (U F' U2 F)
                    if (aPiecesSpan[49] == aPiecesSpan[9] && aPiecesSpan[4] == aPiecesSpan[2] && aPiecesSpan[4] == aPiecesSpan[19] && aPiecesSpan[13] == aPiecesSpan[37] && aPiecesSpan[13] == aPiecesSpan[44])
                    {
                        _ = MakeTurnAsync("U F' U2 F U F' U2 F");
                        continue;
                    }

                    // 2.10   (U' R U2 R') (U' R U2 R')
                    if (aPiecesSpan[49] == aPiecesSpan[2] && aPiecesSpan[4] == aPiecesSpan[39] && aPiecesSpan[4] == aPiecesSpan[44] && aPiecesSpan[13] == aPiecesSpan[9] && aPiecesSpan[13] == aPiecesSpan[28])
                    {
                        _ = MakeTurnAsync("U' R U2 R' U' R U2 R'");
                        continue;
                    }

                    // 2.11   (U F' U' F) (U F' U2 F)
                    if (aPiecesSpan[49] == aPiecesSpan[9] && aPiecesSpan[4] == aPiecesSpan[2] && aPiecesSpan[4] == aPiecesSpan[28] && aPiecesSpan[13] == aPiecesSpan[39] && aPiecesSpan[13] == aPiecesSpan[44])
                    {
                        _ = MakeTurnAsync("U F' U' F U F' U2 F");
                        continue;
                    }

                    // 2.12   (U' R U R') (U' R U2 R')
                    if (aPiecesSpan[49] == aPiecesSpan[2] && aPiecesSpan[4] == aPiecesSpan[37] && aPiecesSpan[4] == aPiecesSpan[44] && aPiecesSpan[13] == aPiecesSpan[9] && aPiecesSpan[13] == aPiecesSpan[19])
                    {
                        _ = MakeTurnAsync("U' R U R' U' R U2 R'");
                        continue;
                    }

                    // 3. Corner pointing up, edge in top
                    // 3.1   (R U2 R' U') (R U R')
                    if (aPiecesSpan[49] == aPiecesSpan[44] && aPiecesSpan[4] == aPiecesSpan[9] && aPiecesSpan[4] == aPiecesSpan[41] && aPiecesSpan[13] == aPiecesSpan[2] && aPiecesSpan[13] == aPiecesSpan[10])
                    {
                        _ = MakeTurnAsync("R U2 R' U' R U R'");
                        continue;
                    }

                    // 3.2   (F' U2 F U) (F' U' F)
                    if (aPiecesSpan[49] == aPiecesSpan[44] && aPiecesSpan[4] == aPiecesSpan[1] && aPiecesSpan[4] == aPiecesSpan[9] && aPiecesSpan[13] == aPiecesSpan[2] && aPiecesSpan[13] == aPiecesSpan[43])
                    {
                        _ = MakeTurnAsync("F' U2 F U F' U' F");
                        continue;
                    }

                    // 3.3   (U R U2 R') (U R U' R')
                    if (aPiecesSpan[49] == aPiecesSpan[44] && aPiecesSpan[4] == aPiecesSpan[9] && aPiecesSpan[4] == aPiecesSpan[37] && aPiecesSpan[13] == aPiecesSpan[2] && aPiecesSpan[13] == aPiecesSpan[19])
                    {
                        _ = MakeTurnAsync("U R U2 R' U R U' R'");
                        continue;
                    }

                    // 3.4   (U' F' U2 F) (U' F' U F)
                    if (aPiecesSpan[49] == aPiecesSpan[44] && aPiecesSpan[4] == aPiecesSpan[9] && aPiecesSpan[4] == aPiecesSpan[28] && aPiecesSpan[13] == aPiecesSpan[2] && aPiecesSpan[13] == aPiecesSpan[39])
                    {
                        _ = MakeTurnAsync("U' F' U2 F U' F' U F");
                        continue;
                    }

                    // 3.5   U2 (R U R' U) (R U' R')
                    if (aPiecesSpan[49] == aPiecesSpan[44] && aPiecesSpan[4] == aPiecesSpan[9] && aPiecesSpan[4] == aPiecesSpan[39] && aPiecesSpan[13] == aPiecesSpan[2] && aPiecesSpan[13] == aPiecesSpan[28])
                    {
                        _ = MakeTurnAsync("U2 R U R' U R U' R'");
                        continue;
                    }

                    // 3.6   U2 (F' U' F U') (F' U F)
                    if (aPiecesSpan[49] == aPiecesSpan[44] && aPiecesSpan[4] == aPiecesSpan[9] && aPiecesSpan[4] == aPiecesSpan[19] && aPiecesSpan[13] == aPiecesSpan[2] && aPiecesSpan[13] == aPiecesSpan[37])
                    {
                        _ = MakeTurnAsync("U2 F' U' F U' F' U F");
                        continue;
                    }

                    // 3.7   (R U R' U') U' (R U R' U') (R U R')
                    if (aPiecesSpan[49] == aPiecesSpan[44] && aPiecesSpan[4] == aPiecesSpan[9] && aPiecesSpan[4] == aPiecesSpan[43] && aPiecesSpan[13] == aPiecesSpan[1] && aPiecesSpan[13] == aPiecesSpan[2])
                    {
                        _ = MakeTurnAsync("R U R' U' U' R U R' U' R U R'");
                        continue;
                    }

                    // 3.8   y' (R' U' R U) U (R' U' R U) (R' U' R)
                    if (aPiecesSpan[49] == aPiecesSpan[44] && aPiecesSpan[4] == aPiecesSpan[9] && aPiecesSpan[4] == aPiecesSpan[10] && aPiecesSpan[13] == aPiecesSpan[2] && aPiecesSpan[13] == aPiecesSpan[41])
                    {
                        _ = MakeTurnAsync("y' R' U' R U U R' U' R U R' U' R");
                        continue;
                    }

                    // 4. Corner in top, edge in middle
                    // 4.1   (U F' U F) (U F' U2 F)
                    if (aPiecesSpan[49] == aPiecesSpan[9] && aPiecesSpan[4] == aPiecesSpan[2] && aPiecesSpan[4] == aPiecesSpan[5] && aPiecesSpan[13] == aPiecesSpan[12] && aPiecesSpan[13] == aPiecesSpan[44])
                    {
                        _ = MakeTurnAsync("U F' U F U F' U2 F");
                        continue;
                    }

                    // 4.2   (U' R U' R') (U' R U2 R')
                    if (aPiecesSpan[49] == aPiecesSpan[2] && aPiecesSpan[4] == aPiecesSpan[5] && aPiecesSpan[4] == aPiecesSpan[44] && aPiecesSpan[13] == aPiecesSpan[9] && aPiecesSpan[13] == aPiecesSpan[12])
                    {
                        _ = MakeTurnAsync("U' R U' R' U' R U2 R'");
                        continue;
                    }

                    // 4.3   (U F' U' F) (d' F U F')
                    if (aPiecesSpan[49] == aPiecesSpan[9] && aPiecesSpan[4] == aPiecesSpan[2] && aPiecesSpan[4] == aPiecesSpan[12] && aPiecesSpan[13] == aPiecesSpan[5] && aPiecesSpan[13] == aPiecesSpan[44])
                    {
                        _ = MakeTurnAsync("U F' U' F d' F U F'");
                        continue;
                    }

                    // 4.4   (U' R U R') (d R' U' R)
                    if (aPiecesSpan[49] == aPiecesSpan[2] && aPiecesSpan[4] == aPiecesSpan[12] && aPiecesSpan[4] == aPiecesSpan[44] && aPiecesSpan[13] == aPiecesSpan[5] && aPiecesSpan[13] == aPiecesSpan[9])
                    {
                        _ = MakeTurnAsync("U' R U R' d R' U' R");
                        continue;
                    }

                    // 4.5   (R U' R') (d R' U R)
                    if (aPiecesSpan[49] == aPiecesSpan[44] && aPiecesSpan[4] == aPiecesSpan[9] && aPiecesSpan[4] == aPiecesSpan[12] && aPiecesSpan[13] == aPiecesSpan[2] && aPiecesSpan[13] == aPiecesSpan[5])
                    {
                        _ = MakeTurnAsync("R U' R' d R' U R");
                        continue;
                    }

                    // 4.6   (R U R' U') (R U R' U') (R U R')
                    if (aPiecesSpan[49] == aPiecesSpan[44] && aPiecesSpan[4] == aPiecesSpan[5] && aPiecesSpan[4] == aPiecesSpan[9] && aPiecesSpan[13] == aPiecesSpan[2] && aPiecesSpan[13] == aPiecesSpan[12])
                    {
                        _ = MakeTurnAsync("R U R' U' R U R' U' R U R'");
                        continue;
                    }

                    // 5. Corner in bottom, edge in top
                    // 5.1   (U R U' R') (U' F' U F)
                    if (aPiecesSpan[49] == aPiecesSpan[47] && aPiecesSpan[4] == aPiecesSpan[1] && aPiecesSpan[4] == aPiecesSpan[8] && aPiecesSpan[13] == aPiecesSpan[15] && aPiecesSpan[13] == aPiecesSpan[43])
                    {
                        _ = MakeTurnAsync("U R U' R' U' F' U F");
                        continue;
                    }

                    // 5.2   (U' F' U F) (U R U' R')
                    if (aPiecesSpan[49] == aPiecesSpan[47] && aPiecesSpan[4] == aPiecesSpan[8] && aPiecesSpan[4] == aPiecesSpan[41] && aPiecesSpan[13] == aPiecesSpan[10] && aPiecesSpan[13] == aPiecesSpan[15])
                    {
                        _ = MakeTurnAsync("U' F' U F U R U' R'");
                        continue;
                    }

                    // 5.3   (F' U F) (U' F' U F)
                    if (aPiecesSpan[49] == aPiecesSpan[15] && aPiecesSpan[4] == aPiecesSpan[1] && aPiecesSpan[4] == aPiecesSpan[47] && aPiecesSpan[13] == aPiecesSpan[8] && aPiecesSpan[13] == aPiecesSpan[43])
                    {
                        _ = MakeTurnAsync("F' U F U' F' U F");
                        continue;
                    }

                    // 5.4   (R U' R') (U R U' R')
                    if (aPiecesSpan[49] == aPiecesSpan[8] && aPiecesSpan[4] == aPiecesSpan[15] && aPiecesSpan[4] == aPiecesSpan[41] && aPiecesSpan[13] == aPiecesSpan[10] && aPiecesSpan[13] == aPiecesSpan[47])
                    {
                        _ = MakeTurnAsync("R U' R' U R U' R'");
                        continue;
                    }

                    // 5.5   (R U R') (U' R U R')
                    if (aPiecesSpan[49] == aPiecesSpan[15] && aPiecesSpan[4] == aPiecesSpan[41] && aPiecesSpan[4] == aPiecesSpan[47] && aPiecesSpan[13] == aPiecesSpan[8] && aPiecesSpan[13] == aPiecesSpan[10])
                    {
                        _ = MakeTurnAsync("R U R' U' R U R'");
                        continue;
                    }

                    // 5.6   (F' U' F) (U F' U' F)
                    if (aPiecesSpan[49] == aPiecesSpan[8] && aPiecesSpan[4] == aPiecesSpan[1] && aPiecesSpan[4] == aPiecesSpan[15] && aPiecesSpan[13] == aPiecesSpan[43] && aPiecesSpan[13] == aPiecesSpan[47])
                    {
                        _ = MakeTurnAsync("F' U' F U F' U' F");
                        continue;
                    }

                    // 6. Corner in bottom, edge in middle
                    // 6.1   (R U' R' U) R U2 R' (U R U' R')
                    if (aPiecesSpan[49] == aPiecesSpan[15] && aPiecesSpan[4] == aPiecesSpan[5] && aPiecesSpan[4] == aPiecesSpan[47] && aPiecesSpan[13] == aPiecesSpan[8] && aPiecesSpan[13] == aPiecesSpan[12])
                    {
                        _ = MakeTurnAsync("R U' R' U R U2 R' U R U' R'");
                        continue;
                    }

                    // 6.2   (R U' R' U') (R U R' U') (R U2 R')
                    if (aPiecesSpan[49] == aPiecesSpan[8] && aPiecesSpan[4] == aPiecesSpan[5] && aPiecesSpan[4] == aPiecesSpan[15] && aPiecesSpan[13] == aPiecesSpan[12] && aPiecesSpan[13] == aPiecesSpan[47])
                    {
                        _ = MakeTurnAsync("R U' R' U' R U R' U' R U2 R'");
                        continue;
                    }

                    // 6.3   (R U R' U') (R U' R') U d (R' U' R)
                    if (aPiecesSpan[49] == aPiecesSpan[15] && aPiecesSpan[4] == aPiecesSpan[12] && aPiecesSpan[4] == aPiecesSpan[47] && aPiecesSpan[13] == aPiecesSpan[5] && aPiecesSpan[13] == aPiecesSpan[8])
                    {
                        _ = MakeTurnAsync("R U R' U' R U' R' U d R' U' R");
                        continue;
                    }

                    // 6.4   (R U' R') d (R' U' R U') (R' U' R)
                    if (aPiecesSpan[49] == aPiecesSpan[8] && aPiecesSpan[4] == aPiecesSpan[12] && aPiecesSpan[4] == aPiecesSpan[15] && aPiecesSpan[13] == aPiecesSpan[5] && aPiecesSpan[13] == aPiecesSpan[47])
                    {
                        _ = MakeTurnAsync("R U' R' d R' U' R U' R' U' R");
                        continue;
                    }

                    // 6.5   (R U' R' d R' U2 R) (U R' U2 R)
                    if (aPiecesSpan[49] == aPiecesSpan[47] && aPiecesSpan[4] == aPiecesSpan[8] && aPiecesSpan[4] == aPiecesSpan[12] && aPiecesSpan[13] == aPiecesSpan[5] && aPiecesSpan[13] == aPiecesSpan[15])
                    {
                        _ = MakeTurnAsync("R U' R' d R' U2 R U R' U2 R");
                        continue;
                    }
                }

                //--------------------------------------------------------------------------------------------------------------

                // https://www.cubelelo.com/blogs/cubing/f2l-method-explained-solve-rubiks-cube-under-30-seconds
                // Piece 4 = 3, 6, 7 and piece 13 = 14, 16, 17
                if (aPiecesSpan[4] == aPiecesSpan[3] && aPiecesSpan[4] == aPiecesSpan[6] && aPiecesSpan[4] == aPiecesSpan[7] && aPiecesSpan[13] == aPiecesSpan[14] && aPiecesSpan[13] == aPiecesSpan[16] && aPiecesSpan[13] == aPiecesSpan[17])
                {
                    // Case 1: Basic cases
                    // 1 Case
                    if (aPiecesSpan[49] == aPiecesSpan[9] && aPiecesSpan[4] == aPiecesSpan[2] && aPiecesSpan[4] == aPiecesSpan[37] && aPiecesSpan[13] == aPiecesSpan[19] && aPiecesSpan[13] == aPiecesSpan[44])
                    {
                        _ = MakeTurnAsync("R U R'");
                        continue;
                    }

                    // 1 Case mirror
                    if (aPiecesSpan[49] == aPiecesSpan[2] && aPiecesSpan[4] == aPiecesSpan[28] && aPiecesSpan[4] == aPiecesSpan[44] && aPiecesSpan[13] == aPiecesSpan[9] && aPiecesSpan[13] == aPiecesSpan[39])
                    {
                        _ = MakeTurnAsync("F' U' F");
                        continue;
                    }

                    // 2 Case
                    if (aPiecesSpan[49] == aPiecesSpan[2] && aPiecesSpan[4] == aPiecesSpan[41] && aPiecesSpan[4] == aPiecesSpan[44] && aPiecesSpan[13] == aPiecesSpan[9] && aPiecesSpan[13] == aPiecesSpan[10])
                    {
                        _ = MakeTurnAsync("U R U' R'");
                        continue;
                    }

                    // 2 Case mirror
                    if (aPiecesSpan[49] == aPiecesSpan[9] && aPiecesSpan[4] == aPiecesSpan[1] && aPiecesSpan[4] == aPiecesSpan[2] && aPiecesSpan[13] == aPiecesSpan[43] && aPiecesSpan[13] == aPiecesSpan[44])
                    {
                        _ = MakeTurnAsync("U' F' U F");
                        continue;
                    }

                    // Case 2: Corner and the Edge in Top Layer
                    // Type 1 Cases
                    // 1 Case
                    if (aPiecesSpan[49] == aPiecesSpan[9] && aPiecesSpan[4] == aPiecesSpan[2] && aPiecesSpan[4] == aPiecesSpan[41] && aPiecesSpan[13] == aPiecesSpan[10] && aPiecesSpan[13] == aPiecesSpan[44])
                    {
                        _ = MakeTurnAsync("U' R U' R' U");
                        continue;
                    }

                    // 1 Case mirror
                    if (aPiecesSpan[49] == aPiecesSpan[2] && aPiecesSpan[4] == aPiecesSpan[1] && aPiecesSpan[4] == aPiecesSpan[44] && aPiecesSpan[13] == aPiecesSpan[9] && aPiecesSpan[13] == aPiecesSpan[43])
                    {
                        _ = MakeTurnAsync("U F' U F U'");
                        continue;
                    }

                    // 2 Case
                    if (aPiecesSpan[49] == aPiecesSpan[9] && aPiecesSpan[4] == aPiecesSpan[2] && aPiecesSpan[4] == aPiecesSpan[39] && aPiecesSpan[13] == aPiecesSpan[28] && aPiecesSpan[13] == aPiecesSpan[39])
                    {
                        _ = MakeTurnAsync("U' R U R' U");
                        continue;
                    }

                    // 2 Case mirror
                    if (aPiecesSpan[49] == aPiecesSpan[2] && aPiecesSpan[4] == aPiecesSpan[19] && aPiecesSpan[4] == aPiecesSpan[44] && aPiecesSpan[13] == aPiecesSpan[9] && aPiecesSpan[13] == aPiecesSpan[37])
                    {
                        _ = MakeTurnAsync("U F' U' F U'");
                        continue;
                    }

                    // 3 Case
                    if (aPiecesSpan[49] == aPiecesSpan[9] && aPiecesSpan[4] == aPiecesSpan[2] && aPiecesSpan[4] == aPiecesSpan[43] && aPiecesSpan[13] == aPiecesSpan[1] && aPiecesSpan[13] == aPiecesSpan[44])
                    {
                        _ = MakeTurnAsync("U F' U2 F U'");
                        continue;
                    }

                    // 3 Case mirror
                    if (aPiecesSpan[49] == aPiecesSpan[2] && aPiecesSpan[4] == aPiecesSpan[10] && aPiecesSpan[4] == aPiecesSpan[44] && aPiecesSpan[13] == aPiecesSpan[9] && aPiecesSpan[13] == aPiecesSpan[41])
                    {
                        _ = MakeTurnAsync("U' R U2 R' U");
                        continue;
                    }

                    // 4 Case
                    if (aPiecesSpan[49] == aPiecesSpan[9] && aPiecesSpan[4] == aPiecesSpan[2] && aPiecesSpan[4] == aPiecesSpan[19] && aPiecesSpan[13] == aPiecesSpan[37] && aPiecesSpan[13] == aPiecesSpan[44])
                    {
                        _ = MakeTurnAsync("U F' U2 F U'");
                        continue;
                    }

                    // 4 Case mirror
                    if (aPiecesSpan[49] == aPiecesSpan[2] && aPiecesSpan[4] == aPiecesSpan[39] && aPiecesSpan[4] == aPiecesSpan[44] && aPiecesSpan[13] == aPiecesSpan[9] && aPiecesSpan[13] == aPiecesSpan[28])
                    {
                        _ = MakeTurnAsync("U' R U2 R' U");
                        continue;
                    }

                    // 5 Case
                    if (aPiecesSpan[49] == aPiecesSpan[9] && aPiecesSpan[4] == aPiecesSpan[2] && aPiecesSpan[4] == aPiecesSpan[28] && aPiecesSpan[13] == aPiecesSpan[39] && aPiecesSpan[13] == aPiecesSpan[44])
                    {
                        _ = MakeTurnAsync("U F' U' F U'");
                        continue;
                    }

                    // 5 Case mirror (!!! no picture of cube !!!)
                    if (aPiecesSpan[49] == aPiecesSpan[2] && aPiecesSpan[4] == aPiecesSpan[37] && aPiecesSpan[4] == aPiecesSpan[44] && aPiecesSpan[13] == aPiecesSpan[9] && aPiecesSpan[13] == aPiecesSpan[19])
                    {
                        _ = MakeTurnAsync("U' R U R' U");
                        continue;
                    }

                    // Type 2 Cases
                    // 1 Case
                    if (aPiecesSpan[49] == aPiecesSpan[9] && aPiecesSpan[4] == aPiecesSpan[2] && aPiecesSpan[4] == aPiecesSpan[10] && aPiecesSpan[13] == aPiecesSpan[41] && aPiecesSpan[13] == aPiecesSpan[44])
                    {
                        _ = MakeTurnAsync("R U' R'");
                        continue;
                    }

                    // 1 Case mirror
                    if (aPiecesSpan[49] == aPiecesSpan[2] && aPiecesSpan[4] == aPiecesSpan[43] && aPiecesSpan[4] == aPiecesSpan[44] && aPiecesSpan[13] == aPiecesSpan[1] && aPiecesSpan[13] == aPiecesSpan[9])
                    {
                        _ = MakeTurnAsync("F' U F");
                        continue;
                    }

                    // 2 Case (!!! no picture of cube !!!)
                    if (aPiecesSpan[49] == aPiecesSpan[44] && aPiecesSpan[4] == aPiecesSpan[9] && aPiecesSpan[4] == aPiecesSpan[41] && aPiecesSpan[13] == aPiecesSpan[2] && aPiecesSpan[13] == aPiecesSpan[10])
                    {
                        _ = MakeTurnAsync("R U2 R'");
                        continue;
                    }

                    // 2 Case mirror
                    if (aPiecesSpan[49] == aPiecesSpan[44] && aPiecesSpan[4] == aPiecesSpan[1] && aPiecesSpan[4] == aPiecesSpan[9] && aPiecesSpan[13] == aPiecesSpan[2] && aPiecesSpan[13] == aPiecesSpan[43])
                    {
                        _ = MakeTurnAsync("F' U2 F");
                        continue;
                    }

                    // 3 Case
                    if (aPiecesSpan[49] == aPiecesSpan[44] && aPiecesSpan[4] == aPiecesSpan[9] && aPiecesSpan[4] == aPiecesSpan[37] && aPiecesSpan[13] == aPiecesSpan[2] && aPiecesSpan[13] == aPiecesSpan[19])
                    {
                        _ = MakeTurnAsync("U R U2 R'");
                        continue;
                    }

                    // 3 Case mirror
                    if (aPiecesSpan[49] == aPiecesSpan[44] && aPiecesSpan[4] == aPiecesSpan[9] && aPiecesSpan[4] == aPiecesSpan[28] && aPiecesSpan[13] == aPiecesSpan[2] && aPiecesSpan[13] == aPiecesSpan[39])
                    {
                        _ = MakeTurnAsync("U' F' U2 F");
                        continue;
                    }

                    // 4 Case
                    if (aPiecesSpan[49] == aPiecesSpan[44] && aPiecesSpan[4] == aPiecesSpan[9] && aPiecesSpan[4] == aPiecesSpan[39] && aPiecesSpan[13] == aPiecesSpan[2] && aPiecesSpan[13] == aPiecesSpan[28])
                    {
                        _ = MakeTurnAsync("U2 R U R'");
                        continue;
                    }

                    // 4 Case mirror
                    if (aPiecesSpan[49] == aPiecesSpan[44] && aPiecesSpan[4] == aPiecesSpan[9] && aPiecesSpan[4] == aPiecesSpan[19] && aPiecesSpan[13] == aPiecesSpan[2] && aPiecesSpan[13] == aPiecesSpan[37])
                    {
                        _ = MakeTurnAsync("U2 F' U' F");
                        continue;
                    }

                    // Case 3: Corner on Bottom, Edge on Top
                    // 1 Case
                    if (aPiecesSpan[49] == aPiecesSpan[47] && aPiecesSpan[4] == aPiecesSpan[1] && aPiecesSpan[4] == aPiecesSpan[8] && aPiecesSpan[13] == aPiecesSpan[15] && aPiecesSpan[13] == aPiecesSpan[43])
                    {
                        _ = MakeTurnAsync("U R U' R'");
                        continue;
                    }

                    // 1 Case mirror
                    if (aPiecesSpan[49] == aPiecesSpan[47] && aPiecesSpan[4] == aPiecesSpan[8] && aPiecesSpan[4] == aPiecesSpan[41] && aPiecesSpan[13] == aPiecesSpan[10] && aPiecesSpan[13] == aPiecesSpan[15])
                    {
                        _ = MakeTurnAsync("U' F' U F");
                        continue;
                    }

                    // 2 Case
                    if (aPiecesSpan[49] == aPiecesSpan[47] && aPiecesSpan[4] == aPiecesSpan[8] && aPiecesSpan[4] == aPiecesSpan[41] && aPiecesSpan[13] == aPiecesSpan[10] && aPiecesSpan[13] == aPiecesSpan[15])
                    {
                        _ = MakeTurnAsync("F' U F");
                        continue;
                    }

                    // 2 Case mirror
                    if (aPiecesSpan[49] == aPiecesSpan[8] && aPiecesSpan[4] == aPiecesSpan[15] && aPiecesSpan[4] == aPiecesSpan[41] && aPiecesSpan[13] == aPiecesSpan[10] && aPiecesSpan[13] == aPiecesSpan[47])
                    {
                        _ = MakeTurnAsync("R U' R'");
                        continue;
                    }

                    // 3 Case
                    if (aPiecesSpan[49] == aPiecesSpan[15] && aPiecesSpan[4] == aPiecesSpan[41] && aPiecesSpan[4] == aPiecesSpan[47] && aPiecesSpan[13] == aPiecesSpan[8] && aPiecesSpan[13] == aPiecesSpan[10])
                    {
                        _ = MakeTurnAsync("R U R'");
                        continue;
                    }

                    // 3 Case mirror
                    if (aPiecesSpan[49] == aPiecesSpan[8] && aPiecesSpan[4] == aPiecesSpan[1] && aPiecesSpan[4] == aPiecesSpan[15] && aPiecesSpan[13] == aPiecesSpan[43] && aPiecesSpan[13] == aPiecesSpan[47])
                    {
                        _ = MakeTurnAsync("F' U' F");
                        continue;
                    }

                    // Case 4: Corner on Top, Edge in Middle
                    // 1 Case
                    if (aPiecesSpan[49] == aPiecesSpan[9] && aPiecesSpan[4] == aPiecesSpan[2] && aPiecesSpan[4] == aPiecesSpan[5] && aPiecesSpan[13] == aPiecesSpan[12] && aPiecesSpan[13] == aPiecesSpan[44])
                    {
                        _ = MakeTurnAsync("U F' U F");
                        continue;
                    }

                    // 1 Case mirror
                    if (aPiecesSpan[49] == aPiecesSpan[2] && aPiecesSpan[4] == aPiecesSpan[5] && aPiecesSpan[4] == aPiecesSpan[44] && aPiecesSpan[13] == aPiecesSpan[9] && aPiecesSpan[13] == aPiecesSpan[12])
                    {
                        _ = MakeTurnAsync("U' R U' R'");
                        continue;
                    }

                    // 2 Case
                    if (aPiecesSpan[49] == aPiecesSpan[2] && aPiecesSpan[4] == aPiecesSpan[5] && aPiecesSpan[4] == aPiecesSpan[44] && aPiecesSpan[13] == aPiecesSpan[9] && aPiecesSpan[13] == aPiecesSpan[12])
                    {
                        _ = MakeTurnAsync("U F' U' F");
                        continue;
                    }

                    // 2 Case mirror
                    if (aPiecesSpan[49] == aPiecesSpan[2] && aPiecesSpan[4] == aPiecesSpan[12] && aPiecesSpan[4] == aPiecesSpan[44] && aPiecesSpan[13] == aPiecesSpan[5] && aPiecesSpan[13] == aPiecesSpan[9])
                    {
                        _ = MakeTurnAsync("U' R U R'");
                        continue;
                    }

                    // 3 Case
                    if (aPiecesSpan[49] == aPiecesSpan[44] && aPiecesSpan[4] == aPiecesSpan[9] && aPiecesSpan[4] == aPiecesSpan[12] && aPiecesSpan[13] == aPiecesSpan[2] && aPiecesSpan[13] == aPiecesSpan[5])
                    {
                        _ = MakeTurnAsync("R U2 R'");
                        continue;
                    }

                    // 4 Case
                    if (aPiecesSpan[49] == aPiecesSpan[44] && aPiecesSpan[4] == aPiecesSpan[5] && aPiecesSpan[4] == aPiecesSpan[9] && aPiecesSpan[13] == aPiecesSpan[2] && aPiecesSpan[13] == aPiecesSpan[12])
                    {
                        _ = MakeTurnAsync("R U R' U' R U R'");
                        continue;
                    }

                    // Case 5: Corner on Bottom, Edge in Middle
                    // 1 Special Case
                    if (aPiecesSpan[49] == aPiecesSpan[15] && aPiecesSpan[4] == aPiecesSpan[5] && aPiecesSpan[4] == aPiecesSpan[47] && aPiecesSpan[13] == aPiecesSpan[8] && aPiecesSpan[13] == aPiecesSpan[12])
                    {
                        _ = MakeTurnAsync("R U2 R U R' U R U2 R2");
                        continue;
                    }

                    // 1 Special Case mirror
                    if (aPiecesSpan[49] == aPiecesSpan[8] && aPiecesSpan[4] == aPiecesSpan[5] && aPiecesSpan[4] == aPiecesSpan[15] && aPiecesSpan[13] == aPiecesSpan[12] && aPiecesSpan[13] == aPiecesSpan[47])
                    {
                        _ = MakeTurnAsync("R2 U2 R' U' R U' R' U2 R'");
                        continue;
                    }

                    // 2 Special Case
                    if (aPiecesSpan[49] == aPiecesSpan[15] && aPiecesSpan[4] == aPiecesSpan[12] && aPiecesSpan[4] == aPiecesSpan[47] && aPiecesSpan[13] == aPiecesSpan[5] && aPiecesSpan[13] == aPiecesSpan[8])
                    {
                        _ = MakeTurnAsync("R U' R' F' L' U2 L F");
                        continue;
                    }

                    // 2 Special Case mirror
                    if (aPiecesSpan[49] == aPiecesSpan[8] && aPiecesSpan[4] == aPiecesSpan[12] && aPiecesSpan[4] == aPiecesSpan[15] && aPiecesSpan[13] == aPiecesSpan[5] && aPiecesSpan[13] == aPiecesSpan[47])
                    {
                        _ = MakeTurnAsync("R U' R U y' L U' L' B2");
                        continue;
                    }
                }

                // Advanced F2L Techniques
                // 1. Stuck in the wrong slots
                // Edge in the wrong slot
                // Case: The edge is in the wrong slot, but the corner is still in the top
                // 1
                //if (aPiecesSpan[49] == aPiecesSpan[9] && aPiecesSpan[4] == aPiecesSpan[12] && aPiecesSpan[4] == aPiecesSpan[44] && aPiecesSpan[31] == aPiecesSpan[2] && aPiecesSpan[31] == aPiecesSpan[5])
                //{
                //    _ = MakeTurnAsync("U F' U F U'");
                //    continue;
                //}

                // 2 (!!! same picture as the previous one !!!)
                //if (aPiecesSpan[49] == aPiecesSpan[9] && aPiecesSpan[4] == aPiecesSpan[12] && aPiecesSpan[4] == aPiecesSpan[44] && aPiecesSpan[31] == aPiecesSpan[2] && aPiecesSpan[31] == aPiecesSpan[5])
                //{
                //    _ = MakeTurnAsync("U' R U R' U");
                //    continue;
                //}

                // Corner in the wrong slot.
                // Case: The corner is in the wrong slot, but the edge is still in the top.
                // 1 (!!! same picture as the previous two !!!)
                //if (aPiecesSpan[49] == aPiecesSpan[9] && aPiecesSpan[4] == aPiecesSpan[12] && aPiecesSpan[4] == aPiecesSpan[44] && aPiecesSpan[31] == aPiecesSpan[2] && aPiecesSpan[31] == aPiecesSpan[5])
                //{
                //    _ = MakeTurnAsync("R U' R'");
                //    continue;
                //}

                // 2
                if (aPiecesSpan[49] == aPiecesSpan[15] && aPiecesSpan[13] == aPiecesSpan[41] && aPiecesSpan[13] == aPiecesSpan[47] && aPiecesSpan[22] == aPiecesSpan[8] && aPiecesSpan[22] == aPiecesSpan[10])
                {
                    _ = MakeTurnAsync("R U R' U'");
                    continue;
                }

                // 2. Stuck pieces
                // At times you would find that the corner and the edge pieces are not on top, but instead are stuck in random incorrect slots.
                // To set it up, use the algorithm stated below.
                if (aPiecesSpan[49] == aPiecesSpan[2] && aPiecesSpan[4] == aPiecesSpan[32] && aPiecesSpan[4] == aPiecesSpan[44] && aPiecesSpan[13] == aPiecesSpan[3] && aPiecesSpan[13] == aPiecesSpan[9])
                {
                    _ = MakeTurnAsync("U R' D' F D R");
                    // Now, in the next step, use the algorithm.
                    _ = MakeTurnAsync("R' U' R' U R");
                    continue;
                }

                // 3. Special shortcuts
                // At times when the F2L pieces are stuck in other slots, there are a few shortcuts that you can use.
                // We have mentioned a few below. However, you could find yourself in many other situations.
                // 1 Case
                if (aPiecesSpan[49] == aPiecesSpan[8] && aPiecesSpan[13] == aPiecesSpan[15] && aPiecesSpan[13] == aPiecesSpan[41] && aPiecesSpan[22] == aPiecesSpan[10] && aPiecesSpan[22] == aPiecesSpan[47])
                {
                    _ = MakeTurnAsync("y L' R U' L R'");
                    continue;
                }

                // 1 Case mirror (!!! same picture as the previous one !!!)
                //if (aPiecesSpan[49] == aPiecesSpan[8] && aPiecesSpan[13] == aPiecesSpan[15] && aPiecesSpan[13] == aPiecesSpan[41] && aPiecesSpan[22] == aPiecesSpan[10] && aPiecesSpan[22] == aPiecesSpan[47])
                //{
                //    _ = MakeTurnAsync("L' R U L R'");
                //    continue;
                //}

                // 2 Case
                if (aPiecesSpan[49] == aPiecesSpan[15] && aPiecesSpan[13] == aPiecesSpan[1] && aPiecesSpan[13] == aPiecesSpan[47] && aPiecesSpan[22] == aPiecesSpan[8] && aPiecesSpan[22] == aPiecesSpan[43])
                {
                    _ = MakeTurnAsync("R U2 R2 U R");
                    continue;
                }

                // 2 Case mirror
                if (aPiecesSpan[49] == aPiecesSpan[8] && aPiecesSpan[4] == aPiecesSpan[10] && aPiecesSpan[4] == aPiecesSpan[47] && aPiecesSpan[31] == aPiecesSpan[15] && aPiecesSpan[31] == aPiecesSpan[41])
                {
                    _ = MakeTurnAsync("y' R' U2 R2 U' R'");
                    continue;
                }

                // 3 Case
                if (aPiecesSpan[49] == aPiecesSpan[9] && aPiecesSpan[4] == aPiecesSpan[2] && aPiecesSpan[4] == aPiecesSpan[21] && aPiecesSpan[13] == aPiecesSpan[14] && aPiecesSpan[13] == aPiecesSpan[44])
                {
                    _ = MakeTurnAsync("R' U' R2 U R'");
                    continue;
                }

                // 3 Case mirror
                if (aPiecesSpan[49] == aPiecesSpan[2] && aPiecesSpan[4] == aPiecesSpan[3] && aPiecesSpan[4] == aPiecesSpan[44] && aPiecesSpan[13] == aPiecesSpan[9] && aPiecesSpan[13] == aPiecesSpan[32])
                {
                    _ = MakeTurnAsync("y' R U R2 U' R");
                    continue;
                }

                // 4 Case
                if (aPiecesSpan[49] == aPiecesSpan[47] && aPiecesSpan[4] == aPiecesSpan[8] && aPiecesSpan[4] == aPiecesSpan[21] && aPiecesSpan[13] == aPiecesSpan[14] && aPiecesSpan[13] == aPiecesSpan[15])
                {
                    _ = MakeTurnAsync("R2 U' R2 U R2");
                    continue;
                }

                // 4 Case mirror
                if (aPiecesSpan[49] == aPiecesSpan[47] && aPiecesSpan[4] == aPiecesSpan[3] && aPiecesSpan[4] == aPiecesSpan[8] && aPiecesSpan[13] == aPiecesSpan[15] && aPiecesSpan[13] == aPiecesSpan[32])
                {
                    _ = MakeTurnAsync("y' R2 U R2 U' R2");
                    continue;
                }

                //continue;

                //--------------------------------------------------------------------------------------------------------------

                // https://ruwix.com/the-rubiks-cube/advanced-cfop-fridrich/first-two-layers-f2l/
                // Piece 4 = 7 and piece 13 = 16
                if (aPiecesSpan[4] == aPiecesSpan[7] && aPiecesSpan[13] == aPiecesSpan[16])
                {
                    // Step 2: First two layers - F2L
                    // 1. Easy cases.  These are the lucky cases which can be solved in 3-4 moves.
                    // 1.1   R U R'
                    if (aPiecesSpan[49] == aPiecesSpan[9] && aPiecesSpan[4] == aPiecesSpan[2] && aPiecesSpan[4] == aPiecesSpan[37] && aPiecesSpan[13] == aPiecesSpan[44])
                    {
                        _ = MakeTurnAsync("R U R'");
                        continue;
                    }

                    // 1.2   F' U' F
                    if (aPiecesSpan[49] == aPiecesSpan[2] && aPiecesSpan[4] == aPiecesSpan[44] && aPiecesSpan[13] == aPiecesSpan[9] && aPiecesSpan[13] == aPiecesSpan[39])
                    {
                        _ = MakeTurnAsync("F' U' F");
                        continue;
                    }

                    // 1.3   U' F' U F
                    if (aPiecesSpan[49] == aPiecesSpan[9] && aPiecesSpan[4] == aPiecesSpan[1] && aPiecesSpan[4] == aPiecesSpan[2] && aPiecesSpan[13] == aPiecesSpan[43] && aPiecesSpan[13] == aPiecesSpan[44])
                    {
                        _ = MakeTurnAsync("U' F' U F");
                        continue;
                    }

                    // 1.4    U R U' R'
                    if (aPiecesSpan[49] == aPiecesSpan[2] && aPiecesSpan[4] == aPiecesSpan[41] && aPiecesSpan[4] == aPiecesSpan[44] && aPiecesSpan[13] == aPiecesSpan[9] && aPiecesSpan[13] == aPiecesSpan[10])
                    {
                        _ = MakeTurnAsync("U R U' R'");
                        continue;
                    }

                    // 2. Case: Corner in bottom, edge in top layer
                    // 2.1   (U R U' R') (U' F' U F)
                    if (aPiecesSpan[4] == aPiecesSpan[1] && aPiecesSpan[4] == aPiecesSpan[8] && aPiecesSpan[13] == aPiecesSpan[15] && aPiecesSpan[13] == aPiecesSpan[43])
                    {
                        _ = MakeTurnAsync("U R U' R' U' F' U F");
                        continue;
                    }

                    // 2.2   (U' F' U F) (U R U' R')
                    if (aPiecesSpan[4] == aPiecesSpan[8] && aPiecesSpan[4] == aPiecesSpan[41] && aPiecesSpan[13] == aPiecesSpan[10] && aPiecesSpan[13] == aPiecesSpan[15])
                    {
                        _ = MakeTurnAsync("U' F' U F U R U' R'");
                        continue;
                    }

                    // 2.3   (F' U F) (U' F' U F)
                    if (aPiecesSpan[49] == aPiecesSpan[15] && aPiecesSpan[4] == aPiecesSpan[1] && aPiecesSpan[13] == aPiecesSpan[8] && aPiecesSpan[13] == aPiecesSpan[43])
                    {
                        _ = MakeTurnAsync("F' U F U' F' U F");
                        continue;
                    }

                    // 2.4   (R U R') (U' R U R')
                    if (aPiecesSpan[49] == aPiecesSpan[15] && aPiecesSpan[4] == aPiecesSpan[41] && aPiecesSpan[13] == aPiecesSpan[8] && aPiecesSpan[13] == aPiecesSpan[10])
                    {
                        _ = MakeTurnAsync("R U R' U' R U R'");
                        continue;
                    }

                    // 2.5   (R U' R') (U R U' R')
                    if (aPiecesSpan[49] == aPiecesSpan[8] && aPiecesSpan[4] == aPiecesSpan[15] && aPiecesSpan[4] == aPiecesSpan[41] && aPiecesSpan[13] == aPiecesSpan[10])
                    {
                        _ = MakeTurnAsync("R U' R' U R U' R'");
                        continue;
                    }

                    // 2.6   (F' U' F) (U F' U' F)
                    if (aPiecesSpan[49] == aPiecesSpan[8] && aPiecesSpan[4] == aPiecesSpan[1] && aPiecesSpan[4] == aPiecesSpan[15] && aPiecesSpan[13] == aPiecesSpan[43])
                    {
                        _ = MakeTurnAsync("F' U' F U F' U' F");
                        continue;
                    }

                    // 3. case: Corner in top, edge in middle
                    // 3.1   (R U R' U') (R U R' U') (R U R')
                    if (aPiecesSpan[49] == aPiecesSpan[44] && aPiecesSpan[4] == aPiecesSpan[5] && aPiecesSpan[4] == aPiecesSpan[9] && aPiecesSpan[13] == aPiecesSpan[2] && aPiecesSpan[13] == aPiecesSpan[12])
                    {
                        _ = MakeTurnAsync("R U R' U' R U R' U' R U R'");
                        continue;
                    }

                    // 3.2   (R U' R') (d R' U R)
                    if (aPiecesSpan[49] == aPiecesSpan[44] && aPiecesSpan[4] == aPiecesSpan[9] && aPiecesSpan[4] == aPiecesSpan[12] && aPiecesSpan[13] == aPiecesSpan[2] && aPiecesSpan[13] == aPiecesSpan[5])
                    {
                        _ = MakeTurnAsync("R U' R' d R' U R");
                        continue;
                    }

                    // 3.3   (U F' U F) (U F' U2 F)
                    if (aPiecesSpan[49] == aPiecesSpan[9] && aPiecesSpan[4] == aPiecesSpan[2] && aPiecesSpan[4] == aPiecesSpan[5] && aPiecesSpan[13] == aPiecesSpan[12] && aPiecesSpan[13] == aPiecesSpan[44])
                    {
                        _ = MakeTurnAsync("U F' U F U F' U2 F");
                        continue;
                    }

                    // 3.4   (U F' U' F) (d' F U F')
                    if (aPiecesSpan[49] == aPiecesSpan[9] && aPiecesSpan[4] == aPiecesSpan[2] && aPiecesSpan[4] == aPiecesSpan[12] && aPiecesSpan[13] == aPiecesSpan[5] && aPiecesSpan[13] == aPiecesSpan[44])
                    {
                        _ = MakeTurnAsync("U F' U' F d' F U F'");
                        continue;
                    }

                    // 3.5   (U' R U' R') (U' R U2 R')
                    if (aPiecesSpan[49] == aPiecesSpan[2] && aPiecesSpan[4] == aPiecesSpan[5] && aPiecesSpan[4] == aPiecesSpan[44] && aPiecesSpan[13] == aPiecesSpan[9] && aPiecesSpan[13] == aPiecesSpan[12])
                    {
                        _ = MakeTurnAsync("U' R U' R' U' R U2 R'");
                        continue;
                    }

                    // 3.6   (U' R U R') (d R' U' R)
                    if (aPiecesSpan[49] == aPiecesSpan[2] && aPiecesSpan[4] == aPiecesSpan[12] && aPiecesSpan[4] == aPiecesSpan[44] && aPiecesSpan[13] == aPiecesSpan[5] && aPiecesSpan[13] == aPiecesSpan[9])
                    {
                        _ = MakeTurnAsync("U' R U R' d R' U' R");
                        continue;
                    }

                    // 4. case: Corner pointing outwards, edge in top layer.  In this case we usually bring the cube to a basic case, reorienting the white corner in the first stage.
                    // 4.1   (R U' R' U) (d R' U' R)
                    if (aPiecesSpan[49] == aPiecesSpan[9] && aPiecesSpan[4] == aPiecesSpan[2] && aPiecesSpan[4] == aPiecesSpan[10] && aPiecesSpan[13] == aPiecesSpan[41] && aPiecesSpan[13] == aPiecesSpan[44])
                    {
                        _ = MakeTurnAsync("R U' R' U d R' U' R");
                        continue;
                    }

                    // 4.2   (F' U F U') (d' F U F')
                    if (aPiecesSpan[49] == aPiecesSpan[2] && aPiecesSpan[4] == aPiecesSpan[43] && aPiecesSpan[4] == aPiecesSpan[44] && aPiecesSpan[13] == aPiecesSpan[1] && aPiecesSpan[13] == aPiecesSpan[9])
                    {
                        _ = MakeTurnAsync("F' U F U' d' F U F'");
                        continue;
                    }

                    // 4.3   (U F' U2 F) (U F' U2 F)
                    if (aPiecesSpan[49] == aPiecesSpan[9] && aPiecesSpan[4] == aPiecesSpan[2] && aPiecesSpan[13] == aPiecesSpan[37] && aPiecesSpan[13] == aPiecesSpan[44])
                    {
                        _ = MakeTurnAsync("U F' U2 F U F' U2 F");
                        continue;
                    }

                    // 4.4   (U' R U2 R') (U' R U2 R')
                    if (aPiecesSpan[49] == aPiecesSpan[2] && aPiecesSpan[4] == aPiecesSpan[39] && aPiecesSpan[4] == aPiecesSpan[44] && aPiecesSpan[13] == aPiecesSpan[9])
                    {
                        _ = MakeTurnAsync("U' R U2 R' U' R U2 R'");
                        continue;
                    }

                    // 4.5   (U F' U' F) (U F' U2 F)
                    if (aPiecesSpan[49] == aPiecesSpan[9] && aPiecesSpan[4] == aPiecesSpan[2] && aPiecesSpan[13] == aPiecesSpan[39] && aPiecesSpan[13] == aPiecesSpan[44])
                    {
                        _ = MakeTurnAsync("U F' U' F U F' U2 F");
                        continue;
                    }

                    // 4.6   (U' R U R') (U' R U2 R')
                    if (aPiecesSpan[49] == aPiecesSpan[2] && aPiecesSpan[4] == aPiecesSpan[37] && aPiecesSpan[4] == aPiecesSpan[44] && aPiecesSpan[13] == aPiecesSpan[9])
                    {
                        _ = MakeTurnAsync("U' R U R' U' R U2 R'");
                        continue;
                    }

                    // 4.7   (U' R U' R' U) (R U R')
                    if (aPiecesSpan[49] == aPiecesSpan[9] && aPiecesSpan[4] == aPiecesSpan[2] && aPiecesSpan[4] == aPiecesSpan[41] && aPiecesSpan[13] == aPiecesSpan[10] && aPiecesSpan[13] == aPiecesSpan[44])
                    {
                        _ = MakeTurnAsync("U' R U' R' U R U R'");
                        continue;
                    }

                    // 4.8   (U F' U F U') (F' U' F)
                    if (aPiecesSpan[49] == aPiecesSpan[2] && aPiecesSpan[4] == aPiecesSpan[1] && aPiecesSpan[4] == aPiecesSpan[44] && aPiecesSpan[13] == aPiecesSpan[9] && aPiecesSpan[13] == aPiecesSpan[43])
                    {
                        _ = MakeTurnAsync("U F' U F U' F' U' F");
                        continue;
                    }

                    // 4.9   (U' R U R' U) (R U R')
                    if (aPiecesSpan[49] == aPiecesSpan[9] && aPiecesSpan[4] == aPiecesSpan[2] && aPiecesSpan[4] == aPiecesSpan[39] && aPiecesSpan[13] == aPiecesSpan[44])
                    {
                        _ = MakeTurnAsync("U' R U R' U R U R'");
                        continue;
                    }

                    // 4.10   (U F' U' F U') (F' U' F)
                    if (aPiecesSpan[49] == aPiecesSpan[2] && aPiecesSpan[4] == aPiecesSpan[44] && aPiecesSpan[13] == aPiecesSpan[9] && aPiecesSpan[13] == aPiecesSpan[37])
                    {
                        _ = MakeTurnAsync("U F' U' F U' F' U' F");
                        continue;
                    }

                    // 4.11   (U F' U2 F U') (R U R')
                    if (aPiecesSpan[49] == aPiecesSpan[9] && aPiecesSpan[4] == aPiecesSpan[2] && aPiecesSpan[4] == aPiecesSpan[43] && aPiecesSpan[13] == aPiecesSpan[1] && aPiecesSpan[13] == aPiecesSpan[44])
                    {
                        _ = MakeTurnAsync("U F' U2 F U' R U R'");
                        continue;
                    }

                    // 4.12   (U' R U2 R' U) (F' U' F)
                    if (aPiecesSpan[49] == aPiecesSpan[2] && aPiecesSpan[4] == aPiecesSpan[10] && aPiecesSpan[4] == aPiecesSpan[44] && aPiecesSpan[13] == aPiecesSpan[9] && aPiecesSpan[13] == aPiecesSpan[41])
                    {
                        _ = MakeTurnAsync("U' R U2 R' U F' U' F");
                        continue;
                    }

                    // 5. case: Corner pointing upwards, edge in top layer
                    // 5.1   (R U R' U') U' (R U R' U') (R U R')
                    if (aPiecesSpan[49] == aPiecesSpan[44] && aPiecesSpan[4] == aPiecesSpan[9] && aPiecesSpan[4] == aPiecesSpan[43] && aPiecesSpan[13] == aPiecesSpan[1] && aPiecesSpan[13] == aPiecesSpan[2])
                    {
                        _ = MakeTurnAsync("R U R' U' U' R U R' U' R U R'");
                        continue;
                    }

                    // 5.2   y' (R' U' R U) U (R' U' R U) (R' U' R)
                    if (aPiecesSpan[49] == aPiecesSpan[44] && aPiecesSpan[4] == aPiecesSpan[9] && aPiecesSpan[4] == aPiecesSpan[10] && aPiecesSpan[13] == aPiecesSpan[2] && aPiecesSpan[13] == aPiecesSpan[41])
                    {
                        _ = MakeTurnAsync("y' R' U' R U U R' U' R U R' U' R");
                        continue;
                    }

                    // 5.3   (U2 R U R') (U R U' R')
                    if (aPiecesSpan[49] == aPiecesSpan[44] && aPiecesSpan[4] == aPiecesSpan[9] && aPiecesSpan[4] == aPiecesSpan[39] && aPiecesSpan[13] == aPiecesSpan[2])
                    {
                        _ = MakeTurnAsync("U2 R U R' U R U' R'");
                        continue;
                    }

                    // 5.4   (U2 F' U' F) (U' F' U F)
                    if (aPiecesSpan[49] == aPiecesSpan[44] && aPiecesSpan[4] == aPiecesSpan[9] && aPiecesSpan[13] == aPiecesSpan[2] && aPiecesSpan[13] == aPiecesSpan[37])
                    {
                        _ = MakeTurnAsync("U2 F' U' F U' F' U F");
                        continue;
                    }

                    // 5.5   (U R U2 R') (U R U' R')
                    if (aPiecesSpan[49] == aPiecesSpan[44] && aPiecesSpan[4] == aPiecesSpan[9] && aPiecesSpan[4] == aPiecesSpan[37] && aPiecesSpan[13] == aPiecesSpan[2])
                    {
                        _ = MakeTurnAsync("U R U2 R' U R U' R'");
                        continue;
                    }

                    // 5.6   (U' F' U2 F) (U' F' U F)
                    if (aPiecesSpan[49] == aPiecesSpan[44] && aPiecesSpan[4] == aPiecesSpan[9] && aPiecesSpan[13] == aPiecesSpan[2] && aPiecesSpan[13] == aPiecesSpan[39])
                    {
                        _ = MakeTurnAsync("U' F' U2 F U' F' U F");
                        continue;
                    }

                    // 5.7   (R U2 R') (U' R U R')
                    if (aPiecesSpan[49] == aPiecesSpan[44] && aPiecesSpan[4] == aPiecesSpan[9] && aPiecesSpan[4] == aPiecesSpan[41] && aPiecesSpan[13] == aPiecesSpan[2] && aPiecesSpan[13] == aPiecesSpan[10])
                    {
                        _ = MakeTurnAsync("R U2 R' U' R U R'");
                        continue;
                    }

                    // 5.8   (F' U2 F) (U F' U' F)
                    if (aPiecesSpan[49] == aPiecesSpan[44] && aPiecesSpan[4] == aPiecesSpan[1] && aPiecesSpan[4] == aPiecesSpan[9] && aPiecesSpan[13] == aPiecesSpan[2] && aPiecesSpan[13] == aPiecesSpan[43])
                    {
                        _ = MakeTurnAsync("F' U2 F U F' U' F");
                        continue;
                    }

                    // 6. case: Corner in bottom, edge in middle
                    // 6.1   (R U' R' d R' U2 R) (U R' U2 R)
                    if (aPiecesSpan[49] == aPiecesSpan[47] && aPiecesSpan[4] == aPiecesSpan[8] && aPiecesSpan[4] == aPiecesSpan[12] && aPiecesSpan[13] == aPiecesSpan[5] && aPiecesSpan[13] == aPiecesSpan[15])
                    {
                        _ = MakeTurnAsync("R U' R' d R' U2 R U R' U2 R");
                        continue;
                    }

                    // 6.2   Does not exist

                    // 6.3   (R U' R' U R U2 R') (U R U' R')
                    if (aPiecesSpan[49] == aPiecesSpan[15] && aPiecesSpan[4] == aPiecesSpan[5] && aPiecesSpan[13] == aPiecesSpan[8] && aPiecesSpan[13] == aPiecesSpan[12])
                    {
                        _ = MakeTurnAsync("R U' R' U R U2 R' U R U' R'");
                        continue;
                    }

                    // 6.4   (R U' R' U' R U R') (U' R U2 R')
                    if (aPiecesSpan[49] == aPiecesSpan[8] && aPiecesSpan[4] == aPiecesSpan[5] && aPiecesSpan[4] == aPiecesSpan[15] && aPiecesSpan[13] == aPiecesSpan[12])
                    {
                        _ = MakeTurnAsync("R U' R' U' R U R' U' R U2 R'");
                        continue;
                    }

                    // 6.5   (R U R' U' R U' R') (U d R' U' R)
                    if (aPiecesSpan[49] == aPiecesSpan[15] && aPiecesSpan[4] == aPiecesSpan[12] && aPiecesSpan[13] == aPiecesSpan[5] && aPiecesSpan[13] == aPiecesSpan[8])
                    {
                        _ = MakeTurnAsync("R U R' U' R U' R' U d R' U' R");
                        continue;
                    }

                    // 6.6   (R U' R' d R' U' R) (U' R' U' R)
                    if (aPiecesSpan[49] == aPiecesSpan[8] && aPiecesSpan[4] == aPiecesSpan[12] && aPiecesSpan[4] == aPiecesSpan[15] && aPiecesSpan[13] == aPiecesSpan[8])
                    {
                        _ = MakeTurnAsync("R U' R' d R' U' R U' R' U' R");
                        continue;
                    }

                    //continue;
                }

                //--------------------------------------------------------------------------------------------------------------

                // https://www.youtube.com/watch?v=Ar_Zit1VLG0
                // 4. Fundamental algorithms
                if (aPiecesSpan[49] == aPiecesSpan[29] && aPiecesSpan[4] == aPiecesSpan[42] && aPiecesSpan[4] == aPiecesSpan[43] && aPiecesSpan[13] == aPiecesSpan[0] && aPiecesSpan[13] == aPiecesSpan[1])
                {
                    _ = MakeTurnAsync("R U' R'");
                    continue;
                }

                if (aPiecesSpan[49] == aPiecesSpan[9] && aPiecesSpan[4] == aPiecesSpan[43] && aPiecesSpan[4] == aPiecesSpan[44] && aPiecesSpan[31] == aPiecesSpan[1] && aPiecesSpan[31] == aPiecesSpan[2])
                {
                    _ = MakeTurnAsync("L' U L");
                    continue;
                }

                if (aPiecesSpan[49] == aPiecesSpan[9] && aPiecesSpan[4] == aPiecesSpan[2] && aPiecesSpan[4] == aPiecesSpan[37] && aPiecesSpan[13] == aPiecesSpan[19] && aPiecesSpan[13] == aPiecesSpan[44])
                {
                    _ = MakeTurnAsync("R U R'");
                    continue;
                }

                if (aPiecesSpan[49] == aPiecesSpan[29] && aPiecesSpan[4] == aPiecesSpan[0] && aPiecesSpan[4] == aPiecesSpan[37] && aPiecesSpan[31] == aPiecesSpan[19] && aPiecesSpan[31] == aPiecesSpan[42])
                {
                    _ = MakeTurnAsync("L' U' L");
                    continue;
                }

                if (aPiecesSpan[49] == aPiecesSpan[15] && aPiecesSpan[4] == aPiecesSpan[28] && aPiecesSpan[4] == aPiecesSpan[47] && aPiecesSpan[13] == aPiecesSpan[8] && aPiecesSpan[13] == aPiecesSpan[39])
                {
                    _ = MakeTurnAsync("R U R'");

                    // 2. Is white facing up?
                    // 3.  If not, move the corner piece
                    if (aPiecesSpan[49] == aPiecesSpan[9])
                    {
                        _ = MakeTurnAsync("U");
                    }

                    // 4. Are top colors the same?
                    if (aPiecesSpan[42] == aPiecesSpan[37])
                    {
                        // 5. If, move the edge piece
                        _ = MakeTurnAsync("F' U' F U'");
                        // 6. Insert
                        _ = MakeTurnAsync("U' L' U L");
                        continue;
                    }

                    // 1. Both pieces in top layer
                    // 2. Is white facing up?
                    if (aPiecesSpan[49] == aPiecesSpan[29] && aPiecesSpan[4] == aPiecesSpan[42] && aPiecesSpan[4] == aPiecesSpan[10] && aPiecesSpan[13] == aPiecesSpan[0] && aPiecesSpan[13] == aPiecesSpan[41])
                    {
                        // 3. If not, move the corner piece
                        _ = MakeTurnAsync("U' U' ");

                        // 4. Are top colors the same?
                        if (aPiecesSpan[39] != aPiecesSpan[38])
                        {
                            // 5. If not, move the edge piece
                            _ = MakeTurnAsync("R U' R'");
                            // 6. Insert
                            _ = MakeTurnAsync("U L' U' L");
                        }

                        // 2. White is facing up
                        // 3. Move the edge piece
                        if (aPiecesSpan[49] == aPiecesSpan[44] && aPiecesSpan[4] == aPiecesSpan[2] && aPiecesSpan[4] == aPiecesSpan[19] && aPiecesSpan[31] == aPiecesSpan[9] && aPiecesSpan[31] == aPiecesSpan[37])
                        {
                            // 4. Put the corner above edge
                            _ = MakeTurnAsync("y' U2 R U2 R'");
                            //5. Insert
                            _ = MakeTurnAsync("R U' R'");
                        }
                    }

                    continue;
                }

                // Special cases (To many options)
                // Option 1: algorithms
                // Option 2: split it up

                //--------------------------------------------------------------------------------------------------------------

                // https://drive.google.com/file/d/1nzAXYUWZJ6H2wIOXaHdWXep3W57tArbR/view
                // Piece 4 = 7 and piece 13 = 16
                if (aPiecesSpan[4] == aPiecesSpan[7] && aPiecesSpan[13] == aPiecesSpan[16])
                {
                    // Section 2: Advanced F2L
                    // 36 advanced cases: 1 piece is in the wrong slot.
                    // Section 2A: Edge is in the wrong slot.
                    // White sticker faces Up
                    // 1.  U' R' U R2 U' R'
                    if (aPiecesSpan[49] == aPiecesSpan[44] && aPiecesSpan[4] == aPiecesSpan[9] && aPiecesSpan[4] == aPiecesSpan[21] && aPiecesSpan[13] == aPiecesSpan[2] && aPiecesSpan[13] == aPiecesSpan[14])
                    {
                        _ = MakeTurnAsync("U' R' U R2 U' R'");
                        continue;
                    }

                    // 2.  y U L U' L2' U L
                    if (aPiecesSpan[49] == aPiecesSpan[44] && aPiecesSpan[4] == aPiecesSpan[3] && aPiecesSpan[4] == aPiecesSpan[9] && aPiecesSpan[13] == aPiecesSpan[2] && aPiecesSpan[13] == aPiecesSpan[32])
                    {
                        _ = MakeTurnAsync("y U L U' L2 U L");
                        continue;
                    }

                    // 3.  U2 (R' U R) U' (S R S') or y R' F R2 U' R' U2 F' or y2 U2 (L F' L' F) (L U L')
                    if (aPiecesSpan[49] == aPiecesSpan[44] && aPiecesSpan[4] == aPiecesSpan[9] && aPiecesSpan[4] == aPiecesSpan[14] && aPiecesSpan[13] == aPiecesSpan[2] && aPiecesSpan[13] == aPiecesSpan[21])
                    {
                        _ = MakeTurnAsync("U2 R' U R U' S R S'");
                        continue;
                    }

                    // 4.  y U2 (L U' L') U (S' L' S) or L F' L2' U L U2' F or y' U2 (R' F R F') (R' U' R)
                    if (aPiecesSpan[49] == aPiecesSpan[44] && aPiecesSpan[4] == aPiecesSpan[9] && aPiecesSpan[4] == aPiecesSpan[32] && aPiecesSpan[13] == aPiecesSpan[2] && aPiecesSpan[13] == aPiecesSpan[3])
                    {
                        _ = MakeTurnAsync("y U2 L U' L' U S' L' S");
                        continue;
                    }
                }
            }

            return Task.FromResult(true);
        }

        /// <summary>
        /// OLL (Orientation of Last Layer) - 2-Look OLL
        /// </summary>
        /// <returns></returns>
        private static Task<bool> SolveBottomLayerOrientation()
        {
            // Create a span for the array aPieces
            ReadOnlySpan<string> aPiecesSpan = aPieces;

            int nLoopTimes = 0;

            while (true)
            {
                nLoopTimes++;
                if (nLoopTimes > nLoopTimesMax)
                {
                    Debug.WriteLine("CFOP: nLoopTimes last layer orientation: " + nLoopTimes);
                    return Task.FromResult(false);
                }

                // If solved, break the loop
                if (aPiecesSpan[40] == aPiecesSpan[36] && aPiecesSpan[40] == aPiecesSpan[37] && aPiecesSpan[40] == aPiecesSpan[38] && aPiecesSpan[40] == aPiecesSpan[39] && aPiecesSpan[40] == aPiecesSpan[41] && aPiecesSpan[40] == aPiecesSpan[42] && aPiecesSpan[40] == aPiecesSpan[43] && aPiecesSpan[40] == aPiecesSpan[44])
                {
                    Debug.WriteLine("CFOP: number of turns last layer orientation: " + lCubeTurns.Count);
                    break;
                }

                // Turn the cube
                if (nLoopTimes > 1)
                {
                    _ = MakeTurnAsync("y");
                }

                // https://ruwix.com/the-rubiks-cube/advanced-cfop-fridrich/orient-the-last-layer-oll/
                // Dot 1
                if (aPiecesSpan[40] == aPiecesSpan[1] && aPiecesSpan[40] == aPiecesSpan[9] && aPiecesSpan[40] == aPiecesSpan[10] && aPiecesSpan[40] == aPiecesSpan[11] && aPiecesSpan[40] == aPiecesSpan[19] && aPiecesSpan[40] == aPiecesSpan[27] && aPiecesSpan[40] == aPiecesSpan[28] && aPiecesSpan[40] == aPiecesSpan[29])
                {
                    _ = MakeTurnAsync("R U B' l U l2 x' U' R' F R F'");
                    continue;
                }

                // Dot 2
                if (aPiecesSpan[40] == aPiecesSpan[0] && aPiecesSpan[40] == aPiecesSpan[1] && aPiecesSpan[40] == aPiecesSpan[2] && aPiecesSpan[40] == aPiecesSpan[10] && aPiecesSpan[40] == aPiecesSpan[11] && aPiecesSpan[40] == aPiecesSpan[19] && aPiecesSpan[40] == aPiecesSpan[27] && aPiecesSpan[40] == aPiecesSpan[28])
                {
                    _ = MakeTurnAsync("R' F R F' U2 R' F R y' R2 U2 R");
                    continue;
                }

                // Dot 3
                if (aPiecesSpan[40] == aPiecesSpan[1] && aPiecesSpan[40] == aPiecesSpan[10] && aPiecesSpan[40] == aPiecesSpan[11] && aPiecesSpan[40] == aPiecesSpan[19] && aPiecesSpan[40] == aPiecesSpan[20] && aPiecesSpan[40] == aPiecesSpan[28] && aPiecesSpan[40] == aPiecesSpan[29] && aPiecesSpan[40] == aPiecesSpan[44])
                {
                    _ = MakeTurnAsync("y L' R2 B R' B L U2 L' B M'");
                    continue;
                }

                // Dot 4
                if (aPiecesSpan[40] == aPiecesSpan[0] && aPiecesSpan[40] == aPiecesSpan[1] && aPiecesSpan[40] == aPiecesSpan[9] && aPiecesSpan[40] == aPiecesSpan[10] && aPiecesSpan[40] == aPiecesSpan[19] && aPiecesSpan[40] == aPiecesSpan[27] && aPiecesSpan[40] == aPiecesSpan[28] && aPiecesSpan[40] == aPiecesSpan[38])
                {
                    _ = MakeTurnAsync("R' U2 x R' U R U' y R' U' R' U R' F");
                    continue;
                }

                // Dot 5
                if (aPiecesSpan[40] == aPiecesSpan[1] && aPiecesSpan[40] == aPiecesSpan[10] && aPiecesSpan[40] == aPiecesSpan[18] && aPiecesSpan[40] == aPiecesSpan[19] && aPiecesSpan[40] == aPiecesSpan[28] && aPiecesSpan[40] == aPiecesSpan[29] && aPiecesSpan[40] == aPiecesSpan[36] && aPiecesSpan[40] == aPiecesSpan[44])
                {
                    _ = MakeTurnAsync("R U R' U R' F R F' U2 R' F R F'");
                    continue;
                }

                // Dot 6
                if (aPiecesSpan[40] == aPiecesSpan[1] && aPiecesSpan[40] == aPiecesSpan[10] && aPiecesSpan[40] == aPiecesSpan[19] && aPiecesSpan[40] == aPiecesSpan[28] && aPiecesSpan[40] == aPiecesSpan[36] && aPiecesSpan[40] == aPiecesSpan[38] && aPiecesSpan[40] == aPiecesSpan[42] && aPiecesSpan[40] == aPiecesSpan[44])
                {
                    _ = MakeTurnAsync("M' U2 M U2 M' U M U2 M' U2 M");
                    continue;
                }

                // Dot 7
                if (aPiecesSpan[40] == aPiecesSpan[1] && aPiecesSpan[40] == aPiecesSpan[9] && aPiecesSpan[40] == aPiecesSpan[10] && aPiecesSpan[40] == aPiecesSpan[19] && aPiecesSpan[40] == aPiecesSpan[28] && aPiecesSpan[40] == aPiecesSpan[29] && aPiecesSpan[40] == aPiecesSpan[36] && aPiecesSpan[40] == aPiecesSpan[38])
                {
                    _ = MakeTurnAsync("R' U2 F R U R' U' y' R2 U2 x' R U");
                    continue;
                }

                // Dot 8
                if (aPiecesSpan[40] == aPiecesSpan[1] && aPiecesSpan[40] == aPiecesSpan[10] && aPiecesSpan[40] == aPiecesSpan[18] && aPiecesSpan[40] == aPiecesSpan[19] && aPiecesSpan[40] == aPiecesSpan[20] && aPiecesSpan[40] == aPiecesSpan[28] && aPiecesSpan[40] == aPiecesSpan[42] && aPiecesSpan[40] == aPiecesSpan[44])
                {
                    _ = MakeTurnAsync("F R U R' U y' R' U2 R' F R F'");
                    continue;
                }

                // Line 1
                if (aPiecesSpan[40] == aPiecesSpan[0] && aPiecesSpan[40] == aPiecesSpan[9] && aPiecesSpan[40] == aPiecesSpan[10] && aPiecesSpan[40] == aPiecesSpan[11] && aPiecesSpan[40] == aPiecesSpan[20] && aPiecesSpan[40] == aPiecesSpan[28] && aPiecesSpan[40] == aPiecesSpan[37] && aPiecesSpan[40] == aPiecesSpan[43])
                {
                    _ = MakeTurnAsync("R' U' y L' U L' y' L F L' F R");
                    continue;
                }

                // Line 2
                if (aPiecesSpan[40] == aPiecesSpan[9] && aPiecesSpan[40] == aPiecesSpan[10] && aPiecesSpan[40] == aPiecesSpan[11] && aPiecesSpan[40] == aPiecesSpan[27] && aPiecesSpan[40] == aPiecesSpan[28] && aPiecesSpan[40] == aPiecesSpan[29] && aPiecesSpan[40] == aPiecesSpan[37] && aPiecesSpan[40] == aPiecesSpan[43])
                {
                    _ = MakeTurnAsync("R U' y R2 D R' U2 R D' R2 d R'");
                    continue;
                }

                // Line 3
                if (aPiecesSpan[40] == aPiecesSpan[0] && aPiecesSpan[40] == aPiecesSpan[1] && aPiecesSpan[40] == aPiecesSpan[9] && aPiecesSpan[40] == aPiecesSpan[11] && aPiecesSpan[40] == aPiecesSpan[19] && aPiecesSpan[40] == aPiecesSpan[20] && aPiecesSpan[40] == aPiecesSpan[39] && aPiecesSpan[40] == aPiecesSpan[41])
                {
                    _ = MakeTurnAsync("F U R U' R' U R U' R' F'");
                    continue;
                }

                // Line 4
                if (aPiecesSpan[40] == aPiecesSpan[1] && aPiecesSpan[40] == aPiecesSpan[9] && aPiecesSpan[40] == aPiecesSpan[11] && aPiecesSpan[40] == aPiecesSpan[19] && aPiecesSpan[40] == aPiecesSpan[27] && aPiecesSpan[40] == aPiecesSpan[29] && aPiecesSpan[40] == aPiecesSpan[39] && aPiecesSpan[40] == aPiecesSpan[41])
                {
                    _ = MakeTurnAsync("L' B' L U' R' U R U' R' U R L' B L");
                    continue;
                }

                // Cross 1
                if (aPiecesSpan[40] == aPiecesSpan[0] && aPiecesSpan[40] == aPiecesSpan[9] && aPiecesSpan[40] == aPiecesSpan[11] && aPiecesSpan[40] == aPiecesSpan[20] && aPiecesSpan[40] == aPiecesSpan[37] && aPiecesSpan[40] == aPiecesSpan[39] && aPiecesSpan[40] == aPiecesSpan[41] && aPiecesSpan[40] == aPiecesSpan[43])
                {
                    _ = MakeTurnAsync("L U' R' U L' U R U R' U R");
                    continue;
                }

                // Cross 2
                if (aPiecesSpan[40] == aPiecesSpan[9] && aPiecesSpan[40] == aPiecesSpan[11] && aPiecesSpan[40] == aPiecesSpan[27] && aPiecesSpan[40] == aPiecesSpan[29] && aPiecesSpan[40] == aPiecesSpan[37] && aPiecesSpan[40] == aPiecesSpan[39] && aPiecesSpan[40] == aPiecesSpan[41] && aPiecesSpan[40] == aPiecesSpan[43])
                {
                    _ = MakeTurnAsync("R U R' U R U' R' U R U2 R'");
                    continue;
                }

                // Cross 3
                if (aPiecesSpan[40] == aPiecesSpan[0] && aPiecesSpan[40] == aPiecesSpan[18] && aPiecesSpan[40] == aPiecesSpan[27] && aPiecesSpan[40] == aPiecesSpan[37] && aPiecesSpan[40] == aPiecesSpan[39] && aPiecesSpan[40] == aPiecesSpan[41] && aPiecesSpan[40] == aPiecesSpan[43] && aPiecesSpan[40] == aPiecesSpan[44])
                {
                    _ = MakeTurnAsync("L' U R U' L U R'");
                    continue;
                }

                // Cross 4
                if (aPiecesSpan[40] == aPiecesSpan[11] && aPiecesSpan[40] == aPiecesSpan[20] && aPiecesSpan[40] == aPiecesSpan[29] && aPiecesSpan[40] == aPiecesSpan[37] && aPiecesSpan[40] == aPiecesSpan[39] && aPiecesSpan[40] == aPiecesSpan[41] && aPiecesSpan[40] == aPiecesSpan[43] && aPiecesSpan[40] == aPiecesSpan[44])
                {
                    _ = MakeTurnAsync("R' U2 R U R' U R");
                    continue;
                }

                // Cross 5
                if (aPiecesSpan[40] == aPiecesSpan[2] && aPiecesSpan[40] == aPiecesSpan[18] && aPiecesSpan[40] == aPiecesSpan[36] && aPiecesSpan[40] == aPiecesSpan[37] && aPiecesSpan[40] == aPiecesSpan[39] && aPiecesSpan[40] == aPiecesSpan[41] && aPiecesSpan[40] == aPiecesSpan[42] && aPiecesSpan[40] == aPiecesSpan[43])
                {
                    _ = MakeTurnAsync("R' F' L F R F' L' F");
                    continue;
                }

                // Cross 6
                if (aPiecesSpan[40] == aPiecesSpan[0] && aPiecesSpan[40] == aPiecesSpan[2] && aPiecesSpan[40] == aPiecesSpan[36] && aPiecesSpan[40] == aPiecesSpan[37] && aPiecesSpan[40] == aPiecesSpan[38] && aPiecesSpan[40] == aPiecesSpan[39] && aPiecesSpan[40] == aPiecesSpan[41] && aPiecesSpan[40] == aPiecesSpan[43])
                {
                    _ = MakeTurnAsync("R2 D R' U2 R D' R' U2 R'");
                    continue;
                }

                // Cross 7
                if (aPiecesSpan[40] == aPiecesSpan[18] && aPiecesSpan[40] == aPiecesSpan[29] && aPiecesSpan[40] == aPiecesSpan[36] && aPiecesSpan[40] == aPiecesSpan[37] && aPiecesSpan[40] == aPiecesSpan[39] && aPiecesSpan[40] == aPiecesSpan[41] && aPiecesSpan[40] == aPiecesSpan[43] && aPiecesSpan[40] == aPiecesSpan[44])
                {
                    _ = MakeTurnAsync("R' F' L' F R F' L F");
                    continue;
                }

                // 4 corners 1
                if (aPiecesSpan[40] == aPiecesSpan[10] && aPiecesSpan[40] == aPiecesSpan[19] && aPiecesSpan[40] == aPiecesSpan[36] && aPiecesSpan[40] == aPiecesSpan[38] && aPiecesSpan[40] == aPiecesSpan[39] && aPiecesSpan[40] == aPiecesSpan[42] && aPiecesSpan[40] == aPiecesSpan[43] && aPiecesSpan[40] == aPiecesSpan[44])
                {
                    _ = MakeTurnAsync("M' U' M U2 M' U' M");
                    continue;
                }

                // 4 corners 2
                if (aPiecesSpan[40] == aPiecesSpan[1] && aPiecesSpan[40] == aPiecesSpan[19] && aPiecesSpan[40] == aPiecesSpan[36] && aPiecesSpan[40] == aPiecesSpan[38] && aPiecesSpan[40] == aPiecesSpan[39] && aPiecesSpan[40] == aPiecesSpan[41] && aPiecesSpan[40] == aPiecesSpan[42] && aPiecesSpan[40] == aPiecesSpan[44])
                {
                    _ = MakeTurnAsync("L' R U R' U' L R' F R F'");
                    continue;
                }

                // Shape _| 1
                if (aPiecesSpan[40] == aPiecesSpan[1] && aPiecesSpan[40] == aPiecesSpan[2] && aPiecesSpan[40] == aPiecesSpan[10] && aPiecesSpan[40] == aPiecesSpan[11] && aPiecesSpan[40] == aPiecesSpan[20] && aPiecesSpan[40] == aPiecesSpan[37] && aPiecesSpan[40] == aPiecesSpan[39] && aPiecesSpan[40] == aPiecesSpan[42])
                {
                    _ = MakeTurnAsync("L F R' F R F2 L'");
                    continue;
                }

                // Shape _| 2
                if (aPiecesSpan[40] == aPiecesSpan[0] && aPiecesSpan[40] == aPiecesSpan[1] && aPiecesSpan[40] == aPiecesSpan[10] && aPiecesSpan[40] == aPiecesSpan[11] && aPiecesSpan[40] == aPiecesSpan[36] && aPiecesSpan[40] == aPiecesSpan[37] && aPiecesSpan[40] == aPiecesSpan[39] && aPiecesSpan[40] == aPiecesSpan[44])
                {
                    _ = MakeTurnAsync("F R' F' R U R U' R'");
                    continue;
                }

                // Shape _| 3
                if (aPiecesSpan[40] == aPiecesSpan[0] && aPiecesSpan[40] == aPiecesSpan[1] && aPiecesSpan[40] == aPiecesSpan[10] && aPiecesSpan[40] == aPiecesSpan[18] && aPiecesSpan[40] == aPiecesSpan[27] && aPiecesSpan[40] == aPiecesSpan[37] && aPiecesSpan[40] == aPiecesSpan[39] && aPiecesSpan[40] == aPiecesSpan[44])
                {
                    _ = MakeTurnAsync("R' U' R y' x' R U' R' F R U R'");
                    continue;
                }

                // Shape _| 4
                if (aPiecesSpan[40] == aPiecesSpan[1] && aPiecesSpan[40] == aPiecesSpan[10] && aPiecesSpan[40] == aPiecesSpan[18] && aPiecesSpan[40] == aPiecesSpan[20] && aPiecesSpan[40] == aPiecesSpan[37] && aPiecesSpan[40] == aPiecesSpan[39] && aPiecesSpan[40] == aPiecesSpan[42] && aPiecesSpan[40] == aPiecesSpan[44])
                {
                    _ = MakeTurnAsync("U' R U2 R' U' R U' R2 y' R' U' R U B");
                    continue;
                }

                // Shape _| 5
                if (aPiecesSpan[40] == aPiecesSpan[1] && aPiecesSpan[40] == aPiecesSpan[2] && aPiecesSpan[40] == aPiecesSpan[10] && aPiecesSpan[40] == aPiecesSpan[18] && aPiecesSpan[40] == aPiecesSpan[27] && aPiecesSpan[40] == aPiecesSpan[29] && aPiecesSpan[40] == aPiecesSpan[37] && aPiecesSpan[40] == aPiecesSpan[39])
                {
                    _ = MakeTurnAsync("F R U R' U' R U R' U' F'");
                    continue;
                }

                // Shape _| 6
                if (aPiecesSpan[40] == aPiecesSpan[0] && aPiecesSpan[40] == aPiecesSpan[1] && aPiecesSpan[40] == aPiecesSpan[2] && aPiecesSpan[40] == aPiecesSpan[10] && aPiecesSpan[40] == aPiecesSpan[18] && aPiecesSpan[40] == aPiecesSpan[20] && aPiecesSpan[40] == aPiecesSpan[37] && aPiecesSpan[40] == aPiecesSpan[39])
                {
                    _ = MakeTurnAsync("L F' L' F U2 L2 y' L F L' F");
                    continue;
                }

                // Shape |_ 1
                if (aPiecesSpan[40] == aPiecesSpan[1] && aPiecesSpan[40] == aPiecesSpan[18] && aPiecesSpan[40] == aPiecesSpan[20] && aPiecesSpan[40] == aPiecesSpan[28] && aPiecesSpan[40] == aPiecesSpan[37] && aPiecesSpan[40] == aPiecesSpan[41] && aPiecesSpan[40] == aPiecesSpan[42] && aPiecesSpan[40] == aPiecesSpan[44])
                {
                    _ = MakeTurnAsync("U' R' U2 R U R' U R2 y R U R' U' F'");
                    continue;
                }

                // Shape |_ 2
                if (aPiecesSpan[40] == aPiecesSpan[0] && aPiecesSpan[40] == aPiecesSpan[1] && aPiecesSpan[40] == aPiecesSpan[9] && aPiecesSpan[40] == aPiecesSpan[27] && aPiecesSpan[40] == aPiecesSpan[28] && aPiecesSpan[40] == aPiecesSpan[37] && aPiecesSpan[40] == aPiecesSpan[38] && aPiecesSpan[40] == aPiecesSpan[41])
                {
                    _ = MakeTurnAsync("r U2 R' U' R U' r'");
                    continue;
                }

                // Shape |_ 3
                if (aPiecesSpan[40] == aPiecesSpan[1] && aPiecesSpan[40] == aPiecesSpan[9] && aPiecesSpan[40] == aPiecesSpan[20] && aPiecesSpan[40] == aPiecesSpan[28] && aPiecesSpan[40] == aPiecesSpan[37] && aPiecesSpan[40] == aPiecesSpan[38] && aPiecesSpan[40] == aPiecesSpan[41] && aPiecesSpan[40] == aPiecesSpan[42])
                {
                    _ = MakeTurnAsync("R' U2 l R U' R' U l' U2 R");
                    continue;
                }

                // Shape |_ 4
                if (aPiecesSpan[40] == aPiecesSpan[0] && aPiecesSpan[40] == aPiecesSpan[1] && aPiecesSpan[40] == aPiecesSpan[9] && aPiecesSpan[40] == aPiecesSpan[11] && aPiecesSpan[40] == aPiecesSpan[20] && aPiecesSpan[40] == aPiecesSpan[28] && aPiecesSpan[40] == aPiecesSpan[37] && aPiecesSpan[40] == aPiecesSpan[41])
                {
                    _ = MakeTurnAsync("F' L' U' L U L' U' L U F");
                    continue;
                }

                // Shape |_ 5
                if (aPiecesSpan[40] == aPiecesSpan[1] && aPiecesSpan[40] == aPiecesSpan[2] && aPiecesSpan[40] == aPiecesSpan[18] && aPiecesSpan[40] == aPiecesSpan[27] && aPiecesSpan[40] == aPiecesSpan[28] && aPiecesSpan[40] == aPiecesSpan[29] && aPiecesSpan[40] == aPiecesSpan[37] && aPiecesSpan[40] == aPiecesSpan[41])
                {
                    _ = MakeTurnAsync("R' F R' F' R2 U2 x' U' R U R'");
                    continue;
                }

                // Shape |_ 6
                if (aPiecesSpan[40] == aPiecesSpan[0] && aPiecesSpan[40] == aPiecesSpan[1] && aPiecesSpan[40] == aPiecesSpan[2] && aPiecesSpan[40] == aPiecesSpan[18] && aPiecesSpan[40] == aPiecesSpan[20] && aPiecesSpan[40] == aPiecesSpan[28] && aPiecesSpan[40] == aPiecesSpan[37] && aPiecesSpan[40] == aPiecesSpan[41])
                {
                    _ = MakeTurnAsync("R' F R F' U2 R2 y R' F' R F'");
                    continue;
                }

                // Shape -| 1
                if (aPiecesSpan[40] == aPiecesSpan[2] && aPiecesSpan[40] == aPiecesSpan[10] && aPiecesSpan[40] == aPiecesSpan[19] && aPiecesSpan[40] == aPiecesSpan[20] && aPiecesSpan[40] == aPiecesSpan[29] && aPiecesSpan[40] == aPiecesSpan[38] && aPiecesSpan[40] == aPiecesSpan[39] && aPiecesSpan[40] == aPiecesSpan[43])
                {
                    _ = MakeTurnAsync("R U R' y R' F R U' R' F' R");
                    continue;
                }

                // Shape -| 2
                if (aPiecesSpan[40] == aPiecesSpan[0] && aPiecesSpan[40] == aPiecesSpan[9] && aPiecesSpan[40] == aPiecesSpan[10] && aPiecesSpan[40] == aPiecesSpan[18] && aPiecesSpan[40] == aPiecesSpan[19] && aPiecesSpan[40] == aPiecesSpan[36] && aPiecesSpan[40] == aPiecesSpan[39] && aPiecesSpan[40] == aPiecesSpan[43])
                {
                    _ = MakeTurnAsync("L' B' L U' R' U R L' B L");
                    continue;
                }

                // Shape -| 3
                if (aPiecesSpan[40] == aPiecesSpan[0] && aPiecesSpan[40] == aPiecesSpan[10] && aPiecesSpan[40] == aPiecesSpan[18] && aPiecesSpan[40] == aPiecesSpan[19] && aPiecesSpan[40] == aPiecesSpan[27] && aPiecesSpan[40] == aPiecesSpan[39] && aPiecesSpan[40] == aPiecesSpan[43] && aPiecesSpan[40] == aPiecesSpan[44])
                {
                    _ = MakeTurnAsync("U2 r R2 U' R U' R' U2 R U' M");
                    continue;
                }

                // Shape -| 4
                if (aPiecesSpan[40] == aPiecesSpan[9] && aPiecesSpan[40] == aPiecesSpan[10] && aPiecesSpan[40] == aPiecesSpan[19] && aPiecesSpan[40] == aPiecesSpan[29] && aPiecesSpan[40] == aPiecesSpan[36] && aPiecesSpan[40] == aPiecesSpan[38] && aPiecesSpan[40] == aPiecesSpan[39] && aPiecesSpan[40] == aPiecesSpan[43])
                {
                    _ = MakeTurnAsync("x' U' R U' R2 F x R U R' U' R B2");
                    continue;
                }

                // Shape |- 1
                if (aPiecesSpan[40] == aPiecesSpan[2] && aPiecesSpan[40] == aPiecesSpan[18] && aPiecesSpan[40] == aPiecesSpan[19] && aPiecesSpan[40] == aPiecesSpan[27] && aPiecesSpan[40] == aPiecesSpan[28] && aPiecesSpan[40] == aPiecesSpan[29] && aPiecesSpan[40] == aPiecesSpan[41] && aPiecesSpan[40] == aPiecesSpan[43])
                {
                    _ = MakeTurnAsync("L U' y' R' U2 R' U R U' R U2 R d' L'");
                    continue;
                }

                // Shape |- 2
                if (aPiecesSpan[40] == aPiecesSpan[2] && aPiecesSpan[40] == aPiecesSpan[11] && aPiecesSpan[40] == aPiecesSpan[19] && aPiecesSpan[40] == aPiecesSpan[20] && aPiecesSpan[40] == aPiecesSpan[28] && aPiecesSpan[40] == aPiecesSpan[41] && aPiecesSpan[40] == aPiecesSpan[42] && aPiecesSpan[40] == aPiecesSpan[43])
                {
                    _ = MakeTurnAsync("U2 l' L2 U L' U L U2 L' U M");
                    continue;
                }

                // Shape |- 3
                if (aPiecesSpan[40] == aPiecesSpan[9] && aPiecesSpan[40] == aPiecesSpan[19] && aPiecesSpan[40] == aPiecesSpan[28] && aPiecesSpan[40] == aPiecesSpan[29] && aPiecesSpan[40] == aPiecesSpan[36] && aPiecesSpan[40] == aPiecesSpan[38] && aPiecesSpan[40] == aPiecesSpan[41] && aPiecesSpan[40] == aPiecesSpan[43])
                {
                    _ = MakeTurnAsync("R2 U R' B' R U' R2 U l U l'");
                    continue;
                }

                // Shape |- 4
                if (aPiecesSpan[40] == aPiecesSpan[11] && aPiecesSpan[40] == aPiecesSpan[19] && aPiecesSpan[40] == aPiecesSpan[20] && aPiecesSpan[40] == aPiecesSpan[28] && aPiecesSpan[40] == aPiecesSpan[29] && aPiecesSpan[40] == aPiecesSpan[41] && aPiecesSpan[40] == aPiecesSpan[43] && aPiecesSpan[40] == aPiecesSpan[44])
                {
                    _ = MakeTurnAsync("r' U2 R U R' U r");
                    continue;
                }

                // C 1
                if (aPiecesSpan[40] == aPiecesSpan[9] && aPiecesSpan[40] == aPiecesSpan[10] && aPiecesSpan[40] == aPiecesSpan[11] && aPiecesSpan[40] == aPiecesSpan[28] && aPiecesSpan[40] == aPiecesSpan[36] && aPiecesSpan[40] == aPiecesSpan[37] && aPiecesSpan[40] == aPiecesSpan[42] && aPiecesSpan[40] == aPiecesSpan[43])
                {
                    _ = MakeTurnAsync("R U x' R U' R' U x U' R'");
                    continue;
                }

                // C 2
                if (aPiecesSpan[40] == aPiecesSpan[1] && aPiecesSpan[40] == aPiecesSpan[11] && aPiecesSpan[40] == aPiecesSpan[19] && aPiecesSpan[40] == aPiecesSpan[27] && aPiecesSpan[40] == aPiecesSpan[39] && aPiecesSpan[40] == aPiecesSpan[41] && aPiecesSpan[40] == aPiecesSpan[42] && aPiecesSpan[40] == aPiecesSpan[44])
                {
                    _ = MakeTurnAsync("R U R' U' x D' R' U R E'");
                    continue;
                }

                // L 1
                if (aPiecesSpan[40] == aPiecesSpan[0] && aPiecesSpan[40] == aPiecesSpan[1] && aPiecesSpan[40] == aPiecesSpan[18] && aPiecesSpan[40] == aPiecesSpan[19] && aPiecesSpan[40] == aPiecesSpan[27] && aPiecesSpan[40] == aPiecesSpan[39] && aPiecesSpan[40] == aPiecesSpan[41] && aPiecesSpan[40] == aPiecesSpan[44])
                {
                    _ = MakeTurnAsync("R' F R U R' F' R y L U' L'");
                    continue;
                }

                // L 2
                if (aPiecesSpan[40] == aPiecesSpan[1] && aPiecesSpan[40] == aPiecesSpan[2] && aPiecesSpan[40] == aPiecesSpan[11] && aPiecesSpan[40] == aPiecesSpan[19] && aPiecesSpan[40] == aPiecesSpan[20] && aPiecesSpan[40] == aPiecesSpan[39] && aPiecesSpan[40] == aPiecesSpan[41] && aPiecesSpan[40] == aPiecesSpan[42])
                {
                    _ = MakeTurnAsync("L F' L' U' L F L' y' R' U R");
                    continue;
                }

                // L 3
                if (aPiecesSpan[40] == aPiecesSpan[1] && aPiecesSpan[40] == aPiecesSpan[11] && aPiecesSpan[40] == aPiecesSpan[19] && aPiecesSpan[40] == aPiecesSpan[20] && aPiecesSpan[40] == aPiecesSpan[29] && aPiecesSpan[40] == aPiecesSpan[39] && aPiecesSpan[40] == aPiecesSpan[41] && aPiecesSpan[40] == aPiecesSpan[44])
                {
                    _ = MakeTurnAsync("L' B' L R' U' R U L' B L");
                    continue;
                }

                // L 4
                if (aPiecesSpan[40] == aPiecesSpan[1] && aPiecesSpan[40] == aPiecesSpan[9] && aPiecesSpan[40] == aPiecesSpan[18] && aPiecesSpan[40] == aPiecesSpan[19] && aPiecesSpan[40] == aPiecesSpan[27] && aPiecesSpan[40] == aPiecesSpan[39] && aPiecesSpan[40] == aPiecesSpan[41] && aPiecesSpan[40] == aPiecesSpan[42])
                {
                    _ = MakeTurnAsync("R B R' L U L' U' R B' R'");
                    continue;
                }

                // P 1
                if (aPiecesSpan[40] == aPiecesSpan[1] && aPiecesSpan[40] == aPiecesSpan[9] && aPiecesSpan[40] == aPiecesSpan[10] && aPiecesSpan[40] == aPiecesSpan[11] && aPiecesSpan[40] == aPiecesSpan[36] && aPiecesSpan[40] == aPiecesSpan[37] && aPiecesSpan[40] == aPiecesSpan[39] && aPiecesSpan[40] == aPiecesSpan[42])
                {
                    _ = MakeTurnAsync("F U R U' R' F'");
                    continue;
                }

                // P 2
                if (aPiecesSpan[40] == aPiecesSpan[0] && aPiecesSpan[40] == aPiecesSpan[1] && aPiecesSpan[40] == aPiecesSpan[20] && aPiecesSpan[40] == aPiecesSpan[28] && aPiecesSpan[40] == aPiecesSpan[37] && aPiecesSpan[40] == aPiecesSpan[38] && aPiecesSpan[40] == aPiecesSpan[41] && aPiecesSpan[40] == aPiecesSpan[44])
                {
                    _ = MakeTurnAsync("R' d' L d R U' R' F' R");
                    continue;
                }

                // P 3
                if (aPiecesSpan[40] == aPiecesSpan[1] && aPiecesSpan[40] == aPiecesSpan[2] && aPiecesSpan[40] == aPiecesSpan[10] && aPiecesSpan[40] == aPiecesSpan[18] && aPiecesSpan[40] == aPiecesSpan[36] && aPiecesSpan[40] == aPiecesSpan[37] && aPiecesSpan[40] == aPiecesSpan[39] && aPiecesSpan[40] == aPiecesSpan[42])
                {
                    _ = MakeTurnAsync("L d R' d' L' U L F L'");
                    continue;
                }

                // P 4
                if (aPiecesSpan[40] == aPiecesSpan[1] && aPiecesSpan[40] == aPiecesSpan[27] && aPiecesSpan[40] == aPiecesSpan[28] && aPiecesSpan[40] == aPiecesSpan[29] && aPiecesSpan[40] == aPiecesSpan[37] && aPiecesSpan[40] == aPiecesSpan[38] && aPiecesSpan[40] == aPiecesSpan[41] && aPiecesSpan[40] == aPiecesSpan[44])
                {
                    _ = MakeTurnAsync("F' U' L' U L F");
                    continue;
                }

                // T 1
                if (aPiecesSpan[40] == aPiecesSpan[1] && aPiecesSpan[40] == aPiecesSpan[19] && aPiecesSpan[40] == aPiecesSpan[27] && aPiecesSpan[40] == aPiecesSpan[29] && aPiecesSpan[40] == aPiecesSpan[38] && aPiecesSpan[40] == aPiecesSpan[39] && aPiecesSpan[40] == aPiecesSpan[41] && aPiecesSpan[40] == aPiecesSpan[44])
                {
                    _ = MakeTurnAsync("F R U R' U' F'");
                    continue;
                }

                // T 2
                if (aPiecesSpan[40] == aPiecesSpan[0] && aPiecesSpan[40] == aPiecesSpan[1] && aPiecesSpan[40] == aPiecesSpan[19] && aPiecesSpan[40] == aPiecesSpan[20] && aPiecesSpan[40] == aPiecesSpan[38] && aPiecesSpan[40] == aPiecesSpan[39] && aPiecesSpan[40] == aPiecesSpan[41] && aPiecesSpan[40] == aPiecesSpan[44])
                {
                    _ = MakeTurnAsync("R U R' U' R' F R F'");
                    continue;
                }

                // W 1
                if (aPiecesSpan[40] == aPiecesSpan[2] && aPiecesSpan[40] == aPiecesSpan[19] && aPiecesSpan[40] == aPiecesSpan[27] && aPiecesSpan[40] == aPiecesSpan[28] && aPiecesSpan[40] == aPiecesSpan[38] && aPiecesSpan[40] == aPiecesSpan[41] && aPiecesSpan[40] == aPiecesSpan[42] && aPiecesSpan[40] == aPiecesSpan[43])
                {
                    _ = MakeTurnAsync("L U L' U L U' L' U' y2 R' F R F'");
                    continue;
                }

                // W 2
                if (aPiecesSpan[40] == aPiecesSpan[0] && aPiecesSpan[40] == aPiecesSpan[10] && aPiecesSpan[40] == aPiecesSpan[11] && aPiecesSpan[40] == aPiecesSpan[19] && aPiecesSpan[40] == aPiecesSpan[36] && aPiecesSpan[40] == aPiecesSpan[39] && aPiecesSpan[40] == aPiecesSpan[43] && aPiecesSpan[40] == aPiecesSpan[44])
                {
                    _ = MakeTurnAsync("R' U' R U' R' U R U y F R' F' R");
                    continue;
                }

                // Z 1
                if (aPiecesSpan[40] == aPiecesSpan[1] && aPiecesSpan[40] == aPiecesSpan[18] && aPiecesSpan[40] == aPiecesSpan[19] && aPiecesSpan[40] == aPiecesSpan[29] && aPiecesSpan[40] == aPiecesSpan[36] && aPiecesSpan[40] == aPiecesSpan[39] && aPiecesSpan[40] == aPiecesSpan[41] && aPiecesSpan[40] == aPiecesSpan[44])
                {
                    _ = MakeTurnAsync("R' F R U R' U' y L' d R");
                    continue;
                }

                // Z 2
                if (aPiecesSpan[40] == aPiecesSpan[1] && aPiecesSpan[40] == aPiecesSpan[9] && aPiecesSpan[40] == aPiecesSpan[19] && aPiecesSpan[40] == aPiecesSpan[20] && aPiecesSpan[40] == aPiecesSpan[38] && aPiecesSpan[40] == aPiecesSpan[39] && aPiecesSpan[40] == aPiecesSpan[41] && aPiecesSpan[40] == aPiecesSpan[42])
                {
                    _ = MakeTurnAsync("L F' L' U' L U y' R d' L'");
                    continue;
                }
            }

            return Task.FromResult(true);
        }

        /// <summary>
        ///  Permutate the last layer - PLL
        /// </summary>
        /// <returns></returns>
        private static Task<bool> SolveBottomLayerPermutation()
        {
            // Create a span for the array aPieces
            ReadOnlySpan<string> aPiecesSpan = aPieces;

            _ = ClassSolveCubeCommon.SolveTopLayerLineUpCenterAsync();

            int nLoopTimes = 0;

            while (true)
            {
                nLoopTimes++;
                if (nLoopTimes > nLoopTimesMax)
                {
                    Debug.WriteLine("CFOP: nLoopTimes last layer permutation: " + nLoopTimes);
                    return Task.FromResult(false);
                }

                // If solved, break the loop
                if (aPiecesSpan[40] == aPiecesSpan[36] && aPiecesSpan[40] == aPiecesSpan[37] && aPiecesSpan[40] == aPiecesSpan[38] && aPiecesSpan[40] == aPiecesSpan[39] && aPiecesSpan[40] == aPiecesSpan[41] && aPiecesSpan[40] == aPiecesSpan[42] && aPiecesSpan[40] == aPiecesSpan[43] && aPiecesSpan[40] == aPiecesSpan[44])
                {
                    if (aPiecesSpan[4] == aPiecesSpan[0] && aPiecesSpan[4] == aPiecesSpan[1] && aPiecesSpan[4] == aPiecesSpan[2] && aPiecesSpan[13] == aPiecesSpan[9] && aPiecesSpan[13] == aPiecesSpan[10] && aPiecesSpan[13] == aPiecesSpan[11] && aPiecesSpan[22] == aPiecesSpan[18] && aPiecesSpan[22] == aPiecesSpan[19] && aPiecesSpan[22] == aPiecesSpan[20] && aPiecesSpan[31] == aPiecesSpan[27] && aPiecesSpan[31] == aPiecesSpan[28] && aPiecesSpan[31] == aPiecesSpan[29])
                    {
                        Debug.WriteLine("CFOP: number of turns last layer permutation: " + lCubeTurns.Count);
                        break;
                    }
                }

                // Turn the cube
                if (nLoopTimes > 1)
                {
                    _ = MakeTurnAsync("y");
                }

                // https://solvethecube.com/algorithms
                // PLL Edges only
                // H
                if (aPiecesSpan[4] == aPiecesSpan[19] && aPiecesSpan[22] == aPiecesSpan[1] && aPiecesSpan[13] == aPiecesSpan[28] && aPiecesSpan[31] == aPiecesSpan[10])
                {
                    _ = MakeTurnAsync("M2 U M2 U2 M2 U M2");
                    continue;
                }

                // Z
                if (aPiecesSpan[4] == aPiecesSpan[10] && aPiecesSpan[13] == aPiecesSpan[1] && aPiecesSpan[22] == aPiecesSpan[28] && aPiecesSpan[31] == aPiecesSpan[19])
                {
                    _ = MakeTurnAsync("R' U' R2 U R U R' U' R U R U' R U' R' U2");
                    continue;
                }

                // Ua
                if (aPiecesSpan[13] == aPiecesSpan[28] && aPiecesSpan[22] == aPiecesSpan[10] && aPiecesSpan[31] == aPiecesSpan[19])
                {
                    _ = MakeTurnAsync("R2 U' R' U' R U R U R U' R");
                    continue;
                }

                // Ub
                if (aPiecesSpan[13] == aPiecesSpan[19] && aPiecesSpan[22] == aPiecesSpan[28] && aPiecesSpan[31] == aPiecesSpan[10])
                {
                    _ = MakeTurnAsync("R' U R' U' R' U' R' U R U R2");
                    continue;
                }

                // PLL Corners only
                // Aa
                if (aPiecesSpan[4] == aPiecesSpan[11] && aPiecesSpan[13] == aPiecesSpan[20] && aPiecesSpan[31] == aPiecesSpan[9])
                {
                    _ = MakeTurnAsync("x z' R2 U2 R' D' R U2 R' D R' z x'");
                    continue;
                }

                // Ab
                if (aPiecesSpan[4] == aPiecesSpan[20] && aPiecesSpan[13] == aPiecesSpan[2] && aPiecesSpan[22] == aPiecesSpan[11])
                {
                    _ = MakeTurnAsync("x R2 D2 R U R' D2 R U' R x'");
                    continue;
                }

                // E
                if (aPiecesSpan[13] == aPiecesSpan[0] && aPiecesSpan[31] == aPiecesSpan[2] && aPiecesSpan[13] == aPiecesSpan[20] && aPiecesSpan[31] == aPiecesSpan[18])
                {
                    _ = MakeTurnAsync("R2 U R' U' y R U R' U' R U R' U' R U R' y' R U' R2");
                    continue;
                }

                // PLL Edges and corners
                // T
                if (aPiecesSpan[4] == aPiecesSpan[11] && aPiecesSpan[22] == aPiecesSpan[9] && aPiecesSpan[13] == aPiecesSpan[28] && aPiecesSpan[31] == aPiecesSpan[10])
                {
                    _ = MakeTurnAsync("R U R' U' R' F R2 U' R' U' R U R' F'");
                    continue;
                }

                // Y
                if (aPiecesSpan[13] == aPiecesSpan[27] && aPiecesSpan[31] == aPiecesSpan[9] && aPiecesSpan[22] == aPiecesSpan[28] && aPiecesSpan[31] == aPiecesSpan[19])
                {
                    _ = MakeTurnAsync("F R U' R' U' R U R' F' R U R' U' R' F R F'");
                    continue;
                }

                // F
                if (aPiecesSpan[4] == aPiecesSpan[19] && aPiecesSpan[22] == aPiecesSpan[1] && aPiecesSpan[4] == aPiecesSpan[11] && aPiecesSpan[22] == aPiecesSpan[9])
                {
                    _ = MakeTurnAsync("U' R' U R U' R2 F' U' F U x R U R' U' R2 x'");
                    continue;
                }

                // V
                if (aPiecesSpan[4] == aPiecesSpan[20] && aPiecesSpan[22] == aPiecesSpan[2] && aPiecesSpan[13] == aPiecesSpan[19] && aPiecesSpan[22] == aPiecesSpan[10])
                {
                    _ = MakeTurnAsync("R' U R' U' y R' D R' D' R2 y' R' B' R B R");
                    continue;
                }

                // Ja
                if (aPiecesSpan[4] == aPiecesSpan[27] && aPiecesSpan[22] == aPiecesSpan[29] && aPiecesSpan[4] == aPiecesSpan[28] && aPiecesSpan[31] == aPiecesSpan[1])
                {
                    _ = MakeTurnAsync("L' U' L F L' U' L U L F' L2 U L U");
                    continue;
                }

                // Jb
                if (aPiecesSpan[4] == aPiecesSpan[11] && aPiecesSpan[22] == aPiecesSpan[9] && aPiecesSpan[4] == aPiecesSpan[10] && aPiecesSpan[13] == aPiecesSpan[1])
                {
                    _ = MakeTurnAsync("R U R' F' R U R' U' R' F R2 U' R' U'");
                    continue;
                }

                // Ra
                if (aPiecesSpan[4] == aPiecesSpan[28] && aPiecesSpan[31] == aPiecesSpan[1] && aPiecesSpan[13] == aPiecesSpan[20] && aPiecesSpan[31] == aPiecesSpan[18])
                {
                    _ = MakeTurnAsync("L U2 L' U2 L F' L' U' L U L F L2 U");
                    continue;
                }

                // Rb
                if (aPiecesSpan[4] == aPiecesSpan[10] && aPiecesSpan[13] == aPiecesSpan[1] && aPiecesSpan[13] == aPiecesSpan[20] && aPiecesSpan[31] == aPiecesSpan[18])
                {
                    _ = MakeTurnAsync("R' U2 R U2 R' F R U R' U' R' F' R2 U'");
                    continue;
                }

                // Na
                if (aPiecesSpan[4] == aPiecesSpan[18] && aPiecesSpan[22] == aPiecesSpan[0] && aPiecesSpan[13] == aPiecesSpan[28] && aPiecesSpan[31] == aPiecesSpan[10])
                {
                    _ = MakeTurnAsync("R U R' U R U R' F' R U R' U' R' F R2 U' R' U2 R U' R'");
                    continue;
                }

                // Nb
                if (aPiecesSpan[4] == aPiecesSpan[20] && aPiecesSpan[22] == aPiecesSpan[2] && aPiecesSpan[13] == aPiecesSpan[28] && aPiecesSpan[31] == aPiecesSpan[10])
                {
                    _ = MakeTurnAsync("R' U R U' R' F' U' F R U R' F R' F' R U' R");
                    continue;
                }

                // Ga
                if (aPiecesSpan[4] == aPiecesSpan[20] && aPiecesSpan[13] == aPiecesSpan[27] && aPiecesSpan[22] == aPiecesSpan[29])
                {
                    if (aPiecesSpan[4] == aPiecesSpan[28] && aPiecesSpan[22] == aPiecesSpan[1] && aPiecesSpan[31] == aPiecesSpan[19])
                    {
                        _ = MakeTurnAsync("y R2 u R' U R' U' R u' R2 y' R' U R");
                        continue;
                    }
                }

                // Gb
                if (aPiecesSpan[4] == aPiecesSpan[27] && aPiecesSpan[13] == aPiecesSpan[0] && aPiecesSpan[22] == aPiecesSpan[2])
                {
                    if (aPiecesSpan[4] == aPiecesSpan[19] && aPiecesSpan[31] == aPiecesSpan[1] && aPiecesSpan[22] == aPiecesSpan[28])
                    {
                        _ = MakeTurnAsync("R' U' R y R2 u R' U R U' R u' R2");
                        continue;
                    }
                }

                // Gc
                if (aPiecesSpan[4] == aPiecesSpan[29] && aPiecesSpan[31] == aPiecesSpan[11] && aPiecesSpan[22] == aPiecesSpan[9])
                {
                    if (aPiecesSpan[4] == aPiecesSpan[10] && aPiecesSpan[13] == aPiecesSpan[19] && aPiecesSpan[22] == aPiecesSpan[1])
                    {
                        _ = MakeTurnAsync("y R2 u' R U' R U R' u R2 y R U' R'");
                        continue;
                    }
                }

                // Gd
                if (aPiecesSpan[4] == aPiecesSpan[11] && aPiecesSpan[31] == aPiecesSpan[2] && aPiecesSpan[22] == aPiecesSpan[0])
                {
                    if (aPiecesSpan[4] == aPiecesSpan[19] && aPiecesSpan[13] == aPiecesSpan[1] && aPiecesSpan[22] == aPiecesSpan[10])
                    {
                        _ = MakeTurnAsync("y2 R U R' y' R2 u' R U' R' U R' u R2");
                        continue;
                    }
                }
            }

            return Task.FromResult(true);
        }
    }
}