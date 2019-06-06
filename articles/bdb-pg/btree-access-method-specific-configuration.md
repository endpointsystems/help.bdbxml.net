# Btree access method specific configuration

There are a series of configuration tasks which you can perform when using the Btree access method. They are described in the following sections.

> [!NOTE]
> The Compare, PrefixCompare and Compress delegates are read-only in Berkelye DB Core as they are implemented internally by Oracle.

## Using Record Numbers

The Btree access method optionally supports retrieval by logical record numbers. To configure a Btree to support record numbers, configure the <xref:BTreeDatabaseConfig> object with the <xref:UseRecordNumbers> set to true.

Configuring a Btree for record numbers should not be done lightly. While often useful, it may significantly slow down the speed at which items can be stored into the database, and can severely impact application throughput. Generally __it should be avoided in trees with a need for high write concurrency__.