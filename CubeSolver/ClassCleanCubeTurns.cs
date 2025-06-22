namespace CubeSolver
{
    internal sealed class ClassCleanCubeTurns
    {
        /// <summary>
        /// Clean the list with the cube turns by replacing or removing turns
        /// <para>The list is passed by reference. This means that the method receives a reference to the original list, not a copy of it. Therefore, any changes made to the list within the method will affect the original list.</para>
        /// </summary>
        /// <param name="lCubeTurnsToClean"></param>
        /// <param name="bCubeTurnsSave"></param>
        public static void CleanListCubeTurns(List<string> lCubeTurnsToClean, bool bCubeTurnsSave)
        {
            //// Start the stopwatch
            //long startTime = Stopwatch.GetTimestamp();

            //// Declare the variables
            const string cNone = "None";
            const char cApos = '\'';
            const char c2 = '2';

            //// Copy the list with the cube turns to a new list to return in case of an error (IndexOutOfRangeException RRRR RRR R L R')
            List<string> lCubeTurnsToCleanOriginal = [.. lCubeTurnsToClean];
#if DEBUG
            //// Save the list with the cube turns before the cleaning to a file, for testing purposes
            if (bCubeTurnsSave)
            {
                _ = ClassFileOperations.CubeTurnsSave("CubeTurnsBeforeFirstClean.txt");
            }
#endif
            //// Do the cleaning between 2 and 10 times
            try
            {
                for (int nNumberCleanings = 1; nNumberCleanings < 11; nNumberCleanings++)
                {
                    for (int i = 0; i < lCubeTurnsToClean.Count - 1; i++)
                    {
                        //// Prevent the IndexOutOfRangeException
                        if (i + 1 < lCubeTurnsToClean.Count)
                        {
                            //// Replace two same turns with one turn or no turn
                            if (lCubeTurnsToClean[i] == lCubeTurnsToClean[i + 1])
                            {
                                // U U -> U2
                                if (lCubeTurnsToClean[i].Length == 1)
                                {
                                    lCubeTurnsToClean[i] = lCubeTurnsToClean[i] + c2;
                                    lCubeTurnsToClean[i + 1] = cNone;
                                }

                                // U' U' -> U2
                                else if (lCubeTurnsToClean[i].EndsWith(cApos))
                                {
                                    lCubeTurnsToClean[i] = lCubeTurnsToClean[i][0] + "2";
                                    lCubeTurnsToClean[i + 1] = cNone;
                                }

                                // U2 U2 -> None
                                else if (lCubeTurnsToClean[i].EndsWith(c2))
                                {
                                    lCubeTurnsToClean[i] = cNone;
                                    lCubeTurnsToClean[i + 1] = cNone;
                                }
                            }

                            //// Replace two same first letters of a turn with another turn or no turn
                            else if (lCubeTurnsToClean[i][0] == lCubeTurnsToClean[i + 1][0])
                            {
                                // U U' -> None
                                if (lCubeTurnsToClean[i].Length == 1 && lCubeTurnsToClean[i + 1].EndsWith(cApos))
                                {
                                    lCubeTurnsToClean[i] = cNone;
                                    lCubeTurnsToClean[i + 1] = cNone;
                                }

                                // U' U -> None
                                else if (lCubeTurnsToClean[i].EndsWith(cApos) && lCubeTurnsToClean[i + 1].Length == 1)
                                {
                                    lCubeTurnsToClean[i] = cNone;
                                    lCubeTurnsToClean[i + 1] = cNone;
                                }

                                // U U2 -> U'
                                else if (lCubeTurnsToClean[i].Length == 1 && lCubeTurnsToClean[i + 1].EndsWith(c2))
                                {
                                    lCubeTurnsToClean[i] = lCubeTurnsToClean[i][0] + "'";
                                    lCubeTurnsToClean[i + 1] = cNone;
                                }

                                // U2 U -> U'
                                else if (lCubeTurnsToClean[i].EndsWith(c2) && lCubeTurnsToClean[i + 1].Length == 1)
                                {
                                    lCubeTurnsToClean[i] = lCubeTurnsToClean[i + 1] + cApos;
                                    lCubeTurnsToClean[i + 1] = cNone;
                                }

                                // U' U2 -> U
                                else if (lCubeTurnsToClean[i].EndsWith(cApos) && lCubeTurnsToClean[i + 1].EndsWith(c2))
                                {
                                    lCubeTurnsToClean[i] = lCubeTurnsToClean[i][0] + "";
                                    lCubeTurnsToClean[i + 1] = cNone;
                                }

                                // U2 U' -> U
                                else if (lCubeTurnsToClean[i].EndsWith(c2) && lCubeTurnsToClean[i + 1].EndsWith(cApos))
                                {
                                    lCubeTurnsToClean[i] = lCubeTurnsToClean[i][0] + "";
                                    lCubeTurnsToClean[i + 1] = cNone;
                                }
                            }

                            //// Slice turns
                            // M - Middle layer turn - in the same direction as an L turn between L and R
                            else if (lCubeTurnsToClean[i].Contains('M') || lCubeTurnsToClean[i + 1].Contains('M'))
                            {
                                // Replace M L -> l
                                if (lCubeTurnsToClean[i] == "M" && lCubeTurnsToClean[i + 1] == "L")
                                {
                                    lCubeTurnsToClean[i] = "l";
                                    lCubeTurnsToClean[i + 1] = cNone;
                                }

                                // Replace M' L' -> l'
                                else if (lCubeTurnsToClean[i] == "M'" && lCubeTurnsToClean[i + 1] == "L'")
                                {
                                    lCubeTurnsToClean[i] = "l'";
                                    lCubeTurnsToClean[i + 1] = cNone;
                                }

                                // Replace L M -> l
                                else if (lCubeTurnsToClean[i] == "L" && lCubeTurnsToClean[i + 1] == "M")
                                {
                                    lCubeTurnsToClean[i] = "l";
                                    lCubeTurnsToClean[i + 1] = cNone;
                                }

                                // Replace L' M' -> l'
                                else if (lCubeTurnsToClean[i] == "L'" && lCubeTurnsToClean[i + 1] == "M'")
                                {
                                    lCubeTurnsToClean[i] = "l'";
                                    lCubeTurnsToClean[i + 1] = cNone;
                                }

                                // Replace M R' -> r'
                                else if (lCubeTurnsToClean[i] == "M" && lCubeTurnsToClean[i + 1] == "R'")
                                {
                                    lCubeTurnsToClean[i] = "r'";
                                    lCubeTurnsToClean[i + 1] = cNone;
                                }

                                // Replace M' R -> r
                                else if (lCubeTurnsToClean[i] == "M'" && lCubeTurnsToClean[i + 1] == "R")
                                {
                                    lCubeTurnsToClean[i] = "r";
                                    lCubeTurnsToClean[i + 1] = cNone;
                                }

                                // Replace R' M -> r'
                                else if (lCubeTurnsToClean[i] == "R'" && lCubeTurnsToClean[i + 1] == "M")
                                {
                                    lCubeTurnsToClean[i] = "r'";
                                    lCubeTurnsToClean[i + 1] = cNone;
                                }

                                // Replace R M' -> r
                                else if (lCubeTurnsToClean[i] == "R" && lCubeTurnsToClean[i + 1] == "M'")
                                {
                                    lCubeTurnsToClean[i] = "r";
                                    lCubeTurnsToClean[i + 1] = cNone;
                                }
                            }

                            // E - Equatorial layer - direction as a D turn between U and D
                            else if (lCubeTurnsToClean[i].Contains('E') || lCubeTurnsToClean[i + 1].Contains('E'))
                            {
                                // Replace E U' -> u'
                                if (lCubeTurnsToClean[i] == "E" && lCubeTurnsToClean[i + 1] == "U'")
                                {
                                    lCubeTurnsToClean[i] = "u'";
                                    lCubeTurnsToClean[i + 1] = cNone;
                                }

                                // Replace E' U -> u
                                else if (lCubeTurnsToClean[i] == "E'" && lCubeTurnsToClean[i + 1] == "U")
                                {
                                    lCubeTurnsToClean[i] = "u";
                                    lCubeTurnsToClean[i + 1] = cNone;
                                }

                                // Replace U' E -> u'
                                else if (lCubeTurnsToClean[i] == "U'" && lCubeTurnsToClean[i + 1] == "E")
                                {
                                    lCubeTurnsToClean[i] = "u'";
                                    lCubeTurnsToClean[i + 1] = cNone;
                                }

                                // Replace U E' -> u
                                else if (lCubeTurnsToClean[i] == "U" && lCubeTurnsToClean[i + 1] == "E'")
                                {
                                    lCubeTurnsToClean[i] = "u";
                                    lCubeTurnsToClean[i + 1] = cNone;
                                }

                                // Replace E D -> d
                                else if (lCubeTurnsToClean[i] == "E" && lCubeTurnsToClean[i + 1] == "D")
                                {
                                    lCubeTurnsToClean[i] = "d";
                                    lCubeTurnsToClean[i + 1] = cNone;
                                }

                                // Replace E' D' -> d'
                                else if (lCubeTurnsToClean[i] == "E'" && lCubeTurnsToClean[i + 1] == "D'")
                                {
                                    lCubeTurnsToClean[i] = "d'";
                                    lCubeTurnsToClean[i + 1] = cNone;
                                }

                                // Replace D E -> d
                                else if (lCubeTurnsToClean[i] == "D" && lCubeTurnsToClean[i + 1] == "E")
                                {
                                    lCubeTurnsToClean[i] = "d";
                                    lCubeTurnsToClean[i + 1] = cNone;
                                }

                                // Replace D' E' -> d'
                                else if (lCubeTurnsToClean[i] == "D'" && lCubeTurnsToClean[i + 1] == "E'")
                                {
                                    lCubeTurnsToClean[i] = "d'";
                                    lCubeTurnsToClean[i + 1] = cNone;
                                }
                            }

                            // S - Standing layer - direction as an F turn between F and B
                            else if (lCubeTurnsToClean[i].Contains('S') || lCubeTurnsToClean[i + 1].Contains('S'))
                            {
                                // Replace S F -> f
                                if (lCubeTurnsToClean[i] == "S" && lCubeTurnsToClean[i + 1] == "F")
                                {
                                    lCubeTurnsToClean[i] = "f";
                                    lCubeTurnsToClean[i + 1] = cNone;
                                }

                                // Replace S' F' -> f'
                                else if (lCubeTurnsToClean[i] == "S'" && lCubeTurnsToClean[i + 1] == "F'")
                                {
                                    lCubeTurnsToClean[i] = "f'";
                                    lCubeTurnsToClean[i + 1] = cNone;
                                }

                                // Replace F S -> f
                                else if (lCubeTurnsToClean[i] == "F" && lCubeTurnsToClean[i + 1] == "S")
                                {
                                    lCubeTurnsToClean[i] = "f";
                                    lCubeTurnsToClean[i + 1] = cNone;
                                }

                                // Replace F' S' -> f'
                                else if (lCubeTurnsToClean[i] == "F'" && lCubeTurnsToClean[i + 1] == "S'")
                                {
                                    lCubeTurnsToClean[i] = "f'";
                                    lCubeTurnsToClean[i + 1] = cNone;
                                }

                                // Replace S B' -> b'
                                else if (lCubeTurnsToClean[i] == "S" && lCubeTurnsToClean[i + 1] == "B'")
                                {
                                    lCubeTurnsToClean[i] = "b'";
                                    lCubeTurnsToClean[i + 1] = cNone;
                                }

                                // Replace S' B -> b
                                else if (lCubeTurnsToClean[i] == "S'" && lCubeTurnsToClean[i + 1] == "B")
                                {
                                    lCubeTurnsToClean[i] = "b";
                                    lCubeTurnsToClean[i + 1] = cNone;
                                }

                                // Replace B' S -> b'
                                else if (lCubeTurnsToClean[i] == "B'" && lCubeTurnsToClean[i + 1] == "S")
                                {
                                    lCubeTurnsToClean[i] = "b'";
                                    lCubeTurnsToClean[i + 1] = cNone;
                                }

                                // Replace B S' -> b
                                else if (lCubeTurnsToClean[i] == "B" && lCubeTurnsToClean[i + 1] == "S'")
                                {
                                    lCubeTurnsToClean[i] = "b";
                                    lCubeTurnsToClean[i + 1] = cNone;
                                }
                            }
                        }

                        //// Prevent the IndexOutOfRangeException
                        if (i + 2 < lCubeTurnsToClean.Count)
                        {
                            //// Whole cube turns (x, y, z) starting with "y"
                            if (lCubeTurnsToClean[i] == "y")
                            {
                                // Replace y x y -> y2 z
                                if (lCubeTurnsToClean[i + 1] == "x" && lCubeTurnsToClean[i + 2] == "y")
                                {
                                    lCubeTurnsToClean[i] = "y2";
                                    lCubeTurnsToClean[i + 1] = "z";
                                    lCubeTurnsToClean[i + 2] = cNone;
                                }

                                // Replace y x y' -> z'
                                else if (lCubeTurnsToClean[i + 1] == "x" && lCubeTurnsToClean[i + 2] == "y'")
                                {
                                    lCubeTurnsToClean[i] = cNone;
                                    lCubeTurnsToClean[i + 1] = "z'";
                                    lCubeTurnsToClean[i + 2] = cNone;
                                }

                                // Replace y x y2 -> y' x'
                                else if (lCubeTurnsToClean[i + 1] == "x" && lCubeTurnsToClean[i + 2] == "y2")
                                {
                                    lCubeTurnsToClean[i] = "y'";
                                    lCubeTurnsToClean[i + 1] = "x'";
                                    lCubeTurnsToClean[i + 2] = cNone;
                                }

                                //// Whole cube turns (y face y)
                                // Replace y U y -> y2 U
                                // Replace y U' y -> y2 U'
                                // Replace y U2 y -> y2 U2
                                else if (lCubeTurnsToClean[i + 1].StartsWith('U') && lCubeTurnsToClean[i + 2] == "y")
                                {
                                    lCubeTurnsToClean[i] = "y2";
                                    lCubeTurnsToClean[i + 2] = cNone;
                                }

                                // Replace y D y -> y2 D
                                // Replace y D' y -> y2 D'
                                // Replace y D2 y -> y2 D2
                                else if (lCubeTurnsToClean[i + 1].StartsWith('D') && lCubeTurnsToClean[i + 2] == "y")
                                {
                                    lCubeTurnsToClean[i] = "y2";
                                    lCubeTurnsToClean[i + 2] = cNone;
                                }

                                // Replace y F y -> y2 L
                                else if (lCubeTurnsToClean[i + 1] == "F" && lCubeTurnsToClean[i + 2] == "y")
                                {
                                    lCubeTurnsToClean[i] = "y2";
                                    lCubeTurnsToClean[i + 1] = "L";
                                    lCubeTurnsToClean[i + 2] = cNone;
                                }

                                // Replace y F' y -> y2 L'
                                else if (lCubeTurnsToClean[i + 1] == "F'" && lCubeTurnsToClean[i + 2] == "y")
                                {
                                    lCubeTurnsToClean[i] = "y2";
                                    lCubeTurnsToClean[i + 1] = "L'";
                                    lCubeTurnsToClean[i + 2] = cNone;
                                }

                                // Replace y F2 y -> y2 L2
                                else if (lCubeTurnsToClean[i + 1] == "F2" && lCubeTurnsToClean[i + 2] == "y")
                                {
                                    lCubeTurnsToClean[i] = "y2";
                                    lCubeTurnsToClean[i + 1] = "L2";
                                    lCubeTurnsToClean[i + 2] = cNone;
                                }

                                // Replace y B y -> y2 R
                                else if (lCubeTurnsToClean[i + 1] == "B" && lCubeTurnsToClean[i + 2] == "y")
                                {
                                    lCubeTurnsToClean[i] = "y2";
                                    lCubeTurnsToClean[i + 1] = "R";
                                    lCubeTurnsToClean[i + 2] = cNone;
                                }

                                // Replace y B' y -> y2 R'
                                else if (lCubeTurnsToClean[i + 1] == "B'" && lCubeTurnsToClean[i + 2] == "y")
                                {
                                    lCubeTurnsToClean[i] = "y2";
                                    lCubeTurnsToClean[i + 1] = "R'";
                                    lCubeTurnsToClean[i + 2] = cNone;
                                }

                                // Replace y B2 y -> y2 R2
                                else if (lCubeTurnsToClean[i + 1] == "B2" && lCubeTurnsToClean[i + 2] == "y")
                                {
                                    lCubeTurnsToClean[i] = "y2";
                                    lCubeTurnsToClean[i + 1] = "R2";
                                    lCubeTurnsToClean[i + 2] = cNone;
                                }

                                // Replace y L y -> y2 B
                                else if (lCubeTurnsToClean[i + 1] == "L" && lCubeTurnsToClean[i + 2] == "y")
                                {
                                    lCubeTurnsToClean[i] = "y2";
                                    lCubeTurnsToClean[i + 1] = "B";
                                    lCubeTurnsToClean[i + 2] = cNone;
                                }

                                // Replace y L' y -> y2 B'
                                else if (lCubeTurnsToClean[i + 1] == "L'" && lCubeTurnsToClean[i + 2] == "y")
                                {
                                    lCubeTurnsToClean[i] = "y2";
                                    lCubeTurnsToClean[i + 1] = "B'";
                                    lCubeTurnsToClean[i + 2] = cNone;
                                }

                                // Replace y L2 y -> y2 B2
                                else if (lCubeTurnsToClean[i + 1] == "L2" && lCubeTurnsToClean[i + 2] == "y")
                                {
                                    lCubeTurnsToClean[i] = "y2";
                                    lCubeTurnsToClean[i + 1] = "B2";
                                    lCubeTurnsToClean[i + 2] = cNone;
                                }

                                // Replace y R y -> y2 F
                                else if (lCubeTurnsToClean[i + 1] == "R" && lCubeTurnsToClean[i + 2] == "y")
                                {
                                    lCubeTurnsToClean[i] = "y2";
                                    lCubeTurnsToClean[i + 1] = "F";
                                    lCubeTurnsToClean[i + 2] = cNone;
                                }

                                // Replace y R' y -> y2 F'
                                else if (lCubeTurnsToClean[i + 1] == "R'" && lCubeTurnsToClean[i + 2] == "y")
                                {
                                    lCubeTurnsToClean[i] = "y2";
                                    lCubeTurnsToClean[i + 1] = "F'";
                                    lCubeTurnsToClean[i + 2] = cNone;
                                }

                                // Replace y R2 y -> y2 F2
                                else if (lCubeTurnsToClean[i + 1] == "R2" && lCubeTurnsToClean[i + 2] == "y")
                                {
                                    lCubeTurnsToClean[i] = "y2";
                                    lCubeTurnsToClean[i + 1] = "F2";
                                    lCubeTurnsToClean[i + 2] = cNone;
                                }

                                //// Whole cube turns (y face y')
                                // Replace y U y' -> U
                                // Replace y U' y' -> U'
                                // Replace y U2 y' -> U2
                                else if (lCubeTurnsToClean[i + 1].StartsWith('U') && lCubeTurnsToClean[i + 2] == "y'")
                                {
                                    lCubeTurnsToClean[i] = cNone;
                                    lCubeTurnsToClean[i + 2] = cNone;
                                }

                                // Replace y D y' -> D
                                // Replace y D' y' -> D'
                                // Replace y D2 y' -> D2
                                else if (lCubeTurnsToClean[i + 1].StartsWith('D') && lCubeTurnsToClean[i + 2] == "y'")
                                {
                                    lCubeTurnsToClean[i] = cNone;
                                    lCubeTurnsToClean[i + 2] = cNone;
                                }

                                // Replace y F y' -> R
                                else if (lCubeTurnsToClean[i + 1] == "F" && lCubeTurnsToClean[i + 2] == "y'")
                                {
                                    lCubeTurnsToClean[i] = cNone;
                                    lCubeTurnsToClean[i + 1] = "R";
                                    lCubeTurnsToClean[i + 2] = cNone;
                                }

                                // Replace y F' y' -> R'
                                else if (lCubeTurnsToClean[i + 1] == "F'" && lCubeTurnsToClean[i + 2] == "y'")
                                {
                                    lCubeTurnsToClean[i] = cNone;
                                    lCubeTurnsToClean[i + 1] = "R'";
                                    lCubeTurnsToClean[i + 2] = cNone;
                                }

                                // Replace y F2 y' -> R2
                                else if (lCubeTurnsToClean[i + 1] == "F2" && lCubeTurnsToClean[i + 2] == "y'")
                                {
                                    lCubeTurnsToClean[i] = cNone;
                                    lCubeTurnsToClean[i + 1] = "R2";
                                    lCubeTurnsToClean[i + 2] = cNone;
                                }

                                // Replace y B y' -> L
                                else if (lCubeTurnsToClean[i + 1] == "B" && lCubeTurnsToClean[i + 2] == "y'")
                                {
                                    lCubeTurnsToClean[i] = cNone;
                                    lCubeTurnsToClean[i + 1] = "L";
                                    lCubeTurnsToClean[i + 2] = cNone;
                                }

                                // Replace y B' y' -> L'
                                else if (lCubeTurnsToClean[i + 1] == "B'" && lCubeTurnsToClean[i + 2] == "y'")
                                {
                                    lCubeTurnsToClean[i] = cNone;
                                    lCubeTurnsToClean[i + 1] = "L'";
                                    lCubeTurnsToClean[i + 2] = cNone;
                                }

                                // Replace y B2 y' -> L2
                                else if (lCubeTurnsToClean[i + 1] == "B2" && lCubeTurnsToClean[i + 2] == "y'")
                                {
                                    lCubeTurnsToClean[i] = cNone;
                                    lCubeTurnsToClean[i + 1] = "L2";
                                    lCubeTurnsToClean[i + 2] = cNone;
                                }

                                // Replace y L y' -> F
                                else if (lCubeTurnsToClean[i + 1] == "L" && lCubeTurnsToClean[i + 2] == "y'")
                                {
                                    lCubeTurnsToClean[i] = cNone;
                                    lCubeTurnsToClean[i + 1] = "F";
                                    lCubeTurnsToClean[i + 2] = cNone;
                                }

                                // Replace y L' y' -> F'
                                else if (lCubeTurnsToClean[i + 1] == "L'" && lCubeTurnsToClean[i + 2] == "y'")
                                {
                                    lCubeTurnsToClean[i] = cNone;
                                    lCubeTurnsToClean[i + 1] = "F'";
                                    lCubeTurnsToClean[i + 2] = cNone;
                                }

                                // Replace y L2 y' -> F2
                                else if (lCubeTurnsToClean[i + 1] == "L2" && lCubeTurnsToClean[i + 2] == "y'")
                                {
                                    lCubeTurnsToClean[i] = cNone;
                                    lCubeTurnsToClean[i + 1] = "F2";
                                    lCubeTurnsToClean[i + 2] = cNone;
                                }

                                // Replace y R y' -> B
                                else if (lCubeTurnsToClean[i + 1] == "R" && lCubeTurnsToClean[i + 2] == "y'")
                                {
                                    lCubeTurnsToClean[i] = cNone;
                                    lCubeTurnsToClean[i + 1] = "B";
                                    lCubeTurnsToClean[i + 2] = cNone;
                                }

                                // Replace y R' y' -> B'
                                else if (lCubeTurnsToClean[i + 1] == "R'" && lCubeTurnsToClean[i + 2] == "y'")
                                {
                                    lCubeTurnsToClean[i] = cNone;
                                    lCubeTurnsToClean[i + 1] = "B'";
                                    lCubeTurnsToClean[i + 2] = cNone;
                                }

                                // Replace y R2 y' -> B2
                                else if (lCubeTurnsToClean[i + 1] == "R2" && lCubeTurnsToClean[i + 2] == "y'")
                                {
                                    lCubeTurnsToClean[i] = cNone;
                                    lCubeTurnsToClean[i + 1] = "B2";
                                    lCubeTurnsToClean[i + 2] = cNone;
                                }

                                //// Whole cube turns (y face y2)
                                // Replace y U y2 -> y' U
                                // Replace y U' y2 -> y' U'
                                // Replace y U2 y2 -> y' U2
                                else if (lCubeTurnsToClean[i + 1].StartsWith('U') && lCubeTurnsToClean[i + 2] == "y2")
                                {
                                    lCubeTurnsToClean[i] = "y'";
                                    lCubeTurnsToClean[i + 2] = cNone;
                                }

                                // Replace y D y2 -> y' D
                                // Replace y D' y2 -> y' D'
                                // Replace y D2 y2 -> y' D2
                                else if (lCubeTurnsToClean[i + 1].StartsWith('D') && lCubeTurnsToClean[i + 2] == "y2")
                                {
                                    lCubeTurnsToClean[i] = "y'";
                                    lCubeTurnsToClean[i + 2] = cNone;
                                }

                                // Replace y F y2 -> y' B
                                else if (lCubeTurnsToClean[i + 1] == "F" && lCubeTurnsToClean[i + 2] == "y2")
                                {
                                    lCubeTurnsToClean[i] = "y'";
                                    lCubeTurnsToClean[i + 1] = "B";
                                    lCubeTurnsToClean[i + 2] = cNone;
                                }

                                // Replace y F' y2 -> y' B'
                                else if (lCubeTurnsToClean[i + 1] == "F'" && lCubeTurnsToClean[i + 2] == "y2")
                                {
                                    lCubeTurnsToClean[i] = "y'";
                                    lCubeTurnsToClean[i + 1] = "B'";
                                    lCubeTurnsToClean[i + 2] = cNone;
                                }

                                // Replace y F2 y2 -> y' B2
                                else if (lCubeTurnsToClean[i + 1] == "F2" && lCubeTurnsToClean[i + 2] == "y2")
                                {
                                    lCubeTurnsToClean[i] = "y'";
                                    lCubeTurnsToClean[i + 1] = "B2";
                                    lCubeTurnsToClean[i + 2] = cNone;
                                }

                                // Replace y B y2 -> y' F
                                else if (lCubeTurnsToClean[i + 1] == "B" && lCubeTurnsToClean[i + 2] == "y2")
                                {
                                    lCubeTurnsToClean[i] = "y'";
                                    lCubeTurnsToClean[i + 1] = "F";
                                    lCubeTurnsToClean[i + 2] = cNone;
                                }

                                // Replace y B' y2 -> y' F'
                                else if (lCubeTurnsToClean[i + 1] == "B'" && lCubeTurnsToClean[i + 2] == "y2")
                                {
                                    lCubeTurnsToClean[i] = "y'";
                                    lCubeTurnsToClean[i + 1] = "F'";
                                    lCubeTurnsToClean[i + 2] = cNone;
                                }

                                // Replace y B2 y2 -> y' F2
                                else if (lCubeTurnsToClean[i + 1] == "B2" && lCubeTurnsToClean[i + 2] == "y2")
                                {
                                    lCubeTurnsToClean[i] = "y'";
                                    lCubeTurnsToClean[i + 1] = "F2";
                                    lCubeTurnsToClean[i + 2] = cNone;
                                }

                                // Replace y L y2 -> y' R
                                else if (lCubeTurnsToClean[i + 1] == "L" && lCubeTurnsToClean[i + 2] == "y2")
                                {
                                    lCubeTurnsToClean[i] = "y'";
                                    lCubeTurnsToClean[i + 1] = "R";
                                    lCubeTurnsToClean[i + 2] = cNone;
                                }

                                // Replace y L' y2 -> y' R'
                                else if (lCubeTurnsToClean[i + 1] == "L'" && lCubeTurnsToClean[i + 2] == "y2")
                                {
                                    lCubeTurnsToClean[i] = "y'";
                                    lCubeTurnsToClean[i + 1] = "R'";
                                    lCubeTurnsToClean[i + 2] = cNone;
                                }

                                // Replace y L2 y2 -> y' R2
                                else if (lCubeTurnsToClean[i + 1] == "L2" && lCubeTurnsToClean[i + 2] == "y2")
                                {
                                    lCubeTurnsToClean[i] = "y'";
                                    lCubeTurnsToClean[i + 1] = "R2";
                                    lCubeTurnsToClean[i + 2] = cNone;
                                }

                                // Replace y R y2 -> y' L
                                else if (lCubeTurnsToClean[i + 1] == "R" && lCubeTurnsToClean[i + 2] == "y2")
                                {
                                    lCubeTurnsToClean[i] = "y'";
                                    lCubeTurnsToClean[i + 1] = "L";
                                    lCubeTurnsToClean[i + 2] = cNone;
                                }

                                // Replace y R' y2 -> y' L'
                                else if (lCubeTurnsToClean[i + 1] == "R'" && lCubeTurnsToClean[i + 2] == "y2")
                                {
                                    lCubeTurnsToClean[i] = "y'";
                                    lCubeTurnsToClean[i + 1] = "L'";
                                    lCubeTurnsToClean[i + 2] = cNone;
                                }

                                // Replace y R2 y2 -> y' L2
                                else if (lCubeTurnsToClean[i + 1] == "R2" && lCubeTurnsToClean[i + 2] == "y2")
                                {
                                    lCubeTurnsToClean[i] = "y'";
                                    lCubeTurnsToClean[i + 1] = "L2";
                                    lCubeTurnsToClean[i + 2] = cNone;
                                }
                            }

                            //// Whole cube turns (x, y, z) starting with "y'"
                            else if (lCubeTurnsToClean[i] == "y'")
                            {
                                // Replace y' x y' -> y2 z'
                                if (lCubeTurnsToClean[i + 1] == "x" && lCubeTurnsToClean[i + 2] == "y'")
                                {
                                    lCubeTurnsToClean[i] = "y2";
                                    lCubeTurnsToClean[i + 1] = "z'";
                                    lCubeTurnsToClean[i + 2] = cNone;
                                }

                                // Replace y' x y -> z
                                else if (lCubeTurnsToClean[i + 1] == "x" && lCubeTurnsToClean[i + 2] == "y")
                                {
                                    lCubeTurnsToClean[i] = cNone;
                                    lCubeTurnsToClean[i + 1] = "z";
                                    lCubeTurnsToClean[i + 2] = cNone;
                                }

                                // Replace y' x y2 -> y x' (or z y)
                                else if (lCubeTurnsToClean[i + 1] == "x" && lCubeTurnsToClean[i + 2] == "y2")
                                {
                                    lCubeTurnsToClean[i] = "y";
                                    lCubeTurnsToClean[i + 1] = "x'";
                                    lCubeTurnsToClean[i + 2] = cNone;
                                }

                                //// Whole cube turns (y' face y)
                                // Replace y' U y -> U
                                // Replace y' U' y -> U'
                                // Replace y' U2 y -> U2
                                else if (lCubeTurnsToClean[i + 1].StartsWith('U') && lCubeTurnsToClean[i + 2] == "y")
                                {
                                    lCubeTurnsToClean[i] = cNone;
                                    lCubeTurnsToClean[i + 2] = cNone;
                                }

                                // Replace y' D y -> D
                                // Replace y' D' y -> D'
                                // Replace y' D2 y -> D2
                                else if (lCubeTurnsToClean[i + 1].StartsWith('D') && lCubeTurnsToClean[i + 2] == "y")
                                {
                                    lCubeTurnsToClean[i] = cNone;
                                    lCubeTurnsToClean[i + 2] = cNone;
                                }

                                // Replace y' F y -> L
                                else if (lCubeTurnsToClean[i + 1] == "F" && lCubeTurnsToClean[i + 2] == "y")
                                {
                                    lCubeTurnsToClean[i] = cNone;
                                    lCubeTurnsToClean[i + 1] = "L";
                                    lCubeTurnsToClean[i + 2] = cNone;
                                }

                                // Replace y' F' y -> L'
                                else if (lCubeTurnsToClean[i + 1] == "F'" && lCubeTurnsToClean[i + 2] == "y")
                                {
                                    lCubeTurnsToClean[i] = cNone;
                                    lCubeTurnsToClean[i + 1] = "L'";
                                    lCubeTurnsToClean[i + 2] = cNone;
                                }

                                // Replace y' F2 y -> L2
                                else if (lCubeTurnsToClean[i + 1] == "F2" && lCubeTurnsToClean[i + 2] == "y")
                                {
                                    lCubeTurnsToClean[i] = cNone;
                                    lCubeTurnsToClean[i + 1] = "L2";
                                    lCubeTurnsToClean[i + 2] = cNone;
                                }

                                // Replace y' B y -> R
                                else if (lCubeTurnsToClean[i + 1] == "B" && lCubeTurnsToClean[i + 2] == "y")
                                {
                                    lCubeTurnsToClean[i] = cNone;
                                    lCubeTurnsToClean[i + 1] = "R";
                                    lCubeTurnsToClean[i + 2] = cNone;
                                }

                                // Replace y' B' y -> R'
                                else if (lCubeTurnsToClean[i + 1] == "B'" && lCubeTurnsToClean[i + 2] == "y")
                                {
                                    lCubeTurnsToClean[i] = cNone;
                                    lCubeTurnsToClean[i + 1] = "R'";
                                    lCubeTurnsToClean[i + 2] = cNone;
                                }

                                // Replace y' B2 y -> R2
                                else if (lCubeTurnsToClean[i + 1] == "B2" && lCubeTurnsToClean[i + 2] == "y")
                                {
                                    lCubeTurnsToClean[i] = cNone;
                                    lCubeTurnsToClean[i + 1] = "R2";
                                    lCubeTurnsToClean[i + 2] = cNone;
                                }

                                // Replace y' L y -> B
                                else if (lCubeTurnsToClean[i + 1] == "L" && lCubeTurnsToClean[i + 2] == "y")
                                {
                                    lCubeTurnsToClean[i] = cNone;
                                    lCubeTurnsToClean[i + 1] = "B";
                                    lCubeTurnsToClean[i + 2] = cNone;
                                }

                                // Replace y' L' y -> B'
                                else if (lCubeTurnsToClean[i + 1] == "L'" && lCubeTurnsToClean[i + 2] == "y")
                                {
                                    lCubeTurnsToClean[i] = cNone;
                                    lCubeTurnsToClean[i + 1] = "B'";
                                    lCubeTurnsToClean[i + 2] = cNone;
                                }

                                // Replace y' L2 y -> B2
                                else if (lCubeTurnsToClean[i + 1] == "L2" && lCubeTurnsToClean[i + 2] == "y")
                                {
                                    lCubeTurnsToClean[i] = cNone;
                                    lCubeTurnsToClean[i + 1] = "B2";
                                    lCubeTurnsToClean[i + 2] = cNone;
                                }

                                // Replace y' R y -> F
                                else if (lCubeTurnsToClean[i + 1] == "R" && lCubeTurnsToClean[i + 2] == "y")
                                {
                                    lCubeTurnsToClean[i] = cNone;
                                    lCubeTurnsToClean[i + 1] = "F";
                                    lCubeTurnsToClean[i + 2] = cNone;
                                }

                                // Replace y' R' y -> F'
                                else if (lCubeTurnsToClean[i + 1] == "R'" && lCubeTurnsToClean[i + 2] == "y")
                                {
                                    lCubeTurnsToClean[i] = cNone;
                                    lCubeTurnsToClean[i + 1] = "F'";
                                    lCubeTurnsToClean[i + 2] = cNone;
                                }

                                // Replace y' R2 y -> F2
                                else if (lCubeTurnsToClean[i + 1] == "R2" && lCubeTurnsToClean[i + 2] == "y")
                                {
                                    lCubeTurnsToClean[i] = cNone;
                                    lCubeTurnsToClean[i + 1] = "F2";
                                    lCubeTurnsToClean[i + 2] = cNone;
                                }

                                //// Whole cube turns (y' face y')
                                // Replace y' U y' -> y2 U
                                // Replace y' U' y' -> y2 U'
                                // Replace y' U2 y' -> y2 U2
                                else if (lCubeTurnsToClean[i + 1].StartsWith('U') && lCubeTurnsToClean[i + 2] == "y'")
                                {
                                    lCubeTurnsToClean[i] = "y2";
                                    lCubeTurnsToClean[i + 2] = cNone;
                                }

                                // Replace y' D y' -> y2 D
                                // Replace y' D' y' -> y2 D'
                                // Replace y' D2 y' -> y2 D2
                                else if (lCubeTurnsToClean[i + 1].StartsWith('D') && lCubeTurnsToClean[i + 2] == "y'")
                                {
                                    lCubeTurnsToClean[i] = "y2";
                                    lCubeTurnsToClean[i + 2] = cNone;
                                }

                                // Replace y' F y' -> y2 R
                                else if (lCubeTurnsToClean[i + 1] == "F" && lCubeTurnsToClean[i + 2] == "y'")
                                {
                                    lCubeTurnsToClean[i] = "y2";
                                    lCubeTurnsToClean[i + 1] = "R";
                                    lCubeTurnsToClean[i + 2] = cNone;
                                }

                                // Replace y' F' y' -> y2 R'
                                else if (lCubeTurnsToClean[i + 1] == "F'" && lCubeTurnsToClean[i + 2] == "y'")
                                {
                                    lCubeTurnsToClean[i] = "y2";
                                    lCubeTurnsToClean[i + 1] = "R'";
                                    lCubeTurnsToClean[i + 2] = cNone;
                                }

                                // Replace y' F2 y' -> y2 R2
                                else if (lCubeTurnsToClean[i + 1] == "F2" && lCubeTurnsToClean[i + 2] == "y'")
                                {
                                    lCubeTurnsToClean[i] = "y2";
                                    lCubeTurnsToClean[i + 1] = "R2";
                                    lCubeTurnsToClean[i + 2] = cNone;
                                }

                                // Replace y' B y' -> y2 L
                                else if (lCubeTurnsToClean[i + 1] == "B" && lCubeTurnsToClean[i + 2] == "y'")
                                {
                                    lCubeTurnsToClean[i] = "y2";
                                    lCubeTurnsToClean[i + 1] = "L";
                                    lCubeTurnsToClean[i + 2] = cNone;
                                }

                                // Replace y' B' y' -> y2 L'
                                else if (lCubeTurnsToClean[i + 1] == "B'" && lCubeTurnsToClean[i + 2] == "y'")
                                {
                                    lCubeTurnsToClean[i] = "y2";
                                    lCubeTurnsToClean[i + 1] = "L'";
                                    lCubeTurnsToClean[i + 2] = cNone;
                                }

                                // Replace y' B2 y' -> y2 L2
                                else if (lCubeTurnsToClean[i + 1] == "B2" && lCubeTurnsToClean[i + 2] == "y'")
                                {
                                    lCubeTurnsToClean[i] = "y2";
                                    lCubeTurnsToClean[i + 1] = "L2";
                                    lCubeTurnsToClean[i + 2] = cNone;
                                }

                                // Replace y' L y' -> y2 F
                                else if (lCubeTurnsToClean[i + 1] == "L" && lCubeTurnsToClean[i + 2] == "y'")
                                {
                                    lCubeTurnsToClean[i] = "y2";
                                    lCubeTurnsToClean[i + 1] = "F";
                                    lCubeTurnsToClean[i + 2] = cNone;
                                }

                                // Replace y' L' y' -> y2 F'
                                else if (lCubeTurnsToClean[i + 1] == "L'" && lCubeTurnsToClean[i + 2] == "y'")
                                {
                                    lCubeTurnsToClean[i] = "y2";
                                    lCubeTurnsToClean[i + 1] = "F'";
                                    lCubeTurnsToClean[i + 2] = cNone;
                                }

                                // Replace y' L2 y' -> y2 F2
                                else if (lCubeTurnsToClean[i + 1] == "L2" && lCubeTurnsToClean[i + 2] == "y'")
                                {
                                    lCubeTurnsToClean[i] = "y2";
                                    lCubeTurnsToClean[i + 1] = "F2";
                                    lCubeTurnsToClean[i + 2] = cNone;
                                }

                                // Replace y' R y' -> y2 B
                                else if (lCubeTurnsToClean[i + 1] == "R" && lCubeTurnsToClean[i + 2] == "y'")
                                {
                                    lCubeTurnsToClean[i] = "y2";
                                    lCubeTurnsToClean[i + 1] = "B";
                                    lCubeTurnsToClean[i + 2] = cNone;
                                }

                                // Replace y' R' y' -> y2 B'
                                else if (lCubeTurnsToClean[i + 1] == "R'" && lCubeTurnsToClean[i + 2] == "y'")
                                {
                                    lCubeTurnsToClean[i] = "y2";
                                    lCubeTurnsToClean[i + 1] = "B'";
                                    lCubeTurnsToClean[i + 2] = cNone;
                                }

                                // Replace y' R2 y' -> y2 B2
                                else if (lCubeTurnsToClean[i + 1] == "R2" && lCubeTurnsToClean[i + 2] == "y'")
                                {
                                    lCubeTurnsToClean[i] = "y2";
                                    lCubeTurnsToClean[i + 1] = "B2";
                                    lCubeTurnsToClean[i + 2] = cNone;
                                }

                                //// Whole cube turns (y' face y2)
                                // Replace y' U y2 -> y U
                                // Replace y' U' y2 -> y U'
                                // Replace y' U2 y2 -> y U2
                                else if (lCubeTurnsToClean[i + 1].StartsWith('U') && lCubeTurnsToClean[i + 2] == "y2")
                                {
                                    lCubeTurnsToClean[i] = "y";
                                    lCubeTurnsToClean[i + 2] = cNone;
                                }

                                // Replace y' D y2 -> y D
                                // Replace y' D' y2 -> y D'
                                // Replace y' D2 y2 -> y D2
                                else if (lCubeTurnsToClean[i + 1].StartsWith('D') && lCubeTurnsToClean[i + 2] == "y2")
                                {
                                    lCubeTurnsToClean[i] = "y";
                                    lCubeTurnsToClean[i + 2] = cNone;
                                }

                                // Replace y' F y2 -> y B
                                else if (lCubeTurnsToClean[i + 1] == "F" && lCubeTurnsToClean[i + 2] == "y2")
                                {
                                    lCubeTurnsToClean[i] = "y";
                                    lCubeTurnsToClean[i + 1] = "B";
                                    lCubeTurnsToClean[i + 2] = cNone;
                                }

                                // Replace y' F' y2 -> y B'
                                else if (lCubeTurnsToClean[i + 1] == "F'" && lCubeTurnsToClean[i + 2] == "y2")
                                {
                                    lCubeTurnsToClean[i] = "y";
                                    lCubeTurnsToClean[i + 1] = "B'";
                                    lCubeTurnsToClean[i + 2] = cNone;
                                }

                                // Replace y' F2 y2 -> y B2
                                else if (lCubeTurnsToClean[i + 1] == "F2" && lCubeTurnsToClean[i + 2] == "y2")
                                {
                                    lCubeTurnsToClean[i] = "y";
                                    lCubeTurnsToClean[i + 1] = "B2";
                                    lCubeTurnsToClean[i + 2] = cNone;
                                }

                                // Replace y' B y2 -> y F
                                else if (lCubeTurnsToClean[i + 1] == "B" && lCubeTurnsToClean[i + 2] == "y2")
                                {
                                    lCubeTurnsToClean[i] = "y";
                                    lCubeTurnsToClean[i + 1] = "F";
                                    lCubeTurnsToClean[i + 2] = cNone;
                                }

                                // Replace y' B' y2 -> y F'
                                else if (lCubeTurnsToClean[i + 1] == "B'" && lCubeTurnsToClean[i + 2] == "y2")
                                {
                                    lCubeTurnsToClean[i] = "y";
                                    lCubeTurnsToClean[i + 1] = "F'";
                                    lCubeTurnsToClean[i + 2] = cNone;
                                }

                                // Replace y' B2 y2 -> y F2
                                else if (lCubeTurnsToClean[i + 1] == "B2" && lCubeTurnsToClean[i + 2] == "y2")
                                {
                                    lCubeTurnsToClean[i] = "y";
                                    lCubeTurnsToClean[i + 1] = "F2";
                                    lCubeTurnsToClean[i + 2] = cNone;
                                }

                                // Replace y' L y2 -> y R
                                else if (lCubeTurnsToClean[i + 1] == "L" && lCubeTurnsToClean[i + 2] == "y2")
                                {
                                    lCubeTurnsToClean[i] = "y";
                                    lCubeTurnsToClean[i + 1] = "R";
                                    lCubeTurnsToClean[i + 2] = cNone;
                                }

                                // Replace y' L' y2 -> y R'
                                else if (lCubeTurnsToClean[i + 1] == "L'" && lCubeTurnsToClean[i + 2] == "y2")
                                {
                                    lCubeTurnsToClean[i] = "y";
                                    lCubeTurnsToClean[i + 1] = "R'";
                                    lCubeTurnsToClean[i + 2] = cNone;
                                }

                                // Replace y' L2 y2 -> y R2
                                else if (lCubeTurnsToClean[i + 1] == "L2" && lCubeTurnsToClean[i + 2] == "y2")
                                {
                                    lCubeTurnsToClean[i] = "y";
                                    lCubeTurnsToClean[i + 1] = "R2";
                                    lCubeTurnsToClean[i + 2] = cNone;
                                }

                                // Replace y' R y2 -> y L
                                else if (lCubeTurnsToClean[i + 1] == "R" && lCubeTurnsToClean[i + 2] == "y2")
                                {
                                    lCubeTurnsToClean[i] = "y";
                                    lCubeTurnsToClean[i + 1] = "L";
                                    lCubeTurnsToClean[i + 2] = cNone;
                                }

                                // Replace y' R' y2 -> y L'
                                else if (lCubeTurnsToClean[i + 1] == "R'" && lCubeTurnsToClean[i + 2] == "y2")
                                {
                                    lCubeTurnsToClean[i] = "y";
                                    lCubeTurnsToClean[i + 1] = "L'";
                                    lCubeTurnsToClean[i + 2] = cNone;
                                }

                                // Replace y' R2 y2 -> y L2
                                else if (lCubeTurnsToClean[i + 1] == "R2" && lCubeTurnsToClean[i + 2] == "y2")
                                {
                                    lCubeTurnsToClean[i] = "y";
                                    lCubeTurnsToClean[i + 1] = "L2";
                                    lCubeTurnsToClean[i + 2] = cNone;
                                }
                            }

                            //// Whole cube turns (x, y, z) starting with "y2"
                            else if (lCubeTurnsToClean[i] == "y2")
                            {
                                // Replace y2 x y2 -> x'
                                if (lCubeTurnsToClean[i + 1] == "x" && lCubeTurnsToClean[i + 2] == "y2")
                                {
                                    lCubeTurnsToClean[i] = cNone;
                                    lCubeTurnsToClean[i + 1] = "x'";
                                    lCubeTurnsToClean[i + 2] = cNone;
                                }

                                // Replace y2 x y -> x' y'
                                else if (lCubeTurnsToClean[i + 1] == "x" && lCubeTurnsToClean[i + 2] == "y")
                                {
                                    lCubeTurnsToClean[i] = cNone;
                                    lCubeTurnsToClean[i + 1] = "x'";
                                    lCubeTurnsToClean[i + 2] = "y'";
                                }

                                // Replace y2 x y' -> y z'
                                else if (lCubeTurnsToClean[i + 1] == "x" && lCubeTurnsToClean[i + 2] == "y'")
                                {
                                    lCubeTurnsToClean[i] = "y";
                                    lCubeTurnsToClean[i + 1] = "z'";
                                    lCubeTurnsToClean[i + 2] = cNone;
                                }

                                //// Whole cube turns (y2 face y)
                                // Replace y2 U y -> y' U
                                // Replace y2 U' y -> y' U'
                                // Replace y2 U2 y -> y' U2
                                else if (lCubeTurnsToClean[i + 1].StartsWith('U') && lCubeTurnsToClean[i + 2] == "y")
                                {
                                    lCubeTurnsToClean[i] = "y'";
                                    lCubeTurnsToClean[i + 2] = cNone;
                                }

                                // Replace y2 D y -> y' D
                                // Replace y2 D' y -> y' D'
                                // Replace y2 D2 y -> y' D2
                                else if (lCubeTurnsToClean[i + 1].StartsWith('D') && lCubeTurnsToClean[i + 2] == "y")
                                {
                                    lCubeTurnsToClean[i] = "y'";
                                    lCubeTurnsToClean[i + 2] = cNone;
                                }

                                // Replace y2 F y -> y' L
                                else if (lCubeTurnsToClean[i + 1] == "F" && lCubeTurnsToClean[i + 2] == "y")
                                {
                                    lCubeTurnsToClean[i] = "y'";
                                    lCubeTurnsToClean[i + 1] = "L";
                                    lCubeTurnsToClean[i + 2] = cNone;
                                }

                                // Replace y2 F' y -> y' L'
                                else if (lCubeTurnsToClean[i + 1] == "F'" && lCubeTurnsToClean[i + 2] == "y")
                                {
                                    lCubeTurnsToClean[i] = "y'";
                                    lCubeTurnsToClean[i + 1] = "L'";
                                    lCubeTurnsToClean[i + 2] = cNone;
                                }

                                // Replace y2 F2 y -> y' L2
                                else if (lCubeTurnsToClean[i + 1] == "F2" && lCubeTurnsToClean[i + 2] == "y")
                                {
                                    lCubeTurnsToClean[i] = "y'";
                                    lCubeTurnsToClean[i + 1] = "L2";
                                    lCubeTurnsToClean[i + 2] = cNone;
                                }

                                // Replace y2 B y -> y' R
                                else if (lCubeTurnsToClean[i + 1] == "B" && lCubeTurnsToClean[i + 2] == "y")
                                {
                                    lCubeTurnsToClean[i] = "y'";
                                    lCubeTurnsToClean[i + 1] = "R";
                                    lCubeTurnsToClean[i + 2] = cNone;
                                }

                                // Replace y2 B' y -> y' R'
                                else if (lCubeTurnsToClean[i + 1] == "B'" && lCubeTurnsToClean[i + 2] == "y")
                                {
                                    lCubeTurnsToClean[i] = "y'";
                                    lCubeTurnsToClean[i + 1] = "R'";
                                    lCubeTurnsToClean[i + 2] = cNone;
                                }

                                // Replace y2 B2 y -> y' R2
                                else if (lCubeTurnsToClean[i + 1] == "B2" && lCubeTurnsToClean[i + 2] == "y")
                                {
                                    lCubeTurnsToClean[i] = "y'";
                                    lCubeTurnsToClean[i + 1] = "R2";
                                    lCubeTurnsToClean[i + 2] = cNone;
                                }

                                // Replace y2 L y -> y' B
                                else if (lCubeTurnsToClean[i + 1] == "L" && lCubeTurnsToClean[i + 2] == "y")
                                {
                                    lCubeTurnsToClean[i] = "y'";
                                    lCubeTurnsToClean[i + 1] = "B";
                                    lCubeTurnsToClean[i + 2] = cNone;
                                }

                                // Replace y2 L' y -> y' B'
                                else if (lCubeTurnsToClean[i + 1] == "L'" && lCubeTurnsToClean[i + 2] == "y")
                                {
                                    lCubeTurnsToClean[i] = "y'";
                                    lCubeTurnsToClean[i + 1] = "B'";
                                    lCubeTurnsToClean[i + 2] = cNone;
                                }

                                // Replace y2 L2 y -> y' B2
                                else if (lCubeTurnsToClean[i + 1] == "L2" && lCubeTurnsToClean[i + 2] == "y")
                                {
                                    lCubeTurnsToClean[i] = "y'";
                                    lCubeTurnsToClean[i + 1] = "B2";
                                    lCubeTurnsToClean[i + 2] = cNone;
                                }

                                // Replace y2 R y -> y' F
                                else if (lCubeTurnsToClean[i + 1] == "R" && lCubeTurnsToClean[i + 2] == "y")
                                {
                                    lCubeTurnsToClean[i] = "y'";
                                    lCubeTurnsToClean[i + 1] = "F";
                                    lCubeTurnsToClean[i + 2] = cNone;
                                }

                                // Replace y2 R' y -> y' F'
                                else if (lCubeTurnsToClean[i + 1] == "R'" && lCubeTurnsToClean[i + 2] == "y")
                                {
                                    lCubeTurnsToClean[i] = "y'";
                                    lCubeTurnsToClean[i + 1] = "F'";
                                    lCubeTurnsToClean[i + 2] = cNone;
                                }

                                // Replace y2 R2 y -> y' F2
                                else if (lCubeTurnsToClean[i + 1] == "R2" && lCubeTurnsToClean[i + 2] == "y")
                                {
                                    lCubeTurnsToClean[i] = "y'";
                                    lCubeTurnsToClean[i + 1] = "F2";
                                    lCubeTurnsToClean[i + 2] = cNone;
                                }

                                //// Whole cube turns (y2 face y')
                                // Replace y2 U y' -> y U
                                // Replace y2 U' y' -> y U'
                                // Replace y2 U2 y' -> y U2
                                else if (lCubeTurnsToClean[i + 1].StartsWith('U') && lCubeTurnsToClean[i + 2] == "y'")
                                {
                                    lCubeTurnsToClean[i] = "y";
                                    lCubeTurnsToClean[i + 2] = cNone;
                                }

                                // Replace y2 D y' -> y D
                                // Replace y2 D' y' -> y D'
                                // Replace y2 D2 y' -> y D2
                                else if (lCubeTurnsToClean[i + 1].StartsWith('D') && lCubeTurnsToClean[i + 2] == "y'")
                                {
                                    lCubeTurnsToClean[i] = "y";
                                    lCubeTurnsToClean[i + 2] = cNone;
                                }

                                // Replace y2 F y' -> y R
                                else if (lCubeTurnsToClean[i + 1] == "F" && lCubeTurnsToClean[i + 2] == "y'")
                                {
                                    lCubeTurnsToClean[i] = "y";
                                    lCubeTurnsToClean[i + 1] = "R";
                                    lCubeTurnsToClean[i + 2] = cNone;
                                }

                                // Replace y2 F' y' -> y R'
                                else if (lCubeTurnsToClean[i + 1] == "F'" && lCubeTurnsToClean[i + 2] == "y'")
                                {
                                    lCubeTurnsToClean[i] = "y";
                                    lCubeTurnsToClean[i + 1] = "R'";
                                    lCubeTurnsToClean[i + 2] = cNone;
                                }

                                // Replace y2 F2 y' -> y R2
                                else if (lCubeTurnsToClean[i + 1] == "F2" && lCubeTurnsToClean[i + 2] == "y'")
                                {
                                    lCubeTurnsToClean[i] = "y";
                                    lCubeTurnsToClean[i + 1] = "R2";
                                    lCubeTurnsToClean[i + 2] = cNone;
                                }

                                // Replace y2 B y' -> y L
                                else if (lCubeTurnsToClean[i + 1] == "B" && lCubeTurnsToClean[i + 2] == "y'")
                                {
                                    lCubeTurnsToClean[i] = "y";
                                    lCubeTurnsToClean[i + 1] = "L";
                                    lCubeTurnsToClean[i + 2] = cNone;
                                }

                                // Replace y2 B' y' -> y L'
                                else if (lCubeTurnsToClean[i + 1] == "B'" && lCubeTurnsToClean[i + 2] == "y'")
                                {
                                    lCubeTurnsToClean[i] = "y";
                                    lCubeTurnsToClean[i + 1] = "L'";
                                    lCubeTurnsToClean[i + 2] = cNone;
                                }

                                // Replace y2 B2 y' -> y L2
                                else if (lCubeTurnsToClean[i + 1] == "B2" && lCubeTurnsToClean[i + 2] == "y'")
                                {
                                    lCubeTurnsToClean[i] = "y";
                                    lCubeTurnsToClean[i + 1] = "L2";
                                    lCubeTurnsToClean[i + 2] = cNone;
                                }

                                // Replace y2 L y' -> y F
                                else if (lCubeTurnsToClean[i + 1] == "L" && lCubeTurnsToClean[i + 2] == "y'")
                                {
                                    lCubeTurnsToClean[i] = "y";
                                    lCubeTurnsToClean[i + 1] = "F";
                                    lCubeTurnsToClean[i + 2] = cNone;
                                }

                                // Replace y2 L' y' -> y F'
                                else if (lCubeTurnsToClean[i + 1] == "L'" && lCubeTurnsToClean[i + 2] == "y'")
                                {
                                    lCubeTurnsToClean[i] = "y";
                                    lCubeTurnsToClean[i + 1] = "F'";
                                    lCubeTurnsToClean[i + 2] = cNone;
                                }

                                // Replace y2 L2 y' -> y F2
                                else if (lCubeTurnsToClean[i + 1] == "L2" && lCubeTurnsToClean[i + 2] == "y'")
                                {
                                    lCubeTurnsToClean[i] = "y";
                                    lCubeTurnsToClean[i + 1] = "F2";
                                    lCubeTurnsToClean[i + 2] = cNone;
                                }

                                // Replace y2 R y' -> y B
                                else if (lCubeTurnsToClean[i + 1] == "R" && lCubeTurnsToClean[i + 2] == "y'")
                                {
                                    lCubeTurnsToClean[i] = "y";
                                    lCubeTurnsToClean[i + 1] = "B";
                                    lCubeTurnsToClean[i + 2] = cNone;
                                }

                                // Replace y2 R' y' -> y B'
                                else if (lCubeTurnsToClean[i + 1] == "R'" && lCubeTurnsToClean[i + 2] == "y'")
                                {
                                    lCubeTurnsToClean[i] = "y";
                                    lCubeTurnsToClean[i + 1] = "B'";
                                    lCubeTurnsToClean[i + 2] = cNone;
                                }

                                // Replace y2 R2 y' -> y B2
                                else if (lCubeTurnsToClean[i + 1] == "R2" && lCubeTurnsToClean[i + 2] == "y'")
                                {
                                    lCubeTurnsToClean[i] = "y";
                                    lCubeTurnsToClean[i + 1] = "B2";
                                    lCubeTurnsToClean[i + 2] = cNone;
                                }

                                //// Whole cube turns (y2 face y2)
                                // Replace y2 U y2 -> U
                                // Replace y2 U' y2 -> U'
                                // Replace y2 U2 y2 -> U2
                                else if (lCubeTurnsToClean[i + 1].StartsWith('U') && lCubeTurnsToClean[i + 2] == "y2")
                                {
                                    lCubeTurnsToClean[i] = cNone;
                                    lCubeTurnsToClean[i + 2] = cNone;
                                }

                                // Replace y2 D y2 -> D
                                // Replace y2 D' y2 -> D'
                                // Replace y2 D2 y2 -> D2
                                else if (lCubeTurnsToClean[i + 1].StartsWith('D') && lCubeTurnsToClean[i + 2] == "y2")
                                {
                                    lCubeTurnsToClean[i] = cNone;
                                    lCubeTurnsToClean[i + 2] = cNone;
                                }

                                // Replace y2 F y2 -> B
                                else if (lCubeTurnsToClean[i + 1] == "F" && lCubeTurnsToClean[i + 2] == "y2")
                                {
                                    lCubeTurnsToClean[i] = cNone;
                                    lCubeTurnsToClean[i + 1] = "B";
                                    lCubeTurnsToClean[i + 2] = cNone;
                                }

                                // Replace y2 F' y2 -> B'
                                else if (lCubeTurnsToClean[i + 1] == "F'" && lCubeTurnsToClean[i + 2] == "y2")
                                {
                                    lCubeTurnsToClean[i] = cNone;
                                    lCubeTurnsToClean[i + 1] = "B'";
                                    lCubeTurnsToClean[i + 2] = cNone;
                                }

                                // Replace y2 F2 y2 -> B2
                                else if (lCubeTurnsToClean[i + 1] == "F2" && lCubeTurnsToClean[i + 2] == "y2")
                                {
                                    lCubeTurnsToClean[i] = cNone;
                                    lCubeTurnsToClean[i + 1] = "B2";
                                    lCubeTurnsToClean[i + 2] = cNone;
                                }

                                // Replace y2 B y2 -> F
                                else if (lCubeTurnsToClean[i + 1] == "B" && lCubeTurnsToClean[i + 2] == "y2")
                                {
                                    lCubeTurnsToClean[i] = cNone;
                                    lCubeTurnsToClean[i + 1] = "F";
                                    lCubeTurnsToClean[i + 2] = cNone;
                                }

                                // Replace y2 B' y2 -> F'
                                else if (lCubeTurnsToClean[i + 1] == "B'" && lCubeTurnsToClean[i + 2] == "y2")
                                {
                                    lCubeTurnsToClean[i] = cNone;
                                    lCubeTurnsToClean[i + 1] = "F'";
                                    lCubeTurnsToClean[i + 2] = cNone;
                                }

                                // Replace y2 B2 y2 -> F2
                                else if (lCubeTurnsToClean[i + 1] == "B2" && lCubeTurnsToClean[i + 2] == "y2")
                                {
                                    lCubeTurnsToClean[i] = cNone;
                                    lCubeTurnsToClean[i + 1] = "F2";
                                    lCubeTurnsToClean[i + 2] = cNone;
                                }

                                // Replace y2 L y2 -> R
                                else if (lCubeTurnsToClean[i + 1] == "L" && lCubeTurnsToClean[i + 2] == "y2")
                                {
                                    lCubeTurnsToClean[i] = cNone;
                                    lCubeTurnsToClean[i + 1] = "R";
                                    lCubeTurnsToClean[i + 2] = cNone;
                                }

                                // Replace y2 L' y2 -> R'
                                else if (lCubeTurnsToClean[i + 1] == "L'" && lCubeTurnsToClean[i + 2] == "y2")
                                {
                                    lCubeTurnsToClean[i] = cNone;
                                    lCubeTurnsToClean[i + 1] = "R'";
                                    lCubeTurnsToClean[i + 2] = cNone;
                                }

                                // Replace y2 L2 y2 -> R2
                                else if (lCubeTurnsToClean[i + 1] == "L2" && lCubeTurnsToClean[i + 2] == "y2")
                                {
                                    lCubeTurnsToClean[i] = cNone;
                                    lCubeTurnsToClean[i + 1] = "R2";
                                    lCubeTurnsToClean[i + 2] = cNone;
                                }

                                // Replace y2 R y2 -> L
                                else if (lCubeTurnsToClean[i + 1] == "R" && lCubeTurnsToClean[i + 2] == "y2")
                                {
                                    lCubeTurnsToClean[i] = cNone;
                                    lCubeTurnsToClean[i + 1] = "L";
                                    lCubeTurnsToClean[i + 2] = cNone;
                                }

                                // Replace y2 R' y2 -> L'
                                else if (lCubeTurnsToClean[i + 1] == "R'" && lCubeTurnsToClean[i + 2] == "y2")
                                {
                                    lCubeTurnsToClean[i] = cNone;
                                    lCubeTurnsToClean[i + 1] = "L'";
                                    lCubeTurnsToClean[i + 2] = cNone;
                                }

                                // Replace y2 R2 y2 -> L2
                                else if (lCubeTurnsToClean[i + 1] == "R2" && lCubeTurnsToClean[i + 2] == "y2")
                                {
                                    lCubeTurnsToClean[i] = cNone;
                                    lCubeTurnsToClean[i + 1] = "L2";
                                    lCubeTurnsToClean[i + 2] = cNone;
                                }
                            }

                            //// Delete the first and the third 1/2 turn who are the same and leave the second turn that is on the opposite face
                            // Replace U2 D U2 -> D
                            else if (lCubeTurnsToClean[i] == "U2" && lCubeTurnsToClean[i + 1].StartsWith('D') && lCubeTurnsToClean[i + 2] == "U2")
                            {
                                lCubeTurnsToClean[i] = cNone;
                                lCubeTurnsToClean[i + 2] = cNone;
                            }

                            // Replace D2 U D2 -> U
                            else if (lCubeTurnsToClean[i] == "D2" && lCubeTurnsToClean[i + 1].StartsWith('U') && lCubeTurnsToClean[i + 2] == "D2")
                            {
                                lCubeTurnsToClean[i] = cNone;
                                lCubeTurnsToClean[i + 2] = cNone;
                            }

                            // Replace F2 B F2 -> B
                            else if (lCubeTurnsToClean[i] == "F2" && lCubeTurnsToClean[i + 1].StartsWith('B') && lCubeTurnsToClean[i + 2] == "F2")
                            {
                                lCubeTurnsToClean[i] = cNone;
                                lCubeTurnsToClean[i + 2] = cNone;
                            }

                            // Replace B2 F B2 -> F
                            else if (lCubeTurnsToClean[i] == "B2" && lCubeTurnsToClean[i + 1].StartsWith('F') && lCubeTurnsToClean[i + 2] == "B2")
                            {
                                lCubeTurnsToClean[i] = cNone;
                                lCubeTurnsToClean[i + 2] = cNone;
                            }

                            // Replace L2 R L2 -> R
                            else if (lCubeTurnsToClean[i] == "L2" && lCubeTurnsToClean[i + 1].StartsWith('R') && lCubeTurnsToClean[i + 2] == "L2")
                            {
                                lCubeTurnsToClean[i] = cNone;
                                lCubeTurnsToClean[i + 2] = cNone;
                            }

                            // Replace R2 L R2 -> L
                            else if (lCubeTurnsToClean[i] == "R2" && lCubeTurnsToClean[i + 1].StartsWith('L') && lCubeTurnsToClean[i + 2] == "R2")
                            {
                                lCubeTurnsToClean[i] = cNone;
                                lCubeTurnsToClean[i + 2] = cNone;
                            }

                            //// Delete the first and the third 1/4 turn who are reversed and leave the second turn that is on the opposite face
                            // Replace U D U' -> D
                            else if (lCubeTurnsToClean[i] == "U" && lCubeTurnsToClean[i + 1].StartsWith('D') && lCubeTurnsToClean[i + 2] == "U'")
                            {
                                lCubeTurnsToClean[i] = cNone;
                                lCubeTurnsToClean[i + 2] = cNone;
                            }

                            // Replace U' D U -> D
                            else if (lCubeTurnsToClean[i] == "U'" && lCubeTurnsToClean[i + 1].StartsWith('D') && lCubeTurnsToClean[i + 2] == "U")
                            {
                                lCubeTurnsToClean[i] = cNone;
                                lCubeTurnsToClean[i + 2] = cNone;
                            }

                            // Replace D U D' -> U
                            else if (lCubeTurnsToClean[i] == "D" && lCubeTurnsToClean[i + 1].StartsWith('U') && lCubeTurnsToClean[i + 2] == "D'")
                            {
                                lCubeTurnsToClean[i] = cNone;
                                lCubeTurnsToClean[i + 2] = cNone;
                            }

                            // Replace D' U D -> U
                            else if (lCubeTurnsToClean[i] == "D'" && lCubeTurnsToClean[i + 1].StartsWith('U') && lCubeTurnsToClean[i + 2] == "D")
                            {
                                lCubeTurnsToClean[i] = cNone;
                                lCubeTurnsToClean[i + 2] = cNone;
                            }

                            // Replace F B F' -> B
                            else if (lCubeTurnsToClean[i] == "F" && lCubeTurnsToClean[i + 1].StartsWith('B') && lCubeTurnsToClean[i + 2] == "F'")
                            {
                                lCubeTurnsToClean[i] = cNone;
                                lCubeTurnsToClean[i + 2] = cNone;
                            }

                            // Replace F' B F -> B
                            else if (lCubeTurnsToClean[i] == "F'" && lCubeTurnsToClean[i + 1].StartsWith('B') && lCubeTurnsToClean[i + 2] == "F")
                            {
                                lCubeTurnsToClean[i] = cNone;
                                lCubeTurnsToClean[i + 2] = cNone;
                            }

                            // Replace B F B' -> F
                            else if (lCubeTurnsToClean[i] == "B" && lCubeTurnsToClean[i + 1].StartsWith('F') && lCubeTurnsToClean[i + 2] == "B'")
                            {
                                lCubeTurnsToClean[i] = cNone;
                                lCubeTurnsToClean[i + 2] = cNone;
                            }

                            // Replace B' F B -> F
                            else if (lCubeTurnsToClean[i] == "B'" && lCubeTurnsToClean[i + 1].StartsWith('F') && lCubeTurnsToClean[i + 2] == "B")
                            {
                                lCubeTurnsToClean[i] = cNone;
                                lCubeTurnsToClean[i + 2] = cNone;
                            }

                            // Replace L R L' -> R
                            else if (lCubeTurnsToClean[i] == "L" && lCubeTurnsToClean[i + 1].StartsWith('R') && lCubeTurnsToClean[i + 2] == "L'")
                            {
                                lCubeTurnsToClean[i] = cNone;
                                lCubeTurnsToClean[i + 2] = cNone;
                            }

                            // Replace L' R L -> R
                            else if (lCubeTurnsToClean[i] == "L'" && lCubeTurnsToClean[i + 1].StartsWith('R') && lCubeTurnsToClean[i + 2] == "L")
                            {
                                lCubeTurnsToClean[i] = cNone;
                                lCubeTurnsToClean[i + 2] = cNone;
                            }

                            // Replace R L R' -> L
                            else if (lCubeTurnsToClean[i] == "R" && lCubeTurnsToClean[i + 1].StartsWith('L') && lCubeTurnsToClean[i + 2] == "R'")
                            {
                                lCubeTurnsToClean[i] = cNone;
                                lCubeTurnsToClean[i + 2] = cNone;
                            }

                            // Replace R' L R -> L
                            else if (lCubeTurnsToClean[i] == "R'" && lCubeTurnsToClean[i + 1].StartsWith('L') && lCubeTurnsToClean[i + 2] == "R")
                            {
                                lCubeTurnsToClean[i] = cNone;
                                lCubeTurnsToClean[i + 2] = cNone;
                            }
                        }
                    }

                    //// Remove the last turn if it is turning the whole cube (starts with x, y or z)
                    if (lCubeTurnsToClean[^1].StartsWith('x') || lCubeTurnsToClean[^1].StartsWith('y') || lCubeTurnsToClean[^1].StartsWith('z'))
                    {
                        lCubeTurnsToClean[^1] = cNone;
                    }

                    Debug.WriteLine($"nNumberCleanings: {nNumberCleanings}, lCubeTurnsToClean: {lCubeTurnsToClean.Count}");  // For testing purposes

                    if (lCubeTurnsToClean.Contains(cNone))
                    {
                        // Remove the items with 'None'
                        lCubeTurnsToClean.RemoveAll(x => x == cNone);
                    }
                    else if (nNumberCleanings > 1)
                    {
                        // If the for loop was executed 2 times, break the loop
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
#if DEBUG                
                Debug.WriteLine("Error in method CleanListCubeTurns: " + ex.Message);
                _ = Application.Current!.Windows[0].Page!.DisplayAlert(CubeLang.ErrorTitle_Text, "CleanListCubeTurns: " + ex.Message, CubeLang.ButtonClose_Text);
#endif
                // Restore the list with the original cube turns
                lCubeTurnsToClean.Clear();
                lCubeTurnsToClean.AddRange(lCubeTurnsToCleanOriginal);

                return;
            }

            //// Stop the stopwatch
            //TimeSpan delta = Stopwatch.GetElapsedTime(startTime);
            //_ = Application.Current!.Windows[0].Page!.DisplayAlert("Time", $"Time elapsed (hh:mm:ss.xxxxxxx): {delta}", "OK");
#if DEBUG
            //// Save the list with the cube turns after the cleaning to a file, for testing purposes
            if (bCubeTurnsSave)
            {
                _ = ClassFileOperations.CubeTurnsSave("CubeTurnsAfterLastClean.txt");
            }
#endif
        }
    }
}
