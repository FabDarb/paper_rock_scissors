using System.Diagnostics;
using System.IO;

namespace paper_rock_scissors.Class.Python
{
    class PythonProcess
    {
        static public PythonProcess Instance { get; set; } = new PythonProcess();
        public ProcessStartInfo StartInfo { get; set; }
        private Process Process {  get; set; }
        public StreamWriter StreamWriter { get; set; }
        public StreamReader StreamReader { get; set; }

        PythonProcess() 
        {
            StartInfo = new ProcessStartInfo { 
                FileName= "py",
                Arguments = "-u ImageRecognition/ImageRecognition.py",
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
            };
            Process = new Process();
            Process.StartInfo = StartInfo;
            Process.Start();

            StreamWriter = Process.StandardInput;
            StreamReader = Process.StandardOutput;
        }
    }
}
