using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;

namespace CubeSolver
{
    internal sealed class ClassProgramLogging
    {
        public static void LogExecutedLineTest()
        {
            LogExecutedLine();

            int a = 1;
            int b = 2;
            int c = a + b;

            Debug.WriteLine($"c = {c}");

            string cResult = LogExecutedLine2();
            Debug.WriteLine(cResult);
        }

        /// <summary>
        /// Log executed lines
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="lineNumber"></param>
        /// <param name="memberName"></param>
        public static void LogExecutedLine([CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            StackTrace stackTrace = new StackTrace(true);
            StackFrame? frame = stackTrace.GetFrame(1);  // Get the previous frame

            if (frame != null)
            {
                int frameLineNumber = frame.GetFileLineNumber();
                string? frameMethodName = frame.GetMethod()?.Name;
                string? frameFileName = frame.GetFileName();

                Debug.WriteLine($"Executed line: {lineNumber} in method: {memberName} of file: {filePath}");
                Debug.WriteLine($"Stack trace line: {frameLineNumber} in method: {frameMethodName} of file: {frameFileName}");
                Debug.WriteLine("StackTrace: '{0}'", Environment.StackTrace);
            }
        }

        public static string LogExecutedLine2()
        {
            //// get call stack
            StackTrace stackTrace = new StackTrace(true);
            StringBuilder sb = new StringBuilder();

            foreach (StackFrame frame in stackTrace.GetFrames())
            {
                sb.AppendLine(" Method Name: " + frame.GetMethod().Name + " File Name:" + frame.GetMethod().Module.Name + " Line No: " + frame.GetFileLineNumber());
            }

            return sb.ToString();
        }
    }
}
