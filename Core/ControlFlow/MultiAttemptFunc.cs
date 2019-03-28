using System;
using System.Threading;

namespace Core.ControlFlow
{
    public class MultiAttemptFuncCallback<T>
    {
        public const int MaxAttempts = 5;

        public MultiAttemptFuncCallback(            
            int      maxAttempts  = MaxAttempts, 
            TimeSpan attemptDelay = default)
        {            
            _maxAttempts  = maxAttempts;
            _attemptDelay = attemptDelay;
        }

        public int AttemptCount { get; private set; }
        public T Invoke(Func<T> func)
        {            
            AttemptCount = 0;
            while (true)
            {
                ++AttemptCount;
                try
                {
                    return func();                    
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

    public abstract class AbstractMultiAttemptFunc<TResult>
    {
        public AbstractMultiAttemptFunc(            
            int           maxAttempts  = MultiAttemptFuncCallback<TResult>.MaxAttempts, 
            TimeSpan      attemptDelay = default)
        {
            Invoker = new MultiAttemptFuncCallback<TResult>(maxAttempts, attemptDelay);            
        }
        
        protected readonly MultiAttemptFuncCallback<TResult> Invoker;
    }

    public class MultiAttemptFunc<TResult> : AbstractMultiAttemptFunc<TResult>
    {
        public MultiAttemptFunc(
            Func<TResult> func,
            int           maxAttempts  = MultiAttemptFuncCallback<TResult>.MaxAttempts,
            TimeSpan      attemptDelay = default) : base(maxAttempts, attemptDelay)
        {
            _func = func;
        }

        public TResult Invoke()
        {
            return Invoker.Invoke(() => _func.Invoke());
        }

        private readonly Func<TResult> _func;
    }

    public class MultiAttemptFunc<TArg, TResult> : AbstractMultiAttemptFunc<TResult>
    {
        public MultiAttemptFunc(
            Func<TArg, TResult> func, 
            int      maxAttempts  = MultiAttemptFuncCallback<TResult>.MaxAttempts,
            TimeSpan attemptDelay = default) : base(maxAttempts, attemptDelay)
        {            
            _func = func;            
        }

        public TResult Invoke(TArg arg)
        {
            return Invoker.Invoke(() => _func.Invoke(arg));            
        }
        
        private readonly Func<TArg, TResult> _func;
    }

    public class MultiAttemptFunc<T1, T2, TResult> : AbstractMultiAttemptFunc<TResult>
    {
        public MultiAttemptFunc(
            Func<T1, T2, TResult> func, 
            int      maxAttempts  = MultiAttemptFuncCallback<TResult>.MaxAttempts,
            TimeSpan attemptDelay = default) : base(maxAttempts, attemptDelay)
        {            
            _func                        = func;            
        }

        public TResult Invoke(T1 arg1, T2 arg2)
        {
            return Invoker.Invoke(() => _func.Invoke(arg1, arg2));            
        }
        
        private readonly Func<T1, T2, TResult> _func;
    }

    public class MultiAttemptFunc<T1, T2, T3, TResult> : AbstractMultiAttemptFunc<TResult>
    {
        public MultiAttemptFunc(
            Func<T1, T2, T3, TResult> func, 
            int      maxAttempts  = MultiAttemptFuncCallback<TResult>.MaxAttempts,
            TimeSpan attemptDelay = default) : base(maxAttempts, attemptDelay)
        {            
            _func                        = func;            
        }

        public TResult Invoke(T1 arg1, T2 arg2, T3 arg3)
        {
            return Invoker.Invoke(() => _func.Invoke(arg1, arg2, arg3));            
        }
        
        private readonly Func<T1, T2, T3, TResult> _func;
    }

    public class MultiAttemptFunc<T1, T2, T3, T4, TResult> : AbstractMultiAttemptFunc<TResult>
    {
        public MultiAttemptFunc(
            Func<T1, T2, T3, T4, TResult> func, 
            int      maxAttempts  = MultiAttemptFuncCallback<TResult>.MaxAttempts,
            TimeSpan attemptDelay = default) : base(maxAttempts, attemptDelay)
        {            
            _func                        = func;            
        }

        public TResult Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            return Invoker.Invoke(() => _func.Invoke(arg1, arg2, arg3, arg4));            
        }
        
        private readonly Func<T1, T2, T3, T4, TResult> _func;
    }

    public class MultiAttemptFunc<T1, T2, T3, T4, T5, TResult> : AbstractMultiAttemptFunc<TResult>
    {
        public MultiAttemptFunc(
            Func<T1, T2, T3, T4, T5, TResult> func, 
            int      maxAttempts  = MultiAttemptFuncCallback<TResult>.MaxAttempts,
            TimeSpan attemptDelay = default) : base(maxAttempts, attemptDelay)
        {            
            _func                        = func;            
        }

        public TResult Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
        {
            return Invoker.Invoke(() => _func.Invoke(arg1, arg2, arg3, arg4, arg5));            
        }
        
        private readonly Func<T1, T2, T3, T4, T5, TResult> _func;
    }

    public class MultiAttemptFunc<T1, T2, T3, T4, T5, T6, TResult> : AbstractMultiAttemptFunc<TResult>
    {
        public MultiAttemptFunc(
            Func<T1, T2, T3, T4, T5, T6, TResult> func, 
            int      maxAttempts  = MultiAttemptFuncCallback<TResult>.MaxAttempts,
            TimeSpan attemptDelay = default) : base(maxAttempts, attemptDelay)
        {            
            _func                        = func;            
        }

        public TResult Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
        {
            return Invoker.Invoke(() => _func.Invoke(arg1, arg2, arg3, arg4, arg5, arg6));            
        }

        private readonly Func<T1, T2, T3, T4, T5, T6, TResult> _func;
    }

    public class MultiAttemptFunc<T1, T2, T3, T4, T5, T6, T7, TResult> : AbstractMultiAttemptFunc<TResult>
    {
        public MultiAttemptFunc(
            Func<T1, T2, T3, T4, T5, T6, T7, TResult> func, 
            int      maxAttempts  = MultiAttemptFuncCallback<TResult>.MaxAttempts,
            TimeSpan attemptDelay = default) : base(maxAttempts, attemptDelay)
        {            
            _func                        = func;            
        }

        public TResult Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7)
        {
            return Invoker.Invoke(() => _func.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7));            
        }

        private readonly Func<T1, T2, T3, T4, T5, T6, T7, TResult> _func;
    }
}
