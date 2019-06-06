---
uid: examining-indices.md
---

# Examining Indices


## Examining Container Indices

You can iterate over all the indices in a container using @Figaro.XmlIndexSpecification.Next. All indices retrieved come back in an @Figaro.XmlIndex object containing the node namespace, node name, and indexing strategy.


``` C#
using (var mgr = new XmlManager(ManagerInitOptions.AllowExternalAccess | ManagerInitOptions.AllowAutoOpen))
	  {
	      using (var container = mgr.OpenContainer(simpleContainer))
	      {
	          XmlIndexSpecification spec = container.GetIndexSpecification();
	          XmlIndex xi = spec.Next();
	          while (null != xi)
	          {
	              Console.WriteLine("index found: '{0}', '{1}', '{2}'", xi.Index, xi.NodeName, xi.Namespace);
	              xi = spec.Next();
	          }
	      }
	  }
```


## See Also

* @using-indices.md
