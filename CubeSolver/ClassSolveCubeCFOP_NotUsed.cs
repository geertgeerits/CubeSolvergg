// https://www.rubiksplace.com/speedcubing/F2L-algorithms/ page 64
// https://www.myrubik.com/descarregues.php?lang=en page 368
// file://C:/Sources/MAUI/CubeSolver/Miscellaneous/Manuals/CubeSkills/f2l-algorithms-different-slot-positions.pdf page 666

using System.Diagnostics;
using static CubeSolver.Globals;

namespace CubeSolver
{
    internal sealed class ClassSolveCubeCFOP_B
    {
        //// Declare variables
        private const int nLoopTimesMax = 200;

        /// <summary>
        /// Solve the cube using the CFOP method
        /// </summary>
        /// <returns></returns>
        private static async Task<bool> SolveTheCubeCFOPAsync()
        {
            // F2L (Solving the first two layers completely)
            if (!await SolveFirstTwoLayersAsync())
            {
                return false;
            }

            // Check if the cube is solved
            if (ClassColorsCube.CheckIfSolved())
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Solve the first two layers (F2L)
        /// </summary>
        /// <returns></returns>
        public static async Task<bool> SolveFirstTwoLayersAsync()
        {
            //string cT;
            int nLoopTimes = 0;

            while (true)
            {
                nLoopTimes++;
                if (nLoopTimes > nLoopTimesMax)
                {
                    Debug.WriteLine("CFOP_B: nLoopTimes first two layers: " + nLoopTimes);
                    return false;
                }

                // If solved, break the loop

                // Turn the cube
                if (nLoopTimes > 1)
                {
                    await MakeTurnAsync("y");
                }

                //--------------------------------------------------------------------------------------------------------------

                // Not used - Line no. 1287-1583 - Removed in version 2.0.34 on 2025-01-13
                // https://www.rubiksplace.com/speedcubing/F2L-algorithms/

                // Piece 4 = 3, 6, 7 and piece 13 = 14, 16, 17
                if (aPieces[4] == aPieces[3] && aPieces[4] == aPieces[6] && aPieces[4] == aPieces[7] && aPieces[13] == aPieces[14] && aPieces[13] == aPieces[16] && aPieces[13] == aPieces[17])
                {
                    // Corner on top, FL color facing side, edge colors match
                    // 1   U (R U' R') or  R' F R F'
                    if (aPieces[49] == aPieces[2] && aPieces[4] == aPieces[41] && aPieces[4] == aPieces[44] && aPieces[13] == aPieces[9] && aPieces[13] == aPieces[10])
                    {
                        await MakeTurnAsync("U R U' R'");
                        continue;
                    }

                    // 2   y' U' (R' U R) or F R' F' R
                    if (aPieces[49] == aPieces[9] && aPieces[4] == aPieces[1] && aPieces[4] == aPieces[2] && aPieces[13] == aPieces[43] && aPieces[13] == aPieces[44])
                    {
                        await MakeTurnAsync("F R' F' R");
                        continue;
                    }

                    // 3   U' R U R' U2 (R U' R')
                    if (aPieces[49] == aPieces[2] && aPieces[4] == aPieces[37] && aPieces[4] == aPieces[44] && aPieces[13] == aPieces[9])
                    {
                        await MakeTurnAsync("U' R U R' U2 R U' R'");
                        continue;
                    }

                    // 4   d R' U' R U2' (R' U R) or y' (U R' U' R) U2 (R' U R)
                    if (aPieces[49] == aPieces[9] && aPieces[4] == aPieces[2] && aPieces[13] == aPieces[39] && aPieces[13] == aPieces[44])
                    {
                        await MakeTurnAsync("d R' U' R U2 R' U R");
                        continue;
                    }

                    // 5   U' R U2' R' U2 (R U' R')
                    if (aPieces[49] == aPieces[2] && aPieces[4] == aPieces[39] && aPieces[4] == aPieces[44] && aPieces[13] == aPieces[9])
                    {
                        await MakeTurnAsync("U' R U2 R' U2 R U' R'");
                        continue;
                    }

                    // 6   d R' U2 R U2' (R' U R) or R' F R F'
                    if (aPieces[49] == aPieces[9] && aPieces[4] == aPieces[2] && aPieces[13] == aPieces[37] && aPieces[13] == aPieces[44])
                    {
                        await MakeTurnAsync("R' F R F'");
                        continue;
                    }

                    // 7   y' R' U R U' d' (R U R') or y L' U L U2 y (R U R')
                    if (aPieces[49] == aPieces[2] && aPieces[4] == aPieces[43] && aPieces[4] == aPieces[44] && aPieces[13] == aPieces[1] && aPieces[13] == aPieces[9])
                    {
                        await MakeTurnAsync("y L' U L U2 y R U R'");
                        continue;
                    }

                    // 8   R U' R' U d (R' U' R) or R U' R' U2 y' (R' U' R) or (R U' R') U2 (F' U' F)
                    if (aPieces[49] == aPieces[9] && aPieces[4] == aPieces[2] && aPieces[4] == aPieces[10] && aPieces[13] == aPieces[41] && aPieces[13] == aPieces[44])
                    {
                        await MakeTurnAsync("R U' R' U2 F' U' F");
                        continue;
                    }

                    // Corner on top, FL color facing side, edge colors opposite
                    // 9   y' (R' U' R)
                    if (aPieces[49] == aPieces[2] && aPieces[4] == aPieces[44] && aPieces[13] == aPieces[9] && aPieces[13] == aPieces[39])
                    {
                        await MakeTurnAsync("y' R' U' R");
                        continue;
                    }

                    // 10   (R U R')
                    if (aPieces[49] == aPieces[9] && aPieces[4] == aPieces[2] && aPieces[4] == aPieces[37] && aPieces[13] == aPieces[44])
                    {
                        await MakeTurnAsync("R U R'");
                        continue;
                    }

                    // 11   d R' U' R U' (R' U' R) or U' R U' R' d R' U' R or U' R U' R' U y' R' U' R
                    if (aPieces[49] == aPieces[2] && aPieces[4] == aPieces[44] && aPieces[13] == aPieces[9] && aPieces[13] == aPieces[37])
                    {
                        await MakeTurnAsync("d R' U' R U' R' U' R");
                        continue;
                    }

                    // 12   U' R U R' U (R U R')
                    if (aPieces[49] == aPieces[9] && aPieces[4] == aPieces[2] && aPieces[4] == aPieces[39] && aPieces[13] == aPieces[44])
                    {
                        await MakeTurnAsync("U' R U R' U R U R'");
                        continue;
                    }

                    // 13   U' R U2' R' d (R' U' R)
                    if (aPieces[49] == aPieces[2] && aPieces[4] == aPieces[10] && aPieces[4] == aPieces[44] && aPieces[13] == aPieces[9] && aPieces[13] == aPieces[41])
                    {
                        await MakeTurnAsync("U' R U2 R' d R' U' R");
                        continue;
                    }

                    // 14   R' U2 R2 U R2' U R or R U' R' U R U' R' U2 (R U' R') or d R' U2 R d' (R U R')	*Last R' U R can be avoided if back slot is empty.
                    if (aPieces[49] == aPieces[9] && aPieces[4] == aPieces[2] && aPieces[4] == aPieces[43] && aPieces[13] == aPieces[1] && aPieces[13] == aPieces[44])
                    {
                        await MakeTurnAsync("R' U2 R2 U R2 U R");
                        continue;
                    }

                    // 15   d R' U R U' (R' U' R)
                    if (aPieces[49] == aPieces[2] && aPieces[4] == aPieces[1] && aPieces[4] == aPieces[44] && aPieces[13] == aPieces[9] && aPieces[13] == aPieces[43])
                    {
                        await MakeTurnAsync("d R' U R U' R' U' R");
                        continue;
                    }

                    // 16   U' R U' R' U (R U R')
                    if (aPieces[49] == aPieces[9] && aPieces[4] == aPieces[2] && aPieces[4] == aPieces[41] && aPieces[13] == aPieces[10] && aPieces[13] == aPieces[44])
                    {
                        await MakeTurnAsync("U' R U' R' U R U R'");
                        continue;
                    }

                    // Corner on top, FL color facing up
                    // 17   R U2' R' U' (R U R')
                    if (aPieces[49] == aPieces[44] && aPieces[4] == aPieces[9] && aPieces[4] == aPieces[41] && aPieces[13] == aPieces[2] && aPieces[13] == aPieces[10])
                    {
                        await MakeTurnAsync("R U2 R' U' R U R'");
                        continue;
                    }

                    // 18   y' R' U2 R U (R' U' R) or y (L' U2 L) U (L' U' L)
                    if (aPieces[49] == aPieces[44] && aPieces[4] == aPieces[1] && aPieces[4] == aPieces[9] && aPieces[13] == aPieces[2] && aPieces[13] == aPieces[43])
                    {
                        await MakeTurnAsync("y' R' U2 R U R' U' R");
                        continue;
                    }

                    // 19   U R U2 R' U (R U' R') or U R U2 R2 F R F'
                    if (aPieces[49] == aPieces[44] && aPieces[4] == aPieces[9] && aPieces[4] == aPieces[37] && aPieces[13] == aPieces[2])
                    {
                        await MakeTurnAsync("U R U2 R2 F R F'");
                        continue;
                    }

                    // 20   y' U' R' U2 R U' (R' U R)
                    if (aPieces[49] == aPieces[44] && aPieces[4] == aPieces[9] && aPieces[13] == aPieces[2] && aPieces[13] == aPieces[39])
                    {
                        await MakeTurnAsync("y' U' R' U2 R U' R' U R");
                        continue;
                    }

                    // 21   U2 R U R' U (R U' R') or (R U' R') U2' (R U R')
                    if (aPieces[49] == aPieces[44] && aPieces[4] == aPieces[9] && aPieces[4] == aPieces[39] && aPieces[13] == aPieces[2])
                    {
                        await MakeTurnAsync("R U' R' U2 R U R'");
                        continue;
                    }

                    // 22   y' U2 R' U' R U' (R' U R) or y' R' U R U2 (R' U' R)
                    if (aPieces[49] == aPieces[44] && aPieces[4] == aPieces[9] && aPieces[13] == aPieces[2] && aPieces[13] == aPieces[37])
                    {
                        await MakeTurnAsync("y' R' U R U2 R' U' R");
                        continue;
                    }

                    // 23   y' U R' U2 R y R U2 R' U R U' R' or U2 R2 U2 R' U' R U' R2 or R U R' U2' R U R' U'(R U R')
                    if (aPieces[49] == aPieces[44] && aPieces[4] == aPieces[9] && aPieces[4] == aPieces[43] && aPieces[13] == aPieces[1] && aPieces[13] == aPieces[2])
                    {
                        await MakeTurnAsync("U2 R2 U2 R' U' R U' R2");
                        continue;
                    }

                    // 24   U' R U2' R' y' R' U2 R U' R' U R or R U R' d R' U R U' (R' U R) or y' U2 R2 U2 R U R' U R2
                    if (aPieces[49] == aPieces[44] && aPieces[4] == aPieces[9] && aPieces[4] == aPieces[10] && aPieces[13] == aPieces[2] && aPieces[13] == aPieces[41])
                    {
                        await MakeTurnAsync("y' U2 R2 U2 R U R' U R2");
                        continue;
                    }

                    // Corner down, edge on top
                    // 25   U R U' R' d' (L' U L) or U R U' R' U' y (L' U L)
                    if (aPieces[49] == aPieces[47] && aPieces[4] == aPieces[1] && aPieces[4] == aPieces[8] && aPieces[13] == aPieces[15] && aPieces[13] == aPieces[43])
                    {
                        await MakeTurnAsync("U R U' R' U' y L' U L");
                        continue;
                    }

                    // 26   y' U' R' U R r' U' R U M' or d' L' U L d(R U' R') or y U' (L' U L) y' U (R U' R')
                    if (aPieces[49] == aPieces[47] && aPieces[4] == aPieces[8] && aPieces[4] == aPieces[41] && aPieces[13] == aPieces[10] && aPieces[13] == aPieces[15])
                    {
                        await MakeTurnAsync("y U' L' U L y' U R U' R'");
                        continue;
                    }

                    // 27   y' R' U' R U (R' U' R)
                    if (aPieces[49] == aPieces[8] && aPieces[4] == aPieces[1] && aPieces[4] == aPieces[15] && aPieces[13] == aPieces[43])
                    {
                        await MakeTurnAsync("y' R' U' R U R' U' R");
                        continue;
                    }

                    // 28   R U R' U' (R U R')
                    if (aPieces[49] == aPieces[15] && aPieces[4] == aPieces[41] && aPieces[13] == aPieces[8] && aPieces[13] == aPieces[10])
                    {
                        await MakeTurnAsync("R U R' U' R U R'");
                        continue;
                    }

                    // 29   R U' R' U (R U' R')
                    if (aPieces[49] == aPieces[8] && aPieces[4] == aPieces[15] && aPieces[4] == aPieces[41] && aPieces[13] == aPieces[10])
                    {
                        await MakeTurnAsync("R U' R' U R U' R'");
                        continue;
                    }

                    // 30   y' R' U R U' (R' U R) or R U R' d (R' U2 R)
                    if (aPieces[49] == aPieces[15] && aPieces[4] == aPieces[1] && aPieces[13] == aPieces[8] && aPieces[13] == aPieces[43])
                    {
                        await MakeTurnAsync("R U R' d R' U2 R");
                        continue;
                    }

                    // Edge down, corner on top
                    // 31   U' R U' R' U2 (R U' R') or d' L' U' R' U L U' R
                    if (aPieces[49] == aPieces[2] && aPieces[4] == aPieces[5] && aPieces[4] == aPieces[44] && aPieces[13] == aPieces[9] && aPieces[13] == aPieces[12])
                    {
                        await MakeTurnAsync("U' R U' R' U2 R U' R'");
                        continue;
                    }

                    // 32   d R' U R U2 (R' U R) or U' (R U2' R') U (R U R') or U R U R' U2 (R U R')
                    if (aPieces[49] == aPieces[9] && aPieces[4] == aPieces[2] && aPieces[4] == aPieces[5] && aPieces[13] == aPieces[12] && aPieces[13] == aPieces[44])
                    {
                        await MakeTurnAsync("U R U R' U2 R U R'");
                        continue;
                    }

                    // 33   U' R U R' d (R' U' R)
                    if (aPieces[49] == aPieces[2] && aPieces[4] == aPieces[12] && aPieces[4] == aPieces[44] && aPieces[13] == aPieces[5] && aPieces[13] == aPieces[9])
                    {
                        await MakeTurnAsync("U' R U R' d R' U' R");
                        continue;
                    }

                    // 34   d R' U' R d' (R U R') or y U2 (L' U L) U y (L U L')
                    if (aPieces[49] == aPieces[9] && aPieces[4] == aPieces[2] && aPieces[4] == aPieces[12] && aPieces[13] == aPieces[5] && aPieces[13] == aPieces[44])
                    {
                        await MakeTurnAsync("d R' U' R d' R U R'");
                        continue;
                    }

                    // 35   R U' R' d (R' U R)
                    if (aPieces[49] == aPieces[44] && aPieces[4] == aPieces[9] && aPieces[4] == aPieces[12] && aPieces[13] == aPieces[2] && aPieces[13] == aPieces[5])
                    {
                        await MakeTurnAsync("R U' R' d R' U R");
                        continue;
                    }

                    // 36   [R U R' U'][R U R' U'](R U R') or U [R U' R' U][R U' R' U](R U' R')
                    if (aPieces[49] == aPieces[44] && aPieces[4] == aPieces[5] && aPieces[4] == aPieces[9] && aPieces[13] == aPieces[2] && aPieces[13] == aPieces[12])
                    {
                        await MakeTurnAsync("R U R' U' R U R' U' R U R'");
                        continue;
                    }

                    // Corner down, edge down
                    // 37   R U' R' U' R U R' U2 (R U' R') or y' R' U' R U2 R' U R U' (R' U' R)
                    if (aPieces[49] == aPieces[8] && aPieces[4] == aPieces[5] && aPieces[4] == aPieces[15] && aPieces[13] == aPieces[12] && aPieces[13] == aPieces[47])
                    {
                        await MakeTurnAsync("R U' R' U' R U R' U2 R U' R'");
                        continue;
                    }

                    // 38   R U R' U2 R U' R' U (R U R') or R U' R' U R U2' R' U (R U' R')
                    if (aPieces[49] == aPieces[15] && aPieces[4] == aPieces[5] && aPieces[4] == aPieces[47] && aPieces[13] == aPieces[8] && aPieces[13] == aPieces[12])
                    {
                        await MakeTurnAsync("R U R' U2 R U' R' U R U R'");
                        continue;
                    }

                    // 39   R U' R' d R' U' R U' (R' U' R)
                    if (aPieces[49] == aPieces[8] && aPieces[4] == aPieces[12] && aPieces[4] == aPieces[15] && aPieces[13] == aPieces[5] && aPieces[13] == aPieces[47])
                    {
                        await MakeTurnAsync("R U' R' d R' U' R U' R' U' R");
                        continue;
                    }

                    // 40   R U R' U' R U' R' U2 y' (R' U' R) or R U' R' U d R' U' R U' (R' U R)
                    if (aPieces[49] == aPieces[15] && aPieces[4] == aPieces[12] && aPieces[4] == aPieces[47] && aPieces[13] == aPieces[5] && aPieces[13] == aPieces[8])
                    {
                        await MakeTurnAsync("R U' R' U d R' U' R U' R' U R");
                        continue;
                    }

                    // 41   R U' R' U y' R' U2 R U2' (R' U R) or R U' R' d R' U2 R U2' (R' U R) or [R' F R F'][R U' R' U][R U' R' U2](R U' R')
                    if (aPieces[49] == aPieces[47] && aPieces[4] == aPieces[8] && aPieces[4] == aPieces[12] && aPieces[13] == aPieces[5] && aPieces[13] == aPieces[15])
                    {
                        await MakeTurnAsync("R U' R' d R' U2 R U2 R' U R");
                        continue;
                    }
                }

                //continue;

                //--------------------------------------------------------------------------------------------------------------

                // Not tested
                // https://www.myrubik.com/descarregues.php?lang=en

                // Piece 4 = 3, 6, 7 and piece 13 = 14, 16, 17
                if (aPieces[4] == aPieces[3] && aPieces[4] == aPieces[6] && aPieces[4] == aPieces[7] && aPieces[13] == aPieces[14] && aPieces[13] == aPieces[16] && aPieces[13] == aPieces[17])
                {
                    // 1.1
                    if (aPieces[49] == aPieces[2] && aPieces[4] == aPieces[41] && aPieces[4] == aPieces[44] && aPieces[13] == aPieces[9] && aPieces[13] == aPieces[10])
                    {
                        await MakeTurnAsync("U R U' R'");
                        continue;
                    }

                    // 1.2
                    if (aPieces[49] == aPieces[9] && aPieces[4] == aPieces[1] && aPieces[4] == aPieces[2] && aPieces[13] == aPieces[43] && aPieces[13] == aPieces[44])
                    {
                        await MakeTurnAsync("U' F' U F");
                        continue;
                    }

                    // 1.3
                    if (aPieces[49] == aPieces[2] && aPieces[4] == aPieces[44] && aPieces[13] == aPieces[9] && aPieces[13] == aPieces[39])
                    {
                        await MakeTurnAsync("F' U' F");
                        continue;
                    }

                    // 1.4
                    if (aPieces[49] == aPieces[9] && aPieces[4] == aPieces[2] && aPieces[4] == aPieces[37] && aPieces[13] == aPieces[44])
                    {
                        await MakeTurnAsync("R U R'");
                        continue;
                    }

                    // 1.5
                    if (aPieces[49] == aPieces[9] && aPieces[4] == aPieces[2] && aPieces[4] == aPieces[43] && aPieces[13] == aPieces[1] && aPieces[13] == aPieces[44])
                    {
                        await MakeTurnAsync("R U' R' U R U' R' U2");
                        continue;
                    }

                    // 1.6
                    if (aPieces[49] == aPieces[2] && aPieces[4] == aPieces[10] && aPieces[4] == aPieces[44] && aPieces[13] == aPieces[9] && aPieces[13] == aPieces[41])
                    {
                        await MakeTurnAsync("U' R' U2 R U F' U' F");
                        continue;
                    }

                    // 1.7
                    if (aPieces[49] == aPieces[9] && aPieces[4] == aPieces[2] && aPieces[4] == aPieces[10] && aPieces[13] == aPieces[41] && aPieces[13] == aPieces[44])
                    {
                        await MakeTurnAsync("R U' R' U2 F' U' F");
                        continue;
                    }

                    // 1.8
                    if (aPieces[49] == aPieces[2] && aPieces[4] == aPieces[43] && aPieces[4] == aPieces[44] && aPieces[13] == aPieces[1] && aPieces[13] == aPieces[9])
                    {
                        await MakeTurnAsync("F' U F U2 R U R'");
                        continue;
                    }

                    // 1.9
                    if (aPieces[49] == aPieces[9] && aPieces[4] == aPieces[2] && aPieces[4] == aPieces[41] && aPieces[13] == aPieces[10] && aPieces[13] == aPieces[44])
                    {
                        await MakeTurnAsync("U' R U' R' U R U R'");
                        continue;
                    }

                    // 1.10
                    if (aPieces[49] == aPieces[2] && aPieces[4] == aPieces[1] && aPieces[4] == aPieces[44] && aPieces[13] == aPieces[9] && aPieces[13] == aPieces[43])
                    {
                        await MakeTurnAsync("d R' U R U' R' U' R");
                        continue;
                    }

                    // 1.11
                    if (aPieces[49] == aPieces[9] && aPieces[4] == aPieces[2] && aPieces[13] == aPieces[39] && aPieces[13] == aPieces[44])
                    {
                        await MakeTurnAsync("U F' U' F U2 F' U F");
                        continue;
                    }

                    // 1.12
                    if (aPieces[49] == aPieces[2] && aPieces[4] == aPieces[37] && aPieces[4] == aPieces[44] && aPieces[13] == aPieces[9])
                    {
                        await MakeTurnAsync("U' R U R' U' R U2 R'");
                        continue;
                    }

                    // 1.13
                    if (aPieces[49] == aPieces[9] && aPieces[4] == aPieces[2] && aPieces[4] == aPieces[39] && aPieces[13] == aPieces[44])
                    {
                        await MakeTurnAsync("U' R U R' U R U R'");
                        continue;
                    }

                    // 1.14
                    if (aPieces[49] == aPieces[2] && aPieces[4] == aPieces[44] && aPieces[13] == aPieces[9] && aPieces[13] == aPieces[37])
                    {
                        await MakeTurnAsync("U' R U' R' U F' U' F");
                        continue;
                    }

                    // 1.15
                    if (aPieces[49] == aPieces[9] && aPieces[4] == aPieces[2] && aPieces[13] == aPieces[37] && aPieces[13] == aPieces[44])
                    {
                        await MakeTurnAsync("U' F' U2 F U2 F' U F");
                        continue;
                    }

                    // 1.16
                    if (aPieces[49] == aPieces[2] && aPieces[4] == aPieces[39] && aPieces[4] == aPieces[44] && aPieces[13] == aPieces[9])
                    {
                        await MakeTurnAsync("U' R U2 R' U2 R U' R'");
                        continue;
                    }

                    // 1.17
                    if (aPieces[49] == aPieces[44] && aPieces[4] == aPieces[9] && aPieces[4] == aPieces[43] && aPieces[13] == aPieces[1] && aPieces[13] == aPieces[2])
                    {
                        await MakeTurnAsync("R U R' U2 R U R' U' R U R'");
                        continue;
                    }

                    // 1.18
                    if (aPieces[49] == aPieces[44] && aPieces[4] == aPieces[9] && aPieces[4] == aPieces[10] && aPieces[13] == aPieces[2] && aPieces[13] == aPieces[41])
                    {
                        await MakeTurnAsync("R U R' U R U2 R' F' U2 F");
                        continue;
                    }

                    // 1.19
                    if (aPieces[49] == aPieces[44] && aPieces[4] == aPieces[1] && aPieces[4] == aPieces[9] && aPieces[13] == aPieces[2] && aPieces[13] == aPieces[43])
                    {
                        await MakeTurnAsync("F' U2 F U F' U' F");
                        continue;
                    }

                    // 1.20
                    if (aPieces[49] == aPieces[44] && aPieces[4] == aPieces[9] && aPieces[4] == aPieces[41] && aPieces[13] == aPieces[2] && aPieces[13] == aPieces[10])
                    {
                        await MakeTurnAsync("R U2 R' U' R U R'");
                        continue;
                    }

                    // 1.21
                    if (aPieces[49] == aPieces[44] && aPieces[4] == aPieces[9] && aPieces[13] == aPieces[2] && aPieces[13] == aPieces[39])
                    {
                        await MakeTurnAsync("U' F' U2 F U' F' U F");
                        continue;
                    }

                    // 1.22
                    if (aPieces[49] == aPieces[44] && aPieces[4] == aPieces[9] && aPieces[4] == aPieces[37] && aPieces[13] == aPieces[2])
                    {
                        await MakeTurnAsync("U R U2 R' U R U' R'");
                        continue;
                    }

                    // 1.23
                    if (aPieces[49] == aPieces[44] && aPieces[4] == aPieces[9] && aPieces[4] == aPieces[39] && aPieces[13] == aPieces[2])
                    {
                        await MakeTurnAsync("U2 R U R' U R U' R'");
                        continue;
                    }

                    // 1.24
                    if (aPieces[49] == aPieces[44] && aPieces[4] == aPieces[9] && aPieces[13] == aPieces[2] && aPieces[13] == aPieces[37])
                    {
                        await MakeTurnAsync("U2 F' U' F U' F' U F");
                        continue;
                    }

                    // 2.1
                    if (aPieces[4] == aPieces[1] && aPieces[4] == aPieces[8] && aPieces[13] == aPieces[15] && aPieces[13] == aPieces[43])
                    {
                        await MakeTurnAsync("U R U' R' U' F' U F");
                        continue;
                    }

                    // 2.2
                    if (aPieces[4] == aPieces[8] && aPieces[4] == aPieces[41] && aPieces[13] == aPieces[10] && aPieces[13] == aPieces[15])
                    {
                        await MakeTurnAsync("U' F' U F U R U' R'");
                        continue;
                    }

                    // 2.3
                    if (aPieces[49] == aPieces[15] && aPieces[4] == aPieces[1] && aPieces[13] == aPieces[8] && aPieces[13] == aPieces[43])
                    {
                        await MakeTurnAsync("F' U F U' F' U F");
                        continue;
                    }

                    // 2.4
                    if (aPieces[49] == aPieces[8] && aPieces[4] == aPieces[15] && aPieces[4] == aPieces[41] && aPieces[13] == aPieces[10])
                    {
                        await MakeTurnAsync("R U' R' U R U' R'");
                        continue;
                    }

                    // 2.5
                    if (aPieces[49] == aPieces[15] && aPieces[4] == aPieces[43] && aPieces[13] == aPieces[1] && aPieces[13] == aPieces[8])
                    {
                        await MakeTurnAsync("U' R U R' U' R U R'");
                        continue;
                    }

                    // 2.6
                    if (aPieces[49] == aPieces[8] && aPieces[4] == aPieces[10] && aPieces[4] == aPieces[15] && aPieces[13] == aPieces[41])
                    {
                        await MakeTurnAsync("U' R U' R' F' U' F");
                        continue;
                    }

                    // 2.7
                    if (aPieces[49] == aPieces[44] && aPieces[4] == aPieces[5] && aPieces[4] == aPieces[9] && aPieces[13] == aPieces[2] && aPieces[13] == aPieces[12])
                    {
                        await MakeTurnAsync("R U R' U' R U R' U' R U R'");
                        continue;
                    }

                    // 2.8
                    if (aPieces[49] == aPieces[44] && aPieces[4] == aPieces[9] && aPieces[4] == aPieces[12] && aPieces[13] == aPieces[2] && aPieces[13] == aPieces[5])
                    {
                        await MakeTurnAsync("R U' R' F' U2 F");
                        continue;
                    }

                    // 2.9
                    if (aPieces[49] == aPieces[9] && aPieces[4] == aPieces[2] && aPieces[4] == aPieces[5] && aPieces[13] == aPieces[12] && aPieces[13] == aPieces[44])
                    {
                        await MakeTurnAsync("U' R U2 R' U R U R'");
                        continue;
                    }

                    // 2.10
                    if (aPieces[49] == aPieces[2] && aPieces[4] == aPieces[5] && aPieces[4] == aPieces[44] && aPieces[13] == aPieces[9] && aPieces[13] == aPieces[12])
                    {
                        await MakeTurnAsync("U' R U' R' U2 R U' R'");
                        continue;
                    }

                    // 2.11
                    if (aPieces[49] == aPieces[9] && aPieces[4] == aPieces[2] && aPieces[4] == aPieces[12] && aPieces[13] == aPieces[5] && aPieces[13] == aPieces[44])
                    {
                        await MakeTurnAsync("d R' U' R d' R U R'");
                        continue;
                    }

                    // 2.12
                    if (aPieces[49] == aPieces[2] && aPieces[4] == aPieces[12] && aPieces[4] == aPieces[44] && aPieces[13] == aPieces[5] && aPieces[13] == aPieces[9])
                    {
                        await MakeTurnAsync("U' R U R' d R' U' R");
                        continue;
                    }

                    // 2.13

                    // 2.14
                    if (aPieces[4] == aPieces[8] && aPieces[4] == aPieces[12] && (aPieces[13] == aPieces[5] && aPieces[13] == aPieces[15]))
                    {
                        await MakeTurnAsync("R U2 R' U R U2 R' U F' U' F");
                        continue;
                    }

                    // 2.15
                    if (aPieces[49] == aPieces[15] && aPieces[4] == aPieces[5] && aPieces[13] == aPieces[8] && aPieces[13] == aPieces[12])
                    {
                        await MakeTurnAsync("R U' R' U R U2 R' U R U' R'");
                        continue;
                    }

                    // 2.16
                    if (aPieces[49] == aPieces[8] && aPieces[4] == aPieces[5] && aPieces[4] == aPieces[15] && aPieces[13] == aPieces[12])
                    {
                        await MakeTurnAsync("R U R' U' R U2 R' U' R U R'");
                        continue;
                    }

                    // 2.17
                    if (aPieces[49] == aPieces[15] && aPieces[4] == aPieces[12] && aPieces[13] == aPieces[5] && aPieces[13] == aPieces[8])
                    {
                        await MakeTurnAsync("R U' R' U R' U' R F' U F");
                        continue;
                    }

                    // 2.18
                    if (aPieces[49] == aPieces[8] && aPieces[4] == aPieces[12] && aPieces[4] == aPieces[15] && aPieces[13] == aPieces[5])
                    {
                        await MakeTurnAsync("F' U F U2 R U R' U R U' R'");
                        continue;
                    }
                }

                //--------------------------------------------------------------------------------------------------------------

                // Not tested
                // file://C:/Sources/MAUI/CubeSolver/Miscellaneous/Manuals/CubeSkills/f2l-algorithms-different-slot-positions.pdf

                // CubeSkills
                // F2L Algorithms – All Four Slot Angles - Developed by Feliks Zemdegs and Andy Klise

                // Piece 4 = 3, 6, 7 and piece 13 = 14, 16, 17
                if (aPieces[4] == aPieces[3] && aPieces[4] == aPieces[6] && aPieces[4] == aPieces[7] && aPieces[13] == aPieces[14] && aPieces[13] == aPieces[16] && aPieces[13] == aPieces[17])
                {
                    // Page 1. F2L Algorithms
                    // Basic Inserts
                    // 1.  U (R U' R')
                    if (aPieces[49] == aPieces[2] && aPieces[4] == aPieces[41] && aPieces[4] == aPieces[44] && aPieces[13] == aPieces[9] && aPieces[13] == aPieces[10])
                    {
                        await MakeTurnAsync("U R U' R'");
                        continue;
                    }

                    // 2.  y' (R' U' R) --- y (L' U' L)
                    if (aPieces[49] == aPieces[2] && aPieces[4] == aPieces[44] && aPieces[13] == aPieces[9] && aPieces[13] == aPieces[39])
                    {
                        await MakeTurnAsync("y' R' U' R");
                        continue;
                    }

                    // 3.  y' U' (R' U R) --- y U' (L' U L) 
                    if (aPieces[49] == aPieces[9] && aPieces[4] == aPieces[1] && aPieces[4] == aPieces[2] && aPieces[13] == aPieces[43] && aPieces[13] == aPieces[44])
                    {
                        await MakeTurnAsync("y' U' R' U R");
                        continue;
                    }

                    // 4.  (R U R')
                    if (aPieces[49] == aPieces[9] && aPieces[4] == aPieces[2] && aPieces[4] == aPieces[37] && aPieces[13] == aPieces[44])
                    {
                        await MakeTurnAsync("R U R'");
                        continue;
                    }

                    // F2L Case 1
                    // 1.  U' (R U' R' U) y' (R' U' R) --- y' U (R' U' R U') (R' U' R) 
                    if (aPieces[49] == aPieces[2] && aPieces[4] == aPieces[44] && aPieces[13] == aPieces[9] && aPieces[13] == aPieces[37])
                    {
                        await MakeTurnAsync("U' R U' R' U y' R' U' R");
                        continue;
                    }

                    // 2.  U' (R U2' R' U) y' (R' U' R) --- U' (R U2' R') d (R' U' R) 
                    if (aPieces[49] == aPieces[2] && aPieces[4] == aPieces[10] && aPieces[4] == aPieces[44] && aPieces[13] == aPieces[9] && aPieces[13] == aPieces[41])
                    {
                        await MakeTurnAsync("U' R U2 R' U y' R' U' R");
                        continue;
                    }

                    // 3.  y' U (R' U R U') (R' U' R)
                    if (aPieces[49] == aPieces[2] && aPieces[4] == aPieces[1] && aPieces[4] == aPieces[44] && aPieces[13] == aPieces[9] && aPieces[13] == aPieces[43])
                    {
                        await MakeTurnAsync("y' U R' U R U' R' U' R");
                        continue;
                    }

                    // 4.  U' (R U R' U) (R U R')
                    if (aPieces[49] == aPieces[9] && aPieces[4] == aPieces[2] && aPieces[4] == aPieces[39] && aPieces[13] == aPieces[44])
                    {
                        await MakeTurnAsync("U' R U R' U R U R'");
                        continue;
                    }

                    // 5.  R' U2' R2 U R2' U R --- y' U (R' U2 R) U' y (R U R') --- (R U' R' U) (R U' R') U2 (R U' R')
                    if (aPieces[49] == aPieces[9] && aPieces[4] == aPieces[2] && aPieces[4] == aPieces[43] && aPieces[13] == aPieces[1] && aPieces[13] == aPieces[44])
                    {
                        await MakeTurnAsync("R' U2 R2 U R2 U R");
                        continue;
                    }

                    // 6.  U' (R U' R' U) (R U R')
                    if (aPieces[49] == aPieces[9] && aPieces[4] == aPieces[2] && aPieces[4] == aPieces[41] && aPieces[13] == aPieces[10] && aPieces[13] == aPieces[44])
                    {
                        await MakeTurnAsync("U' R U' R' U R U R'");
                        continue;
                    }

                    // F2L Case 2
                    // 1.  (U' R U R') U2 (R U' R')
                    if (aPieces[49] == aPieces[2] && aPieces[4] == aPieces[37] && aPieces[4] == aPieces[44] && aPieces[13] == aPieces[9])
                    {
                        await MakeTurnAsync("U' R U R' U2 R U' R'");
                        continue;
                    }

                    // 2.  U' (R U2' R') U2 (R U' R')
                    if (aPieces[49] == aPieces[2] && aPieces[4] == aPieces[39] && aPieces[4] == aPieces[44] && aPieces[13] == aPieces[9])
                    {
                        await MakeTurnAsync("U' R U2 R' U2 R U' R'");
                        continue;
                    }

                    // 3.  y' (U R' U' R) U2' (R' U R) --- d (R' U' R) U2' (R' U R) --- Note: (y' U) and (d) are interchangeable
                    if (aPieces[49] == aPieces[9] && aPieces[4] == aPieces[2] && aPieces[13] == aPieces[39] && aPieces[13] == aPieces[44])
                    {
                        await MakeTurnAsync("y' U R' U' R U2 R' U R");
                        continue;
                    }

                    // 4.  y' U (R' U2 R) U2' (R' U R) --- d (R' U2 R) U2' (R' U R)
                    if (aPieces[49] == aPieces[9] && aPieces[4] == aPieces[2] && aPieces[13] == aPieces[37] && aPieces[13] == aPieces[44])
                    {
                        await MakeTurnAsync("y' U R' U2 R U2 R' U R");
                        continue;
                    }

                    // F2L Case 3
                    // 1.  U (R U2' R') U (R U' R')
                    if (aPieces[49] == aPieces[44] && aPieces[4] == aPieces[9] && aPieces[4] == aPieces[37] && aPieces[13] == aPieces[2])
                    {
                        await MakeTurnAsync("U R U2 R' U R U' R'");
                        continue;
                    }

                    // 2.  U2 (R U R' U) (R U' R') --- (R U' R') U2 (R U R')
                    if (aPieces[49] == aPieces[44] && aPieces[4] == aPieces[9] && aPieces[4] == aPieces[39] && aPieces[13] == aPieces[2])
                    {
                        await MakeTurnAsync("U2 R U R' U R U' R'");
                        continue;
                    }

                    // 3.  y' U' (R' U2 R) U' (R' U R)
                    if (aPieces[49] == aPieces[44] && aPieces[4] == aPieces[9] && aPieces[13] == aPieces[2] && aPieces[13] == aPieces[39])
                    {
                        await MakeTurnAsync("y' U' R' U2 R U' R' U R");
                        continue;
                    }

                    // 4.  y' U2 (R' U' R) U' (R' U R) --- F' L' U2 L F --- Note: The second algorithm is fewer moves, but less intuitive and less finger friendly
                    if (aPieces[49] == aPieces[44] && aPieces[4] == aPieces[9] && aPieces[13] == aPieces[2] && aPieces[13] == aPieces[37])
                    {
                        await MakeTurnAsync("F' L' U2 L F");
                        continue;
                    }


                    // Page 2. F2L Algorithms
                    // Incorrectly Connected Pieces
                    // 1.  y' (R' U R) U2' y (R U R') --- (R U R') U2 (R U' R' U) (R U' R') 
                    if (aPieces[49] == aPieces[2] && aPieces[4] == aPieces[43] && aPieces[4] == aPieces[44] && aPieces[13] == aPieces[1] && aPieces[13] == aPieces[9])
                    {
                        await MakeTurnAsync("y' R' U R U2 y R U R'");
                        continue;
                    }

                    // 2.  (R U2 R') U' (R U R')
                    if (aPieces[49] == aPieces[44] && aPieces[4] == aPieces[9] && aPieces[4] == aPieces[41] && aPieces[13] == aPieces[2] && aPieces[13] == aPieces[10])
                    {
                        await MakeTurnAsync("R U2 R' U' R U R'");
                        continue;
                    }

                    // 3.  U (R U' R' U') (R U' R' U) (R U' R') --- (R U R' U2') (R U R' U') (R U R')
                    if (aPieces[49] == aPieces[44] && aPieces[4] == aPieces[9] && aPieces[4] == aPieces[43] && aPieces[13] == aPieces[1] && aPieces[13] == aPieces[2])
                    {
                        await MakeTurnAsync("R U R' U2 R U R' U' R U R'");
                        continue;
                    }

                    // 4.  (R U' R' U2) y' (R' U' R) --- U F(R U R' U') F' (U R U' R')
                    if (aPieces[49] == aPieces[9] && aPieces[4] == aPieces[2] && aPieces[4] == aPieces[10] && aPieces[13] == aPieces[41] && aPieces[13] == aPieces[44])
                    {
                        await MakeTurnAsync("R U' R' U2 y' R' U' R");
                        continue;
                    }

                    // 5.  y' (R' U2 R) U (R' U' R)
                    if (aPieces[49] == aPieces[44] && aPieces[4] == aPieces[1] && aPieces[4] == aPieces[9] && aPieces[13] == aPieces[2] && aPieces[13] == aPieces[43])
                    {
                        await MakeTurnAsync("y' R' U2 R U R' U' R");
                        continue;
                    }

                    // 6.  y' U' (R' U R U) (R' U R U') (R' U R) --- F (U R U' R') F' (R U' R')
                    if (aPieces[49] == aPieces[44] && aPieces[4] == aPieces[9] && aPieces[4] == aPieces[10] && aPieces[13] == aPieces[2] && aPieces[13] == aPieces[41])
                    {
                        await MakeTurnAsync("F U R U' R' F' R U' R'");
                        continue;
                    }

                    // Corner in Place, Edge in U Face
                    // 1.  U' F' (R U R' U') R' F R --- R' F' R U (R U' R') F
                    if (aPieces[49] == aPieces[47] && aPieces[4] == aPieces[8] && aPieces[4] == aPieces[41] && aPieces[13] == aPieces[10] && aPieces[13] == aPieces[15])
                    {
                        await MakeTurnAsync("R' F' R U R U' R' F");
                        continue;
                    }

                    // 2.  (R U' R' U) (R U' R')
                    if (aPieces[49] == aPieces[8] && aPieces[4] == aPieces[15] && aPieces[4] == aPieces[41] && aPieces[13] == aPieces[10])
                    {
                        await MakeTurnAsync("R U' R' U R U' R'");
                        continue;
                    }

                    // 3.  y' (R' U' R U) (R' U' R) --- (R' F R F') U(R U' R')
                    if (aPieces[49] == aPieces[8] && aPieces[4] == aPieces[1] && aPieces[4] == aPieces[15] && aPieces[13] == aPieces[43])
                    {
                        await MakeTurnAsync("y' R' U' R U R' U' R");
                        continue;
                    }

                    // 4.  U (R U' R') U' (F' U F) --- U(R U' R')(F R' F' R)
                    if (aPieces[49] == aPieces[47] && aPieces[4] == aPieces[1] && aPieces[4] == aPieces[8] && aPieces[13] == aPieces[15] && aPieces[13] == aPieces[43])
                    {
                        await MakeTurnAsync("U R U' R' U' F' U F");
                        continue;
                    }

                    // 5.  y' (R' U R U') (R' U R)
                    if (aPieces[49] == aPieces[15] && aPieces[4] == aPieces[1] && aPieces[13] == aPieces[8] && aPieces[13] == aPieces[43])
                    {
                        await MakeTurnAsync("y' R' U R U' R' U R");
                        continue;
                    }

                    // 6.  (R U R' U') (R U R')
                    if (aPieces[49] == aPieces[15] && aPieces[4] == aPieces[41] && aPieces[13] == aPieces[8] && aPieces[13] == aPieces[41])
                    {
                        await MakeTurnAsync("R U R' U' R U R'");
                        continue;
                    }

                    // Edge in Place, Corner in U face
                    // 1.  (R U' R' U) y' (R' U R) --- U' (R' F R F') (R U' R')
                    if (aPieces[49] == aPieces[44] && aPieces[4] == aPieces[9] && aPieces[4] == aPieces[12] && aPieces[13] == aPieces[2] && aPieces[13] == aPieces[5])
                    {
                        await MakeTurnAsync("R U' R' U y' R' U R");
                        continue;
                    }

                    // 2.  (U' R U' R') U2 (R U' R')
                    if (aPieces[49] == aPieces[2] && aPieces[4] == aPieces[5] && aPieces[4] == aPieces[44] && aPieces[13] == aPieces[9] && aPieces[13] == aPieces[12])
                    {
                        await MakeTurnAsync("U' R U' R' U2 R U' R'");
                        continue;
                    }

                    // 3.  (U' R U R') d (R' U' R)
                    if (aPieces[49] == aPieces[2] && aPieces[4] == aPieces[12] && aPieces[4] == aPieces[44] && aPieces[13] == aPieces[5] && aPieces[13] == aPieces[9])
                    {
                        await MakeTurnAsync("U' R U R' d R' U' R");
                        continue;
                    }

                    // 4.  (U R U' R') (U R U' R') (U R U' R')
                    if (aPieces[49] == aPieces[44] && aPieces[4] == aPieces[5] && aPieces[4] == aPieces[9] && aPieces[13] == aPieces[2] && aPieces[13] == aPieces[12])
                    {
                        await MakeTurnAsync("U R U' R' U R U' R' U R U' R'");
                        continue;
                    }

                    // 5.  U (R U R') U2 (R U R')
                    if (aPieces[49] == aPieces[9] && aPieces[4] == aPieces[2] && aPieces[4] == aPieces[5] && aPieces[13] == aPieces[12] && aPieces[13] == aPieces[44])
                    {
                        await MakeTurnAsync("U R U R' U2 R U R'");
                        continue;
                    }

                    // 6.  U (F' U' F) U' (R U R')
                    if (aPieces[49] == aPieces[9] && aPieces[4] == aPieces[2] && aPieces[4] == aPieces[12] && aPieces[13] == aPieces[5] && aPieces[13] == aPieces[44])
                    {
                        await MakeTurnAsync("U F' U' F U' R U R'");
                        continue;
                    }

                    // Edge and Corner in Place
                    // 1.  Solved Pair

                    // 2.  (R U' R' U') R U R' U2 (R U' R') --- (R U R' U') R U2 R' U'(R U R')
                    if (aPieces[49] == aPieces[8] && aPieces[4] == aPieces[5] && aPieces[4] == aPieces[15] && aPieces[13] == aPieces[12])
                    {
                        await MakeTurnAsync("R U' R' U' R U R' U2 R U' R'");
                        continue;
                    }

                    // 3.  (F' U F) U2 (R U R' U) (R U' R') --- (R U' R') F (R U R' U') F' (R U' R')
                    if (aPieces[49] == aPieces[8] && aPieces[4] == aPieces[12] && aPieces[4] == aPieces[15] && aPieces[13] == aPieces[5])
                    {
                        await MakeTurnAsync("F' U F U2 R U R' U R U' R'");
                        continue;
                    }

                    // 4.  (R U' R') d (R' U2 R) U2' (R' U R)
                    if (aPieces[49] == aPieces[47] && aPieces[4] == aPieces[8] && aPieces[4] == aPieces[12] && aPieces[13] == aPieces[5] && aPieces[13] == aPieces[15])
                    {
                        await MakeTurnAsync("R U' R' d R' U2 R U2 R' U R");
                        continue;
                    }

                    // 5.  (R U' R' U) (R U2' R') U (R U' R') --- (R U R') U2'(R U' R' U)(R U R')
                    if (aPieces[49] == aPieces[15] && aPieces[4] == aPieces[5] && aPieces[13] == aPieces[8] && aPieces[13] == aPieces[12])
                    {
                        await MakeTurnAsync("R U' R' U R U2 R' U R U' R'");
                        continue;
                    }

                    // 6.  (R U R' U') (R U' R') U2 y' (R' U' R)
                    if (aPieces[49] == aPieces[15] && aPieces[4] == aPieces[12] && aPieces[13] == aPieces[5] && aPieces[13] == aPieces[8])
                    {
                        await MakeTurnAsync("R U R' U' R U' R' U2 y' R' U' R");
                        continue;
                    }

                    // Page 3. Algorithms for slot in back-right position
                    // Algorithms for slot in back - right position
                    // Piece 4 = 3, 5, 6, 7, 8 and piece 13 = 12, 15, 16
                    if (aPieces[4] == aPieces[3] && aPieces[4] == aPieces[5] && aPieces[4] == aPieces[6] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[8] && aPieces[13] == aPieces[12] && aPieces[13] == aPieces[15] && aPieces[13] == aPieces[16])
                    {
                        // Basic Inserts
                        // 1.  y U (R U' R') --- y' U (L U' L') 
                        if (aPieces[49] == aPieces[11] && aPieces[13] == aPieces[37] && aPieces[13] == aPieces[38])
                        {
                            await MakeTurnAsync("y U R U' R'");
                            continue;
                        }

                        // 2.  (R' U' R)
                        if (aPieces[49] == aPieces[11] && aPieces[13] == aPieces[1] && aPieces[13] == aPieces[38] && aPieces[22] == aPieces[43])
                        {
                            await MakeTurnAsync("R' U' R");
                            continue;
                        }

                        // 3.  U' (R' U R)
                        if (aPieces[49] == aPieces[18] && aPieces[13] == aPieces[10] && aPieces[13] == aPieces[11] && aPieces[22] == aPieces[38] && aPieces[22] == aPieces[41])
                        {
                            await MakeTurnAsync("U' R' U R");
                            continue;
                        }

                        // 4.  y (R U R') --- y' (L U L') 
                        if (aPieces[49] == aPieces[18] && aPieces[13] == aPieces[11] && aPieces[13] == aPieces[39] && aPieces[22] == aPieces[38])
                        {
                            await MakeTurnAsync("y R U R'");
                            continue;
                        }

                        // F2L Case 1
                        // 1.  U (R' U' R U') (R' U' R)
                        if (aPieces[49] == aPieces[11] && aPieces[13] == aPieces[38] && aPieces[22] == aPieces[39])
                        {
                            await MakeTurnAsync("U R' U' R U' R' U' R");
                            continue;
                        }

                        // 2.  R U2' R2' U' (R2 U' R')
                        if (aPieces[49] == aPieces[11] && aPieces[13] == aPieces[38] && aPieces[22] == aPieces[37])
                        {
                            await MakeTurnAsync("R U2 R2 U' R2 U' R'");
                            continue;
                        }

                        // 3.  U (R' U R U') (R' U' R)
                        if (aPieces[49] == aPieces[11] && aPieces[13] == aPieces[38] && aPieces[22] == aPieces[41])
                        {
                            await MakeTurnAsync("U R' U R U' R' U' R");
                            continue;
                        }

                        // 4.  U (R' U R U') y (R U R')
                        if (aPieces[49] == aPieces[18] && aPieces[13] == aPieces[11] && aPieces[13] == aPieces[43] && aPieces[22] == aPieces[1] && aPieces[22] == aPieces[38])
                        {
                            await MakeTurnAsync("U R' U R U' y R U R'");
                            continue;
                        }

                        // 5.  U (R' U2 R) U' y (R U R')
                        if (aPieces[49] == aPieces[18] && aPieces[13] == aPieces[11] && aPieces[13] == aPieces[41] && aPieces[22] == aPieces[10] && aPieces[22] == aPieces[38])
                        {
                            await MakeTurnAsync("U R' U2 R U' y R U R'");
                            continue;
                        }

                        // 6.  y U' (R U' R' U) (R U R')
                        if (aPieces[49] == aPieces[18] && aPieces[13] == aPieces[11] && aPieces[13] == aPieces[37] && aPieces[22] == aPieces[38])
                        {
                            await MakeTurnAsync("y U' R U' R' U R U R'");
                            continue;
                        }

                        // F2L Case 2
                        // 1.  y (U' R U R') U2 (R U' R') --- U r' (U R U' R') U' r
                        if (aPieces[49] == aPieces[11] && aPieces[13] == aPieces[38] && aPieces[13] == aPieces[39])
                        {
                            await MakeTurnAsync("y U' R U R' U2 R U' R'");
                            continue;
                        }

                        // 2.  y U' (R U2' R') U2 (R U' R')
                        if (aPieces[49] == aPieces[11] && aPieces[13] == aPieces[38] && aPieces[13] == aPieces[43] && aPieces[22] == aPieces[1])
                        {
                            await MakeTurnAsync("y U' R U2 R' U2 R U' R'");
                            continue;
                        }

                        // 3.  (U R' U' R) U2' (R' U R)
                        if (aPieces[49] == aPieces[18] && aPieces[13] == aPieces[1] && aPieces[13] == aPieces[11] && aPieces[22] == aPieces[38] && aPieces[22] == aPieces[43])
                        {
                            await MakeTurnAsync("U R' U' R U2 R' U R");
                            continue;
                        }

                        // 4.  U (R' U2 R) U2' (R' U R)
                        if (aPieces[49] == aPieces[18] && aPieces[13] == aPieces[11] && aPieces[22] == aPieces[38] && aPieces[22] == aPieces[39])
                        {
                            await MakeTurnAsync("U R' U2 R U2 R' U R");
                            continue;
                        }

                        // F2L Case 3
                        // 1.  y U (R U2' R') U (R U' R')
                        if (aPieces[49] == aPieces[38] && aPieces[13] == aPieces[39] && aPieces[22] == aPieces[11])
                        {
                            await MakeTurnAsync("y U R U2 R' U R U' R'");
                            continue;
                        }

                        // 2.  y U2 (R U R' U) (R U' R')
                        if (aPieces[49] == aPieces[38] && aPieces[13] == aPieces[43] && aPieces[22] == aPieces[1] && aPieces[22] == aPieces[11])
                        {
                            await MakeTurnAsync("y U2 R U R' U R U' R'");
                            continue;
                        }

                        // 3.  U' (R' U2 R) U' (R' U R)
                        if (aPieces[49] == aPieces[38] && aPieces[13] == aPieces[1] && aPieces[22] == aPieces[11] && aPieces[22] == aPieces[43])
                        {
                            await MakeTurnAsync("U' R' U2 R U' R' U R");
                            continue;
                        }

                        // 4.  U2 (R' U' R) U' (R' U R) --- R' F' U2 F R --- Note: The second algorithm is fewer moves, but less intuitive and less finger-friendly.
                        if (aPieces[49] == aPieces[38] && aPieces[22] == aPieces[11] && aPieces[22] == aPieces[39])
                        {
                            await MakeTurnAsync("R' F' U2 F R");
                            continue;
                        }

                        // Page 4. Algorithms for slot in back-right position
                        // Incorrectly Connected Pieces
                        // 1.  (R' U R) U2' y (R U R') --- (R2' F R F' R) U2' (R' U R)
                        if (aPieces[49] == aPieces[11] && aPieces[13] == aPieces[38] && aPieces[13] == aPieces[41] && aPieces[22] == aPieces[10])
                        {
                            await MakeTurnAsync("R' U R U2 y R U R'");
                            continue;
                        }

                        // 2.  y (R U2 R') U' (R U R')
                        if (aPieces[49] == aPieces[38] && aPieces[13] == aPieces[37] && aPieces[22] == aPieces[11])
                        {
                            await MakeTurnAsync("y R U2 R' U' R U R'");
                            continue;
                        }

                        // 3.  (R' U' R U' y) (R U' R' U) (R U' R') --- (U R' U2' R) y (R U2' R' U) (R U' R')
                        if (aPieces[49] == aPieces[38] && aPieces[13] == aPieces[41] && aPieces[22] == aPieces[10] && aPieces[22] == aPieces[11])
                        {
                            await MakeTurnAsync("R' U' R U' y R U' R' U R U' R'");
                            continue;
                        }

                        // 4.  y (R U' R' U2) y' (R' U' R) --- U (R U' R' U) (R' U' R) --- Note: the second algorithm should only be used when the front-right slot is empty.
                        if (aPieces[49] == aPieces[18] && aPieces[13] == aPieces[11] && aPieces[22] == aPieces[37] && aPieces[22] == aPieces[38])
                        {
                            await MakeTurnAsync("y R U' R' U2 y' R' U' R");
                            continue;
                        }

                        // 5.  (R' U2' R) U (R' U' R)
                        if (aPieces[49] == aPieces[38] && aPieces[13] == aPieces[10] && aPieces[22] == aPieces[11] && aPieces[22] == aPieces[41])
                        {
                            await MakeTurnAsync("R' U2 R U R' U' R");
                            continue;
                        }

                        // 6.  U' (R' U R U) (R' U R U') (R' U R) --- y F (U R U' R') F' (R U' R')
                        if (aPieces[49] == aPieces[38] && aPieces[22] == aPieces[11] && aPieces[22] == aPieces[37])
                        {
                            await MakeTurnAsync("y F U R U' R' F' R U' R'");
                            continue;
                        }

                        // Corner in Place, Edge in U Face
                        // 1.  (U' R' U R) y U (R U' R')
                        if (aPieces[49] == aPieces[47] && aPieces[13] == aPieces[17] && aPieces[13] == aPieces[37])
                        {
                            await MakeTurnAsync("U' R' U R y U R U' R'");
                            continue;
                        }

                        // 2.  y (R U' R' U) (R U' R') --- R' U2 R' F R F' R
                        if (aPieces[49] == aPieces[17] && aPieces[13] == aPieces[37])
                        {
                            await MakeTurnAsync("y R U' R' U R U' R'");
                            continue;
                        }

                        // 3.  (R' U' R U) (R' U' R)
                        if (aPieces[49] == aPieces[17] && aPieces[13] == aPieces[10] && aPieces[22] == aPieces[41])
                        {
                            await MakeTurnAsync("R' U' R U R' U' R");
                            continue;
                        }

                        // 4.  y U (R U' R') U' (F' U F) --- y U (R U' R') (F R' F' R)
                        if (aPieces[49] == aPieces[47] && aPieces[13] == aPieces[10] && aPieces[13] == aPieces[17] && aPieces[22] == aPieces[41])
                        {
                            await MakeTurnAsync("y U R U' R' U' F' U F");
                            continue;
                        }

                        // 5.  (R' U R U') (R' U R)
                        if (aPieces[49] == aPieces[47] && aPieces[13] == aPieces[10] && aPieces[22] == aPieces[17] && aPieces[22] == aPieces[41])
                        {
                            await MakeTurnAsync("R' U R U' R' U R");
                            continue;
                        }

                        // 6.  y (R U R' U') (R U R')
                        if (aPieces[49] == aPieces[47] && aPieces[13] == aPieces[37] && aPieces[22] == aPieces[17])
                        {
                            await MakeTurnAsync("y R U R' U' R U R'");
                            continue;
                        }

                        // Edge in Place, Corner in U face
                        // 1.  (R' U R' F) (R F' R) --- (R' U R U') y (R U' R')
                        if (aPieces[49] == aPieces[38] && aPieces[22] == aPieces[11] && aPieces[22] == aPieces[14])
                        {
                            await MakeTurnAsync("R' U R' F R F' R");
                            continue;
                        }

                        // 2.  (U' R' U' R) U2 (R' U' R)
                        if (aPieces[49] == aPieces[11] && aPieces[13] == aPieces[14] && aPieces[13] == aPieces[38])
                        {
                            await MakeTurnAsync("U' R' U' R U2 R' U' R");
                            continue;
                        }

                        // 3.  y (U' R U R') d (R' U' R)
                        if (aPieces[49] == aPieces[11] && aPieces[13] == aPieces[38] && aPieces[22] == aPieces[14])
                        {
                            await MakeTurnAsync("y U' R U R' d R' U' R");
                            continue;
                        }

                        // 4.  (U' R' U R) (U' R' U R) (U' R' U R)
                        if (aPieces[49] == aPieces[38] && aPieces[13] == aPieces[14] && aPieces[22] == aPieces[11])
                        {
                            await MakeTurnAsync("U' R' U R U' R' U R U' R' U R");
                            continue;
                        }

                        // 5.  U (R' U R) U2' (R' U R)
                        if (aPieces[49] == aPieces[18] && aPieces[13] == aPieces[11] && aPieces[13] == aPieces[14] && aPieces[22] == aPieces[38])
                        {
                            await MakeTurnAsync("U R' U R U2 R' U R");
                            continue;
                        }

                        // 6.  U (R' U' R) y U' (R U R')
                        if (aPieces[49] == aPieces[18] && aPieces[13] == aPieces[11] && aPieces[22] == aPieces[14] && aPieces[22] == aPieces[38])
                        {
                            await MakeTurnAsync("U R' U' R y U' R U R'");
                            continue;
                        }

                        // Edge and Corner in Place
                        // 1.  Solved Pair

                        // 2.  (R' U R U') (R' U2 R U') (R' U R) 
                        if (aPieces[49] == aPieces[17] && aPieces[13] == aPieces[14])
                        {
                            await MakeTurnAsync("R' U R U' (R' U2 R U' R' U R");
                            continue;
                        }

                        // 3.  (R' U R) U2' y (R U R' U) (R U' R')
                        if (aPieces[49] == aPieces[17] && aPieces[22] == aPieces[14])
                        {
                            await MakeTurnAsync("R' U R U2 y R U R' U R U' R'");
                            continue;
                        }

                        // 4.  (R' U R) d' (R U2' R') U2 (R U' R')
                        if (aPieces[49] == aPieces[47] && aPieces[13] == aPieces[17] && aPieces[22] == aPieces[14])
                        {
                            await MakeTurnAsync("R' U R d' R U2 R' U2 R U' R'");
                            continue;
                        }

                        // 5.  (R' U' R U) (R' U2' R) U (R' U' R) --- (R' U R U) (R' U' R U2') (R' U R)
                        if (aPieces[49] == aPieces[47] && aPieces[13] == aPieces[14] && aPieces[22] == aPieces[17])
                        {
                            await MakeTurnAsync("R' U' R U R' U2 R U R' U' R");
                            continue;
                        }

                        // 6.  (R' U R U) (R' U R U') y (R U R')
                        if (aPieces[49] == aPieces[47] && aPieces[22] == aPieces[14] && aPieces[22] == aPieces[17])
                        {
                            await MakeTurnAsync("R' U R U R' U R U' y R U R'");
                            continue;
                        }
                    }

                    // Piece 4 = 5, 7, 8 and piece 13 = 12, 14, 15, 16, 17
                    if (aPieces[4] == aPieces[5] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[8] && aPieces[13] == aPieces[12] && aPieces[13] == aPieces[14] && aPieces[13] == aPieces[15] && aPieces[13] == aPieces[16] && aPieces[13] == aPieces[17])
                    {
                        // Page 5. Algorithms for slot in front-left position
                        // Algorithms for slot in front-left position
                        // Basic Inserts
                        // 1.  y' U (R U' R') --- y U(L U' L')
                        if (aPieces[49] == aPieces[29] && aPieces[4] == aPieces[0] && aPieces[4] == aPieces[1] && aPieces[31] == aPieces[42] && aPieces[31] == aPieces[43])
                        {
                            await MakeTurnAsync("y' U R U' R'");
                            continue;
                        }

                        // 2.  (L' U' L)
                        if (aPieces[49] == aPieces[29] && aPieces[4] == aPieces[0] && aPieces[4] == aPieces[37] && aPieces[31] == aPieces[42])
                        {
                            await MakeTurnAsync("L' U' L");
                            continue;
                        }

                        // 3.  U' (L' U L)
                        if (aPieces[49] == aPieces[0] && aPieces[4] == aPieces[39] && aPieces[4] == aPieces[42] && aPieces[31] == aPieces[29])
                        {
                            await MakeTurnAsync("U' L' U L");
                            continue;
                        }

                        // 4.  y' (R U R') --- y (L U L')
                        if (aPieces[49] == aPieces[0] && aPieces[4] == aPieces[10] && aPieces[4] == aPieces[42] && aPieces[31] == aPieces[29] && aPieces[31] == aPieces[41])
                        {
                            await MakeTurnAsync("y' R U R'");
                            continue;
                        }

                        // F2L Case 1
                        // 1.  U (L' U' L U') (L' U' L)
                        if (aPieces[49] == aPieces[29] && aPieces[4] == aPieces[0] && aPieces[4] == aPieces[41] && aPieces[31] == aPieces[10] && aPieces[31] == aPieces[42])
                        {
                            await MakeTurnAsync("U L' U' L U' L' U' L");
                            continue;
                        }

                        // 2.  L U2' L2' U' (L2 U' L')
                        if (aPieces[49] == aPieces[29] && aPieces[4] == aPieces[0] && aPieces[4] == aPieces[43] && aPieces[31] == aPieces[1] && aPieces[31] == aPieces[42])
                        {
                            await MakeTurnAsync("L U2 L2 U' L2 U' L'");
                            continue;
                        }

                        // 3.  U (L' U L U') (L' U' L)
                        if (aPieces[49] == aPieces[29] && aPieces[4] == aPieces[0] && aPieces[4] == aPieces[39] && aPieces[31] == aPieces[42])
                        {
                            await MakeTurnAsync("U L' U L U' L' U' L");
                            continue;
                        }

                        // 4.  U (L' U L U') y' (R U R')
                        if (aPieces[49] == aPieces[0] && aPieces[4] == aPieces[42] && aPieces[31] == aPieces[29] && aPieces[31] == aPieces[37])
                        {
                            await MakeTurnAsync("U L' U L U' y' R U R'");
                            continue;
                        }

                        // 5.  U (L' U2 L) U' y' (R U R')
                        if (aPieces[49] == aPieces[0] && aPieces[4] == aPieces[42] && aPieces[31] == aPieces[29] && aPieces[31] == aPieces[39])
                        {
                            await MakeTurnAsync("U L' U2 L U' y' R U R'");
                            continue;
                        }

                        // 6.  y' U' (R U' R' U) (R U R')
                        if (aPieces[49] == aPieces[0] && aPieces[4] == aPieces[1] && aPieces[4] == aPieces[42] && aPieces[31] == aPieces[29] && aPieces[31] == aPieces[43])
                        {
                            await MakeTurnAsync("y' U' R U' R' U R U R'");
                            continue;
                        }

                        // F2L Case 2
                        // 1.  y' (U' R U R') U2 (R U' R') --- (L U L') y'(U R U' R') --- Note: the second algorithm should only be used when the back-left slot is empty
                        if (aPieces[49] == aPieces[29] && aPieces[4] == aPieces[0] && aPieces[4] == aPieces[10] && aPieces[31] == aPieces[41] && aPieces[31] == aPieces[42])
                        {
                            await MakeTurnAsync("y' U' R U R' U2 R U' R'");
                            continue;
                        }

                        // 2.  y' U' (R U2' R') U2 (R U' R')
                        if (aPieces[49] == aPieces[29] && aPieces[4] == aPieces[0] && aPieces[31] == aPieces[37] && aPieces[31] == aPieces[42])
                        {
                            await MakeTurnAsync("y' U' R U2 R' U2 R U' R'");
                            continue;
                        }

                        // 3.  (U L' U' L) U2' (L' U L)
                        if (aPieces[49] == aPieces[0] && aPieces[4] == aPieces[37] && aPieces[4] == aPieces[42] && aPieces[31] == aPieces[29])
                        {
                            await MakeTurnAsync("U L' U' L U2 L' U L");
                            continue;
                        }

                        // 4.  U (L' U2 L) U2' (L' U L)
                        if (aPieces[49] == aPieces[0] && aPieces[4] == aPieces[41] && aPieces[4] == aPieces[42] && aPieces[31] == aPieces[10] && aPieces[31] == aPieces[29])
                        {
                            await MakeTurnAsync("U L' U2 L U2 L' U L");
                            continue;
                        }

                        // F2L Case 3
                        // 1.  y' U (R U2' R') U (R U' R')
                        if (aPieces[49] == aPieces[42] && aPieces[4] == aPieces[10] && aPieces[4] == aPieces[29] && aPieces[31] == aPieces[0] && aPieces[31] == aPieces[41])
                        {
                            await MakeTurnAsync("y' U R U2 R' U R U' R'");
                            continue;
                        }

                        // 2.  F R U2' R' F'
                        if (aPieces[49] == aPieces[42] && aPieces[4] == aPieces[19] && aPieces[4] == aPieces[29] && aPieces[31] == aPieces[0] && aPieces[31] == aPieces[37])
                        {
                            await MakeTurnAsync("F R U2 R' F'");
                            continue;
                        }

                        // 3.  U' (L' U2 L) U' (L' U L)
                        if (aPieces[49] == aPieces[42] && aPieces[4] == aPieces[29] && aPieces[4] == aPieces[37] && aPieces[31] == aPieces[0] && aPieces[31] == aPieces[19])
                        {
                            await MakeTurnAsync("U' L' U2 L U' L' U L");
                            continue;
                        }

                        // 4.  U2 (L' U' L) U' (L' U L)
                        if (aPieces[49] == aPieces[42] && aPieces[4] == aPieces[29] && aPieces[4] == aPieces[41] && aPieces[31] == aPieces[0] && aPieces[31] == aPieces[10])
                        {
                            await MakeTurnAsync("U2 L' U' L U' L' U L");
                            continue;
                        }

                        // Page 6. Algorithms for slot in front-left position
                        // !!! Not sure that all comparisons with the pieces are correct, not all colors are visible !!!
                        // Incorrectly Connected Pieces
                        // 1.  (L' U L) U2' y' (R U R')
                        if (aPieces[49] == aPieces[29] && aPieces[4] == aPieces[0] && aPieces[4] == aPieces[28] && aPieces[31] == aPieces[39] && aPieces[31] == aPieces[42])
                        {
                            await MakeTurnAsync("L' U L U2 y' R U R'");
                            continue;
                        }

                        // 2.  y' (R U2 R') U' (R U R')
                        if (aPieces[49] == aPieces[42] && aPieces[4] == aPieces[1] && aPieces[4] == aPieces[29] && aPieces[31] == aPieces[0] && aPieces[31] == aPieces[43])
                        {
                            await MakeTurnAsync("y' R U2 R' U' R U R'");
                            continue;
                        }

                        // 3.  y' U (R U' R' U') (R U' R' U R U' R') --- y' (R U R' U2') (R U R' U') (R U R')
                        if (aPieces[49] == aPieces[42] && aPieces[4] == aPieces[28] && aPieces[4] == aPieces[29] && aPieces[31] == aPieces[0] && aPieces[31] == aPieces[39])
                        {
                            await MakeTurnAsync("y' R U R' U2 R U R' U' R U R'");
                            continue;
                        }

                        // 4.  y' (R U' R' U2) y' (R' U' R) --- (U' R U' R') U'(L' U' L) --- Note: the second algorithm should only be used when the front-right slot is empty
                        if (aPieces[49] == aPieces[0] && aPieces[4] == aPieces[42] && aPieces[4] == aPieces[43] && aPieces[31] == aPieces[1] && aPieces[31] == aPieces[29])
                        {
                            await MakeTurnAsync("y' R U' R' U2 y' R' U' R");
                            continue;
                        }

                        // 5.  (L' U2 L) U (L' U' L)
                        if (aPieces[49] == aPieces[42] && aPieces[4] == aPieces[29] && aPieces[4] == aPieces[39] && aPieces[31] == aPieces[0] && aPieces[31] == aPieces[28])
                        {
                            await MakeTurnAsync("L' U2 L U L' U' L");
                            continue;
                        }

                        // 6.  U' (L' U L U) (L' U L U') (L' U L) --- y' F (U R U' R') F' (R U' R')
                        if (aPieces[49] == aPieces[42] && aPieces[4] == aPieces[29] && aPieces[4] == aPieces[43] && aPieces[31] == aPieces[0] && aPieces[31] == aPieces[1])
                        {
                            await MakeTurnAsync("y' F U R U' R' F' R U' R'");
                            continue;
                        }

                        // Corner in Place, Edge in U Face
                        // 1.  (U' L' U L) d (R U' R')
                        if (aPieces[49] == aPieces[45] && aPieces[4] == aPieces[1] && aPieces[4] == aPieces[6] && aPieces[31] == aPieces[35] && aPieces[31] == aPieces[43])
                        {
                            await MakeTurnAsync("U' L' U L d R U' R'");
                            continue;
                        }

                        // 2.  y' (R U' R' U) (R U' R')
                        if (aPieces[49] == aPieces[35] && aPieces[4] == aPieces[1] && aPieces[4] == aPieces[45] && aPieces[31] == aPieces[6] && aPieces[31] == aPieces[43])
                        {
                            await MakeTurnAsync("y' R U' R' U R U' R'");
                            continue;
                        }

                        // 3.  (L' U' L U) (L' U' L)
                        if (aPieces[49] == aPieces[35] && aPieces[4] == aPieces[39] && aPieces[4] == aPieces[45] && aPieces[31] == aPieces[6] && aPieces[31] == aPieces[28])
                        {
                            await MakeTurnAsync("L' U' L U L' U' L");
                            continue;
                        }

                        // 4.  y' U (R U' R') U' (F' U F) --- y' U (R U' R') (F R' F' R)
                        if (aPieces[49] == aPieces[45] && aPieces[4] == aPieces[6] && aPieces[4] == aPieces[39] && aPieces[31] == aPieces[28] && aPieces[31] == aPieces[35])
                        {
                            await MakeTurnAsync("y' U R U' R' U' F' U F");
                            continue;
                        }

                        // 5.  (L' U L U') (L' U L)
                        if (aPieces[49] == aPieces[6] && aPieces[4] == aPieces[35] && aPieces[4] == aPieces[39] && aPieces[31] == aPieces[28] && aPieces[31] == aPieces[45])
                        {
                            await MakeTurnAsync("L' U L U' L' U L");
                            continue;
                        }

                        // 6.  y' (R U R' U') (R U R') --- (r U' r' F) (r U' r' F)
                        if (aPieces[49] == aPieces[6] && aPieces[4] == aPieces[1] && aPieces[4] == aPieces[35] && aPieces[31] == aPieces[43] && aPieces[31] == aPieces[45])
                        {
                            await MakeTurnAsync("y' R U R' U' R U R'");
                            continue;
                        }

                        // Edge in Place, Corner in U face
                        // 1.  (L' U L U') y' (R U' R')
                        if (aPieces[49] == aPieces[42] && aPieces[4] == aPieces[29] && aPieces[4] == aPieces[32] && aPieces[31] == aPieces[0] && aPieces[31] == aPieces[3])
                        {
                            await MakeTurnAsync("L' U L U' y' R U' R'");
                            continue;
                        }

                        // 2.  (U' L' U' L) U2 (L' U' L)
                        if (aPieces[49] == aPieces[29] && aPieces[4] == aPieces[0] && aPieces[4] == aPieces[3] && aPieces[31] == aPieces[32] && aPieces[31] == aPieces[42])
                        {
                            await MakeTurnAsync("U' L' U' L U2 L' U' L");
                            continue;
                        }

                        // 3.  y' (U' R U R') d (R' U' R)
                        if (aPieces[49] == aPieces[29] && aPieces[4] == aPieces[0] && aPieces[4] == aPieces[32] && aPieces[31] == aPieces[3] && aPieces[31] == aPieces[42])
                        {
                            await MakeTurnAsync("y' U' R U R' d R' U' R");
                            continue;
                        }

                        // 4.  (U' L' U L) (U' L' U L) (U' L' U L)
                        if (aPieces[49] == aPieces[42] && aPieces[4] == aPieces[3] && aPieces[4] == aPieces[29] && aPieces[31] == aPieces[0] && aPieces[31] == aPieces[32])
                        {
                            await MakeTurnAsync("U' L' U L U' L' U L U' L' U L");
                            continue;
                        }

                        // 5.  U (L' U L) U2' (L' U L)
                        if (aPieces[49] == aPieces[0] && aPieces[4] == aPieces[3] && aPieces[4] == aPieces[42] && aPieces[31] == aPieces[29] && aPieces[31] == aPieces[32])
                        {
                            await MakeTurnAsync("U L' U L U2 L' U L");
                            continue;
                        }

                        // 6.  U (L' U' L) y' U' (R U R')
                        if (aPieces[49] == aPieces[0] && aPieces[4] == aPieces[32] && aPieces[4] == aPieces[42] && aPieces[31] == aPieces[3] && aPieces[31] == aPieces[29])
                        {
                            await MakeTurnAsync("U L' U' L y' U' R U R'");
                            continue;
                        }

                        // Edge and Corner in Place
                        // 1.  Solved Pair

                        // 2.  (L' U L U') (L' U2 L U') (L' U L)
                        if (aPieces[49] == aPieces[35] && aPieces[4] == aPieces[3] && aPieces[4] == aPieces[45] && aPieces[31] == aPieces[6] && aPieces[31] == aPieces[32])
                        {
                            await MakeTurnAsync("L' U L U' L' U2 L U' L' U L");
                            continue;
                        }

                        // 3.  (L' U L) F R U2' R' F'
                        if (aPieces[49] == aPieces[35] && aPieces[4] == aPieces[32] && aPieces[4] == aPieces[45] && aPieces[31] == aPieces[3] && aPieces[31] == aPieces[6])
                        {
                            await MakeTurnAsync("L' U L F R U2 R' F'");
                            continue;
                        }

                        // 4.  (L' U L U') y' (R U2' R' U2 R U' R')
                        if (aPieces[49] == aPieces[45] && aPieces[4] == aPieces[6] && aPieces[4] == aPieces[32] && aPieces[31] == aPieces[3] && aPieces[31] == aPieces[35])
                        {
                            await MakeTurnAsync("L' U L U' y' R U2 R' U2 R U' R'");
                            continue;
                        }

                        // 5.  (L' U' L U) (L' U2' L) U (L' U' L) --- (L' U L U) (L' U' L U2') (L' U L)
                        if (aPieces[49] == aPieces[6] && aPieces[4] == aPieces[3] && aPieces[4] == aPieces[35] && aPieces[31] == aPieces[32] && aPieces[31] == aPieces[45])
                        {
                            await MakeTurnAsync("L' U' L U L' U2 L U L' U' L");
                            continue;
                        }

                        // 6.  (L' U L U) (L' U L U') y (L U L')
                        if (aPieces[49] == aPieces[6] && aPieces[4] == aPieces[32] && aPieces[4] == aPieces[35] && aPieces[31] == aPieces[3] && aPieces[31] == aPieces[45])
                        {
                            await MakeTurnAsync("L' U L U L' U L U' y L U L'");
                            continue;
                        }

                    }

                    // Piece 4 = 3, 5, 6, 7, 8 and piece 13 = 12, 14, 15, 16, 17
                    if (aPieces[4] == aPieces[3] && aPieces[4] == aPieces[5] && aPieces[4] == aPieces[6] && aPieces[4] == aPieces[7] && aPieces[4] == aPieces[8] && aPieces[13] == aPieces[12] && aPieces[13] == aPieces[14] && aPieces[13] == aPieces[15] && aPieces[13] == aPieces[16] && aPieces[13] == aPieces[17])
                    {
                        // Page 7. Algorithms for slot in back-left position
                        // Algorithms for slot in back-left position
                        // Basic Inserts
                        // 1.  U (L U' L')

                        // 2.  y (R' U' R)

                        // 3.  y U' (R' U R) --- y' U'(L' U L)

                        // 4.  (L U L')

                        // F2L Case 1
                        // 1.  y U (R' U' R U') (R' U' R)

                        // 2.  y R U2' R2' U' (R2 U' R')

                        // 3.  y U (R' U R U') (R' U' R)

                        // 4.  U' (L U L' U) (L U L')

                        // 5.  L' U2 L2 U (L2' U L)

                        // 6.  U' (L U' L' U) (L U L')

                        // F2L Case 2
                        // 1.  (U' L U L') U2 (L U' L')

                        // 2.  U' (L U2 L') U2 (L U' L')

                        // 3.  y (U R' U' R) U2' (R' U R)

                        // 4.  y U (R' U2' R) U2' (R' U R)

                        // F2L Case 3
                        // 1.  U (L U2' L') U (L U' L')

                        // 2.  y' F R U2' R' F'

                        // 3.  y U' (R' U2 R) U' (R' U R)

                        // 4.  y U2 (R' U' R) U' (R' U R) --- y R' F' U2 F R --- Note: The second algorithm is fewer moves, but less intuitive and less finger-friendly

                        // Page 8. Algorithms for slot in back-left position
                        // Incorrectly Connected Pieces
                        // 1.  y (R' U R) U2' y (R U R') --- U' (L' U L U') (L U' L') --- Note: the second algorithm should only be used when the front-left slot is empty

                        // 2.  (L U2 L') U' (L U L')

                        // 3.  U (L U' L' U') (L U' L' U) (L U' L')

                        // 4.  (L U' L' U2) y (R' U' R)

                        // 5.  y (R' U2' R) U (R' U' R)

                        // 6.  y U' (R' U R U) (R' U R U') (R' U R)

                    }

                    // Piece 4 = 3, 6, 7 and piece 13 = 14, 16, 17
                    if (aPieces[4] == aPieces[3] && aPieces[4] == aPieces[6] && aPieces[4] == aPieces[7] && aPieces[13] == aPieces[14] && aPieces[13] == aPieces[16] && aPieces[13] == aPieces[17])
                    {
                        // Page 8. Algorithms for slot in back-left position
                        // Corner in Place, Edge in U Face
                        // 1.  [y2]  y' (U' L' U L) d (R U' R')

                        // 2.  [y2]  (L U' L' U) (L U' L')

                        // 3.  [y2]  y (R' U' R U) (R' U' R)

                        // 4.  [y2]  U (L U' L') d' (R' U R)

                        // 5.  [y2]  y (R' U R U') (R' U R)

                        // 6.  [y2]  (L U L' U') (L U L')

                        // Edge in Place, Corner in U face
                        // 1.  [y2]  y (R' U R' F) (R F' R)

                        // 2.  [y2]  (U' L U' L') U2 (L U' L')

                        // 3.  [y2]  (U2' L U L') d' (R' U R)

                        // 4.  [y2]  (U L U' L') (U L U' L') (U L U' L')

                        // 5.  [y2]  U (L U L') U2 (L U L')

                        // 6.  [y2]  y U2' (R' U R) d (L U L')

                        // Edge and Corner in Place
                        // 1.  [y2]  Solved Pair

                        // 2.  [y2]  (L U L' U') (L U2 L' U') (L U L')

                        // 3.  [y2]  y (R' U R) U2' y (R U R' U R U' R')

                        // 4.  [y2]  y (R' U R) d' (R U2' R' U2 R U' R')

                        // 5.  [y2]  (L U' L' U) (L U2' L') U (L U' L')

                        // 6.  [y2]  (L U' L') d' (U' R' U' R U') (R' U R)

                    }
                }

                //--------------------------------------------------------------------------------------------------------------


                //--------------------------------------------------------------------------------------------------------------
            }

            //return true;
        }
    }
}
