---
uid: event-handling.md
---

# Event Handling

The replication system is capable of reporting a great many events to which your code might need to respond. To track and respond to these events, you implement a callback whose function it is to respond to events that happen within the DB library.


Note that this callback is usable for events beyond those required for replication purposes. In this section, however, we only discuss some of the replication-specific events.


The callback is required to determine which event has been passed to it, and then take action depending on the event.

## Replication Events

Some of the more commonly handled replication events are described below. For a complete list of events, see the @Figaro.EnvironmentEvent enumeration.

* @Figaro.EnvironmentEvent.ReplicationClient - The local environment is now a replica.
* @Figaro.EnvironmentEvent.ReplicationLocalSiteRemoved The local Replication Manager site has been removed from the group.
* The local environment is now a master (@Figaro.EnvironmentEvent.ReplicationNewMaster)
* @Figaro.EnvironmentEvent.ReplicationAckFailure - An election was held and a new environment was made a master. However, the current environment is not the master. This event exists so that you can cause your code to take some unique action in the event that the replication groups switches masters.

The Replication Manager did not receive enough acknowledgments to ensure the transaction's durability within the replication group. The Replication Manager has therefore flushed the transaction to the master's local disk for storage.

How the Replication Manager knows whether the acknowledgments it has received is determined by the acknowledgment policy you have set for your application. See Identifying Permanent Message Policies for more information.

* @Figaro.EnvironmentEvent.ReplicationSiteAdded - A new Replication Manager has joined the replication group.
* EnvironmentEvent.ReplicationSiteRemoved - An existing Replication Manager site has been removed from the replication group.
* @Figaro.EnvironmentEvent.ReplicationStartupDone - The replica has completed startup synchronization and is now processing log records received from the master.

