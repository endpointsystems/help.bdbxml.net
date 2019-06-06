---
uid: opening-and-closing-environments.md
---

# Opening and Closing Environments

To use an environment, you must first open it. At open time, you must identify the directory in which it resides and this directory must exist prior to the open attempt. At open time, you also specify the open flags, properties, if any, that you want to use for your environment.


When you are done with the environment, you must make sure it is closed. You can either do this explicitly, or you can have the @Figaro.XmlManager object do it for you.


If you are explicitly closing your environment, you must make sure an containers opened in the environment have been closed before you close your environment.


For information on @Figaro.XmlManager instantiation, see [XmlManager Instantiation and Destruction](xref:xmlmanager-instantiation-and-destruction.md). For example:


``` C#
using (FigaroEnv env = new FigaroEnv())
	    {
	        try
	        {
	            //specify the environment subsystems you want enabled
	            const EnvOpenOptions flags = EnvOpenOptions.UseEnvironment |
	                EnvOpenOptions.Create |
	                EnvOpenOptions.InitLock |
	                EnvOpenOptions.InitLog;
	
	            //open the environment directory
	            env.Open(homePath, flags);
	            //close the environment after using it. the using scope will dispose.
	            env.Close();
	        }
	        catch (DatabaseException de)
	        {
	            trace("DatabaseException caught. Error code: {0}, message: {1}", de.ErrorCode , de.Message);
	        }
	        catch (XmlException de)
	        {
	            trace("XmlException caught. Error code: {0}, message: {1}", de.ErrorCode, de.Message);
	        }
	        catch (FigaroException de)
	        {
	            trace("FigaroException caught. Error code: {0}, message: {1}", de.ErrorCode, de.Message);
	        }
	    }
```


## See Also

* @Figaro.XmlManager
* @Figaro.FigaroEnv
* @Figaro.XmlManager.OpenContainer(Figaro.ContainerConfig)
* @Figaro.FigaroEnv.Open(System.String,Figaro.EnvOpenOptions)

#### Other Resources
* [XML Manager and Environments](xref:xml-manager-and-environments.md)