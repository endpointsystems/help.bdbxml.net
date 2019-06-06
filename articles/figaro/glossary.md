---
uid: glossary.md
---

# Glossary

### A


##### Abstract Syntax Tree, AST

A tree representation of the abstract (simplified) syntactic structure of source code written in a programming language.



##### Atomicity

The ability to guarantee all pieces of work in a transaction are performed or none are performed. Atomicity states that transactions follow an "all or nothing" rule. If one part of a transaction fails, the entire transaction fails.

### B

##### Blocked

When a thread cannot obtain a lock because some other thread already holds a lock on that object, the lock attempt is said to be _blocked_. See [Blocks](xref:blocks.md) for more information.

### C


##### Consistency

Consistency states that only valid data will be written to the database. In other words, the database will maintain a consistent state regardless of a transaction's success or failure.



##### Container

A unit of storage for XML nodes and documents; also referred to as a database.



### D


##### Deadlock

Occurs when two or more threads of control attempt to access conflicting resource in such a way as none of the threads can any longer may further progress.


For example, if Thread A is blocked waiting for a resource held by Thread B, while at the same time Thread B is blocked waiting for a resource held by Thread A, then neither thread can make any forward progress. In this situation, Thread A and Thread B are said to be _deadlocked_.


For more information, see [Deadlocks](xref:deadlocks.md).



##### Durability

The guarantee that once the user has been notified of success, the transaction will persist, and not be undone. This means it will survive system failure, and that the database system has checked the integrity constraints and won't need to abort the transaction. Many databases implement durability by writing all transactions into a transaction log that can be played back to recreate the system state right before a failure. A transaction can only be deemed committed after it is safely in the log.




### E


##### Environment

An encapsulation of one or more containers and any associated log and region files. Environments are used to support multi-threaded and multi-process applications by allowing different threads of control to share the in-memory cache, the locking tables, the logging subsystem, and the file namespace.




### F


##### Free-threaded, Thread-safe

Data structures and objects are free-threaded if they can be shared across threads of control without any explicit locking on the part of the application. Some books, libraries, and programming languages may use the term thread-safe for data structures or objects that have this characteristic. The two terms mean the same thing. For a description of free-threaded Figaro objects, see [Which Handles are Free-Threaded?](xref:which-handles-are-free-threaded.md).




### I


##### Isolation

The requirement that other operations cannot access or see data in an intermediate state during a transaction. This constraint is required for performance and consistency purposes between transactions in a database management system.




### L


##### Locker

An object holding a lock. In a transactional application, a transactional handle is a locker. In a non-transactional application, the locker is a cursor, a @Figaro.Container, or some document management handle within the Figaro environment.



##### Locking

When a thread of control obtains access to a shared resource, it is said to be _locking_ that resource. Note that Figaro supports both exclusive and non-exclusive locks. See [Locks](xref:locks.md) for more information.




### T


##### Thread of control

The thread in your applications performing Figaro operations.


Note that this term can also be taken to mean a separate process that is performing work — Figaro supports multi-process operations on your containers.


Also, Figaro is agnostic with regard to the type or style of threads in use in your application. So if you are using multiple threads (as opposed to multiple processes) to perform concurrent database access, you are free to use whatever thread package is best for your platform and application.



