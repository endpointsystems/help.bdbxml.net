---
uid: managing-election-times.md
---

# Managing Election Times

Where it comes to elections, there are two timeout values with which you should be concerned: election timeouts and election retries.

## Managing Election Timeouts

When an environment calls for an election, it will wait some amount of time for the other replicas in the replication group to respond. The amount of time that the environment will wait before declaring the election completed is the election timeout.


If the environment hears from all other known replicas before the election timeout occurs, the election is considered a success and a master is elected.


If only a subset of replicas respond, then the success or failure of the election is determined by how many replicas have participated in the election. It only takes a simple majority of replicas to elect a master. If there are enough votes for a given environment to meet that standard, then the master has been elected and the election is considered a success.


However, if not enough replicas have participated in the election when the election timeout value is reached, the election is considered a failure and a master is not elected. At this point, your replication group is operating without a master, which means that, essentially, your replicated application has been placed in read-only mode.


Note, however, that the Replication Manager will attempt a new election after a given amount of time has passed.

## Managing Election Retry Times

In the event that a election fails, an election will not be attempted again until the election retry timeout value has expired.


You set the retry value using the @Figaro.FigaroReplicationManager.SetConfiguration(Figaro.ReplicationConfig) method and the @Figaro.ReplicationConfig object.



## See Also


#### Reference
* @Figaro.FigaroReplicationManager.SetConfiguration(Figaro.ReplicationConfig)
* @Figaro.ReplicationConfig