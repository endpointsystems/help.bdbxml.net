---
uid: container-properties.md
---

# Container Properties

When you create or open a container, there are a large number of properties that you can specify which control various aspects of the container's behavior. The following are the properties commonly used by Figaro applications. For a complete listing of the properties available for use, see the @Figaro.ContainerConfig object in the API reference.

Property | Description
-------- | -----------
@Figaro.ContainerConfig.AllowCreate | Causes the container and all underlying databases to be created. It is not necessary to specify this property on the call to @Figaro.XmlManager.CreateContainer(Figaro.ContainerConfig). In addition, you need to specify it for @Figaro.XmlManager.OpenContainer(Figaro.ContainerConfig) only if the container has not already been created.
@Figaro.ContainerConfig.ExclusiveCreate | Causes the container creation to fail if the container already exists. It is not necessary to specify this property on the call to @Figaro.XmlManager.CreateContainer(Figaro.ContainerConfig). Note that this property is meaningless unless @Figaro.ContainerConfig.AllowCreate is also used.
@Figaro.ContainerConfig.ReadOnly | The container is opened for read-only access.
@Figaro.ContainerConfig.AllowValidation | Causes XML documents to be validated when they are loaded into the container. The default behavior is to not validate documents.
@Figaro.ContainerConfig.IndexNodes | Determines whether indexes for the container will return nodes (if this property is set to `On`) or documents (if this property is set to `Off`). Note that the default index type is determined by the type of container you are creating. If you are creating a container of type @Figaro.XmlContainerType.NodeContainer, then this property is set to `On` by default. For containers of type @Figaro.XmlContainerType.WholeDocContainer, this property is set to `Off` by default. If you want to change this property on an existing container, you must re-index the container in order for the new index type to take effect. For more information on index nodes, see [Using Indices](xref:using-indices.md).
@Figaro.ContainerConfig.Transactional | The container supports transactions. For more information, see the [Getting Started with Concurrent and Transactional Processing](xref:getting-started-with-concurrent-and-transactional-processing.md) guide.

>[!NOTE]
>This property is only available to Figaro Transactional Data Store (Figaro TDS) customers.

## See Also

* @Figaro.ContainerConfig

#### Other Resources
* [XmlManager and Containers](xref:xmlmanager-and-containers.md)
* [Developing .NET Applications with Figaro](xref:developing-dotnet-apps-with-figaro.md)
* [Using Indices](xref:using-indices.md)
* [Getting Started with Concurrent and Transactional Processing](xref:getting-started-with-concurrent-and-transactional-processing.md)
