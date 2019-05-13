using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Extensions.CollectionRelated;

namespace Core.Net.Impl
{
    public class DefaultUrlBuilder : IUrlBuilder
    {
        private readonly IUrlEncoder _urlEncoder;

        private readonly List<KeyValuePair<string,string>> _nameValueCollection = new List<KeyValuePair<string,string>>();
        private readonly string _scheme;
        private readonly string _host;
        private readonly IList<string> _segments;
        private int? _port;
        private string _username;
        private string _password;

        public DefaultUrlBuilder(string baseUrl, IUrlEncoder urlEncoder = default)
        {
            var uri = new Uri(baseUrl);
            _scheme = uri.Scheme;
            _host = uri.Host;
            _segments = uri
                         .Segments
                         .Where(s => s != "/")
                         .Select(s=>s.Trim('/'))
                         .ToList();
            _urlEncoder = urlEncoder ?? new DefaultUrlEncoder();
        }

        public IUrlBuilder Credentials(string userName, string password)
        {
            _username = userName;
            _password = password;
            return this;
        }

        public IUrlBuilder Port(int? value)
        {
            _port = value;
            return this;
        }

        public IUrlBuilder AddPath(params string[] paths)
        {
            foreach (var path in paths)
                _segments.Add(path);

            return this;
        }

        public IUrlBuilder AddParam(string key, string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return this;
            _nameValueCollection.Add(new KeyValuePair<string, string>(key, value));
            return this;
        }

        public IUrlBuilder ClearPath()
        {
            _segments.Clear();
            return this;
        }

        public IUrlBuilder ClearParameter()
        {
            _nameValueCollection.Clear();
            return this;
        }

        public string Build()
        {

            var sb = new StringBuilder();

            sb.Append(_scheme);
            sb.Append("://");

            if (_username != null)
            {
                sb.Append(_urlEncoder.Encode(_username));
                if (_password != null)
                {
                    sb.Append($":{_urlEncoder.Encode(_password)}");
                }
                sb.Append("@");
            }

            sb.Append(_host);
            if (_port != null)
                sb.Append($":{_port}");

            foreach (var segment in _segments)
            {
                sb.Append("/");
                sb.Append(segment);
            }

            if (_nameValueCollection.Count == 0)
                return sb.ToString();

            sb.Append("?");
            sb.Append(_nameValueCollection
                      .Select(i => $"{_urlEncoder.Encode(i.Key)}={_urlEncoder.Encode(i.Value)}")
                      .ToSeparatedString(separator: "&"));

            return sb.ToString();
        }
    }
}
