---
uid: replacing-indices.md
---

# Replacing Indices

You can replace the indices maintained for a specific node by using @Figaro.XmlIndexSpecification.ReplaceIndex(Figaro.XmlIndex). When you replace the index for a specified node, all of the current indices for that node are deleted and the replacement index strategies that you provide are used in their place.


Note that all the indices for a specific node can be retrieved and specified as a space separated list in a single string. So if you set a `node-element-equality-string` and a `node-element-presence` index for a given node, then its indices are identified as:


**XML**
``` XML
"node-element-equality-string node-element-presence"
```


## See Also


#### Reference
@Figaro.XmlIndexSpecification

#### Other Resources
[Managing Indices](xref:managing-indices.md)
[Using Indices](xref:using-indices.md)
