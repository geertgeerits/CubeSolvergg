using static CubeSolver.Globals;

namespace CubeSolver
{
    internal sealed class ClassTestCubeTurns
    {
        /// <summary>
        /// Test the turns of the cube
        /// </summary>
        /// <returns></returns>
        public static async Task<bool> TestCubeTurnsAsync()
        {
            await TestCubeTurnsLetterAsync();
            //await TestCubeTurnsToCleanAsync();
            //await TestSolveCubeTurnsCFOP();
            //await TestSolveCubeTurnsBasic();
            //await TestSolveCubeTurnsDaisy();
            //await TestSolveCubeTurnsCross();
            //await TestSolveCubeTurnsCommon();

            return true;
        }

        /// <summary>
        /// Use turn letters to test the turns of the cube
        /// </summary>
        /// <returns></returns>
        private static async Task<bool> TestCubeTurnsLetterAsync()
        {
            // Test the face turns
            await MakeTurnAsync("U U' U2");
            await MakeTurnAsync("D D' D2");
            await MakeTurnAsync("F F' F2");
            await MakeTurnAsync("B B' B2");
            await MakeTurnAsync("L L' L2");
            await MakeTurnAsync("R R' R2");

            // Test the middle layer turns
            await MakeTurnAsync("M M' M2");
            await MakeTurnAsync("E E' E2");
            await MakeTurnAsync("S S' S2");

            // Test two layers at the same time
            await MakeTurnAsync("u u' u2");
            await MakeTurnAsync("d d' d2");
            await MakeTurnAsync("f f' f2");
            await MakeTurnAsync("b b' b2");
            await MakeTurnAsync("l l' l2");
            await MakeTurnAsync("r r' r2");

            // Test the cube turns
            await MakeTurnAsync("x x' x2");
            await MakeTurnAsync("y y' y2");
            await MakeTurnAsync("z z' z2");

            // Test single cube turns
            await MakeTurnAsync("U");
            await MakeTurnAsync("U'");
            await MakeTurnAsync("U2");

            await MakeTurnAsync("y");
            await MakeTurnAsync("y'");
            await MakeTurnAsync("y2");

            // Test invalid cube turns
            await MakeTurnAsync("U ");
            await MakeTurnAsync(" F");
            await MakeTurnAsync("L RR R2' U D");

            return true;
        }

        /// <summary>
        /// Test the cleaning process of the turns
        /// </summary>
        /// <returns></returns>
        private static async Task<bool> TestCubeTurnsToCleanAsync()
        {
            await MakeTurnAsync("R");
            await MakeTurnAsync("L");

            await MakeTurnAsync("U U");       // -> U2
            await MakeTurnAsync("U' U'");     // -> U2
            await MakeTurnAsync("U2 U2");     // -> None

            await MakeTurnAsync("U U'");      // -> None
            await MakeTurnAsync("U' U");      // -> None
            await MakeTurnAsync("U U2");      // -> U'
            await MakeTurnAsync("U2 U");      // -> U'
            await MakeTurnAsync("U' U2");     // -> U
            await MakeTurnAsync("U2 U'");     // -> U

            await MakeTurnAsync("F");
            await MakeTurnAsync("x");
            await MakeTurnAsync("y'");

            ClassSolveCubeMain.CleanListCubeTurns(lCubeTurns, true);

            return true;
        }

        /// <summary>
        /// Test the turns of the cube for the CFOP method
        /// </summary>
        /// <returns></returns>
        private static async Task<bool> TestSolveCubeTurnsCFOP()
        {
            await MakeTurnAsync("y");
            await MakeTurnAsync("U");
            await MakeTurnAsync("U2");
            await MakeTurnAsync("U'");
            await MakeTurnAsync("U2 F' U' F");
            await MakeTurnAsync("U' R U R'");
            await MakeTurnAsync("F' U F");
            await MakeTurnAsync("U' R U");
            await MakeTurnAsync("R U R' y R U' R'");
            await MakeTurnAsync("R' U R");
            await MakeTurnAsync("U' R U' R'");
            await MakeTurnAsync("R' U2 R y U' L' U L");
            await MakeTurnAsync("R U R'");
            await MakeTurnAsync("U2 F' U' R y U' L' U L");
            await MakeTurnAsync("U' R U2 R' U2 R U' R'");
            await MakeTurnAsync("y L' U' L");
            await MakeTurnAsync("y' U R' U2 R y U' R U R'");
            await MakeTurnAsync("R U R'");
            await MakeTurnAsync("F' U' F");
            await MakeTurnAsync("U R U' R'");
            await MakeTurnAsync("U' F' U F");
            await MakeTurnAsync("U' R U' R' U R U R'");
            await MakeTurnAsync("U F' U F U' F' U' F");
            await MakeTurnAsync("U' R U R' U R U R'");
            await MakeTurnAsync("U F' U' F U' F' U' F");
            await MakeTurnAsync("d R' U2 R d' R U R'");
            await MakeTurnAsync("U' R U2 R' d R' U' R");
            await MakeTurnAsync("R U' R' U d R' U' R");
            await MakeTurnAsync("F' U F U' d' F U F'");
            await MakeTurnAsync("U F' U2 F U F' U2 F");
            await MakeTurnAsync("U' R U2 R' U' R U2 R'");
            await MakeTurnAsync("U F' U' F U F' U2 F");
            await MakeTurnAsync("U' R U R' U' R U2 R'");
            await MakeTurnAsync("R U2 R' U' R U R'");
            await MakeTurnAsync("F' U2 F U F' U' F");
            await MakeTurnAsync("U R U2 R' U R U' R'");
            await MakeTurnAsync("U' F' U2 F U' F' U F");
            await MakeTurnAsync("U2 R U R' U R U' R'");
            await MakeTurnAsync("U2 F' U' F U' F' U F");
            await MakeTurnAsync("R U R' U' U' R U R' U' R U R'");
            await MakeTurnAsync("y' R' U' R U U R' U' R U R' U' R");
            await MakeTurnAsync("U F' U F U F' U2 F");
            await MakeTurnAsync("U' R U' R' U' R U2 R'");
            await MakeTurnAsync("U F' U' F d' F U F'");
            await MakeTurnAsync("U' R U R' d R' U' R");
            await MakeTurnAsync("R U' R' d R' U R");
            await MakeTurnAsync("R U R' U' R U R' U' R U R'");
            await MakeTurnAsync("U R U' R' U' F' U F");
            await MakeTurnAsync("U' F' U F U R U' R'");
            await MakeTurnAsync("F' U F U' F' U F");
            await MakeTurnAsync("R U' R' U R U' R'");
            await MakeTurnAsync("R U R' U' R U R'");
            await MakeTurnAsync("F' U' F U F' U' F");
            await MakeTurnAsync("R U' R' U R U2 R' U R U' R'");
            await MakeTurnAsync("R U' R' U' R U R' U' R U2 R'");
            await MakeTurnAsync("R U R' U' R U' R' U d R' U' R");
            await MakeTurnAsync("R U' R' d R' U' R U' R' U' R");
            await MakeTurnAsync("R U' R' d R' U2 R U R' U2 R");
            await MakeTurnAsync("R U R'");
            await MakeTurnAsync("F' U' F");
            await MakeTurnAsync("U R U' R'");
            await MakeTurnAsync("U' F' U F");
            await MakeTurnAsync("U' R U' R' U");
            await MakeTurnAsync("U F' U F U'");
            await MakeTurnAsync("U' R U R' U");
            await MakeTurnAsync("U F' U' F U'");
            await MakeTurnAsync("U F' U2 F U'");
            await MakeTurnAsync("U' R U2 R' U");
            await MakeTurnAsync("U F' U2 F U'");
            await MakeTurnAsync("U' R U2 R' U");
            await MakeTurnAsync("U F' U' F U'");
            await MakeTurnAsync("U' R U R' U");
            await MakeTurnAsync("R U' R'");
            await MakeTurnAsync("F' U F");
            await MakeTurnAsync("R U2 R'");
            await MakeTurnAsync("F' U2 F");
            await MakeTurnAsync("U R U2 R'");
            await MakeTurnAsync("U' F' U2 F");
            await MakeTurnAsync("U2 R U R'");
            await MakeTurnAsync("U2 F' U' F");
            await MakeTurnAsync("U R U' R'");
            await MakeTurnAsync("U' F' U F");
            await MakeTurnAsync("F' U F");
            await MakeTurnAsync("R U' R'");
            await MakeTurnAsync("R U R'");
            await MakeTurnAsync("F' U' F");
            await MakeTurnAsync("U F' U F");
            await MakeTurnAsync("U' R U' R'");
            await MakeTurnAsync("U F' U' F");
            await MakeTurnAsync("U' R U R'");
            await MakeTurnAsync("R U2 R'");
            await MakeTurnAsync("R U R' U' R U R'");
            await MakeTurnAsync("R U2 R U R' U R U2 R2");
            await MakeTurnAsync("R2 U2 R' U' R U' R' U2 R'");
            await MakeTurnAsync("R U' R' F' L' U2 L F");
            await MakeTurnAsync("R U' R U y' L U' L' B2");
            await MakeTurnAsync("U F' U F U'");
            await MakeTurnAsync("U' R U R' U");
            await MakeTurnAsync("R U' R'");
            await MakeTurnAsync("R U R' U'");
            await MakeTurnAsync("U R' D' F D R");
            await MakeTurnAsync("R' U' R' U R");
            await MakeTurnAsync("y L' R U' L R'");
            await MakeTurnAsync("L' R U L R'");
            await MakeTurnAsync("R U2 R2 U R");
            await MakeTurnAsync("y' R' U2 R2 U' R'");
            await MakeTurnAsync("R' U' R2 U R'");
            await MakeTurnAsync("y' R U R2 U' R");
            await MakeTurnAsync("R2 U' R2 U R2");
            await MakeTurnAsync("y' R2 U R2 U' R2");
            await MakeTurnAsync("R U R'");
            await MakeTurnAsync("F' U' F");
            await MakeTurnAsync("U' F' U F");
            await MakeTurnAsync("U R U' R'");
            await MakeTurnAsync("U R U' R' U' F' U F");
            await MakeTurnAsync("U' F' U F U R U' R'");
            await MakeTurnAsync("F' U F U' F' U F");
            await MakeTurnAsync("R U R' U' R U R'");
            await MakeTurnAsync("R U' R' U R U' R'");
            await MakeTurnAsync("F' U' F U F' U' F");
            await MakeTurnAsync("R U R' U' R U R' U' R U R'");
            await MakeTurnAsync("R U' R' d R' U R");
            await MakeTurnAsync("U F' U F U F' U2 F");
            await MakeTurnAsync("U F' U' F d' F U F'");
            await MakeTurnAsync("U' R U' R' U' R U2 R'");
            await MakeTurnAsync("U' R U R' d R' U' R");
            await MakeTurnAsync("R U' R' U d R' U' R");
            await MakeTurnAsync("F' U F U' d' F U F'");
            await MakeTurnAsync("U F' U2 F U F' U2 F");
            await MakeTurnAsync("U' R U2 R' U' R U2 R'");
            await MakeTurnAsync("U F' U' F U F' U2 F");
            await MakeTurnAsync("U' R U R' U' R U2 R'");
            await MakeTurnAsync("U' R U' R' U R U R'");
            await MakeTurnAsync("U F' U F U' F' U' F");
            await MakeTurnAsync("U' R U R' U R U R'");
            await MakeTurnAsync("U F' U' F U' F' U' F");
            await MakeTurnAsync("U F' U2 F U' R U R'");
            await MakeTurnAsync("U' R U2 R' U F' U' F");
            await MakeTurnAsync("R U R' U' U' R U R' U' R U R'");
            await MakeTurnAsync("y' R' U' R U U R' U' R U R' U' R");
            await MakeTurnAsync("U2 R U R' U R U' R'");
            await MakeTurnAsync("U2 F' U' F U' F' U F");
            await MakeTurnAsync("U R U2 R' U R U' R'");
            await MakeTurnAsync("U' F' U2 F U' F' U F");
            await MakeTurnAsync("R U2 R' U' R U R'");
            await MakeTurnAsync("F' U2 F U F' U' F");
            await MakeTurnAsync("R U' R' d R' U2 R U R' U2 R");
            await MakeTurnAsync("R U' R' U R U2 R' U R U' R'");
            await MakeTurnAsync("U R U' R'");
            await MakeTurnAsync("F R' F' R");
            await MakeTurnAsync("U' R U R' U2 R U' R'");
            await MakeTurnAsync("d R' U' R U2 R' U R");
            await MakeTurnAsync("U' R U2 R' U2 R U' R'");
            await MakeTurnAsync("R' F R F'");
            await MakeTurnAsync("y L' U L U2 y R U R'");
            await MakeTurnAsync("R U' R' U2 F' U' F");
            await MakeTurnAsync("y' R' U' R");
            await MakeTurnAsync("R U R'");
            await MakeTurnAsync("d R' U' R U' R' U' R");
            await MakeTurnAsync("U' R U R' U R U R'");
            await MakeTurnAsync("U' R U2 R' d R' U' R");
            await MakeTurnAsync("R' U2 R2 U R2 U R");
            await MakeTurnAsync("d R' U R U' R' U' R");
            await MakeTurnAsync("U' R U' R' U R U R'");
            await MakeTurnAsync("R U2 R' U' R U R'");
            await MakeTurnAsync("y' R' U2 R U R' U' R");
            await MakeTurnAsync("U R U2 R2 F R F'");
            await MakeTurnAsync("y' U' R' U2 R U' R' U R");
            await MakeTurnAsync("R U' R' U2 R U R'");
            await MakeTurnAsync("y' R' U R U2 R' U' R");
            await MakeTurnAsync("U2 R2 U2 R' U' R U' R2");
            await MakeTurnAsync("y' U2 R2 U2 R U R' U R2");
            await MakeTurnAsync("U R U' R' U' y L' U L");
            await MakeTurnAsync("y U' L' U L y' U R U' R'");
            await MakeTurnAsync("y' R' U' R U R' U' R");
            await MakeTurnAsync("R U R' U' R U R'");
            await MakeTurnAsync("R U' R' U R U' R'");
            await MakeTurnAsync("R U R' d R' U2 R");
            await MakeTurnAsync("U' R U' R' U2 R U' R'");
            await MakeTurnAsync("U R U R' U2 R U R'");
            await MakeTurnAsync("U' R U R' d R' U' R");
            await MakeTurnAsync("d R' U' R d' R U R'");
            await MakeTurnAsync("R U' R' d R' U R");
            await MakeTurnAsync("R U R' U' R U R' U' R U R'");
            await MakeTurnAsync("R U' R' U' R U R' U2 R U' R'");
            await MakeTurnAsync("R U R' U2 R U' R' U R U R'");
            await MakeTurnAsync("R U' R' d R' U' R U' R' U' R");
            await MakeTurnAsync("R U' R' U d R' U' R U' R' U R");
            await MakeTurnAsync("R U' R' d R' U2 R U2 R' U R");
            await MakeTurnAsync("R U' R'");
            await MakeTurnAsync("L' U L");
            await MakeTurnAsync("R U R'");
            await MakeTurnAsync("L' U' L");
            await MakeTurnAsync("R U R'");
            await MakeTurnAsync("U");
            await MakeTurnAsync("F' U' F U'");
            await MakeTurnAsync("U' L' U L");
            await MakeTurnAsync("U' U' ");
            await MakeTurnAsync("R U' R'");
            await MakeTurnAsync("U L' U' L");
            await MakeTurnAsync("y' U2 R U2 R'");
            await MakeTurnAsync("R U' R'");
            await MakeTurnAsync("R' U R ");
            await MakeTurnAsync("U' R' U R2 U' R'");
            await MakeTurnAsync("y U L U' L2 U L");
            await MakeTurnAsync("U2 R' U R U' S R S'");
            await MakeTurnAsync("y U2 L U' L' U S' L' S");
            await MakeTurnAsync("R U B' l U l2 x' U' R' F R F'");
            await MakeTurnAsync("R' F R F' U2 R' F R y' R2 U2 R");
            await MakeTurnAsync("y L' R2 B R' B L U2 L' B M'");
            await MakeTurnAsync("R' U2 x R' U R U' y R' U' R' U R' F");
            await MakeTurnAsync("R U R' U R' F R F' U2 R' F R F'");
            await MakeTurnAsync("M' U2 M U2 M' U M U2 M' U2 M");
            await MakeTurnAsync("R' U2 F R U R' U' y' R2 U2 x' R U");
            await MakeTurnAsync("F R U R' U y' R' U2 R' F R F'");
            await MakeTurnAsync("R' U' y L' U L' y' L F L' F R");
            await MakeTurnAsync("R U' y R2 D R' U2 R D' R2 d R'");
            await MakeTurnAsync("F U R U' R' U R U' R' F'");
            await MakeTurnAsync("L' B' L U' R' U R U' R' U R L' B L");
            await MakeTurnAsync("L U' R' U L' U R U R' U R");
            await MakeTurnAsync("R U R' U R U' R' U R U2 R'");
            await MakeTurnAsync("L' U R U' L U R'");
            await MakeTurnAsync("R' U2 R U R' U R");
            await MakeTurnAsync("R' F' L F R F' L' F");
            await MakeTurnAsync("R2 D R' U2 R D' R' U2 R'");
            await MakeTurnAsync("R' F' L' F R F' L F");
            await MakeTurnAsync("M' U' M U2 M' U' M");
            await MakeTurnAsync("L' R U R' U' L R' F R F'");
            await MakeTurnAsync("L F R' F R F2 L'");
            await MakeTurnAsync("F R' F' R U R U' R'");
            await MakeTurnAsync("R' U' R y' x' R U' R' F R U R'");
            await MakeTurnAsync("U' R U2 R' U' R U' R2 y' R' U' R U B");
            await MakeTurnAsync("F R U R' U' R U R' U' F'");
            await MakeTurnAsync("L F' L' F U2 L2 y' L F L' F");
            await MakeTurnAsync("U' R' U2 R U R' U R2 y R U R' U' F'");
            await MakeTurnAsync("r U2 R' U' R U' r'");
            await MakeTurnAsync("R' U2 l R U' R' U l' U2 R");
            await MakeTurnAsync("F' L' U' L U L' U' L U F");
            await MakeTurnAsync("R' F R' F' R2 U2 x' U' R U R'");
            await MakeTurnAsync("R' F R F' U2 R2 y R' F' R F'");
            await MakeTurnAsync("R U R' y R' F R U' R' F' R");
            await MakeTurnAsync("L' B' L U' R' U R L' B L");
            await MakeTurnAsync("U2 r R2 U' R U' R' U2 R U' M");
            await MakeTurnAsync("x' U' R U' R2 F x R U R' U' R B2");
            await MakeTurnAsync("L U' y' R' U2 R' U R U' R U2 R d' L'");
            await MakeTurnAsync("U2 l' L2 U L' U L U2 L' U M");
            await MakeTurnAsync("R2 U R' B' R U' R2 U l U l'");
            await MakeTurnAsync("r' U2 R U R' U r");
            await MakeTurnAsync("R U x' R U' R' U x U' R'");
            await MakeTurnAsync("R U R' U' x D' R' U R E'");
            await MakeTurnAsync("R' F R U R' F' R y L U' L'");
            await MakeTurnAsync("L F' L' U' L F L' y' R' U R");
            await MakeTurnAsync("L' B' L R' U' R U L' B L");
            await MakeTurnAsync("R B R' L U L' U' R B' R'");
            await MakeTurnAsync("F U R U' R' F'");
            await MakeTurnAsync("R' d' L d R U' R' F' R");
            await MakeTurnAsync("L d R' d' L' U L F L'");
            await MakeTurnAsync("F' U' L' U L F");
            await MakeTurnAsync("F R U R' U' F'");
            await MakeTurnAsync("R U R' U' R' F R F'");
            await MakeTurnAsync("L U L' U L U' L' U' y2 R' F R F'");
            await MakeTurnAsync("R' U' R U' R' U R U y F R' F' R");
            await MakeTurnAsync("R' F R U R' U' y L' d R");
            await MakeTurnAsync("L F' L' U' L U y' R d' L'");
            await MakeTurnAsync("y");
            await MakeTurnAsync("M2 U M2 U2 M2 U M2");
            await MakeTurnAsync("R' U' R2 U R U R' U' R U R U' R U' R' U2");
            await MakeTurnAsync("R2 U' R' U' R U R U R U' R");
            await MakeTurnAsync("R' U R' U' R' U' R' U R U R2");
            await MakeTurnAsync("x z' R2 U2 R' D' R U2 R' D R' z x'");
            await MakeTurnAsync("x R2 D2 R U R' D2 R U' R x'");
            await MakeTurnAsync("R2 U R' U' y R U R' U' R U R' U' R U R' y' R U' R2");
            await MakeTurnAsync("R U R' U' R' F R2 U' R' U' R U R' F'");
            await MakeTurnAsync("F R U' R' U' R U R' F' R U R' U' R' F R F'");
            await MakeTurnAsync("U' R' U R U' R2 F' U' F U x R U R' U' R2 x'");
            await MakeTurnAsync("R' U R' U' y R' D R' D' R2 y' R' B' R B R");
            await MakeTurnAsync("L' U' L F L' U' L U L F' L2 U L U");
            await MakeTurnAsync("R U R' F' R U R' U' R' F R2 U' R' U'");
            await MakeTurnAsync("L U2 L' U2 L F' L' U' L U L F L2 U");
            await MakeTurnAsync("R' U2 R U2 R' F R U R' U' R' F' R2 U'");
            await MakeTurnAsync("R U R' U R U R' F' R U R' U' R' F R2 U' R' U2 R U' R'");
            await MakeTurnAsync("R' U R U' R' F' U' F R U R' F R' F' R U' R");
            await MakeTurnAsync("y R2 u R' U R' U' R u' R2 y' R' U R");
            await MakeTurnAsync("R' U' R y R2 u R' U R U' R u' R2");
            await MakeTurnAsync("y R2 u' R U' R U R' u R2 y R U' R'");
            await MakeTurnAsync("y2 R U R' y' R2 u' R U' R' U R' u R2");
            await MakeTurnAsync("x R' U R' D2 R U' R' D2 R2");
            await MakeTurnAsync("x' R U' R D2 R' U R D2 R2");
            await MakeTurnAsync("R2 U R U R' U' R' U' R' U R'");
            await MakeTurnAsync("R U' R U R U R U' R' U' R2");
            await MakeTurnAsync("M2 U M2 U2 M2 U M2");
            await MakeTurnAsync("R U R' U' R' F R2 U' R' U' R U R' F'");
            await MakeTurnAsync("R' U L' U2 R U' R' U2 R L U'");
            await MakeTurnAsync("R U R' F' R U R' U' R' F R2 U' R' U'");
            await MakeTurnAsync("L U2 L' U2 L F' L' U' L U L F L2 U");
            await MakeTurnAsync("R' U2 R U2 R' F R U R' U' R' F' R2 U'");
            await MakeTurnAsync("R' U R' d' R' F' R2 U' R' U R' F R F");
            await MakeTurnAsync("R2 u R' U R' U' R u' R2 y' R' U R");
            await MakeTurnAsync("R' U' R y R2 u R' U R U' R u' R2");
            await MakeTurnAsync("R2 u' R U' R U R' u R2 y R U' R'");
            await MakeTurnAsync("R U R' y' R2 u' R U' R' U R' u R2");
            await MakeTurnAsync("R' U2 R' d' R' F' R2 U' R' U R' F R U' F");
            await MakeTurnAsync("M2 U M2 U M' U2 M2 U2 M' U2");
            await MakeTurnAsync("F R U' R' U' R U R' F' R U R' U' R' F R F'");
            await MakeTurnAsync("L U' R U2 L' U R' L U' R U2 L' U R' U");
            await MakeTurnAsync("R' U L' U2 R U' L R' U L' U2 R U' L U'");
            await MakeTurnAsync("x' R U' R' D R U R' u2 R' U R D R' U' R");

            return true;
        }

        /// <summary>
        /// Test the turns of the cube for the Basic method
        /// </summary>
        /// <returns></returns>
        private static async Task<bool> TestSolveCubeTurnsBasic()
        {
            await MakeTurnAsync("U2");
            await MakeTurnAsync("U2");
            await MakeTurnAsync("U");
            await MakeTurnAsync("U'");
            await MakeTurnAsync("U");
            await MakeTurnAsync("U'");
            await MakeTurnAsync("U2");
            await MakeTurnAsync("D'");
            await MakeTurnAsync("D");
            await MakeTurnAsync("D2");
            await MakeTurnAsync("R2");
            await MakeTurnAsync("F D F'");
            await MakeTurnAsync("R' D' R");
            await MakeTurnAsync("R' D R D2 R' D' R");
            await MakeTurnAsync("y");
            await MakeTurnAsync("U'");
            await MakeTurnAsync("U2");
            await MakeTurnAsync("U");
            await MakeTurnAsync("R' U' D B U' L U' D F'");
            await MakeTurnAsync("U' B' U D' R");
            await MakeTurnAsync("U2 L U' D F'");
            await MakeTurnAsync("R' U2 D2 L U' F U' D R'");
            await MakeTurnAsync("R' U' D B");
            await MakeTurnAsync("U F U' D' R'");
            await MakeTurnAsync("U2 L' U D' B");
            await MakeTurnAsync("R' U' D B");
            await MakeTurnAsync("R' U' D B");
            await MakeTurnAsync("U");
            await MakeTurnAsync("U'");
            await MakeTurnAsync("U2");
            await MakeTurnAsync("D'");
            await MakeTurnAsync("D'");
            await MakeTurnAsync("D2");
            await MakeTurnAsync("D2");
            await MakeTurnAsync("D");
            await MakeTurnAsync("D");
            await MakeTurnAsync("y");
            await MakeTurnAsync("D' R' D R D F D' F'");
            await MakeTurnAsync("D L D' L' D' F' D F");
            await MakeTurnAsync("y");
            await MakeTurnAsync("y'");
            await MakeTurnAsync("y2");
            await MakeTurnAsync("z2");
            await MakeTurnAsync("U");
            await MakeTurnAsync("U'");
            await MakeTurnAsync("U2");
            await MakeTurnAsync("L' U R U' L U R' U'");
            await MakeTurnAsync("U R U' L' U R' U' L");
            await MakeTurnAsync("F U' B' U F' U' B U2");
            await MakeTurnAsync("U F U R U' R' F'");
            await MakeTurnAsync("y");
            await MakeTurnAsync("L2 U F' B L2 F B' U L2");
            await MakeTurnAsync("L2 U' F' B L2 F B' U' L2");
            await MakeTurnAsync("R2 L2 D R2 L2 U2 R2 L2 D R2 L2");
            await MakeTurnAsync("R B U B' U' R2 F' U' F U R");
            await MakeTurnAsync("y");
            await MakeTurnAsync("U");
            await MakeTurnAsync("U'");
            await MakeTurnAsync("U2");
            await MakeTurnAsync("R' D R F D F'");
            await MakeTurnAsync("U");
            await MakeTurnAsync("U'");
            await MakeTurnAsync("U2");
            await MakeTurnAsync("F D' F' R' D' R");
            await MakeTurnAsync("U");
            await MakeTurnAsync("U");
            await MakeTurnAsync("U'");
            await MakeTurnAsync("U2");
            await MakeTurnAsync("U");
            await MakeTurnAsync("U'");
            await MakeTurnAsync("U2");
            await MakeTurnAsync("F U D' L2 U2 D2 R");
            await MakeTurnAsync("U");
            await MakeTurnAsync("U'");
            await MakeTurnAsync("U2");
            await MakeTurnAsync("R' D2 U2 L2 D U' F'");

            return true;
        }

        /// <summary>
        /// Test the turns of the cube for the Daisy method
        /// </summary>
        /// <returns></returns>
        private static async Task<bool> TestSolveCubeTurnsDaisy()
        {
            await MakeTurnAsync("y");
            await MakeTurnAsync("y");
            await MakeTurnAsync("y");
            await MakeTurnAsync("y'");
            await MakeTurnAsync("y'");
            await MakeTurnAsync("y'");
            await MakeTurnAsync("L' U' L U");
            await MakeTurnAsync("L' U' L U");
            await MakeTurnAsync("L' U' L U");
            await MakeTurnAsync("R U R' U'");
            await MakeTurnAsync("R U R' U'");
            await MakeTurnAsync("R U R' U'");
            await MakeTurnAsync("y' R U R' U'");
            await MakeTurnAsync("y L' U' L U");
            await MakeTurnAsync("R U R' U'");
            await MakeTurnAsync("y2 L' U' L U");
            await MakeTurnAsync("y R U R' U'");
            await MakeTurnAsync("y' L' U' L U");
            await MakeTurnAsync("y2 R U R' U'");
            await MakeTurnAsync("L' U' L U");
            await MakeTurnAsync("U");
            await MakeTurnAsync("U");
            await MakeTurnAsync("U'");
            await MakeTurnAsync("U2");
            await MakeTurnAsync("U' L' U L U F U' F'");
            await MakeTurnAsync("U");
            await MakeTurnAsync("U'");
            await MakeTurnAsync("U2");
            await MakeTurnAsync("U R U' R' U' F' U F");
            await MakeTurnAsync("U R U' R' U' F' U F");
            await MakeTurnAsync("U R U' R' U' F' U F");
            await MakeTurnAsync("y");
            await MakeTurnAsync("U'");
            await MakeTurnAsync("U");
            await MakeTurnAsync("U2");
            await MakeTurnAsync("F U R U' R' F'");
            await MakeTurnAsync("U");
            await MakeTurnAsync("F R U R' U' F'");
            await MakeTurnAsync("F R U R' U' F' U2 F U R U' R' F'");
            await MakeTurnAsync("y");
            await MakeTurnAsync("F R U R' U' F'");
            await MakeTurnAsync("y");
            await MakeTurnAsync("y2");
            await MakeTurnAsync("y'");
            await MakeTurnAsync("U R U' L' U R' U' L");
            await MakeTurnAsync("U R U' L' U R' U' L");
            await MakeTurnAsync("y");
            await MakeTurnAsync("y2");
            await MakeTurnAsync("y'");
            await MakeTurnAsync("R U R' U'");
            await MakeTurnAsync("D'");
            await MakeTurnAsync("y");
            await MakeTurnAsync("y2");
            await MakeTurnAsync("y'");
            await MakeTurnAsync("U");
            await MakeTurnAsync("y2");
            await MakeTurnAsync("y U");
            await MakeTurnAsync("y2");
            await MakeTurnAsync("R U R' U R U2 R' U");

            return true;
        }

        /// <summary>
        /// Test the turns of the cube for the Cross method
        /// </summary>
        /// <returns></returns>
        private static async Task<bool> TestSolveCubeTurnsCross()
        {
            await MakeTurnAsync("F2");
            await MakeTurnAsync("D R2");
            await MakeTurnAsync("D2 B2");
            await MakeTurnAsync("D' L2");
            await MakeTurnAsync("L2");
            await MakeTurnAsync("D F2");
            await MakeTurnAsync("D2 R2");
            await MakeTurnAsync("D' B2");
            await MakeTurnAsync("R2");
            await MakeTurnAsync("D' F2");
            await MakeTurnAsync("D B2");
            await MakeTurnAsync("D2 L2");
            await MakeTurnAsync("B2");
            await MakeTurnAsync("D2 F2");
            await MakeTurnAsync("D' R2");
            await MakeTurnAsync("D L2");
            await MakeTurnAsync("y");
            await MakeTurnAsync("y2");
            await MakeTurnAsync("y'");
            await MakeTurnAsync("L D' L'");
            await MakeTurnAsync("R' D' R");
            await MakeTurnAsync("F D' F'");
            await MakeTurnAsync("B' D B");
            await MakeTurnAsync("R D' R'");
            await MakeTurnAsync("L' D L");
            await MakeTurnAsync("B D' B'");
            await MakeTurnAsync("F' D F");
            await MakeTurnAsync("D y");
            await MakeTurnAsync("D' y'");
            await MakeTurnAsync("D2 y2");
            await MakeTurnAsync("F2 F U' R U");
            await MakeTurnAsync("U'");
            await MakeTurnAsync("U");
            await MakeTurnAsync("U2");
            await MakeTurnAsync("z2");
            await MakeTurnAsync("y");
            await MakeTurnAsync("y");
            await MakeTurnAsync("y");
            await MakeTurnAsync("y'");
            await MakeTurnAsync("y'");
            await MakeTurnAsync("y'");
            await MakeTurnAsync("L' U' L U");
            await MakeTurnAsync("L' U' L U");
            await MakeTurnAsync("L' U' L U");
            await MakeTurnAsync("R U R' U'");
            await MakeTurnAsync("R U R' U'");
            await MakeTurnAsync("R U R' U'");
            await MakeTurnAsync("y' R U R' U'");
            await MakeTurnAsync("y L' U' L U");
            await MakeTurnAsync("R U R' U'");
            await MakeTurnAsync("y2 L' U' L U");
            await MakeTurnAsync("y R U R' U'");
            await MakeTurnAsync("y' L' U' L U");
            await MakeTurnAsync("y2 R U R' U'");
            await MakeTurnAsync("L' U' L U");
            await MakeTurnAsync("U");
            await MakeTurnAsync("U' y");
            await MakeTurnAsync("U2 y2");
            await MakeTurnAsync("U y'");
            await MakeTurnAsync("U");
            await MakeTurnAsync("y");
            await MakeTurnAsync("U' y2");
            await MakeTurnAsync("U2 y'");
            await MakeTurnAsync("U2");
            await MakeTurnAsync("U y");
            await MakeTurnAsync("y2");
            await MakeTurnAsync("U' y'");
            await MakeTurnAsync("U'");
            await MakeTurnAsync("U2 y");
            await MakeTurnAsync("U y2");
            await MakeTurnAsync("y'");
            await MakeTurnAsync("y");
            await MakeTurnAsync("y2");
            await MakeTurnAsync("y'");
            await MakeTurnAsync("y");
            await MakeTurnAsync("y2");
            await MakeTurnAsync("y2");
            await MakeTurnAsync("U");
            await MakeTurnAsync("U2");
            await MakeTurnAsync("U'");
            await MakeTurnAsync("U");
            await MakeTurnAsync("U2");
            await MakeTurnAsync("U'");
            await MakeTurnAsync("U R U R' U' y L' U' L U");
            await MakeTurnAsync("U' L' U' L U y' R U R' U'");
            await MakeTurnAsync("U R U' R' U' F' U F U2 U R U' R' U' F' U F");
            await MakeTurnAsync("U R U' R' U' F' U F");
            await MakeTurnAsync("U' L' U L U F U' F'");
            await MakeTurnAsync("U");
            await MakeTurnAsync("U2");
            await MakeTurnAsync("U'");
            await MakeTurnAsync("F R U R' U' F'");
            await MakeTurnAsync("U2");
            await MakeTurnAsync("U'");
            await MakeTurnAsync("U");
            await MakeTurnAsync("F R U R' U' F'");
            await MakeTurnAsync("U");
            await MakeTurnAsync("F R U R' U' F'");
            await MakeTurnAsync("F R U R' U' F'");
            await MakeTurnAsync("y");
            await MakeTurnAsync("y2");
            await MakeTurnAsync("y'");
            await MakeTurnAsync("U R U' L' U R' U' L");
            await MakeTurnAsync("z2");
            await MakeTurnAsync("y");
            await MakeTurnAsync("y2");
            await MakeTurnAsync("y'");
            await MakeTurnAsync("R U R' U'");
            await MakeTurnAsync("D'");
            await MakeTurnAsync("F R U R' U' F'");
            await MakeTurnAsync("y");
            await MakeTurnAsync("y2");
            await MakeTurnAsync("y'");
            await MakeTurnAsync("U");
            await MakeTurnAsync("y2");
            await MakeTurnAsync("y U");
            await MakeTurnAsync("y2");
            await MakeTurnAsync("R U R' U R U2 R' U");

            return true;
        }

        /// <summary>
        /// Test the turns of the cube for the Common method
        /// </summary>
        /// <returns></returns>
        private static async Task<bool> TestSolveCubeTurnsCommon()
        {
            await MakeTurnAsync("F'");
            await MakeTurnAsync("U'");
            await MakeTurnAsync("U");
            await MakeTurnAsync("U2");
            await MakeTurnAsync("L'");
            await MakeTurnAsync("F'");
            await MakeTurnAsync("U");
            await MakeTurnAsync("U'");
            await MakeTurnAsync("U2");
            await MakeTurnAsync("R");
            await MakeTurnAsync("U");
            await MakeTurnAsync("U'");
            await MakeTurnAsync("U2");
            await MakeTurnAsync("R2");
            await MakeTurnAsync("y");
            await MakeTurnAsync("y2");
            await MakeTurnAsync("y'");
            await MakeTurnAsync("F2");
            await MakeTurnAsync("R2");
            await MakeTurnAsync("B2");
            await MakeTurnAsync("L2");
            await MakeTurnAsync("F2");
            await MakeTurnAsync("U' R2");
            await MakeTurnAsync("U L2");
            await MakeTurnAsync("U2 B2");
            await MakeTurnAsync("y");
            await MakeTurnAsync("U");
            await MakeTurnAsync("U2");
            await MakeTurnAsync("U'");
            await MakeTurnAsync("R2 L2 D R2 L2 U2 R2 L2 D R2 L2");
            await MakeTurnAsync("R B U B' U' R2 F' U' F U R");
            await MakeTurnAsync("F2 U L R' F2 L' R U F2");
            await MakeTurnAsync("y");
            await MakeTurnAsync("y'");
            await MakeTurnAsync("y2");
            await MakeTurnAsync("F2 U L R' F2 L' R U F2");
            await MakeTurnAsync("F2 U' L R' F2 L' R U' F2");
            await MakeTurnAsync("F U' B' U F' U' B U2");
            await MakeTurnAsync("R' F R' B2 R F' R' B2 R2");
            await MakeTurnAsync("F R U' R' U' R U R' F' R U R' U' R' F R F'");
            await MakeTurnAsync("U F U R U' R' F'");
            await MakeTurnAsync("L' U R U' L U R' U'");
            await MakeTurnAsync("U R U' L' U R' U' L");
            await MakeTurnAsync("l' U R' D2 R U' R' D2 R2");
            await MakeTurnAsync("x' R U' R' D R U R' D' R U R' D R U' R' D'");

            return true;
        }
    }
}
