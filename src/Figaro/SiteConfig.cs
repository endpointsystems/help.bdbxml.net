using System;

namespace Figaro
{
    /// <summary>
    /// Configuration properties for a site in <see cref="FigaroReplicationManager"/>.
    /// </summary>
    public class SiteConfig
    {
        /// <summary>
        /// The host of the site.
        /// </summary>
        public string Host { get; set; }
        /// <summary>
        /// The port of the site.
        /// </summary>
        public UInt32 Port { get; set; }

        /// <summary>
        /// Gets or sets whether the site is a group creator.
        /// </summary>
        /// <remarks>
        /// Only the local site could be applied as a group creator. The group
        /// creator would create the initial membership database, defining a
        /// replication group of just the one site, rather than trying to join
        /// an existing group when it starts for the first time.
        /// </remarks>
        /// <value>False</value>
        public bool SiteCreator { get; set; }
        /// <summary>
        /// Gets or sets whether the site is a helper site.
        /// </summary>
        /// <remarks>
        /// A remote site may be a helper site when the local site first joins
        /// the replication group. Once the local site has been established as
        /// a member of the group, this config setting is ignored. 
        /// </remarks>
        public bool Helper { get; set; }

        /// <summary>
        /// Whether the site is within a legacy group.
        /// </summary>
        /// <remarks>
        /// Specify the site in a legacy group. It would be considered as part
        /// of an existing group, upgrading from a previous version of Berkeley DB. All
        /// sites in the legacy group must specify this for themselves (the 
        /// local site) and for all other sites initially in the group. 
        /// </remarks>
        public bool Legacy { get; set; }
        /// <summary>
        /// Gets or sets whether the site is the local one.
        /// </summary>
        public bool LocalSite { get; set; }
        /// <summary>
        /// Gets or sets whether the site is peer to local site. 
        /// </summary>
        /// <remarks>
        /// A peer site may be used as a target for "client-to-client" 
        /// synchronization messages. It only makes sense to specify this for a
        /// remote site. 
        /// </remarks>
        public bool Peer { get; set; }

    }
}
