---
uid: metadata-based-queries.md
---

# Metadata Based Queries

You can query for documents based on the metadata that you set for them. To do so, do the following:

* Define a namespace for the query that uses the URI that you set for the metadata against which you will perform the query. If you did not specify a namespace for your metadata when you added it to the document, use an empty string.
* Perform the query using the special `dbxml:metadata()` from within a predicate.

For example, suppose you placed a timestamp in your metadata using the URI 'http://dbxmlExamples/timestamp' and the attribute name 'timeStamp'. Then you can query for documents that use a specific timestamp as follows:



``` C#
//get our manager
using (var mgr = new XmlManager(ManagerInitOptions.AllowExternalAccess | ManagerInitOptions.AllowAutoOpen))
{
//open a container
using (var container = mgr.OpenContainer(baseUri + testdb))
{
Console.WriteLine("container has {0} documents.", container.GetNumDocuments());
// add a container alias if your container is not in
// the same directory as your application
container.AddAlias("testdb");
// get a query context
QueryContext queryContext = mgr.CreateQueryContext();
// Declare a namespace. The first argument, 'ts', is the
// namespace prefix and in this case it can be anything so
// long as it is not reused with another URI within the same
// query.
queryContext.SetNamespace("ts","http://dbxmlExamples/timestamp");
// the query - note the use of the 'timeStamp'
// metadata variable name
string xquery = "collection('testdb')/*[dbxml:metadata('ts:timeStamp')='00:12:01']";
// prepare the query
XQueryExpression expression = mgr.Prepare(xquery, queryContext);
// execute the query
XmlResults results = expression.Execute(queryContext);
container.Close();
}
}
```
>[!NOTE]
Queries are case-sensitive.
