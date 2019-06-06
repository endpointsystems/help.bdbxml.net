---
uid: designing-your-application-for-recovery.md
---

# Designing Your Application for Recovery

When building your Figaro application, you should consider how you will run recovery. If you are building a single threaded, single process application, it is fairly simple to run recovery when your application first opens its environment. In this case, you need only decide if you want to run recovery every time you open your application (recommended) or only some of the time, presumably triggered by a start up option controlled by your application's user.


However, for multi-threaded and multi-process applications, you need to carefully consider how you will design your application's startup code so as to run recovery only when it makes sense to do so.

## In This Section

* @recovery-for-multi-threaded-applications.md
* @recovery-for-multi-process-applications.md
