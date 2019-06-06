---
uid: which-handles-are-free-threaded.md
---

# Which Handles are Free-Threaded?

The following table lists what Figaro handles are free-threaded.

Figaro Object | Free Threaded?
------------- | --------------
@Figaro.FigaroEnv <sup>1</sup> | ![yes](/images/checked.png)
@Figaro.XmlManager | ![yes](/images/checked.png)
@Figaro.Container | ![yes](/images/checked.png)
@Figaro.XmlDebugListener <sup>2</sup> | ![yes](/images/checked.png)
@Figaro.XmlDocument | ![no](/images/unchecked.png)
@Figaro.XmlIndexSpecification | ![no](/images/unchecked.png)
@Figaro.XmlExternalFunction <sup>3</sup> | ![no](/images/unchecked.png)
@Figaro.XmlMetadataIterator | ![no](/images/unchecked.png)
@Figaro.QueryContext | ![no](/images/unchecked.png)
@Figaro.XQueryExpression | ![yes](/images/checked.png)
@Figaro.XQueryResolver <sup>4</sup> | ![no](/images/unchecked.png)
@Figaro.XmlResults | ![no](/images/unchecked.png)
@Figaro.KeyStatistics | ![no](/images/unchecked.png)
@Figaro.XmlTransaction <sup>5</sup> | ![no](/images/unchecked.png)
@Figaro.UpdateContext | ![no](/images/unchecked.png)
@Figaro.XmlValue | ![no](/images/unchecked.png)

1. Free-threaded so long as the @Figaro.EnvOpenOptions.Thread flag is provided to the @Figaro.FigaroEnv.Open(System.String,Figaro.EnvOpenOptions) method.
2. Implemented by your application, so whether this class can be shared across multiple threads is up to your local implementation.
3. If @Figaro.XQueryResolver.ResolveExternalFunction(Figaro.XmlTransaction,Figaro.XmlManager,System.String,System.String,System.UInt64) returns a new object, then it is not free-threaded. However, if the application is multi-threaded and @Figaro.XQueryResolver.ResolveExternalFunction(Figaro.XmlTransaction,Figaro.XmlManager,System.String,System.String,System.UInt64) returns a shared instance, then it is free-threaded.
4. If an application uses multiple threads, custom implementations of @Figaro.XQueryResolver must be free threaded to allow multiple, simultaneous calls for resolution.
5. Access must be serialized by the application across threads of control.

## See Also

* @Figaro.XmlManager
* @Figaro.FigaroEnv
* @Figaro.EnvOpenOptions.Thread
* @Figaro.Container
* @Figaro.XmlTransaction
* @Figaro.Container
* @Figaro.Container
* @Figaro.XmlDebugListener
* @Figaro.Container
* @Figaro.XmlDocument
* @Figaro.XmlIndexSpecification
* @Figaro.XmlExternalFunction
* @Figaro.XmlExternalMetadataIterator
* @Figaro.QueryContext
* @Figaro.XQueryExpression
* @Figaro.XQueryResolver
* @Figaro.XmlResults
* @Figaro.KeyStatistics
* @Figaro.XmlTransaction
* @Figaro.Container 
* @Figaro.XmlValue
* @Figaro.EnvOpenOptions.Thread
* @Figaro.Container
* @Figaro.FigaroEnv.Open(System.String,Figaro.EnvOpenOptions)
* @Figaro.XQueryResolver.ResolveExternalFunction(Figaro.XmlTransaction,Figaro.XmlManager,System.String,System.String,System.UInt64)

#### Other Resources

* [Concurrency](xref:concurrency.md)
* [Getting Started with Concurrent and Transactional Processing](xref:getting-started-with-concurrent-and-transactional-processing.md)
