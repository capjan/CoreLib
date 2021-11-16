using System.IO;
using Core.Text.Generator;
using Core.Text.Generator.Impl;

namespace Core.IO.Impl
{
    public class DefaultPathNameGenerator : IPathNameGenerator
    {
        private readonly string _prefix;
        private readonly string _postfix;
        private readonly int    _randomLength;
        private readonly IRandomStringGenerator _nameGenerator;

        public DefaultPathNameGenerator(
            string prefix           = "", 
            string postfix          = "", 
            int    randomLength     = 5,
            IRandomStringGenerator? nameGenerator = default)
        {
            _nameGenerator = nameGenerator ?? new RandomStringGenerator();
            _prefix = prefix;
            _postfix = postfix;
            _randomLength = randomLength;
        }
        
        public string Generate(string rootDir)
        {
            while (true)
            {
                var randomChars = _nameGenerator.CreateAlphanumericString(_randomLength);
                var name = _prefix + randomChars + _postfix;
                var fullPath = Path.Combine(rootDir, name);

                if (!Directory.Exists(fullPath) && !File.Exists(fullPath))
                    return fullPath;
            }     
        }
    }
}
