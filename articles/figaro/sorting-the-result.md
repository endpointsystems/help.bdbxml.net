---
uid: sorting-the-result.md
---

# Sorting the Result

The 'O' in FLWOR stands for '*order by*'. The previous XQuery expression did not contain explicit ordering instructions, and so the results were presented based on its order in the container. Over time, as the document set changes, the data will not maintain a constant order. Adding an explicit order by clause to the XQuery statement allows us to implement strict ordering:


```
dbxml> query '<html><body>
<ul>
{
for $part in
(collection("parts.dbxml")/part[@number > 100 and @number < 105])
order by xs:decimal($part/@number) descending
return
<li>{$part/description/string()}</li>
}
</ul></body></html>'
1 objects returned for eager expression '<html><body>
<ul>
{
for $part in
(collection("parts.dbxml")/part[@number > 100 and @number < 105])
order by xs:decimal($part/@number) descending
return
<li>{$part/description/string()}</li>
}
</ul></body></html>'

dbxml> print
<html><body><ul>
<li>Description of 104</li>
<li>Description of 103</li>
<li>Description of 102</li>
<li>Description of 101</li>
</ul></body></html>
```

The following shows the previous HTML as displayed in a web browser:

![Parts in descending order](/images/multiple-containers.png)

The parts are now ordered in descending order, as expected.

![Sorted parts](/images/reshaping-sample-sorted.png)


#### Other Resources
* [Fiagro and XQuery](xref:figaro-and-xquery.md)
* [The dbxml Shell](xref:the-dbxml-shell.md)
* [Using FLWOR](xref:using-flwor.md)
* [Retrieving Data using XQuery](xref:retrieving-data-using-xquery.md)
* [Performing Queries](xref:performing-queries.md)