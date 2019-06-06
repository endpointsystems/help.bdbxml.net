---
uid: node-types.md
---

# Node Types

Figaro can index three types of nodes: `element`, `attribute`, or `metadata`. Metadata nodes are, of course, indices set for a document's metadata content.


## Element and Attribute Nodes

Element and attribute nodes are only found in document content. In the following document:


``` XML
<vendor type="wholesale">
  <name>TriCounty Produce</name>
</vendor>
```

`vendor` and `name` are element nodes, while `type` is an attribute node.


Use the element node type to improve queries that test the value of an element node. Use the attribute node type to improve any query that examines an attribute or attribute value.



## Metadata Nodes

Metadata nodes are found only in a document's metadata content. These indices improve the performance of querying for documents based on metadata information. If you are declaring a metadata node, you cannot use a path type of edge.



## See Also

[Index Types](xref:index-types.md)
[Using Indices](xref:using-indices.md)