using System;
using Core.ControlFlow;
// ReSharper disable UnusedMember.Global

namespace Core.Extensions.ControlFlowRelated
{
    public static class ActionExt
    {
        public static MultiAttemptAction WithMultipleAttempts(this Action action, int maxAttempts = 5, TimeSpan attemptDelay = default)
        {
            return new MultiAttemptAction(action, maxAttempts, attemptDelay);
        }

        public static MultiAttemptAction<T> WithMultipleAttempts<T>(this Action<T> action, int maxAttempts = 5, TimeSpan attemptDelay = default)
        {
            return new MultiAttemptAction<T>(action, maxAttempts, attemptDelay);
        }

        public static MultiAttemptAction<T1, T2> WithMultipleAttempts<T1, T2>(this Action<T1, T2> action, int maxAttempts = 5, TimeSpan attemptDelay = default)
        {
            return new MultiAttemptAction<T1, T2>(action, maxAttempts, attemptDelay);
        }

        public static MultiAttemptAction<T1, T2, T3> WithMultipleAttempts<T1, T2, T3>(this Action<T1, T2, T3> action, int maxAttempts = 5, TimeSpan attemptDelay = default)
        {
            return new MultiAttemptAction<T1, T2, T3>(action, maxAttempts, attemptDelay);
        }

        public static MultiAttemptAction<T1, T2, T3, T4> WithMultipleAttempts<T1, T2, T3, T4>(this Action<T1, T2, T3, T4> action, int maxAttempts = 5, TimeSpan attemptDelay = default)
        {
            return new MultiAttemptAction<T1, T2, T3, T4>(action, maxAttempts, attemptDelay);
        }

        public static MultiAttemptAction<T1, T2, T3, T4, T5> WithMultipleAttempts<T1, T2, T3, T4, T5>(this Action<T1, T2, T3, T4, T5> action, int maxAttempts = 5, TimeSpan attemptDelay = default)
        {
            return new MultiAttemptAction<T1, T2, T3, T4, T5>(action, maxAttempts, attemptDelay);
        }

        public static MultiAttemptAction<T1, T2, T3, T4, T5, T6> WithMultipleAttempts<T1, T2, T3, T4, T5, T6>(this Action<T1, T2, T3, T4, T5, T6> action, int maxAttempts = 5, TimeSpan attemptDelay = default)
        {
            return new MultiAttemptAction<T1, T2, T3, T4, T5, T6>(action, maxAttempts, attemptDelay);
        }

        public static MultiAttemptAction<T1, T2, T3, T4, T5, T6, T7> WithMultipleAttempts<T1, T2, T3, T4, T5, T6, T7>(this Action<T1, T2, T3, T4, T5, T6, T7> action, int maxAttempts = 5, TimeSpan attemptDelay = default)
        {
            return new MultiAttemptAction<T1, T2, T3, T4, T5, T6, T7>(action, maxAttempts, attemptDelay);
        }
    }
}
