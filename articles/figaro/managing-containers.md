---
uid: managing-containers.md
---

# Managing Containers

To create and open a container, you use @Figaro.XmlManager.CreateContainer(Figaro.ContainerConfig). Once a container has been created, you can not use @Figaro.XmlManager.CreateContainer(Figaro.ContainerConfig) on it again. Instead, simply open it using @Figaro.XmlManager.OpenContainer(Figaro.ContainerConfig).


Note that you can test for the existence of a container using the @Figaro.XmlManager.ExistsContainer(System.String) method. This method should be used on closed containers. It returns `false` if the named file is not a database. Otherwise, it returns `true`. Alternatively, you can cause a container to be created and opened by calling @Figaro.XmlManager.OpenContainer(Figaro.ContainerConfig) and pass it the necessary properties to allow the container to be created.


You can open a container multiple times. Each time you open a container, Figaro retrieves a reference-counted handle for that container.


You close a container by allowing the container object to go out of scope. Note that the container is not actually closed until the last handle for your 

@Figaro.Container objects are removed by the garbage collector.


For example:



``` C#
	using (var mgr = new XmlManager())
	{
		// create the first container
		var container1 = mgr.CreateContainer("container1.dbxml");
		
		// container2 configuration
		var cfg = new ContainerConfig
		{
		  AllowCreate = true
		};
		
		// create if the container doesn't exist
		var container2 = mgr.OpenContainer("container2.dbxml", cfg);	
	}
```


## See Also


#### Reference
* @Figaro.XmlManager.ExistsContainer(System.String)
* @Figaro.XmlManager.CreateContainer(Figaro.ContainerConfig)
* @Figaro.XmlManager.OpenContainer(Figaro.ContainerConfig)
* @Figaro.Container
* @Figaro.XmlManager.OpenContainer(Figaro.ContainerConfig)
* [XmlManager and Containers](xref:xmlmanager-and-containers.md)
