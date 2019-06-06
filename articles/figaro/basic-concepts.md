---
uid: basic-concepts.md
---
# Basic Concepts

Typically, Figaro is used as a library that is linked directly into your application. In addition, Figaro comes with a command line shell that allows you to work with XML documents outside of the programming languages that you normally use to interact with Figaro containers and environments. You can use the command line shell as part of your application, as a management tool, or simply as a means to explore the features of the product as we do here.
>[!NOTE]
>Remember that Figaro is an _embedded_ database engine that supports XML data and queries against that data. This means that in contrast to other database systems, Figaro is not a relational database, is not a database server, and it does not support SQL queries. Instead, it is a library that is meant to be used directly with your code and which provides XQuery queries against the XML data that you store within the engine.

## Environments
The Berkeley DB database engine is configured to run through an _environment_ object, or [FigaroEnv](xref:Figaro.FigaroEnv) in the case of Figaro. The environment is where data and log directories are set, as well as configuration options such as logging, locking, and system memory. The environment is not available as a feature in Figaro DS, which uses some default settings, but is available in the rest of the [product editions](xref:product-editions.md). 

## Containers
In Figaro, all XML data is stored within files called _containers_. [The dbxml Shell](xref:the-dbxml-shell.md) provides a simple and convenient way to work with these containers and exposes most of the database functionality in a friendly, interactive environment, without requiring the use of a programming language.


Containers are really a collection of XML documents and information about those documents. For example, containers include any [indices](xref:introducing-indices.md) that are being maintained for the documents.


Containers also store XML documents as either whole documents or as nodes. When containers store whole documents, the XML document is stored all as one unit in the container exactly as it was presented to the system. When documents are stored as nodes, the XML document is deconstructed into smaller pieces – nodes – and those small chunks are what is stored in the container.

>[!NOTE]
>The Figaro library does not break these down into nodes for you - that is a developer task, at least for the time being.

For the node storage case, retrieval of the document still returns the document in the same formatting state (assuming you didn't modify it) as it was in when it was stored in the container. The only difference is how the document is physically held within the container. Note that node storage typically offers better performance than does whole document storage, and for this reason node storage is the default container type.


## See Also

* [FigaroEnv](xref:Figaro.FigaroEnv)
* [Product Editions](xref:product-editions.md)
* [Introducing Indices](xref:introducing-indices.md)
* [Exploring Figaro and XQuery suing the dbxml Shell](xref:figaro-and-xquery.md)
* [The dbxml Shell](xref:the-dbxml-shell.md)
