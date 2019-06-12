using System.IO;
using Core.Diagnostics;

namespace Core.Extensions.DiagnosticsRelated
{
    public static class CliRunnerExt
    {
        /// <summary>
        /// Runs the CLI process and redirects all output to the given TextWriter instance
        /// </summary>
        /// <param name="runner">used cli runner object</param>
        /// <param name="writer">writer output that should write the output</param>
        public static void Redirect(this ICliRunner runner, TextWriter writer)
        {
            runner.ReadLines(writer.WriteLine);
        }
    }
}