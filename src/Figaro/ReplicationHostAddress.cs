namespace Figaro
{
    /// <summary>
    /// The address of a replication site used by Figaro High Availability (HA).
    /// </summary>
    public class ReplicationHostAddress
    {
        /// <summary>
        /// The site's host identification string, generally a TCP/IP host name. 
        /// </summary>
        public string Host;
        /// <summary>
        /// The port number on which the site is receiving. 
        /// </summary>
        public uint Port;

        /// <summary>
        /// Instantiate a new, empty address
        /// </summary>
        public ReplicationHostAddress() { }

        /// <summary>
        /// Instantiates a new address with the given TCP/IP address and port.
        /// </summary>
        /// <param name="host">The site's host identification string.</param>
        /// <param name="port">The port number on which the site is receiving.</param>
        public ReplicationHostAddress(string host, uint port)
        {
            Host = host;
            Port = port;
        }

        /// <summary>
        /// Instantiates a new address, parsing the host and port from the given address.
        /// </summary>
        /// <param name="hostAndPort">A string in host:port format</param>
        public ReplicationHostAddress(string hostAndPort)
        { }

    }
}
