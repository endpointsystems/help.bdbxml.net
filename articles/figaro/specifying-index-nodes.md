---
uid: specifying-index-nodes.md
---

# Specifying Index Nodes


It is possible to have Figaro build indices at a node granularity rather than a document granularity. The difference is that document granularity is good for retrieving large documents while node granularity is good for retrieving nodes from within documents.

Indexing nodes can only be performed if your containers are performing node-level storage. You should consider using node indices if you have a few large documents stored in your containers and you will be performing queries intended to retrieve subsections of those documents. Otherwise, you should use document level indexes.

Because node indices can actually be harmful to your application's performance, depending on the actual read/write activity on your containers, expect to experiment with your indexing strategy to find out whether node or document indexes work best for you.

Node indices contain a little more information, so they may take more space on disk and could also potentially take longer to write. For example, consider the following document:

``` XML
<names>
  <name>Joe</name>
  <name>Joe</name>
  <name>Fred</name>
</names>
```

If you are using document-level indexing, then there is one index entry for each unique value occurring in the document for a given index. So if you have a string index on the name element, the above document would result in two index entries; one for Joe and another for Fred.


However, for node-level indices, there is one index entry for each node regardless of whether it is unique. Therefore, given an a string index on the name element, the above document would result in three index entries.


Given this, imagine that the document in use had 1000 name elements, 500 of which contained Joe and 500 of which contained Fred. For document-level indexing, you would still only have two index entries, while for node-level indexing you would have 1000 index entries per stored document. Whether the considerably larger size of the node-level index is worthwhile is something that you would have to evaluate based on the number of documents you are storing and the nature of your query patterns.


Note that by default, containers of type @Figaro.XmlContainerType.NodeContainer use node-level indexes. Containers of type @Figaro.XmlContainerType.WholeDocContainer use document level indexes by default. You can change the default indexing strategy for a container by setting the @Figaro.ContainerConfig.IndexNodes to @Figaro.ConfigurationState.On (for node-level indexes) and @Figaro.ConfigurationState.Off (for document-level indexes) for the container.


You can tell whether a container is using node-level indices using the method. If the container is creating node-level indices, this method will return true.


You can switch between node-level indices and document-level indices using @Figaro.XmlManager.ReindexContainer(System.String,Figaro.UpdateContext,Figaro.ReindexOptions). Specify @Figaro.ReindexOptions.IndexNodes to cause a the container to use node-level indices. To switch from node-level to document-level indices, use @Figaro.ReindexOptions.NoIndexNodes.

>[!NOTE]
>This method causes your container to be completely re-indexed. Therefore, for containers containing large amount of data, or large numbers of indices, or both, this method should not be used routinely as it may take some time to write the new indices.

## See Also


#### Reference
* @Figaro.XmlManager.ReindexContainer(System.String,Figaro.UpdateContext,Figaro.ReindexOptions)
* @Figaro.ReindexOptions
* @Figaro.XmlContainerType

#### Other Resources
* [Using Indices](xref:using-indices.md)