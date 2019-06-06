---
uid: environment-administrative-infrastructure.md
---

# Environment Administrative Infrastructure

When building transactional applications, it is usually necessary to build an administrative infrastructure around the database environment. There are five components to this infrastructure, and each is supported by the Berkeley DB package in two different ways: a standalone utility and one or more library interfaces.


When writing multithreaded server applications and/or applications intended for download from the Web, it is usually simpler to create local threads that are responsible for administration of the database environment as scheduling is often simpler in a single-process model, and only a single binary need be installed and run. However, the supplied utilities can be generally useful tools even when the application is responsible for doing its own administration because applications rarely offer external interfaces to database administration.



## In This Section
* [Deadlock Detection](xref:deadlock-detection.md)
* [Checkpoints](xref:checkpoints.md)
* [Database and Log File Archival](xref:database-and-log-file-archival.md)
* [Recovery Procedures](xref:recovery-procedures.md)