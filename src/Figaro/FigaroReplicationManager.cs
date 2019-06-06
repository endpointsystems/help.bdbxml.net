using System;
using System.Collections.Generic;

namespace Figaro
{
    /// <summary>
    /// The replication manager for Figaro High Availability (HA).
    /// </summary>
    /// <remarks>
    /// </remarks>
    public class FigaroReplicationManager
    {
        /// <summary>
        /// Gets replication statistics for the database environment.
        /// </summary>
        /// <param name="clear">If <c>true</c>, resets the statistics within the environment.</param>
        /// <returns>A new <c>ReplicationStatistics</c> data set.</returns>
        public ReplicationManagerStatistics GetStatistics(bool clear)
        {
            return new ReplicationManagerStatistics();
        }

        /// <summary>
        /// Prints replication statistics to the standard output.
        /// </summary>
        /// <param name="clear">If <c>true</c>, resets the statistics within the environment.</param>
        public void PrintStatistics(bool clear)
        {
        }

        /// <summary>
        /// Create a replication view and specify the function to determine
        /// whether a database file is replicated to the local site.
        /// </summary>
        /// <remarks>
        /// If it is null, the replication view is a full view and all database
        /// files are replicated to the local site. Otherwise it is a partial
        /// view and only some database files are replicated to the local site.
        /// </remarks>
        public EventHandler<ReplicationViewArgs> ReplicationView;

        /// <summary>
        /// Configure the replication manager.
        /// </summary>
        /// <param name="config">The replication configuration properties.</param>
        public void SetConfiguration(ReplicationConfig config)
        {

        }

        /// <summary>
        /// Configure the site as a replication view using the specified option.
        /// </summary>
        /// <param name="viewOption">The specified replication view option.</param>
        public void SetReplicationView(ReplicationViewOptions viewOption)
        {

        }

        /// <summary>
        /// Retrieve the replication configuration settings from the replication manager.
        /// </summary>
        /// <returns>The current replication configuration settings.</returns>
        public ReplicationConfig GetConfiguration()
        {
            return new ReplicationConfig();
        }

        /// <summary>
        /// Forces synchronization to begin between master and client.
        /// </summary>
        /// <remarks>
        /// If an application has configured delayed master synchronization, the application must synchronize 
        /// explicitly (otherwise the client will remain out-of-date and will ignore all database changes 
        /// forwarded from the replication group master).
        /// <para>
        /// This method may also be called after the client application learns a new master has 
        /// been established via the <see cref="EnvironmentEvent.ReplicationNewMaster"/> flag.
        /// </para>
        /// </remarks>
        /// <see cref="FigaroEnv.OnProcess"/>
        public void Sync()
        { }

        /// <summary>
        /// Starts the replication manager.
        /// </summary>
        /// <param name="threads">The number of threads of control created and dedicated to processing replication messages.</param>
        /// <param name="options">The replication manager start options.</param>
        /// <remarks>
        /// This method can only be called after <see cref="FigaroEnv.Open"/> has been called.
        /// </remarks>
        /// <seealso cref="ReplicationStartOptions"/>
        /// <seealso cref="FigaroEnv.Open"/>
        /// <exception cref="FigaroReplicationException"></exception>
        public void Start(UInt16 threads, ReplicationStartOptions options)
        { }

        /// <summary>
        /// Sets the database environment's priority in replication group elections. A special value of 0 indicates that 
        /// this environment cannot be a replication group master.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Each database environment included in a replication group must have a priority, which specifies a relative ordering 
        /// among the different environments in a replication group. This ordering is a factor in determining which environment 
        /// will be selected as a new master in case the existing master fails. Applications should specify environment priorities.
        /// </para>
        /// <para>
        /// Priorities are an unsigned integer, but do not need to be unique throughout the replication group. A priority of 0 
        /// means the system can never become a master. Otherwise, larger valued priorities indicate a more desirable master. 
        /// For example, if a replication group consists of three database environments, two of which are connected by an OC3 
        /// and the third of which is connected by a T1, the third database environment should be assigned a priority value which 
        /// is lower than either of the other two.</para>
        /// <para>
        /// Desirability of the master is first determined by the client having the most recent log records. Ties in log records are 
        /// broken with the client priority. If both sites have the same log and the same priority, one is selected at random.
        /// </para>
        /// </remarks>
        /// <param name="priority">The environment's priority.</param>
        public void SetPriority(UInt32 priority)
        { }

        /// <summary>
        /// Gets a list of sites configured to interact with the replication manager.
        /// </summary>
        /// <returns>The configured list of sites.</returns>
        public List<DBSite> GetDBSites()
        {
            return new List<DBSite>();
        }

        /// <summary>
        /// List a read-only list of site addresses and ports.
        /// </summary>
        /// <returns>A read-only list of addresses and ports.</returns>
        public IReadOnlyCollection<ReplicationHostAddress> ListSites()
        {
            return new List<ReplicationHostAddress>();
        }

        /// <summary>
        /// Add a replication site to the replication manager.
        /// </summary>
        /// <param name="replicationSite">The site to add.</param>
        /// <see cref="DBSite"/>
        public void AddSite(DBSite replicationSite)
        { }

        /// <summary>
        /// Specify the policy for how master and client sites will handle acknowledgment of replication messages 
        /// which are necessary for "permanent" records. 
        /// </summary>
        /// <remarks>
        /// View sites never send these acknowledgments and are not counted by any acknowledgment policy. The current 
        /// implementation requires all sites in a replication group to configure the same acknowledgment policy.
        /// <para>
        /// Waiting for client acknowledgments is always limited by the <see cref="ReplicationConfig.ElectionTimeout"/>. If 
        /// an insufficient number of client acknowledgments have been received, then the master will invoke the 
        /// event callback function, if set, with the <see cref="EnvironmentEvent.ReplicationAckFailure"/> value.
        /// </para>
        /// </remarks>
        /// <param name="policy"></param>
        public void SetAcknowledgmentPolicy(RecordAcknowledgmentPolicy policy)
        { }

        /// <summary>
        /// Gets the total throttling size, in megabytes.
        /// </summary>
        /// 
        //public Double Throttling { get; private set; }
        /// <summary>
        /// Gets the replication manager priority.
        /// </summary>
        /// <seealso cref="SetPriority"/>
        public uint Priority { get; private set; }

        /// <summary>
        /// Gets the acknowledgment policy of the replication manager.
        /// </summary>
        /// <seealso cref="RecordAcknowledgmentPolicy"/>
        public RecordAcknowledgmentPolicy AcknowledgmentPolicy { get; private set; }
    }
}
