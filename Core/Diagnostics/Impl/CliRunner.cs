using System;
using System.Diagnostics;

namespace Core.Diagnostics
{
    public class CliRunner : ICliRunner
    {
        public CliRunner(string filePath, string arguments)
        {
            _psi = new ProcessStartInfo(filePath)
            {
                Arguments              = arguments,
                UseShellExecute        = false,
                RedirectStandardOutput = true,
                RedirectStandardError  = true,
                CreateNoWindow         = false
            };
        }

        public string Arguments
        {
            get => _psi.Arguments;
            set => _psi.Arguments = value;
        }

        public string ReadToEnd()
        {
            var result = "";
            using (var p = Process.Start(_psi))
            {                                
                result = p.StandardOutput.ReadToEnd();
                p.WaitForExit();                                
            }
            return result;
        }

        public void ReadLines(Action<string> callback)
        {
            _readDataCallback = callback;
            using (var p = new Process())
            {                
                p.OutputDataReceived += DataReceived;
                p.ErrorDataReceived  += DataReceived;
                p.StartInfo          =  _psi;
                p.Start();
                p.BeginOutputReadLine();
                p.BeginErrorReadLine();
                p.WaitForExit();                
                p.OutputDataReceived -= DataReceived;
                p.ErrorDataReceived  -= DataReceived;
            }            
        }

        private void DataReceived(object sender, DataReceivedEventArgs e)
        {
            _readDataCallback(e.Data);       
        }

        private          Action<string>   _readDataCallback;
        private readonly ProcessStartInfo _psi;
    }
}
