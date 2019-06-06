---
uid: replication-manager-overview.md
---

# Replication Manager Overview

The Replication Manager exists as a layer on top of the database library. The Replication Manager is a multi-threaded implementation that allows you to easily add replication to your existing transactional application. You access and manage the Replication Manager using the @Figaro.FigaroReplicationManager object available in the @Figaro.FigaroEnv class.



## The Replication Manager

* Provides a multi-threaded communications layer using Windows threads on Microsoft Windows systems.
* **Uses TCP/IP sockets.** Network traffic is handled via threads that handle inbound and outbound messages. However, each process uses a single socket that is shared.

>[!NOTE]
>For this reason, the Replication Manager is limited to a maximum of 60 replicas (on Windows) and approximately 1000 replicas (on Unix and related systems), depending on how your system is configured.

Upon application startup, a master can be selected either manually or via elections. After startup time, however, during the course of normal operations it is possible for the replication group to need to locate a new master (due to network or other hardware related problems, for example).

## See Also
* @Figaro.FigaroReplicationManager
* @Figaro.FigaroEnv