---
uid: renaming-nodes.md
---

# Renaming Nodes

You can rename a node using `rename node` query. For example, given the document named "mydoc.xml" in container "con.dbxml":

``` XML
<a>
<b1>first child</b1>
<b2>second child</b2>
<b3>third child</b3>
</a>
```

You can rename node `b3` to `z1` using the following query:


```
rename node doc("dbxml:/con.dbxml/mydoc.xml")/a/b3 as "z1"
```

The selection expression that you provide must be a non-updating expression, and the result must be non-empty and consist of a single element, attribute, or processing instruction node.



## See Also


#### Other Resources
* [Modifying XML Data](xref:modifying-xml-data.md)
* [Managing XML Data in Containers](xref:managing-xml-data-in-containers.md)
