---
uid: working-with-data-from-a-specific-document.md
---

# Working with Data from a Specific Document

Previous queries have executed against all the documents in a container, but there are cases where access to data in a single document is the goal. It is possible to isolate a single document component based on the name we assigned to it, and then perform XQuery expressions against it alone.


For example, to select the number attribute from a document named `component1` in the `components.dbxml` container:

```
dbxml> query '
doc("components.dbxml/component1")/component/@number'
1 objects returned for eager expression '
doc("components.dbxml/component1")/component/@number'

dbxml> print
{}number="1"
```
>[!NOTE]
>The `doc` XQuery function shown here can be used to access XML data external to any managed container. For instance, to integrate with a web service that returns XML over HTTP use the `doc` function to execute that web service and then use the resulting data as part of an XQuery query.

An API or microservice that can look up the price of a particular part could be knit into a HTML page as it's built in a single XQuery FLWOR expression, and this page can then be hosted under any web server. It is then possible to access that pricing data using the `doc` function in an XQuery expression.

For example, suppose you had this page, which provide the prices of the parts of our components:

```
<prices>
<part number="87">29.95</part>
<part number="89">19.95</part>
<part number="150">24.95</part>
<part number="189">5.00</part>
<part number="899">9.95</part>
<part number="901">15.00</part>
</prices>
```

And suppose this page was available from at the following location:

```
http://www.oracle.com/fakefile.html
```

In this case, we can enhance our earlier parts query to add prices for all the parts. At the same time we'll also convert it to use an HTML table to display the data.


```
dbxml> query '<html><body>
<ul>
{
for $component in collection("dbxml:components.dbxml")/component
return
<li>
<b>Component number: {$component/@number/string()}</b><br/>
<table>
{
for $part-ref in $component/uses-part
return
for $part in collection("dbxml:parts.dbxml")/part[@number =
$part-ref cast as xs:decimal]
return
<tr><td>{$part/description/string()}</td>
<td>{
doc("http://www.oracle.com/fakefile.html")//part[
@number = $part/@number]/string()
}</td></tr>
}
</table>
</li>
}
</ul>
</body></html>'
1 objects returned for eager expression '<html><body>
<ul>
{
for $component in collection("dbxml:components.dbxml")/component
return
<li>
<b>Component number: {$component/@number/string()}</b><br/>
<table>
{
for $part-ref in $component/uses-part
return
for $part in collection("dbxml:parts.dbxml")/part[@number =
$part-ref cast as xs:decimal]
return
<tr><td>{$part/description/string()}</td>
<td>{
doc("http://www.oracle.com/fakefile.html")//part[
@number = $part/@number]/string()
}</td></tr>
}
</table>
</li>
}
</ul>
</body></html>'
```

And the result with formatting for readability:


```
dbxml> print
<html>
<body>
<ul>
<li>
<b>Component number: 1</b>
<br/>
<table>
<tr>
<td>Description of 89</td>
<td>19.95</td>
</tr>
<tr>
<td>Description of 150</td>
<td>24.95</td>
</tr>
<tr>
<td>Description of 899</td>
<td>9.95</td>
</tr>
</table>
</li>
<li>
<b>Component number: 2</b>
<br/>
<table>
<tr>
<td>Description of 901</td>
<td>15.00</td>
</tr>
<tr>
<td>Description of 87</td>
<td>29.95</td>
</tr>
<tr>
<td>Description of 189</td>
<td>5.00</td>
</tr>
</table>
</li>
</ul>
</body>
</html>
```

The following shows the previous HTML as displayed in a web browser:

![The result set in HTML](/images/working-with-data.png)

This ability to bring in data from outside the containers as part of any query from a web service or other source of XML data provides tremendous power and flexibility when building applications.

#### Other Resources
* [Fiagro and XQuery](xref:figaro-and-xquery.md)
* [Working with External Functions](xref:working-with-external-functions.md)
* [Calling External Functions from XQuery](xref:calling-external-functions-from-xquery.md)
* [The dbxml Shell](xref:the-dbxml-shell.md)
* [Using FLWOR](xref:using-flwor.md)
* [Retrieving Data using XQuery](xref:retrieving-data-using-xquery.md)
* [Performing Queries](xref:performing-queries.md)