---
uid: the-replication-manager.md
---

# The Replication Manager

The easiest way to add replication to your transactional application is to use the Replication Manager via the @Figaro.FigaroReplicationManager object. The Replication Manager provides a comprehensive communications layer that enables replication. For a brief listing of the Replication Manager's feature set, see [Replication Manager Overview](xref:replication-manager-overview.md).


To use the Replication Manager, you make use of a combination of the @Figaro.DBSite class and related methods, plus special methods off the @Figaro.FigaroEnv class. That is:

* Create and configure a @Figaro.FigaroEnv environment handle with the desired settings.
* Use the @Figaro.FigaroReplicationManager instance inside the @Figaro.FigaroEnv handle to configure replication properties. Configuring the Replication Manager entails setting the replication environment's priority, setting the TCP/IP address that this replication environment will use for incoming replication messages, identifying TCP/IP addresses of other replication environments, and so forth.
* Open the environment handle (using @Figaro.FigaroEnv.Open(System.String,Figaro.EnvOpenOptions)) using the appropriate combination with the @Figaro.EnvOpenOptions.Replication and @Figaro.EnvOpenOptions.Thread flag set or simply by using the @Figaro.EnvOpenOptions.ReplicationDefaults combination flag. The @Figaro.EnvOpenOptions.Replication and @Figaro.EnvOpenOptions.Thread flags enable the Replication Manager and ensure the environment handle is free-threaded (thread safe). **Both of these flags are required for Replication Manager usage**.
* Start replication by calling @Figaro.FigaroReplicationManager.Start.
* Open your databases as needed. Masters must open their databases for read and write activity. Replicas can open their databases for read-only activity, but doing so means they must re-open the databases if the replica ever becomes a master. Either way, replicas should never attempt to write to the database(s) directly.

When you are ready to shut down your application:

* Close/Dispose any @Figaro.DBSite objects that are open.
* Close/Dispose any @Figaro.XmlManager or other references.
* Dispose of the @Figaro.FigaroEnv object last.

>[!WARNING]
>It is imperative that the @Figaro.FigaroEnv object be disposed last in your Figaro applications. Failure to ensure this ordering will result in C++ exceptions in your application.

## See Also

* @Figaro.FigaroEnv@Figaro.XmlManager
* @Figaro.FigaroReplicationManager
* @Figaro.FigaroReplicationManager.Start

#### Other Resources
* [Replication Manager Overview](xref:replication-manager-overview.md)
* [XmlManager Instantiation and Destruction](xref:xmlmanager-instantiation-and-destruction.md)
