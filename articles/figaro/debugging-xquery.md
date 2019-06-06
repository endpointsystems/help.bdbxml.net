---
uid: debugging-xquery.md
---

# Debugging XQuery

Figaro provides an @Figaro.XmlDebugListener object that allows developers to listen for debug events against the query optimizer when performing XQuery operations against containers.



## Code Sample

The following sample code demonstrates the @Figaro.XmlDebugListener configuration and setup in a .NET application:



``` C#
private const string query = "for $x in collection('simpleContainer') where $x/products[category = $cat] return $x";
public void DebugListenerTest()
    {
        using (var mgr = new XmlManager(ManagerInitOptions.AllowAutoOpen))
        {
            using (var container = mgr.OpenContainer(Path.Combine(homePath, simpleContainer)))
            {
                //add an alias to the container for the query
                container.AddAlias("simpleContainer");

                using (var ctx = mgr.CreateQueryContext())
                {
                    //create a new debug listener that listens for the specified query
                    var listener = new XmlDebugListener(query);

                    //set the debug events
                    listener.Debug_Enter += listener_Debug_Enter;
                    listener.Debug_Error += listener_Debug_Error;
                    listener.Debug_Exit += listener_Debug_Exit;
                    listener.Debug_Start += listener_Debug_Start;
                    listener.Debug_Stop += listener_Debug_Stop;

                    //set the listener on the query context
                    ctx.SetDebugListener(listener);

                    //set the 'cat' query variable
                    ctx.SetVariableValue("cat", "fruits");

                    using (var results = mgr.Query(query, ctx))
                    {
                        while (results.HasNext())
                        {
                            var value = results.NextValue();
                            trace("query results : {0}", value.AsString);
                        }
                    }

                }
            }
        }
    }

    static void listener_Debug_Stop(object sender, StackFrameEventArgs args)
    {
       trace("Debug_Stop");
    }

    static void listener_Debug_Start(object sender, StackFrameEventArgs args)
    {
        trace("Debug_Start");
    }

    static void listener_Debug_Exit(object sender, StackFrameEventArgs args)
    {
        trace("Debug_Exit");
    }

    static void listener_Debug_Error(object sender, StackFrameExceptionEventArgs args)
    {
        trace("Debug_Error");
    }

    static void listener_Debug_Enter(object sender, StackFrameEventArgs args)
    {
        trace("Debug_Enter");
    }
```


## See Also


#### Reference
* @Figaro.XmlDebugListener
* [Exception Handling and Debugging](xref:exception-handling-and-debugging.md)