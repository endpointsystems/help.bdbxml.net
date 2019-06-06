---
uid: resolving-conflicting-updates.md
---

# Resolving Conflicting Updates

Modifications that you specify as a part of an update query are not actually made until after the query is completed. The order in which update statements are made may or may not be relevant when it comes time to apply the update. As a result, it's possible to request an update that on its own is acceptable, but when used with other update statement may result in an error.


Keep the following rules in mind as you use update expressions:

An exception is raised if:

* Two or more `rename` expressions target the same node
* Two or more `replace` expressions or `replace value of` expressions target the same node

The following expressions are made effective, in the following order:

* All `insert into`, `insert attributes` and `replace value` expressions in the order they are supplied.
* All `insert before`, `insert after`, `insert as first`, and `insert as last` expressions in the order they are supplied.
* All `replace` expressions
* All `replace value of` expressions
* All `delete` expressions

Note that atomicity of the expression is guaranteed; either the entire expression is made effective with regard to the original document, or no aspect of the expression is made effective.

## See Also

* [Modifying XML Data](xref:modifying-xml-data.md)
* [Managing XML Data in Containers](xref:managing-xml-data-in-containers.md)