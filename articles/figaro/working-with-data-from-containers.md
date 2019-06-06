---
uid: working-with-data-from-containers.md
---

# Working with Data from Containers

An application may use one or more containers. Figaro and XQuery provide excellent support for this situation. First, create a second container and add some additional data. A few simple documents will be enough to demonstrate this feature. To begin, we add them the new container:


```
dbxml> createContainer components.dbxml

Creating node storage container with nodes indexed

dbxml> putDocument component1 '<component number="1">
<uses-part>89</uses-part>
<uses-part>150</uses-part>
<uses-part>899</uses-part>
</component>'

Document added, name = component1

dbxml> putDocument component2 '<component number="2">
<uses-part>901</uses-part>
<uses-part>87</uses-part>
<uses-part>189</uses-part>
</component>'

Document added, name = component2

dbxml> preload parts.dbxml

dbxml> preload components.dbxml
```

These new nodes are intended to represent a larger component consisting of several of the parts defined earlier. To output an HTML view of all the components and their associated parts across containers, use:


```
dbxml> query '<html><body>
<ul>
{
for $component in collection("components.dbxml")/component
return
<li>
<b>Component number: {$component/@number/string()}</b><br/>
{
for $part-ref in $component/uses-part
return
for $part in collection("parts.dbxml")/part[@number =
$part-ref cast as xs:decimal]
return
<p>{$part/description/string()}</p>
}
</li>
}
</ul>
</body></html>'
1 objects returned for eager expression '<html><body>
<ul>
{
for $component in collection("components.dbxml")/component
return
<li>
<b>Component number: {$component/@number/string()}</b><br/>
{
for $part-ref in $component/uses-part
return
for $part in collection("parts.dbxml")/part[@number =
$part-ref cast as xs:decimal]
return
<p>{$part/description/string()}</p>
}
</li>
}
</ul>
</body></html>'
```

This query will take advantage of one of the indices we created earlier. XQuery assigns the variable `$part-ref` the very general XPath number type. The index we defined earlier applies only to decimal values which is a more specific numeric type than number. To get the query to use that index we need to provide some help to the query optimizer by using the cast as `xs:decimal` clause. This provides more specific type information about the data we are comparing. If we do not use this, the query optimizer cannot use the decimal index because the type XQuery is using and the type of the index is using do not match.


The output of the query, reformatted for readability, is:

```
dbxml> print
<html><body>
<ul>
<li>
<b>Component number: 1</b><br/>
<p>Description of 89</p>
<p>Description of 150</p>
<p>Description of 899</p>
</li>
<li>
<b>Component number: 2</b><br/>
<p>Description of 901</p>
<p>Description of 87</p>
<p>Description of 189</p>
</li>
</ul>
</body></html>
```

The following shows the previous HTML as displayed in a web browser:

![The data rendered in HTML](/images/working-with-data.png)

The container model provided by Berkeley DB provides a great deal of flexibility because there is no specific XML schema associated with a container. XML data of varying structures can coexist in a single container . Alternatively, separate containers can contain XML documents that are identical along conceptual lines, or for other purposes. *Container and document organization should be tailored to the needs of your application.*


#### Other Resources
* [Fiagro and XQuery](xref:figaro-and-xquery.md)
* [The dbxml Shell](xref:the-dbxml-shell.md)
* [Using FLWOR](xref:using-flwor.md)
* [Retrieving Data using XQuery](xref:retrieving-data-using-xquery.md)
* [Performing Queries](xref:performing-queries.md)