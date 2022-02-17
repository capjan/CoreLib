using System;
using Core.ControlFlow;
// ReSharper disable UnusedMember.Global

namespace Core.Extensions.ControlFlowRelated;

public static class FuncExt
{
    public static MultiAttemptFunc<T> WithMultipleAttempts<T>(this Func<T> func, int maxAttempts = 5, TimeSpan attemptDelay = default)
    {
        return new MultiAttemptFunc<T>(func, maxAttempts, attemptDelay);
    }

    public static MultiAttemptFunc<T1, T2> WithMultipleAttempts<T1, T2>(this Func<T1, T2> func, int maxAttempts = 5, TimeSpan attemptDelay = default)
    {
        return new MultiAttemptFunc<T1, T2>(func, maxAttempts, attemptDelay);
    }

    public static MultiAttemptFunc<T1, T2, T3> WithMultipleAttempts<T1, T2, T3>(this Func<T1, T2, T3> func, int maxAttempts = 5, TimeSpan attemptDelay = default)
    {
        return new MultiAttemptFunc<T1, T2, T3>(func, maxAttempts, attemptDelay);
    }

    public static MultiAttemptFunc<T1, T2, T3, T4> WithMultipleAttempts<T1, T2, T3, T4>(this Func<T1, T2, T3, T4> func, int maxAttempts = 5, TimeSpan attemptDelay = default)
    {
        return new MultiAttemptFunc<T1, T2, T3, T4>(func, maxAttempts, attemptDelay);
    }

    public static MultiAttemptFunc<T1, T2, T3, T4, T5> WithMultipleAttempts<T1, T2, T3, T4, T5>(this Func<T1, T2, T3, T4, T5> func, int maxAttempts = 5, TimeSpan attemptDelay = default)
    {
        return new MultiAttemptFunc<T1, T2, T3, T4, T5>(func, maxAttempts, attemptDelay);
    }

    public static MultiAttemptFunc<T1, T2, T3, T4, T5, T6> WithMultipleAttempts<T1, T2, T3, T4, T5, T6>(this Func<T1, T2, T3, T4, T5, T6> func, int maxAttempts = 5, TimeSpan attemptDelay = default)
    {
        return new MultiAttemptFunc<T1, T2, T3, T4, T5, T6>(func, maxAttempts, attemptDelay);
    }

    public static MultiAttemptFunc<T1, T2, T3, T4, T5, T6, T7> WithMultipleAttempts<T1, T2, T3, T4, T5, T6, T7>(this Func<T1, T2, T3, T4, T5, T6, T7> func, int maxAttempts = 5, TimeSpan attemptDelay = default)
    {
        return new MultiAttemptFunc<T1, T2, T3, T4, T5, T6, T7>(func, maxAttempts, attemptDelay);
    }
}