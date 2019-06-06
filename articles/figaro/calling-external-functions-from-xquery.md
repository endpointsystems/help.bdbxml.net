---
uid: calling-external-functions-from-xquery.md
---

# Calling External Functions from XQuery

In order to use external functions, you must register the resolver that manages them. This is done with the @Figaro.XmlManager.RegisterResolver(Figaro.XQueryResolver) method. You then set a URI prefix for the URI that you use to identify the resolver.


For example:

``` C#
	    // instantiate the manager and resolver
	    var mgr = new XmlManager(ManagerInitOptions.AllowExternalAccess);
	    var resolver = new MyFunResolver();
	    
	    // register the resolver
	    mgr.RegisterResolver(resolver);
	    // set up the query context
	    var qc = mgr.CreateQueryContext(EvaluationType.Eager);
	    qc.SetNamespace("myxfunc", resolver.Uri.ToString());
	    var results = mgr.Query(File.ReadAllText("ResolverQuery.xq"), qc);
	    if (!results.IsNull())
	        Trace.WriteLine(results.NextDocument().GetContentAsXDocument().ToString());
```

To use the external function, declare them in the preamble of your query, and then use them as you would any XQuery function (for a complete explanation of examining query results, see the next section).

For example:

```XQuery

	(: declare the external functions resolved by the resolver :)
	declare function myxfunc:sfunc($a as xs:double, $b as xs:double) as xs:double external;
	declare function myxfunc:bfunc($a as xs:double, $b as xs:double, 
		$c as xs:double, $d as xs:double, $e as xs:double) as xs:double external;
	
	(: declare variables containing the output :)
	declare variable $small := myxfunc:sfunc(2,3);
	declare variable $big := myxfunc:bfunc(1,2,3,4,5);
	
	(: return a small XML structure containing the values :)
	<ret><small>{$small}</small><big>{$big}</big></ret>
```


## See Also


#### Reference
* @Figaro.XmlExternalFunction
* @Figaro.XQueryResolver
* @Figaro.Container

#### Other Resources
* [Implementing XQueryResolver](xref:implementing-xqueryresolver.md)
* [Working with External Functions](xref:working-with-external-functions.md)
