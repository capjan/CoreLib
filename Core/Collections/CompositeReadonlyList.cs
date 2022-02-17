using System.Collections;
using System.Collections.Generic;

namespace Core.Collections;

public class CompositeReadOnlyList<T> : IReadOnlyList<T>
{
    #region Fields

    private readonly IReadOnlyList<T> _first;
    private readonly IReadOnlyList<T> _second;

    #endregion

    public int Count => _first.Count + _second.Count;

    public T this[int index] => index < _first.Count
        ? _first[index]
        : _second[index - _first.Count];

    public CompositeReadOnlyList(IReadOnlyList<T> first, IReadOnlyList<T> second)
    {
        _first  = first;
        _second = second;
    }

    public IEnumerator<T> GetEnumerator()
    {
        return new CompositeReadOnlyListEnumerator(this);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    #region Helper Classes

    private class CompositeReadOnlyListEnumerator : IEnumerator<T>
    {
        #region Fields

        private readonly CompositeReadOnlyList<T> _list;

        private int _i = -1;

        #endregion


        public T Current => _list[_i];

        // object IEnumerator.Current => Current;

        public CompositeReadOnlyListEnumerator(CompositeReadOnlyList<T> list)
        {
            _list = list;
        }

        public bool MoveNext()
        {
            ++_i;
            return _i < _list.Count;
        }

        public void Reset()
        {
            _i = -1;
        }

        object? IEnumerator.Current => _list[_i];

        public void Dispose()
        {
        }
    }

    #endregion
}
