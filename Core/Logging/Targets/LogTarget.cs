using System;
using Core.Enums;
using Core.Logging.Formatter;
using Core.Text.Formatter;
using Core.Text.Formatter.Impl;

namespace Core.Logging.Targets
{
    public abstract class LogTarget : IDisposable
    {
        /// <inheritdoc />
        public void Dispose()
        {
            Connected = false;
            if (_isDisposed) return;
            OnDispose();
            _isDisposed = true;
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

        public IDateTimeFormatter DateTimeFormatter { get; set; } = new DefaultDateTimeFormatter();
        public ITextFormatter<LogLevel> LogLevelFormatter { get; set; } = new LogLevelFormatter();

        protected virtual void OnDispose()
        {

        }

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
        private bool _isDisposed;
        
    }
}
