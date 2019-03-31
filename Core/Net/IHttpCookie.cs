using System;

namespace Core.Net
{
    public interface IHttpCookie
    {
        string   Domain     { get; }
        string   Path       { get; }
        DateTime ExpiresUtc { get; }
        bool     IsSecure   { get; }
        bool     HttpOnly   { get; }
        string   Name       { get; set; }
        string   Value      { get; set; }
    }
    
}
