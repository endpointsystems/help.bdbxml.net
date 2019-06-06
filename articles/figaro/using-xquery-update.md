---
uid: using-xquery-update.md
---
# Using XQuery Update


XQuery Update allows you to insert, delete, replace and rename nodes using built-in keywords (`insert`, `delete`, `replace` and `rename`, respectively). You can also perform a node update by declaring an `update` function.

XQuery Update does not perform updates (node insertion, deletion, and so forth) until after the query has completed. This means a couple of things. First, you cannot perform an update and return the results in the same query.


Also, update statements are order independent, although in some cases conflicting updates are performed in an order defined by the XQuery Update statement specification).


Finally, updates are generally expected to be performed in isolation from other queries. You cannot, for example, perform a search and then in a subsequent statement perform an update, all in the same query.

>[!NOTE]
> XQuery Update is described in the W3C specification, [XQuery Update Specification](http://www.w3.org/TR/2007/WD-xquery-update-10-20070828/). This specification is currently a working draft. Figaro's XQuery language engine, [XQilla](http://xqilla.souceforge.net), implements the version specified in the XQilla release documentation.

## See Also

* [XQuery Update Specification](http://www.w3.org/TR/2007/WD-xquery-update-10-20070828/)
* [XQilla](http://xqilla.souceforge.net)
* [Modifying XML Data](xref:modifying-xml-data.md)
* [Managing XML Data in Containers](xref:managing-xml-data-in-containers.md)
