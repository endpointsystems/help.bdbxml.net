---
uid: implementing-xmlexternalfunction.md
---

# Implementing XmlExternalFunction

@Figaro.XmlExternalFunction implementations only require you to implement the @Figaro.XmlExternalFunction.Execute(Figaro.XmlManager,Figaro.XmlArguments) method with your function code.


The `Execute` method has the following parameters:

*@Figaro.XmlTransaction* - This is the transaction in use, if any, at the time the external function was called.

>[!NOTE]
>This argument only applies to Figaro Transactional Data Store (TDS) Edition.

*@Figaro.XmlManager* - The manager instance in use at the time when the function was called.

* @Figaro.XmlArguments* - An array of @Figaro.XmlResults objects which hold the current argument values needed by this function.

For example, suppose you wanted to write an external function that takes two numbers and returns the first number to the power of the second number. It would look like this:

``` C#
	using System;
	using System.Diagnostics;
	using Figaro.Xml;
    class PowFunction: XmlExternalFunction  
    {
#if TDS
        public override XmlResults Execute(XmlTransaction txn, XmlManager mgr, XmlArguments args)
#else
            public override XmlResults Execute(XmlManager mgr, XmlArguments args)
#endif
        {
            var ret = mgr.CreateXmlResults();            
            var arg0 = args.GetArguments(0);            
            var arg1 = args.GetArguments(1);
            Trace.WriteLine("executing PowFuction");
            ret.Add(new XmlValue(Math.Pow(arg0.NextValue().AsNumber,arg1.NextValue().AsNumber)));
            return ret;
        }
    }
```


## See Also


* @Figaro.XmlTransaction
* @Figaro.XmlManager
* @Figaro.XmlExternalFunction

#### Other Resources
* [Working with External Functions](xref:working-with-external-functions.md)