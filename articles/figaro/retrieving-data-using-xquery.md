---
uid: retrieving-data-using-xquery.md
---

# Retrieving Data Using XQuery

Documents are retrieved from Figaro when they match an XQuery path expression. Queries are either performed or prepared using an @Figaro.XmlManager object, but the query itself usually restricts its scope to a single container or document using one of the XQuery Navigation Functions.

When you perform a query, you must provide:

* The XQuery expression to be used for the query contained in a single string object.
* A @Figaro.QueryContext object that identifies contextual information about the query, such as the namespaces in use and what you want for results (entire documents, or document values).

What you then receive back is a result set that is returned in the form of an @Figaro.XmlResults object. You iterate over this result sets in order to obtain the individual documents or values returned as a result of the query.



## In This Section
* [The Query Context](xref:the-query-context.md)
* [Defining Namespaces](xref:defining-namespaces.md)
* [Defining Variables](xref:defining-variables.md)
* [Defining the Evaluation Type](xref:defining-the-evaluation-type.md)

## See Also
@Figaro.XmlManager