---
uid: path-types.md
---

# Path Types


If you think of an XML document as a tree of nodes, then there are two types of path elements in the tree. One type is just a node, such as an element or attribute within the document. The other type is any location in a path where two nodes meet. The path type, then, identifies the path element type that you want indexed. Path type node indicates that you want to index a single node in the path. Path type edge indicates that you want to index the portion of the path where two nodes meet.

Of the two of these, the Figaro query processor prefers edge-type indices because they are more specific than a node-type index. This means that the query processor will use an edge-type index over a node-type if both indices provide similar information.


Consider the following document:

``` XML
<vendor type="wholesale">
  <name>TriCounty Produce</name>
  <address>309 S. Main Street</address>
  <city>Middle Town</city>
  <state>MN</state>
  <zipcode>55432</zipcode>
  <phonenumber>763 555 5761</phonenumber>
  <salesrep>
    <name>Mort Dufresne</name>
    <phonenumber>763 555 5765</phonenumber>
  </salesrep>
</vendor>
```

Suppose you want to declare an index for the name node in the preceding document. In that case:

Path Type | Description
--------- | ------------
Node | There are two locations in the document where the name node appears. The first of these has a value of "TriCounty Produce," while the second has a value of "Mort Dufresne." The result is that the name node will require two index entries, each with a different value. Queries based on a name node may have to examine both index entries in order to satisfy the query.
Edge | There are two edge nodes in the document that involve the name node: `/vendor/name ` and `salesrep/name`. Indices that use this path type are more specific because queries that cross these edge boundaries only have to examine one index entry for the document instead of two.

Given this, use:
* `Node` path types to improve queries where there can be no overlap in the node name. That is, if the query is based on an element or attribute that appears on only one context within the document, then use node path types. In the preceding sample document, you would want to use node-type indices with the `address`, city, state, `zipcode`, and `salesrep` elements because they appear in only one context within the document.
* `Edge` path types to improve query performance when a node name is used in multiple contexts within the document. In the preceding document, use edge path types for the name and `phonenumber` elements because they appear in multiple (2) contexts within the document.

## See Also
* [Index Types](xref:index-types.md)
* [Using Indices](xref:using-indices.md)
