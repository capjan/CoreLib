using System.Collections.Generic;
using Core.Logging;
using Core.Logging.Targets;

namespace Core.Test.LoggingRelated;

/// <summary>
/// Collects log events in memory.
/// </summary>
/// <remarks>
/// The log event is process-wide (static), so events of tests running in parallel end up in every connected target.
/// Pass <c>onlyFromClass</c> to collect only the events of the class under test.
/// </remarks>
internal class DumpLogTarget : LogTarget
{
    private readonly string? _onlyFromClass;

    public DumpLogTarget(string? onlyFromClass = null)
    {
        _onlyFromClass = onlyFromClass;
    }

    /// <inheritdoc />
    protected override void OnLog(LogEventArgs itm)
    {
        if (_onlyFromClass != null && itm.CallerClassFullName != _onlyFromClass)
            return;
        EventLog.Add(itm);
    }

    public List<LogEventArgs> EventLog { get; } = new List<LogEventArgs>();
}
