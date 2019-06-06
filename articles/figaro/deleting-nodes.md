---
uid: deleting-nodes.md
---

# Deleting Nodes

You can delete zero or more nodes using a `delete nodes` query. For example, given the document named **mydoc.xml** in container **con.dbxml**:

``` XML
<a>
  <b1>first child</b1>
  <b2>second child</b2>
  <b4>inserted child</b4>
  <b3>third child</b3>
</a>
```

The following query deletes the `b4` node:


``` XML
delete nodes doc("dbxml:/con.dbxml/mydoc.xml")/a/b4
```

The selection expression that you provide must be a non-updating expression, and the result must be a sequence of zero or more nodes. If the selection expression selects a node that has no parent, then the result is to delete the entire document from the container.


## See Also

* [Modifying XML Data](xref:modifying-xml-data.md)
* [Managing XML Data in Containers](xref:managing-xml-data-in-containers.md)
