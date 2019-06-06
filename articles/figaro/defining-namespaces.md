---
uid: defining-namespaces.md
---

# Defining Namespaces

In order for you to use a namespace prefix in your query, you must first declare that namespace to Figaro. When you do this, you must identify the URI that corresponds to the prefix, and this URI must match the URI in use on your documents.

You can declare as many namespaces as are needed for your query.

To declare a namespace, use @Figaro.QueryContext.SetNamespace(System.String,System.String). For example:


``` C#
using (var mgr = new XmlManager { DefaultContainerType = XmlContainerType.WholeDocContainer })
{
    var cont = mgr.OpenContainer(Path.Combine(homePath, simpleContainer));                
    //create the query context
    var queryContext = mgr.CreateQueryContext(EvaluationType.Lazy);
    //set some namespace values
    queryContext.SetNamespace("fruits", "http://tempuri.org/groceryItem/fruits");
    queryContext.SetNamespace("vegetables", "http://tempuri.org/groceryItem/vegetables");
}
```


#### See Also
* @Figaro.QueryContext.SetNamespace(System.String,System.String)
* [Retrieving Data Using XQuery](xref:retrieving-data-using-xquery.md)