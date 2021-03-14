using System.IO;
using Core.Mathematics;
using Core.Mathematics.Impl;
// ReSharper disable StringLiteralTypo

namespace Core.Text.Generator.Impl
{
    /// <summary>
    /// a simple lorem ipsum text generator
    /// </summary>
    public class LoremIpsumGenerator : ILoremIpsumGenerator
    {
        public LoremIpsumGenerator(IRandom random = default)
        {
            _random = random ?? new DefaultRandom();
        }

        public void WriteText(int wordCount, TextWriter output)
        {
            var wordsWritten = 0;
            // special handling of the first 5 words
            while (wordsWritten < wordCount && wordsWritten < 5)
            {
                if (wordsWritten != 0) output.Write(' ');
                output.Write(Words[wordsWritten]);
                wordsWritten++;
            }

            while (wordsWritten < wordCount)
            {
                var randomWord = Words[_random.Next(Words.Length - 1)];
                output.Write(' ');
                output.Write(randomWord);
                wordsWritten++;
            }
        }

        private readonly IRandom _random;
        private static readonly string[] Words =
        {
            "lorem", "ipsum", "dolor", "sit", "amet", "consetetur", "sadipscing", "elitr", "sed", "diam", "nonumy", "eirmod",
            "tempor", "invidunt", "ut", "labore", "et", "dolore", "magna", "aliquyam", "erat", "sed", "diam", "voluptua",
            "at", "vero", "eos", "et", "accusam", "et", "justo", "duo", "dolores", "et", "ea", "rebum", "stet", "clita",
            "kasd", "gubergren", "no", "sea", "takimata", "sanctus", "est", "lorem", "ipsum", "dolor", "sit", "amet",
            "lorem", "ipsum", "dolor", "sit", "amet", "consetetur", "sadipscing", "elitr", "sed", "diam", "nonumy",
            "eirmod", "tempor", "invidunt", "ut", "labore", "et", "dolore", "magna", "aliquyam", "erat", "sed", "diam",
            "voluptua", "at", "vero", "eos", "et", "accusam", "et", "justo", "duo", "dolores", "et", "ea", "rebum",
            "stet", "clita", "kasd", "gubergren", "no", "sea", "takimata", "sanctus", "est", "lorem", "ipsum", "dolor",
            "sit", "amet", "lorem", "ipsum", "dolor", "sit", "amet", "consetetur", "sadipscing", "elitr", "sed", "diam",
            "nonumy", "eirmod", "tempor", "invidunt", "ut", "labore", "et", "dolore", "magna", "aliquyam", "erat", "sed",
            "diam", "voluptua", "at", "vero", "eos", "et", "accusam", "et", "justo", "duo", "dolores", "et", "ea",
            "rebum", "stet", "clita", "kasd", "gubergren", "no", "sea", "takimata", "sanctus", "est", "lorem", "ipsum",
            "dolor", "sit", "amet", "duis", "autem", "vel", "eum", "iriure", "dolor", "in", "hendrerit", "in",
            "vulputate", "velit", "esse", "molestie", "consequat", "vel", "illum", "dolore", "eu", "feugiat", "nulla",
            "facilisis", "at", "vero", "eros", "et", "accumsan", "et", "iusto", "odio", "dignissim", "qui", "blandit",
            "praesent", "luptatum", "zzril", "delenit", "augue", "duis", "dolore", "te", "feugait", "nulla", "facilisi",
            "lorem", "ipsum", "dolor", "sit", "amet", "consectetuer", "adipiscing", "elit", "sed", "diam", "nonummy",
            "nibh", "euismod", "tincidunt", "ut", "laoreet", "dolore", "magna", "aliquam", "erat", "volutpat", "ut",
            "wisi", "enim", "ad", "minim", "veniam", "quis", "nostrud", "exerci", "tation", "ullamcorper", "suscipit",
            "lobortis", "nisl", "ut", "aliquip", "ex", "ea", "commodo", "consequat", "duis", "autem", "vel", "eum",
            "iriure", "dolor", "in", "hendrerit", "in", "vulputate", "velit", "esse", "molestie", "consequat", "vel",
            "illum", "dolore", "eu", "feugiat", "nulla", "facilisis", "at", "vero", "eros", "et", "accumsan", "et",
            "iusto", "odio", "dignissim", "qui", "blandit", "praesent", "luptatum", "zzril", "delenit", "augue", "duis",
            "dolore", "te", "feugait", "nulla", "facilisi", "nam", "liber", "tempor", "cum", "soluta", "nobis",
            "eleifend", "option", "congue", "nihil", "imperdiet", "doming", "id", "quod", "mazim", "placerat", "facer",
            "possim", "assum", "lorem", "ipsum", "dolor", "sit", "amet", "consectetuer", "adipiscing", "elit", "sed",
            "diam", "nonummy", "nibh", "euismod", "tincidunt", "ut", "laoreet", "dolore", "magna", "aliquam", "erat",
            "volutpat", "ut", "wisi", "enim", "ad", "minim", "veniam", "quis", "nostrud", "exerci", "tation",
            "ullamcorper", "suscipit", "lobortis", "nisl", "ut", "aliquip", "ex", "ea", "commodo", "consequat", "duis",
            "autem", "vel", "eum", "iriure", "dolor", "in", "hendrerit", "in", "vulputate", "velit", "esse", "molestie",
            "consequat", "vel", "illum", "dolore", "eu", "feugiat", "nulla", "facilisis", "at", "vero", "eos", "et",
            "accusam", "et", "justo", "duo", "dolores", "et", "ea", "rebum", "stet", "clita", "kasd", "gubergren", "no",
            "sea", "takimata", "sanctus", "est", "lorem", "ipsum", "dolor", "sit", "amet", "lorem", "ipsum", "dolor",
            "sit", "amet", "consetetur", "sadipscing", "elitr", "sed", "diam", "nonumy", "eirmod", "tempor", "invidunt",
            "ut", "labore", "et", "dolore", "magna", "aliquyam", "erat", "sed", "diam", "voluptua", "at", "vero", "eos",
            "et", "accusam", "et", "justo", "duo", "dolores", "et", "ea", "rebum", "stet", "clita", "kasd", "gubergren",
            "no", "sea", "takimata", "sanctus", "est", "lorem", "ipsum", "dolor", "sit", "amet", "lorem", "ipsum",
            "dolor", "sit", "amet", "consetetur", "sadipscing", "elitr", "at", "accusam", "aliquyam", "diam", "diam",
            "dolore", "dolores", "duo", "eirmod", "eos", "erat", "et", "nonumy", "sed", "tempor", "et", "et", "invidunt",
            "justo", "labore", "stet", "clita", "ea", "et", "gubergren", "kasd", "magna", "no", "rebum", "sanctus",
            "sea", "sed", "takimata", "ut", "vero", "voluptua", "est", "lorem", "ipsum", "dolor", "sit", "amet", "lorem",
            "ipsum", "dolor", "sit", "amet", "consetetur", "sadipscing", "elitr", "sed", "diam", "nonumy", "eirmod",
            "tempor", "invidunt", "ut", "labore", "et", "dolore", "magna", "aliquyam", "erat", "consetetur",
            "sadipscing", "elitr", "sed", "diam", "nonumy", "eirmod", "tempor", "invidunt", "ut", "labore", "et",
            "dolore", "magna", "aliquyam", "erat", "sed", "diam", "voluptua", "at", "vero", "eos", "et", "accusam", "et",
            "justo", "duo", "dolores", "et", "ea", "rebum", "stet", "clita", "kasd", "gubergren", "no", "sea",
            "takimata", "sanctus", "est", "lorem", "ipsum"
        };
    }
}
