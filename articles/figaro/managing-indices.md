---
uid: managing-indices.md
---

# Managing Indices

Indices are set for a container using the container's index specification. You can specify an index either against a specific node and namespace, or you can define default indices that are applied to every node in the container.

You add, delete, and replace indices using the container's index specification. You can also iterate through the specification, so as to examine each of the indices declared for the container. Finally, if you want to retrieve all the indices maintained for a named node, you can use the index specification to find and retrieve them.

An API exists that allows you to retrieve all of the documents or nodes referenced by a given index.

For simple programs, managing the index specification and then setting it to the container (as is illustrated in the following examples) can be tedious. For this reason, The Figaro API also provides index management functions directly on the container.

Performing index modifications (for example, adding and replacing indices) on a container that already contains documents can be a very expensive operation — especially if the container holds a large number of documents, or very large documents, or both. This is because indexing a container requires Figaro to traverse every document in the container.

>[!NOTE]
>If you are considering re-indexing a large container, be aware that the operation can take a long time to complete.

## In This Section
* [Adding Indices](xref:adding-indices.md)
* [Deleting Indices](xref:deleting-indices.md)
* [Replacing Indices](xref:replacing-indices.md)
