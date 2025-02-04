/* This solution is based on the program 'SolCube' I wrote in 1981 in Microsoft Basic-80 for the Commodore PET 2001.
   The solution for solving the cube is based on a book by Don Taylor, Mastering Rubik's Cube, Dutch version 1981.
   It is not the most efficient solution, but it works most of the time.
   Basic-80 was a language with line numbers.
   I haven't been able to get rid of all the line numbers, so there are still some 'goto' statements left.
   The majority were replaced by tasks, while loops, continue and break statements. */

using System.Diagnostics;
using static CubeSolver.Globals;

namespace CubeSolver
{
    internal sealed class ClassSolveCubeBasic
    {
        //// Declare variables
        private const int nLoopTimesMax = 200;

        /// <summary>
        /// Solve the cube.  From Basic-80 to C# - 1981-1984
        /// </summary>
        /// <returns></returns>
        public static async Task<bool> SolveTheCubeBasicAsync()
        {
            if (!await SolveTopLayerCornersAsync())
            {
                return false;
            }

            if (!await SolveTopLayerEdgesAsync())
            {
                return false;
            }

            if (!await SolveMiddleLayerAsync())
            {
                return false;
            }

            if (!await SolveBottomLayerCornersAsync())
            {
                return false;
            }

            if (!await SolveBottomLayerEdgesAsync())
            {
                return false;
            }

            if (!await SolveBottomLayerTumblingCornersAsync())
            {
                return false;
            }

            if (!await SolveBottomLayerTumblingEdgesAsync())
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
        /// Solve the corners of the top layer - Chapter 6, page 16 
        /// </summary>
        /// <returns></returns>
        private static async Task<bool> SolveTopLayerCornersAsync()
        {
            bool bO, bP, bQ, bR;
            string cB;
            int nLoopTimes = 0;

            while (true)
            {
                nLoopTimes++;
                if (nLoopTimes > nLoopTimesMax)
                {
                    Debug.WriteLine("Basic: nLoopTimes top layer corners: " + nLoopTimes);
                    return false;
                }

                // 510
                cB = aPieces[40];
                bO = false;
                bP = false;
                bQ = false;
                bR = false;
                // 515
                if (cB == aPieces[36] && cB == aPieces[38] && cB == aPieces[42] && cB == aPieces[44])
                {
                    bO = true;
                }
                // 520
                if (aPieces[0] == aPieces[2])
                {
                    bP = true;
                }
                // 525
                if (aPieces[9] == aPieces[11])
                {
                    bQ = true;
                }
                // 530
                if (aPieces[18] == aPieces[20])
                {
                    bR = true;
                }
                // 535
                if (bO && bP && bQ && bR)
                {
                    break;
                }
                // 540
                bO = false;
                if (cB == aPieces[38] && cB == aPieces[42] && cB == aPieces[44])
                {
                    bO = true;
                }
                // 545
                if (bO && bP && bQ)
                {
                    await MakeTurnAsync("U2");
                    goto Line600;
                }
                // 550
                bO = false;
                if (cB == aPieces[38] && cB == aPieces[44])
                {
                    bO = true;
                }
                // 555
                if (bO && bQ)
                {
                    await MakeTurnAsync("U2");
                    goto Line600;
                }
                // 560
                bO = false;
                if (cB == aPieces[42] && cB == aPieces[44])
                {
                    bO = true;
                }
                // 565
                if (bO && bP)
                {
                    await MakeTurnAsync("U");
                    goto Line600;
                }
                // 570
                bO = false;
                if (cB == aPieces[36] && cB == aPieces[38])
                    bO = true;
                // 575
                if (bO && bR)
                {
                    await MakeTurnAsync("U'");
                    goto Line600;
                }
                // 580
                if (cB != aPieces[44])
                {
                    goto Line600;
                }
                // 585
                if (cB != aPieces[38])
                {
                    await MakeTurnAsync("U");
                    goto Line600;
                }
                // 590
                if (cB != aPieces[42])
                {
                    await MakeTurnAsync("U'");
                    goto Line600;
                }
                // 595
                if (cB != aPieces[36])
                {
                    await MakeTurnAsync("U2");
                }

            // 600
            Line600:
                if (cB == aPieces[8] || cB == aPieces[15] || cB == aPieces[47])
                {
                    goto Line680;
                }
                // 605
                if (cB == aPieces[17] || cB == aPieces[24] || cB == aPieces[53])
                {
                    await MakeTurnAsync("D'");
                    goto Line680;
                }
                // 610
                if (cB == aPieces[6] || cB == aPieces[35] || cB == aPieces[45])
                {
                    await MakeTurnAsync("D");
                    goto Line680;
                }
                // 615
                if (cB == aPieces[26] || cB == aPieces[33] || cB == aPieces[51])
                {
                    await MakeTurnAsync("D2");
                    goto Line680;
                }
                // 670
                await MakeTurnAsync("R2");
                continue;

            // 680
            Line680:
                if (cB == aPieces[8])
                {
                    await MakeTurnAsync("F D F'");
                    continue;
                }
                // 685
                if (cB == aPieces[15])
                {
                    await MakeTurnAsync("R' D' R");
                    continue;
                }
                // 690
                if (cB == aPieces[47])
                {
                    await MakeTurnAsync("R' D R D2 R' D' R");
                }
            }

            Debug.WriteLine("Basic: number of turns top layer corners: " + lCubeTurns.Count);
            return true;
        }

        /// <summary>
        /// Solve the edges of the top layer - Chapter 4, page 14-3 
        /// </summary>
        /// <returns></returns>
        private static async Task<bool> SolveTopLayerEdgesAsync()
        {
            bool bO, bP, bQ, bV, bX, bY, bZ;
            string cB, cX;
            int nLoopTimes = 0;

            while (true)
            {
                nLoopTimes++;
                if (nLoopTimes > nLoopTimesMax)
                {
                    Debug.WriteLine("Basic: nLoopTimes top layer edges: " + nLoopTimes);
                    return false;
                }

                // 710
                cB = aPieces[40];
                bV = false;
                bX = false;
                bY = false;
                bZ = false;

                if (cB == aPieces[43] && aPieces[0] == aPieces[1])
                {
                    bV = true;
                }
                // 720
                if (cB == aPieces[41] && aPieces[9] == aPieces[10])
                {
                    bX = true;
                }
                // 730
                if (cB == aPieces[37] && aPieces[18] == aPieces[19])
                {
                    bY = true;
                }
                // 740
                if (cB == aPieces[39] && aPieces[27] == aPieces[28])
                {
                    bZ = true;
                }
                // 750
                if (bV && bX && bY && bZ)
                {
                    break;
                }
                // 760
                bO = false;
                bP = false;
                bQ = false;

                if (cB == aPieces[5] || cB == aPieces[10] || cB == aPieces[12] || cB == aPieces[14])
                {
                    bO = true;
                }
                // 770
                if (cB == aPieces[21] || cB == aPieces[16] || cB == aPieces[50])
                {
                    bP = true;
                }
                // 780
                if (cB == aPieces[41])                  // 780 IF B = D(41) AND X <> D(10) THEN Q = 1
                {
                    bQ = true;
                }
                // 790
                if (bO || bP || bQ)
                {
                    goto Line810;
                }
                // 800
                await MakeTurnAsync("y");
                continue;

            // 810
            Line810:
                if (bV && bY && bZ)
                {
                    goto Line910;
                }
                // 820
                if (bY && bZ)
                {
                    await MakeTurnAsync("U'");
                    goto Line910;
                }
                // 830
                if (bY)
                {
                    await MakeTurnAsync("U2");
                    goto Line910;
                }
                // 840
                await MakeTurnAsync("U");

            // 910
            Line910:
                cX = aPieces[9];
                if (cB == aPieces[10] && cX == aPieces[41])
                {
                    await MakeTurnAsync("R' U' D B U' L U' D F'");
                    continue;
                }
                // 915
                if (cB == aPieces[5] && cX == aPieces[12])
                {
                    await MakeTurnAsync("U' B' U D' R");
                    continue;
                }
                // 920
                if (cB == aPieces[12] && cX == aPieces[5])
                {
                    await MakeTurnAsync("U2 L U' D F'");
                    continue;
                }
                // 925
                if (cB == aPieces[50] && cX == aPieces[16])
                {
                    await MakeTurnAsync("R' U2 D2 L U' F U' D R'");
                    continue;
                }
                // 930
                if (cB == aPieces[16] && cX == aPieces[50])
                {
                    await MakeTurnAsync("R' U' D B");
                    continue;
                }
                // 935
                if (cB == aPieces[21] && cX == aPieces[14])
                {
                    await MakeTurnAsync("U F U' D' R'");
                    continue;
                }
                // 940
                if (cB == aPieces[14] && cX == aPieces[21])
                {
                    await MakeTurnAsync("U2 L' U D' B");
                    continue;
                }
                // 945
                if (cB == aPieces[41] && cX != aPieces[10])
                {
                    await MakeTurnAsync("R' U' D B");
                    continue;
                }
                // 950
                if (cB == aPieces[10] && cX != aPieces[41])
                {
                    await MakeTurnAsync("R' U' D B");
                }
                // 960
            }

            Debug.WriteLine("Basic: number of turns top layer edges: " + lCubeTurns.Count);
            return true;
        }

        /// <summary>
        /// Solve the middle layer of the cube - Chapter 10, page 21 
        /// </summary>
        /// <returns></returns>
        private static async Task<bool> SolveMiddleLayerAsync()
        {
            bool bO, bP, bQ, bR, bS;
            string cO, cP, cQ, cR, cV, cX, cY, cZ;
            int nLoopTimes = 0;

            while (true)
            {
                nLoopTimes++;
                if (nLoopTimes > nLoopTimesMax)
                {
                    Debug.WriteLine("Basic: nLoopTimes middle layer: " + nLoopTimes);
                    return false;
                }

                // 1010
                cV = aPieces[4];
                cX = aPieces[13];
                cY = aPieces[22];
                cZ = aPieces[31];
                bO = false;
                bP = false;
                bQ = false;
                bR = false;
                bS = false;

                // 1015
                if (cV == aPieces[1] && cX == aPieces[10] && cY == aPieces[19])
                {
                    bO = true;
                }
                //1020
                if (cV == aPieces[3] && cV == aPieces[5])
                {
                    bP = true;
                }
                //1025
                if (cX == aPieces[12] && cX == aPieces[14])
                {
                    bQ = true;
                }
                //1030
                if (cY == aPieces[21] && cY == aPieces[23])
                {
                    bR = true;
                }
                //1035
                if (cZ == aPieces[30] && cZ == aPieces[32])
                {
                    bS = true;
                }
                //1040
                if (bO && bP && bQ && bR && bS)
                {
                    break;
                }
                //1050
                if (cV == aPieces[10])
                {
                    await MakeTurnAsync("U");
                    continue;
                }
                //1060
                if (cV == aPieces[28])
                {
                    await MakeTurnAsync("U'");
                    continue;
                }
                //1070
                if (cV == aPieces[19])
                {
                    await MakeTurnAsync("U2");
                    continue;
                }
                //1080
                if (bP && cX == aPieces[12] && cZ == aPieces[32])
                {
                    goto Line1460;
                }

                //1100
                cO = aPieces[7];
                cP = aPieces[16];
                cQ = aPieces[25];
                cR = aPieces[34];

                //1110
                if (cV == cO && cX == aPieces[46])
                {
                    goto Line1410;
                }
                //1120
                if (cV == cO && cZ == aPieces[46])
                {
                    goto Line1420;
                }
                //1130
                if (cV == cP && cX == aPieces[50])
                {
                    await MakeTurnAsync("D'");
                    goto Line1410;
                }
                //1140
                if (cV == cP && cZ == aPieces[50])
                {
                    await MakeTurnAsync("D'");
                    goto Line1420;
                }
                //1150
                if (cV == cQ && cX == aPieces[52])
                {
                    await MakeTurnAsync("D2");
                    goto Line1410;
                }
                //1160
                if (cV == cQ && cZ == aPieces[52])
                {
                    await MakeTurnAsync("D2");
                    goto Line1420;
                }
                //1170
                if (cV == cR && cX == aPieces[48])
                {
                    await MakeTurnAsync("D");
                    goto Line1410;
                }
                //1180
                if (cV == cR && cZ == aPieces[48])
                {
                    await MakeTurnAsync("D");
                    goto Line1420;
                }
                //1210
                if (cX == cO && cV == aPieces[46])
                {
                    goto Line1460;
                }
                //1215
                if (cX == cO && cY == aPieces[46])
                {
                    goto Line1460;
                }
                //1220
                if (cX == cP && cV == aPieces[50])
                {
                    goto Line1460;
                }
                //1225
                if (cX == cP && cY == aPieces[50])
                {
                    goto Line1460;
                }
                //1230
                if (cX == cQ && cV == aPieces[52])
                {
                    goto Line1460;
                }
                //1235
                if (cX == cQ && cY == aPieces[52])
                {
                    goto Line1460;
                }
                //1240
                if (cX == cR && cV == aPieces[48])
                {
                    goto Line1460;
                }
                //1245
                if (cX == cR && cY == aPieces[48])
                {
                    goto Line1460;
                }
                //1250
                if (cZ == cO && cV == aPieces[46])
                {
                    goto Line1470;
                }
                //1255
                if (cZ == cO && cY == aPieces[46])
                {
                    goto Line1470;
                }
                //1260
                if (cZ == cP && cV == aPieces[50])
                {
                    goto Line1470;
                }
                //1265
                if (cZ == cP && cY == aPieces[50])
                {
                    goto Line1470;
                }
                //1270
                if (cZ == cQ && cV == aPieces[52])
                {
                    goto Line1470;
                }
                //1275
                if (cZ == cQ && cY == aPieces[52])
                {
                    goto Line1470;
                }
                //1280
                if (cZ == cR && cV == aPieces[48])
                {
                    goto Line1470;
                }
                //1285
                if (cZ == cR && cY == aPieces[48])
                {
                    goto Line1470;
                }
                //1290
                if (cY == cO && cX == aPieces[46])
                {
                    goto Line1480;
                }
                //1295
                if (cY == cO && cZ == aPieces[46])
                {
                    goto Line1480;
                }
                //1300
                if (cY == cP && cX == aPieces[50])
                {
                    goto Line1480;
                }
                //1305
                if (cY == cP && cZ == aPieces[50])
                {
                    goto Line1480;
                }
                //1310
                if (cY == cQ && cX == aPieces[52])
                {
                    goto Line1480;
                }
                //1315
                if (cY == cQ && cZ == aPieces[52])
                {
                    goto Line1480;
                }
                //1320
                if (cY == cR && cX == aPieces[48])
                {
                    goto Line1480;
                }
                //1325
                if (cY == cR && cZ == aPieces[48])
                {
                    goto Line1480;
                }
                //1360
                if (cV != aPieces[5])
                {
                    goto Line1410;
                }
                //1370
                if (cV != aPieces[3])
                {
                    goto Line1420;
                }
                //1380
                await MakeTurnAsync("y");

            //1410
            Line1410:
                await MakeTurnAsync("D' R' D R D F D' F'");
                continue;

            //1420
            Line1420:
                await MakeTurnAsync("D L D' L' D' F' D F");
                continue;

            //1460
            Line1460:
                await MakeTurnAsync("y");
                continue;

            //1470
            Line1470:
                await MakeTurnAsync("y'");
                continue;

            //1480
            Line1480:
                await MakeTurnAsync("y2");
            }

            Debug.WriteLine("Basic: number of turns middle layer: " + lCubeTurns.Count);
            return true;
        }

        /// <summary>
        /// Solve the bottom layer of the cube - Corners on the right place
        /// </summary>
        /// <returns></returns>
        private static async Task<bool> SolveBottomLayerCornersAsync()
        {
            bool bO, bP, bQ, bR;
            string cV, cX, cZ;
            int nLoopTimes = 0;

            await MakeTurnAsync("z2");

            while (true)
            {
                nLoopTimes++;
                if (nLoopTimes > nLoopTimesMax)
                {
                    Debug.WriteLine("Basic: nLoopTimes bottom layer corners: " + nLoopTimes);
                    return false;
                }

                //1515
                cV = aPieces[4];
                cX = aPieces[13];
                //cY = aPieces[22];
                cZ = aPieces[31];
                //1520
                bO = false;
                bP = false;
                bQ = false;
                bR = false;
                if (cV == aPieces[2] || cV == aPieces[9] || cV == aPieces[44])
                {
                    bO = true;
                }
                //1525
                if (cX == aPieces[2] || cX == aPieces[9] || cX == aPieces[44])
                {
                    bP = true;
                }
                //1530
                if (cX == aPieces[11] || cX == aPieces[18] || cX == aPieces[38])
                {
                    bQ = true;
                }
                //1535
                if (cV == aPieces[0] || cV == aPieces[29] || cV == aPieces[42])
                {
                    bR = true;
                }
                //1540
                if (bO && bP && bQ && bR)
                {
                    break;
                }
                //1545
                if (bO && bP)
                {
                    goto Line1560;
                }
                //1550
                bO = false;
                if (cV == aPieces[11] || cV == aPieces[18] || cV == aPieces[38])
                {
                    bO = true;
                }
                //1552
                if (bO && bQ)
                {
                    await MakeTurnAsync("U");
                    continue;
                }
                //1554
                bO = false;
                if (cX == aPieces[0] || cX == aPieces[29] || cX == aPieces[42])
                {
                    bO = true;
                }
                //1556
                if (bO && bR)
                {
                    await MakeTurnAsync("U'");
                    continue;
                }
                //1558
                await MakeTurnAsync("U2");
                continue;

            //1560
            Line1560:
                bO = false;
                bP = false;
                if (cV == aPieces[11] || cV == aPieces[18] || cV == aPieces[38])
                {
                    bO = true;
                }
                //1565
                if (cX == aPieces[20] || cX == aPieces[27] || cX == aPieces[36])
                {
                    bP = true;
                }
                //1570
                if (bO && bP)
                {
                    await MakeTurnAsync("L' U R U' L U R' U'");
                    continue;
                }
                //1575
                bO = false;
                bP = false;
                if (cV == aPieces[20] || cV == aPieces[27] || cV == aPieces[36])
                {
                    bO = true;
                }
                //1580
                if (cX == aPieces[0] || cX == aPieces[29] || cX == aPieces[42])
                {
                    bP = true;
                }
                //1582
                if (bO && bP)
                {
                    await MakeTurnAsync("U R U' L' U R' U' L");
                    continue;
                }
                //1584
                bO = false;
                bP = false;
                if (cX == aPieces[20] || cX == aPieces[27] || cX == aPieces[36])
                {
                    bO = true;
                }
                //1586
                if (cZ == aPieces[11] || cZ == aPieces[18] || cZ == aPieces[38])
                {
                    bP = true;
                }
                //1588
                if (bO && bP)
                {
                    await MakeTurnAsync("F U' B' U F' U' B U2");
                    continue;
                }
                //1590
                bO = false;
                bP = false;
                if (cV == aPieces[11] || cV == aPieces[18] || cV == aPieces[38])
                {
                    bO = true;
                }
                //1592
                if (cX == aPieces[0] || cX == aPieces[29] || cX == aPieces[42])
                {
                    bP = true;
                }
                //1594
                if (bO && bP)
                {
                    await MakeTurnAsync("U F U R U' R' F'");
                    continue;
                }
                //1596
                await MakeTurnAsync("y");
            }

            Debug.WriteLine("Basic: number of turns bottom layer corners: " + lCubeTurns.Count);
            return true;
        }

        /// <summary>
        /// Solve the bottom layer of the cube - Edges on the right place
        /// </summary>
        /// <returns></returns>
        private static async Task<bool> SolveBottomLayerEdgesAsync()
        {
            bool bO, bP, bQ, bR;
            string cV, cX, cY, cZ;
            int nLoopTimes = 0;

            while (true)
            {
                nLoopTimes++;
                if (nLoopTimes > nLoopTimesMax)
                {
                    Debug.WriteLine("Basic: nLoopTimes bottom layer edges: " + nLoopTimes);
                    return false;
                }

                //1610
                cV = aPieces[4];
                cX = aPieces[13];
                cY = aPieces[22];
                cZ = aPieces[31];
                //1615
                bO = false;
                bP = false;
                bQ = false;
                bR = false;
                if (cV == aPieces[1] || cV == aPieces[43])
                {
                    bO = true;
                }
                //1620
                if (cX == aPieces[10] || cX == aPieces[41])
                {
                    bP = true;
                }
                //1625
                if (cY == aPieces[19] || cY == aPieces[37])
                {
                    bQ = true;
                }
                //1630
                if (cZ == aPieces[28] || cZ == aPieces[39])
                {
                    bR = true;
                }
                //1635
                if (bO && bP && bQ && bR)
                {
                    break;
                }
                //1640
                bO = false;
                bP = false;
                if (cV == aPieces[19] || cV == aPieces[37])
                {
                    bO = true;
                }
                //1645
                if (cY == aPieces[28] || cY == aPieces[39])
                {
                    bP = true;
                }
                //1650
                if (bO && bP)
                {
                    await MakeTurnAsync("L2 U F' B L2 F B' U L2");
                    continue;
                }
                //1655
                bO = false;
                bP = false;
                if (cV == aPieces[28] || cV == aPieces[39])
                {
                    bO = true;
                }
                //1660
                if (cY == aPieces[1] || cY == aPieces[43])
                {
                    bP = true;
                }
                //1665
                if (bO && bP)
                {
                    await MakeTurnAsync("L2 U' F' B L2 F B' U' L2");
                    continue;
                }
                //1670
                bO = false;
                bP = false;
                if (cV == aPieces[19] || cV == aPieces[37])
                {
                    bO = true;
                }
                //1675
                if (cY == aPieces[1] || cY == aPieces[43])
                {
                    bP = true;
                }
                //1680
                if (bO && bP)
                {
                    await MakeTurnAsync("R2 L2 D R2 L2 U2 R2 L2 D R2 L2");
                    continue;
                }
                //1685
                bO = false;
                bP = false;
                if (cV == aPieces[28] || cV == aPieces[39])
                {
                    bO = true;
                }
                //1690
                if (cX == aPieces[19] || cX == aPieces[37])
                {
                    bP = true;
                }
                //1692
                if (bO && bP)
                {
                    await MakeTurnAsync("R B U B' U' R2 F' U' F U R");
                }
                //1694
                await MakeTurnAsync("y");
                continue;
            }

            Debug.WriteLine("Basic: number of turns bottom layer edges: " + lCubeTurns.Count);
            return true;
        }

        /// <summary>
        /// Solve the bottom layer of the cube - Tumbling corners
        /// </summary>
        /// <returns></returns>
        private static async Task<bool> SolveBottomLayerTumblingCornersAsync()
        {
            string cB;
            int nLoopTimes = 0;

            while (true)
            {
                nLoopTimes++;
                if (nLoopTimes > nLoopTimesMax)
                {
                    Debug.WriteLine("Basic: nLoopTimes bottom layer tumbling corners: " + nLoopTimes);
                    return false;
                }

                //1710
                cB = aPieces[40];
                if (cB == aPieces[36] && cB == aPieces[38] && cB == aPieces[42] && cB == aPieces[44])
                {
                    break;
                }
                //1715
                if (cB == aPieces[2])
                {
                    goto Line1735;
                }
                //1720
                if (cB == aPieces[11])
                {
                    await MakeTurnAsync("U");
                    goto Line1735;
                }
                //1725
                if (cB == aPieces[29])
                {
                    await MakeTurnAsync("U'");
                    goto Line1735;
                }
                //1730
                if (cB == aPieces[20])
                {
                    await MakeTurnAsync("U2");
                }

            //1735
            Line1735:
                await MakeTurnAsync("R' D R F D F'");
                //1740
                if (cB == aPieces[9])
                {
                    goto Line1760;
                }
                //1745
                if (cB == aPieces[18])
                {
                    await MakeTurnAsync("U");
                    goto Line1760;
                }
                //1750
                if (cB == aPieces[0])
                {
                    await MakeTurnAsync("U'");
                    goto Line1760;
                }
                //1755
                await MakeTurnAsync("U2");

            //1760
            Line1760:
                await MakeTurnAsync("F D' F' R' D' R");
                //1765
                if (cB == aPieces[36] && cB == aPieces[38] && cB == aPieces[42] && cB == aPieces[44])
                {
                    break;
                }
                //1770
                await MakeTurnAsync("U");
            }

            Debug.WriteLine("Basic: number of turns bottom layer tumbling corners: " + lCubeTurns.Count);
            return true;
        }

        /// <summary>
        /// Solve the bottom layer of the cube - Tumbling edges 
        /// </summary>
        /// <returns></returns>
        private static async Task<bool> SolveBottomLayerTumblingEdgesAsync()
        {
            bool bO, bP;
            string cB, cV, cX, cY;
            int nLoopTimes = 0;

            while (true)
            {
                nLoopTimes++;
                if (nLoopTimes > nLoopTimesMax)
                {
                    Debug.WriteLine("Basic: nLoopTimes bottom layer tumbling edges: " + nLoopTimes);
                    return false;
                }

                //1810
                cB = aPieces[40];
                cV = aPieces[4];
                cX = aPieces[13];
                cY = aPieces[22];
                bO = false;
                bP = false;
                //1815
                if (cB == aPieces[37] && cB == aPieces[39] && cB == aPieces[41] && cB == aPieces[43])
                {
                    bO = true;
                }
                //1820
                if (cV == aPieces[1] && cX == aPieces[10] && cY == aPieces[19])
                {
                    bP = true;
                }
                //1825
                if (bO && bP)
                {
                    break;
                }
                //1830
                if (!bO)
                {
                    goto Line1850;
                }
                //1835
                if (cV == aPieces[10])
                {
                    await MakeTurnAsync("U");
                    continue;
                }
                //1840
                if (cV == aPieces[28])
                {
                    await MakeTurnAsync("U'");
                    continue;
                }
                //1845
                if (cV == aPieces[19])
                {
                    await MakeTurnAsync("U2");
                    continue;
                }

            //1850
            Line1850:
                if (cB != aPieces[43])
                    goto Line1890;
                //1860
                if (cB != aPieces[41])
                {
                    await MakeTurnAsync("U");
                    goto Line1890;
                }
                //1870
                if (cB != aPieces[39])
                {
                    await MakeTurnAsync("U'");
                    goto Line1890;
                }
                //1880
                if (cB != aPieces[37])
                    await MakeTurnAsync("U2");
                //1890
                Line1890:
                await MakeTurnAsync("F U D' L2 U2 D2 R");
                //1910
                if (cB != aPieces[41])
                {
                    goto Line1950;
                }
                //1920
                if (cB != aPieces[37])
                {
                    await MakeTurnAsync("U");
                    goto Line1950;
                }
                //1930
                if (cB != aPieces[43])
                {
                    await MakeTurnAsync("U'");
                    goto Line1950;
                }
                //1940
                if (cB != aPieces[39])
                {
                    await MakeTurnAsync("U2");
                }

            //1950
            Line1950:
                await MakeTurnAsync("R' D2 U2 L2 D U' F'");
            }

            Debug.WriteLine("Basic: number of turns bottom layer tumbling edges: " + lCubeTurns.Count);
            return true;
        }
    }
}
