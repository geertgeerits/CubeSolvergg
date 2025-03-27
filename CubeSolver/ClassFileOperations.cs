using System.Diagnostics;

namespace CubeSolver
{
    internal sealed class ClassFileOperations
    {
        /// <summary>
        /// Save the cube data
        /// </summary>
        /// <returns></returns>
        public static bool CubeDataSave()
        {
            string cFileName = Path.Combine(FileSystem.Current.AppDataDirectory, "CubeSolver.txt");

            //Debug.WriteLine("FileSystem.Current.AppDataDirectory: " + FileSystem.Current.AppDataDirectory);  // For testing

            if (File.Exists(cFileName))
            {
                File.Delete(cFileName);
            }

            int nRow;

            try
            {
                using StreamWriter sw = new(cFileName, false);
                
                for (nRow = 1; nRow < 7; nRow++)
                {
                    sw.WriteLine(Globals.aFaceColors[nRow]);
                }

                for (nRow = 0; nRow < 54; nRow++)
                {
                    sw.WriteLine(Globals.aPieces[nRow]);
                }

                sw.Close();
            }
            catch (Exception ex)
            {
                _ = Application.Current!.Windows[0].Page!.DisplayAlert(CubeLang.ErrorTitle_Text, ex.Message, CubeLang.ButtonClose_Text);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Open, restore the cube
        /// </summary>
        /// <returns></returns>
        public static bool CubeDataOpen()
        {
            string cFileName = Path.Combine(FileSystem.Current.AppDataDirectory, "CubeSolver.txt");

            if (File.Exists(cFileName) == false)
            {
                return false;
            }

            int nRow;

            try
            {
                // Open the text file using a stream reader
                using StreamReader sr = new(cFileName, false);
                
                for (nRow = 1; nRow < 7; nRow++)
                {
                    Globals.aFaceColors[nRow] = sr.ReadLine() ?? string.Empty;
                }

                for (nRow = 0; nRow < 54; nRow++)
                {
                    Globals.aPieces[nRow] = sr.ReadLine() ?? string.Empty;
                }

                sr.Close();
            }
            catch (Exception ex)
            {
                _ = Application.Current!.Windows[0].Page!.DisplayAlert(CubeLang.ErrorTitle_Text, ex.Message, CubeLang.ButtonClose_Text);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Save the cube turns (for testing)
        /// </summary>
        /// <param name="cFile"></param>
        /// <returns></returns>
        public static bool CubeTurnsSave(string cFile)
        {
            string cFileName = Path.Combine(FileSystem.Current.CacheDirectory, cFile);
            //Debug.WriteLine("FileSystem.Current.CacheDirectory: " + FileSystem.Current.CacheDirectory);  // For testing

            try
            {
                if (File.Exists(cFileName))
                {
                    File.Delete(cFileName);
                }

                using StreamWriter sw = new(cFileName, false);
                
                sw.WriteLine($"Turns: {Globals.lCubeTurns.Count}");
                sw.WriteLine();

                foreach (string cItem in Globals.lCubeTurns)
                {
                    sw.WriteLine(cItem);
                }

                sw.Close();
            }
            catch (Exception ex)
            {
                _ = Application.Current!.Windows[0].Page!.DisplayAlert(CubeLang.ErrorTitle_Text, ex.Message, CubeLang.ButtonClose_Text);
                return false;
            }

            Debug.WriteLine($"CubeTurnsSave cFileName:\n{cFileName}");

            return true;
        }

        /// <summary>
        /// Copy the data files to the cache directory
        /// </summary>
        /// <returns></returns>
        public static async Task CopyDataFilesToCacheAsync()
        {
            string[] fileNames =
            [
                "Flip", "FRtoBR", "MergeURtoULandUBtoDF", "Slice_Flip_Prun", "Slice_Twist_Prun", "Slice_URFtoDLF_Parity_Prun",
                "Slice_URtoDF_Parity_Prun", "twist", "UBtoDF", "URFtoDLF", "URtoDF", "URtoUL"
            ];

            foreach (string fileName in fileNames)
            {
                string destinationPathFile = Path.Combine(Globals.cPathTables, fileName);

                try
                {
                    using Stream resourceStream = await FileSystem.OpenAppPackageFileAsync(fileName);
                    if (resourceStream != null && !File.Exists(destinationPathFile))
                    {
                        using FileStream fileStream = File.Create(destinationPathFile);
                        await resourceStream.CopyToAsync(fileStream);
                        Debug.WriteLine($"File copied: {destinationPathFile}");
                    }
                }
                catch (FileNotFoundException ex)
                {
                    Debug.WriteLine($"File not found: {fileName} - {ex.Message}");
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error copying file: {fileName} - {ex.Message}");
                }
            }
        }

        /// <summary>
        /// Calculate the duration for the first Kociemba solve with creation of the tables
        /// </summary>
        public static async Task<int> DurationFirstKociembaSolveAsync()
        {
            // Create and start a stopwatch instance
            long startTime = Stopwatch.GetTimestamp();

            string cFileName = Path.Combine(FileSystem.Current.CacheDirectory, "SpeedTest.txt");

            try
            {
                if (File.Exists(cFileName))
                {
                    File.Delete(cFileName);
                }

                using (StreamWriter sw = new(cFileName, false))
                {
                    for (int nI = 1; nI < 1001; nI++)
                    {
                        sw.WriteLine($"Loop: {nI} {Math.Pow(nI, 2)} {Math.Sqrt(nI)}");
                    }

                    sw.Close();
                }

                File.Delete(cFileName);
            }
            catch (Exception ex)
            {
#if DEBUG
                _ = Application.Current!.Windows[0].Page!.DisplayAlert(CubeLang.ErrorTitle_Text, ex.Message, CubeLang.ButtonClose_Text);
#endif
                // Return a default value of 60 seconds
                return await Task.FromResult(60);
            }

            // Stop the stopwatch and get the elapsed time
            TimeSpan delta = Stopwatch.GetElapsedTime(startTime);

            Debug.WriteLine($"Time elapsed (Milliseconds): {delta.TotalMilliseconds} - (Seconds): {delta.TotalSeconds}");

            double nMillisecondsToMultiply = delta.TotalMilliseconds switch
            {
                < 30 => 1.5,
                < 60 => 2.5,
                _ => 1,
            };

            // Round up to the nearest 10 milliseconds
            int nDurationFirstKociembaSolve = (int)Math.Ceiling(delta.TotalMilliseconds * nMillisecondsToMultiply / 10.0) * 10;

            Debug.WriteLine($"Time elapsed (nDurationFirstKociembaSolve): {nDurationFirstKociembaSolve}");

            // The duration in milliseconds is treated as a delay in seconds in the Kociemba solve
            return await Task.FromResult(nDurationFirstKociembaSolve);
        }
    }
}
