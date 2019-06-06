---
uid: inserting-nodes.md
---

# Inserting Nodes

To insert a node into an existing document, you must identify the node that you want to insert, and the location in the document where you want the insertion to be performed. You indicate that you are performing an insertion operation using the XQuery insert keyword.

The general format of this expression is:


`insert nodes`_nodes keyword position_


where:

* _nodes_ is the content you want to insert. This can be a string, or it can be an XQuery selection statement.
* _keywords_ indicates how you would like the new content to be inserted.
* _position_ indicates the document and the location in that document where the insertion is to occur.

Be aware that `position` must be an XQuery expression that selects exactly one location in the document. Also, keywords can be one of several keywords that indicate where the new content is to be inserted relative to the location in the document that is indicated by position. See the [Position Keywords](xref:position-keywords.md) topic for information on the available keywords.

For example, consider the document:

``` XML
<a>
  <b1>first child</b1>
  <b2>second child</b2>
  <b4>inserted child</b4>
  <b3>third child</b3>
</a>
```

Assuming this document is called 'mydoc.xml', then you can insert a node, `b4`, after node `b2` using the following query expression:


```
insert nodes <b4>inserted child</b4> after
doc("dbxml:/container.dbxml/mydoc.xml")/a/b2
```

The above expression applied to the XML document results in a document like this:

``` XML
<a>
  <b1>first child</b1>
  <b2>second child</b2>
  <b4>inserted child</b4>
  <b3>third child</b3>
</a>
```


## See Also

* [Modifying XML Data](xref:modifying-xml-data.md)
* [Managing XML Data in Containers](xref:managing-xml-data-in-containers.md)
