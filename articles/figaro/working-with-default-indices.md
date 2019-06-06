---
uid: working-with-default-indices.md
---

# Working with Default Indices

Default indices are applied to all applicable nodes in the container that are not otherwise indexed. For example, if you declare a default index for a metadata node, then all metadata nodes will be indexed according to that indexing strategy, unless some other indexing strategy is explicitly set for them. In this way, you can avoid the labor of specifying a given indexing strategy for all occurrences of a specific kind of a node.


You add, delete, and replace default indices using:
* @Figaro.XmlIndexSpecification.AddDefaultIndex(Figaro.IndexingStrategy) 
* @Figaro.XmlIndexSpecification.DeleteIndex(Figaro.XmlIndex) 
* @Figaro.XmlIndexSpecification.ReplaceDefaultIndex(System.String)

When you work with a default index, you identify only the indexing strategy; you do not identify a URI or node name to which the strategy is to be applied.

Note that just as is the case with other indexing methods, you can use either strings or enumerated types to identify the index strategy.

For example, to add a default index to a container:

``` C#
  //get our manager
using (var mgr = new XmlManager())
{
    //open a container
    using (var container = mgr.OpenContainer(simpleContainer))
    {
        IndexingStrategy strategy =
            new IndexingStrategy
            {
                KeyType = IndexKeyType.Equality,
                NodeType = IndexNodeType.Metadata,
                PathType = IndexPathType.NodePath,
                IndexValueSyntax = XmlDatatype.String
            };
        XmlIndexSpecification spec = container.GetIndexSpecification();
        spec.AddDefaultIndex(strategy);
        container.SetIndexSpecification(spec, mgr.CreateUpdateContext());
    }
}
```


## See Also


#### Other Resources
[Using Indices](xref:using-indices.md)