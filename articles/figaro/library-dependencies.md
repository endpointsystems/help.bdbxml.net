---
uid: library-dependencies.md
---

# Library Dependencies

The Figaro .NET assembly depends on several external native libraries. The result is that build instructions for Figaro may change from release to release as its dependencies mature.


Figaro currently relies on the following libraries:
* [Oracle Berkeley DB](http://www.oracle.com/technology/documentation/berkeley-db/db/index.html) is the database engine for Figaro. 
* [Apache Xerces](http://xerces.apache.org/xerces-c/index.html) is the XML parsing engine providing DOM and SAX support that Figaro employs for XML data parsing. 
* The [XQilla XQuery engine](http://xqilla.sourceforge.net/HomePage) is an extensible query markup engine that has language extensions available in Figaro to allow for XQuery extension methods using .NET code. 
* [Desaware Licensing System](http://desaware.com/products/licensingsystem/) for managing the licensing of the database engine.

>[!NOTE]
>Figaro is built as a multi-file .NET assembly. This means that all of its immediate dependencies are linked to each other. You'll notice this the most if you add the Figaro assembly to the Global Assembly Cache; if you look inside the folder created in the GAC you will these assemblies registered with the original Figaro assembly.

## See Also
* [Oracle Berkeley DB](http://www.oracle.com/technology/documentation/berkeley-db/db/index.html)
* [Apache Xerces](http://xerces.apache.org/xerces-c/index.html)
* [XQilla XQuery engine](http://xqilla.sourceforge.net/HomePage) 
* [Desaware Licensing System](http://desaware.com/products/licensingsystem/)
* [Endpoint Systems Licensing Portal](https://licensing.endpointsystems.com)
