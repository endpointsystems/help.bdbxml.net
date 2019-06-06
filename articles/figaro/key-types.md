---
uid: key-types.md
---

# Key Types

The Key Type identifies what sort of test the index supports. You can use one of three key types:

Key Type | Description
-------- | -----------
Equality | The performances of tests that look for nodes with a specific value.
Presence | Improves the performance of tests that look for the existence of an node, regardless of its value.
Substring | Improves the performance of tests that look for a node whose value contains a given substring. This key type is best used when your queries use the XQuery `contains()` substring function.

## See Also
* [Index Types](xref:index-types.md)
* [Using Indices](xref:using-indices.md)
