---
uid: deleting-records.md
---

# Deleting Records

You can delete a document by calling 
@Figaro.Container.DeleteDocument. This method can operate either on a document's name or on an @Figaro.XmlDocument object. You might want to use an @Figaro.XmlDocument object to delete a document if you have queried your container for some documents and you want to delete every document in the results set.
>[!NOTE]
>In a non-transactional environment, the delete changes are not committed to the container until the container is closed and re-opened.

``` C#
using (var mgr = new XmlManager())
{
    using (var container = mgr.OpenContainer(simpleContainer))
    {
        var numDocs = container.GetNumDocuments();
        trace("container has {0} documents.",numDocs);
        container.DeleteDocument("Bananas.xml",mgr.CreateUpdateContext());
        numDocs = container.GetNumDocuments();
        trace("container has {0} documents.", numDocs);
    }
}
```


## See Also


#### Reference
@Figaro.Container.DeleteDocument
@Figaro.XmlDocument

#### Other Resources
@managing-xml-data-in-containers.md