---
uid: reshaping-the-result.md
---

# Reshaping the Result

XQuery is also useful when reshaping XML content. A common use for this feature is to restructure data into a display oriented dialect of XML, such as XHTML for presentation in a web browser.


Again, begin with the same value query seen earlier, modify it using XQuery and generate an XHTML version of the result suitable for display in a web browser:


```
dbxml> query '<html><body>
<ul>
{
for $part in
(collection("parts.dbxml")/part[@number > 100 and @number < 105])
return
<li>{$part/description/string()}</li>
}
</ul></body></html>'
1 objects returned for eager expression '<html><body>
<ul>
{
for $part in
(collection("parts.dbxml")/part[@number > 100 and @number < 105])
return
<li>{$part/description/string()}</li>
}
</ul></body></html>'

dbxml> print
<html><body><ul>
<li>Description of 101</li>
<li>Description of 102</li>
<li>Description of 103</li>
<li>Description of 104</li>
</ul></body></html>
```

The following shows the previous HTML as displayed in a web browser:

![Reshaping sample](/images/reshaping-sample.png)

This XQuery introduces the XQuery FLWOR expression (For, Let, While, Order by, Return — sometimes written as FLWR or FLOWR). Note that XPath is still used in the query, but now as part of the overall FLWOR structure.

>[!NOTE]
>Processing XML data in containers for display in dynamic web sites is best done using the .NET Framework API rather than the command line tool in this example.

#### Other Resources
* [Fiagro and XQuery](xref:figaro-and-xquery.md)
* [The dbxml Shell](xref:the-dbxml-shell.md)
* [Using FLWOR](xref:using-flwor.md)
* [Retrieving Data using XQuery](xref:retrieving-data-using-xquery.md)
* [Performing Queries](xref:performing-queries.md)