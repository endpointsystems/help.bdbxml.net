---
uid: performing-queries.md
---

# Performing Queries

You perform queries using an @Figaro.XmlManager object. When you perform a query, you can either:

* Perform a one-off query using @Figaro.XmlManager.Query(System.String,Figaro.QueryContext). This is useful if you are performing queries that you know you will never repeat within the process scope. For example, if you are writing a command line utility to perform a query, display the results, then shut down, you may want to use this method.
* Perform the same query repeatedly by using @Figaro.XmlManager.Prepare(System.String,Figaro.QueryContext) to obtain an @Figaro.XQueryExpression object. You can then run the query repeatedly by calling @Figaro.XQueryExpression.Execute(Figaro.QueryContext).

Regardless of how you want to run your query, you must restrict the scope of your query to a container, document, or node. Usually you use one of the XQuery navigation functions to do this. See Navigation Functions for more information.


You can indicate that the query is to be performed lazily. If it is performed lazily, then only those portions of the document that are actually required to satisfy the query are returned in the results set immediately. All other portions of the document may then be retrieved by Figaro as you iterate over and use the items in the result set.


If you are using node-level storage, then a lazy query may result in only the document being returned, but not its metadata, or the metadata but not the document itself. In this case, use @Figaro.XmlDocument.FetchAllData to ensure you have both the document and its metadata.

>[!NOTE]
>Be aware that lazy docs are different from lazy evaluation. Lazy docs determine whether all document data and document metadata is returned as a result of the query. Lazy evaluation determines how much query processing is deferred until the results set is actually examined.

``` C#
static void Main()
{
//get our manager
using (var mgr = new XmlManager(
		ManagerInitOptions.AllowExternalAccess | ManagerInitOptions.AllowAutoOpen))
	{
		//open a container
		using (var container = mgr.OpenContainer(baseUri + testdb))
		{
			Console.WriteLine("container has {0} documents.",container.GetNumDocuments());
			//add a container alias if your container is not in
			//the same directory as your application
			container.AddAlias("testdb");

			//get a query context
			QueryContext queryContext = mgr.CreateQueryContext();
			//set an XML namespace value                    
			queryContext.SetNamespace("fruits","http://groceryItem.dbxml/fruits");
			//set an XQuery variable value
			queryContext.SetVariableValue("myVar", "Bananas");
			//the query
			string xquery = "collection('testdb')/fruits:item[product=$myVar]";
			//prepare the query
			XQueryExpression expression = mgr.Prepare(xquery, queryContext);
			//execute query with lazy docs enabled
			XmlResults results = expression.Execute(queryContext, QueryOptions.LazyDocs);

			/*
			* do something with the results here
			*/

			//find another fruit
			queryContext.SetVariableValue("myVar", "Cranberry");
			results = expression.Execute(queryContext,QueryOptions.LazyDocs);

			/*
			* do something with the results here
			*/

			//find another fruit
			queryContext.SetVariableValue("myVar", "Honey Locust");
			results = expression.Execute(queryContext, QueryOptions.LazyDocs);

			/*
			* do something with the results here
			*/

			container.Close();
			
		}
	}
}
```
>[!NOTE]
Queries are case-sensitive.

Finally, note that when you perform a query, by default Figaro will read and validate the document and any attached schema or DTDs. This can cause performance problems, so to avoid it you can pass the @Figaro.QueryOptions.WellFormedOnly flag to @Figaro.XmlIndexLookup.Execute(Figaro.QueryContext). This can improve performance by causing the scanner to examine only the XML document itself, but it can also cause parsing errors if the document references information that might have come from a schema or DTD.



## In This Section
* [Metadata Based Queries](xref:metadata-based-queries.md)
