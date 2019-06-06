---
uid: xquery-a-brief-introduction.md
---

# XQuery: A Brief Introduction

XQuery can be used to:
* *Query for a document*. Note that queries can be formed against an individual document, or against multiple documents.
* *Query for document subsections*, including values found on individual document nodes.
* *Manipulate and transform* the results of a query.
* *Modify a document* (see [Modifying XML Data](xref:modifying-xml-data.md) for more information).

To do this, XQuery views an XML document as a collection of element, text, and attribute nodes. For example, consider the following XML document:

``` XML
<?xml version="1.0"?>
<Node0>
  <Node1 class="myValue1">Node1 text </Node1>
  <Node2>
    <Node3>Node3 text</Node3>
    <Node3>Node3 text 2</Node3>
    <Node3>Node3 text 3</Node3>
    <Node4>300</Node4>
  </Node2>
</Node0>
```

In the above document, `<Node0>` is the document's root node, and `<Node1>` is an element node. Further, the element node, `<Node1>`, contains a single attribute node whose name is class and whose value is `myValue1`. Finally, `<Node1>` contains a text node whose value is `Node1` text.


#### Other Resources
* [Modifying XML Data](xref:modifying-xml-data.md)
* [Using XQuery](xref:using-xquery.md)