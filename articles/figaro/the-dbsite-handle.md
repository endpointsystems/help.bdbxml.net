---
uid: the-dbsite-handle.md
---

# The DBSite Handle

The @Figaro.DBSite handle is used to configure important attributes about a site such as its host name and port number, and whether it is the local site. It is also used to indicate whether a site is a group creator, which is important when you are starting the very first site in a replication group for the very first time.


The @Figaro.DBSite handle is used whenever you start up a site. It must be closed before you close your @Figaro.FigaroEnv handle.



## See Also


#### Reference
@Figaro.DBSite