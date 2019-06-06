---
uid: project-setup.md
---

# Project Setup

In order to set up your project you will need to perform the following steps:
* Obtain a license
* Change the platform target
* Install the NuGet package
* Configure Figaro



## Licensing
In order to use Figaro in your .NET application, the first thing you will need to do is obtain a license for the library. Here's a brief overview of how the licensing works:

* For development purposes, you'll need a NuGet license. 
* For any project going into production use, you'll need to purchase a license.

Both licenses can be obtained at the [Endpoint Systems Licensing Portal](https://licensing.endpointsystems.com). If you have not done so yet you will need to register in order to obtain a license. This information is shared with Oracle for licensing purposes, which is why social media logins are disabled. Simply click on the [Register](https://licensing.endpointsystems.com/Account/SignUp) link in order to set up your account. Once you have your account enabled, you can sign in.

### Obtaining a NuGet License
![Endpoint Systems licensing page](/images/licensing-page.png)

Once you are signed in, click on the Figaro Licenses drop-down menu and select [Figaro NuGet Licensing](https://licensing.endpointsystems.com/Cart/NuGet).

![Figaro NuGet License](/images/nuget-license.png)

Select the Activate link to the product you wish to use, and you will obtain an install code that you can use in your project.

>[!NOTE]
>The install code is used to install a license on your system. This license will come from the licensing server at https://licensing.endpointsystems.net so be sure to keep communications open within your network to access that site, or you may encounter licensing install issues. 

## Setting up the NuGet package

To use the Figaro NuGet packages, it is important that you know which processing platform you are targeting. Currently the Figaro licenses support the `Win32` and `x64` platforms, and the 4.6.1 version of the .NET Framework. 

>[!NOTE]
> Custom builds supporting specific .NET Framework targets are available for commercial license holders. 
> A version supporting .NET Core is in the works.

In your .NET project, you will need to change your target platform to match the NuGet package you are downloading. For example, if you are going to use the x64 version, you will need to set up your project to target `x64`. 

>[!NOTE]
> If you do not change the platform target and leave it at `Any CPU` (the default setting) you will experience a compile error.

When you install the package, you will see the following added to your project:

Assemblies:
* Figaro.dll
* Figaro.Configuration.dll
* Desaware.Dls.Interfaces40.dll
* Desaware.MachineLicense40.dll

Files:
* Figaro.config - Figaro configuration file
* FigaroConfigSchema.xsd - the schema to the configuration file for the edition you are using
* FigaroDataManager.cs - An example on how to get started with Figaro in your application

>[!NOTE]
> The NuGet package manager will attempt to add a `section` entry in the `configSections` setting in your `app.config` or `web.config` file. The odds are good that it will not put this at the top of your configuration file, where it belongs. It's a known issue within the NuGet package manager and we're working on improving the experience.

Your configuration file should now look something like this:

```XML
<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <configSections>
    <section name="Figaro" type="Figaro.Configuration.FigaroSection, Figaro.Configuration"/>
  </configSections>
  <startup> 
        <supportedRuntime version="v4.0" sku=".NETFramework,Version=v4.6.1"/>
    </startup>
    <appSettings>
        <!-- You can specify a path to where your license key is installed. 
        By default it resides in your bin directory. -->
        <add key="figaro:licensePath" value=""/>
        <!-- You will need an install code in order for Figaro to work. 
        You can obtain one at https://licensing.endpointsystems.com  -->
        <add key="figaro:installCode" value="your key here"/>
    </appSettings>
    <Figaro configSource="Figaro.config"/>
</configuration>
```
Paste the install code you obtained from the licensing portal in the `installCode` setting. Optionally, you can add a path where you would like your licensing file to go.

## Configuring Figaro

Let's take a look at the `Figaro.config` file. This particular configuration is for the [Figaro.Samples.EncryptionAtRest](https://github.com/bdbxml/Figaro.Samples.EncryptionAtRest) application using Figaro CDS:

```XML
<Figaro xmlns="http://schemas.bdbxml.net/configuration/6.0/CDS">
    <DefaultContainerSettings allowCreate="true" compression="false" 
                              containerType="WholeDocContainer" threaded="true" statistics="On"/>    
    <Environments>
      <FigaroEnv name="demoEnv" threadCount="3">
        <!-- Caching requires InitMemoryBufferPool in the Open options. -->
        <Cache gigaBytes="1"/>
        <DataDirectories>
          <Directory create="true" path="demo\data"/>
        </DataDirectories>
        <EnvConfig>
          <ConfigItem setting="AllConcurrentDatabases" enabled="true"/>
        </EnvConfig>
        <!-- note: the 'anUnsecurePassword will throw an exception when you try to instantiate the XmlManager -->
        <!--<Encryption enabled="true" password="anUnsecurePassword"/>-->
        <Encryption enabled="true" password="asldfkjasldkgjs"/>
        <Tracing category="All" level="All" errorPrefix="err"/>
        <!-- You can add additional values to the options attribute, separated by spaces -->
        <Open create="true" home="demo\" options="InitMemoryBufferPool Create Thread ConcurrentDataStoreDefaults"/>
        
      </FigaroEnv>
    </Environments>
    <Managers>
      <XmlManager env="demoEnv" name="demoMgr" defaultContainerType="WholeDocContainer" options="AllOptions">
        <Containers>
          <Container name="demo" alias="demo" path="data\demo.dbxml"
                     allowCreate="true" allowValidation="false" checksum="false" compression="false"
                     containerType="NodeContainer" encrypted="false" exclusiveCreate="false"
                     indexNodes="On" multiVersion="false" noMMap="true" pageSize="1024"
            readOnly="false" sequenceIncrement="1" statistics="On" threaded="true"/>
        </Containers>
      </XmlManager>
    </Managers>
  </Figaro>
```
Discussing what goes into each of these settings is covered throughout the documentation, but for now it is important to know that you can configure settings here, or you can set them within your code. The @Figaro.Configuration library reads these settings and uses factory objects to create your Figaro objects for you, as seen in the FigaroDataManager:

```C#
/// <summary>
/// Initialize the Figaro data objects via Figaro.Configuration 
/// </summary>
public FigaroDataManager()
{
    //The Figaro.Configuration will create the FigaroEnv object 
    // for the XmlManager it is assigned to, so we can simply 
    // retrieve the reference to it from the manager and 
    // avoid creating multiple instances and adding additional, 
    // unnecessary reference instances. Otherwise, we'd simply 
    // create it first and assign to the manager.

    Manager = ManagerFactory.Create("demoMgr");
    Environment = Manager.Environment;

    //configure logging, progress and panic events
    Environment.OnErr += Environment_OnErr;
    Environment.OnMessage += Environment_OnMessage;
    Environment.OnProcess += Environment_OnProcess;
    Environment.OnProgress += Environment_OnProgress;
    Environment.ErrEventEnabled = true;
    Environment.MessageEventEnabled = true;
    Environment.ProcessEventEnabled = true;
    Environment.ProgressEventEnabled = true;
    
    Database = Manager.OpenContainer(ContainerConfigFactory.Create("demoMgr", "demo"));

}
```



#### See Also
[Figaro on NuGet](https://www.nuget.org/packages?q=figaro)
[Figaro.Samples.EncryptionAtRest](https://github.com/bdbxml/Figaro.Samples.EncryptionAtRest)