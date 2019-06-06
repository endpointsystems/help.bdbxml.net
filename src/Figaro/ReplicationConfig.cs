using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Figaro
{
    /// <summary>
    /// Replication configuration settings for the <seealso cref="FigaroReplicationManager"/>.
    /// </summary>
    public class ReplicationConfig
    {
        /// <summary>
        /// The list of sites, including the local site, that the replication manager communicates with.
        /// </summary>
        public List<DBSite> Sites { get; set; }

        /// <summary>
        /// If true, the <see cref="FigaroReplicationManager"/> will immediately start after being configured.
        /// </summary>
        /// <remarks>
        /// This is an 'external signal' - it will not be automatically started by the <seealso cref="FigaroReplicationManager"/> 
        /// because starting the replication manager requires the environment be opened first.
        /// If you configure this to true, make sure you also set the <seealso cref="StartOption"/> property to the desired setting.
        /// </remarks>
        public bool StartReplication { get; set; }

        /// <summary>
        /// If the StartReplication property is set to true, automatically start the <see cref="FigaroReplicationManager"/> 
        /// with this startup setting.
        /// </summary>
        /// <seealso cref="FigaroReplicationManager.Start"/>
        public ReplicationStartOptions StartOption { get; set; }
        /// <summary>
        /// Gets or sets the number of threads to use within the <seealso cref="FigaroReplicationManager"/>.
        /// </summary>
        public UInt16 StartThreads { get; set; }

        /// <summary>
        /// IF true, the replication master sends groups of records to the clients in a single network transfer.
        /// </summary>
        public bool BulkTransfer { get; set; }

        /// <summary>
        /// If true, the client delays synchronizing to a newly declared
        /// master (defaults to false). Clients configured in this way
        /// remain unsynchronized until the application explicitly synchronizes.
        /// </summary>
        /// <seealso cref="FigaroReplicationManager.Sync"/>
        public bool DelayClientSync { get; set; }
        /// <summary>
        /// If true, replication only stores the internal information in-memory
        /// and cannot keep persistent state across a site crash or reboot. By
        /// default, it is false and replication creates files in the
        /// environment home directory to preserve the internal information. 
        /// This configuration flag can only be set before the
        /// <see cref="FigaroEnv"/> is opened.
        /// </summary>
        /// <remarks>
        /// By default, replication creates files in the environment home directory to preserve some 
        /// internal information. If this configuration flag is turned on, replication only stores this 
        /// internal information in-memory and cannot keep persistent state across a site crash or reboot. 
        /// This results in the following limitations:
        /// <list type="bullet">
        /// <item>
        /// A master site should not reappoint itself master immediately after crashing or rebooting because 
        /// the application would incur a slightly higher risk of client crashes. The former master site should 
        /// rejoin the replication group as a client. The application should either hold an election or 
        /// appoint a different site to be the next master.
        /// </item>
        /// <item>
        /// An application has a slightly higher risk that elections will fail or be unable to complete. 
        /// Calling additional elections should eventually yield a winner.
        /// </item>
        /// <item>
        /// An application has a slight risk that the wrong site may win an election, resulting in the loss of 
        /// some data. This is consistent with the general loss of data durability when running in-memory.
        /// </item>
        /// <item>
        /// <see cref="FigaroReplicationManager"/> applications do not maintain group membership information persistently on-disk. 
        /// </item>
        /// </list>
        /// <para>All sites in the replication group should have the same value for this configuration flag.</para>
        /// </remarks>
        /// <value>False</value>
        public bool InMemory { get; set; }

        /// <summary>
        /// If true, master leases are used for this site (defaults to
        /// false). 
        /// </summary>
        /// <remarks>
        /// Configuring this option may result in a 
        /// <see cref="LeaseExpiredException"/> when attempting to read entries
        /// from a database after the site's master lease has expired.
        /// <para>All sites in the replication group should have the same value for this configuration flag.</para>
        /// </remarks>
        /// <value>False</value>
        public bool UseMasterLeases { get; set; }
        /// <summary>
        /// If true, the replication master automatically re-initializes
        /// outdated clients (defaults to true). 
        /// </summary>
        /// <value>True</value>
        public bool AutoInit { get; set; }

        /// <summary>
        /// If true, method calls that would normally block while
        /// clients are in recovery will return errors immediately (defaults to
        /// false).
        /// </summary>
        /// <remarks>See <a href="/managing-blocking-operations.htm">Managing Blocking Operations</a> for more information.</remarks>
        /// <value>False</value>        
        public bool NoBlocking { get; set; }

        /// <summary>
        /// If true, the <see cref="FigaroReplicationManager"/> observes the strict "majority"
        /// rule in managing elections, even in a group with only 2 sites. 
        /// </summary>
        /// <remarks>
        /// This means the client in a 2-site group is unable to take over as
        /// master if the original master fails or becomes disconnected. 
        /// <para>
        /// Both sites in the replication group should have the
        /// same value for this parameter.
        /// </para>
        /// </remarks>
        /// <value>False</value>
        public bool StrictTwoSite { get; set; }

        /// <summary>
        /// If true, <see cref="FigaroReplicationManager"/> automatically runs elections to
        /// choose a new master when the old master appears to
        /// have become disconnected (defaults to true).
        /// </summary>
        public bool Elections { get; set; }

        /// <summary>
        /// If true, runs in a two-site replication group as the master; if false, runs as the client.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This flag is used to specify the preferred master site in a
        /// replication group operating in preferred master mode. A preferred
        /// master replication group must contain only two sites, with one site
        /// specified as the preferred master site and the other site specified
        /// as the client site. The preferred master site operates as the
        /// master site whenever possible.
        /// </para>
        /// <para>
        ///		One of the benefits of replication is that it helps your application remain 
        ///		available for writes even when a site crashes.Another benefit is the added 
        ///		durability achieved by storing multiple copies of your application data 
        ///		at different sites.However, if your replication group contains only 
        ///		two sites, you must prioritize which of these benefits is more important to your application.
        /// </para>
        /// <para>
        ///		A two-site replication group is particularly vulnerable to duplicate masters if there 
        ///		is a loss of communication between the sites.The original master continues to accept 
        ///		new transactions. If the original client detects the loss of the master and elects 
        ///		itself master, it also starts accepting new transactions. When communications are 
        ///		restored, there are duplicate masters and one site's new transactions will be rolled back.
        /// </para>
        /// <para>
        ///		If it is unacceptable to your application for any new transactions to be rolled back, 
        ///		the alternative in a two - site replication group is to require both sites to be present 
        ///		in order to elect a master.This stops a client from electing itself master when it loses 
        ///		contact with the master and prevents creation of parallel sets of transactions, one of 
        ///		which must be rolled back.
        ///	</para>
        /// <para>
        ///		However, requiring both sites to be present to elect a master results in a loss of write 
        ///		availability when the master crashes.The client cannot take over as master and the 
        ///		replication group exists in a read - only state until the original master site rejoins the replication group.
        /// </para>
        /// <para>
        ///		A two-site HA application uses this property to make this trade-off
        ///		between write availability and transaction durability. When this value is true, the 
        ///		<see cref="FigaroReplicationManager"/> favors transaction durability. When this value is false, 
        ///		<see cref="FigaroReplicationManager"/> favors write availability. 
        /// </para>
        /// <para>
        ///		A two-site HA Figaro application that uses heartbeats in an environment with frequent communications 
        ///		disruptions generally should operate with this value set to True. Otherwise, frequent heartbeat 
        ///		failures will cause frequent duplicate masters and the resulting elections and client synchronizations 
        ///		will make one or both sites unavailable for extended periods of time.
        /// </para>
        /// </remarks>
        public bool TwoSitePreferredMaster { get; set; }

        /// <summary>
        /// Gets or sets whether the site is a replication view, and if so 
        /// whether it is a partial or a full view.
        /// </summary>
        /// <value><c>ReplicationViewOptions.None</c></value>
        public ReplicationViewOptions ReplicationView { get; set; }

        /// <summary>
        /// The amount of time (in microseconds) the replication manager's transport function
        /// waits to collect enough acknowledgments from replication group
        /// clients, before giving up and returning a failure indication. The
        /// default wait time is 1 second.
        /// </summary>
        public UInt32 AckTimeout { get; set; }

        /// <summary>
        /// The amount of time (in microseconds) a master site delays between completing a
        /// checkpoint and writing a checkpoint record into the log.
        /// </summary>
        /// <remarks>
        /// This delay allows clients to complete their own checkpoints before
        /// the master requires completion of them. The default is 30 seconds.
        /// If all databases in the environment, and the environment's
        /// transaction log, are configured to reside in-memory (never preserved
        /// to disk), then, although checkpoints are still necessary, the delay
        /// is not useful and should be set to 0.
        /// </remarks>
        public UInt32 CheckpointDelay { get; set; }

        /// <summary>
        /// The amount of time (in microseconds) the replication manager waits before trying
        /// to re-establish a connection to another site after a communication
        /// failure. The default wait time is 30 seconds.
        /// </summary>
        public UInt32 ConnectionRetry { get; set; }

        /// <summary>
        /// The timeout period (in microseconds) for an election. The default timeout is 2
        /// seconds.
        /// </summary>
        public UInt32 ElectionTimeout { get; set; }

        /// <summary>
        /// Configure the amount of time (in microseconds) the replication manager waits
        /// before retrying a failed election. The default wait time is 10
        /// seconds. 
        /// </summary>
        public UInt32 ElectionRetry { get; set; }

        /// <summary>
        /// An optional configuration timeout period (in microseconds) to wait for full election
        /// participation the first time the replication group finds a master.
        /// By default this option is turned off and normal election timeouts
        /// are used.
        /// </summary>
        public UInt32 FullElectionTimeout { get; set; }

        /// <summary>
        /// The amount of time (in microseconds) the replication manager, running at a client
        /// site, waits for some message activity on the connection from the
        /// master (heartbeats or other messages) before concluding that the
        /// connection has been lost. When 0 (the default), no monitoring is
        /// performed.
        /// </summary>
        public UInt32 HeartbeatMonitor { get; set; }

        /// <summary>
        /// The frequency (in microseconds) at which the replication manager, running at a master
        /// site, broadcasts a heartbeat message in an otherwise idle system.
        /// When 0 (the default), no heartbeat messages are sent. 
        /// </summary>
        /// <value>0</value>
        public UInt32 HeartbeatSend { get; set; }

        /// <summary>
        /// The amount of time (in microseconds) a client grants its master lease to a master.
        /// When using master leases all sites in a replication group must use
        /// the same lease timeout value. There is no default value. If leases
        /// are desired, this method must be called prior to calling
        /// <see cref="FigaroEnv.FigaroReplicationManager.Start"/>.
        /// </summary>
        public UInt32 LeaseTimeout { get; set; }

        /// <summary>
        /// The value, relative to <see cref="ClockSkewSlow"/>, of the fastest
        /// clock in the group of sites.
        /// </summary>
        public UInt32 ClockSkewFast { get; private set; }

        /// <summary>
        /// The value of the slowest clock in the group of sites.
        /// </summary>
        public UInt32 ClockSkewSlow { get; private set; }

        /// <summary>
        /// Set the clock skew ratio among replication group members based on
        /// the fastest and slowest measurements among the group for use with
        /// master leases.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Calling this method is optional, the default values for clock skew
        /// assume no skew. The user must also configure leases via
        /// <see cref="UseMasterLeases"/>. Additionally, the user must also
        /// set the master lease timeout via <see cref="LeaseTimeout"/> and
        /// the number of sites in the replication group via
        /// <see cref="Sites"/>. These settings may be configured in any
        /// order. For a description of the clock skew values, see Clock skew 
        /// in the Figaro Programmer's Reference Guide. For a description
        /// of master leases, see Master leases in the Figaro Programmer's
        /// Reference Guide.
        /// </para>
        /// <para>
        /// These arguments can be used to express either raw measurements of a
        /// clock timing experiment or a percentage across machines. For
        /// instance a group of sites have a 2% variance, then
        /// <paramref name="fast"/> should be set to 102, and
        /// <paramref name="slow"/> should be set to 100. Or, for a 0.03%
        /// difference, you can use 10003 and 10000 respectively.
        /// </para>
        /// </remarks>
        /// <param name="fast">
        /// The value, relative to <paramref name="slow"/>, of the fastest clock
        /// in the group of sites.
        /// </param>
        /// <param name="slow">
        /// The value of the slowest clock in the group of sites.
        /// </param>
        public void ClockSkew(UInt32 fast, UInt32 slow)
        { }

        /// <summary>
        /// The database environment's priority in replication group elections.
        /// A special value of 0 indicates that this environment cannot be a
        /// replication group master. If not configured, then a default value
        /// of 100 is used.
        /// </summary>
        public UInt32 Priority { get; set; }
        /// <summary>
		/// The minimum number of microseconds a client waits before requesting
		/// retransmission.
        /// </summary>
        /// <value>40000</value>
        public UInt32 RetransmissionRequestMin { get; private set; }

        /// <summary>
		/// The maximum number of microseconds a client waits before requesting
		/// retransmission.
        /// </summary>
        /// <value>1280000</value>
        public UInt32 RetransmissionRequestMax { get; private set; }

        /// <summary>
        /// Set a threshold for the minimum and maximum time that a client waits
        /// before requesting retransmission of a missing message.
        /// </summary>
        /// <remarks>
        /// <para>
        /// If the client detects a gap in the sequence of incoming log records
        /// or database pages, Figaro waits for at least
        /// <paramref name="min"/> microseconds before requesting retransmission
        /// of the missing record. Figaro doubles that amount before
        /// requesting the same missing record again, and so on, up to a
        /// maximum threshold of <paramref name="max"/> microseconds.
        /// </para>
        /// <para>
        /// These values are thresholds only. Replication Manager applications 
        /// use these values to determine when to automatically request 
        /// retransmission of missing messages. For Base API applications, 
        /// Figaro has no thread available in the library as a timer, the 
        /// threshold is only checked when a thread enters the Figaro
        /// library to process an incoming replication message. Any amount of
        /// time may have passed since the last message arrived and Figaro
        /// only checks whether the amount of time since a request was made is 
        /// beyond the threshold value or not.
        /// </para>
        /// <para>
        /// By default the minimum is 40000 and the maximum is 1280000 (1.28
        /// seconds). These defaults are fairly arbitrary and the application
        /// likely needs to adjust these. The values should be based on expected
        /// load and performance characteristics of the master and client host
        /// platforms and transport infrastructure as well as round-trip message
        /// time.
        /// </para></remarks>
        /// <param name="min">
        /// The minimum number of microseconds a client waits before requesting
        /// retransmission.
        /// </param>
        /// <param name="max">
        /// The maximum number of microseconds a client waits before requesting
        /// retransmission.
        /// </param>
        public void RetransmissionRequest(UInt32 min, UInt32 max)
        { }

        /// <summary>
        /// The gigabytes component of the maximum amount of dynamic memory
        /// used by the Replication Manager incoming queue.
        /// </summary>
        public UInt32 IncomingQueueMaxGBytes { get; private set; }

        /// <summary>
        /// The bytes component of the maximum amount of dynamic memory
        /// used by the Replication Manager incoming queue.
        /// </summary>
        public UInt32 IncomingQueueMaxBytes { get; private set; }

        /// <summary>
        /// Set a byte-count limit on the maximum amount of dynamic memory
        /// used by the Replication Manager incoming queue.
        /// </summary>
        /// <remarks>
        /// <para>
        /// By default, the Replication Manager incoming queue size has a
        /// limit of 100MB.
        /// </para>
        /// <para>
        /// If both <paramref name="gBytes"/> and <paramref name="bytes"/> are
        /// zero, then the Replication Manager incoming queue size is limited
        /// by available heap memory.
        /// </para>
        /// </remarks>
        /// <param name="gBytes">
        /// The number of gigabytes which, when added to
        /// <paramref name="bytes"/>, specifies the maximum amount of dynamic
        /// memory used by the Replication Manager incoming queue.
        /// </param>
        /// <param name="bytes">
        /// The number of bytes which, when added to 
        /// <paramref name="gBytes"/>, specifies the maximum amount of dynamic
        /// memory used by the Replication Manager incoming queue.
        /// </param>
        public void IncomingQueueMax(UInt32 gBytes, UInt32 bytes)
        { }

        /// <summary>
        /// Specify how master and client sites handle the acknowledgment of
        /// replication messages which is necessary for "permanent" records.
        /// The current implementation requires all sites in a replication group
        /// to configure the same acknowledgment policy. 
        /// </summary>
        /// <seealso cref="AckTimeout"/>
        public RecordAcknowledgmentPolicy RecordAckPolicy { get; set; }


        /*Base API*/
        ///// <summary>
        ///// The gigabytes component of the byte-count limit on the amount of
        ///// data that is transmitted from a site in response to a single
        ///// message processed by <see cref=""/>
        ///// <see cref="DatabaseEnvironment.RepProcessMessage"/>.
        ///// </summary>
        //public UInt32 TransmitLimitGBytes { get; private set; }

        //public UInt32 TransmitLimitBytes { get; private set; }

        //public void TransmitLimit(UInt32 gBytes, UInt32 bytes) { }
    }
}
