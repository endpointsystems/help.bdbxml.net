---
uid: database-features.md
---

# Database Features

Beyond XML-specific features, Figaro leverages many features from Oracle Berkeley DB, which allows Figaro to provide the same fast, reliable, and scalable database support as does Berkeley DB. The result is that Figaro is an ideal candidate for mission-critical applications that must manage XML data.

## In-Process Data Access

Figaro is compiled and linked in the same way as any library. It runs in the same process space as your application. The result is database support in a small footprint without the IPC-overhead required by traditional client/server-based database implementations.

## Size Limitations

Berkeley DB XML can manage databases up to 256 terabytes in size.

## Database Environment Support

Figaro database environments support all of the same features as Berkeley DB environments, including multiple databases, transactions, deadlock detection, lock and page control, and encryption. In particular, this means that Figaro databases can share an environment with Berkeley DB databases, thus allowing an application to gracefully use both. See [Berkeley DB Environments](xref:berkeley-db-environments.md) for an introduction to environments.


## Atomic Operations

Complex sequences of read and write access can be grouped together into a single atomic operation using Figaro's transaction support. Either all of the read and write operations within a transaction succeed, or none of them succeed. See [Why Transactions?](xref:why-transactions.md) for more information.


## Isolated Operations

Operations performed inside a transaction see all XML documents as if no other transactions are currently operating on them. See [Isolation](xref:isolation.md) for more information.

## Recoverability

Figaro's transaction support ensures that all committed data is available no matter how the application or system might subsequently fail. See [Recovery Procedures](xref:recovery-procedures.md) for more details.

## Concurrent Access

Through the combined use of isolation mechanisms built into Figaro, plus deadlock handling supplied by the application, multiple threads and processes can concurrently access the XML data set in a safe manner. See [Concurrency](xref:concurrency.md) for more information.


## Replication

Figaro provides the ability to distribute updates made to a master database to multiple replica databases. This provides the application with the ability to support fail-over for High Availability applications, as well as scalability for load balancing of queries across multiple systems.

#### Other Resources
* [Berkeley DB Environments](xref:berkeley-db-environments.md)
* [Why Transactions?](xref:why-transactions.md)
* [Isolation](xref:isolation.md)
* [Recovery Procedures](xref:recovery-procedures.md)
* [Concurrency](xref:concurrency.md)