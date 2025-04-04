﻿namespace CubeSolver
{
    //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    //Cube on the cubie level
    public class CubieCube
    {
        // initialize to Id-Cube

        // corner permutation
        public Corner[] cp = [Corner.URF, Corner.UFL, Corner.ULB, Corner.UBR, Corner.DFR, Corner.DLF, Corner.DBL, Corner.DRB];

        // corner orientation
        public byte[] co = [0, 0, 0, 0, 0, 0, 0, 0];

        // edge permutation
        public Edge[] ep = [Edge.UR, Edge.UF, Edge.UL, Edge.UB, Edge.DR, Edge.DF, Edge.DL, Edge.DB, Edge.FR, Edge.FL, Edge.BL, Edge.BR];

        // edge orientation
        public byte[] eo = [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0];

        // ************************************** Moves on the cubie level ***************************************************

        private static readonly Corner[] cpU = [Corner.UBR, Corner.URF, Corner.UFL, Corner.ULB, Corner.DFR, Corner.DLF, Corner.DBL, Corner.DRB];
        private static readonly byte[] coU = [0, 0, 0, 0, 0, 0, 0, 0];
        private static readonly Edge[] epU = [Edge.UB, Edge.UR, Edge.UF, Edge.UL, Edge.DR, Edge.DF, Edge.DL, Edge.DB, Edge.FR, Edge.FL, Edge.BL, Edge.BR];
        private static readonly byte[] eoU = [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0];

        private static readonly Corner[] cpR = [Corner.DFR, Corner.UFL, Corner.ULB, Corner.URF, Corner.DRB, Corner.DLF, Corner.DBL, Corner.UBR];
        private static readonly byte[] coR = [2, 0, 0, 1, 1, 0, 0, 2];
        private static readonly Edge[] epR = [Edge.FR, Edge.UF, Edge.UL, Edge.UB, Edge.BR, Edge.DF, Edge.DL, Edge.DB, Edge.DR, Edge.FL, Edge.BL, Edge.UR];
        private static readonly byte[] eoR = [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0];

        private static readonly Corner[] cpF = [Corner.UFL, Corner.DLF, Corner.ULB, Corner.UBR, Corner.URF, Corner.DFR, Corner.DBL, Corner.DRB];
        private static readonly byte[] coF = [1, 2, 0, 0, 2, 1, 0, 0];
        private static readonly Edge[] epF = [Edge.UR, Edge.FL, Edge.UL, Edge.UB, Edge.DR, Edge.FR, Edge.DL, Edge.DB, Edge.UF, Edge.DF, Edge.BL, Edge.BR];
        private static readonly byte[] eoF = [0, 1, 0, 0, 0, 1, 0, 0, 1, 1, 0, 0];

        private static readonly Corner[] cpD = [Corner.URF, Corner.UFL, Corner.ULB, Corner.UBR, Corner.DLF, Corner.DBL, Corner.DRB, Corner.DFR];
        private static readonly byte[] coD = [0, 0, 0, 0, 0, 0, 0, 0];
        private static readonly Edge[] epD = [Edge.UR, Edge.UF, Edge.UL, Edge.UB, Edge.DF, Edge.DL, Edge.DB, Edge.DR, Edge.FR, Edge.FL, Edge.BL, Edge.BR];
        private static readonly byte[] eoD = [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0];

        private static readonly Corner[] cpL = [Corner.URF, Corner.ULB, Corner.DBL, Corner.UBR, Corner.DFR, Corner.UFL, Corner.DLF, Corner.DRB];
        private static readonly byte[] coL = [0, 1, 2, 0, 0, 2, 1, 0];
        private static readonly Edge[] epL = [Edge.UR, Edge.UF, Edge.BL, Edge.UB, Edge.DR, Edge.DF, Edge.FL, Edge.DB, Edge.FR, Edge.UL, Edge.DL, Edge.BR];
        private static readonly byte[] eoL = [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0];

        private static readonly Corner[] cpB = [Corner.URF, Corner.UFL, Corner.UBR, Corner.DRB, Corner.DFR, Corner.DLF, Corner.ULB, Corner.DBL];
        private static readonly byte[] coB = [0, 0, 1, 2, 0, 0, 2, 1];
        private static readonly Edge[] epB = [Edge.UR, Edge.UF, Edge.UL, Edge.BR, Edge.DR, Edge.DF, Edge.DL, Edge.BL, Edge.FR, Edge.FL, Edge.UB, Edge.DB];
        private static readonly byte[] eoB = [0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 1, 1];

        static CubieCube SetMoveU()
        {
            CubieCube move = new()
            {
                cp = cpU,
                co = coU,
                ep = epU,
                eo = eoU
            };
            return move;
        }

        static CubieCube SetMoveR()
        {
            CubieCube move = new()
            {
                cp = cpR,
                co = coR,
                ep = epR,
                eo = eoR
            };
            return move;
        }

        static CubieCube SetMoveF()
        {
            CubieCube move = new()
            {
                cp = cpF,
                co = coF,
                ep = epF,
                eo = eoF
            };
            return move;
        }

        static CubieCube SetMoveD()
        {
            CubieCube move = new()
            {
                cp = cpD,
                co = coD,
                ep = epD,
                eo = eoD
            };
            return move;
        }

        static CubieCube SetMoveL()
        {
            CubieCube move = new()
            {
                cp = cpL,
                co = coL,
                ep = epL,
                eo = eoL
            };
            return move;
        }

        static CubieCube SetMoveB()
        {
            CubieCube move = new()
            {
                cp = cpB,
                co = coB,
                ep = epB,
                eo = eoB
            };
            return move;
        }


        // this CubieCube array represents the 6 basic cube moves
        //public static CubieCube[] moveCube = [SetMoveU(), SetMoveR(), SetMoveF(), SetMoveD(), SetMoveL(), SetMoveB(),];
        public static readonly CubieCube[] moveCube = [SetMoveU(), SetMoveR(), SetMoveF(), SetMoveD(), SetMoveL(), SetMoveB(),];

        public CubieCube()
        {

        }

        // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        public CubieCube(Corner[] cp, byte[] co, Edge[] ep, byte[] eo)
        {
         
            for (int i = 0; i < 8; i++)
            {
                this.cp[i] = cp[i];
                this.co[i] = co[i];
            }
            for (int i = 0; i < 12; i++)
            {
                this.ep[i] = ep[i];
                this.eo[i] = eo[i];
            }
        }

        // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        // n choose k
        static int Cnk(int n, int k)
        {
            int i, j, s;
            if (n < k)
                return 0;
            if (k > n / 2)
                k = n - k;
            for (s = 1, i = n, j = 1; i != n - k; i--, j++)
            {
                s *= i;
                s /= j;
            }
            return s;
        }

        // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        static void RotateLeft(Corner[] arr, int l, int r)
        // Left rotation of all array elements between l and r
        {
            Corner temp = arr[l];
            for (int i = l; i < r; i++)
                arr[i] = arr[i + 1];
            arr[r] = temp;
        }

        // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        static void RotateRight(Corner[] arr, int l, int r)
        // Right rotation of all array elements between l and r
        {
            Corner temp = arr[r];
            for (int i = r; i > l; i--)
                arr[i] = arr[i - 1];
            arr[l] = temp;
        }

        // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        static void RotateLeft(Edge[] arr, int l, int r)
        // Left rotation of all array elements between l and r
        {
            Edge temp = arr[l];
            for (int i = l; i < r; i++)
                arr[i] = arr[i + 1];
            arr[r] = temp;
        }

        // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        static void RotateRight(Edge[] arr, int l, int r)
        // Right rotation of all array elements between l and r
        {
            Edge temp = arr[r];
            for (int i = r; i > l; i--)
                arr[i] = arr[i - 1];
            arr[l] = temp;
        }

        // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        // return cube in facelet representation
        public FaceCube ToFaceCube()
        {
            FaceCube fcRet = new();

            foreach (Corner c in Enum.GetValues<Corner>())
            {
                int i = (int)c;
                int j = (int)cp[i];// cornercubie with index j is at
                                        // cornerposition with index i
                byte ori = co[i];// Orientation of this cubie
                for (int n = 0; n < 3; n++)
                    fcRet.f[(int)FaceCube.cornerFacelet[i][(n + ori) % 3]] = FaceCube.cornerColor[j][n];
            }

            foreach (Edge e in Enum.GetValues<Edge>())
            {
                int i = (int)e;
                int j = (int)ep[i];// edgecubie with index j is at edgeposition
                                        // with index i
                byte ori = eo[i];// Orientation of this cubie
                for (int n = 0; n < 2; n++)
                    fcRet.f[(int)FaceCube.edgeFacelet[i][(n + ori) % 2]] = FaceCube.edgeColor[j][n];
            }


            return fcRet;
        }

        // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        // Multiply this CubieCube with another cubiecube b, restricted to the corners.
        // Because we also describe reflections of the whole cube by permutations, we get a complication with the corners. The
        // orientations of mirrored corners are described by the numbers 3, 4 and 5. The composition of the orientations
        // cannot
        // be computed by addition modulo three in the cyclic group C3 any more. Instead the rules below give an addition in
        // the dihedral group D3 with 6 elements.
        //	 
        // NOTE: Because we do not use symmetry reductions and hence no mirrored cubes in this simple implementation of the
        // Two-Phase-Algorithm, some code is not necessary here.
        //	
        public void CornerMultiply(CubieCube b)
        {
            Corner[] cPerm = new Corner[8];
            byte[] cOri = new byte[8];
            foreach (Corner corn in Enum.GetValues<Corner>())
                {
                cPerm[(int)corn] = cp[(int)b.cp[(int)corn]];

                byte oriA = co[(int)b.cp[(int)corn]];
                byte oriB = b.co[(int)corn];
                byte ori = 0;
                ;
                if (oriA < 3 && oriB < 3) // if both cubes are regular cubes...
                {
                    ori = (byte)(oriA + oriB); // just do an addition modulo 3 here
                    if (ori >= 3)
                        ori -= 3; // the composition is a regular cube

                    // +++++++++++++++++++++not used in this implementation +++++++++++++++++++++++++++++++++++
                }
                else if (oriA < 3 && oriB >= 3) // if cube b is in a mirrored
                                                // state...
                {
                    ori = (byte)(oriA + oriB);
                    if (ori >= 6)
                        ori -= 3; // the composition is a mirrored cube
                }
                else if (oriA >= 3 && oriB < 3) // if cube a is an a mirrored
                                                // state...
                {
                    ori = (byte)(oriA - oriB);
                    if (ori < 3)
                        ori += 3; // the composition is a mirrored cube
                }
                else if (oriA >= 3 && oriB >= 3) // if both cubes are in mirrored
                                                 // states...
                {
                    ori = (byte)(oriA - oriB);
                    if (ori < 0)
                        ori += 3; // the composition is a regular cube
                                  // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                }
                cOri[(int)corn] = ori;
            }
 
            foreach (Corner c in Enum.GetValues<Corner>())
            {
                cp[(int)c] = cPerm[(int)c];
                co[(int)c] = cOri[(int)c];
            }
        }

        // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        // Multiply this CubieCube with another cubiecube b, restricted to the edges.
        public void EdgeMultiply(CubieCube b)
        {
            Edge[] ePerm = new Edge[12];
            byte[] eOri = new byte[12];
            foreach (Edge edge in Enum.GetValues<Edge>())
            {
                ePerm[(int)edge] = ep[(int)b.ep[(int)edge]];
                eOri[(int)edge] = (byte)((b.eo[(int)edge] + eo[(int)b.ep[(int)edge]]) % 2);
            }
            foreach (Edge e in Enum.GetValues<Edge>())
            {
                ep[(int)e] = ePerm[(int)e];
                eo[(int)e] = eOri[(int)e];
            }
        }

        // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        // Multiply this CubieCube with another CubieCube b.
        void Multiply(CubieCube b)
        {
            CornerMultiply(b);
            // EdgeMultiply(b);
        }

        // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        // Compute the inverse CubieCube
        void InvCubieCube(CubieCube c)
        {
            foreach (Edge edge in Enum.GetValues<Edge>())
                c.ep[(int)ep[(int)edge]] = edge;
            foreach (Edge edge in Enum.GetValues<Edge>())
                c.eo[(int)edge] = eo[(int)c.ep[(int)edge]];
            foreach (Corner corn in Enum.GetValues<Corner>())
                c.cp[(int)cp[(int)corn]] = corn;
            foreach (Corner corn in Enum.GetValues<Corner>())
            {
                byte ori = co[(int)c.cp[(int)corn]];
                if (ori >= 3)// Just for completeness. We do not invert mirrored
                             // cubes in the program.
                    c.co[(int)corn] = ori;
                else
                {// the standard case
                    c.co[(int)corn] = (byte)-ori;
                    if (c.co[(int)corn] < 0)
                        c.co[(int)corn] += 3;
                }
            }
        }

        // ********************************************* Get and set coordinates *********************************************

        // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        // return the twist of the 8 corners. 0 <= twist < 3^7
        public short GetTwist()
        {
            short ret = 0;
            for (int i = (int)Corner.URF; i < (int)Corner.DRB; i++)
                ret = (short)(3 * ret + co[i]);
            return ret;
        }

        // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        public void SetTwist(short twist)
        {
            int twistParity = 0;
            for (int i = (int)Corner.DRB - 1; i >= (int)Corner.URF; i--)
            {
                twistParity += co[i] = (byte)(twist % 3);
                twist /= 3;
            }
            co[(int)Corner.DRB] = (byte)((3 - twistParity % 3) % 3);
        }

        // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        // return the flip of the 12 edges. 0<= flip < 2^11
        public short GetFlip()
        {
            short ret = 0;
            for (int i = (int)Edge.UR; i < (int)Edge.BR; i++)
                ret = (short)(2 * ret + eo[i]);
            return ret;
        }

        // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        public void SetFlip(short flip)
        {
            int flipParity = 0;
            for (int i = (int)Edge.BR - 1; i >= (int)Edge.UR; i--)
            {
                flipParity += eo[i] = (byte)(flip % 2);
                flip /= 2;
            }
            eo[(int)Edge.BR] = (byte)((2 - flipParity % 2) % 2);
        }

        // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        // Parity of the corner permutation
        public short CornerParity()
        {
            int s = 0;
            for (int i = (int)Corner.DRB; i >= (int)Corner.URF + 1; i--)
                for (int j = i - 1; j >= (int)Corner.URF; j--)
                    if ((int)cp[j] > (int)cp[i])
                        s++;
            return (short)(s % 2);
        }

        // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        // Parity of the edges permutation. Parity of corners and edges are the same if the cube is solvable.
        public short EdgeParity()
        {
            int s = 0;
            for (int i = (int)Edge.BR; i >= (int)Edge.UR + 1; i--)
                for (int j = i - 1; j >= (int)Edge.UR; j--)
                    if ((int)ep[j] > (int)ep[i])
                        s++;
            return (short)(s % 2);
        }

        // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        // permutation of the UD-slice edges FR,FL,BL and BR
        public short GetFRtoBR()
        {
            int a = 0, x = 0;
            Edge[] edge4 = new Edge[4];
            // compute the index a < (12 choose 4) and the permutation array perm.
            for (int j = (int)Edge.BR; j >= (int)Edge.UR; j--)
                if ((int)Edge.FR <= (int)ep[j] && (int)ep[j] <= (int)Edge.BR)
                {
                    a += Cnk(11 - j, x + 1);
                    edge4[3 - x++] = ep[j];
                }

            int b = 0;
            for (int j = 3; j > 0; j--)// compute the index b < 4! for the
                                       // permutation in perm
            {
                int k = 0;
                while ((int)edge4[j] != j + 8)
                {
                    RotateLeft(edge4, 0, j);
                    k++;
                }
                b = (j + 1) * b + k;
            }
            return (short)(24 * a + b);
        }

        // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        public void SetFRtoBR(short idx)
        {
            int x;
            Edge[] sliceEdge = [Edge.FR, Edge.FL, Edge.BL, Edge.BR];
            Edge[] otherEdge = [Edge.UR, Edge.UF, Edge.UL, Edge.UB, Edge.DR, Edge.DF, Edge.DL, Edge.DB];
            int b = idx % 24; // Permutation
            int a = idx / 24; // Combination
            foreach (Edge e in Enum.GetValues<Edge>())
                ep[(int)e] = Edge.DB;// Use UR to invalidate all edges

            for (int j = 1, k; j < 4; j++)// generate permutation from index b
            {
                k = b % (j + 1);
                b /= j + 1;
                while (k-- > 0)
                    RotateRight(sliceEdge, 0, j);
            }

            x = 3;// generate combination and set slice edges
            for (int j = (int)Edge.UR; j <= (int)Edge.BR; j++)
                if (a - Cnk(11 - j, x + 1) >= 0)
                {
                    ep[j] = sliceEdge[3 - x];
                    a -= Cnk(11 - j, x-- + 1);
                }
            x = 0; // set the remaining edges UR..DB
            for (int j = (int)Edge.UR; j <= (int)Edge.BR; j++)
                if (ep[j] == Edge.DB)
                    ep[j] = otherEdge[x++];

        }

        // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        // Permutation of all corners except DBL and DRB
        public short GetURFtoDLF()
        {
            int a = 0, x = 0;
            Corner[] corner6 = new Corner[6];
            // compute the index a < (8 choose 6) and the corner permutation.
            for (int j = (int)Corner.URF; j <= (int)Corner.DRB; j++)
                if ((int)cp[j] <= (int)Corner.DLF)
                {
                    a += Cnk(j, x + 1);
                    corner6[x++] = cp[j];
                }

            int b = 0;
            for (int j = 5; j > 0; j--)// compute the index b < 6! for the
                                       // permutation in corner6
            {
                int k = 0;
                while ((int)corner6[j] != j)
                {
                    RotateLeft(corner6, 0, j);
                    k++;
                }
                b = (j + 1) * b + k;
            }
            return (short)(720 * a + b);
        }

        // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        public void SetURFtoDLF(short idx)
        {
            int x;
            Corner[] corner6 = [Corner.URF, Corner.UFL, Corner.ULB, Corner.UBR, Corner.DFR, Corner.DLF];
            Corner[] otherCorner = [Corner.DBL, Corner.DRB];
            int b = idx % 720; // Permutation
            int a = idx / 720; // Combination
            foreach (Corner c in Enum.GetValues<Corner>())
                cp[(int)c] = Corner.DRB;// Use DRB to invalidate all corners

            for (int j = 1, k; j < 6; j++)// generate permutation from index b
            {
                k = b % (j + 1);
                b /= j + 1;
                while (k-- > 0)
                    RotateRight(corner6, 0, j);
            }
            x = 5;// generate combination and set corners
            for (int j = (int)Corner.DRB; j >= 0; j--)
                if (a - Cnk(j, x + 1) >= 0)
                {
                    cp[j] = corner6[x];
                    a -= Cnk(j, x-- + 1);
                }
            x = 0;
            for (int j = (int)Corner.URF; j <= (int)Corner.DRB; j++)
                if (cp[j] == Corner.DRB)
                    cp[j] = otherCorner[x++];
        }

        // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        // Permutation of the six edges UR,UF,UL,UB,DR,DF.
        public int GetURtoDF()
        {
            int a = 0, x = 0;
            Edge[] edge6 = new Edge[6];
            // compute the index a < (12 choose 6) and the edge permutation.
            for (int j = (int)Edge.UR; j <= (int)Edge.BR; j++)
                if ((int)ep[j] <= (int)Edge.DF)
                {
                    a += Cnk(j, x + 1);
                    edge6[x++] = ep[j];
                }

            int b = 0;
            for (int j = 5; j > 0; j--)// compute the index b < 6! for the
                                       // permutation in edge6
            {
                int k = 0;
                while ((int)edge6[j] != j)
                {
                    RotateLeft(edge6, 0, j);
                    k++;
                }
                b = (j + 1) * b + k;
            }
            return 720 * a + b;
        }

        // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        public void SetURtoDF(int idx)
        {
            int x;
            Edge[] edge6 = [Edge.UR, Edge.UF, Edge.UL, Edge.UB, Edge.DR, Edge.DF];
            Edge[] otherEdge = [Edge.DL, Edge.DB, Edge.FR, Edge.FL, Edge.BL, Edge.BR];
            int b = idx % 720; // Permutation
            int a = idx / 720; // Combination
            foreach (Edge e in Enum.GetValues<Edge>())
                ep[(int)e] = Edge.BR;// Use BR to invalidate all edges

            for (int j = 1, k; j < 6; j++)// generate permutation from index b
            {
                k = b % (j + 1);
                b /= j + 1;
                while (k-- > 0)
                    RotateRight(edge6, 0, j);
            }
            x = 5;// generate combination and set edges
            for (int j = (int)Edge.BR; j >= 0; j--)
                if (a - Cnk(j, x + 1) >= 0)
                {
                    ep[j] = edge6[x];
                    a -= Cnk(j, x-- + 1);
                }
            x = 0; // set the remaining edges DL..BR
            for (int j = (int)Edge.UR; j <= (int)Edge.BR; j++)
                if (ep[j] == Edge.BR)
                    ep[j] = otherEdge[x++];
        }

        // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        // Permutation of the six edges UR,UF,UL,UB,DR,DF
        public static int GetURtoDF(short idx1, short idx2)
        {
            CubieCube a = new();
            CubieCube b = new();
            a.SetURtoUL(idx1);
            b.SetUBtoDF(idx2);
            for (int i = 0; i < 8; i++)
            {
                if (a.ep[i] != Edge.BR)
                    if (b.ep[i] != Edge.BR)// collision
                        return -1;
                    else
                        b.ep[i] = a.ep[i];
            }
            return b.GetURtoDF();
        }

        // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        // Permutation of the three edges UR,UF,UL
        public short GetURtoUL()
        {
            int a = 0, x = 0;
            Edge[] edge3 = new Edge[3];
            // compute the index a < (12 choose 3) and the edge permutation.
            for (int j = (int)Edge.UR; j <= (int)Edge.BR; j++)
                if ((int)ep[j] <= (int)Edge.UL)
                {
                    a += Cnk(j, x + 1);
                    edge3[x++] = ep[j];
                }

            int b = 0;
            for (int j = 2; j > 0; j--)// compute the index b < 3! for the
                                       // permutation in edge3
            {
                int k = 0;
                while ((int)edge3[j] != j)
                {
                    RotateLeft(edge3, 0, j);
                    k++;
                }
                b = (j + 1) * b + k;
            }
            return (short)(6 * a + b);
        }

        // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        public void SetURtoUL(short idx)
        {
            int x;
            Edge[] edge3 = [Edge.UR, Edge.UF, Edge.UL];
            int b = idx % 6; // Permutation
            int a = idx / 6; // Combination
            foreach (Edge e in Enum.GetValues<Edge>())
                ep[(int)e] = Edge.BR;// Use BR to invalidate all edges

            for (int j = 1, k; j < 3; j++)// generate permutation from index b
            {
                k = b % (j + 1);
                b /= j + 1;
                while (k-- > 0)
                    RotateRight(edge3, 0, j);
            }
            x = 2;// generate combination and set edges
            for (int j = (int)Edge.BR; j >= 0; j--)
                if (a - Cnk(j, x + 1) >= 0)
                {
                    ep[j] = edge3[x];
                    a -= Cnk(j, x-- + 1);
                }
        }

        // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        // Permutation of the three edges UB,DR,DF
        public short GetUBtoDF()
        {
            int a = 0, x = 0;
            Edge[] edge3 = new Edge[3];
            // compute the index a < (12 choose 3) and the edge permutation.
            for (int j = (int)Edge.UR; j <= (int)Edge.BR; j++)
                if ((int)Edge.UB <= (int)ep[j] && (int)ep[j] <= (int)Edge.DF)
                {
                    a += Cnk(j, x + 1);
                    edge3[x++] = ep[j];
                }

            int b = 0;
            for (int j = 2; j > 0; j--)// compute the index b < 3! for the
                                       // permutation in edge3
            {
                int k = 0;
                while ((int)edge3[j] != (int)Edge.UB + j)
                {
                    RotateLeft(edge3, 0, j);
                    k++;
                }
                b = (j + 1) * b + k;
            }
            return (short)(6 * a + b);
        }

        // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        public void SetUBtoDF(short idx)
        {
            int x;
            Edge[] edge3 = [Edge.UB, Edge.DR, Edge.DF];
            int b = idx % 6; // Permutation
            int a = idx / 6; // Combination
            foreach (Edge e in Enum.GetValues<Edge>())
                ep[(int)e] = Edge.BR;// Use BR to invalidate all edges

            for (int j = 1, k; j < 3; j++)// generate permutation from index b
            {
                k = b % (j + 1);
                b /= j + 1;
                while (k-- > 0)
                    RotateRight(edge3, 0, j);
            }
            x = 2;// generate combination and set edges
            for (int j = (int)Edge.BR; j >= 0; j--)
                if (a - Cnk(j, x + 1) >= 0)
                {
                    ep[j] = edge3[x];
                    a -= Cnk(j, x-- + 1);
                }
        }

        // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        int GetURFtoDLB()
        {
            Corner[] perm = new Corner[8];
            int b = 0;
            for (int i = 0; i < 8; i++)
                perm[i] = cp[i];
            for (int j = 7; j > 0; j--)// compute the index b < 8! for the permutation in perm
            {
                int k = 0;
                while ((int)perm[j] != j)
                {
                    RotateLeft(perm, 0, j);
                    k++;
                }
                b = (j + 1) * b + k;
            }
            return b;
        }

        // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        public void SetURFtoDLB(int idx)
        {
            Corner[] perm = [Corner.URF, Corner.UFL, Corner.ULB, Corner.UBR, Corner.DFR, Corner.DLF, Corner.DBL, Corner.DRB];
            int k;
            for (int j = 1; j < 8; j++)
            {
                k = idx % (j + 1);
                idx /= j + 1;
                while (k-- > 0)
                    RotateRight(perm, 0, j);
            }
            int x = 7;// set corners
            for (int j = 7; j >= 0; j--)
                cp[j] = perm[x--];
        }

        // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        int GetURtoBR()
        {
            Edge[] perm = new Edge[12];
            int b = 0;
            for (int i = 0; i < 12; i++)
                perm[i] = ep[i];
            for (int j = 11; j > 0; j--)// compute the index b < 12! for the permutation in perm
            {
                int k = 0;
                while ((int)perm[j] != j)
                {
                    RotateLeft(perm, 0, j);
                    k++;
                }
                b = (j + 1) * b + k;
            }
            return b;
        }

        // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        public void SetURtoBR(int idx)
        {
            Edge[] perm = [Edge.UR, Edge.UF, Edge.UL, Edge.UB, Edge.DR, Edge.DF, Edge.DL, Edge.DB, Edge.FR, Edge.FL, Edge.BL, Edge.BR];
            int k;
            for (int j = 1; j < 12; j++)
            {
                k = idx % (j + 1);
                idx /= j + 1;
                while (k-- > 0)
                    RotateRight(perm, 0, j);
            }
            int x = 11;// set edges
            for (int j = 11; j >= 0; j--)
                ep[j] = perm[x--];
        }

        // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        // Check a cubiecube for solvability. Return the error code.
        // 0: Cube is solvable
        // -2: Not all 12 edges exist exactly once
        // -3: Flip error: One edge has to be flipped
        // -4: Not all corners exist exactly once
        // -5: Twist error: One corner has to be twisted
        // -6: Parity error: Two corners ore two edges have to be exchanged
        public int Verify()
        {
            int sum = 0;
            int[] edgeCount = new int[12];
            foreach (Edge e in Enum.GetValues<Edge>())
                edgeCount[(int)ep[(int)e]]++;
            for (int i = 0; i < 12; i++)
                if (edgeCount[i] != 1)
                    return -2;

            for (int i = 0; i < 12; i++)
                sum += eo[i];
            if (sum % 2 != 0)
                return -3;

            int[] cornerCount = new int[8];
            foreach (Corner c in Enum.GetValues<Corner>())
                cornerCount[(int)cp[(int)c]]++;
            for (int i = 0; i < 8; i++)
                if (cornerCount[i] != 1)
                    return -4;// missing corners

            sum = 0;
            for (int i = 0; i < 8; i++)
                sum += co[i];
            if (sum % 3 != 0)
                return -5;// twisted corner

            if ((EdgeParity() ^ CornerParity()) != 0)
                return -6;// parity error

            return 0;// cube ok
        }
    }
}