---
uid: defining-variables.md
---

# Defining Variables

In XQuery FLWOR expressions, you can set variables using the `let` clause. In addition to this, you can use variables that are defined by Figaro. You define these variables using @Figaro.QueryContext.SetVariableValue(System.String,System.String).


You can declare as many variables this way as you need. Note that the variables that you declare this way can only be used from within a predicate. For example:


``` C#
var mgr = new XmlManager();
var container = mgr.OpenContainer(simpleContainer);
container.AddAlias("groceries");
var context = mgr.CreateQueryContext();

context.SetVariableValue("myVar", "Tarragon");
var query = "collection('groceries')/product[item=$myVar]";
```


#### See Also
* @Figaro.QueryContext.SetVariableValue(System.String,System.String)
* [Retrieving Data Using XQuery](xref:retrieving-data-using-xquery.md)