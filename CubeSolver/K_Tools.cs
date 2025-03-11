using System.Text.Json;

namespace CubeSolver
{
    public class Tools
    {
        // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        // Check if the cube string s represents a solvable cube.
        // 0: Cube is solvable
        // -1: There is not exactly one facelet of each colour
        // -2: Not all 12 edges exist exactly once
        // -3: Flip error: One edge has to be flipped
        // -4: Not all corners exist exactly once
        // -5: Twist error: One corner has to be twisted
        // -6: Parity error: Two corners or two edges have to be exchanged
        // 
        /// <summary>
        /// Check if the cube definition string s represents a solvable cube.
        /// </summary>
        /// <param name="s"> is the cube definition string , see <seealso cref="Facelet"/> </param>
        /// <returns> 0: Cube is solvable
        ///         -1: There is not exactly one facelet of each colour
        ///         -2: Not all 12 edges exist exactly once
        ///         -3: Flip error: One edge has to be flipped
        ///         -4: Not all 8 corners exist exactly once
        ///         -5: Twist error: One corner has to be twisted
        ///         -6: Parity error: Two corners or two edges have to be exchanged </returns>
        public static int Verify(string s)
        {
            int[] count = new int[6];
            try
            {
                for (int i = 0; i < 54; i++)
                {
                    count[(int)Enum.Parse<CubeColor>(i.ToString())]++;
                }
            }
            catch (Exception)
            {
                return -1;
            }

            for (int i = 0; i < 6; i++)
            {
                if (count[i] != 9)
                {
                    return -1;
                }
            }

            FaceCube fc = new(s);
            CubieCube cc = fc.ToCubieCube();

            return cc.Verify();
        }

        /// <summary>
        /// Generates a random cube. </summary>
        /// <returns> A random cube in the string representation. Each cube of the cube space has the same probability. </returns>
        public static string RandomCube()
        {
            CubieCube cc = new();
            Random gen = new();
            cc.SetFlip((short)gen.Next(CoordCube.N_FLIP));
            cc.SetTwist((short)gen.Next(CoordCube.N_TWIST));
            do
            {
                cc.SetURFtoDLB(gen.Next(CoordCube.N_URFtoDLB));
                cc.SetURtoBR(gen.Next(CoordCube.N_URtoBR));
            } while ((cc.EdgeParity() ^ cc.CornerParity()) != 0);
            FaceCube fc = cc.ToFaceCube();
            return fc.To_fc_String();
        }

        //// Error when serialize tables !!!
        //// NotSupportedException: Serialization and deserialization of 'System.Int16[,]' instances is not supported
        //public static void SerializeTable(string filename, short[,] array)
        //{
        //    EnsureFolder(Globals.cPathTables);
        //    using Stream s = File.Open(Path.Combine(Globals.cPathTables, filename), FileMode.Create);
        //    JsonSerializer.Serialize(s, array);
        //}

        /// <summary>
        /// Serialize tables
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="array"></param>
        public static void SerializeTable(string filename, short[,] array)
        {
            EnsureFolder(Globals.cPathTables);
            using Stream s = File.Open(Path.Combine(Globals.cPathTables, filename), FileMode.Create);
            var jaggedArray = ConvertToJaggedArray(array);
            JsonSerializer.Serialize(s, jaggedArray);
        }

        /// <summary>
        /// Convert a 2D array to a jagged array
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        private static short[][] ConvertToJaggedArray(short[,] array)
        {
            int rows = array.GetLength(0);
            int cols = array.GetLength(1);
            var jaggedArray = new short[rows][];
            
            for (int i = 0; i < rows; i++)
            {
                jaggedArray[i] = new short[cols];
                for (int j = 0; j < cols; j++)
                {
                    jaggedArray[i][j] = array[i, j];
                }
            }
            
            return jaggedArray;
        }

        //// Error when deserialize tables !!!
        //// System.TypeInitializationException: 'The type initializer for 'CubeSolver.CoordCube' threw an exception.'
        //// TypeInitializationException: The type initializer for 'CubeSolver.CoordCubeTables' threw an exception.
        //public static short[,] DeserializeTable(string filename)
        //{
        //    EnsureFolder(Globals.cPathTables);
        //    using Stream s = File.Open(Path.Combine(Globals.cPathTables, filename), FileMode.Open);
        //    var result = JsonSerializer.Deserialize<short[,]>(s) ?? throw new InvalidOperationException("Deserialization returned null.");
        //    return result;
        //}

        public static short[,] DeserializeTable(string filename)
        {
            EnsureFolder(Globals.cPathTables);
            using Stream s = File.Open(Path.Combine(Globals.cPathTables, filename), FileMode.Open);
            var jaggedArray = JsonSerializer.Deserialize<short[][]>(s) ?? throw new InvalidOperationException("Deserialization returned null.");
            return ConvertTo2DArray(jaggedArray);
        }

        /// <summary>
        /// Convert a jagged array to a 2D array
        /// </summary>
        /// <param name="jaggedArray"></param>
        /// <returns></returns>
        private static short[,] ConvertTo2DArray(short[][] jaggedArray)
        {
            int rows = jaggedArray.Length;
            int cols = jaggedArray[0].Length;
            var array = new short[rows, cols];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    array[i, j] = jaggedArray[i][j];
                }
            }

            return array;
        }

        public static void SerializeSbyteArray(string filename, sbyte[] array)
        {
            EnsureFolder(Globals.cPathTables);
            using Stream s = File.Open(Path.Combine(Globals.cPathTables, filename), FileMode.Create);
            JsonSerializer.Serialize(s, array);
        }

        public static sbyte[] DeserializeSbyteArray(string filename)
        {
            EnsureFolder(Globals.cPathTables);
            using Stream s = File.Open(Path.Combine(Globals.cPathTables, filename), FileMode.Open);
            var result = JsonSerializer.Deserialize<sbyte[]>(s) ?? throw new InvalidOperationException("Deserialization returned null.");
            return result;
        }

        static void EnsureFolder(string path)
        {
            string? directoryName = Path.GetDirectoryName(path);
            // If path is a file name only, directory name will be null
            if (!string.IsNullOrEmpty(directoryName))
            {
                // Create all directories on the path that don't already exist
                Directory.CreateDirectory(directoryName);
            }
        }
    }
}
