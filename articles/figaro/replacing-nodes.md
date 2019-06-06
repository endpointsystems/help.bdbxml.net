---
uid: replacing-nodes.md
---

# Replacing Nodes

You can use XQuery Update statements to either replace an entire node or a node's value. To replace a node, use the replace node query. For example, given the document named `mydoc.xml` in container `con.dbxml`:


``` XML
<a>
  <b1>first child</b1>
  <b2>second child</b2>
  <b4>inserted child</b4>
  <b3>third child</b3>
</a>
```

You can replace node b2 with a different node such as `<r1>replacement child</r1>` using the following query:

``` XML
replace node doc("dbxml:/con.dbxml/mydoc.xml")/a/b2
with <z1>replacement node</z1>
```

The result of this replacement query is:


``` XML
<a>
  <b1>first child</b1>
  <z1>replacement node</z1>
  <b3>third child</b3>
</a>
```

The replacement value can also be a selection expression. For example, suppose you had a second document named `replace.xml`:

``` XML
<a>
<rep>more replacement data</rep>
</a>
```

Then you can replace node `z1` with the `rep` node using the following query:


``` XML
replace node doc("dbxml:/con.dbxml/mydoc.xml")/a/z1
with doc("dbxml:/con.dbxml/replace.xml")/a/rep
```

This results in the document:


``` XML
<a>
  <b1>first child</b1>
  <rep>more replacement data</rep>
  <b3>third child</b3>
</a>
```

In addition to the `replace node ... with ...` form, you can also replace node values. Do this using `replace value of node ... with ...` queries.

For example, to replace the value of the rep node, above, use:


``` XML
replace value of node doc("dbxml:/con.dbxml/mydoc.xml")/a/rep
with "random replacement text".
```

The results of this query is:


**XML**
``` XML
<a>
  <b1>first child</b1>
  <rep>random replacement text</rep>
  <b3>third child</b3>
</a>
```


## See Also

* [Modifying XML Data](xref:modifying-xml-data.md)
* [Managing XML Data in Containers](xref:managing-xml-data-in-containers.md)
