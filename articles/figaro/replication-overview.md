---
uid: replication-overview.md
---

# Replication Overview

This document describes how to write replicated applications for Berkeley DB using the Figaro library. The APIs used to implement replication in your application are described here. This book describes the concepts surrounding replication, the scenarios under which you might choose to use it, and the architectural requirements that a replication application has over a transactional application.


This documentation is aimed at the software engineer responsible for writing a replicated database application.

This documentation assumes that you have already read and understood the concepts contained in the [Getting Started with Concurrent and Transactional Processing](xref:getting-started-with-concurrent-and-transactional-processing.md) guide.

This documentation provides a thorough introduction and discussion on replication as used with the Figaro library. It begins by offering a general overview to replication and the benefits it provides. It also describes the APIs that you use to implement replication, and it describes architecturally the things that you need to do to your application code in order to use the replication APIs. Finally, it discusses the differences in backup and restore strategies that you might pursue when using replication, especially where it comes to log file removal.


## See Also

[Getting Started with Concurrent and Transactional Processing](xref:getting-started-with-concurrent-and-transactional-processing.md)