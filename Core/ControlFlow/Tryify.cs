using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ControlFlow
{
    public class Tryify<T>
    {
        public bool TryInvoke(Func<T> callback, out T result)
        {
            try
            {
                result = callback();
                return true;
            }
            catch (Exception)
            {
                result = default;
                return false;
            }
        }
    }
}
