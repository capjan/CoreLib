using System.IO;
using System.Text;
using Core.Extensions.TextRelated;

namespace Core.Logging.Targets;

public class FileLogTarget : LogTarget
{
    private static readonly object SyncLock = new object();

    public FileLogTarget(string filePath)
    {                      
        _outputStream = new StreamWriter(filePath, true, Encoding.UTF8);
    }

    public bool AutoFlush
    {
        get
        {
            lock (SyncLock)
            {
                return _outputStream.AutoFlush;
            }
        } 
        set
        {
            lock (SyncLock)
            {
                _outputStream.AutoFlush = value;
            }
        } 
    }

    public void Flush()
    {
        lock (SyncLock)
        {
            _outputStream.Flush();
        }
    }

    protected override void OnLog(LogEventArgs itm)
    {
        lock (SyncLock)
        {                
            var createdAt = DateTimeFormatter.WriteToString(itm.CreatedAtUtc);
            var level     = LogLevelFormatter.WriteToString(itm.Level);
            _outputStream.WriteLine($"{createdAt} {level} {itm.Message}");    
        }            
    }

    protected override void OnDispose()
    {
        lock (SyncLock)
        {
            _outputStream.Flush();

            _outputStream.Close();
            _outputStream.Dispose();            
        }            
        base.OnDispose();
    }

    private readonly StreamWriter _outputStream;
}