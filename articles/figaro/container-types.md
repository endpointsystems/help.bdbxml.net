---
uid: container-types.md
---

# Container Types

At creation time, every container must have a type defined for it. This container type identifies how XML documents are stored in the container. As such, the container type can only be determined at container creation time; you cannot change it on subsequent container opens.


Containers can have one of the following types specified for them:
## Wholedoc containers

The container contains entire documents; the documents are stored "as is" without any manipulation of line breaks or whitespace. To cause the container to hold whole documents, set @Figaro.XmlContainerType.WholeDocContainer on the call to @Figaro.XmlManager.CreateContainer(Figaro.ContainerConfig).

## Node containers
@Figaro.XmlDocument documents are stored as individual nodes in the container. That is, each record in the underlying database contains a single leaf node, its attributes and attribute values if any, and its text nodes, if any. Figaro also keeps the information it needs to reassemble the document from the individual nodes stored in the underlying databases.

>[!NOTE]
>This is the default, and preferred, container type.

Note that node containers are generally faster to query than `Wholedoc` containers. On the other hand, `Wholedoc` provides faster document loading times into the container than does NodeContainer because Figaro does not have to deconstruct the document into its individual leaf nodes. For the same reason, `Wholedoc` containers are faster at retrieving whole documents for the same reason — the document does not have to be reassembled.

Because of this, you should use node containers unless one of the following conditions are true:

* Load performance is more important to you than is query performance.
* You want to frequently retrieve the entire XML document (as opposed to just a portion of the document).
* Your documents are so small in size that the query advantage offered by node containers is negligible or vanishes entirely. The size at which this threshold is reached is of course dependent on the physical resources available to your .NET application (memory, CPU, disk speeds, and so forth).

>[!NOTE]
>You should avoid using `Wholedoc` containers if your documents tend to be greater than a megabyte in size. `Wholedoc` containers are tuned for small documents and you will pay increasingly heavy performance penalties as your documents grow larger.


For example:



``` C#
	    var mgr = new XmlManager();
	    mgr.DefaultContainerType = XmlContainerType.WholeDocContainer;
	    var container = mgr.CreateContainer("docContainer.dbxml");
```


## See Also

* @Figaro.XmlContainerType

#### Other Resources
* [XmlManager and Containers](xref:xmlmanager-and-containers.md)
* [Developing .NET Applications with Figaro](xref:developing-dotnet-apps-with-figaro.md)