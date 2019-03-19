namespace Core.Text.Impl
{
    public class DefaultAlphabet : IAlphabet
    {
        // Must be ordered: digits -> uppercase letters -> lowercase letters
        private const string FixedAlphabet = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";

        public DefaultAlphabet()
        {
            Chars = FixedAlphabet;
            IndexFirstUpperCaseLetter = Chars.IndexOf('A');
            IndexLastUppercaseLetter  = Chars.IndexOf('Z');
            IndexFirstLowerCaseLetter = Chars.IndexOf('a');
            IndexLastLowerCaseLetter  = Chars.IndexOf('z');
            IndexFirstDigit           = Chars.IndexOf('0');
            IndexLastDigit            = Chars.IndexOf('9');
        }

        public string Chars                     { get; }
        public int    IndexFirstUpperCaseLetter { get; }
        public int    IndexLastUppercaseLetter  { get; }
        public int    IndexFirstLowerCaseLetter { get; }
        public int    IndexLastLowerCaseLetter  { get; }
        public int    IndexFirstDigit           { get; }
        public int    IndexLastDigit            { get; }
    }
}
