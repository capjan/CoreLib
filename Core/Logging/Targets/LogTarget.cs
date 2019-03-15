using System;
using System.Linq;
using Core.Text.Formatter;

namespace Core.Logging.Targets
{
    public abstract class LogTarget : IDisposable
    {
        public LogTarget()
        {
            LogLevelMaxCharLength = nameof(LogLevel.Warning).Length;;
        }
        /// <inheritdoc />
        public void Dispose()
        {
            Connected = false;
        }

        public bool Connected
        {
            get => _isConnected;
            set
            {
                if (_isConnected == value)
                    return;
                if (_isConnected) DetachLogEvent();
                else AttachLogEvent();
                _isConnected = value;
            }
        }

        public LogLevel LogMask { get; set; } = LogLevel.AllMask;

        public IDateTimeFormatter DateTimeFormatter { get; set; } = new DateTimeFormatter();

        private void AttachLogEvent()
        {
            Log.OnLog += FilterLog;
        }

        private void DetachLogEvent()
        {
            Log.OnLog -= FilterLog;
        }

        private void FilterLog(LogEventArgs itm)
        {
            if ((itm.Level & LogMask) == 0) return;            
            OnLog(itm);
        }

        protected abstract void OnLog(LogEventArgs itm);

        private bool _isConnected;
        protected readonly int LogLevelMaxCharLength;
    }
}
