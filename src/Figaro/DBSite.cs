using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// ReSharper disable UnusedAutoPropertyAccessor.Local

namespace Figaro
{
    /// <summary>
    /// The replication site for Figaro High Availability (HA). 
    /// </summary>
    /// <remarks>
    /// DBSite is used by the 
    /// <see cref="FigaroReplicationManager"/> to manage and configure replication sites. 
    /// </remarks>
    public class DBSite
    {
        /// <summary>
        /// Creates a new instance of the <see cref="DBSite"/> object.
        /// </summary>
        public DBSite()
        {

        }

        /// <summary>
        /// Specifies that this site should create the initial group membership database contents, 
        /// defining a "group" of just the one site, rather than trying to join an existing group when 
        /// it starts for the first time.
        /// </summary>
        /// <remarks>
        /// This setting can only be used on the local site.It is ignored after the local site's initial 
        /// startup and when configured for a remote site.
        /// </remarks>
        /// <value>True</value>
        public bool GroupCreator { get; set; }

        /// <summary>
        /// Specifies that a remote site may be used as a "helper" when the 
        /// local site is first joining the replication group. 
        /// </summary>
        /// <remarks>
        /// Once the local site has been established as a member of the group, this setting is ignored.
        /// </remarks>
        /// <value>False</value>
        public bool BootstrapHelper { get; set; }
        /// <summary>
        /// Specifies that the site is already part of an existing group. 
        /// </summary>
        /// <remarks>
        /// This setting causes the site to be upgraded from a previous version of Berkeley DB. All sites 
        /// in the legacy group must specify this setting for themselves (the local site) and for all 
        /// other sites currently existing in the group. Once the upgrade has been completed, this setting is no longer required.
        /// </remarks>
        /// <value>False</value>
        public bool LegacyGroup { get; set; }

        /// <summary>
        /// Specifies that this site is the local site within the replication group.
        /// </summary>
        /// <remarks>
        /// The application must identify exactly one site as the local site in this way, before starting the Replication Manager.
        /// </remarks>
        /// <value>False</value>
        public bool LocalSite { get; set; }

        /// <summary>
        /// Specifies that the site may be used as a target for "client-to-client" synchronization messages. 
        /// </summary>
        /// <remarks>
        /// A peer can be either a client or a view. This setting is ignored if it is specified for the local site.
        /// </remarks>
        /// <value>False</value>
        public bool Peer { get; set; }

        /// <summary>
        /// Gets the replication site's environment ID. 
        /// </summary>
        /// <remarks>
        /// </remarks>
        public int EnvironmentId { get; private set; }

        /// <summary>
        /// Gets or sets a site's network address.
        /// </summary>
        /// <remarks>
        /// The network address can be an IP address or a DNS name, and must include a port number.
        /// </remarks>
        public ReplicationHostAddress Address { get; set; }


    }
}
