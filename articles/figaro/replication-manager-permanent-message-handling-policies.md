---
uid: replication-manager-permanent-message-handling-policies.md
---

# Replication Manager Permanent Message Handling Policies

As described in [Permanent Message Handling](xref:permanent-message-handling.md), messages are marked permanent if they contain database modifications that should be committed at the replica. The internal replication system decides if it must flush its transaction logs to disk depending on whether it receives sufficient permanent message acknowledgments from the participating replicas. More importantly, the thread performing the transaction commit blocks until it either receives enough acknowledgments, or the acknowledgment timeout expires.

The Replication Manager is fully capable of managing permanent messages for you if your application requires it (most do). Almost all of the details of this are handled by the Replication Manager for you. However, you do have to set some policies that tell the Replication Manager how to handle permanent messages.


There are two things you have to do:
* Determine how many acknowledgments must be received by the master.
* Identify the amount of time that replicas have to send their acknowledgments.

## Permanent Message Acknowledgment Policy

You identify permanent message policies using the @Figaro.FigaroReplicationManager.AcknowledgmentPolicy property with the @Figaro.RecordAcknowledgmentPolicy enumeration.
>[!NOTE]
>You can set permanent message policies at any time during the life of the application.

The following permanent message policies are available when you use the Replication Manager:

>[!NOTE]
>The following list mentions _electable peer_ several times. This is simply another environment that can be elected to be a master (that is, it has a priority greater than 0). Do not confuse this with the concept of a peer as used for **client to client transfers**. See [Client to Client Transfer](xref:client-to-client-transfer.md) for more information on client to client transfers.

* *All*. All replicas must acknowledge the message within the timeout period. This policy should be selected only if your replication group has a small number of replicas, and those replicas are on extremely reliable networks and servers.
* *AllAvailable*. All currently connected replication clients must acknowledge the message. This policy will invoke the @Figaro.EnvironmentEvent.ReplicationAckFailure event if fewer than a quorum of clients acknowledged during that time.
* *AllPeers*. All electable peers must acknowledge the message within the timeout period. This policy should be selected only if your replication group is small, and its various environments are on extremely reliable networks and servers.
* *None*. No permanent message acknowledgments are required. If this policy is selected, permanent message handling is essentially "turned off." That is, the master will never wait for replica acknowledgments. In this case, transaction log data is either flushed or not strictly depending on the type of commit that is being performed (synchronous or asynchronous).
* *One*. At least one replica must acknowledge the permanent message within the timeout period.
* *OnePeer*. At least one electable peer must acknowledge the permanent message within the timeout period.
* *Quorum*. A quorum of electable peers must acknowledge the message within the timeout period. A quorum is reached when acknowledgments are received from the minimum number of environments needed to ensure that the record remains durable if an election is held. That is, the master wants to hear from enough electable replicas that they have committed the record so that if an election is held, the master knows the record will exist even if a new master is selected. By default, a quorum of electable peers must acknowledge a permanent message in order for it considered to have been successfully transmitted.

## Setting the Permanent Message Timeout

The permanent message timeout represents the maximum amount of time the committing thread will block waiting for message acknowledgments. If sufficient acknowledgments arrive before this timeout has expired, the thread continues operations as normal. However, if this timeout expires, the committing thread flushes its transaction log buffer before continuing with normal operations.


You set the replication timeout setting using the @Figaro.FigaroReplicationManager.SetConfiguration(Figaro.ReplicationConfig) method. When you do this, you provide the @Figaro.ReplicationConfig.AckTimeout property which will set the timeout.

>[!NOTE]
This timeout value can be set at anytime during the life of the application.

## See Also

#### Reference
* @Figaro.FigaroReplicationManager.SetConfiguration(Figaro.ReplicationConfig)
* @Figaro.ReplicationConfig.AckTimeout
* @Figaro.FigaroReplicationManager.AcknowledgmentPolicy
* @Figaro.FigaroReplicationManager.RecordAcknowledgmentPolicy
* @Figaro.EnvironmentEvent

#### Other Resources
* [Client to Client Transfer](xref:client-to-client-transfer.md)
* [Permanent Message Handling](xref:permanent-message-handling.md)
