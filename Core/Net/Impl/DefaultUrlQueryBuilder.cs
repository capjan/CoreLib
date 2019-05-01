using System.Collections.Generic;
using System.Linq;
using Core.Extensions.CollectionRelated;

namespace Core.Net.Impl
{
    public class DefaultUrlQueryBuilder : IUrlQueryBuilder
    {
        private readonly IUrlEncoder _urlEncoder;
        private readonly string                            _baseUrl;
        private readonly List<KeyValuePair<string,string>> _nameValueCollection = new List<KeyValuePair<string,string>>();

        public DefaultUrlQueryBuilder(string baseUrl, IUrlEncoder urlEncoder = default)
        {
            _baseUrl = baseUrl;
            _urlEncoder = urlEncoder ?? new DefaultUrlEncoder();
        }

        public IUrlQueryBuilder AddParameter(string key, string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return this;
            _nameValueCollection.Add(new KeyValuePair<string, string>(key, value));
            return this;
        }

        public string Build()
        {
            if (_nameValueCollection.Count == 0)
                return _baseUrl;
            
            var parameter = _nameValueCollection
                            .Select(i=> $"{_urlEncoder.Encode(i.Key)}={_urlEncoder.Encode(i.Value)}")
                            .ToSeparatedString(separator:"&");
            
            return $"{_baseUrl}?{parameter}";
        }
    }
}
