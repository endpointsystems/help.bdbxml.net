---
uid: using-metadata.md
---

# Using Metadata

Metadata is data about data. That is, it provides additional information about a document that isn't really part of that document. For example, documents added to the `components.dbxml` container were given a name. Each name represents metadata about each individual document. Other common metadata might include the time a document was modified or the name of the person who modified it. In addition, there are cases when modifying the actual document is not possible and additional data is required to track desired information about the document. As an example, you may be required to keep track of what user last altered a document within a container, and you may need to do this in a way that does not modify the document itself. For this reason, Figaro stores metadata separately from the document, while still allowing you to perform indexed searches against the metadata as if it were actually part of the document.

To add custom metadata to a document, use the `setMetaData` command.

```
dbxml> openContainer components.dbxml

dbxml> setMetaData component1 '' modifyuser string john                            

MetaData item 'modifyuser' added to document component1

dbxml> setMetaData component2 '' modifyuser string mary

MetaData item 'modifyuser' added to document component2
```

Metadata is essentially contained within its own, unique namespace (`dbxml:metadata`), so queries against metadata must identify this namespace:

```
dbxml> query '
collection("components.dbxml")/component[dbxml:metadata("modifyuser")="john"]'

1 objects returned for eager expression '
collection("components.dbxml")/component[dbxml:metadata("modifyuser")="john"]'

dbxml> print
<component number="1">
<uses-part>89</uses-part>
<uses-part>150</uses-part>
<uses-part>899</uses-part>
</component>
```

Notice how the metadata doesn't actually appear in the result document. The metadata is not part of the document; it exists only within the container and with respect to a particular document. If you retrieve the document from Figaro and transfer it to another system, the metadata will not be included. This is useful when you need to preserve the original state of a document, but also want to track some additional information while it's stored within Figaro.

## See Also


#### Other Resources
* [Fiagro and XQuery](xref:figaro-and-xquery.md)
* [The dbxml Shell](xref:the-dbxml-shell.md)
* [Adding Metadata](xref:adding-metadata.md)
* [Using FLWOR](xref:using-flwor.md)
* [Retrieving Data using XQuery](xref:retrieving-data-using-xquery.md)
* [Performing Queries](xref:performing-queries.md)