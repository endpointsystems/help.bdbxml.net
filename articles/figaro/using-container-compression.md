---
uid: using-container-compression.md
---

# Using Container Compression

By default all documents stored in Figaro @Figaro.XmlContainerType.WholeDocContainer containers are compressed when they are stored in those containers, and uncompressed when they are retrieved from those containers. This requires a little bit of overhead on document storage and retrieval, but it also saves on disk space.

>[!NOTE]
> Only XML record data is compressed; metadata and indexes are not compressed.

Container compression is used for the lifetime of the container. You cannot, for example, turn compression off for some documents in the container and leave it on for others.



## See Also


#### Other Resources
* [Managing XML Data in Containers](xref:managing-xml-data-in-containers.md)