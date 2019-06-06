---
uid: managing-election-policies.md
---

# Managing Election Policies


Before continuing, it is worth taking a look at the startup options in the @Figaro.ReplicationManager.Start method call.
Flag | Description 
---- | -----------
`Master` | The application starts up and declares the environment to be a master without calling for an election. It is an error for more than one environment to start up using this flag, or for an environment to use this flag when a master already exists.
`Client` | The application starts up and declares the environment to be a replica without calling for an election. Note that the environment can still become a master if a subsequent application starts up, calls for an election, and this environment is elected master.
`Election` | The application starts up, looks for a master, and if one is not found calls for an election.

>[!NOTE]
No replication group should _ever_ operate with more than one master.

In the event that a environment attempts to become a master when a master already exists, the replication system will resolve the problem by holding an election. Note, however, that there is always a possibility of data loss in the face of duplicate masters, because once a master is selected, the environment that loses the election will have to roll back any transactions committed until it is in sync with the "real" master.

The `Election` flag only requires that other environments in the replication group participate in the vote. There is no requirement that all such environments participate. In other words, if an environment starts up, it can call for an election, and select a master, even if all other environments have not yet joined the replication group.

It only requires a simple majority of participating environments to elect a master. This is always true of elections held using the Replication Manager.

As always, the environment participating in the election with the most up-to-date log files is selected as master. If an environment with more recent log files has not yet joined the replication group, it may not become the master.

## Selecting the Number of Threads

Under the hood, the Replication Manager is threaded and you can control the number of threads used to process messages received from other replicas. The threads that the Replication Manager uses are:

* **Incoming message thread**. This thread receives messages from the site's socket and passes those messages to message processing threads (see below) for handling.</li><li>
* **Outgoing message thread**. Outgoing messages are sent from whatever thread performed a write to the database(s).

Note that if this write activity would cause the thread to be blocked due to some condition on the socket, the Replication Manager will hand the outgoing message to the incoming message thread, and it will then write the message to the socket. This prevents your database write threads from blocking due to abnormal network I/O conditions.

* **Message processing threads** are responsible for parsing and then responding to incoming replication messages. Typically, a response will include write activity to your database(s), so these threads can be busy performing disk I/O.

>[!IMPORTANT]
Of these threads, the only ones that you have any configuration control over are the message processing threads. In this case, you can determine how many of these threads you want to run.

It is always a bit of an art to decide on a thread count, but the short answer is you probably do not need more than three threads here, and it is likely that one will suffice. That said, the best thing to do is set your thread count to a fairly low number and then increase it if it appears that your application will benefit from the additional threads.

## See Also

* @Figaro.ReplicationManager.Start