---
uid: adding-metadata.md
---

# Adding Metadata


Every XML document stored in Figaro actually consists of two kinds of information: the document itself, and metadata.

Metadata can contain an arbitrarily complex set of information. Typically it contains information about the document that you do not or cannot include in the document itself. As an example, you could carry information about the date and time a document was added to the container, last modified, or possibly an expiration time. Metadata might also be used to store information about the document that is external to Figaro, such as the on-disk location where the document was originally stored, or possibly notes about the document that might be useful to the document's maintainer.


In other words, metadata can contain anything — Figaro places no restrictions on what you can use it for. Further, you can both query and index metadata (see Using Figaro Indices for more information). It is even possible to have a document in your container that contains only metadata.


In order to set metadata onto a document, you must:

* (Optionally but recommended) Create a URI for each piece of data (some sort of string)
* Create an attribute name to use for the metadata, again in the form of a string.
* Create the attribute value – the actual metadata information you want to carry on the document – either as an @Figaro.XmlValue or an @Figaro.XmlData class object.
* Set this information on an @Figaro.XmlDocument object.
* Optionally (but commonly) set the actual XML document to the same @Figaro.XmlDocument object.
* Add the @Figaro.XmlDocument to the container.

For example:

``` C#
using (var mgr = new XmlManager() { DefaultContainerType = XmlContainerType.WholeDocContainer })
    {
        using (var cont = mgr.OpenContainer(Path.Combine(homePath,simpleContainer)))
        {
            //create our local, reusable update context
            var ctx = mgr.CreateUpdateContext();
            var inputStream =
                mgr.CreateLocalFileInputStream(Path.Combine(nsData,"Avocado.xml"));
            XmlDocument myDoc = mgr.CreateDocument();

            //don't forget to set the Name property - also a metadata field
            myDoc.Name = Guid.NewGuid().ToString("N");
            myDoc.SetContentAsInputStream(inputStream);

            // create a timestamp for inserted documents
            myDoc.SetMetadata("http://sample.metadata/timestamp",
                              "DateTimeCreated", 
                              new XmlValue(DateTime.Now.ToString())
                              );
            cont.PutDocument(myDoc, mgr.CreateUpdateContext());
        }
    }
```


## See Also


* @Figaro.Container
* @Figaro.XmlDocument
* @Figaro.XmlValue
* @Figaro.XmlData

#### Other Resources
[Adding XML Documents to Containers](xref:adding-xml-documents-to-containers.md)