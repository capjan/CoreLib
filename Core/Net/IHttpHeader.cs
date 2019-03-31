using System;

namespace Core.Net
{
    public interface IHttpHeader
    {
        /// <summary>
        /// True, if the server allows reading ranges of the content.
        /// </summary>
        bool      AcceptRanges    { get; }

        /// <summary>
        /// Http connection information 
        /// </summary>
        string    Connection      { get; }

        /// <summary>
        /// Content Length in Bytes
        /// </summary>
        long?     ContentLength   { get; }

        /// <summary>
        /// Mime Type of Content
        /// </summary>
        string    ContentType     { get; }

        /// <summary>
        /// DateTime UTC - Time at which the header was sent from the server.
        /// </summary>
        DateTime? CreatedAtUtc    { get; }

        /// <summary>
        /// Entity Tag (eTag) of the requested File. Should be unique - desired to identify unique resources.
        /// </summary>
        string    EntityTag       { get; }

        /// <summary>
        /// DateTime UTC - Time at which the file was last modified on the server.
        /// </summary>
        DateTime? LastModifiedUtc { get; }

        /// <summary>
        /// Location of the requested resource. Only set if redirection is intended by the server.
        /// </summary>
        string Location { get; }

        /// <summary>
        /// String Name of the Server
        /// </summary>
        string    Server          { get; }
        string SetCookie { get; }

        // Non standard 
        string Status { get; }
        
    }
}