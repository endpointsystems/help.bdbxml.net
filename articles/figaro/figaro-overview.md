---
uid: figaro-overview.md
---

# Figaro Overview

Figaro is the framework library that extends Oracle Berkeley DB XML for .NET developers. Oracle Berkeley DB XML is an embedded database that is tuned for managing and querying hundreds, thousands, or even millions of XML documents. You use the Figaro .NET Framework API to manage, query, and modify your documents via an in-process database engine. Because it is an embedded database engine, you compile and link it into your application in the same was as you would any third-party library.

In Figaro, documents are stored in containers, which you create and manage using [XmlManager](xref:Figaro.XmlManager) objects. Each manager object can open and manage multiple containers at a time.

Each container can hold millions of documents. For each document placed in a container, the container holds all the document data, any metadata that you have created for the document, and any indices maintained for the documents in the container.

(Metadata is information that you associate with your document that might not readily fit into the document schema itself. For example, you might use metadata to track the last time a document was modified instead of maintaining that information from within the actual document.)


XML documents may be stored in Figaro containers in one of two ways:

* **Whole Documents**. Documents are stored in their entirety. This method works best for smaller documents (under 1MB in size).
* **Document nodes**. Documents as nodes are broken down into their individual document element nodes and each such node is then stored as an individual record in the container. Along with each such record, Figaro also stores all node attributes, and the text node, if any.

From an API-usage perspective, there are very few differences between whole document and node storage containers. For more information, see [Container Types](xref:container-types.md).

Once a document has been placed in a container, you can use XQuery to retrieve one or more documents. You can also use XQuery to retrieve one or more portions of one or more documents. Queries are performed using [XmlManager](xref:Figaro.XmlManager) objects. The queries themselves, however, limit the scope of the query to a specified list of containers or documents.


Figaro supports the entire XQuery specification. You can read the specification here: https://www.w3.org/TR/2014/REC-xquery-30-20140408/ . Because XQuery is an extension of XPath 2.0, XPath is also fully supported.


Finally, Figaro provides a robust document modification facility that allows you to easily add, delete, or modify selected portions of XML data. This means you can avoid writing modification code that manipulates (for example) DOM trees — Figaro can handle all those details for you.

## See Also

#### Other Resources
* [Introducing Figaro](xref:intro.md)
* [XQuery Specification](https://www.w3.org/TR/2014/REC-xquery-30-20140408//)
* [Container Types](xref:container-types.md)
* [XmlManager](xref:Figaro.XmlManager)
* [Oracle Berkeley DB XML Product Website](http://www.oracle.com/technology/documentation/berkeley-db/xml/index.html) 