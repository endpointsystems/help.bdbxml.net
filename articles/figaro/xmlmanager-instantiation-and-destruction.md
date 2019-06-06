---
uid: xmlmanager-instantiation-and-destruction.md
---

# XmlManager Instantiation and Destruction

You create an @Figaro.XmlManager object by calling its constructor. You destroy a @Figaro.XmlManager object by calling its destructor, either by using the delete operator or by allowing the object to go out of scope (if it was created on the stack). Note that @Figaro.XmlManager is closed and all of its resources released when the last open handle to the object is destroyed.

To construct an @Figaro.XmlManager object, you may or may not provide the destructor with an open @Figaro.FigaroEnv object. If you do instantiate @Figaro.XmlManager with an opened environment handle, then @Figaro.XmlManager will close and destroy that @Figaro.FigaroEnv object for you if you specify @Figaro.ManagerInitOptions.AdoptFigaroEnv for the @Figaro.XmlManager constructor.


If you provide a @Figaro.FigaroEnv object to the constructor, then you can use that object to use whatever subsystems that you application may require (see [Environment Open Flags](xref:environment-open-flags.md) for some common subsystems).


If you do not provide an environment object, then @Figaro.XmlManager will implicitly create an environment for you. In this case, the environment will not be configured to use any subsystems and it is only capable of being shared by multiple threads from within the same process. Also, in this case you must identify the on-disk location where you want your containers to reside using one of the following mechanisms:
Specify the path to the on-disk location in the container's name.Specify the environment's data location using the `DB_HOME` environment variable.
In either case, you can pass the @Figaro.XmlManager constructor a @Figaro.ManagerInitOptions argument that controls that object's behavior with regard to the underlying containers (the flag is NOT passed directly to the underlying environment or databases). Valid values are:

* @Figaro.ManagerInitOptions.AllowAutoOpen
* @Figaro.ManagerInitOptions.AdoptFigaroEnv
* @Figaro.ManagerInitOptions.AllowExternalAccess

For example, to instantiate @Figaro.XmlManager with a default environment: 

``` C#
using (var mgr = new XmlManager())
{
    //do something here
}
```
 And to instantiate an XmlManager using an explicit environment object: 

``` C#
const EnvOpenOptions flags = EnvOpenOptions.Create |
                              EnvOpenOptions.InitLock |
                              EnvOpenOptions.InitLog |
                              EnvOpenOptions.InitMemoryBufferPool |
                              EnvOpenOptions.InitTransaction |
                              EnvOpenOptions.Recover;
var env = new FigaroEnv();

env.Open(homePath, flags);
const ManagerInitOptions mgrFlags = ManagerInitOptions.AdoptFigaroEnv |
                                ManagerInitOptions.AllowAutoOpen |
                                ManagerInitOptions.AllowExternalAccess;
var mgr = new XmlManager(env, mgrFlags);
```

## See Also

* @Figaro.XmlManager
* @Figaro.ManagerInitOptions
* [XML Manager and Environments](xref:xml-manager-and-environments.md)