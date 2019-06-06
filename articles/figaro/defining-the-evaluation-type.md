---
uid: defining-the-evaluation-type.md
---

# Defining the Evaluation Type

The evaluation type defines how much work Figaro performs as a part of the query, and how much it defers until the results are evaluated. There are two evaluation types:

Evaluation Type | Description
--------------- | -----------
Eager | The query is executed and its resultant values are derived and stored in-memory before the query returns. This is the default.
Lazy | Minimal processing is performed before the query returns, and the remaining processing is deferred until you enumerate over the result set.

>[!NOTE]
>If you retrieve a query using lazy evaluation, you will not be able to get a count of the number of results returned - you will get an exception instead.

You can set the evaluation when creating the @Figaro.QueryContext object, or you can set the property after initialization.

``` C#
	    XmlManager mgr = new XmlManager();
	    Container myContainer = mgr.OpenContainer(@"C:\dev\db\exampleData.dbxml");
	
	    //we can set the evaluation type at initialization...
	    QueryContext queryContext = mgr.CreateQueryContext(EvaluationType.Eager);
	    //...or we can set it at design time.
	    queryContext.EvaluationType = EvaluationType.Lazy;
```

## See Also
* [Retrieving Data Using XQuery](xref:retrieving-data-using-xquery.md)