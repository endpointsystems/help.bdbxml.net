---
uid: benefits.md
---

# Benefits

When choosing a XML database for your application, consider all the qualities you've observed of Oracle Berkeley DB XML. Also consider the foundations of this technology; the Berkeley DB database engine is a proven, scalable transactional system with all the mature features you'd expect and likely require in your application.


## Ideal for structured data
Figaro provides a series of features that makes it more suitable for storing XML documents and data than other common storage mechanisms. Figaro's ability to provide efficient indexed queries means that it is a far more efficient storage mechanism than simply storing XML data in the file system. And because Figaro provides the same transaction protection as does Berkeley DB, it is a much safer choice than is the file system for applications that might have multiple simultaneous readers and writers of the XML data.


More, because Figaro stores XML data in its native format, Figaro enjoys the same extensible schema that has attracted many developers to XML. It is this flexibility that makes Figaro a better choice than relational database offerings that must translate XML data into internal tables and rows, thus locking the data into a relational database schema.

## XQuery and APIs

On top of this sold database foundation, Figaro offers a solid XQuery implementation that offers the ability to retrieve documents, modify existing documents, and format the query output as needed.


In addition, Figaro provides indexed queries and whole or node level document storage within containers. W3C XML schemas can be used to validate individual documents or all documents stored within containers. Schema validation is enabled per container and the schema used is specified as part of the document being stored. This provides great flexibility in how you utilize schemas, including allowing you to store XML with no associated schema.


Moreover, because Figaro is a native XML database that stores XML data in its native format, it maintains the same extensible structure that has attracted many developers to XML. It is this flexibility that makes Figaro a better choice than relational database offerings that must translate XML data into internal tables and rows, thus locking the data into a static schema while paying a heavy penalty in processing overhead when documents are reconstituted from tables and rows.