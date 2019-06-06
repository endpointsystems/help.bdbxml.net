---
uid: introduction-to-replication.md
---

# Introduction to Replication


## Overview

The DB replication APIs allow you to distribute your database write operations (performed on a read-write master) to one or more read-only replicas. For this reason, DB's replication implementation is said to be a _single master, multiple replica_ replication strategy.


Note that your database write operations can occur only on the master; any attempt to write to a replica results in an error being raised by the DB API used to perform the write.


A single replication master and all of its replicas are referred to as a _replication group_. While all members of the replication group can reside on the same machine, usually each replication participant is placed on a separate physical machine somewhere on the network.


All replication applications must first be transactional applications. The data that the master transmits to its replicas are log records that are generated as records are updated. Upon transactional commit, the master transmits a transaction record which tells the replicas to commit the records they previously received from the master. In order for all of this to work, your replicated application must also be a transactional application. For this reason, it is recommended that you write and debug your DB application as a stand-alone transactional application before introducing the replication layer to your code.



## Replication Environments

The most important requirement for a replication participant is that it must use a unique @Figaro.FigaroEnv environment independent of all other replication participants. So while multiple replication participants can reside on the same physical machine, no two such participants can share the same environment home directory.


For this reason, technically replication occurs between _unique database environments_. So in the strictest sense, a replication group consists of a master environment and one or more replica environments. However, the reality is that for production code, each such environment will usually be located on its own unique machine. Consequently, the documentation occasionally talks about replication sites, meaning the unique combination of environment home directory, host and port that a specific replication application is using.


There is no specified limit to the number of environments which can participate in a replication group. The only limitation here is one of resources — network bandwidth, for example. The Replication Manager, however, does place a limit on the number of environments you can use. See [Replication Manager Overview](xref:replication-manager-overview.md) for more details.


Also, DB's replication implementation requires all participating environments to be assigned IDs that are locally unique to the given environment. Depending on the replication APIs that you choose to use, you may or may not need to manage this particular detail.

## Replication Databases

DB's databases are managed and used in exactly the same way as if you were writing a non-replicated application, with a couple of caveats. First, the databases maintained in a replicated environment must reside in the directory identified by the @Figaro.FigaroEnv.AddDataDirectory(System.String) method. Unlike non-replication applications, you cannot place your databases in a subdirectory below these locations. You should also not use full path names for your databases or environments as these are likely to break when they are replicated to other machines.

## Selecting a Master

Every replication group is allowed one and only one master environment. Usually masters are selected by holding an election, although it is possible to turn elections off and manually select masters (this is not recommended for most replicated applications).


When elections are being used, they are performed by the underlying replication code so you have to do very little to implement them.


When holding an election, replicas "vote" on who should be the master. Among replicas participating in the election, the one with the most up-to-date set of log records will win the election. Note that it's possible for there to be a tie. When this occurs, priorities are used to select the master. See @replication-elections. for details.


For more information on holding and managing elections, see @replication-elections.md .



## See Also

* @Figaro.FigaroEnv.AddDataDirectory(System.String)

#### Other Resources
* @replication-elections.md
* [Replication Manager Overview](xref:replication-manager-overview.md)