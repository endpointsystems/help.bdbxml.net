---
uid: insertion-rules.md
---

# Insertion Rules

When inserting elements, the selection expression must be non-updating, and it must not result in an empty set.

If any form of the `into` keyword is specified, the selection expression must result in a single element or document node. Also, if before or after is provided, the selection expression result must be a single element, text, comment or processing instruction node.


If an attribute node is selected, then the new content must provide an attribute.

## See Also

* [Modifying XML Data](xref:modifying-xml-data.md)
* [Managing XML Data in Containers](xref:managing-xml-data-in-containers.md)
