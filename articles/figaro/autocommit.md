---
uid: autocommit.md
---

# AutoCommit

While transactions are frequently used to provide atomicity to multiple container operations, it is sometimes necessary to perform a single container operation under the control of a transaction. Rather than force you to obtain a transaction, perform the single write operation, and then either commit or abort the transaction, you can automatically group this sequence of events using auto commit.


To use auto commit:

Open your environment and containers so that they support transactions. See [Enabling Transactions](xref:enabling-transactions.md) for details.
Do not provide a transactional handle to the method that is performing the container write operation.

>[!WARNING]
>The Oracle BDB XML support team has advised against using auto commit transactions within applications, instead recommending users explicitly use transactions wherever and whenever possible. As a result, measures were taken initially (in earlier versions) to ensure that functionality was completely disabled within the Figaro library.
>However, the documentation has not been changed throughout the years, and there may be some circumstances where it can be deemed safe to perform an operation without an explicit transaction (for example, opening a container). Therefore, the restrictions inside the Figaro library will be disabled, and it will be up to the user to decide where or when they want to use the auto commit feature.

## See Also

@Figaro.EnvConfig

#### Other Resources
[Turning off Auto Commit (Oracle Berkeley DB XML forum)](https://community.oracle.com/message/3606037?#3604037)
