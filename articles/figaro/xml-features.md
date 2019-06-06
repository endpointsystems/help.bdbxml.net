---
uid: xml-features.md
---

# XML Features

The database engine is designed to conform to the W3C standards for XML, XML Namespaces, and the latest available XQuery standards, in addition to the evolution of the .NET Framework itself. The following additional features specifically designed to support XML data management and queries go above and beyond any existing standard, and serve to set Figaro further ahead of similar solutions:


## Containers

A container is a single file that contains one or more XML documents, and their metadata and indices. You use containers to add, delete, and modify documents, and to manage indices.


## Indices

Figaro indices greatly enhance the performance of queries against the corresponding XML data set. Figaro indices are based on the structure of your XML documents, and as such you declare indices based on the nodes that appear in your documents as well the data that appears on those nodes.
>[!NOTE]
>You can also declare indices against metadata. See [Using Indices](xref:using-indices.md) for more information.

## Queries

Figaro XQuery queries are performed using the XQuery 3.0 language. XQuery is a W3C  specification; for more information, visit the specification website at http://www.w3.org/XML/Query .


## Query Results

Figaro retrieves documents that match a given XQuery query or API function call. Figaro query results are always returned as a set, typically of the [XmlResults](xref:Figaro.XmlResults) data type. The set can contain either matching documents, or a set of values from those matching documents.


## Storage

If you use node-level storage for you documents (see [Container Types](xref:container-types.md)), then Figaro automatically trans codes your documents to Unicode UTF-8. If you use whole document storage, then the document is stored in whatever encoding that it uses. Note that in either case, your documents must use an encoding supported by the Xerces XML parser (see [Library Dependencies](xref:library-dependencies.md) before they can be stored in Figaro containers.

>[!NOTE]
>Beyond the encoding, documents are stored (and retrieved) in their native format with all whitespace preserved.

## Metadata Attribute Support

Each document stored in Figaro can have metadata attributes associated with it. This allows information to be associated with the document without actually storing that information in the data itself. For example, metadata attributes might identify the last accessed and last modified timestamps for the document. See [Using Metadata](xref:using-metadata.md) for more information and examples.



## Document Modification

Figaro provides robust API and XQuery mechanisms for modifying documents. Using this mechanism, you can add, replace, and delete nodes from your document. This mechanism allows you to modify both element and attribute nodes, as well as processing instructions and comments. See [Modifying Documents](xref:modifying-documents.md) for more information and examples.


## See Also


#### Other Resources
* [Using Indices](xref:using-indices.md)
* [Container Types](xref:container-types.md)
* [Library Dependencies](xref:library-dependencies.md)
* [Modifying Documents](xref:modifying-documents.md)
* [Using Metadata](xref:using-metadata.md)
* http://www.w3.org/XML/Query