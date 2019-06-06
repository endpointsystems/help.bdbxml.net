---
uid: berkeley-db-environments.md
---

# Berkeley DB Environments

Before you can instantiate an @Figaro.XmlManager object, you have to make some decisions about your environment. Figaro requires you to use a database environment. You can use an environment explicitly, or you can allow the @Figaro.XmlManager constructor to manage the environment for you.


If you explicitly create an environment, then you can turn on important features in Figaro such as logging, transactional support, and support for multithreaded and multiprocess applications. It also provides you with an on-disk location to store all of your application's containers.


If you allow the @Figaro.XmlManager constructor to implicitly create and/or open an environment for you, then the environment is only configured to allow multithreaded sharing of the environment and the underlying databases (@Figaro.EnvOpenOptions.Private is used). All other features are not enabled for the environment.


The next several sections describe the things you need to know in order to create and open an environment explicitly. We start with this activity first because it is likely to be the first thing you will do for all but the most trivial of Figaro applications.



## See Also

#### Reference
@Figaro.XmlManager@Figaro.FigaroEnv

#### Other Resources
* [Environment Open Flags](xref:environment-open-flags.md)
* [Opening and Closing Environments](xref:opening-and-closing-environments.md)
* [XmlManager Instantiation and Destruction](xref:xmlmanager-instantiation-and-destruction.md)
* [XML Manager and Environments](xref:xml-manager-and-environments.md)