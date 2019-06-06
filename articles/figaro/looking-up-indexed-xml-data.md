---
uid: looking-up-indexed-xml-data.md
---

# Looking Up Indexed XML Data


## Looking Up Indexed Documents

You can retrieve all of the values referenced by an index using an @Figaro.XmlIndexLookup object, which is returned by the @Figaro.XmlManager.CreateIndexLookup(Figaro.Container,System.String,System.String,System.String,Figaro.XmlValue,Figaro.IndexLookupOperation) method. @Figaro.XmlIndexLookup allows you to obtain an @Figaro.XmlResults object that contains all of the nodes or documents for which the identified index has keys. Whether nodes or documents is return depends on several factors:

* If your container is of type @Figaro.XmlContainerType.WholeDocContainer, then by default entire documents are always returned in this method's results set. If your container is of type @Figaro.XmlContainerType.NodeContainer then by default this method returns the nodes to which the index's keys refer. For example, every container is created with a default index that ensures the uniqueness of the document names in your container.
* URI is `http://www.sleepycat.com/2002/dbxml`.
* Node name is `name`.
* Indexing strategy is `unique-node-metadata-equality-string`.

Given this, you can efficiently retrieve every document in the container using @Figaro.XmlIndexLookup as follows:



``` C#
using (var mgr = new XmlManager(ManagerInitOptions.AllowAutoOpen))
{
    using (var container = mgr.OpenContainer(simpleContainer))
    {
        // get a query context
        QueryContext queryContext = mgr.CreateQueryContext();
        XmlValue val = new XmlValue("Bananas");
        XmlIndexLookup lookup = mgr.CreateIndexLookup(container, "",
                                                      "product",
                                                      "unique-node-element-equality-string", val,
                                                      IndexLookupOperation.Equal);
        //perform lookup with an additional option specified
        XmlResults res = lookup.Execute(queryContext, IndexLookupOptions.CacheDocuments);
        while (res.HasNext())
        {
            XmlDocument doc = res.NextDocument();
            trace("document: {0}", doc.Name);
            trace("{0}", doc);
        }
    }
}
```

In the event that you want to look up an edge index, you must provide the lookup method with both the node and the parent node that together comprise the XML edge.


For example, suppose you have the following document in your container:


``` XML
<?xml version="1.0"?>
<fruits:item xmlns:fruits="http://groceryItem.dbxml/fruits">
  <product>Bananas</product>
  <inventory>
    <sku>Banafruiqud6Kq</sku>
    <price>0.55</price>
    <inventory>220</inventory>
  </inventory>
  <vendor>Simply Fresh</vendor>
</fruits:item>
```

Further suppose you indexed the presence of the `fruits/product` edges. In this case, you can look up the values referred to by this index by doing the following:


``` C#
//get our manager
using (var mgr = new XmlManager())
{
    //open a container
    using (var container = mgr.OpenContainer(simpleContainer))
    {
        // get a query context
        QueryContext queryContext = mgr.CreateQueryContext();
        XmlValue val = new XmlValue("Banafruiqud6Kq");
        XmlIndexLookup lookup = mgr.CreateIndexLookup(container, "",
                                                      "sku",
                                                      "unique-edge-element-equality-string", val,
                                                      IndexLookupOperation.None);
        //perform lookup with an additional option specified
        lookup.SetParent("", "inventory");
        XmlResults res = lookup.Execute(queryContext, IndexLookupOptions.CacheDocuments);
        while (res.HasNext())
        {
            XmlDocument doc = res.NextDocument();
            Console.WriteLine("document: {0}", doc.Name);
            Console.WriteLine("{0}", doc.ToString());
        }
    }
}
```


## See Also
* @Figaro.XmlIndexLookup
* @Figaro.XmlContainerType
* @Figaro.XmlManager.CreateIndexLookup(Figaro.Container,System.String,System.String,System.String,Figaro.XmlValue,Figaro.IndexLookupOperation)
* [Using Indices](xref:using-indices.md)