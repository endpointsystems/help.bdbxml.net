---
uid: input-streams-and-strings.md
---

# Input Streams and Strings

When you add a document to a container, you must identify the location where the document resides. You can do this by using:

* A string object that holds the entire document.
* An input stream that is created from a filename. Use @Figaro.XmlManager.CreateLocalFileInputStream(System.String) to create the input stream.
* Using @Figaro.Container.PutDocument(Figaro.XmlDocument,Figaro.UpdateContext), which loads the specified file as an @Figaro.XmlInputStream and inserts the stream into the container.

>[!NOTE]
>Figaro does not validate an input stream until you actually attempt to put the document to your container. This means that you can create an input stream to an invalid location or to invalid content, and Figaro will not throw an exception until you actually attempt to load data from that location.

#### Reference

* @Figaro.XmlManager.CreateLocalFileInputStream(System.String)
* @Figaro.Container.PutDocument(Figaro.XmlDocument,Figaro.UpdateContext)
* @Figaro.XmlInputStream
* @Figaro.XmlDocument.SetContentAsInputStream(Figaro.XmlInputStream)
* @Figaro.XmlManager.CreateLocalFileInputStream(System.String)
* @Figaro.XmlManager
* @Figaro.XmlInputStream
