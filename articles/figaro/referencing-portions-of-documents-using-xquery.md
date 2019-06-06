---
uid: referencing-portions-of-documents-using-xquery.md
---

# Referencing Portions of Documents Using XQuery

A document's root can always be referenced using a single forward slash:


``` XML
/
```

``` XML
/Node1
```

To reference an attribute node, prefix the attribute node's name with '@':

``` XML
/Node1/@class
```

To return the value contained in a node's text node (remember that not all element nodes contain a text node), use `distinct-values()` function:

``` XML
distinct-values(/Node1)
```

To return the value assigned to an attribute node, you also use the `distinct-values()` function:



``` XML
distinct-values(/Node1/@class)
```


## Predicates

When you provide an XQuery path, what you receive back is a result set. You can further filter this result set by using predicates. Predicates are always contained in brackets ([]) and there are two types of predicates that you can use: numeric and boolean.



#### Numeric Predicates

Numeric predicates allow you to select a node based on its position relative to another node in the document (that is, based on its context).


For example, consider the document presented in A Sample XML Document. This document contains three `<Node3>` elements. If you simply enter the XQuery expression:



``` XML
/Node1/Node2/Node3
```

All `<Node3>` elements in the document are returned. To return, say, the second `<Node3>` element, use a predicate:



``` XML
/Node1/Node2/Node3[2]
```


#### Boolean Predicates

Boolean predicates filter a query result so that only those elements of the result are kept if the expression evaluates to true. For example, suppose you want to select a node only if its text node is equal to some value. Then:



``` XML
/Node1/Node2[Node3="Node3 text 3"]
```


## Context

The meaning of an XQuery expression can change depending on the current context. Within XQuery expressions, context is usually only important if you want to use relative paths or if your documents use namespaces. However, Figaro only supports relative paths from within a predicate (see below). Also, do not confuse XQuery contexts with Figaro contexts. While Figaro contexts are related to XQuery contexts, they differ in that Figaro contexts are a data structure that allows you to define namespaces, define variables, and to identify the type of information that is returned as the result of a query (all of these topics are discussed later in this chapter).



## Relative Paths

Just like Unix file system paths, any path that does not begin with a slash (/) is relative to your current location in a document. Your current location in a document is determined by your context. Thus, if in the sample document your context is set to `Node2`, you can refer to `Node3` with the simple notation:



``` XML
Node3
```

Further, you can refer to a parent node using the following familiar notation:



``` XML
..
```

You can refer to the current node by using:



``` XML
.
```
>[!NOTE]
>Figaro supports relative paths only from within predicates.

## Namespaces

Natural language and, therefore, tag names can be imprecise. Two different tags can have identical names and yet hold entirely different sorts of information. Namespaces are intended to resolve any such sources of confusion.


Consider the following document:



``` XML
<?xml version="1.0"?>
<definition>
  <ring>
    Jewelry that you wear.
  </ring>
  <ring>
    A sound that a telephone makes.
  </ring>
  <ring>
    A circular space for exhibitions.
  </ring>
</definition>
```

As constructed, this document makes it difficult (though not impossible) to select the node for, say, a ringing telephone.


To resolve any potential confusion in your schema or supporting code, you can introduce namespaces to your documents. For example:



``` XML
<?xml version="1.0"?>
<definition>
  <jewelry:ring xmlns:jewelry="http://myDefinition.dbxml/jewelry">
    Jewelry that you wear.
  </jewelry:ring>
  <sounds:ring xmlns:sounds="http://myDefinition.dbxml/sounds">
    A sound a telephone makes.
  </sounds:ring>
  <showplaces:ring
  xmlns:showplaces="http://myDefinition.dbxml/showplaces">
    A circular space for exhibitions.
  </showplaces:ring>
</definition>
```

Now that the document has defined namespaces, you can precisely query any given node:


``` XML
/definition/sounds:ring
```
>[!NOTE]
>In order to perform queries against a document stored in Figaro that makes use of namespaces, you must declare the namespace to your query. You do this using @Figaro.QueryContext.SetNamespace(System.String,System.String). See [Defining Namespaces](xref:defining-namespaces.md) for more information.

By identifying the namespace to which the node belongs, you are declaring a context for the query.


The URI used in the namespace definition is not required to actually resolve to anything. The only requirement is that it be unique within the scope of any document set(s) in which it might be used.


Also, the namespace is only required to be declared once in the document. All subsequent usages need only use the relevant prefix. For example, we could have added the following to our previous document:



``` XML
<jewelry:diamond>
The centerpiece of many rings.
</jewelry:diamond>
<showplaces:diamond>
A place where baseball is played.
</showplaces:diamond>
```

Finally, namespaces can be used with attributes too. For an example:



``` XML
<clubMembers>
  <surveyResults school:class="English"
  xmlns:school="http://myExampleDefinitions.dbxml/school"
  number="200"/>
  <surveyResults school:class="Mathematics"
  number="165"/>
  <surveyResults social:class="Middle"
  xmlns:social="http://myExampleDefinitions.dbxml/social"
  number="543"/>
</clubMembers>
```

Once you have declared a namespace for an attribute, you can query the attribute in the following way:



``` XML
/clubMembers/surveyResults/@school:class
```

And to retrieve the value set for the attribute:



``` XML
distinct-values(/clubMembers/surveyResults/@school:class)
```


#### Wildcards

XQuery allows you to use wildcards when document elements are unknown. For example:



``` XML
/Node0/*/Node6
```

This query selects all the Node6 nodes that are 3 nodes deep in the document and whose path starts with Node0. Other wildcard matches:


Selects all of the nodes in the document:



``` XML
//*
```

Selects all of the `Node6` nodes that have three ancestors:



``` XML
/*/*/*/Node6
```

Selects all the nodes immediately beneath `Node5`:



``` XML
/Node0/Node5/*
```

Selects all of `Node5`'s attributes:



``` XML
/Node0/Node5/@*
```


## Case Insensitive Searches

It is possible to perform a case-insensitive and diacritic insensitive match using Figaro's built-in function, `dbxml:contains()`. This function takes two parameters, both strings. The first identifies the attribute or element that you want to examine, and the second provides the string you want to match.


For example, the search matches "resume", "Resume", "Resumé" and so forth:

``` XML
collection('myCollection.dbxml')/book[dbxml:contains(title, "Résumé")]
```

Note that searches performed using `dbxml:contains()` can be backed by Figaro's substring indexes.



## Navigation Functions

XQuery provides several functions that can be used for global navigation to a specific document or collection of documents. From the perspective of this manual, two of these are interesting because they have specific meaning from within the context of Figaro.



### collection()

Within XQuery, `collection()` is a function that allows you to create a named sequence. From within Figaro, however, it is also used to navigate to a specific container. In this case, you must identify to `collection()` the literal name of the container. You do this either by passing the container name directly to the function, or by declaring a default container name using the @Figaro.QueryContext.DefaultCollection  property. Note that the container must have already been opened by the @Figaro.XmlManager in order for collection to reference that container. The exception to this is if @Figaro.XmlManager was opened using the @Figaro.ManagerInitOptions.AllowAutoOpen flag. For example, suppose you want to perform a query against a container named `container1.dbxml`. In this case, first open the container using @Figaro.XmlManager.OpenContainer(System.String) and then specify the `collection()` function on the query.


For example:

``` XML
collection("container1.dbxml")/Node0
```

Note that this is actually short-hand for:



``` XML
collection("dbxml:/container1.dbxml")/Node0
```

`dbxml:/` is the default base URI for Figaro. You can change the base URI using @Figaro.QueryContext.BaseUri.


If you want to perform a query against multiple containers, use the union ("|") operator. For example, to query against containers `c1.dbxml` and `c2.dbxml`, you would use the following expression:

``` XML
(collection("c1.dbxml") | collection("c2.dbxml"))/Node0
```

See [Retrieving Data using XQuery](xref:retrieving-data-using-xquery.md) for more information on how to prepare and perform queries.



### doc()

XQuery provides the `doc()` function so that you can trivially navigate to the root of a named document. `doc()` is required to take a URI.


To use `doc()` to navigate to a specific document stored in Figaro, provide an XQuery path that uses the `dbxml:` base URI, and that identifies the container in which the document can be found. The actual document name that you provide is the same name that was set for the document when it was added to the container (see Adding Documents for more information).


For example, suppose you have a document named `mydoc1.xml` in container `container1.dbxml`. Then to perform a query against that specific document, first open `container1.dbxml` and then provide a query something like this:

``` XML
doc("dbxml:/container1.dbxml/mydoc1.xml")/Node0
```

See [Retrieving Data Using XQuery](xref:retrieving-data-using-xquery.md) for more information on how to prepare and perform queries.



## See Also

* [Using XQuery](xref:using-xquery.md)
* [Developing .NET Applications with Figaro](xref:developing-dotnet-apps-with-figaro.md)
* [Retrieving Data using XQuery](xref:retrieving-data-using-xquery.md)