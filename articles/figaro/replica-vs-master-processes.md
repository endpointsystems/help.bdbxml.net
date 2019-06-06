---
uid: replica-vs-master-process.md
---

# Replica vs. Master Processes

Every environment participating in a replicated application must know whether it is a master or replica. The reason for this is because, simply, the master can modify the database while replicas cannot. As a result, not only will you open databases differently depending on whether the environment is running as a master, but the environment will frequently behave quite a bit differently depending on whether it thinks it is operating as the read/write interface for your database.

Moreover, an environment must also be capable of gracefully switching between master and replica states. This means that the environment must be able to detect when it has switched states.


Not surprisingly, a large part of your application's code will be tied up in knowing which state a given environment is in and then in the logic of how to behave depending on its state.


This section shows you how to determine your environment's state, and it then shows you some sample code on how an application might behave depending on whether it is a master or a replica in a replicated application.



## Determining State

In order to determine whether your code is running as a master or a replica, you implement an event handling callback, which we initially describe in [Event Handling](xref:event-handling.md). When the current environment becomes a client — including at application startup — the @Figaro.EnvironmentEvent.ReplicationClient value is passed to the @Figaro.FigaroEnv.OnProcess event. When an election is held and a replica is elected to be a master, the @Figaro.EnvironmentEvent.ReplicationMaster value is passed to a newly elected master, and a @Figaro.EnvironmentEvent.ReplicationNewMaster is passed to the other replicas.

## See Also

[Event Handling](xref:event-handling.md)