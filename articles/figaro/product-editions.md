---
uid: product-editions.md
---
# Product Editions

Oracle Berkeley DB XML comes in four distinct product editions - Data Store (DS), for single-threaded applications; Concurrent Data Store (CDS), for multi-threaded read, single-threaded write operations; Transactional Data Storage (TDS) for multi-threaded, ATOMIC operations; and High Availability (HA), for replication and multiple instance master-slave database application data storage requirements.



## Data Store (DS)

Figaro Data Store, or DS, covers the basics of data storage. The API is simple and uses direct key or cursor-based access under the hood; this minimizes overhead when adding, removing, and updating XML data. Frequently accessed data is cached in memory. DS is very fast; it avoids storing data to disk until it is necessary. It performs no locking or other concurrency control, and it does not provide the ACID features of transactional systems. DS is simple and efficient data storage for non-concurrent applications that favor raw throughput over guaranteed data integrity.


Choose Figaro DS when:

* There is only one reader or writer, or multiple read-only processes.
* Transactional data protection (ACID) is not required.

## Concurrent Data Store (CDS)

Figaro Concurrent Data Store, or CDS, adds locking and concurrency management services to DS. With the CDS features enabled, Figaro CDS uses locks to manage multiple readers, and a single writer orchestrating their concurrent access to data within the databases. Figaro CDS is simple and efficient data storage for concurrent systems. ACID transactions are not supported.


Choose Figaro CDS when:

* There are many concurrent readers and writers to the database.
* Reads are much more common than writes.
* Transactional data protection is not required.

## Transactional Data Store (TDS)

Figaro Transactional Data Store, or TDS, adds full ACID transaction support to Figaro CDS. Changes to databases or their contents are atomic, consistent, isolated, and durable when using TDS. Transactions can be short lived or long running, lasting days if necessary. Transactions can improve system throughput in concurrent applications by grouping actions into a single commit to disk. Applications that use secondary indices to manage relationships require transactions to keep those relationships consistent. The Figaro TDS transactional system in the Berkeley DB database engine offers a great deal of flexibility to accommodate your desired performance and durability requirements. For example, Figaro TDS supports the ability to trade off durability for speed, or to allow readers to see uncommitted data to reduce locking overhead. Figaro TDS utilizes log files to contain information about transactional data that can be used to recover from application or systems failure.


Choose Figaro TDS when:

* There is a balance of concurrent readers and writers.
* Data integrity is critical.
* Recovery of committed data is a requirement.
* Database corruption is unacceptable.

## High Availability (HA)

Figaro High Availability, or HA, adds support for replicated systems to Figaro TDS. There are the two basic reasons to build a replicated highly available system: scale and reliability:

* Scale your transactional application beyond the processing constraints of a single system.
* Deliver a highly reliable system where failure of one node does not cause system wide failure.

Replicated databases come in a number of different configurations. The HA layer of the Berkeley DB database engine can be configured for any of the replicated designs that involve a single writer as a master and multiple replicas allowing for read-only access to data.


Figaro HA supports the entire range of configurations from a simple hot standby with failover setup to a geographically diverse set of tiered replica clusters. Figaro HA makes no assumptions about how the nodes communicate with one another; it will support TCP/IP communications networks.


Figaro HA supports any topology regardless of the number of replicated nodes within it. Upon notification of a failure of the master node, an election algorithm will identify a new master so that processing can continue uninterrupted. Figaro HA can even be used as a completely disk-less in memory reliable distributed storage system.


Choose Figaro HA when:

* Single-system throughput isn't enough to meet your data requirements.
* System uptime of %99.999 or better is a requirement.
* The system must be able to survive node failure.
* There is a need for consistent online backup of system data.
* Your architecture calls for a hot-failover strategy.
* Your hardware device has multiple redundant systems and must be able to fail from one to the other gracefully.

## See Also


#### Other Resources
* [Developing .NET Applications with the Figaro XML Database](xref:developing-dotnet-apps-with-figaro.md)
* [Getting Started with Concurrent and Transactional Processing](xref:getting-started-with-concurrent-and-transactional-processing.md)
* [Replication Overview](replication-overview.md)