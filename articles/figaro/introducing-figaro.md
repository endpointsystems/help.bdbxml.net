---
uid: introducing-figaro.md
---

# Introducing Figaro

The Figaro XML Database is an embedded, native XML database specifically designed for the storage and retrieval of XML-formatted documents and data using the Microsoft .NET Framework. Built on the award-winning Berkeley DB database library, Figaro supports efficient queries against millions of XML documents using XQuery 1.0. XQuery is a W3C query language designed for the examination and retrieval of portions of XML documents.

![product stack](/images/ProductStack.png)

**Figaro and the Oracle Berkeley DB Product Family**

This section introduces the Figaro database layer and provides a walkthrough of some of its features using the [dbxml](xref:the-dbxml-shell.md) command-line shell. It is a high-level overview of the system that provides a basic understanding of what the system does and how it might be useful to your project. This document is not a detailed tutorial or reference guide, so we will omit technical detail, emphasizing what things can be done with Figaro. To get the most out of this documentation, you should be familiar with the basics of XML and XQuery. This guide is written so that you can follow along using the `dbxml` shell to run the examples and become familiar Figaro's capabilities.

>[!NOTE]
>This documentation is based on Oracle's "Introduction to Berkeley DB XML" documentation. Its content has been tailored to Microsoft .NET developers who are unfamiliar with Oracle Berkeley DB and Berkeley DB XML (BDB XML). While Figaro is referenced in the same context as the Oracle Berkeley DB XML product, please keep in mind that Figaro is an extension that sits atop the product, and is not a "pure" replacement to the product itself.

## See Also
* [The dbxml Shell](xref:the-dbxml-shell.md)