---
uid: replacing-xml-in-records.md
---

# Replacing XML in Records

You can either replace a document in its entirety as described here, or you can modify just portions of the document as described in [Modifying XML Data](xref:modifying-xml-data.md).


If you already have code in place to perform document modifications, then replacement is the easiest mechanism to implement. However, replacement requires that at least the entire replacement document be held in memory. Modification, on the other hand, only requires that the portion of the document to be modified be held in memory. Depending on the size of your documents, modification may prove to be significantly faster and less costly to operate.


You can directly replace a document that exists in a container. To do this:

* Retrieve the document from the container. Either do this using an XQuery query and iterating through the results set looking for the document that you want to replace, or use @Figaro.Container.GetDocument(System.String) to retrieve the document by its name. Either way, make sure you have the document as an @Figaro.XmlDocument object.
* Use @Figaro.XmlDocument.SetContent(System.String) or @Figaro.XmlDocument.SetContentAsInputStream(Figaro.XmlInputStream) to set the object's content to the desired value.
* Use @Figaro.Container.UpdateDocument(Figaro.XmlDocument,Figaro.UpdateContext) to save the modified document back to the container.

>[!NOTE]
>Alternatively, you can create a new blank document using @Figaro.XmlManager.CreateDocument(System.Xml.XmlReader), set the document's name to be identical to a document already existing in the container, set the document's content to the desired content, then call @Figaro.Container.UpdateDocument.

``` C#
using (var mgr = new XmlManager())
{
    using (var container = mgr.OpenContainer(simpleContainer))
    {            
        var bananaDoc = container.GetDocument("Bananas.xml");
        bananaDoc.SetContent("<products><product>New Bananas!</product></products>");
        using (var uc = mgr.CreateUpdateContext()) {container.UpdateDocument(bananaDoc, uc);}
    }
}
```

## See Also

* @Figaro.XmlDocument.SetContent(Figaro.XmlData)
* @Figaro.XmlManager.CreateDocument(System.Xml.XmlReader)
* @Figaro.Container.UpdateDocument(Figaro.XmlDocument,Figaro.UpdateContext)
* [Managing XML Data in Containers](xref:managing-xml-data-in-containers.md)
