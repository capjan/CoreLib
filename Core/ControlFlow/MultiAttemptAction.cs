using System;
using System.Threading;

namespace Core.ControlFlow
{
    public class MultiAttemptActionCallback
    {
        public const int MaxAttempts = 5;

        public MultiAttemptActionCallback(
            int maxAttempts = MaxAttempts, 
            TimeSpan attemptDelay = default)
        {            
            _maxAttempts = maxAttempts;
            _attemptDelay = attemptDelay;
        }

        public int AttemptCount { get; private set; }

        public void Invoke(Action action)
        {            
            AttemptCount = 0;
            while (true)
            {
                try
                {
                    ++AttemptCount;
                    action.Invoke();
                    return;
                }
                catch (Exception)
                {
                    if (AttemptCount > _maxAttempts)
                        throw;
                    Thread.Sleep(_attemptDelay);
                }
            }
        }
        
        private readonly TimeSpan _attemptDelay;
        private readonly int      _maxAttempts;
    }

    public class AbstractMultiAttemptAction
    {
        public AbstractMultiAttemptAction(int maxAttempts, TimeSpan attemptDelay)
        {
            Invoker = new MultiAttemptActionCallback(maxAttempts, attemptDelay);
        }

        public int AttemptCount => Invoker.AttemptCount;
        protected readonly MultiAttemptActionCallback Invoker;
    }

    public class MultiAttemptAction : AbstractMultiAttemptAction
    {
        public MultiAttemptAction(
            Action action, int maxAttempts = MultiAttemptActionCallback.MaxAttempts, TimeSpan attemptDelay = default) :
            base(maxAttempts, attemptDelay)
        {
            _action = action;
        }

        public void Invoke()
        {
            Invoker.Invoke(_action);
        }

        private readonly Action _action;
    }

    public class MultiAttemptAction<T> : AbstractMultiAttemptAction
    {
        public MultiAttemptAction(Action<T> action, int maxAttempts = MultiAttemptActionCallback.MaxAttempts, TimeSpan attemptDelay = default) :
            base(maxAttempts, attemptDelay)
        {
            _action = action;
        }

        public void Invoke(T arg)
        {
            Invoker.Invoke(()=>_action(arg));
        }

        private readonly Action<T> _action;
    }

    public class MultiAttemptAction<T1, T2> : AbstractMultiAttemptAction
    {
        public MultiAttemptAction(Action<T1, T2> action, int maxAttempts = MultiAttemptActionCallback.MaxAttempts, TimeSpan attemptDelay = default) :
            base(maxAttempts, attemptDelay)
        {
            _action        = action;
        }

        public void Invoke(T1 arg1, T2 arg2)
        {
            Invoker.Invoke(()=>_action(arg1, arg2));
        }

        private readonly Action<T1, T2>          _action;
    }

    public class MultiAttemptAction<T1, T2, T3> : AbstractMultiAttemptAction
    {
        public MultiAttemptAction(Action<T1, T2, T3> action, int maxAttempts = MultiAttemptActionCallback.MaxAttempts, TimeSpan attemptDelay = default) :
            base(maxAttempts, attemptDelay)
        {
            _action        = action;
        }

        public void Invoke(T1 arg1, T2 arg2, T3 arg3)
        {
            Invoker.Invoke(()=>_action(arg1, arg2, arg3));
        }
        
        private readonly Action<T1, T2, T3>     _action;
    }

    public class MultiAttemptAction<T1, T2, T3, T4> : AbstractMultiAttemptAction
    {
        public MultiAttemptAction(Action<T1, T2, T3, T4> action, int maxAttempts = MultiAttemptActionCallback.MaxAttempts, TimeSpan attemptDelay = default) :
            base(maxAttempts, attemptDelay)
        {
            _action        = action;
        }

        public void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            Invoker.Invoke(()=>_action(arg1, arg2, arg3, arg4));
        }

        private readonly Action<T1, T2, T3, T4> _action;
    }

    public class MultiAttemptAction<T1, T2, T3, T4, T5> : AbstractMultiAttemptAction
    {
        public MultiAttemptAction(Action<T1, T2, T3, T4, T5> action, int maxAttempts = MultiAttemptActionCallback.MaxAttempts, TimeSpan attemptDelay = default) :
            base(maxAttempts, attemptDelay)
        {
            _action        = action;
        }

        public void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
        {
            Invoker.Invoke(()=>_action(arg1, arg2, arg3, arg4, arg5));
        }

        private readonly Action<T1, T2, T3, T4, T5> _action;
    }

    public class MultiAttemptAction<T1, T2, T3, T4, T5, T6> : AbstractMultiAttemptAction
    {
        public MultiAttemptAction(Action<T1, T2, T3, T4, T5, T6> action, int maxAttempts = MultiAttemptActionCallback.MaxAttempts, TimeSpan attemptDelay = default) :
            base(maxAttempts, attemptDelay)
        {
            _action        = action;
        }

        public void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
        {
            Invoker.Invoke(()=>_action(arg1, arg2, arg3, arg4, arg5, arg6));
        }

        private readonly Action<T1, T2, T3, T4, T5, T6> _action;
    }

    public class MultiAttemptAction<T1, T2, T3, T4, T5, T6, T7> : AbstractMultiAttemptAction
    {
        public MultiAttemptAction(Action<T1, T2, T3, T4, T5, T6, T7> action, int maxAttempts = MultiAttemptActionCallback.MaxAttempts, TimeSpan attemptDelay = default) :
            base(maxAttempts, attemptDelay)
        {            
            _action        = action;
        }

        public void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7)
        {
            Invoker.Invoke(()=>_action(arg1, arg2, arg3, arg4, arg5, arg6, arg7));
        }
        
        private readonly Action<T1, T2, T3, T4, T5, T6, T7> _action;
    }
}
