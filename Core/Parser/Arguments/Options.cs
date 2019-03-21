namespace Core.Parser.Arguments
{
    public delegate void OptionAction<in TKey, in TValue>(TKey key, TValue value);
}
