# Managing Connection Retries

In the event that a communication failure occurs between two environments in a replication group, the Replication Manager will wait a set amount of time before attempting to re-establish the connection. You can configure this wait value using the @Figaro.ReplicationConfig.RetransmissionRequest(System.UInt32, System.UInt32) method.


