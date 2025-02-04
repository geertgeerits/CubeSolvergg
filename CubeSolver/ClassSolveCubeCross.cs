/* This solution is based on:
   https://www.youtube.com/watch?v=7Ron6MN45LY&t=34s
   https://ruwix.com/the-rubiks-cube/how-to-solve-the-rubiks-cube-beginners-method/#step1
   https://www.learnhowtosolvearubikscube.com/step-7-moving-the-corners-into-place
   https://www.youtube.com/@RichardSchouw
   https://www.bing.com/videos/riverview/relatedvideo?q=how+to+move+corners+on+rubik%27s&mid=D8975C707A0A2C50FCEFD8975C707A0A2C50FCEF&FORM=VIRE
   file:///C:/Sources/MAUI/CubeSolver/Miscellaneous/Manuals/RubiksCubeBeginnerInstructions.pdf
   https://www.youtube.com/watch?v=Lm9jRkikhlI
   https://www.youtube.com/watch?v=lgm7NuQGgtw&list=PLfZ_bKS9WEOA-woYuj-_y3EmQqzhRboNw&index=57&t=706s */

using System.Diagnostics;
using static CubeSolver.Globals;

namespace CubeSolver
{
    internal sealed class ClassSolveCubeCross
    {
        //// Declare variables
        private const int nLoopTimesMax = 200;

        /// <summary>
        /// Solve the cube 
        /// </summary>
        /// <returns></returns>
        public static async Task<bool> SolveTheCubeCrossAsync()
        {
            if (!await SolveTopLayerEdgesAsync())
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
        /// Solve the edges of the top layer - Part 1
        /// </summary>
        /// <returns></returns>
        private static async Task<bool> SolveTopLayerEdgesAsync()
        {
            string cB = aPieces[40];
            int nLoopTimes = 0;

            while (true)
            {
                nLoopTimes++;
                if (nLoopTimes > nLoopTimesMax)
                {
                    Debug.WriteLine("Cross: nLoopTimes top layer edges: " + nLoopTimes);
                    return false;
                }

                // If solved, break the loop
                if (cB == aPieces[37] && cB == aPieces[39] && cB == aPieces[41] && cB == aPieces[43])
                {
                    if (aPieces[1] == aPieces[4] && aPieces[10] == aPieces[13] && aPieces[19] == aPieces[22] && aPieces[28] == aPieces[31])
                    {
                        break;
                    }
                    else
                    {
                        await SwitchEdgeCubesTopLayerAsync();
                    }
                }

                // Top color is at the down face and the second color is at another face of the bottom layer
                if (cB == aPieces[46])
                {
                    if (aPieces[7] == aPieces[4])
                    {
                        await MakeTurnAsync("F2");
                    }

                    if (aPieces[7] == aPieces[13])
                    {
                        await MakeTurnAsync("D R2");
                    }

                    if (aPieces[7] == aPieces[22])
                    {
                        await MakeTurnAsync("D2 B2");
                    }

                    if (aPieces[7] == aPieces[31])
                    {
                        await MakeTurnAsync("D' L2");
                    }
                }

                if (cB == aPieces[48])
                {
                    if (aPieces[34] == aPieces[31])
                    {
                        await MakeTurnAsync("L2");
                    }

                    if (aPieces[34] == aPieces[4])
                    {
                        await MakeTurnAsync("D F2");
                    }

                    if (aPieces[34] == aPieces[13])
                    {
                        await MakeTurnAsync("D2 R2");
                    }

                    if (aPieces[34] == aPieces[22])
                    {
                        await MakeTurnAsync("D' B2");
                    }
                }

                if (cB == aPieces[50])
                {
                    if (aPieces[16] == aPieces[13])
                    {
                        await MakeTurnAsync("R2");
                    }

                    if (aPieces[16] == aPieces[4])
                    {
                        await MakeTurnAsync("D' F2");
                    }

                    if (aPieces[16] == aPieces[22])
                    {
                        await MakeTurnAsync("D B2");
                    }

                    if (aPieces[16] == aPieces[31])
                    {
                        await MakeTurnAsync("D2 L2");
                    }
                }

                if (cB == aPieces[52])
                {
                    if (aPieces[25] == aPieces[22])
                    {
                        await MakeTurnAsync("B2");
                    }

                    if (aPieces[25] == aPieces[4])
                    {
                        await MakeTurnAsync("D2 F2");
                    }

                    if (aPieces[25] == aPieces[13])
                    {
                        await MakeTurnAsync("D' R2");
                    }

                    if (aPieces[25] == aPieces[31])
                    {
                        await MakeTurnAsync("D L2");
                    }
                }

                // Top color is at the bottom - front / right / back / left face and the second color is at the down face
                if (cB == aPieces[7])
                {
                    await SolveTopLayerEdges2Async();
                }

                if (cB == aPieces[16])
                {
                    await MakeTurnAsync("y");
                    await SolveTopLayerEdges2Async();
                }

                if (cB == aPieces[25])
                {
                    await MakeTurnAsync("y2");
                    await SolveTopLayerEdges2Async();
                }

                if (cB == aPieces[34])
                {
                    await MakeTurnAsync("y'");
                    await SolveTopLayerEdges2Async();
                }

                // Top color is at the middle face - turn it to the down face
                if (cB == aPieces[3])
                {
                    await MakeTurnAsync("L D' L'");
                }

                if (cB == aPieces[5])
                {
                    await MakeTurnAsync("R' D' R");
                }

                if (cB == aPieces[12])
                {
                    await MakeTurnAsync("F D' F'");
                }

                if (cB == aPieces[14])
                {
                    await MakeTurnAsync("B' D B");
                }

                if (cB == aPieces[21])
                {
                    await MakeTurnAsync("R D' R'");
                }

                if (cB == aPieces[23])
                {
                    await MakeTurnAsync("L' D L");
                }

                if (cB == aPieces[30])
                {
                    await MakeTurnAsync("B D' B'");
                }

                if (cB == aPieces[32])
                {
                    await MakeTurnAsync("F' D F");
                }
            }

            return true;
        }

        /// <summary>
        /// Solve the edges of the top layer - Part 2
        /// </summary>
        /// <returns></returns>
        private static async Task SolveTopLayerEdges2Async()
        {
            if (aPieces[46] == aPieces[13])
            {
                await MakeTurnAsync("D y");
            }

            if (aPieces[46] == aPieces[31])
            {
                await MakeTurnAsync("D' y'");
            }

            if (aPieces[46] == aPieces[22])
            {
                await MakeTurnAsync("D2 y2");
            }

            await MakeTurnAsync("F2 F U' R U");
        }

        /// <summary>
        /// Solve the corners of the top layer
        /// </summary>
        /// <returns></returns>
        private static async Task<bool> SolveTopLayerCornersAsync()
        {
            // Check if the center of the top layer is the same of the middle layer
            if (aPieces[1] == aPieces[13])
            {
                await MakeTurnAsync("U'");
            }

            if (aPieces[1] == aPieces[31])
            {
                await MakeTurnAsync("U");
            }

            if (aPieces[1] == aPieces[22])
            {
                await MakeTurnAsync("U2");
            }

            // Turn the cube to the right, front stays at the front
            await MakeTurnAsync("z2");

            string cB = aPieces[49];
            int nLoopTimes = 0;

            while (true)
            {
                nLoopTimes++;
                if (nLoopTimes > nLoopTimesMax)
                {
                    Debug.WriteLine("Cross: nLoopTimes top layer corners: " + nLoopTimes);
                    return false;
                }

                // If solved, break the loop
                if (cB == aPieces[45] && cB == aPieces[46] && cB == aPieces[47] && cB == aPieces[48] && cB == aPieces[50] && cB == aPieces[51] && cB == aPieces[52] && cB == aPieces[53])
                {
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
                                    Debug.WriteLine("Cross: nLoopTimes top layer corners: " + nLoopTimes);
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
                                    Debug.WriteLine("Cross: nLoopTimes top layer corners: " + nLoopTimes);
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
                                    Debug.WriteLine("Cross: nLoopTimes top layer corners: " + nLoopTimes);
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
                                    Debug.WriteLine("Cross: nLoopTimes top layer corners: " + nLoopTimes);
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
                                    Debug.WriteLine("Cross: nLoopTimes top layer corners: " + nLoopTimes);
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
                                    Debug.WriteLine("Cross: nLoopTimes top layer corners: " + nLoopTimes);
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
        /// Solve the middle layer - Part 1
        /// </summary>
        /// <returns></returns>
        private static async Task<bool> SolveMiddleLayerAsync()
        {
            string cB = aPieces[40];
            int nLoopTimes = 0;

            while (true)
            {
                nLoopTimes++;
                if (nLoopTimes > nLoopTimesMax)
                {
                    Debug.WriteLine("Cross: nLoopTimes middle layer: " + nLoopTimes);
                    return false;
                }

                // If solved, break the loop
                if (aPieces[4] == aPieces[3] && aPieces[4] == aPieces[5] && aPieces[13] == aPieces[12] && aPieces[13] == aPieces[14] && aPieces[22] == aPieces[21] && aPieces[22] == aPieces[23] && aPieces[31] == aPieces[30] && aPieces[31] == aPieces[32])
                {
                    break;
                }

                // If an edge cube at the top layer does not have the color of the up face center cube
                if (aPieces[1] == aPieces[4] && cB != aPieces[43])
                {
                    await SolveMiddleLayer2Async();
                }

                if (aPieces[1] == aPieces[13] && cB != aPieces[43])
                {
                    await MakeTurnAsync("U' y");
                    await SolveMiddleLayer2Async();
                }

                if (aPieces[1] == aPieces[22] && cB != aPieces[43])
                {
                    await MakeTurnAsync("U2 y2");
                    await SolveMiddleLayer2Async();
                }

                if (aPieces[1] == aPieces[31] && cB != aPieces[43])
                {
                    await MakeTurnAsync("U y'");
                    await SolveMiddleLayer2Async();
                }

                if (aPieces[10] == aPieces[4] && cB != aPieces[41])
                {
                    await MakeTurnAsync("U");
                    await SolveMiddleLayer2Async();
                }

                if (aPieces[10] == aPieces[13] && cB != aPieces[41])
                {
                    await MakeTurnAsync("y");
                    await SolveMiddleLayer2Async();
                }

                if (aPieces[10] == aPieces[22] && cB != aPieces[41])
                {
                    await MakeTurnAsync("U' y2");
                    await SolveMiddleLayer2Async();
                }

                if (aPieces[10] == aPieces[31] && cB != aPieces[41])
                {
                    await MakeTurnAsync("U2 y'");
                    await SolveMiddleLayer2Async();
                }

                if (aPieces[19] == aPieces[4] && cB != aPieces[37])
                {
                    await MakeTurnAsync("U2");
                    await SolveMiddleLayer2Async();
                }

                if (aPieces[19] == aPieces[13] && cB != aPieces[37])
                {
                    await MakeTurnAsync("U y");
                    await SolveMiddleLayer2Async();
                }

                if (aPieces[19] == aPieces[22] && cB != aPieces[37])
                {
                    await MakeTurnAsync("y2");
                    await SolveMiddleLayer2Async();
                }

                if (aPieces[19] == aPieces[31] && cB != aPieces[37])
                {
                    await MakeTurnAsync("U' y'");
                    await SolveMiddleLayer2Async();
                }

                if (aPieces[28] == aPieces[4] && cB != aPieces[39])
                {
                    await MakeTurnAsync("U'");
                    await SolveMiddleLayer2Async();
                }

                if (aPieces[28] == aPieces[13] && cB != aPieces[39])
                {
                    await MakeTurnAsync("U2 y");
                    await SolveMiddleLayer2Async();
                }

                if (aPieces[28] == aPieces[22] && cB != aPieces[39])
                {
                    await MakeTurnAsync("U y2");
                    await SolveMiddleLayer2Async();
                }

                if (aPieces[28] == aPieces[31] && cB != aPieces[39])
                {
                    await MakeTurnAsync("y'");
                    await SolveMiddleLayer2Async();
                }

                // Wrong orientation of the edge cubes at the middle layer
                if (aPieces[4] == aPieces[12] && aPieces[13] == aPieces[5])
                {
                    await SolveMiddleLayer3Async();
                }

                if (aPieces[13] == aPieces[21] && aPieces[22] == aPieces[14])
                {
                    await MakeTurnAsync("y");
                    await SolveMiddleLayer3Async();
                }

                if (aPieces[22] == aPieces[30] && aPieces[31] == aPieces[23])
                {
                    await MakeTurnAsync("y2");
                    await SolveMiddleLayer3Async();
                }

                if (aPieces[31] == aPieces[3] && aPieces[4] == aPieces[32])
                {
                    await MakeTurnAsync("y'");
                    await SolveMiddleLayer3Async();
                }

                // If an edge cube at the top layer must switch with a cube at the middle layer - Turn the cube
                if (aPieces[13] == aPieces[10] && aPieces[22] == aPieces[41])
                {
                    await MakeTurnAsync("y");
                }

                if (aPieces[22] == aPieces[19] && aPieces[31] == aPieces[37])
                {
                    await MakeTurnAsync("y2");
                }

                if (aPieces[31] == aPieces[28] && aPieces[4] == aPieces[39])
                {
                    await MakeTurnAsync("y2");
                }

                // If an edge cube at the top layer must switch with a cube at the middle layer - Right algorithm
                if (aPieces[4] == aPieces[1] && aPieces[13] == aPieces[43])
                {
                    await SolveMiddleLayer4RightAsync();
                }

                if (aPieces[4] == aPieces[10] && aPieces[13] == aPieces[41])
                {
                    await MakeTurnAsync("U");
                    await SolveMiddleLayer4RightAsync();
                }

                if (aPieces[4] == aPieces[19] && aPieces[13] == aPieces[37])
                {
                    await MakeTurnAsync("U2");
                    await SolveMiddleLayer4RightAsync();
                }

                if (aPieces[4] == aPieces[28] && aPieces[13] == aPieces[39])
                {
                    await MakeTurnAsync("U'");
                    await SolveMiddleLayer4RightAsync();
                }

                // If an edge cube at the top layer must switch with a cube at the middle layer - Left algorithm
                if (aPieces[4] == aPieces[1] && aPieces[31] == aPieces[43])
                {
                    await SolveMiddleLayer4LeftAsync();
                }

                if (aPieces[4] == aPieces[10] && aPieces[31] == aPieces[41])
                {
                    await MakeTurnAsync("U");
                    await SolveMiddleLayer4LeftAsync();
                }

                if (aPieces[4] == aPieces[19] && aPieces[31] == aPieces[37])
                {
                    await MakeTurnAsync("U2");
                    await SolveMiddleLayer4LeftAsync();
                }

                if (aPieces[4] == aPieces[28] && aPieces[31] == aPieces[39])
                {
                    await MakeTurnAsync("U'");
                    await SolveMiddleLayer4LeftAsync();
                }
            }

            return true;
        }

        /// <summary>
        /// Solve the middle layer - Part 2
        /// <para>If an edge cube at the top layer does not have the color of the up face center cube</para>
        /// </summary>
        /// <returns></returns>
        private static async Task SolveMiddleLayer2Async()
        {
            if (aPieces[43] == aPieces[13])
            {
                await MakeTurnAsync("U R U R' U' y L' U' L U");
            }

            if (aPieces[43] == aPieces[28])
            {
                await MakeTurnAsync("U' L' U' L U y' R U R' U'");
            }
        }

        /// <summary>
        /// Solve the middle layer - Part 3 - Wrong orientation of the edge cubes at the middle layer
        /// </summary>
        /// <returns></returns>
        private static async Task SolveMiddleLayer3Async()
        {
            await MakeTurnAsync("U R U' R' U' F' U F U2 U R U' R' U' F' U F");
        }

        /// <summary>
        /// Solve the middle layer - Part 4
        /// <para>If an edge cube at the top layer must switch with a cube at the middle layer - Right algorithm</para>
        /// </summary>
        /// <returns></returns>
        private static async Task SolveMiddleLayer4RightAsync()
        {
            await MakeTurnAsync("U R U' R' U' F' U F");
        }

        /// <summary>
        /// Solve the middle layer - Part 4
        /// <para>If an edge cube at the top layer must switch with a cube at the middle layer - Left algorithm</para>
        /// </summary>
        /// <returns></returns>
        private static async Task SolveMiddleLayer4LeftAsync()
        {
            await MakeTurnAsync("U' L' U L U F U' F'");
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
                    Debug.WriteLine("Cross: nLoopTimes bottom layer edges: " + nLoopTimes);
                    return false;
                }

                // If solved, break the loop
                if (cB == aPieces[37] && cB == aPieces[39] && cB == aPieces[41] && cB == aPieces[43])
                {
                    break;
                }

                if (cB != aPieces[1] && cB == aPieces[10])
                {
                    await MakeTurnAsync("U");
                }

                if (cB != aPieces[1] && cB == aPieces[19])
                {
                    await MakeTurnAsync("U2");
                }

                if (cB != aPieces[1] && cB == aPieces[28])
                {
                    await MakeTurnAsync("U'");
                }

                if (cB == aPieces[1])
                {
                    await MakeTurnAsync("F R U R' U' F'");
                }

                if (cB == aPieces[41] && cB == aPieces[43])
                {
                    await MakeTurnAsync("U2");
                }

                if (cB == aPieces[37] && cB == aPieces[41])
                {
                    await MakeTurnAsync("U'");
                }

                if (cB == aPieces[39] && cB == aPieces[43])
                {
                    await MakeTurnAsync("U");
                }

                if (cB == aPieces[37] && cB == aPieces[39])
                {
                    await MakeTurnAsync("F R U R' U' F'");
                }

                if (cB == aPieces[37] && cB == aPieces[43])
                {
                    await MakeTurnAsync("U");
                }

                if (cB == aPieces[39] && cB == aPieces[41])
                {
                    await MakeTurnAsync("F R U R' U' F'");
                }

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
                    Debug.WriteLine("Cross: nLoopTimes bottom layer edges 2: " + nLoopTimes);
                    return false;
                }

                // If solved, break the loop
                if (cB == aPieces[37] && cB == aPieces[39] && cB == aPieces[41] && cB == aPieces[43])
                {
                    if (aPieces[1] == aPieces[4] && aPieces[10] == aPieces[13] && aPieces[19] == aPieces[22] && aPieces[28] == aPieces[31])
                    {
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
        /// Solve the bottom layer - Corners on their places
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
                    Debug.WriteLine("Cross: nLoopTimes bottom layer corners: " + nLoopTimes);
                    return false;
                }

                // If all corners are on their places, break the loop
                if (SolveBottomLayerCheckCornersInRightPlace())
                {
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

                continue;
            }

            return true;
        }

        /// <summary>
        /// Corners on their places - Part 2
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
                    Debug.WriteLine("Cross: nLoopTimes bottom layer corners 2: " + nLoopTimes);
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
                    nLoopTimes++;
                    if (nLoopTimes > nLoopTimesMax)
                    {
                        Debug.WriteLine("Cross: nLoopTimes bottom layer corners 2: " + nLoopTimes);
                        return false;
                    }

                    while (true)
                    {
                        nLoopTimes++;
                        if (nLoopTimes > nLoopTimesMax)
                        {
                            Debug.WriteLine("Cross: nLoopTimes bottom layer corners 2: " + nLoopTimes);
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
        /// Switch the edge cubes at the top layer - Part 1
        /// </summary>
        /// <returns></returns>
        private static async Task SwitchEdgeCubesTopLayerAsync()
        {
            string cB = aPieces[40];

            //if (cB != aPieces[37] && cB != aPieces[39] && cB != aPieces[41] && cB != aPieces[43])
            //{
            //    await MakeTurnAsync("F R U R' U' F'");
            //}

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
