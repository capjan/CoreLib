using System.IO;
using System.Text;

namespace Core.Logging.Targets
{
    public class FileLogTarget : LogTarget
    {
        public FileLogTarget(string filePath)
        {                      
            _outputStream = new StreamWriter(filePath, true, Encoding.UTF8);
        }

        public bool AutoFlush
        {
            get => _outputStream.AutoFlush;
            set => _outputStream.AutoFlush = value;
        }

        public void Flush()
        {
            _outputStream.Flush();
        }

        protected override void OnLog(LogEventArgs itm)
        {
            var createdAt = DateTimeFormatter.Format(itm.CreatedAtUtc);
            var level = LogLevelFormatter.Format(itm.Level);
            _outputStream.WriteLine($"{createdAt} {level} {itm.Message}");
        }

        protected override void OnDispose()
        {
            _outputStream.Flush();

            _outputStream.Close();
            _outputStream.Dispose();            
            base.OnDispose();
        }

        private readonly StreamWriter _outputStream;
    }
}
