---
uid: replication-elections.md
---

# Replication Elections

Finding a master environment is one of the fundamental activities that every replication replica must perform. Upon startup, the underlying DB replication code will attempt to locate a master. If a master cannot be found, then the environment should initiate an election.

## Influencing Elections

If you want to control the election process, you can declare a specific environment to be the master. Should the master become unavailable during run-time for any reason, an election is held. The environment that receives the most number of votes, wins the election and becomes the master. A machine receives a vote because it has the most up-to-date log records.


Because ties are possible when elections are held, it is possible to influence which environment will win the election. How you do this depends on which API you are using. In particular, if you are writing a custom replication layer, then there are a great many ways to manually influence elections.


One such mechanism is priorities. When votes are cast during an election, the winner is determined first by the environment with the most up-to-date log records. But if this is a tie, the environment's priority is considered. So given two environments with log records that are equally recent, votes are cast for the environment with the higher priority.


Therefore, if you have a machine that you prefer to become a master in the event of an election, assign it a high priority. Assuming that the election is held at a time when the preferred machine has up-to-date log records, that machine will win the election.



## Winning Elections

To win an election:

* There cannot currently be a master environment.
* The environment must have the most recent log records. Part of holding the election is determining which environments have the most recent log records. This process happens automatically; your code does not need to involve itself in this process.
* The environment must receive the most number of votes from the replication environments that are participating in the election.
* In the event of a tie vote, the environment with the highest priority wins the election. If two or more environments receive the same number of votes and have the same priority, then the underlying replication code picks one of the environments to be the winner. Which winner will be picked by the replication code is unpredictable from the perspective of your application code.



## Switching Masters

To switch masters:

* Start up the environment that you want to be master as normal. At this time it is a replica. Make sure this environment has a higher priority than all the other environments.
* Allow the new environment to run for a time as a replica. This allows it to obtain the most recent copies of the log files.
* Shut down the current master. This should force an election. Because the new environment has the highest priority, it will win the election, provided it has had enough time to obtain all the log records.
* Optionally restart the old master environment. Because there is currently a master environment, an election will not be held and the old master will now run as a replica environment.</li></ol>
