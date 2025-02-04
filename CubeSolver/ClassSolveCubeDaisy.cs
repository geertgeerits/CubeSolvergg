/* This solution is based on:
   https://www.youtube.com/watch?v=Lm9jRkikhlI
   https://www.youtube.com/watch?v=lgm7NuQGgtw&list=PLfZ_bKS9WEOA-woYuj-_y3EmQqzhRboNw&index=57&t=706s
   file:///C:/Sources/MAUI/CubeSolver/Miscellaneous/Manuals/RubiksCubeBeginnerInstructions.pdf */

using System.Diagnostics;
using static CubeSolver.Globals;

namespace CubeSolver
{
    internal sealed class ClassSolveCubeDaisy
    {
        //// Declare variables
        private const int nLoopTimesMax = 200;

        /// <summary>
        /// Solve the cube
        /// </summary>
        /// <returns></returns>
        public static async Task<bool> SolveTheCubeDaisyAsync()
        {
            if (!await ClassSolveCubeCommon.SolveTopLayerEdgesAsync())
            {
                return false;
            }

            if (!await ClassSolveCubeCommon.SolveTopLayerEdges2Async())
            {
                return false;
            }

            if (!await SolveTopLayerCornersAsync())
            {
                return false;
            }

            if (!await SolveMiddleLayerAsync())
            {
                return false;
            }

            if (!await SolveBottomLayerEdgesAsync())
            {
                return false;
            }

            if (!await SolveBottomLayerEdges2Async())
            {
                return false;
            }

            if (!await SolveBottomLayerCornersAsync())
            {
                return false;
            }

            if (!await SolveBottomLayerTumblingCornersAsync())
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
        /// Solve the corners of the top layer but turned at the bottom
        /// </summary>
        /// <returns></returns>
        private static async Task<bool> SolveTopLayerCornersAsync()
        {
            string cB = aPieces[49];
            int nLoopTimes = 0;

            while (true)
            {
                nLoopTimes++;
                if (nLoopTimes > nLoopTimesMax)
                {
                    Debug.WriteLine("Daisy: nLoopTimes top layer corners: " + nLoopTimes);
                    return false;
                }

                // If solved, break the loop
                if (cB == aPieces[45] && cB == aPieces[46] && cB == aPieces[47] && cB == aPieces[48] && cB == aPieces[50] && cB == aPieces[51] && cB == aPieces[52] && cB == aPieces[53])
                {
                    Debug.WriteLine("Daisy: number of turns top layer corners: " + lCubeTurns.Count);
                    break;
                }

                // The down color (before turning the cube: Up color) is at the back face [18]
                if (cB == aPieces[18])
                {
                    if (aPieces[22] == aPieces[11] || aPieces[22] == aPieces[38])
                    {
                        if (aPieces[13] == aPieces[11] || aPieces[13] == aPieces[38])
                        {
                            await MakeTurnAsync("y");
                        }
                    }
                }

                // The down color (before turning the cube: Up color) is at the right face [11]
                if (cB == aPieces[11])
                {
                    if (aPieces[22] == aPieces[18] || aPieces[22] == aPieces[38])
                    {
                        if (aPieces[13] == aPieces[18] || aPieces[13] == aPieces[38])
                        {
                            await MakeTurnAsync("y");
                        }
                    }
                }

                // The down color (before turning the cube: Up color) is at the up face [38]
                if (cB == aPieces[38])
                {
                    if (aPieces[22] == aPieces[11] || aPieces[22] == aPieces[18])
                    {
                        if (aPieces[13] == aPieces[11] || aPieces[13] == aPieces[18])
                        {
                            await MakeTurnAsync("y");
                        }
                    }
                }

                // The down color (before turning the cube: Up color) is at the back face [20]
                if (cB == aPieces[20])
                {
                    if (aPieces[22] == aPieces[27] || aPieces[22] == aPieces[36])
                    {
                        if (aPieces[31] == aPieces[27] || aPieces[31] == aPieces[36])
                        {
                            await MakeTurnAsync("y'");
                        }
                    }
                }

                // The down color (before turning the cube: Up color) is at the left face [27]
                if (cB == aPieces[27])
                {
                    if (aPieces[22] == aPieces[20] || aPieces[22] == aPieces[36])
                    {
                        if (aPieces[31] == aPieces[20] || aPieces[31] == aPieces[36])
                        {
                            await MakeTurnAsync("y'");
                        }
                    }
                }

                // The down color (before turning the cube: Up color) is at the up face [36]
                if (cB == aPieces[36])
                {
                    if (aPieces[22] == aPieces[20] || aPieces[22] == aPieces[27])
                    {
                        if (aPieces[31] == aPieces[20] || aPieces[31] == aPieces[27])
                        {
                            await MakeTurnAsync("y'");
                        }
                    }
                }

                // The down color (before turning the cube: Up color) is at the front face [0]
                if (cB == aPieces[0])
                {
                    if (aPieces[4] == aPieces[29] || aPieces[4] == aPieces[42])
                    {
                        if (aPieces[31] == aPieces[29] || aPieces[31] == aPieces[42])
                        {
                            while (true)
                            {
                                nLoopTimes++;
                                if (nLoopTimes > nLoopTimesMax)
                                {
                                    Debug.WriteLine("Daisy: nLoopTimes corners top layer: " + nLoopTimes);
                                    return false;
                                }

                                if (cB == aPieces[45] && aPieces[4] == aPieces[6] && aPieces[31] == aPieces[35])
                                {
                                    break;
                                }

                                await MakeTurnAsync("L' U' L U");
                            }
                        }
                    }
                }

                // The down color (before turning the cube: Up color) is at the left face [29]
                if (cB == aPieces[29])
                {
                    if (aPieces[4] == aPieces[0] || aPieces[4] == aPieces[42])
                    {
                        if (aPieces[31] == aPieces[0] || aPieces[31] == aPieces[42])
                        {
                            while (true)
                            {
                                nLoopTimes++;
                                if (nLoopTimes > nLoopTimesMax)
                                {
                                    Debug.WriteLine("Daisy: nLoopTimes corners top layer: " + nLoopTimes);
                                    return false;
                                }

                                if (cB == aPieces[45] && aPieces[4] == aPieces[6] && aPieces[31] == aPieces[35])
                                {
                                    break;
                                }

                                await MakeTurnAsync("L' U' L U");
                            }
                        }
                    }
                }

                // The down color (before turning the cube: Up color) is at the up face [42]
                if (cB == aPieces[42])
                {
                    if (aPieces[4] == aPieces[0] || aPieces[4] == aPieces[29])
                    {
                        if (aPieces[31] == aPieces[0] || aPieces[31] == aPieces[29])
                        {
                            while (true)
                            {
                                nLoopTimes++;
                                if (nLoopTimes > nLoopTimesMax)
                                {
                                    Debug.WriteLine("Daisy: nLoopTimes corners top layer: " + nLoopTimes);
                                    return false;
                                }

                                if (cB == aPieces[45] && aPieces[4] == aPieces[6] && aPieces[31] == aPieces[35])
                                {
                                    break;
                                }

                                await MakeTurnAsync("L' U' L U");
                            }
                        }
                    }
                }

                // The down color (before turning the cube: Up color) is at the front face [2]
                if (cB == aPieces[2])
                {
                    if (aPieces[4] == aPieces[9] || aPieces[4] == aPieces[44])
                    {
                        if (aPieces[13] == aPieces[9] || aPieces[13] == aPieces[44])
                        {
                            while (true)
                            {
                                nLoopTimes++;
                                if (nLoopTimes > nLoopTimesMax)
                                {
                                    Debug.WriteLine("Daisy: nLoopTimes corners top layer: " + nLoopTimes);
                                    return false;
                                }

                                if (cB == aPieces[47] && aPieces[4] == aPieces[8] && aPieces[13] == aPieces[15])
                                {
                                    break;
                                }

                                await MakeTurnAsync("R U R' U'");
                            }
                        }
                    }
                }

                // The down color (before turning the cube: Up color) is at the right face [9]
                if (cB == aPieces[9])
                {
                    if (aPieces[4] == aPieces[2] || aPieces[4] == aPieces[44])
                    {
                        if (aPieces[13] == aPieces[2] || aPieces[13] == aPieces[44])
                        {
                            while (true)
                            {
                                nLoopTimes++;
                                if (nLoopTimes > nLoopTimesMax)
                                {
                                    Debug.WriteLine("Daisy: nLoopTimes corners top layer: " + nLoopTimes);
                                    return false;
                                }

                                if (cB == aPieces[47] && aPieces[4] == aPieces[8] && aPieces[13] == aPieces[15])
                                {
                                    break;
                                }

                                await MakeTurnAsync("R U R' U'");
                            }
                        }
                    }
                }

                // The down color (before turning the cube: Up color) is at the up face [44]
                if (cB == aPieces[44])
                {
                    if (aPieces[4] == aPieces[2] || aPieces[4] == aPieces[9])
                    {
                        if (aPieces[13] == aPieces[2] || aPieces[13] == aPieces[9])
                        {
                            while (true)
                            {
                                nLoopTimes++;
                                if (nLoopTimes > nLoopTimesMax)
                                {
                                    Debug.WriteLine("Daisy: nLoopTimes corners top layer: " + nLoopTimes);
                                    return false;
                                }

                                if (cB == aPieces[47] && aPieces[4] == aPieces[8] && aPieces[13] == aPieces[15])
                                {
                                    break;
                                }

                                await MakeTurnAsync("R U R' U'");
                            }
                        }
                    }
                }

                // If there is a color like [49] at the bottom side, turn that color to the top side
                if (cB == aPieces[6])
                {
                    await MakeTurnAsync("y' R U R' U'");
                }

                if (cB == aPieces[8])
                {
                    await MakeTurnAsync("y L' U' L U");
                }

                if (cB == aPieces[15])
                {
                    await MakeTurnAsync("R U R' U'");
                }

                if (cB == aPieces[17])
                {
                    await MakeTurnAsync("y2 L' U' L U");
                }

                if (cB == aPieces[24])
                {
                    await MakeTurnAsync("y R U R' U'");
                }

                if (cB == aPieces[26])
                {
                    await MakeTurnAsync("y' L' U' L U");
                }

                if (cB == aPieces[33])
                {
                    await MakeTurnAsync("y2 R U R' U'");
                }

                if (cB == aPieces[35])
                {
                    await MakeTurnAsync("L' U' L U");
                }

                await MakeTurnAsync("U");
            }

            return true;
        }

        /// <summary>
        /// Solve the middle layer
        /// </summary>
        /// <returns></returns>
        private static async Task<bool> SolveMiddleLayerAsync()
        {
            int nLoopTimes = 0;

            while (true)
            {
                nLoopTimes++;
                if (nLoopTimes > nLoopTimesMax)
                {
                    Debug.WriteLine("Daisy: nLoopTimes middle layer: " + nLoopTimes);
                    return false;
                }

                // If solved, break the loop
                if (aPieces[4] == aPieces[3] && aPieces[4] == aPieces[5] && aPieces[13] == aPieces[12] && aPieces[13] == aPieces[14] && aPieces[22] == aPieces[21] && aPieces[22] == aPieces[23] && aPieces[31] == aPieces[30] && aPieces[31] == aPieces[32])
                {
                    Debug.WriteLine("Daisy: number of turns middle layer: " + lCubeTurns.Count);
                    break;
                }

                // 1-43 has to go to the left 3-32
                if (aPieces[4] == aPieces[10] && aPieces[31] == aPieces[41])
                {
                    await MakeTurnAsync("U");
                }

                if (aPieces[4] == aPieces[28] && aPieces[31] == aPieces[39])
                {
                    await MakeTurnAsync("U'");
                }

                if (aPieces[4] == aPieces[19] && aPieces[31] == aPieces[37])
                {
                    await MakeTurnAsync("U2");
                }

                if (aPieces[4] == aPieces[1] && aPieces[31] == aPieces[43])
                {
                    await MakeTurnAsync("U' L' U L U F U' F'");
                }

                // 1-43 has to go to the right 5-12
                if (aPieces[4] == aPieces[10] && aPieces[13] == aPieces[41])
                {
                    await MakeTurnAsync("U");
                }

                if (aPieces[4] == aPieces[28] && aPieces[13] == aPieces[39])
                {
                    await MakeTurnAsync("U'");
                }

                if (aPieces[4] == aPieces[19] && aPieces[13] == aPieces[37])
                {
                    await MakeTurnAsync("U2");
                }

                if (aPieces[4] == aPieces[1] && aPieces[13] == aPieces[43])
                {
                    await MakeTurnAsync("U R U' R' U' F' U F");
                }

                // Edge in the right place but flipped
                if (aPieces[4] == aPieces[12] && aPieces[13] == aPieces[5])
                {
                    await MakeTurnAsync("U R U' R' U' F' U F");
                }

                // Edge in the wrong place within the middle layer
                if (aPieces[4] != aPieces[12] && aPieces[13] == aPieces[5])
                {
                    await MakeTurnAsync("U R U' R' U' F' U F");
                }

                await MakeTurnAsync("y");
            }

            return true;
        }

        /// <summary>
        /// Solve the bottom layer - Make a cross
        /// </summary>
        /// <returns></returns>
        private static async Task<bool> SolveBottomLayerEdgesAsync()
        {
            string cB = aPieces[40];
            int nLoopTimes = 0;

            while (true)
            {
                nLoopTimes++;
                if (nLoopTimes > nLoopTimesMax)
                {
                    Debug.WriteLine("Daisy: nLoopTimes bottom layer edges: " + nLoopTimes);
                    return false;
                }

                // If solved, break the loop
                if (cB == aPieces[37] && cB == aPieces[39] && cB == aPieces[41] && cB == aPieces[43])
                {
                    Debug.WriteLine("Daisy: number of turns bottom layer edges: " + lCubeTurns.Count);
                    break;
                }

                // Make a cross - L shape
                if (cB == aPieces[37] && cB == aPieces[41])
                {
                    await MakeTurnAsync("U'");
                }

                if (cB == aPieces[39] && cB == aPieces[43])
                {
                    await MakeTurnAsync("U");
                }

                if (cB == aPieces[41] && cB == aPieces[43])
                {
                    await MakeTurnAsync("U2");
                }

                if (cB == aPieces[37] && cB == aPieces[39])
                {
                    await MakeTurnAsync("F U R U' R' F'");
                    break;
                }

                // Make a cross - Horizontal line
                if (cB == aPieces[37] && cB == aPieces[43])
                {
                    await MakeTurnAsync("U");
                }

                if (cB == aPieces[39] && cB == aPieces[41])
                {
                    await MakeTurnAsync("F R U R' U' F'");
                    break;
                }

                // Make a cross - Only the upper center piece is correct
                if (cB != aPieces[36] && cB != aPieces[37] && cB != aPieces[38] && cB != aPieces[39] && cB != aPieces[41] && cB != aPieces[42] && cB != aPieces[43] && cB != aPieces[44])
                {
                    await MakeTurnAsync("F R U R' U' F' U2 F U R U' R' F'");
                    break;
                }

                await MakeTurnAsync("y");
            }

            return true;
        }

        /// <summary>
        /// Solve the bottom layer - Put the edges on the correct place
        /// </summary>
        /// <returns></returns>
        private static async Task<bool> SolveBottomLayerEdges2Async()
        {
            string cB = aPieces[40];
            int nLoopTimes = 0;

            while (true)
            {
                nLoopTimes++;
                if (nLoopTimes > nLoopTimesMax)
                {
                    Debug.WriteLine("Daisy: nLoopTimes bottom layer edges 2: " + nLoopTimes);
                    return false;
                }

                // If solved, break the loop
                if (cB == aPieces[37] && cB == aPieces[39] && cB == aPieces[41] && cB == aPieces[43])
                {
                    if (aPieces[1] == aPieces[4] && aPieces[10] == aPieces[13] && aPieces[19] == aPieces[22] && aPieces[28] == aPieces[31])
                    {
                        Debug.WriteLine("Daisy: number of turns bottom layer edges 2: " + lCubeTurns.Count);
                        break;
                    }
                }

                if (cB != aPieces[37] && cB != aPieces[39] && cB != aPieces[41] && cB != aPieces[43])
                {
                    await MakeTurnAsync("F R U R' U' F'");
                }

                await SwitchEdgeCubesTopLayerAsync();
            }

            return true;
        }

        /// <summary>
        /// Solve the bottom layer - Corners on their places - Part 1
        /// </summary>
        /// <returns></returns>
        private static async Task<bool> SolveBottomLayerCornersAsync()
        {
            int nLoopTimes = 0;

            while (true)
            {
                nLoopTimes++;
                if (nLoopTimes > nLoopTimesMax)
                {
                    Debug.WriteLine("Daisy: nLoopTimes bottom layer corners: " + nLoopTimes);
                    return false;
                }

                // If all corners are on their places, break the loop
                if (SolveBottomLayerCheckCornersInRightPlace())
                {
                    Debug.WriteLine("Daisy: number of turns bottom layer corners: " + lCubeTurns.Count);
                    break;
                }

                // Corners on their places
                if (aPieces[4] == aPieces[2] || aPieces[4] == aPieces[9] || aPieces[4] == aPieces[44])
                {
                    if (aPieces[13] == aPieces[2] || aPieces[13] == aPieces[9] || aPieces[13] == aPieces[44])
                    {
                        goto Line1010;
                    }
                }

                if (aPieces[13] == aPieces[11] || aPieces[13] == aPieces[18] || aPieces[13] == aPieces[38])
                {
                    if (aPieces[22] == aPieces[11] || aPieces[22] == aPieces[18] || aPieces[22] == aPieces[38])
                    {
                        await MakeTurnAsync("y");
                        goto Line1010;
                    }
                }

                if (aPieces[22] == aPieces[20] || aPieces[22] == aPieces[27] || aPieces[22] == aPieces[36])
                {
                    if (aPieces[31] == aPieces[20] || aPieces[31] == aPieces[27] || aPieces[31] == aPieces[36])
                    {
                        await MakeTurnAsync("y2");
                        goto Line1010;
                    }
                }

                if (aPieces[4] == aPieces[0] || aPieces[4] == aPieces[29] || aPieces[4] == aPieces[42])
                {
                    if (aPieces[31] == aPieces[0] || aPieces[31] == aPieces[29] || aPieces[31] == aPieces[42])
                    {
                        await MakeTurnAsync("y'");
                        goto Line1010;
                    }
                }

                // If no corner is on its place
                await MakeTurnAsync("U R U' L' U R' U' L");
                continue;

            // If a corner is on its place
            Line1010:
                await MakeTurnAsync("U R U' L' U R' U' L");

                // If there is still one or more corners not in their place
                if (aPieces[4] == aPieces[11] || aPieces[4] == aPieces[18] || aPieces[4] == aPieces[38])
                {
                    if (aPieces[13] == aPieces[11] || aPieces[13] == aPieces[18] || aPieces[13] == aPieces[38])
                    {
                        goto Line1010;
                    }
                }

                if (aPieces[4] == aPieces[20] || aPieces[4] == aPieces[27] || aPieces[4] == aPieces[36])
                {
                    if (aPieces[13] == aPieces[20] || aPieces[13] == aPieces[27] || aPieces[13] == aPieces[36])
                    {
                        goto Line1010;
                    }
                }

                if (aPieces[4] == aPieces[0] || aPieces[4] == aPieces[29] || aPieces[4] == aPieces[42])
                {
                    if (aPieces[13] == aPieces[0] || aPieces[13] == aPieces[29] || aPieces[13] == aPieces[42])
                    {
                        goto Line1010;
                    }
                }

                if (SolveBottomLayerCheckCornersInRightPlace())
                {
                    return true;
                }
            }

            return true;
        }

        /// <summary>
        /// Solve the bottom layer - Corners on their places - Part 2
        /// </summary>
        /// <returns></returns>
        private static bool SolveBottomLayerCheckCornersInRightPlace()
        {
            // Check if the corners are in the right place
            // If all corners are on their places, break the loop
            bool bCorner36 = false;
            bool bCorner38 = false;
            bool bCorner42 = false;
            bool bCorner44 = false;

            if (aPieces[4] == aPieces[2] || aPieces[4] == aPieces[9] || aPieces[4] == aPieces[44])
            {
                if (aPieces[13] == aPieces[2] || aPieces[13] == aPieces[9] || aPieces[13] == aPieces[44])
                {
                    bCorner44 = true;
                }
            }

            if (aPieces[13] == aPieces[11] || aPieces[13] == aPieces[18] || aPieces[13] == aPieces[38])
            {
                if (aPieces[22] == aPieces[11] || aPieces[22] == aPieces[18] || aPieces[22] == aPieces[38])
                {
                    bCorner38 = true;
                }
            }

            if (aPieces[22] == aPieces[20] || aPieces[22] == aPieces[27] || aPieces[22] == aPieces[36])
            {
                if (aPieces[31] == aPieces[20] || aPieces[31] == aPieces[27] || aPieces[31] == aPieces[36])
                {
                    bCorner36 = true;
                }
            }

            if (aPieces[31] == aPieces[0] || aPieces[31] == aPieces[29] || aPieces[31] == aPieces[42])
            {
                if (aPieces[4] == aPieces[0] || aPieces[4] == aPieces[29] || aPieces[4] == aPieces[42])
                {
                    bCorner42 = true;
                }
            }

            return bCorner36 && bCorner38 && bCorner42 && bCorner44;
        }

        /// <summary>
        /// Solve the bottom layer - Tumbling the corners
        /// </summary>
        /// <returns></returns>
        private static async Task<bool> SolveBottomLayerTumblingCornersAsync()
        {
            int nLoopTimes = 0;

            await MakeTurnAsync("z2");

            while (true)
            {
                nLoopTimes++;
                if (nLoopTimes > nLoopTimesMax)
                {
                    Debug.WriteLine("Daisy: nLoopTimes bottom layer corners 2: " + nLoopTimes);
                    return false;
                }

                if (aPieces[4] != aPieces[8] || aPieces[4] != aPieces[15] || aPieces[4] != aPieces[47])
                {
                    if (aPieces[13] != aPieces[8] || aPieces[13] != aPieces[15] || aPieces[13] != aPieces[47])
                    {
                        // Do nothing
                    }
                }

                if (aPieces[13] != aPieces[17] || aPieces[13] != aPieces[24] || aPieces[13] != aPieces[53])
                {
                    if (aPieces[22] != aPieces[17] || aPieces[22] != aPieces[24] || aPieces[22] != aPieces[53])
                    {
                        await MakeTurnAsync("y");
                    }
                }

                if (aPieces[22] != aPieces[26] || aPieces[22] != aPieces[33] || aPieces[22] != aPieces[51])
                {
                    if (aPieces[31] != aPieces[26] || aPieces[31] != aPieces[33] || aPieces[31] != aPieces[51])
                    {
                        await MakeTurnAsync("y2");
                    }
                }

                if (aPieces[31] != aPieces[6] || aPieces[31] != aPieces[35] || aPieces[31] != aPieces[45])
                {
                    if (aPieces[4] != aPieces[6] || aPieces[4] != aPieces[35] || aPieces[4] != aPieces[45])
                    {
                        await MakeTurnAsync("y'");
                    }
                }

                while (true)
                {
                    while (true)
                    {
                        nLoopTimes++;
                        if (nLoopTimes > nLoopTimesMax)
                        {
                            Debug.WriteLine("Daisy: nLoopTimes bottom layer: " + nLoopTimes);
                            return false;
                        }

                        if (aPieces[4] == aPieces[8] && aPieces[13] == aPieces[15] || aPieces[49] == aPieces[47])
                        {
                            break;
                        }

                        await MakeTurnAsync("R U R' U'");
                    }

                    // Check if the cube is solved
                    if (ClassColorsCube.CheckIfSolved())
                    {
                        return true;
                    }

                    await MakeTurnAsync("D'");
                }
            }
        }

        /// <summary>
        /// Switch the edge cubes at the top layer and bottom layer - Part 1
        /// </summary>
        /// <returns></returns>
        private static async Task SwitchEdgeCubesTopLayerAsync()
        {
            string cB = aPieces[40];

            if (cB == aPieces[37] && cB == aPieces[39] && cB == aPieces[41] && cB == aPieces[43])
            {
                if (aPieces[4] == aPieces[28] && aPieces[31] == aPieces[1])
                {
                    await SwitchEdgeCubesTopLayer2Async();
                }

                if (aPieces[4] == aPieces[10] && aPieces[13] == aPieces[1])
                {
                    await MakeTurnAsync("y");
                    await SwitchEdgeCubesTopLayer2Async();
                }

                if (aPieces[13] == aPieces[19] && aPieces[22] == aPieces[10])
                {
                    await MakeTurnAsync("y2");
                    await SwitchEdgeCubesTopLayer2Async();
                }

                if (aPieces[22] == aPieces[28] && aPieces[31] == aPieces[19])
                {
                    await MakeTurnAsync("y'");
                    await SwitchEdgeCubesTopLayer2Async();
                }

                if (aPieces[13] == aPieces[28] && aPieces[31] == aPieces[10])
                {
                    await MakeTurnAsync("U");
                    await SwitchEdgeCubesTopLayer2Async();
                    await MakeTurnAsync("y2");
                    await SwitchEdgeCubesTopLayer2Async();
                }

                if (aPieces[4] == aPieces[19] && aPieces[22] == aPieces[1])
                {
                    await MakeTurnAsync("y U");
                    await SwitchEdgeCubesTopLayer2Async();
                    await MakeTurnAsync("y2");
                    await SwitchEdgeCubesTopLayer2Async();
                }
            }
        }

        /// <summary>
        /// Switch the edge cubes at the top layer - Part 2
        /// </summary>
        /// <returns></returns>
        private static async Task SwitchEdgeCubesTopLayer2Async()
        {
            await MakeTurnAsync("R U R' U R U2 R' U");
        }
    }
}
