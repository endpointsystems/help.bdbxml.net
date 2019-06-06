---
uid: deleting-indices.md
---

# Deleting Indices

To delete an index from a container:

* Retrieve the index specification from the container.
* Use @Figaro.XmlIndexSpecification.ReplaceDefaultIndex(System.String) to delete the index from the index specification.
* Set the updated index specification back to the container.

``` C#
//get our manager
using (var mgr = new XmlManager(ManagerInitOptions.AllowExternalAccess | ManagerInitOptions.AllowAutoOpen))
{
    //open a container
    using (var container = mgr.OpenContainer(nsContainer))
    {
        //Get the collection of index specifications
        XmlIndexSpecification spec = container.GetIndexSpecification();
        spec.DeleteIndex("http://groceryItem.dbxml/fruits", "item", "edge-element-equality-string");
        container.SetIndexSpecification(spec, mgr.CreateUpdateContext());
    }
}
```


## See Also

* @Figaro.XmlIndexSpecification.ReplaceDefaultIndex(System.String)
* [Managing Indices](xref:managing-indices.md)
* [Using Indices](xref:using-indices.md)