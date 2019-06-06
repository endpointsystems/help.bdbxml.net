---
uid: position-keywords.md
---

# Position Keywords

XQuery Update expressions that add content to a document must first select the location in the document where the content is to be added, and then it must identify where the content is to be added relative to the selected location. You do this by specifying the appropriate keywords to the update expression.


Valid keywords are:
* `before` - The new content precedes the target node
* `after` - The new content follows the target node
* `as first into` - The new content becomes the first child of the target node.
* `as last into` - The new content becomes the last child of the target node.
* `into` - The new content is inserted as the last child of the target node, provided that this keyword is not used in an update expression that also makes use of the keywords noted above. It that happens, the node is inserted so that it does not interfere with the indicated position of the other new nodes. 

>[!NOTE]
The behavior described here is an artifact of Figaro's current implementation of the XQuery Update specification. The specification does not require the inserted node to be placed as the last child of the target node, so this behavior may change for some future release of the product.

## See Also

* [Modifying XML Data](xref:modifying-xml-data.md)
* [Managing XML Data in Containers](xref:managing-xml-data-in-containers.md)
