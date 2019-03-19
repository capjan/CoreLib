namespace Core.Text.Impl
{
    public class TextGenUtil : ITextGenUtil
    {
        public TextGenUtil(
            IRandomStringGenerator randomStringGenerator = default)
        {
            _randomStringGenerator = randomStringGenerator ?? new RandomStringGenerator();
        }
        public string CreateAlphanumericString(int length)
        {
            return _randomStringGenerator.CreateAlphanumericString(length);
        }

        private readonly IRandomStringGenerator _randomStringGenerator;
        
    }
}
