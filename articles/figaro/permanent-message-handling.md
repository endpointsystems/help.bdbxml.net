---
uid: permanent-message-handling.md
---

# Permanent Message Handling

Messages received by a replica may be marked with special flag that indicates the message is permanent. A message is marked as being permanent if the message affects transactional integrity. For example, transaction commit messages are an example of a message that is marked permanent. What the application does about the permanent message is driven by the durability guarantees required by the application.


For example, consider what the Replication Manager does when it has permanent message handling turned on and a transactional commit record is sent to the replicas. First, the replicas must transactional-commit the data modifications identified by the message. And then, upon a successful commit, the Replication Manager sends the master a message acknowledgment.


For the master (again, using the Replication Manager), things are a little more complicated than simple message acknowledgment. Usually in a replicated application, the master commits transactions asynchronously; that is, the commit operation does not block waiting for log data to be flushed to disk before returning. So when a master is managing permanent messages, it typically blocks the committing thread immediately before the commit returns. The thread then waits for acknowledgments from its replicas. If it receives enough acknowledgments, it continues to operate as normal.


If the master does not receive message acknowledgments — or, more likely, it does not receive enough acknowledgments — the committing thread flushes its log data to disk and then continues operations as normal. The master application can do this because replicas that fail to handle a message, for whatever reason, will eventually catch up to the master. So by flushing the transaction logs to disk, the master is ensuring that the data modifications have made it to stable storage in one location (its own hard drive).



## When Not To Manage Permanent Messages

>[!NOTE]
Customized permanent message handling is not an intended feature of the Figaro library, as it currently requires the Berkeley DB Base API, which is not currently implemented as a feature in Figaro.

There are two reasons why you might choose to not implement permanent messages. In part, these go to why you are using replication in the first place.


One class of applications uses replication so that the application can improve transaction throughput. Essentially, the application chooses a reduced transactional durability guarantee so as to avoid the overhead forced by the disk I/O required to flush transaction logs to disk. However, the application can then regain that durability guarantee to a certain degree by replicating the commit to some number of replicas.


Using replication to improve an application's transactional commit guarantee is called _replicating to the network_.


In extreme cases where performance is of critical importance to the application, the master might choose to both use asynchronous commits and decide not to wait for message acknowledgments. In this case the master is simply broadcasting its commit activities to its replicas without waiting for any sort of a reply. An application like this might also choose to use something other than TCP/IP for its network communications since that protocol involves a fair amount of packet acknowledgment all on its own. Of course, this sort of an application should also be very sure about the reliability of both its network and the machines that are hosting its replicas.


At the other extreme, there is a class of applications that use replication purely to improve read performance. This sort of application might choose to use synchronous commits on the master because write performance there is not of critical performance. In any case, this kind of an application might not care to know whether its replicas have received and successfully handled permanent messages because the primary storage location is assumed to be on the master, not the replicas.

## Managing Permanent Messages

With the exception of a rare breed of replicated applications, most masters need some view as to whether commits are occurring on replicas as expected. At a minimum, this is because masters will not flush their log buffers unless they have reason to expect that permanent messages have not been committed on the replicas.


That said, remember that managing permanent messages involves a fair amount of network traffic. The messages must be sent to the replicas and the replicas must acknowledge them. This represents a performance overhead that can be worsened by congested networks or outright outages.


Therefore, when managing permanent messages, you must first decide on how many of your replicas must send acknowledgments before your master decides that all is well and it can continue normal operations. When making this decision, you could decide that all replicas must send acknowledgments. But unless you have only one or two replicas, or you are replicating over a very fast and reliable network, this policy could prove very harmful to your application's performance.


Therefore, a common strategy is to wait for an acknowledgment from a simple majority of replicas. This ensures that commit activity has occurred on enough machines that you can be reliably certain that data writes are preserved across your network.


Remember that replicas that do not acknowledge a permanent message are not necessarily unable to perform the commit; it might be that network problems have simply resulted in a delay at the replica. In any case, the underlying DB replication code is written such that a replica that falls behind the master will eventually take action to catch up.


Depending on your application, it may be possible for you to code your permanent message handling such that acknowledgment must come from only one or two replicas. This is a particularly attractive strategy if you are closely managing which machines are eligible to become masters. Assuming that you have one or two machines designated to be a master in the event that the current master goes down, you may only want to receive acknowledgments from those specific machines.


Finally, beyond simple message acknowledgment, you also need to implement an acknowledgment timeout for your application. This timeout value is simply meant to ensure that your master does not hang indefinitely waiting for responses that will never come because a machine or router is down.



## Implementing Permanent Message Handling

How you implement permanent message handling depends on which API you are using to implement replication. Because Figaro applications use the Replication Manager, permanent message handling is configured using policies that you specify to the framework. For a list of these policies, see [Replication Manager Permanent Message Handling Policies](xref:replication-manager-permanent-message-handling-policies.md).



## See Also

* [Replication Manager Permanent Message Handling Policies](xref:replication-manager-permanent-message-handling-policies.md)