---
uid: transform-functions.md
---

# Transform Functions

While it is true that you cannot run an update query and simultaneously return the results, there is a way to almost do the same thing. You do this by making a copy of the nodes that you want to modify, then perform the modifications against that copy. The result of the modification is returned to you. This type of an operation is called a transformation.


Note that when you perform a transformation, the original nodes that you copied are not modified. For this reason, transformations are often limited only to situations where you want to modify a query result — for reporting purposes, for example.


To run a transformation, use the:

`copy` keyword to copy the nodes of interest@Figaro.UpdateContext

`modify` keyword to perform the XQuery Update against the newly copied nodes@Figaro.UpdateContext

`return` keyword to return the result of the transformation.

For example, given the following XML document (which is identified as document `mydoc.xml`, and is stored in container `con.dbxml`):


``` XML
<a>
  <aab1>first child</aab1>
  <b2>second child</b2>
  <b3>third child</b3>
</a>
```

then the following transformation:


``` XML
copy $c := doc("dbxml:/con.dbxml/mydoc.xml")/a
modify (delete nodes $c/aab1,
replace value of node $c/b2 with "replacement value")
return $c
```

results in the following document:


``` XML
<a>
  <b2>replacement value</b2>
  <b3>third child</b3>
</a>
```


## See Also

* [Modifying XML Data](xref:modifying-xml-data.md)
* [Managing XML Data in Containers](xref:managing-xml-data-in-containers.md)
