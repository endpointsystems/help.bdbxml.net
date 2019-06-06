---
uid: replacement-rules.md
---

# Replacement Rules

When replacing elements, the selection expression used to select the target must be non-updating, and it must not result in an empty set.

Selection results must consist of a single element, text, comment or processing instruction. In addition, the selection expression must not select a node without a parent node.


Finally, If you replace an attribute node, its replacement value must not have a namespace property that conflicts with the namespaces property of the parent node.

## See Also


#### Other Resources
* [Modifying XML Data](xref:modifying-xml-data.md)
* [Managing XML Data in Containers](xref:managing-xml-data-in-containers.md)
