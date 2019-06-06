---
uid: reading-uncommitted-data.md
---

# Reading Uncommitted Data

You can configure your application to read data that has been modified but not yet committed by another transaction; that is, dirty data. When you do this, you may see a performance benefit by allowing your application to not have to block waiting for write locks. On the other hand, the data that your application is reading may change before the transaction has completed.

When used with transactions, uncommitted reads means that one transaction can see data modified but not yet committed by another transaction. When used with transactional cursors, uncommitted reads means that any container reader can see data modified by the cursor before the cursor's transaction has committed.

Because of this, uncommitted reads allow a transaction to read data that may subsequently be aborted by another transaction. In this case, the reading transaction will have read data that never really existed in the container.


To configure your application to read uncommitted data:
* Open your container such that it will allow uncommitted reads. You do this by specifying @Figaro.ContainerConfig.ReadUncommitted when you open your container.
* Specify @Figaro.TransactionType.ReadUncommitted when you create the transaction, or specify @Figaro.QueryOptions.ReadUncommitted when you execute a query against your container.

## See Also

* @Figaro.ContainerConfig
* @Figaro.QueryOptions
* @Figaro.TransactionType