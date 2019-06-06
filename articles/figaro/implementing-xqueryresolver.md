---
uid: implementing-xqueryresolver.md
---

# Implementing XQueryResolver

The @Figaro.XQueryResolver class is used to provide a handle to the appropriate external function, when a given XQuery statement requires an external function. For this reason, your @Figaro.XQueryResolver implementation must have knowledge of every external function you have implemented.


The resolver is responsible for instantiating an instance of the required external function. It is also responsible for destroying that instance, either once the query has been process or when the resolver instance itself is being destroyed. Which is the correct option for your application is an implementation detail that is up to you.


It is possible for your code to have multiple instances of an @Figaro.XQueryResolver class, each instance of which can potentially be responsible for different collections of external functions. For this reason, you uniquely identify each resolver class with a URI.

In order to call a specific external function, your XQueries must provide a URI as identification, as well as a function name. You can decide which external function to return based on the URI, the function name, and/or the number of arguments provided in the XQuery. Which of these are necessary for you to match the correct external function is driven by how many external functions you have implemented, how many resolver classes you have implemented, and how many variations on functions with the same name you have implemented. In theory, a very simple implementation could return an external function instance based only on the function name. Other implementation may need to match based on all possible criteria.


For the absolute most correct and safest implementation, you should match on all three criteria: URI, function name, and number of arguments.


For example, suppose you had two external functions: `SmallFunction` and `BigFunction`. `SmallFunction` is a small function that requires few resources to instantiate and is called infrequently; `BigFunction` is a larger function that opens containers, obtains lots of memory and from a performance perspective is something that is best instantiated once and then not destroyed until program shutdown. Further, `SmallFunction` takes two arguments while `BigFunction` takes five.


An @Figaro.XQueryResolver implementation for this example would be as follows:

``` C#
/// <summary>
/// Resolve our small and large functions
/// </summary>
public class MyFunResolver : XQueryResolver
{
    public MyFunResolver() : base(new Uri("my://my.fun.resolver"))
    {            
    }

    public override bool ResolveCollection(XmlTransaction txn, XmlManager mgr, string uri, XmlResults collection)
    {
        return false;
    }

    public override XmlDocument ResolveDocument(XmlTransaction txn, XmlManager mgr, string uri)
    {
        return null;
    }

    public override XmlInputStream ResolveEntity(XmlTransaction txn, XmlManager mgr, string systemId, string publicId)
    {
        return null;
    }

    public override XmlExternalFunction ResolveExternalFunction(XmlTransaction txn, XmlManager mgr, string uri, string name, uint numArgs)
    {
        if (uri != Uri.ToString()) return null;
        switch (numArgs)
        {
            case 5:
                Trace.WriteLine("resolving big function...");
                return new BigFunction();
            case 2:
                Trace.WriteLine("resolving small function...");
                return new SmallFunction();
        }

        return null;

    }

    public override XmlInputStream ResolveModule(XmlTransaction txn, XmlManager mgr, string moduleLocation, string nameSpace)
    {
        return null;
    }

    public override bool ResolveModuleLocation(XmlTransaction txn, XmlManager mgr, string nameSpace, XmlResults moduleLocations)
    {
        return false;
    }

    public override XmlInputStream ResolveSchema(XmlTransaction txn, XmlManager mgr, string schemaLocation, string nameSpace)
    {
        return null;
    }
}
```


## See Also

* @Figaro.XQueryResolver
* @Figaro.Container
* @Figaro.XmlExternalFunction

#### Other Resources
* [Working with External Functions](xref:working-with-external-functions.md)
* [Implementing XmlExternalFunction](xref:implementing-xmlexternalfunction.md)
