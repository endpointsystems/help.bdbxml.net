---
uid: using-indices.md
---

# Using Indices


Figaro provides a robust and flexible indexing mechanism that can greatly improve the performance of your Figaro queries. Designing your indexing strategy is one of the most important performance aspects of designing a Figaro-based application.


To make the most effective usage of Figaro indices, design your indices for your most frequently occurring XQuery queries. Be aware that Figaro indices can be updated or deleted in-place so if you find that your application's queries have changed over time, then you can modify your indices to meet your application's shifting requirements.

>[!NOTE]
>The time it takes to re-index a container is proportional to the container's size. Re-indexing a container can be an extremely expensive and time-consuming operation. If you have large containers in use in a production setting, you should not expect container re-indexing to be a routine operation.

You can define indices for both document content and for metadata. You can also define default indices that are used for portions of your documents for which no other index is defined.


When you declare an index, you must identify its type and its syntax. There are two ways that you can provide this information. One way is to provide a string that identifies the type and syntax for the index. The other way is to use enumerated types to do that same thing.


Most of the Figaro APIs that you use to manage indices allow you to use either form for declaring indices. A few methods, however, only support the string approach.


See [Syntax Types](xref:syntax-types.md) for information on specifying the index syntax.

## In This Section
* [Index Types](xref:index-types.md)
* [Specifying Index Nodes](xref:specifying-index-nodes.md)
* [Specifying Index Strategies](xref:specifying-index-strategies.md)
* [Managing Indices](xref:managing-indices.md)
* [Examining Indices](xref:examining-indices.md)
* [Working with Default Indices](xref:working-with-default-indices.md)
* [Looking Up Indexed XML Data](xref:looking-up-indexed-xml-data.md)

## See Also
* [Index Types](xref:index-types.md)
