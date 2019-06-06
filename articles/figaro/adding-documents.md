---
uid: adding-documents.md
---

# Adding Documents

Adding a document to a container is done with the @Figaro.Container.PutDocument method. To do this, you must perform the following steps:



## Prerequisites

You must have the @Figaro.Container object opened and assigned to a working variable before you can insert XML data into it.

### Adding a Document to a Container

**Obtain the document you want to put into the container.** You can do this with the path and filename of a document residing on the file system, a string variable containing the document contents, or through the Figaro API using @Figaro.XmlDocument and related objects.
Provide a name for the document. The document name must be unique or an exception is thrown. You can also request the 

@Figaro.Container generate a unique name for you by passing @Figaro.PutDocumentOptions.GenerateFileName to the method.

>[!NOTE]
> If you pass a name and use the @Figaro.PutDocumentOptions.GenerateFileName parameter, the @Figaro.Container object will use both to create the name.

Create an @Figaro.UpdateContext object. This object encapsulates the context within which the container is updated.

>[!NOTE]
> Reusing the same @Figaro.UpdateContext for a series of puts against the same container can improve your container's write performance.

Note that the content that you supply to @Figaro.Container.PutDocument is read and validated. By default, this includes any schema or DTDs that the document might reference. Since this can cause you some performance issues, you can cause Figaro to only examine the document body itself by passing the @Figaro.PutDocumentOptions.WellFormedOnly flag to @Figaro.Container.PutDocument. However, using this flag cause parsing errors if the document references information that might have come from a schema or DTD.


Further, note that while your documents are stored in the container with their shared text entities (if any) as-is, the underlying XML parser does attempt to expand them for indexing purposes. Therefore, you must make sure that any entities contained in your documents are resolvable at load time.



``` C#
using (var mgr = new XmlManager() { DefaultContainerType = XmlContainerType.WholeDocContainer })
      {
          // open the container object
          using (var cont = mgr.OpenContainer(Path.Combine(homePath,simpleContainer)))
          {
              // our sample data, as a string
              const string sampleData = "<product><item>Bananas</item><inventory><price>0.55</price><inventory>220</inventory></inventory><vendor>Simply Fresh</vendor></product>";
              // insert the string XML into the Container. Create a one-off 
              // update context for the occasion. Auto-generate a file name 
              // for the record.
              cont.PutDocument("",sampleData, mgr.CreateUpdateContext(),PutDocumentOptions.GenerateFileName);                    
          }
      }
```

To load the document from an input stream, the code is identical except that you use the appropriate method on @Figaro.XmlManager to obtain the stream. For example, to load an @Figaro.XmlDocument directly from a file on disk:



``` C#
using (var mgr = new XmlManager() { DefaultContainerType = XmlContainerType.WholeDocContainer })
    {
        using (var cont = mgr.OpenContainer(Path.Combine(homePath,simpleContainer)))
        {
            //create our local, reusable update context
            var ctx = mgr.CreateUpdateContext();

            /* Approach 1 - create the file stream first */
            // grab the file we want to load
            var inputStream =
                mgr.CreateLocalFileInputStream(Path.Combine(simpleData,"Avocado.xml"));

            // insert the file - because it already exists, auto-generate a file name
            cont.PutDocument("",inputStream, ctx, PutDocumentOptions.GenerateFileName);

            // insert the file directly from the file system
            // in this approach, the Container creates its own internal InputStream
            cont.PutDocument(Path.Combine(simpleData,"Avocado.xml"), ctx, PutDocumentOptions.GenerateFileName);

            // Prevent memory leaks by disposing of our remaining resources
            ctx.Dispose();
            inputStream.Dispose();
        }
    }
```


## See Also

* @Figaro.Container
* @Figaro.Container
* @Figaro.PutDocumentOptions.GenerateFileName

#### Other Resources
[Adding XML Documents to Containers](xref:adding-xml-documents-to-containers.md)