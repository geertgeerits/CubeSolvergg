using System.Diagnostics;
using static CubeSolver.Globals;

namespace CubeSolver
{
    internal sealed class ClassSolveCubeCFOP_A
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
            if (!await SolveFirstTwoLayersAsync())
            {
                return false;
            }

            // OLL (Orientation of last layer) Bottom layer
            if (!await SolveBottomLayerOrientationAsync())
            {
                return false;
            }

            // OLL (Permutation of last layer) Bottom layer
            if (!await SolveBottomLayerPermutationAsync())
            {
                return false;
            }

            // Check if the cube is solved
            if (ClassColorsCube.CheckIfSolved())
            {
                // Stop the stopwatch and get the elapsed time
                //TimeSpan delta = Stopwatch.GetElapsedTime(startTime);
                //await Application.Current!.Windows[0].Page!.DisplayAlert("SolveTheCubeCFOPAsync", $"Time elapsed (hh:mm:ss.xxxxxxx): {delta}", "OK");

                return true;
            }

            return false;
        }

        /// <summary>
        /// Solve the first two layers (F2L)
        /// </summary>
        /// <returns></returns>
        private static async Task<bool> SolveFirstTwoLayersAsync()
        {
            //ClassProgramLogging.LogExecutedLine();

            string cT;
            int nLoopTimes = 0;

            while (true)
            {
                nLoopTimes++;
                if (nLoopTimes > nLoopTimesMax)
                {
                    Debug.WriteLine("CFOP: nLoopTimes first two layers: " + nLoopTimes);
                    return false;
                }

                // If solved, break the loop
                cT = aPieces[49];
                if (cT == aPieces[45] && cT == aPieces[46] && cT == aPieces[47] && cT == aPieces[48] && cT == aPieces[50] && cT == aPieces[51] && cT == aPieces[52] && cT == aPieces[53])
                {
                    cT = aPieces[4];
                    if (cT == aPieces[3] && cT == aPieces[5] && cT == aPieces[6] && cT == aPieces[7] && cT == aPieces[8])
                    {
                        cT = aPieces[13];
                        if (cT == aPieces[12] && cT == aPieces[14] && cT == aPieces[15] && cT == aPieces[16] && cT == aPieces[17])
                        {
                            cT = aPieces[22];
                            if (cT == aPieces[21] && cT == aPieces[23] && cT == aPieces[24] && cT == aPieces[25] && cT == aPieces[26])
                            {
                                cT = aPieces[31];
                                if (cT == aPieces[30] && cT == aPieces[32] && cT == aPieces[33] && cT == aPieces[34] && cT == aPieces[35])
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
                    await MakeTurnAsync("y");
                }

                // When two adjacent faces (front and right) have been solved, turn the cube
                if (aPieces[49] == aPieces[46] && aPieces[49] == aPieces[47] && aPieces[49] == aPieces[50])
                {
                    if (aPieces[4] == aPieces[5] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[8])
                    {
                        if (aPieces[13] == aPieces[12] && aPieces[13] == aPieces[15] && aPieces[13] == aPieces[16])
                        {
                            await MakeTurnAsync("y");
                        }
                    }
                }

                // Bring the corner piece at the top to the right position on the top layer
                if (aPieces[49] == aPieces[11] || aPieces[49] == aPieces[18] || aPieces[49] == aPieces[38])
                {
                    if (aPieces[4] == aPieces[11] || aPieces[4] == aPieces[18] || aPieces[4] == aPieces[38])
                    {
                        if (aPieces[13] == aPieces[11] || aPieces[13] == aPieces[18] || aPieces[13] == aPieces[38])
                        {
                            await MakeTurnAsync("U");
                        }
                    }
                }

                if (aPieces[49] == aPieces[20] || aPieces[49] == aPieces[27] || aPieces[49] == aPieces[36])
                {
                    if (aPieces[4] == aPieces[20] || aPieces[4] == aPieces[27] || aPieces[4] == aPieces[36])
                    {
                        if (aPieces[13] == aPieces[20] || aPieces[13] == aPieces[27] || aPieces[13] == aPieces[36])
                        {
                            await MakeTurnAsync("U2");
                        }
                    }
                }

                if (aPieces[49] == aPieces[0] || aPieces[49] == aPieces[29] || aPieces[49] == aPieces[42])
                {
                    if (aPieces[4] == aPieces[0] || aPieces[4] == aPieces[29] || aPieces[4] == aPieces[42])
                    {
                        if (aPieces[13] == aPieces[0] || aPieces[13] == aPieces[29] || aPieces[13] == aPieces[42])
                        {
                            await MakeTurnAsync("U'");
                        }
                    }
                }

                //--------------------------------------------------------------------------------------------------------------

                // https://kubuspuzzel.nl/3x3-kubus-oplossen/f2l/
                // Split
                // Situation 1
                if (aPieces[49] == aPieces[44] && aPieces[4] == aPieces[3] && aPieces[4] == aPieces[6] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[9] && aPieces[4] == aPieces[19])
                {
                    if (aPieces[13] == aPieces[2] && aPieces[13] == aPieces[14] && aPieces[13] == aPieces[16] && aPieces[13] == aPieces[17] && aPieces[13] == aPieces[37])
                    {
                        await MakeTurnAsync("U2 F' U' F");
                        continue;
                    }
                }

                // Situation 2
                if (aPieces[49] == aPieces[2] && aPieces[4] == aPieces[3] && aPieces[4] == aPieces[6] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[37] && aPieces[4] == aPieces[44])
                {
                    if (aPieces[13] == aPieces[9] && aPieces[13] == aPieces[14] && aPieces[13] == aPieces[16] && aPieces[13] == aPieces[17] && aPieces[13] == aPieces[19])
                    {
                        await MakeTurnAsync("U' R U R'");
                        continue;
                    }
                }

                // Situation 3.1
                if (aPieces[49] == aPieces[2] && aPieces[4] == aPieces[3] && aPieces[4] == aPieces[6] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[28] && aPieces[4] == aPieces[44])
                {
                    if (aPieces[13] == aPieces[9] && aPieces[13] == aPieces[14] && aPieces[13] == aPieces[16] && aPieces[13] == aPieces[17] && aPieces[13] == aPieces[39])
                    {
                        await MakeTurnAsync("F' U F");
                        continue;
                    }
                }

                // Situation 3.2
                if (aPieces[49] == aPieces[2] && aPieces[4] == aPieces[3] && aPieces[4] == aPieces[6] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[19] && aPieces[4] == aPieces[44])
                {
                    if (aPieces[13] == aPieces[9] && aPieces[13] == aPieces[14] && aPieces[13] == aPieces[16] && aPieces[13] == aPieces[17] && aPieces[13] == aPieces[37])
                    {
                        await MakeTurnAsync("U' R U");
                        continue;
                    }
                }

                //--------------------------------------------------------------------------------------------------------------

                // https://www.youtube.com/watch?v=Hx9ZbPdX8zM
                // Part 1. Converting cases. Convert any F2L case into one of five using a 3-step process
                if (aPieces[49] == aPieces[15] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[14] && aPieces[4] == aPieces[47])
                {
                    if (aPieces[13] == aPieces[8] && aPieces[13] == aPieces[16] && aPieces[13] == aPieces[21])
                    {
                        await MakeTurnAsync("R U R' y R U' R'");
                        continue;
                    }
                }

                if (aPieces[49] == aPieces[44] && aPieces[4] == aPieces[1] && aPieces[4] == aPieces[2] && aPieces[4] == aPieces[7])
                {
                    if (aPieces[31] == aPieces[9] && aPieces[31] == aPieces[34] && aPieces[31] == aPieces[43])
                    {
                        await MakeTurnAsync("R' U R");
                        continue;
                    }
                }

                if (aPieces[49] == aPieces[44] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[9] && aPieces[4] == aPieces[41])
                {
                    if (aPieces[13] == aPieces[2] && aPieces[13] == aPieces[10] && aPieces[13] == aPieces[16])
                    {
                        await MakeTurnAsync("U' R U' R'");
                        continue;
                    }
                }

                // Part 2. Five fundamental cases
                // Case 1. Matching
                if (aPieces[49] == aPieces[9] && aPieces[4] == aPieces[2] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[19])
                {
                    if (aPieces[13] == aPieces[16] && aPieces[13] == aPieces[37] && aPieces[13] == aPieces[44])
                    {
                        await MakeTurnAsync("R' U2 R y U' L' U L");
                        continue;
                    }
                }

                // Case 2. Non-matching
                if (aPieces[49] == aPieces[9] && aPieces[4] == aPieces[2] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[37])
                {
                    if (aPieces[13] == aPieces[16] && aPieces[13] == aPieces[19] && aPieces[13] == aPieces[44])
                    {
                        await MakeTurnAsync("R U R'");
                        continue;
                    }
                }

                // Case 3. White on top
                if (aPieces[49] == aPieces[44] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[9] && aPieces[4] == aPieces[19])
                {
                    if (aPieces[13] == aPieces[2] && aPieces[13] == aPieces[16] && aPieces[13] == aPieces[37])
                    {
                        await MakeTurnAsync("U2 F' U' R y U' L' U L");
                        continue;
                    }
                }

                // Case 4. Mirrored matching
                if (aPieces[49] == aPieces[2] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[39] && aPieces[4] == aPieces[44])
                {
                    if (aPieces[13] == aPieces[9] && aPieces[13] == aPieces[16] && aPieces[13] == aPieces[28])
                    {
                        await MakeTurnAsync("U' R U2 R' U2 R U' R'");
                        continue;
                    }
                }

                // Case 5. Mirrored non-matching
                if (aPieces[49] == aPieces[2] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[28] && aPieces[4] == aPieces[44])
                {
                    if (aPieces[13] == aPieces[9] && aPieces[13] == aPieces[16] && aPieces[13] == aPieces[39])
                    {
                        await MakeTurnAsync("y L' U' L");
                        continue;
                    }
                }

                // Part 3. Solving the last pair
                if (aPieces[49] == aPieces[9] && aPieces[4] == aPieces[2] && aPieces[4] == aPieces[3] && aPieces[4] == aPieces[6] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[43])
                {
                    if (aPieces[13] == aPieces[1] && aPieces[13] == aPieces[14] && aPieces[13] == aPieces[16] && aPieces[13] == aPieces[17] && aPieces[13] == aPieces[44])
                    {
                        await MakeTurnAsync("y' U R' U2 R y U' R U R'");
                        continue;
                    }
                }

                //--------------------------------------------------------------------------------------------------------------

                // https://solvethecube.com/algorithms

                // 1. Basic cases
                // 1.1   R U R'
                if (aPieces[49] == aPieces[9] && aPieces[4] == aPieces[2] && aPieces[4] == aPieces[37])
                {
                    if (aPieces[13] == aPieces[19] && aPieces[13] == aPieces[44])
                    {
                        await MakeTurnAsync("R U R'");
                        continue;
                    }
                }

                // 1.2   F' U' F
                if (aPieces[49] == aPieces[2] && aPieces[4] == aPieces[28] && aPieces[4] == aPieces[44])
                {
                    if (aPieces[13] == aPieces[9] && aPieces[13] == aPieces[39])
                    {
                        await MakeTurnAsync("F' U' F");
                        continue;
                    }
                }

                // 1.3   U R U' R'
                if (aPieces[49] == aPieces[2] && aPieces[4] == aPieces[41] && aPieces[4] == aPieces[44])
                {
                    if (aPieces[13] == aPieces[9] && aPieces[13] == aPieces[10])
                    {
                        await MakeTurnAsync("U R U' R'");
                        continue;
                    }
                }

                // 1.4   U' F' U F
                if (aPieces[49] == aPieces[9] && aPieces[4] == aPieces[1] && aPieces[4] == aPieces[2])
                {
                    if (aPieces[13] == aPieces[43] && aPieces[13] == aPieces[44])
                    {
                        await MakeTurnAsync("U' F' U F");
                        continue;
                    }
                }

                // 2. Corner and edge in top
                // 2.1   (U' R U') (R' U R) U R'
                if (aPieces[49] == aPieces[9] && aPieces[4] == aPieces[2] && aPieces[4] == aPieces[41])
                {
                    if (aPieces[13] == aPieces[10] && aPieces[13] == aPieces[44])
                    {
                        await MakeTurnAsync("U' R U' R' U R U R'");
                        continue;
                    }
                }

                // 2.2   (U F' U) (F U' F') U' F
                if (aPieces[49] == aPieces[2] && aPieces[4] == aPieces[1] && aPieces[4] == aPieces[44])
                {
                    if (aPieces[13] == aPieces[9] && aPieces[13] == aPieces[43])
                    {
                        await MakeTurnAsync("U F' U F U' F' U' F");
                        continue;
                    }
                }

                // 2.3   (U' R U) (R' U R) U R'
                if (aPieces[49] == aPieces[9] && aPieces[4] == aPieces[2] && aPieces[4] == aPieces[39])
                {
                    if (aPieces[13] == aPieces[28] && aPieces[13] == aPieces[44])
                    {
                        await MakeTurnAsync("U' R U R' U R U R'");
                        continue;
                    }
                }

                // 2.4   (U F' U') (F U' F') U' F
                if (aPieces[49] == aPieces[2] && aPieces[4] == aPieces[19] && aPieces[4] == aPieces[44])
                {
                    if (aPieces[13] == aPieces[9] && aPieces[13] == aPieces[37])
                    {
                        await MakeTurnAsync("U F' U' F U' F' U' F");
                        continue;
                    }
                }

                // 2.5   d (R' U2 R) d' (R U R')
                if (aPieces[49] == aPieces[9] && aPieces[4] == aPieces[2] && aPieces[4] == aPieces[43])
                {
                    if (aPieces[13] == aPieces[1] && aPieces[13] == aPieces[44])
                    {
                        await MakeTurnAsync("d R' U2 R d' R U R'");
                        continue;
                    }
                }

                // 2.6   U' (R U2 R') d (R' U' R)
                if (aPieces[49] == aPieces[2] && aPieces[4] == aPieces[10] && aPieces[4] == aPieces[44])
                {
                    if (aPieces[13] == aPieces[9] && aPieces[13] == aPieces[41])
                    {
                        await MakeTurnAsync("U' R U2 R' d R' U' R");
                        continue;
                    }
                }

                // 2.7   (R U' R' U) d (R' U' R)
                if (aPieces[49] == aPieces[9] && aPieces[4] == aPieces[2] && aPieces[4] == aPieces[10])
                {
                    if (aPieces[13] == aPieces[41] && aPieces[13] == aPieces[44])
                    {
                        await MakeTurnAsync("R U' R' U d R' U' R");
                        continue;
                    }
                }

                // 2.8   (F' U F U') d' (F U F')
                if (aPieces[49] == aPieces[2] && aPieces[4] == aPieces[43] && aPieces[4] == aPieces[44])
                {
                    if (aPieces[13] == aPieces[1] && aPieces[13] == aPieces[9])
                    {
                        await MakeTurnAsync("F' U F U' d' F U F'");
                        continue;
                    }
                }

                // 2.9   (U F' U2 F) (U F' U2 F)
                if (aPieces[49] == aPieces[9] && aPieces[4] == aPieces[2] && aPieces[4] == aPieces[19])
                {
                    if (aPieces[13] == aPieces[37] && aPieces[13] == aPieces[44])
                    {
                        await MakeTurnAsync("U F' U2 F U F' U2 F");
                        continue;
                    }
                }

                // 2.10   (U' R U2 R') (U' R U2 R')
                if (aPieces[49] == aPieces[2] && aPieces[4] == aPieces[39] && aPieces[4] == aPieces[44])
                {
                    if (aPieces[13] == aPieces[9] && aPieces[13] == aPieces[28])
                    {
                        await MakeTurnAsync("U' R U2 R' U' R U2 R'");
                        continue;
                    }
                }

                // 2.11   (U F' U' F) (U F' U2 F)
                if (aPieces[49] == aPieces[9] && aPieces[4] == aPieces[2] && aPieces[4] == aPieces[28])
                {
                    if (aPieces[13] == aPieces[39] && aPieces[13] == aPieces[44])
                    {
                        await MakeTurnAsync("U F' U' F U F' U2 F");
                        continue;
                    }
                }

                // 2.12   (U' R U R') (U' R U2 R')
                if (aPieces[49] == aPieces[2] && aPieces[4] == aPieces[37] && aPieces[4] == aPieces[44])
                {
                    if (aPieces[13] == aPieces[9] && aPieces[13] == aPieces[19])
                    {
                        await MakeTurnAsync("U' R U R' U' R U2 R'");
                        continue;
                    }
                }

                // 3. Corner pointing up, edge in top
                // 3.1   (R U2 R' U') (R U R')
                if (aPieces[49] == aPieces[44] && aPieces[4] == aPieces[9] && aPieces[4] == aPieces[41])
                {
                    if (aPieces[13] == aPieces[2] && aPieces[13] == aPieces[10])
                    {
                        await MakeTurnAsync("R U2 R' U' R U R'");
                        continue;
                    }
                }

                // 3.2   (F' U2 F U) (F' U' F)
                if (aPieces[49] == aPieces[44] && aPieces[4] == aPieces[1] && aPieces[4] == aPieces[9])
                {
                    if (aPieces[13] == aPieces[2] && aPieces[13] == aPieces[43])
                    {
                        await MakeTurnAsync("F' U2 F U F' U' F");
                        continue;
                    }
                }

                // 3.3   (U R U2 R') (U R U' R')
                if (aPieces[49] == aPieces[44] && aPieces[4] == aPieces[9] && aPieces[4] == aPieces[37])
                {
                    if (aPieces[13] == aPieces[2] && aPieces[13] == aPieces[19])
                    {
                        await MakeTurnAsync("U R U2 R' U R U' R'");
                        continue;
                    }
                }

                // 3.4   (U' F' U2 F) (U' F' U F)
                if (aPieces[49] == aPieces[44] && aPieces[4] == aPieces[9] && aPieces[4] == aPieces[28])
                {
                    if (aPieces[13] == aPieces[2] && aPieces[13] == aPieces[39])
                    {
                        await MakeTurnAsync("U' F' U2 F U' F' U F");
                        continue;
                    }
                }

                // 3.5   U2 (R U R' U) (R U' R')
                if (aPieces[49] == aPieces[44] && aPieces[4] == aPieces[9] && aPieces[4] == aPieces[39])
                {
                    if (aPieces[13] == aPieces[2] && aPieces[13] == aPieces[28])
                    {
                        await MakeTurnAsync("U2 R U R' U R U' R'");
                        continue;
                    }
                }

                // 3.6   U2 (F' U' F U') (F' U F)
                if (aPieces[49] == aPieces[44] && aPieces[4] == aPieces[9] && aPieces[4] == aPieces[19])
                {
                    if (aPieces[13] == aPieces[2] && aPieces[13] == aPieces[37])
                    {
                        await MakeTurnAsync("U2 F' U' F U' F' U F");
                        continue;
                    }
                }

                // 3.7   (R U R' U') U' (R U R' U') (R U R')
                if (aPieces[49] == aPieces[44] && aPieces[4] == aPieces[9] && aPieces[4] == aPieces[43])
                {
                    if (aPieces[13] == aPieces[1] && aPieces[13] == aPieces[2])
                    {
                        await MakeTurnAsync("R U R' U' U' R U R' U' R U R'");
                        continue;
                    }
                }

                // 3.8   y' (R' U' R U) U (R' U' R U) (R' U' R)
                if (aPieces[49] == aPieces[44] && aPieces[4] == aPieces[9] && aPieces[4] == aPieces[10])
                {
                    if (aPieces[13] == aPieces[2] && aPieces[13] == aPieces[41])
                    {
                        await MakeTurnAsync("y' R' U' R U U R' U' R U R' U' R");
                        continue;
                    }
                }

                // 4. Corner in top, edge in middle
                // 4.1   (U F' U F) (U F' U2 F)
                if (aPieces[49] == aPieces[9] && aPieces[4] == aPieces[2] && aPieces[4] == aPieces[5])
                {
                    if (aPieces[13] == aPieces[12] && aPieces[13] == aPieces[44])
                    {
                        await MakeTurnAsync("U F' U F U F' U2 F");
                        continue;
                    }
                }

                // 4.2   (U' R U' R') (U' R U2 R')
                if (aPieces[49] == aPieces[2] && aPieces[4] == aPieces[5] && aPieces[4] == aPieces[44])
                {
                    if (aPieces[13] == aPieces[9] && aPieces[13] == aPieces[12])
                    {
                        await MakeTurnAsync("U' R U' R' U' R U2 R'");
                        continue;
                    }
                }

                // 4.3   (U F' U' F) (d' F U F')
                if (aPieces[49] == aPieces[9] && aPieces[4] == aPieces[2] && aPieces[4] == aPieces[12])
                {
                    if (aPieces[13] == aPieces[5] && aPieces[13] == aPieces[44])
                    {
                        await MakeTurnAsync("U F' U' F d' F U F'");
                        continue;
                    }
                }

                // 4.4   (U' R U R') (d R' U' R)
                if (aPieces[49] == aPieces[2] && aPieces[4] == aPieces[12] && aPieces[4] == aPieces[44])
                {
                    if (aPieces[13] == aPieces[5] && aPieces[13] == aPieces[9])
                    {
                        await MakeTurnAsync("U' R U R' d R' U' R");
                        continue;
                    }
                }

                // 4.5   (R U' R') (d R' U R)
                if (aPieces[49] == aPieces[44] && aPieces[4] == aPieces[9] && aPieces[4] == aPieces[12])
                {
                    if (aPieces[13] == aPieces[2] && aPieces[13] == aPieces[5])
                    {
                        await MakeTurnAsync("R U' R' d R' U R");
                        continue;
                    }
                }

                // 4.6   (R U R' U') (R U R' U') (R U R')
                if (aPieces[49] == aPieces[44] && aPieces[4] == aPieces[5] && aPieces[4] == aPieces[9])
                {
                    if (aPieces[13] == aPieces[2] && aPieces[13] == aPieces[12])
                    {
                        await MakeTurnAsync("R U R' U' R U R' U' R U R'");
                        continue;
                    }
                }

                // 5. Corner in bottom, edge in top
                // 5.1   (U R U' R') (U' F' U F)
                if (aPieces[49] == aPieces[47] && aPieces[4] == aPieces[1] && aPieces[4] == aPieces[8])
                {
                    if (aPieces[13] == aPieces[15] && aPieces[13] == aPieces[43])
                    {
                        await MakeTurnAsync("U R U' R' U' F' U F");
                        continue;
                    }
                }

                // 5.2   (U' F' U F) (U R U' R')
                if (aPieces[49] == aPieces[47] && aPieces[4] == aPieces[8] && aPieces[4] == aPieces[41])
                {
                    if (aPieces[13] == aPieces[10] && aPieces[13] == aPieces[15])
                    {
                        await MakeTurnAsync("U' F' U F U R U' R'");
                        continue;
                    }
                }

                // 5.3   (F' U F) (U' F' U F)
                if (aPieces[49] == aPieces[15] && aPieces[4] == aPieces[1] && aPieces[4] == aPieces[47])
                {
                    if (aPieces[13] == aPieces[8] && aPieces[13] == aPieces[43])
                    {
                        await MakeTurnAsync("F' U F U' F' U F");
                        continue;
                    }
                }

                // 5.4   (R U' R') (U R U' R')
                if (aPieces[49] == aPieces[8] && aPieces[4] == aPieces[15] && aPieces[4] == aPieces[41])
                {
                    if (aPieces[13] == aPieces[10] && aPieces[13] == aPieces[47])
                    {
                        await MakeTurnAsync("R U' R' U R U' R'");
                        continue;
                    }
                }

                // 5.5   (R U R') (U' R U R')
                if (aPieces[49] == aPieces[15] && aPieces[4] == aPieces[41] && aPieces[4] == aPieces[47])
                {
                    if (aPieces[13] == aPieces[8] && aPieces[13] == aPieces[10])
                    {
                        await MakeTurnAsync("R U R' U' R U R'");
                        continue;
                    }
                }

                // 5.6   (F' U' F) (U F' U' F)
                if (aPieces[49] == aPieces[8] && aPieces[4] == aPieces[1] && aPieces[4] == aPieces[15])
                {
                    if (aPieces[13] == aPieces[43] && aPieces[13] == aPieces[47])
                    {
                        await MakeTurnAsync("F' U' F U F' U' F");
                        continue;
                    }
                }

                // 6. Corner in bottom, edge in middle
                // 6.1   (R U' R' U) R U2 R' (U R U' R')
                if (aPieces[49] == aPieces[15] && aPieces[4] == aPieces[5] && aPieces[4] == aPieces[47])
                {
                    if (aPieces[13] == aPieces[8] && aPieces[13] == aPieces[12])
                    {
                        await MakeTurnAsync("R U' R' U R U2 R' U R U' R'");
                        continue;
                    }
                }

                // 6.2   (R U' R' U') (R U R' U') (R U2 R')
                if (aPieces[49] == aPieces[8] && aPieces[4] == aPieces[5] && aPieces[4] == aPieces[15])
                {
                    if (aPieces[13] == aPieces[12] && aPieces[13] == aPieces[47])
                    {
                        await MakeTurnAsync("R U' R' U' R U R' U' R U2 R'");
                        continue;
                    }
                }

                // 6.3   (R U R' U') (R U' R') U d (R' U' R)
                if (aPieces[49] == aPieces[15] && aPieces[4] == aPieces[12] && aPieces[4] == aPieces[47])
                {
                    if (aPieces[13] == aPieces[5] && aPieces[13] == aPieces[8])
                    {
                        await MakeTurnAsync("R U R' U' R U' R' U d R' U' R");
                        continue;
                    }
                }

                // 6.4   (R U' R') d (R' U' R U') (R' U' R)
                if (aPieces[49] == aPieces[8] && aPieces[4] == aPieces[12] && aPieces[4] == aPieces[15])
                {
                    if (aPieces[13] == aPieces[5] && aPieces[13] == aPieces[47])
                    {
                        await MakeTurnAsync("R U' R' d R' U' R U' R' U' R");
                        continue;
                    }
                }

                // 6.5   (R U' R' d R' U2 R) (U R' U2 R)
                if (aPieces[49] == aPieces[47] && aPieces[4] == aPieces[8] && aPieces[4] == aPieces[12])
                {
                    if (aPieces[13] == aPieces[5] && aPieces[13] == aPieces[15])
                    {
                        await MakeTurnAsync("R U' R' d R' U2 R U R' U2 R");
                        continue;
                    }
                }

                //--------------------------------------------------------------------------------------------------------------

                // https://www.cubelelo.com/blogs/cubing/f2l-method-explained-solve-rubiks-cube-under-30-seconds
                // Case 1: Basic cases
                // 1 Case
                if (aPieces[49] == aPieces[9] && aPieces[4] == aPieces[2] && aPieces[4] == aPieces[37])
                {
                    if (aPieces[13] == aPieces[19] && aPieces[13] == aPieces[44])
                    {
                        await MakeTurnAsync("R U R'");
                        continue;
                    }
                }

                // 1 Case mirror
                if (aPieces[49] == aPieces[2] && aPieces[4] == aPieces[28] && aPieces[4] == aPieces[44])
                {
                    if (aPieces[13] == aPieces[9] && aPieces[13] == aPieces[39])
                    {
                        await MakeTurnAsync("F' U' F");
                        continue;
                    }
                }

                // 2 Case
                if (aPieces[49] == aPieces[2] && aPieces[4] == aPieces[41] && aPieces[4] == aPieces[44])
                {
                    if (aPieces[13] == aPieces[9] && aPieces[13] == aPieces[10])
                    {
                        await MakeTurnAsync("U R U' R'");
                        continue;
                    }
                }

                // 2 Case mirror
                if (aPieces[49] == aPieces[9] && aPieces[4] == aPieces[1] && aPieces[4] == aPieces[2])
                {
                    if (aPieces[13] == aPieces[43] && aPieces[13] == aPieces[44])
                    {
                        await MakeTurnAsync("U' F' U F");
                        continue;
                    }
                }

                // Case 2: Corner and the Edge in Top Layer
                // Type 1 Cases
                // 1 Case
                if (aPieces[49] == aPieces[9] && aPieces[4] == aPieces[2] && aPieces[4] == aPieces[41])
                {
                    if (aPieces[13] == aPieces[10] && aPieces[13] == aPieces[44])
                    {
                        await MakeTurnAsync("U' R U' R' U");
                        continue;
                    }
                }

                // 1 Case mirror
                if (aPieces[49] == aPieces[2] && aPieces[4] == aPieces[1] && aPieces[4] == aPieces[44])
                {
                    if (aPieces[13] == aPieces[9] && aPieces[13] == aPieces[43])
                    {
                        await MakeTurnAsync("U F' U F U'");
                        continue;
                    }
                }

                // 2 Case
                if (aPieces[49] == aPieces[9] && aPieces[4] == aPieces[2] && aPieces[4] == aPieces[39])
                {
                    if (aPieces[13] == aPieces[28] && aPieces[13] == aPieces[39])
                    {
                        await MakeTurnAsync("U' R U R' U");
                        continue;
                    }
                }

                // 2 Case mirror
                if (aPieces[49] == aPieces[2] && aPieces[4] == aPieces[19] && aPieces[4] == aPieces[44])
                {
                    if (aPieces[13] == aPieces[9] && aPieces[13] == aPieces[37])
                    {
                        await MakeTurnAsync("U F' U' F U'");
                        continue;
                    }
                }

                // 3 Case
                if (aPieces[49] == aPieces[9] && aPieces[4] == aPieces[2] && aPieces[4] == aPieces[43])
                {
                    if (aPieces[13] == aPieces[1] && aPieces[13] == aPieces[44])
                    {
                        await MakeTurnAsync("U F' U2 F U'");
                        continue;
                    }
                }

                // 3 Case mirror
                if (aPieces[49] == aPieces[2] && aPieces[4] == aPieces[10] && aPieces[4] == aPieces[44])
                {
                    if (aPieces[13] == aPieces[9] && aPieces[13] == aPieces[41])
                    {
                        await MakeTurnAsync("U' R U2 R' U");
                        continue;
                    }
                }

                // 4 Case
                if (aPieces[49] == aPieces[9] && aPieces[4] == aPieces[2] && aPieces[4] == aPieces[19])
                {
                    if (aPieces[13] == aPieces[37] && aPieces[13] == aPieces[44])
                    {
                        await MakeTurnAsync("U F' U2 F U'");
                        continue;
                    }
                }

                // 4 Case mirror
                if (aPieces[49] == aPieces[2] && aPieces[4] == aPieces[39] && aPieces[4] == aPieces[44])
                {
                    if (aPieces[13] == aPieces[9] && aPieces[13] == aPieces[28])
                    {
                        await MakeTurnAsync("U' R U2 R' U");
                        continue;
                    }
                }

                // 5 Case
                if (aPieces[49] == aPieces[9] && aPieces[4] == aPieces[2] && aPieces[4] == aPieces[28])
                {
                    if (aPieces[13] == aPieces[39] && aPieces[13] == aPieces[44])
                    {
                        await MakeTurnAsync("U F' U' F U'");
                        continue;
                    }
                }

                // 5 Case mirror (!!! no picture of cube !!!)
                if (aPieces[49] == aPieces[2] && aPieces[4] == aPieces[37] && aPieces[4] == aPieces[44])
                {
                    if (aPieces[13] == aPieces[9] && aPieces[13] == aPieces[19])
                    {
                        await MakeTurnAsync("U' R U R' U");
                        continue;
                    }
                }

                // Type 2 Cases
                // 1 Case
                if (aPieces[49] == aPieces[9] && aPieces[4] == aPieces[2] && aPieces[4] == aPieces[10])
                {
                    if (aPieces[13] == aPieces[41] && aPieces[13] == aPieces[44])
                    {
                        await MakeTurnAsync("R U' R'");
                        continue;
                    }
                }

                // 1 Case mirror
                if (aPieces[49] == aPieces[2] && aPieces[4] == aPieces[43] && aPieces[4] == aPieces[44])
                {
                    if (aPieces[13] == aPieces[1] && aPieces[13] == aPieces[9])
                    {
                        await MakeTurnAsync("F' U F");
                        continue;
                    }
                }

                // 2 Case (!!! no picture of cube !!!)
                if (aPieces[49] == aPieces[44] && aPieces[4] == aPieces[9] && aPieces[4] == aPieces[41])
                {
                    if (aPieces[13] == aPieces[2] && aPieces[13] == aPieces[10])
                    {
                        await MakeTurnAsync("R U2 R'");
                        continue;
                    }
                }

                // 2 Case mirror
                if (aPieces[49] == aPieces[44] && aPieces[4] == aPieces[1] && aPieces[4] == aPieces[9])
                {
                    if (aPieces[13] == aPieces[2] && aPieces[13] == aPieces[43])
                    {
                        await MakeTurnAsync("F' U2 F");
                        continue;
                    }
                }

                // 3 Case
                if (aPieces[49] == aPieces[44] && aPieces[4] == aPieces[9] && aPieces[4] == aPieces[37])
                {
                    if (aPieces[13] == aPieces[2] && aPieces[13] == aPieces[19])
                    {
                        await MakeTurnAsync("U R U2 R'");
                        continue;
                    }
                }

                // 3 Case mirror
                if (aPieces[49] == aPieces[44] && aPieces[4] == aPieces[9] && aPieces[4] == aPieces[28])
                {
                    if (aPieces[13] == aPieces[2] && aPieces[13] == aPieces[39])
                    {
                        await MakeTurnAsync("U' F' U2 F");
                        continue;
                    }
                }

                // 4 Case
                if (aPieces[49] == aPieces[44] && aPieces[4] == aPieces[9] && aPieces[4] == aPieces[39])
                {
                    if (aPieces[13] == aPieces[2] && aPieces[13] == aPieces[28])
                    {
                        await MakeTurnAsync("U2 R U R'");
                        continue;
                    }
                }

                // 4 Case mirror
                if (aPieces[49] == aPieces[44] && aPieces[4] == aPieces[9] && aPieces[4] == aPieces[19])
                {
                    if (aPieces[13] == aPieces[2] && aPieces[13] == aPieces[37])
                    {
                        await MakeTurnAsync("U2 F' U' F");
                        continue;
                    }
                }

                // Case 3: Corner on Bottom, Edge on Top
                // 1 Case
                if (aPieces[49] == aPieces[47] && aPieces[4] == aPieces[1] && aPieces[4] == aPieces[8])
                {
                    if (aPieces[13] == aPieces[15] && aPieces[13] == aPieces[43])
                    {
                        await MakeTurnAsync("U R U' R'");
                        continue;
                    }
                }

                // 1 Case mirror
                if (aPieces[49] == aPieces[47] && aPieces[4] == aPieces[8] && aPieces[4] == aPieces[41])
                {
                    if (aPieces[13] == aPieces[10] && aPieces[13] == aPieces[15])
                    {
                        await MakeTurnAsync("U' F' U F");
                        continue;
                    }
                }

                // 2 Case
                if (aPieces[49] == aPieces[47] && aPieces[4] == aPieces[8] && aPieces[4] == aPieces[41])
                {
                    if (aPieces[13] == aPieces[10] && aPieces[13] == aPieces[15])
                    {
                        await MakeTurnAsync("F' U F");
                        continue;
                    }
                }

                // 2 Case mirror
                if (aPieces[49] == aPieces[8] && aPieces[4] == aPieces[15] && aPieces[4] == aPieces[41])
                {
                    if (aPieces[13] == aPieces[10] && aPieces[13] == aPieces[47])
                    {
                        await MakeTurnAsync("R U' R'");
                        continue;
                    }
                }

                // 3 Case
                if (aPieces[49] == aPieces[15] && aPieces[4] == aPieces[41] && aPieces[4] == aPieces[47])
                {
                    if (aPieces[13] == aPieces[8] && aPieces[13] == aPieces[10])
                    {
                        await MakeTurnAsync("R U R'");
                        continue;
                    }
                }

                // 3 Case mirror
                if (aPieces[49] == aPieces[8] && aPieces[4] == aPieces[1] && aPieces[4] == aPieces[15])
                {
                    if (aPieces[13] == aPieces[43] && aPieces[13] == aPieces[47])
                    {
                        await MakeTurnAsync("F' U' F");
                        continue;
                    }
                }

                // Case 4: Corner on Top, Edge in Middle
                // 1 Case
                if (aPieces[49] == aPieces[9] && aPieces[4] == aPieces[2] && aPieces[4] == aPieces[5])
                {
                    if (aPieces[13] == aPieces[12] && aPieces[13] == aPieces[44])
                    {
                        await MakeTurnAsync("U F' U F");
                        continue;
                    }
                }

                // 1 Case mirror
                if (aPieces[49] == aPieces[2] && aPieces[4] == aPieces[5] && aPieces[4] == aPieces[44])
                {
                    if (aPieces[13] == aPieces[9] && aPieces[13] == aPieces[12])
                    {
                        await MakeTurnAsync("U' R U' R'");
                        continue;
                    }
                }

                // 2 Case
                if (aPieces[49] == aPieces[2] && aPieces[4] == aPieces[5] && aPieces[4] == aPieces[44])
                {
                    if (aPieces[13] == aPieces[9] && aPieces[13] == aPieces[12])
                    {
                        await MakeTurnAsync("U F' U' F");
                        continue;
                    }
                }

                // 2 Case mirror
                if (aPieces[49] == aPieces[2] && aPieces[4] == aPieces[12] && aPieces[4] == aPieces[44])
                {
                    if (aPieces[13] == aPieces[5] && aPieces[13] == aPieces[9])
                    {
                        await MakeTurnAsync("U' R U R'");
                        continue;
                    }
                }

                // 3 Case
                if (aPieces[49] == aPieces[44] && aPieces[4] == aPieces[9] && aPieces[4] == aPieces[12])
                {
                    if (aPieces[13] == aPieces[2] && aPieces[13] == aPieces[5])
                    {
                        await MakeTurnAsync("R U2 R'");
                        continue;
                    }
                }

                // 4 Case
                if (aPieces[49] == aPieces[44] && aPieces[4] == aPieces[5] && aPieces[4] == aPieces[9])
                {
                    if (aPieces[13] == aPieces[2] && aPieces[13] == aPieces[12])
                    {
                        await MakeTurnAsync("R U R' U' R U R'");
                        continue;
                    }
                }

                // Case 5: Corner on Bottom, Edge in Middle
                // 1 Special Case
                if (aPieces[49] == aPieces[15] && aPieces[4] == aPieces[5] && aPieces[4] == aPieces[47])
                {
                    if (aPieces[13] == aPieces[8] && aPieces[13] == aPieces[12])
                    {
                        await MakeTurnAsync("R U2 R U R' U R U2 R2");
                        continue;
                    }
                }

                // 1 Special Case mirror
                if (aPieces[49] == aPieces[8] && aPieces[4] == aPieces[5] && aPieces[4] == aPieces[15])
                {
                    if (aPieces[13] == aPieces[12] && aPieces[13] == aPieces[47])
                    {
                        await MakeTurnAsync("R2 U2 R' U' R U' R' U2 R'");
                        continue;
                    }
                }

                // 2 Special Case
                if (aPieces[49] == aPieces[15] && aPieces[4] == aPieces[12] && aPieces[4] == aPieces[47])
                {
                    if (aPieces[13] == aPieces[5] && aPieces[13] == aPieces[8])
                    {
                        await MakeTurnAsync("R U' R' F' L' U2 L F");
                        continue;
                    }
                }

                // 2 Special Case mirror
                if (aPieces[49] == aPieces[8] && aPieces[4] == aPieces[12] && aPieces[4] == aPieces[15])
                {
                    if (aPieces[13] == aPieces[5] && aPieces[13] == aPieces[47])
                    {
                        await MakeTurnAsync("R U' R U y' L U' L' B2");
                        continue;
                    }
                }

                // Advanced F2L Techniques
                // 1. Stuck in the wrong slots
                // Edge in the wrong slot
                // Case: The edge is in the wrong slot, but the corner is still in the top
                // 1
                //if (aPieces[49] == aPieces[9] && aPieces[4] == aPieces[12] && aPieces[4] == aPieces[44])
                //{
                //    if (aPieces[31] == aPieces[2] && aPieces[31] == aPieces[5])
                //    {
                //        await MakeTurnAsync("U F' U F U'");
                //        continue;
                //    }
                //}

                // 2 (!!! same picture as the previous one !!!)
                //if (aPieces[49] == aPieces[9] && aPieces[4] == aPieces[12] && aPieces[4] == aPieces[44])
                //{
                //    if (aPieces[31] == aPieces[2] && aPieces[31] == aPieces[5])
                //    {
                //        await MakeTurnAsync("U' R U R' U");
                //        continue;
                //    }
                //}

                // Corner in the wrong slot.
                // Case: The corner is in the wrong slot, but the edge is still in the top.
                // 1 (!!! same picture as the previous two !!!)
                //if (aPieces[49] == aPieces[9] && aPieces[4] == aPieces[12] && aPieces[4] == aPieces[44])
                //{
                //    if (aPieces[31] == aPieces[2] && aPieces[31] == aPieces[5])
                //    {
                //        await MakeTurnAsync("R U' R'");
                //        continue;
                //    }
                //}

                // 2
                if (aPieces[49] == aPieces[15] && aPieces[13] == aPieces[41] && aPieces[13] == aPieces[47])
                {
                    if (aPieces[22] == aPieces[8] && aPieces[22] == aPieces[10])
                    {
                        await MakeTurnAsync("R U R' U'");
                        continue;
                    }
                }

                // 2. Stuck pieces
                // At times you would find that the corner and the edge pieces are not on top, but instead are stuck in random incorrect slots.
                // To set it up, use the algorithm stated below.
                if (aPieces[49] == aPieces[2] && aPieces[4] == aPieces[32] && aPieces[4] == aPieces[44])
                {
                    if (aPieces[13] == aPieces[3] && aPieces[13] == aPieces[9])
                    {
                        await MakeTurnAsync("U R' D' F D R");
                        // Now, in the next step, use the algorithm.
                        await MakeTurnAsync("R' U' R' U R");
                        continue;
                    }
                }

                // 3. Special shortcuts
                // At times when the F2L pieces are stuck in other slots, there are a few shortcuts that you can use.
                // We have mentioned a few below. However, you could find yourself in many other situations.
                // 1 Case
                if (aPieces[49] == aPieces[8] && aPieces[13] == aPieces[15] && aPieces[13] == aPieces[41])
                {
                    if (aPieces[22] == aPieces[10] && aPieces[22] == aPieces[47])
                    {
                        await MakeTurnAsync("y L' R U' L R'");
                        continue;
                    }
                }

                // 1 Case mirror (!!! same picture as the previous one !!!)
                //if (aPieces[49] == aPieces[8] && aPieces[13] == aPieces[15] && aPieces[13] == aPieces[41])
                //{
                //    if (aPieces[22] == aPieces[10] && aPieces[22] == aPieces[47])
                //    {
                //        await MakeTurnAsync("L' R U L R'");
                //        continue;
                //    }
                //}

                // 2 Case
                if (aPieces[49] == aPieces[15] && aPieces[13] == aPieces[1] && aPieces[13] == aPieces[47])
                {
                    if (aPieces[22] == aPieces[8] && aPieces[22] == aPieces[43])
                    {
                        await MakeTurnAsync("R U2 R2 U R");
                        continue;
                    }
                }

                // 2 Case mirror
                if (aPieces[49] == aPieces[8] && aPieces[4] == aPieces[10] && aPieces[4] == aPieces[47])
                {
                    if (aPieces[31] == aPieces[15] && aPieces[31] == aPieces[41])
                    {
                        await MakeTurnAsync("y' R' U2 R2 U' R'");
                        continue;
                    }
                }

                // 3 Case
                if (aPieces[49] == aPieces[9] && aPieces[4] == aPieces[2] && aPieces[4] == aPieces[21])
                {
                    if (aPieces[13] == aPieces[14] && aPieces[13] == aPieces[44])
                    {
                        await MakeTurnAsync("R' U' R2 U R'");
                        continue;
                    }
                }

                // 3 Case mirror
                if (aPieces[49] == aPieces[2] && aPieces[4] == aPieces[3] && aPieces[4] == aPieces[44])
                {
                    if (aPieces[13] == aPieces[9] && aPieces[13] == aPieces[32])
                    {
                        await MakeTurnAsync("y' R U R2 U' R");
                        continue;
                    }
                }

                // 4 Case
                if (aPieces[49] == aPieces[47] && aPieces[4] == aPieces[8] && aPieces[4] == aPieces[21])
                {
                    if (aPieces[13] == aPieces[14] && aPieces[13] == aPieces[15])
                    {
                        await MakeTurnAsync("R2 U' R2 U R2");
                        continue;
                    }
                }

                // 4 Case mirror
                if (aPieces[49] == aPieces[47] && aPieces[4] == aPieces[3] && aPieces[4] == aPieces[8])
                {
                    if (aPieces[13] == aPieces[15] && aPieces[13] == aPieces[32])
                    {
                        await MakeTurnAsync("y' R2 U R2 U' R2");
                        continue;
                    }
                }

                //continue;

                //--------------------------------------------------------------------------------------------------------------

                // https://ruwix.com/the-rubiks-cube/advanced-cfop-fridrich/first-two-layers-f2l/
                // Step 2: First two layers - F2L
                // 1. Easy cases.  These are the lucky cases which can be solved in 3-4 moves.
                // 1.1   R U R'
                if (aPieces[49] == aPieces[9] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[2] && aPieces[4] == aPieces[37])
                {
                    if (aPieces[13] == aPieces[16] && aPieces[13] == aPieces[44])
                    {
                        await MakeTurnAsync("R U R'");
                        continue;
                    }
                }

                // 1.2   F' U' F
                if (aPieces[49] == aPieces[2] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[44])
                {
                    if (aPieces[13] == aPieces[16] && aPieces[13] == aPieces[9] && aPieces[13] == aPieces[39])
                    {
                        await MakeTurnAsync("F' U' F");
                        continue;
                    }
                }

                // 1.3   U' F' U F
                if (aPieces[49] == aPieces[9] && aPieces[4] == aPieces[1] && aPieces[4] == aPieces[2] && aPieces[4] == aPieces[7])
                {
                    if (aPieces[13] == aPieces[16] && aPieces[13] == aPieces[43] && aPieces[13] == aPieces[44])
                    {
                        await MakeTurnAsync("U' F' U F");
                        continue;
                    }
                }

                // 1.4    U R U' R'
                if (aPieces[49] == aPieces[2] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[41] && aPieces[4] == aPieces[44])
                {
                    if (aPieces[13] == aPieces[9] && aPieces[13] == aPieces[10] && aPieces[13] == aPieces[16])
                    {
                        await MakeTurnAsync("U R U' R'");
                        continue;
                    }
                }

                // 2. Case: Corner in bottom, edge in top layer
                // 2.1   (U R U' R') (U' F' U F)
                if (aPieces[4] == aPieces[1] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[8])
                {
                    if (aPieces[13] == aPieces[15] && aPieces[13] == aPieces[16] && aPieces[13] == aPieces[43])
                    {
                        await MakeTurnAsync("U R U' R' U' F' U F");
                        continue;
                    }
                }

                // 2.2   (U' F' U F) (U R U' R')
                if (aPieces[4] == aPieces[7] && aPieces[4] == aPieces[8] && aPieces[4] == aPieces[41])
                {
                    if (aPieces[13] == aPieces[10] && aPieces[13] == aPieces[15] && aPieces[13] == aPieces[16])
                    {
                        await MakeTurnAsync("U' F' U F U R U' R'");
                        continue;
                    }
                }

                // 2.3   (F' U F) (U' F' U F)
                if (aPieces[49] == aPieces[15] && aPieces[4] == aPieces[1] && aPieces[4] == aPieces[7])
                {
                    if (aPieces[13] == aPieces[8] && aPieces[13] == aPieces[16] && aPieces[13] == aPieces[43])
                    {
                        await MakeTurnAsync("F' U F U' F' U F");
                        continue;
                    }
                }

                // 2.4   (R U R') (U' R U R')
                if (aPieces[49] == aPieces[15] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[41])
                {
                    if (aPieces[13] == aPieces[8] && aPieces[13] == aPieces[10] && aPieces[13] == aPieces[16])
                    {
                        await MakeTurnAsync("R U R' U' R U R'");
                        continue;
                    }
                }

                // 2.5   (R U' R') (U R U' R')
                if (aPieces[49] == aPieces[8] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[15] && aPieces[4] == aPieces[41])
                {
                    if (aPieces[13] == aPieces[10] && aPieces[13] == aPieces[16])
                    {
                        await MakeTurnAsync("R U' R' U R U' R'");
                        continue;
                    }
                }

                // 2.6   (F' U' F) (U F' U' F)
                if (aPieces[49] == aPieces[8] && aPieces[4] == aPieces[1] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[15])
                {
                    if (aPieces[13] == aPieces[16] && aPieces[13] == aPieces[43])
                    {
                        await MakeTurnAsync("F' U' F U F' U' F");
                        continue;
                    }
                }

                // 3. case: Corner in top, edge in middle
                // 3.1   (R U R' U') (R U R' U') (R U R')
                if (aPieces[49] == aPieces[44] && aPieces[4] == aPieces[5] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[9])
                {
                    if (aPieces[13] == aPieces[2] && aPieces[13] == aPieces[12] && aPieces[13] == aPieces[16])
                    {
                        await MakeTurnAsync("R U R' U' R U R' U' R U R'");
                        continue;
                    }
                }

                // 3.2   (R U' R') (d R' U R)
                if (aPieces[49] == aPieces[44] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[9] && aPieces[4] == aPieces[12])
                {
                    if (aPieces[13] == aPieces[2] && aPieces[13] == aPieces[5] && aPieces[13] == aPieces[16])
                    {
                        await MakeTurnAsync("R U' R' d R' U R");
                        continue;
                    }
                }

                // 3.3   (U F' U F) (U F' U2 F)
                if (aPieces[49] == aPieces[9] && aPieces[4] == aPieces[2] && aPieces[4] == aPieces[5] && aPieces[4] == aPieces[7])
                {
                    if (aPieces[13] == aPieces[12] && aPieces[13] == aPieces[16] && aPieces[13] == aPieces[44])
                    {
                        await MakeTurnAsync("U F' U F U F' U2 F");
                        continue;
                    }
                }

                // 3.4   (U F' U' F) (d' F U F')
                if (aPieces[49] == aPieces[9] && aPieces[4] == aPieces[2] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[12])
                {
                    if (aPieces[13] == aPieces[5] && aPieces[13] == aPieces[16] && aPieces[13] == aPieces[44])
                    {
                        await MakeTurnAsync("U F' U' F d' F U F'");
                        continue;
                    }
                }

                // 3.5   (U' R U' R') (U' R U2 R')
                if (aPieces[49] == aPieces[2] && aPieces[4] == aPieces[5] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[44])
                {
                    if (aPieces[13] == aPieces[9] && aPieces[13] == aPieces[12] && aPieces[13] == aPieces[16])
                    {
                        await MakeTurnAsync("U' R U' R' U' R U2 R'");
                        continue;
                    }
                }

                // 3.6   (U' R U R') (d R' U' R)
                if (aPieces[49] == aPieces[2] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[12] && aPieces[4] == aPieces[44])
                {
                    if (aPieces[13] == aPieces[5] && aPieces[13] == aPieces[9] && aPieces[13] == aPieces[16])
                    {
                        await MakeTurnAsync("U' R U R' d R' U' R");
                        continue;
                    }
                }

                // 4. case: Corner pointing outwards, edge in top layer.  In this case we usually bring the cube to a basic case, reorienting the white corner in the first stage.
                // 4.1   (R U' R' U) (d R' U' R)
                if (aPieces[49] == aPieces[9] && aPieces[4] == aPieces[2] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[10])
                {
                    if (aPieces[13] == aPieces[16] && aPieces[13] == aPieces[41] && aPieces[13] == aPieces[44])
                    {
                        await MakeTurnAsync("R U' R' U d R' U' R");
                        continue;
                    }
                }

                // 4.2   (F' U F U') (d' F U F')
                if (aPieces[49] == aPieces[2] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[43] && aPieces[4] == aPieces[44])
                {
                    if (aPieces[13] == aPieces[1] && aPieces[13] == aPieces[9] && aPieces[13] == aPieces[16])
                    {
                        await MakeTurnAsync("F' U F U' d' F U F'");
                        continue;
                    }
                }

                // 4.3   (U F' U2 F) (U F' U2 F)
                if (aPieces[49] == aPieces[9] && aPieces[4] == aPieces[2] && aPieces[4] == aPieces[7])
                {
                    if (aPieces[13] == aPieces[16] && aPieces[13] == aPieces[37] && aPieces[13] == aPieces[44])
                    {
                        await MakeTurnAsync("U F' U2 F U F' U2 F");
                        continue;
                    }
                }

                // 4.4   (U' R U2 R') (U' R U2 R')
                if (aPieces[49] == aPieces[2] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[39] && aPieces[4] == aPieces[44])
                {
                    if (aPieces[13] == aPieces[9] && aPieces[13] == aPieces[16])
                    {
                        await MakeTurnAsync("U' R U2 R' U' R U2 R'");
                        continue;
                    }
                }

                // 4.5   (U F' U' F) (U F' U2 F)
                if (aPieces[49] == aPieces[9] && aPieces[4] == aPieces[2] && aPieces[4] == aPieces[7])
                {
                    if (aPieces[13] == aPieces[16] && aPieces[13] == aPieces[39] && aPieces[13] == aPieces[44])
                    {
                        await MakeTurnAsync("U F' U' F U F' U2 F");
                        continue;
                    }
                }

                // 4.6   (U' R U R') (U' R U2 R')
                if (aPieces[49] == aPieces[2] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[37] && aPieces[4] == aPieces[44])
                {
                    if (aPieces[13] == aPieces[9] && aPieces[13] == aPieces[16])
                    {
                        await MakeTurnAsync("U' R U R' U' R U2 R'");
                        continue;
                    }
                }

                // 4.7   (U' R U' R' U) (R U R')
                if (aPieces[49] == aPieces[9] && aPieces[4] == aPieces[2] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[41])
                {
                    if (aPieces[13] == aPieces[10] && aPieces[13] == aPieces[16] && aPieces[13] == aPieces[44])
                    {
                        await MakeTurnAsync("U' R U' R' U R U R'");
                        continue;
                    }
                }

                // 4.8   (U F' U F U') (F' U' F)
                if (aPieces[49] == aPieces[2] && aPieces[4] == aPieces[1] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[44])
                {
                    if (aPieces[13] == aPieces[9] && aPieces[13] == aPieces[16] && aPieces[13] == aPieces[43])
                    {
                        await MakeTurnAsync("U F' U F U' F' U' F");
                        continue;
                    }
                }

                // 4.9   (U' R U R' U) (R U R')
                if (aPieces[49] == aPieces[9] && aPieces[4] == aPieces[2] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[39])
                {
                    if (aPieces[13] == aPieces[16] && aPieces[13] == aPieces[44])
                    {
                        await MakeTurnAsync("U' R U R' U R U R'");
                        continue;
                    }
                }

                // 4.10   (U F' U' F U') (F' U' F)
                if (aPieces[49] == aPieces[2] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[44])
                {
                    if (aPieces[13] == aPieces[9] && aPieces[13] == aPieces[16] && aPieces[13] == aPieces[37])
                    {
                        await MakeTurnAsync("U F' U' F U' F' U' F");
                        continue;
                    }
                }

                // 4.11   (U F' U2 F U') (R U R')
                if (aPieces[49] == aPieces[9] && aPieces[4] == aPieces[2] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[43])
                {
                    if (aPieces[13] == aPieces[1] && aPieces[13] == aPieces[16] && aPieces[13] == aPieces[44])
                    {
                        await MakeTurnAsync("U F' U2 F U' R U R'");
                        continue;
                    }
                }

                // 4.12   (U' R U2 R' U) (F' U' F)
                if (aPieces[49] == aPieces[2] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[10] && aPieces[4] == aPieces[44])
                {
                    if (aPieces[13] == aPieces[9] && aPieces[13] == aPieces[16] && aPieces[13] == aPieces[41])
                    {
                        await MakeTurnAsync("U' R U2 R' U F' U' F");
                        continue;
                    }
                }

                // 5. case: Corner pointing upwards, edge in top layer
                // 5.1   (R U R' U') U' (R U R' U') (R U R')
                if (aPieces[49] == aPieces[44] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[9] && aPieces[4] == aPieces[43])
                {
                    if (aPieces[13] == aPieces[1] && aPieces[13] == aPieces[2] && aPieces[13] == aPieces[16])
                    {
                        await MakeTurnAsync("R U R' U' U' R U R' U' R U R'");
                        continue;
                    }
                }

                // 5.2   y' (R' U' R U) U (R' U' R U) (R' U' R)
                if (aPieces[49] == aPieces[44] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[9] && aPieces[4] == aPieces[10])
                {
                    if (aPieces[13] == aPieces[2] && aPieces[13] == aPieces[16] && aPieces[13] == aPieces[41])
                    {
                        await MakeTurnAsync("y' R' U' R U U R' U' R U R' U' R");
                        continue;
                    }
                }

                // 5.3   (U2 R U R') (U R U' R')
                if (aPieces[49] == aPieces[44] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[9] && aPieces[4] == aPieces[39])
                {
                    if (aPieces[13] == aPieces[2] && aPieces[13] == aPieces[16])
                    {
                        await MakeTurnAsync("U2 R U R' U R U' R'");
                        continue;
                    }
                }

                // 5.4   (U2 F' U' F) (U' F' U F)
                if (aPieces[49] == aPieces[44] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[9])
                {
                    if (aPieces[13] == aPieces[2] && aPieces[13] == aPieces[16] && aPieces[13] == aPieces[37])
                    {
                        await MakeTurnAsync("U2 F' U' F U' F' U F");
                        continue;
                    }
                }

                // 5.5   (U R U2 R') (U R U' R')
                if (aPieces[49] == aPieces[44] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[9] && aPieces[4] == aPieces[37])
                {
                    if (aPieces[13] == aPieces[2] && aPieces[13] == aPieces[16])
                    {
                        await MakeTurnAsync("U R U2 R' U R U' R'");
                        continue;
                    }
                }

                // 5.6   (U' F' U2 F) (U' F' U F)
                if (aPieces[49] == aPieces[44] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[9])
                {
                    if (aPieces[13] == aPieces[2] && aPieces[13] == aPieces[16] && aPieces[13] == aPieces[39])
                    {
                        await MakeTurnAsync("U' F' U2 F U' F' U F");
                        continue;
                    }
                }

                // 5.7   (R U2 R') (U' R U R')
                if (aPieces[49] == aPieces[44] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[9] && aPieces[4] == aPieces[41])
                {
                    if (aPieces[13] == aPieces[2] && aPieces[13] == aPieces[10] && aPieces[13] == aPieces[16])
                    {
                        await MakeTurnAsync("R U2 R' U' R U R'");
                        continue;
                    }
                }

                // 5.8   (F' U2 F) (U F' U' F)
                if (aPieces[49] == aPieces[44] && aPieces[4] == aPieces[1] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[9])
                {
                    if (aPieces[13] == aPieces[2] && aPieces[13] == aPieces[16] && aPieces[13] == aPieces[43])
                    {
                        await MakeTurnAsync("F' U2 F U F' U' F");
                        continue;
                    }
                }

                // 6. case: Corner in bottom, edge in middle
                // 6.1   (R U' R' d R' U2 R) (U R' U2 R)
                if (aPieces[4] == aPieces[7] && aPieces[4] == aPieces[8] && aPieces[4] == aPieces[12])
                {
                    if (aPieces[13] == aPieces[5] && aPieces[13] == aPieces[15] && aPieces[13] == aPieces[16])
                    {
                        await MakeTurnAsync("R U' R' d R' U2 R U R' U2 R");
                        continue;
                    }
                }

                // 6.2   Does not exist

                // 6.3   (R U' R' U R U2 R') (U R U' R')
                if (aPieces[49] == aPieces[15] && aPieces[4] == aPieces[5] && aPieces[4] == aPieces[7])
                {
                    if (aPieces[13] == aPieces[8] && aPieces[13] == aPieces[12] && aPieces[13] == aPieces[16])
                    {
                        await MakeTurnAsync("R U' R' U R U2 R' U R U' R'");
                        continue;
                    }
                }

                //continue;

                //--------------------------------------------------------------------------------------------------------------

                // https://www.rubiksplace.com/speedcubing/F2L-algorithms/
                // Corner on top, FL color facing side, edge colors match
                // 1   U (R U' R') or  R' F R F'
                if (aPieces[49] == aPieces[2] && aPieces[4] == aPieces[3] && aPieces[4] == aPieces[6] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[41] && aPieces[4] == aPieces[44])
                {
                    if (aPieces[13] == aPieces[9] && aPieces[13] == aPieces[10] && aPieces[13] == aPieces[14] && aPieces[13] == aPieces[16] && aPieces[13] == aPieces[17])
                    {
                        await MakeTurnAsync("U R U' R'");
                        continue;
                    }
                }

                // 2   y' U' (R' U R) or F R' F' R
                if (aPieces[49] == aPieces[9] && aPieces[4] == aPieces[1] && aPieces[4] == aPieces[2] && aPieces[4] == aPieces[3] && aPieces[4] == aPieces[6] && aPieces[4] == aPieces[7])
                {
                    if (aPieces[13] == aPieces[14] && aPieces[13] == aPieces[16] && aPieces[13] == aPieces[17] && aPieces[13] == aPieces[43] && aPieces[13] == aPieces[44])
                    {
                        await MakeTurnAsync("F R' F' R");
                        continue;
                    }
                }

                // 3   U' R U R' U2 (R U' R')
                if (aPieces[49] == aPieces[2] && aPieces[4] == aPieces[3] && aPieces[4] == aPieces[6] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[37] && aPieces[4] == aPieces[44])
                {
                    if (aPieces[13] == aPieces[9] && aPieces[13] == aPieces[14] && aPieces[13] == aPieces[16] && aPieces[13] == aPieces[17])
                    {
                        await MakeTurnAsync("U' R U R' U2 R U' R'");
                        continue;
                    }
                }

                // 4   d R' U' R U2' (R' U R) or y' (U R' U' R) U2 (R' U R)
                if (aPieces[49] == aPieces[9] && aPieces[4] == aPieces[2] && aPieces[4] == aPieces[3] && aPieces[4] == aPieces[6] && aPieces[4] == aPieces[7])
                {
                    if (aPieces[13] == aPieces[14] && aPieces[13] == aPieces[16] && aPieces[13] == aPieces[17] && aPieces[13] == aPieces[39] && aPieces[13] == aPieces[44])
                    {
                        await MakeTurnAsync("d R' U' R U2 R' U R");
                        continue;
                    }
                }

                // 5   U' R U2' R' U2 (R U' R')
                if (aPieces[49] == aPieces[2] && aPieces[4] == aPieces[3] && aPieces[4] == aPieces[6] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[39] && aPieces[4] == aPieces[44])
                {
                    if (aPieces[13] == aPieces[9] && aPieces[13] == aPieces[14] && aPieces[13] == aPieces[16] && aPieces[13] == aPieces[17])
                    {
                        await MakeTurnAsync("U' R U2 R' U2 R U' R'");
                        continue;
                    }
                }

                // 6   d R' U2 R U2' (R' U R) or R' F R F'
                if (aPieces[49] == aPieces[9] && aPieces[4] == aPieces[2] && aPieces[4] == aPieces[3] && aPieces[4] == aPieces[6] && aPieces[4] == aPieces[7])
                {
                    if (aPieces[13] == aPieces[14] && aPieces[13] == aPieces[16] && aPieces[13] == aPieces[17] && aPieces[13] == aPieces[37] && aPieces[13] == aPieces[44])
                    {
                        await MakeTurnAsync("R' F R F'");
                        continue;
                    }
                }

                // 7   y' R' U R U' d' (R U R') or y L' U L U2 y (R U R')
                if (aPieces[49] == aPieces[2] && aPieces[4] == aPieces[3] && aPieces[4] == aPieces[6] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[43] && aPieces[4] == aPieces[44])
                {
                    if (aPieces[13] == aPieces[1] && aPieces[13] == aPieces[9] && aPieces[13] == aPieces[14] && aPieces[13] == aPieces[16] && aPieces[13] == aPieces[17])
                    {
                        await MakeTurnAsync("y L' U L U2 y R U R'");
                        continue;
                    }
                }

                // 8   R U' R' U d (R' U' R) or R U' R' U2 y' (R' U' R) or (R U' R') U2 (F' U' F)
                if (aPieces[49] == aPieces[9] && aPieces[4] == aPieces[2] && aPieces[4] == aPieces[3] && aPieces[4] == aPieces[6] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[10])
                {
                    if (aPieces[13] == aPieces[14] && aPieces[13] == aPieces[16] && aPieces[13] == aPieces[17] && aPieces[13] == aPieces[41] && aPieces[13] == aPieces[44])
                    {
                        await MakeTurnAsync("R U' R' U2 F' U' F");
                        continue;
                    }
                }

                // Corner on top, FL color facing side, edge colors opposite
                // 9   y' (R' U' R)
                if (aPieces[49] == aPieces[2] && aPieces[4] == aPieces[3] && aPieces[4] == aPieces[6] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[44])
                {
                    if (aPieces[13] == aPieces[9] && aPieces[13] == aPieces[14] && aPieces[13] == aPieces[16] && aPieces[13] == aPieces[17] && aPieces[13] == aPieces[39])
                    {
                        await MakeTurnAsync("y' R' U' R");
                        continue;
                    }
                }

                // 10   (R U R')
                if (aPieces[49] == aPieces[9] && aPieces[4] == aPieces[2] && aPieces[4] == aPieces[3] && aPieces[4] == aPieces[6] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[37])
                {
                    if (aPieces[13] == aPieces[14] && aPieces[13] == aPieces[16] && aPieces[13] == aPieces[17] && aPieces[13] == aPieces[44])
                    {
                        await MakeTurnAsync("R U R'");
                        continue;
                    }
                }

                // 11   d R' U' R U' (R' U' R) or U' R U' R' d R' U' R or U' R U' R' U y' R' U' R
                if (aPieces[49] == aPieces[2] && aPieces[4] == aPieces[3] && aPieces[4] == aPieces[6] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[44])
                {
                    if (aPieces[13] == aPieces[9] && aPieces[13] == aPieces[14] && aPieces[13] == aPieces[16] && aPieces[13] == aPieces[17] && aPieces[13] == aPieces[37])
                    {
                        await MakeTurnAsync("d R' U' R U' R' U' R");
                        continue;
                    }
                }

                // 12   U' R U R' U (R U R')
                if (aPieces[49] == aPieces[9] && aPieces[4] == aPieces[2] && aPieces[4] == aPieces[3] && aPieces[4] == aPieces[6] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[39])
                {
                    if (aPieces[13] == aPieces[14] && aPieces[13] == aPieces[16] && aPieces[13] == aPieces[17] && aPieces[13] == aPieces[44])
                    {
                        await MakeTurnAsync("U' R U R' U R U R'");
                        continue;
                    }
                }

                // 13   U' R U2' R' d (R' U' R)
                if (aPieces[49] == aPieces[2] && aPieces[4] == aPieces[3] && aPieces[4] == aPieces[6] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[10] && aPieces[4] == aPieces[44])
                {
                    if (aPieces[13] == aPieces[9] && aPieces[13] == aPieces[14] && aPieces[13] == aPieces[16] && aPieces[13] == aPieces[17] && aPieces[13] == aPieces[41])
                    {
                        await MakeTurnAsync("U' R U2 R' d R' U' R");
                        continue;
                    }
                }

                // 14   R' U2 R2 U R2' U R or R U' R' U R U' R' U2 (R U' R') or d R' U2 R d' (R U R')	*Last R' U R can be avoided if back slot is empty.
                if (aPieces[49] == aPieces[9] && aPieces[4] == aPieces[2] && aPieces[4] == aPieces[3] && aPieces[4] == aPieces[6] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[43])
                {
                    if (aPieces[13] == aPieces[1] && aPieces[13] == aPieces[14] && aPieces[13] == aPieces[16] && aPieces[13] == aPieces[17] && aPieces[13] == aPieces[44])
                    {
                        await MakeTurnAsync("R' U2 R2 U R2 U R");
                        continue;
                    }
                }

                // 15   d R' U R U' (R' U' R)
                if (aPieces[49] == aPieces[2] && aPieces[4] == aPieces[1] && aPieces[4] == aPieces[3] && aPieces[4] == aPieces[6] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[44])
                {
                    if (aPieces[13] == aPieces[9] && aPieces[13] == aPieces[14] && aPieces[13] == aPieces[16] && aPieces[13] == aPieces[17] && aPieces[13] == aPieces[43])
                    {
                        await MakeTurnAsync("d R' U R U' R' U' R");
                        continue;
                    }
                }

                // 16   U' R U' R' U (R U R')
                if (aPieces[49] == aPieces[9] && aPieces[4] == aPieces[2] && aPieces[4] == aPieces[3] && aPieces[4] == aPieces[6] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[41])
                {
                    if (aPieces[13] == aPieces[10] && aPieces[13] == aPieces[14] && aPieces[13] == aPieces[16] && aPieces[13] == aPieces[17] && aPieces[13] == aPieces[44])
                    {
                        await MakeTurnAsync("U' R U' R' U R U R'");
                        continue;
                    }
                }

                // Corner on top, FL color facing up
                // 17   R U2' R' U' (R U R')
                if (aPieces[49] == aPieces[44] && aPieces[4] == aPieces[3] && aPieces[4] == aPieces[6] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[9] && aPieces[4] == aPieces[41])
                {
                    if (aPieces[13] == aPieces[2] && aPieces[13] == aPieces[10] && aPieces[13] == aPieces[14] && aPieces[13] == aPieces[16] && aPieces[13] == aPieces[17])
                    {
                        await MakeTurnAsync("R U2 R' U' R U R'");
                        continue;
                    }
                }

                // 18   y' R' U2 R U (R' U' R) or y (L' U2 L) U (L' U' L)
                if (aPieces[49] == aPieces[44] && aPieces[4] == aPieces[1] && aPieces[4] == aPieces[3] && aPieces[4] == aPieces[6] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[9])
                {
                    if (aPieces[13] == aPieces[2] && aPieces[13] == aPieces[14] && aPieces[13] == aPieces[16] && aPieces[13] == aPieces[17] && aPieces[13] == aPieces[43])
                    {
                        await MakeTurnAsync("y' R' U2 R U R' U' R");
                        continue;
                    }
                }

                // 19   U R U2 R' U (R U' R') or U R U2 R2 F R F'
                if (aPieces[49] == aPieces[44] && aPieces[4] == aPieces[3] && aPieces[4] == aPieces[6] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[9] && aPieces[4] == aPieces[37])
                {
                    if (aPieces[13] == aPieces[2] && aPieces[13] == aPieces[14] && aPieces[13] == aPieces[16] && aPieces[13] == aPieces[17])
                    {
                        await MakeTurnAsync("U R U2 R2 F R F'");
                        continue;
                    }
                }

                // 20   y' U' R' U2 R U' (R' U R)
                if (aPieces[49] == aPieces[44] && aPieces[4] == aPieces[3] && aPieces[4] == aPieces[6] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[9])
                {
                    if (aPieces[13] == aPieces[2] && aPieces[13] == aPieces[14] && aPieces[13] == aPieces[16] && aPieces[13] == aPieces[17] && aPieces[13] == aPieces[39])
                    {
                        await MakeTurnAsync("y' U' R' U2 R U' R' U R");
                        continue;
                    }
                }

                // 21   U2 R U R' U (R U' R') or (R U' R') U2' (R U R')
                if (aPieces[49] == aPieces[44] && aPieces[4] == aPieces[3] && aPieces[4] == aPieces[6] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[9] && aPieces[4] == aPieces[39])
                {
                    if (aPieces[13] == aPieces[2] && aPieces[13] == aPieces[14] && aPieces[13] == aPieces[16] && aPieces[13] == aPieces[17])
                    {
                        await MakeTurnAsync("R U' R' U2 R U R'");
                        continue;
                    }
                }

                // 22   y' U2 R' U' R U' (R' U R) or y' R' U R U2 (R' U' R)
                if (aPieces[49] == aPieces[44] && aPieces[4] == aPieces[3] && aPieces[4] == aPieces[6] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[9])
                {
                    if (aPieces[13] == aPieces[2] && aPieces[13] == aPieces[14] && aPieces[13] == aPieces[16] && aPieces[13] == aPieces[17] && aPieces[13] == aPieces[37])
                    {
                        await MakeTurnAsync("y' R' U R U2 R' U' R");
                        continue;
                    }
                }

                // 23   y' U R' U2 R y R U2 R' U R U' R' or U2 R2 U2 R' U' R U' R2 or R U R' U2' R U R' U'(R U R')
                if (aPieces[49] == aPieces[44] && aPieces[4] == aPieces[3] && aPieces[4] == aPieces[6] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[9] && aPieces[4] == aPieces[43])
                {
                    if (aPieces[13] == aPieces[1] && aPieces[13] == aPieces[2] && aPieces[13] == aPieces[14] && aPieces[13] == aPieces[16] && aPieces[13] == aPieces[17])
                    {
                        await MakeTurnAsync("U2 R2 U2 R' U' R U' R2");
                        continue;
                    }
                }

                // 24   U' R U2' R' y' R' U2 R U' R' U R or R U R' d R' U R U' (R' U R) or y' U2 R2 U2 R U R' U R2
                if (aPieces[49] == aPieces[44] && aPieces[4] == aPieces[3] && aPieces[4] == aPieces[6] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[9] && aPieces[4] == aPieces[10])
                {
                    if (aPieces[13] == aPieces[2] && aPieces[13] == aPieces[14] && aPieces[13] == aPieces[16] && aPieces[13] == aPieces[17] && aPieces[13] == aPieces[41])
                    {
                        await MakeTurnAsync("y' U2 R2 U2 R U R' U R2");
                        continue;
                    }
                }

                // Corner down, edge on top
                // 25   U R U' R' d' (L' U L) or U R U' R' U' y (L' U L)
                if (aPieces[49] == aPieces[47] && aPieces[4] == aPieces[1] && aPieces[4] == aPieces[3] && aPieces[4] == aPieces[6] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[8])
                {
                    if (aPieces[13] == aPieces[14] && aPieces[13] == aPieces[15] && aPieces[13] == aPieces[16] && aPieces[13] == aPieces[17] && aPieces[13] == aPieces[43])
                    {
                        await MakeTurnAsync("U R U' R' U' y L' U L");
                        continue;
                    }
                }

                // 26   y' U' R' U R r' U' R U M' or d' L' U L d(R U' R') or y U' (L' U L) y' U (R U' R')
                if (aPieces[49] == aPieces[47] && aPieces[4] == aPieces[3] && aPieces[4] == aPieces[6] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[8] && aPieces[4] == aPieces[41])
                {
                    if (aPieces[13] == aPieces[10] && aPieces[13] == aPieces[14] && aPieces[13] == aPieces[15] && aPieces[13] == aPieces[16] && aPieces[13] == aPieces[17])
                    {
                        await MakeTurnAsync("y U' L' U L y' U R U' R'");
                        continue;
                    }
                }

                // 27   y' R' U' R U (R' U' R)
                if (aPieces[49] == aPieces[8] && aPieces[4] == aPieces[1] && aPieces[4] == aPieces[3] && aPieces[4] == aPieces[6] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[15])
                {
                    if (aPieces[13] == aPieces[14] && aPieces[13] == aPieces[16] && aPieces[13] == aPieces[17] && aPieces[13] == aPieces[43])
                    {
                        await MakeTurnAsync("y' R' U' R U R' U' R");
                        continue;
                    }
                }

                // 28   R U R' U' (R U R')
                if (aPieces[49] == aPieces[15] && aPieces[4] == aPieces[3] && aPieces[4] == aPieces[6] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[41])
                {
                    if (aPieces[13] == aPieces[8] && aPieces[13] == aPieces[10] && aPieces[13] == aPieces[14] && aPieces[13] == aPieces[16] && aPieces[13] == aPieces[17])
                    {
                        await MakeTurnAsync("R U R' U' R U R'");
                        continue;
                    }
                }

                // 29   R U' R' U (R U' R')
                if (aPieces[49] == aPieces[8] && aPieces[4] == aPieces[3] && aPieces[4] == aPieces[6] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[15] && aPieces[4] == aPieces[41])
                {
                    if (aPieces[13] == aPieces[10] && aPieces[13] == aPieces[14] && aPieces[13] == aPieces[16] && aPieces[13] == aPieces[17])
                    {
                        await MakeTurnAsync("R U' R' U R U' R'");
                        continue;
                    }
                }

                // 30   y' R' U R U' (R' U R) or R U R' d (R' U2 R)
                if (aPieces[49] == aPieces[15] && aPieces[4] == aPieces[1] && aPieces[4] == aPieces[3] && aPieces[4] == aPieces[6] && aPieces[4] == aPieces[7])
                {
                    if (aPieces[13] == aPieces[8] && aPieces[13] == aPieces[14] && aPieces[13] == aPieces[16] && aPieces[13] == aPieces[17] && aPieces[13] == aPieces[43])
                    {
                        await MakeTurnAsync("R U R' d R' U2 R");
                        continue;
                    }
                }

                // Edge down, corner on top
                // 31   U' R U' R' U2 (R U' R') or d' L' U' R' U L U' R
                if (aPieces[49] == aPieces[2] && aPieces[4] == aPieces[3] && aPieces[4] == aPieces[5] && aPieces[4] == aPieces[6] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[44])
                {
                    if (aPieces[13] == aPieces[9] && aPieces[13] == aPieces[12] && aPieces[13] == aPieces[14] && aPieces[13] == aPieces[16] && aPieces[13] == aPieces[17])
                    {
                        await MakeTurnAsync("U' R U' R' U2 R U' R'");
                        continue;
                    }
                }

                // 32   d R' U R U2 (R' U R) or U' (R U2' R') U (R U R') or U R U R' U2 (R U R')
                if (aPieces[49] == aPieces[9] && aPieces[4] == aPieces[2] && aPieces[4] == aPieces[3] && aPieces[4] == aPieces[5] && aPieces[4] == aPieces[6] && aPieces[4] == aPieces[7])
                {
                    if (aPieces[13] == aPieces[12] && aPieces[13] == aPieces[14] && aPieces[13] == aPieces[16] && aPieces[13] == aPieces[17] && aPieces[13] == aPieces[44])
                    {
                        await MakeTurnAsync("U R U R' U2 R U R'");
                        continue;
                    }
                }

                // 33   U' R U R' d (R' U' R)
                if (aPieces[49] == aPieces[2] && aPieces[4] == aPieces[3] && aPieces[4] == aPieces[6] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[12] && aPieces[4] == aPieces[44])
                {
                    if (aPieces[13] == aPieces[5] && aPieces[13] == aPieces[9] && aPieces[13] == aPieces[14] && aPieces[13] == aPieces[16] && aPieces[13] == aPieces[17])
                    {
                        await MakeTurnAsync("U' R U R' d R' U' R");
                        continue;
                    }
                }

                // 34   d R' U' R d' (R U R') or y U2 (L' U L) U y (L U L')
                if (aPieces[49] == aPieces[9] && aPieces[4] == aPieces[2] && aPieces[4] == aPieces[3] && aPieces[4] == aPieces[6] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[12])
                {
                    if (aPieces[13] == aPieces[5] && aPieces[13] == aPieces[14] && aPieces[13] == aPieces[16] && aPieces[13] == aPieces[17] && aPieces[13] == aPieces[44])
                    {
                        await MakeTurnAsync("d R' U' R d' R U R'");
                        continue;
                    }
                }

                // 35   R U' R' d (R' U R)
                if (aPieces[49] == aPieces[44] && aPieces[4] == aPieces[3] && aPieces[4] == aPieces[6] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[9] && aPieces[4] == aPieces[12])
                {
                    if (aPieces[13] == aPieces[2] && aPieces[13] == aPieces[5] && aPieces[13] == aPieces[14] && aPieces[13] == aPieces[16] && aPieces[13] == aPieces[17])
                    {
                        await MakeTurnAsync("R U' R' d R' U R");
                        continue;
                    }
                }

                // 36   [R U R' U'][R U R' U'](R U R') or U [R U' R' U][R U' R' U](R U' R')
                if (aPieces[49] == aPieces[44] && aPieces[4] == aPieces[3] && aPieces[4] == aPieces[5] && aPieces[4] == aPieces[6] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[9])
                {
                    if (aPieces[13] == aPieces[2] && aPieces[13] == aPieces[12] && aPieces[13] == aPieces[14] && aPieces[13] == aPieces[16] && aPieces[13] == aPieces[17])
                    {
                        await MakeTurnAsync("R U R' U' R U R' U' R U R'");
                        continue;
                    }
                }

                // Corner down, edge down
                // 37   R U' R' U' R U R' U2 (R U' R') or y' R' U' R U2 R' U R U' (R' U' R)
                if (aPieces[49] == aPieces[8] && aPieces[4] == aPieces[3] && aPieces[4] == aPieces[5] && aPieces[4] == aPieces[6] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[15])
                {
                    if (aPieces[13] == aPieces[12] && aPieces[13] == aPieces[14] && aPieces[13] == aPieces[16] && aPieces[13] == aPieces[17] && aPieces[13] == aPieces[47])
                    {
                        await MakeTurnAsync("R U' R' U' R U R' U2 R U' R'");
                        continue;
                    }
                }

                // 38   R U R' U2 R U' R' U (R U R') or R U' R' U R U2' R' U (R U' R')
                if (aPieces[49] == aPieces[15] && aPieces[4] == aPieces[3] && aPieces[4] == aPieces[5] && aPieces[4] == aPieces[6] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[47])
                {
                    if (aPieces[13] == aPieces[8] && aPieces[13] == aPieces[12] && aPieces[13] == aPieces[14] && aPieces[13] == aPieces[16] && aPieces[13] == aPieces[17])
                    {
                        await MakeTurnAsync("R U R' U2 R U' R' U R U R'");
                        continue;
                    }
                }

                // 39   R U' R' d R' U' R U' (R' U' R)
                if (aPieces[49] == aPieces[8] && aPieces[4] == aPieces[3] && aPieces[4] == aPieces[6] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[12] && aPieces[4] == aPieces[15])
                {
                    if (aPieces[13] == aPieces[5] && aPieces[13] == aPieces[14] && aPieces[13] == aPieces[16] && aPieces[13] == aPieces[17] && aPieces[13] == aPieces[47])
                    {
                        await MakeTurnAsync("R U' R' d R' U' R U' R' U' R");
                        continue;
                    }
                }

                // 40   R U R' U' R U' R' U2 y' (R' U' R) or R U' R' U d R' U' R U' (R' U R)
                if (aPieces[49] == aPieces[15] && aPieces[4] == aPieces[3] && aPieces[4] == aPieces[6] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[12] && aPieces[4] == aPieces[47])
                {
                    if (aPieces[13] == aPieces[5] && aPieces[13] == aPieces[8] && aPieces[13] == aPieces[14] && aPieces[13] == aPieces[16] && aPieces[13] == aPieces[17])
                    {
                        await MakeTurnAsync("R U' R' U d R' U' R U' R' U R");
                        continue;
                    }
                }

                // 41   R U' R' U y' R' U2 R U2' (R' U R) or R U' R' d R' U2 R U2' (R' U R) or [R' F R F'][R U' R' U][R U' R' U2](R U' R')
                if (aPieces[49] == aPieces[47] && aPieces[4] == aPieces[3] && aPieces[4] == aPieces[6] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[8] && aPieces[4] == aPieces[12])
                {
                    if (aPieces[13] == aPieces[5] && aPieces[13] == aPieces[14] && aPieces[13] == aPieces[15] && aPieces[13] == aPieces[16] && aPieces[13] == aPieces[17])
                    {
                        await MakeTurnAsync("R U' R' d R' U2 R U2 R' U R");
                        continue;
                    }
                }

                //continue;

                //--------------------------------------------------------------------------------------------------------------

                // https://www.youtube.com/watch?v=Ar_Zit1VLG0
                // 4. Fundamental algorithms
                if (aPieces[49] == aPieces[29] && aPieces[4] == aPieces[42] && aPieces[4] == aPieces[43] && aPieces[13] == aPieces[0] && aPieces[13] == aPieces[1])
                {
                    await MakeTurnAsync("R U' R'");
                    continue;
                }

                if (aPieces[49] == aPieces[9] && aPieces[4] == aPieces[43] && aPieces[4] == aPieces[44] && aPieces[31] == aPieces[1] && aPieces[31] == aPieces[2])
                {
                    await MakeTurnAsync("L' U L");
                    continue;
                }

                if (aPieces[49] == aPieces[9] && aPieces[4] == aPieces[2] && aPieces[4] == aPieces[37] && aPieces[13] == aPieces[19] && aPieces[13] == aPieces[44])
                {
                    await MakeTurnAsync("R U R'");
                    continue;
                }

                if (aPieces[49] == aPieces[29] && aPieces[4] == aPieces[0] && aPieces[4] == aPieces[37] && aPieces[31] == aPieces[19] && aPieces[31] == aPieces[42])
                {
                    await MakeTurnAsync("L' U' L");
                    continue;
                }

                if (aPieces[49] == aPieces[15] && aPieces[4] == aPieces[28] && aPieces[4] == aPieces[47] && aPieces[13] == aPieces[8] && aPieces[13] == aPieces[39])
                {
                    await MakeTurnAsync("R U R'");

                    // 2. Is white facing up?
                    // 3.  If not, move the corner piece
                    if (aPieces[49] == aPieces[9])
                    {
                        await MakeTurnAsync("U");
                    }

                    // 4. Are top colors the same?
                    if (aPieces[42] == aPieces[37])
                    {
                        // 5. If, move the edge piece
                        await MakeTurnAsync("F' U' F U'");
                        // 6. Insert
                        await MakeTurnAsync("U' L' U L");
                        continue;
                    }

                    // 1. Both pieces in top layer
                    // 2. Is white facing up?
                    if (aPieces[49] == aPieces[29] && aPieces[4] == aPieces[42] && aPieces[4] == aPieces[10] && aPieces[13] == aPieces[0] && aPieces[13] == aPieces[41])
                    {
                        // 3. If not, move the corner piece
                        await MakeTurnAsync("U' U' ");

                        // 4. Are top colors the same?
                        if (aPieces[39] != aPieces[38])
                        {
                            // 5. If not, move the edge piece
                            await MakeTurnAsync("R U' R'");
                            // 6. Insert
                            await MakeTurnAsync("U L' U' L");
                        }

                        // 2. White is facing up
                        // 3. Move the edge piece
                        if (aPieces[49] == aPieces[44] && aPieces[4] == aPieces[2] && aPieces[4] == aPieces[19] && aPieces[31] == aPieces[9] && aPieces[31] == aPieces[37])
                        {
                            // 4. Put the corner above edge
                            await MakeTurnAsync("y' U2 R U2 R'");
                            //5. Insert
                            await MakeTurnAsync("R U' R'");
                        }
                    }

                    continue;
                }

                // Special cases (To many options)
                // Option 1: algorithms
                // Option 2: split it up

                //--------------------------------------------------------------------------------------------------------------

                // https://drive.google.com/file/d/1nzAXYUWZJ6H2wIOXaHdWXep3W57tArbR/view
                // Section 2: Advanced F2L
                // 36 advanced cases: 1 piece is in the wrong slot.
                // Section 2A: Edge is in the wrong slot.
                // White sticker faces Up
                // 1. U' R' U R2 U' R'
                if (aPieces[49] == aPieces[44] && aPieces[4] == aPieces[9] && aPieces[4] == aPieces[21])
                {
                    if (aPieces[13] == aPieces[2] && aPieces[13] == aPieces[14])
                    {
                        await MakeTurnAsync("U' R' U R2 U' R'");
                        continue;

                    }
                }

                // 2. y U L U' L2' U L
                if (aPieces[49] == aPieces[44] && aPieces[4] == aPieces[3] && aPieces[4] == aPieces[9])
                {
                    if (aPieces[13] == aPieces[2] && aPieces[13] == aPieces[32])
                    {
                        await MakeTurnAsync("y U L U' L2 U L");
                        continue;
                    }
                }

                // 3. U2 (R' U R) U' (S R S') or y R' F R2 U' R' U2 F' or y2 U2 (L F' L' F) (L U L')
                if (aPieces[49] == aPieces[44] && aPieces[4] == aPieces[9] && aPieces[4] == aPieces[14])
                {
                    if (aPieces[13] == aPieces[2] && aPieces[13] == aPieces[21])
                    {
                        await MakeTurnAsync("U2 R' U R U' S R S'");
                        continue;
                    }
                }

                // 4. y U2 (L U' L') U (S' L' S) or L F' L2' U L U2' F or y' U2 (R' F R F') (R' U' R)
                if (aPieces[49] == aPieces[44] && aPieces[4] == aPieces[9] && aPieces[4] == aPieces[32])
                {
                    if (aPieces[13] == aPieces[2] && aPieces[13] == aPieces[3])
                    {
                        await MakeTurnAsync("y U2 L U' L' U S' L' S");
                        continue;
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// OLL (Orientation of Last Layer) - 2-Look OLL
        /// </summary>
        /// <returns></returns>
        private static async Task<bool> SolveBottomLayerOrientationAsync()
        {
            int nLoopTimes = 0;

            while (true)
            {
                nLoopTimes++;
                if (nLoopTimes > nLoopTimesMax)
                {
                    Debug.WriteLine("CFOP: nLoopTimes last layer orientation: " + nLoopTimes);
                    return false;
                }

                // If solved, break the loop
                if (aPieces[40] == aPieces[36] && aPieces[40] == aPieces[37] && aPieces[40] == aPieces[38] && aPieces[40] == aPieces[39] && aPieces[40] == aPieces[41] && aPieces[40] == aPieces[42] && aPieces[40] == aPieces[43] && aPieces[40] == aPieces[44])
                {
                    Debug.WriteLine("CFOP: number of turns last layer orientation: " + lCubeTurns.Count);
                    break;
                }

                // Turn the cube
                if (nLoopTimes > 1)
                {
                    await MakeTurnAsync("y");
                }

                // https://ruwix.com/the-rubiks-cube/advanced-cfop-fridrich/orient-the-last-layer-oll/
                // Dot 1
                if (aPieces[40] == aPieces[1] && aPieces[40] == aPieces[9] && aPieces[40] == aPieces[10] && aPieces[40] == aPieces[11] && aPieces[40] == aPieces[19] && aPieces[40] == aPieces[27] && aPieces[40] == aPieces[28] && aPieces[40] == aPieces[29])
                {
                    await MakeTurnAsync("R U B' l U l2 x' U' R' F R F'");
                    continue;
                }

                // Dot 2
                if (aPieces[40] == aPieces[0] && aPieces[40] == aPieces[1] && aPieces[40] == aPieces[2] && aPieces[40] == aPieces[10] && aPieces[40] == aPieces[11] && aPieces[40] == aPieces[19] && aPieces[40] == aPieces[27] && aPieces[40] == aPieces[28])
                {
                    await MakeTurnAsync("R' F R F' U2 R' F R y' R2 U2 R");
                    continue;
                }

                // Dot 3
                if (aPieces[40] == aPieces[1] && aPieces[40] == aPieces[10] && aPieces[40] == aPieces[11] && aPieces[40] == aPieces[19] && aPieces[40] == aPieces[20] && aPieces[40] == aPieces[28] && aPieces[40] == aPieces[29] && aPieces[40] == aPieces[44])
                {
                    await MakeTurnAsync("y L' R2 B R' B L U2 L' B M'");
                    continue;
                }

                // Dot 4
                if (aPieces[40] == aPieces[0] && aPieces[40] == aPieces[1] && aPieces[40] == aPieces[9] && aPieces[40] == aPieces[10] && aPieces[40] == aPieces[19] && aPieces[40] == aPieces[27] && aPieces[40] == aPieces[28] && aPieces[40] == aPieces[38])
                {
                    await MakeTurnAsync("R' U2 x R' U R U' y R' U' R' U R' F");
                    continue;
                }

                // Dot 5
                if (aPieces[40] == aPieces[1] && aPieces[40] == aPieces[10] && aPieces[40] == aPieces[18] && aPieces[40] == aPieces[19] && aPieces[40] == aPieces[28] && aPieces[40] == aPieces[29] && aPieces[40] == aPieces[36] && aPieces[40] == aPieces[44])
                {
                    await MakeTurnAsync("R U R' U R' F R F' U2 R' F R F'");
                    continue;
                }

                // Dot 6
                if (aPieces[40] == aPieces[1] && aPieces[40] == aPieces[10] && aPieces[40] == aPieces[19] && aPieces[40] == aPieces[28] && aPieces[40] == aPieces[36] && aPieces[40] == aPieces[38] && aPieces[40] == aPieces[42] && aPieces[40] == aPieces[44])
                {
                    await MakeTurnAsync("M' U2 M U2 M' U M U2 M' U2 M");
                    continue;
                }

                // Dot 7
                if (aPieces[40] == aPieces[1] && aPieces[40] == aPieces[9] && aPieces[40] == aPieces[10] && aPieces[40] == aPieces[19] && aPieces[40] == aPieces[28] && aPieces[40] == aPieces[29] && aPieces[40] == aPieces[36] && aPieces[40] == aPieces[38])
                {
                    await MakeTurnAsync("R' U2 F R U R' U' y' R2 U2 x' R U");
                    continue;
                }

                // Dot 8
                if (aPieces[40] == aPieces[1] && aPieces[40] == aPieces[10] && aPieces[40] == aPieces[18] && aPieces[40] == aPieces[19] && aPieces[40] == aPieces[20] && aPieces[40] == aPieces[28] && aPieces[40] == aPieces[42] && aPieces[40] == aPieces[44])
                {
                    await MakeTurnAsync("F R U R' U y' R' U2 R' F R F'");
                    continue;
                }

                // Line 1
                if (aPieces[40] == aPieces[0] && aPieces[40] == aPieces[9] && aPieces[40] == aPieces[10] && aPieces[40] == aPieces[11] && aPieces[40] == aPieces[20] && aPieces[40] == aPieces[28] && aPieces[40] == aPieces[37] && aPieces[40] == aPieces[43])
                {
                    await MakeTurnAsync("R' U' y L' U L' y' L F L' F R");
                    continue;
                }

                // Line 2
                if (aPieces[40] == aPieces[9] && aPieces[40] == aPieces[10] && aPieces[40] == aPieces[11] && aPieces[40] == aPieces[27] && aPieces[40] == aPieces[28] && aPieces[40] == aPieces[29] && aPieces[40] == aPieces[37] && aPieces[40] == aPieces[43])
                {
                    await MakeTurnAsync("R U' y R2 D R' U2 R D' R2 d R'");
                    continue;
                }

                // Line 3
                if (aPieces[40] == aPieces[0] && aPieces[40] == aPieces[1] && aPieces[40] == aPieces[9] && aPieces[40] == aPieces[11] && aPieces[40] == aPieces[19] && aPieces[40] == aPieces[20] && aPieces[40] == aPieces[39] && aPieces[40] == aPieces[41])
                {
                    await MakeTurnAsync("F U R U' R' U R U' R' F'");
                    continue;
                }

                // Line 4
                if (aPieces[40] == aPieces[1] && aPieces[40] == aPieces[9] && aPieces[40] == aPieces[11] && aPieces[40] == aPieces[19] && aPieces[40] == aPieces[27] && aPieces[40] == aPieces[29] && aPieces[40] == aPieces[39] && aPieces[40] == aPieces[41])
                {
                    await MakeTurnAsync("L' B' L U' R' U R U' R' U R L' B L");
                    continue;
                }

                // Cross 1
                if (aPieces[40] == aPieces[0] && aPieces[40] == aPieces[9] && aPieces[40] == aPieces[11] && aPieces[40] == aPieces[20] && aPieces[40] == aPieces[37] && aPieces[40] == aPieces[39] && aPieces[40] == aPieces[41] && aPieces[40] == aPieces[43])
                {
                    await MakeTurnAsync("L U' R' U L' U R U R' U R");
                    continue;
                }

                // Cross 2
                if (aPieces[40] == aPieces[9] && aPieces[40] == aPieces[11] && aPieces[40] == aPieces[27] && aPieces[40] == aPieces[29] && aPieces[40] == aPieces[37] && aPieces[40] == aPieces[39] && aPieces[40] == aPieces[41] && aPieces[40] == aPieces[43])
                {
                    await MakeTurnAsync("R U R' U R U' R' U R U2 R'");
                    continue;
                }

                // Cross 3
                if (aPieces[40] == aPieces[0] && aPieces[40] == aPieces[18] && aPieces[40] == aPieces[27] && aPieces[40] == aPieces[37] && aPieces[40] == aPieces[39] && aPieces[40] == aPieces[41] && aPieces[40] == aPieces[43] && aPieces[40] == aPieces[44])
                {
                    await MakeTurnAsync("L' U R U' L U R'");
                    continue;
                }

                // Cross 4
                if (aPieces[40] == aPieces[11] && aPieces[40] == aPieces[20] && aPieces[40] == aPieces[29] && aPieces[40] == aPieces[37] && aPieces[40] == aPieces[39] && aPieces[40] == aPieces[41] && aPieces[40] == aPieces[43] && aPieces[40] == aPieces[44])
                {
                    await MakeTurnAsync("R' U2 R U R' U R");
                    continue;
                }

                // Cross 5
                if (aPieces[40] == aPieces[2] && aPieces[40] == aPieces[18] && aPieces[40] == aPieces[36] && aPieces[40] == aPieces[37] && aPieces[40] == aPieces[39] && aPieces[40] == aPieces[41] && aPieces[40] == aPieces[42] && aPieces[40] == aPieces[43])
                {
                    await MakeTurnAsync("R' F' L F R F' L' F");
                    continue;
                }

                // Cross 6
                if (aPieces[40] == aPieces[0] && aPieces[40] == aPieces[2] && aPieces[40] == aPieces[36] && aPieces[40] == aPieces[37] && aPieces[40] == aPieces[38] && aPieces[40] == aPieces[39] && aPieces[40] == aPieces[41] && aPieces[40] == aPieces[43])
                {
                    await MakeTurnAsync("R2 D R' U2 R D' R' U2 R'");
                    continue;
                }

                // Cross 7
                if (aPieces[40] == aPieces[18] && aPieces[40] == aPieces[29] && aPieces[40] == aPieces[36] && aPieces[40] == aPieces[37] && aPieces[40] == aPieces[39] && aPieces[40] == aPieces[41] && aPieces[40] == aPieces[43] && aPieces[40] == aPieces[44])
                {
                    await MakeTurnAsync("R' F' L' F R F' L F");
                    continue;
                }

                // 4 corners 1
                if (aPieces[40] == aPieces[10] && aPieces[40] == aPieces[19] && aPieces[40] == aPieces[36] && aPieces[40] == aPieces[38] && aPieces[40] == aPieces[39] && aPieces[40] == aPieces[42] && aPieces[40] == aPieces[43] && aPieces[40] == aPieces[44])
                {
                    await MakeTurnAsync("M' U' M U2 M' U' M");
                    continue;
                }

                // 4 corners 2
                if (aPieces[40] == aPieces[1] && aPieces[40] == aPieces[19] && aPieces[40] == aPieces[36] && aPieces[40] == aPieces[38] && aPieces[40] == aPieces[39] && aPieces[40] == aPieces[41] && aPieces[40] == aPieces[42] && aPieces[40] == aPieces[44])
                {
                    await MakeTurnAsync("L' R U R' U' L R' F R F'");
                    continue;
                }

                // Shape _| 1
                if (aPieces[40] == aPieces[1] && aPieces[40] == aPieces[2] && aPieces[40] == aPieces[10] && aPieces[40] == aPieces[11] && aPieces[40] == aPieces[20] && aPieces[40] == aPieces[37] && aPieces[40] == aPieces[39] && aPieces[40] == aPieces[42])
                {
                    await MakeTurnAsync("L F R' F R F2 L'");
                    continue;
                }

                // Shape _| 2
                if (aPieces[40] == aPieces[0] && aPieces[40] == aPieces[1] && aPieces[40] == aPieces[10] && aPieces[40] == aPieces[11] && aPieces[40] == aPieces[36] && aPieces[40] == aPieces[37] && aPieces[40] == aPieces[39] && aPieces[40] == aPieces[44])
                {
                    await MakeTurnAsync("F R' F' R U R U' R'");
                    continue;
                }

                // Shape _| 3
                if (aPieces[40] == aPieces[0] && aPieces[40] == aPieces[1] && aPieces[40] == aPieces[10] && aPieces[40] == aPieces[18] && aPieces[40] == aPieces[27] && aPieces[40] == aPieces[37] && aPieces[40] == aPieces[39] && aPieces[40] == aPieces[44])
                {
                    await MakeTurnAsync("R' U' R y' x' R U' R' F R U R'");
                    continue;
                }

                // Shape _| 4
                if (aPieces[40] == aPieces[1] && aPieces[40] == aPieces[10] && aPieces[40] == aPieces[18] && aPieces[40] == aPieces[20] && aPieces[40] == aPieces[37] && aPieces[40] == aPieces[39] && aPieces[40] == aPieces[42] && aPieces[40] == aPieces[44])
                {
                    await MakeTurnAsync("U' R U2 R' U' R U' R2 y' R' U' R U B");
                    continue;
                }

                // Shape _| 5
                if (aPieces[40] == aPieces[1] && aPieces[40] == aPieces[2] && aPieces[40] == aPieces[10] && aPieces[40] == aPieces[18] && aPieces[40] == aPieces[27] && aPieces[40] == aPieces[29] && aPieces[40] == aPieces[37] && aPieces[40] == aPieces[39])
                {
                    await MakeTurnAsync("F R U R' U' R U R' U' F'");
                    continue;
                }

                // Shape _| 6
                if (aPieces[40] == aPieces[0] && aPieces[40] == aPieces[1] && aPieces[40] == aPieces[2] && aPieces[40] == aPieces[10] && aPieces[40] == aPieces[18] && aPieces[40] == aPieces[20] && aPieces[40] == aPieces[37] && aPieces[40] == aPieces[39])
                {
                    await MakeTurnAsync("L F' L' F U2 L2 y' L F L' F");
                    continue;
                }

                // Shape |_ 1
                if (aPieces[40] == aPieces[1] && aPieces[40] == aPieces[18] && aPieces[40] == aPieces[20] && aPieces[40] == aPieces[28] && aPieces[40] == aPieces[37] && aPieces[40] == aPieces[41] && aPieces[40] == aPieces[42] && aPieces[40] == aPieces[44])
                {
                    await MakeTurnAsync("U' R' U2 R U R' U R2 y R U R' U' F'");
                    continue;
                }

                // Shape |_ 2
                if (aPieces[40] == aPieces[0] && aPieces[40] == aPieces[1] && aPieces[40] == aPieces[9] && aPieces[40] == aPieces[27] && aPieces[40] == aPieces[28] && aPieces[40] == aPieces[37] && aPieces[40] == aPieces[38] && aPieces[40] == aPieces[41])
                {
                    await MakeTurnAsync("r U2 R' U' R U' r'");
                    continue;
                }

                // Shape |_ 3
                if (aPieces[40] == aPieces[1] && aPieces[40] == aPieces[9] && aPieces[40] == aPieces[20] && aPieces[40] == aPieces[28] && aPieces[40] == aPieces[37] && aPieces[40] == aPieces[38] && aPieces[40] == aPieces[41] && aPieces[40] == aPieces[42])
                {
                    await MakeTurnAsync("R' U2 l R U' R' U l' U2 R");
                    continue;
                }

                // Shape |_ 4
                if (aPieces[40] == aPieces[0] && aPieces[40] == aPieces[1] && aPieces[40] == aPieces[9] && aPieces[40] == aPieces[11] && aPieces[40] == aPieces[20] && aPieces[40] == aPieces[28] && aPieces[40] == aPieces[37] && aPieces[40] == aPieces[41])
                {
                    await MakeTurnAsync("F' L' U' L U L' U' L U F");
                    continue;
                }

                // Shape |_ 5
                if (aPieces[40] == aPieces[1] && aPieces[40] == aPieces[2] && aPieces[40] == aPieces[18] && aPieces[40] == aPieces[27] && aPieces[40] == aPieces[28] && aPieces[40] == aPieces[29] && aPieces[40] == aPieces[37] && aPieces[40] == aPieces[41])
                {
                    await MakeTurnAsync("R' F R' F' R2 U2 x' U' R U R'");
                    continue;
                }

                // Shape |_ 6
                if (aPieces[40] == aPieces[0] && aPieces[40] == aPieces[1] && aPieces[40] == aPieces[2] && aPieces[40] == aPieces[18] && aPieces[40] == aPieces[20] && aPieces[40] == aPieces[28] && aPieces[40] == aPieces[37] && aPieces[40] == aPieces[41])
                {
                    await MakeTurnAsync("R' F R F' U2 R2 y R' F' R F'");
                    continue;
                }

                // Shape -| 1
                if (aPieces[40] == aPieces[2] && aPieces[40] == aPieces[10] && aPieces[40] == aPieces[19] && aPieces[40] == aPieces[20] && aPieces[40] == aPieces[29] && aPieces[40] == aPieces[38] && aPieces[40] == aPieces[39] && aPieces[40] == aPieces[43])
                {
                    await MakeTurnAsync("R U R' y R' F R U' R' F' R");
                    continue;
                }

                // Shape -| 2
                if (aPieces[40] == aPieces[0] && aPieces[40] == aPieces[9] && aPieces[40] == aPieces[10] && aPieces[40] == aPieces[18] && aPieces[40] == aPieces[19] && aPieces[40] == aPieces[36] && aPieces[40] == aPieces[39] && aPieces[40] == aPieces[43])
                {
                    await MakeTurnAsync("L' B' L U' R' U R L' B L");
                    continue;
                }

                // Shape -| 3
                if (aPieces[40] == aPieces[0] && aPieces[40] == aPieces[10] && aPieces[40] == aPieces[18] && aPieces[40] == aPieces[19] && aPieces[40] == aPieces[27] && aPieces[40] == aPieces[39] && aPieces[40] == aPieces[43] && aPieces[40] == aPieces[44])
                {
                    await MakeTurnAsync("U2 r R2 U' R U' R' U2 R U' M");
                    continue;
                }

                // Shape -| 4
                if (aPieces[40] == aPieces[9] && aPieces[40] == aPieces[10] && aPieces[40] == aPieces[19] && aPieces[40] == aPieces[29] && aPieces[40] == aPieces[36] && aPieces[40] == aPieces[38] && aPieces[40] == aPieces[39] && aPieces[40] == aPieces[43])
                {
                    await MakeTurnAsync("x' U' R U' R2 F x R U R' U' R B2");
                    continue;
                }

                // Shape |- 1
                if (aPieces[40] == aPieces[2] && aPieces[40] == aPieces[18] && aPieces[40] == aPieces[19] && aPieces[40] == aPieces[27] && aPieces[40] == aPieces[28] && aPieces[40] == aPieces[29] && aPieces[40] == aPieces[41] && aPieces[40] == aPieces[43])
                {
                    await MakeTurnAsync("L U' y' R' U2 R' U R U' R U2 R d' L'");
                    continue;
                }

                // Shape |- 2
                if (aPieces[40] == aPieces[2] && aPieces[40] == aPieces[11] && aPieces[40] == aPieces[19] && aPieces[40] == aPieces[20] && aPieces[40] == aPieces[28] && aPieces[40] == aPieces[41] && aPieces[40] == aPieces[42] && aPieces[40] == aPieces[43])
                {
                    await MakeTurnAsync("U2 l' L2 U L' U L U2 L' U M");
                    continue;
                }

                // Shape |- 3
                if (aPieces[40] == aPieces[9] && aPieces[40] == aPieces[19] && aPieces[40] == aPieces[28] && aPieces[40] == aPieces[29] && aPieces[40] == aPieces[36] && aPieces[40] == aPieces[38] && aPieces[40] == aPieces[41] && aPieces[40] == aPieces[43])
                {
                    await MakeTurnAsync("R2 U R' B' R U' R2 U l U l'");
                    continue;
                }

                // Shape |- 4
                if (aPieces[40] == aPieces[11] && aPieces[40] == aPieces[19] && aPieces[40] == aPieces[20] && aPieces[40] == aPieces[28] && aPieces[40] == aPieces[29] && aPieces[40] == aPieces[41] && aPieces[40] == aPieces[43] && aPieces[40] == aPieces[44])
                {
                    await MakeTurnAsync("r' U2 R U R' U r");
                    continue;
                }

                // C 1
                if (aPieces[40] == aPieces[9] && aPieces[40] == aPieces[10] && aPieces[40] == aPieces[11] && aPieces[40] == aPieces[28] && aPieces[40] == aPieces[36] && aPieces[40] == aPieces[37] && aPieces[40] == aPieces[42] && aPieces[40] == aPieces[43])
                {
                    await MakeTurnAsync("R U x' R U' R' U x U' R'");
                    continue;
                }

                // C 2
                if (aPieces[40] == aPieces[1] && aPieces[40] == aPieces[11] && aPieces[40] == aPieces[19] && aPieces[40] == aPieces[27] && aPieces[40] == aPieces[39] && aPieces[40] == aPieces[41] && aPieces[40] == aPieces[42] && aPieces[40] == aPieces[44])
                {
                    await MakeTurnAsync("R U R' U' x D' R' U R E'");
                    continue;
                }

                // L 1
                if (aPieces[40] == aPieces[0] && aPieces[40] == aPieces[1] && aPieces[40] == aPieces[18] && aPieces[40] == aPieces[19] && aPieces[40] == aPieces[27] && aPieces[40] == aPieces[39] && aPieces[40] == aPieces[41] && aPieces[40] == aPieces[44])
                {
                    await MakeTurnAsync("R' F R U R' F' R y L U' L'");
                    continue;
                }

                // L 2
                if (aPieces[40] == aPieces[1] && aPieces[40] == aPieces[2] && aPieces[40] == aPieces[11] && aPieces[40] == aPieces[19] && aPieces[40] == aPieces[20] && aPieces[40] == aPieces[39] && aPieces[40] == aPieces[41] && aPieces[40] == aPieces[42])
                {
                    await MakeTurnAsync("L F' L' U' L F L' y' R' U R");
                    continue;
                }

                // L 3
                if (aPieces[40] == aPieces[1] && aPieces[40] == aPieces[11] && aPieces[40] == aPieces[19] && aPieces[40] == aPieces[20] && aPieces[40] == aPieces[29] && aPieces[40] == aPieces[39] && aPieces[40] == aPieces[41] && aPieces[40] == aPieces[44])
                {
                    await MakeTurnAsync("L' B' L R' U' R U L' B L");
                    continue;
                }

                // L 4
                if (aPieces[40] == aPieces[1] && aPieces[40] == aPieces[9] && aPieces[40] == aPieces[18] && aPieces[40] == aPieces[19] && aPieces[40] == aPieces[27] && aPieces[40] == aPieces[39] && aPieces[40] == aPieces[41] && aPieces[40] == aPieces[42])
                {
                    await MakeTurnAsync("R B R' L U L' U' R B' R'");
                    continue;
                }

                // P 1
                if (aPieces[40] == aPieces[1] && aPieces[40] == aPieces[9] && aPieces[40] == aPieces[10] && aPieces[40] == aPieces[11] && aPieces[40] == aPieces[36] && aPieces[40] == aPieces[37] && aPieces[40] == aPieces[39] && aPieces[40] == aPieces[42])
                {
                    await MakeTurnAsync("F U R U' R' F'");
                    continue;
                }

                // P 2
                if (aPieces[40] == aPieces[0] && aPieces[40] == aPieces[1] && aPieces[40] == aPieces[20] && aPieces[40] == aPieces[28] && aPieces[40] == aPieces[37] && aPieces[40] == aPieces[38] && aPieces[40] == aPieces[41] && aPieces[40] == aPieces[44])
                {
                    await MakeTurnAsync("R' d' L d R U' R' F' R");
                    continue;
                }

                // P 3
                if (aPieces[40] == aPieces[1] && aPieces[40] == aPieces[2] && aPieces[40] == aPieces[10] && aPieces[40] == aPieces[18] && aPieces[40] == aPieces[36] && aPieces[40] == aPieces[37] && aPieces[40] == aPieces[39] && aPieces[40] == aPieces[42])
                {
                    await MakeTurnAsync("L d R' d' L' U L F L'");
                    continue;
                }

                // P 4
                if (aPieces[40] == aPieces[1] && aPieces[40] == aPieces[27] && aPieces[40] == aPieces[28] && aPieces[40] == aPieces[29] && aPieces[40] == aPieces[37] && aPieces[40] == aPieces[38] && aPieces[40] == aPieces[41] && aPieces[40] == aPieces[44])
                {
                    await MakeTurnAsync("F' U' L' U L F");
                    continue;
                }

                // T 1
                if (aPieces[40] == aPieces[1] && aPieces[40] == aPieces[19] && aPieces[40] == aPieces[27] && aPieces[40] == aPieces[29] && aPieces[40] == aPieces[38] && aPieces[40] == aPieces[39] && aPieces[40] == aPieces[41] && aPieces[40] == aPieces[44])
                {
                    await MakeTurnAsync("F R U R' U' F'");
                    continue;
                }

                // T 2
                if (aPieces[40] == aPieces[0] && aPieces[40] == aPieces[1] && aPieces[40] == aPieces[19] && aPieces[40] == aPieces[20] && aPieces[40] == aPieces[38] && aPieces[40] == aPieces[39] && aPieces[40] == aPieces[41] && aPieces[40] == aPieces[44])
                {
                    await MakeTurnAsync("R U R' U' R' F R F'");
                    continue;
                }

                // W 1
                if (aPieces[40] == aPieces[2] && aPieces[40] == aPieces[19] && aPieces[40] == aPieces[27] && aPieces[40] == aPieces[28] && aPieces[40] == aPieces[38] && aPieces[40] == aPieces[41] && aPieces[40] == aPieces[42] && aPieces[40] == aPieces[43])
                {
                    await MakeTurnAsync("L U L' U L U' L' U' y2 R' F R F'");
                    continue;
                }

                // W 2
                if (aPieces[40] == aPieces[0] && aPieces[40] == aPieces[10] && aPieces[40] == aPieces[11] && aPieces[40] == aPieces[19] && aPieces[40] == aPieces[36] && aPieces[40] == aPieces[39] && aPieces[40] == aPieces[43] && aPieces[40] == aPieces[44])
                {
                    await MakeTurnAsync("R' U' R U' R' U R U y F R' F' R");
                    continue;
                }

                // Z 1
                if (aPieces[40] == aPieces[1] && aPieces[40] == aPieces[18] && aPieces[40] == aPieces[19] && aPieces[40] == aPieces[29] && aPieces[40] == aPieces[36] && aPieces[40] == aPieces[39] && aPieces[40] == aPieces[41] && aPieces[40] == aPieces[44])
                {
                    await MakeTurnAsync("R' F R U R' U' y L' d R");
                    continue;
                }

                // Z 2
                if (aPieces[40] == aPieces[1] && aPieces[40] == aPieces[9] && aPieces[40] == aPieces[19] && aPieces[40] == aPieces[20] && aPieces[40] == aPieces[38] && aPieces[40] == aPieces[39] && aPieces[40] == aPieces[41] && aPieces[40] == aPieces[42])
                {
                    await MakeTurnAsync("L F' L' U' L U y' R d' L'");
                    continue;
                }
            }

            return true;
        }

        /// <summary>
        ///  Permutate the last layer - PLL
        /// </summary>
        /// <returns></returns>
        private static async Task<bool> SolveBottomLayerPermutationAsync()
        {
            await ClassSolveCubeCommon.SolveTopLayerLineUpCenterAsync();

            int nLoopTimes = 0;

            while (true)
            {
                nLoopTimes++;
                if (nLoopTimes > nLoopTimesMax)
                {
                    Debug.WriteLine("CFOP: nLoopTimes last layer permutation: " + nLoopTimes);
                    return false;
                }

                // If solved, break the loop
                if (aPieces[40] == aPieces[36] && aPieces[40] == aPieces[37] && aPieces[40] == aPieces[38] && aPieces[40] == aPieces[39] && aPieces[40] == aPieces[41] && aPieces[40] == aPieces[42] && aPieces[40] == aPieces[43] && aPieces[40] == aPieces[44])
                {
                    if (aPieces[4] == aPieces[0] && aPieces[4] == aPieces[1] && aPieces[4] == aPieces[2] && aPieces[13] == aPieces[9] && aPieces[13] == aPieces[10] && aPieces[13] == aPieces[11] && aPieces[22] == aPieces[18] && aPieces[22] == aPieces[19] && aPieces[22] == aPieces[20] && aPieces[31] == aPieces[27] && aPieces[31] == aPieces[28] && aPieces[31] == aPieces[29])
                    {
                        Debug.WriteLine("CFOP: number of turns last layer permutation: " + lCubeTurns.Count);
                        break;
                    }
                }

                // Turn the cube
                if (nLoopTimes > 1)
                {
                    await MakeTurnAsync("y");
                }

                // https://solvethecube.com/algorithms
                // PLL Edges only
                // H
                if (aPieces[4] == aPieces[19] && aPieces[22] == aPieces[1] && aPieces[13] == aPieces[28] && aPieces[31] == aPieces[10])
                {
                    await MakeTurnAsync("M2 U M2 U2 M2 U M2");
                    continue;
                }

                // Z
                if (aPieces[4] == aPieces[10] && aPieces[13] == aPieces[1] && aPieces[22] == aPieces[28] && aPieces[31] == aPieces[19])
                {
                    await MakeTurnAsync("R' U' R2 U R U R' U' R U R U' R U' R' U2");
                    continue;
                }

                // Ua
                if (aPieces[13] == aPieces[28] && aPieces[22] == aPieces[10] && aPieces[31] == aPieces[19])
                {
                    await MakeTurnAsync("R2 U' R' U' R U R U R U' R");
                    continue;
                }

                // Ub
                if (aPieces[13] == aPieces[19] && aPieces[22] == aPieces[28] && aPieces[31] == aPieces[10])
                {
                    await MakeTurnAsync("R' U R' U' R' U' R' U R U R2");
                    continue;
                }

                // PLL Corners only
                // Aa
                if (aPieces[4] == aPieces[11] && aPieces[13] == aPieces[20] && aPieces[31] == aPieces[9])
                {
                    await MakeTurnAsync("x z' R2 U2 R' D' R U2 R' D R' z x'");
                    continue;
                }

                // Ab
                if (aPieces[4] == aPieces[20] && aPieces[13] == aPieces[2] && aPieces[22] == aPieces[11])
                {
                    await MakeTurnAsync("x R2 D2 R U R' D2 R U' R x'");
                    continue;
                }

                // E
                if (aPieces[13] == aPieces[0] && aPieces[31] == aPieces[2] && aPieces[13] == aPieces[20] && aPieces[31] == aPieces[18])
                {
                    await MakeTurnAsync("R2 U R' U' y R U R' U' R U R' U' R U R' y' R U' R2");
                    continue;
                }

                // PLL Edges and corners
                // T
                if (aPieces[4] == aPieces[11] && aPieces[22] == aPieces[9] && aPieces[13] == aPieces[28] && aPieces[31] == aPieces[10])
                {
                    await MakeTurnAsync("R U R' U' R' F R2 U' R' U' R U R' F'");
                    continue;
                }

                // Y
                if (aPieces[13] == aPieces[27] && aPieces[31] == aPieces[9] && aPieces[22] == aPieces[28] && aPieces[31] == aPieces[19])
                {
                    await MakeTurnAsync("F R U' R' U' R U R' F' R U R' U' R' F R F'");
                    continue;
                }

                // F
                if (aPieces[4] == aPieces[19] && aPieces[22] == aPieces[1] && aPieces[4] == aPieces[11] && aPieces[22] == aPieces[9])
                {
                    await MakeTurnAsync("U' R' U R U' R2 F' U' F U x R U R' U' R2 x'");
                    continue;
                }

                // V
                if (aPieces[4] == aPieces[20] && aPieces[22] == aPieces[2] && aPieces[13] == aPieces[19] && aPieces[22] == aPieces[10])
                {
                    await MakeTurnAsync("R' U R' U' y R' D R' D' R2 y' R' B' R B R");
                    continue;
                }

                // Ja
                if (aPieces[4] == aPieces[27] && aPieces[22] == aPieces[29] && aPieces[4] == aPieces[28] && aPieces[31] == aPieces[1])
                {
                    await MakeTurnAsync("L' U' L F L' U' L U L F' L2 U L U");
                    continue;
                }

                // Jb
                if (aPieces[4] == aPieces[11] && aPieces[22] == aPieces[9] && aPieces[4] == aPieces[10] && aPieces[13] == aPieces[1])
                {
                    await MakeTurnAsync("R U R' F' R U R' U' R' F R2 U' R' U'");
                    continue;
                }

                // Ra
                if (aPieces[4] == aPieces[28] && aPieces[31] == aPieces[1] && aPieces[13] == aPieces[20] && aPieces[31] == aPieces[18])
                {
                    await MakeTurnAsync("L U2 L' U2 L F' L' U' L U L F L2 U");
                    continue;
                }

                // Rb
                if (aPieces[4] == aPieces[10] && aPieces[13] == aPieces[1] && aPieces[13] == aPieces[20] && aPieces[31] == aPieces[18])
                {
                    await MakeTurnAsync("R' U2 R U2 R' F R U R' U' R' F' R2 U'");
                    continue;
                }

                // Na
                if (aPieces[4] == aPieces[18] && aPieces[22] == aPieces[0] && aPieces[13] == aPieces[28] && aPieces[31] == aPieces[10])
                {
                    await MakeTurnAsync("R U R' U R U R' F' R U R' U' R' F R2 U' R' U2 R U' R'");
                    continue;
                }

                // Nb
                if (aPieces[4] == aPieces[20] && aPieces[22] == aPieces[2] && aPieces[13] == aPieces[28] && aPieces[31] == aPieces[10])
                {
                    await MakeTurnAsync("R' U R U' R' F' U' F R U R' F R' F' R U' R");
                    continue;
                }

                // Ga
                if (aPieces[4] == aPieces[20] && aPieces[13] == aPieces[27] && aPieces[22] == aPieces[29])
                {
                    if (aPieces[4] == aPieces[28] && aPieces[22] == aPieces[1] && aPieces[31] == aPieces[19])
                    {
                        await MakeTurnAsync("y R2 u R' U R' U' R u' R2 y' R' U R");
                        continue;
                    }
                }

                // Gb
                if (aPieces[4] == aPieces[27] && aPieces[13] == aPieces[0] && aPieces[22] == aPieces[2])
                {
                    if (aPieces[4] == aPieces[19] && aPieces[31] == aPieces[1] && aPieces[22] == aPieces[28])
                    {
                        await MakeTurnAsync("R' U' R y R2 u R' U R U' R u' R2");
                        continue;
                    }
                }

                // Gc
                if (aPieces[4] == aPieces[29] && aPieces[31] == aPieces[11] && aPieces[22] == aPieces[9])
                {
                    if (aPieces[4] == aPieces[10] && aPieces[13] == aPieces[19] && aPieces[22] == aPieces[1])
                    {
                        await MakeTurnAsync("y R2 u' R U' R U R' u R2 y R U' R'");
                        continue;
                    }
                }

                // Gd
                if (aPieces[4] == aPieces[11] && aPieces[31] == aPieces[2] && aPieces[22] == aPieces[0])
                {
                    if (aPieces[4] == aPieces[19] && aPieces[13] == aPieces[1] && aPieces[22] == aPieces[10])
                    {
                        await MakeTurnAsync("y2 R U R' y' R2 u' R U' R' U R' u R2");
                        continue;
                    }
                }
            }

            return true;
        }
    }
}