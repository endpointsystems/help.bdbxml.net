# Partitioning Databases

You can improve concurrency on your database reads and writes by splitting access to a single database into multiple databases. This helps to avoid contention for internal database pages, as well as allowing you to spread your databases across multiple disks, which can help to improve disk I/O.

While you can manually do this by creating and using more than one database for your data, DB is capable of partitioning your database for you. When you use DB's built-in database partitioning feature, your access to your data is performed in exactly the same way as if you were only using one database; all the work of knowing which database to use to access a particular record is handled for you under the hood.

> [!IMPORTANT]
> Only the BTree and Hash access methods are supported for partitioned databases.

There are two ways to indicate what key/data pairs should go on which partition. The first is by specifying an array of DBTs that indicate the minimum key value for a given partition by using <xref:DatabaseConfig.SetPartitionByKeys>. The second is by providing a callback that returns the number of the partition on which a specified key is placed in the <xref:DatabaseConfig.SetPartitionByCallback> method.

Once you have partitioned a database, you cannot change your partitioning scheme.

## Specifying Partition Keys

For simple cases, you can partition your database by providing an array of <xref:DatabaseEntry>, each element of which provides the minimum key value to be placed on a partition. There must be one fewer elements in this array than you have partitions. The first element of the array indicates the minimum key value for the second partition in your database. Key values that are less than the first key value provided in this array are placed on the first partition (partition 0).

>[!NOTE]
> You can only use partition keys if you are using the **Btree** access method.

For example, suppose you had a database of fruit, and you want three partitions for your database. Then you need a <xref:DatabaseEntry> array of size two. The first element in this array indicates the minimum keys that should be placed on partition 1. The second element in this array indicates the minimum key value placed on partition 2. Keys that compare less than the first <xref:DatabaseEntry> in the array are placed on partition 0.

All comparisons are performed according to the lexicographic comparison used by your platform.

In our GettingStarted.Btree.Partitioning example, we take the vendor and inventory databases and partition them. For vendor, we partition by VendorId which is an `int`:

```cs
    // we have 50k vendors, so we're going to split into two equal partitions of 25k
    cfg.SetPartitionByKeys(new[] {new DatabaseEntry(BitConverter.GetBytes(25000))});
```

## Partitioning callback

In some cases, a simple lexicographical comparison of key data will not sufficiently support a partitioning scheme. For those situations, you should write a <xref:PartitionDelegate> lambda in the <xref:DatabaseConfig.SetPartitionByCallback> method, taking a key and returning a partition number where the key belongs. This function accepts a pointer to the DB and the DBT, and it returns the number of the partition on which the key belongs.

Note that DB actually places the key on the partition calculated by:

```
returned_partition modulo number_of_partitions
```

For example, if you have three partitions and you want your data to go into the first, you could return `0`, because `0 % 3 = 0`; to get into the first, you'd enter `1`, and to get into the third you'd return `2`.

## Placing partition files
When you partition a database, a database file is created on disk in the same way as if you were not partitioning the database. That is, this file uses the name you provide to the <xref:Database.Open> file parameter.

However, DB then also creates a series of database files on disk, one for each partition that you want to use. These partition files share the same name as the database file name, but are also number sequentially. So if you create a database named `mydb.db`, and you create 3 partitions for it, then you will see the following database files on disk:

```shell
mydb.db
__dbp.mydb.db.000
__dbp.mydb.db.001
__dbp.mydb.db.002 
```

>[!NOTE]
> Berkeley DB for .NET Core doesn't currently support partition directories. 