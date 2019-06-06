---
uid: examining-document-values.md
---

# Examining Document Values

It is frequently useful to retrieve a document from Figaro and then perform follow-on queries to retrieve individual values from the document itself. You do this by creating and executing a query, except that you pass the specific @Figaro.XmlValue object that you want to query to the @Figaro.XQueryExpression.Execute method. You must then iterate over a result set exactly as you would when retrieving information from a container.

For example, suppose you have an address book product that manages individual contacts using XML documents such as:


``` XML
<contact>
  <familiarName>John</familiarName>
  <surname>Doe</surname>
  <phone work="555 555 5555" home="555 666 777" />
  <address>
    <street>1122 Somewhere Lane</street>
    <city>Nowhere</city>
    <state>Minnesota</state>
    <zipcode>11111</zipcode>
  </address>
</contact>
```

Then you could retrieve individual documents and pull data off of them like this:

``` C#
        private const string data = "<contact><familiarName>John</familiarName><surname>Doe</surname><phone work=\"555 555 5555\" home=\"555 666 777\" /><address><street>1122 Somewhere Lane</street><city>Nowhere</city><state>Minnesota</state><zipcode>11111</zipcode></address></contact>";

	    var mgr = new XmlManager(ManagerInitOptions.AllowAutoOpen);
	    var cfg = new ContainerConfig
	                  {
	                      AllowCreate = true,
	                      Alias = "contacts"
	                  };
	    var container = mgr.OpenContainer("", cfg);
	    var doc = mgr.CreateDocument();
	    doc.SetContent(data);
	    doc.Name = "test";
	    container.PutDocument(doc, mgr.CreateUpdateContext());
	
	    // Declare the query string. Retrieves all the documents
	    // for people with the last name 'Doe'.
	    const string myQuery = "collection()/contact";
	
	    // Query to get the familiar name from the
	    // document.
	    const string fn = "distinct-values(collection()/contact/familiarName)";
	
	    // Query to get the surname from the
	    // document.
	    const string sn = "distinct-values(collection()/contact/surname)";
	
	    // Work phone number
	    const string wrkPhone = "distinct-values(collection()/contact/phone/@work)";
	
	    // Get the context for the XmlManager query
	    QueryContext managerContext = mgr.CreateQueryContext();
	    managerContext.DefaultCollection = "";
	    // Get a context for the document queries
	    QueryContext documentContext = mgr.CreateQueryContext();
	    documentContext.DefaultCollection = "";
	
	    // Prepare the XmlManager query
	    XQueryExpression managerQuery = mgr.Prepare(myQuery, managerContext);
	    
	    // Prepare the individual document queries
	    XQueryExpression fnExpr = mgr.Prepare(fn, documentContext);
	    XQueryExpression snExpr = mgr.Prepare(sn, documentContext);
	    
	    XQueryExpression wrkPhoneExpr =
	        mgr.Prepare(wrkPhone, documentContext);
	
	    // Perform the query.
	    XmlResults results = managerQuery.Execute(managerContext, 0);
	
	    // Display the result set
	    while (results.HasNext())
	    {
	        // Get the individual values
	        var value = results.NextValue();
	        XmlResults fnResults = fnExpr.Execute(value, documentContext, QueryOptions.None);
	        XmlResults snResults = snExpr.Execute(value, documentContext, QueryOptions.None);
	        XmlResults phoneResults = wrkPhoneExpr.Execute(value, documentContext, QueryOptions.None);
	
	        if (fnResults.HasNext() &amp;&amp; snResults.HasNext() &amp;&amp; phoneResults.HasNext())
	        {
	            trace("fnResults: {0}, snResults: {1}, phoneResults: {2}",
	                  fnResults.NextValue().AsString,
	                  snResults.NextValue().AsString,
	                  phoneResults.NextValue().AsString);
	        }
	            continue;
	    }
```

Note that you can use the same basic mechanism to pull information out of very long documents, except that in this case you need to maintain the query's focus; that is, the location in the document that the result set item is referencing. For example suppose you have a document with 2,000 contact nodes and you want to get the name attribute from some particular contact in the document.


There are several ways to perform this query. You could, for example, ask for the node based on the value of some other attribute or element in the node:


``` XML
/document/contact[category='personal']
```

Or you could create a result set that holds all of the document's contact nodes:


``` XML
/document/contact
```

Regardless of how you get your result set, you can then go ahead and query each value in the result set for information contained in the value. To do this:

Iterate over the result set as normal.
Query for document information as described above.
However, in this case change the query so that you reference the self access. That is, for the surname query described above, you would use the following query instead so as to reference nodes relative to the current node (notice the self-access (`.`) in use in the following query):


``` XML
distinct-values(./surname)
```

## See Also

@examining-query-results.md