---
uid: using-flwor.md
---

# Using FLWOR

XQuery offers iterative and transformative capabilities through FLWOR (pronounced "flower") expressions. FLWOR is an acronym that stands for the five major clauses in a FLWOR expression: for, let, where, order, by and return. Using FLWOR expressions, you can iterate over sequences (frequently result sets in Figaro), use variables, and filter, group, and sort sequences. You can even use FLWOR to perform joins of different data sources.


For example, suppose you had documents in your container that looked like this:


``` XML
<product>
<name>Widget A</name>
<price>0.83</price>
</product>
```

In this case, queries against the container for these documents return the documents in order by their document name. But suppose you wanted to see all such documents in your container, ordered by price. You can do this with a FLWOR expression:


```
for $i in collection('myContainer.dbxml')/product
order by $i/price descending
return $i
```

Note that from within Figaro, you must provide FLWOR expressions in a single string. Lines can be separated either by a carriage return ("\n") or by a space. Thus, the above expression would become:


``` C#
string  flwor = "for $i in collection('myContainer.dbxml')/product\n";
  flwor += "order by $i/price descending\n";
  flwor += "return $i";
```


## See Also


#### Other Resources
* [Using XQuery](xref:using-xquery.md)