---
uid: examining-metadata.md
---

# Examining Metadata

When you retrieve a document from Figaro, there are two ways to examine the metadata associated with that document. The first is to use @Figaro.XmlDocument.GetMetaData. Use this form if you want to examine the value for a specific metadata value.


The second way to examine metadata is to obtain an @Figaro.XmlMetadataIterator object using Figaro.XmlDocument.GetMetadataIterator. You can use this mechanism to loop over and display every piece of metadata associated with the document.


For example:

``` C#
	    var cfg = new ContainerConfig
	                  {
	                      AllowCreate = true
	                  };
	    // get a manager object
	    var mgr = new XmlManager(ManagerInitOptions.AllowAutoOpen);
	    var uc = mgr.CreateUpdateContext();
	
	    // open a container
	    var container = mgr.OpenContainer(nsContainer, cfg);
	    
	    // create a query context. 
	    var context = mgr.CreateQueryContext();
	    // add an alias, as we've opened a container with an absolute path
	    container.AddAlias("groceries");
	    // declare a namespace
	    context.SetNamespace("fruits", "http://groceryItem.dbxml/fruits");
	
	    // declare the query string. Find all the product documents 
	    // in the fruits namespace.
	    const string myQuery = "collection('groceries')/fruits:item";
	
	    // declare a namespace.
	    context.SetNamespace("fruits", "http://groceryItem.dbxml/fruits");
	
	    // Perform the query.
	    XmlResults results = mgr.Query(myQuery, context);
	
	    // Display the result set
	    while (results.HasNext())
	    {
	        XmlDocument theDoc = results.NextDocument();
	
	        // Display all of the metadata set for this document
	        XmlMetadataIterator mdi = theDoc.GetMetadataIterator();
	        trace("for document {0} found metadata:", theDoc.Name);
	
	        while (mdi.Next())
	        {
	            trace("URI: {0}, name: {1}, value: {2}", mdi.Uri, mdi.Name, mdi.Value.ToString());
	        }
	    }
```


## See Also

* @Figaro.XmlMetadataIterator
* @examining-query-results.md