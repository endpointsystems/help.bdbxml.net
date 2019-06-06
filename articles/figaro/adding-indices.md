---
uid: adding-indices.md
---

# Adding Indices

To add an index to a container:

* Retrieve the index specification from the container. 
* Use @Figaro.XmlIndexSpecification.AddIndex(Figaro.XmlIndex) to add the index to the container. You must provide to this method the namespace and node name to which the index is applied. You must also identify the indexing strategy. If the index already exists for the specified node, then the method silently does nothing.
* Set the updated index specification back to the container.

For example:



``` C#
//get our manager
var mgr = new XmlManager(ManagerInitOptions.AllowExternalAccess | ManagerInitOptions.AllowAutoOpen);
//open a container
using (var container = mgr.OpenContainer(simpleContainer))
{
    XmlIndexSpecification xis = container.GetIndexSpecification();
    xis.AddIndex(new XmlIndex
                      {
                          Index = "node-element-presence-none",
                          NodeName = "vendor",
                          Namespace = String.Empty
                      });
    container.SetIndexSpecification(xis, mgr.CreateUpdateContext());
}
```


## See Also

* @Figaro.XmlIndexSpecification.AddIndex(Figaro.XmlIndex)
* [Managing Indices](xref:managing-indices.md)
* [Using Indices](xref:using-indices.md)