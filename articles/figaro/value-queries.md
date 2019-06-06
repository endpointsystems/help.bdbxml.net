---
uid: value-queries.md
---

# Value Queries

XQuery is equally adept at finding data based on value. The following examples combine structural queries with restrictions on the values returned in the result.

To select all parts that have a parent-part as a child and also have a parent-part value of 1:

```
dbxml> query '
collection("parts.dbxml")/part[parent-part = 1]'

3333 objects returned for eager expression '
collection("parts.dbxml")/part[parent-part = 1]'
```

Notice that the query is identical to the query used in the previous example, except that it uses '[`parent-part = 1]`'. The results follow:

```
dbxml> print
...
<part number="99820"><description>Description of 99820</description>
<category>0</category><parent-part>1</parent-part></part>
<part number="99850"><description>Description of 99850</description>
<category>0</category><parent-part>1</parent-part></part>
<part number="99880"><description>Description of 99880</description>
<category>0</category><parent-part>1</parent-part></part>
<part number="99910"><description>Description of 99910</description>
<category>0</category><parent-part>1</parent-part></part>
<part number="99940"><description>Description of 99940</description>
<category>0</category><parent-part>1</parent-part></part>
<part number="99970"><description>Description of 99970</description>
<category>0</category><parent-part>1</parent-part></part>
```

XQuery also provides a full set of expressions that you can use to select documents from the container. For instance, if we wanted to look up the parts with part numbers 1070 and 1032 we could run the following query:

>[!NOTE]
> This query is searching on the value of an _attribute_ rather than the value of an _element_. This is an equally valid way to search for documents.

```
dbxml> query 'collection("parts.dbxml")/part[@number = 1070 or @number = 1032]'

2 objects returned for eager expression 'collection("parts.dbxml")/part[@number = 1070 or @number = 1032]'

dbxml> print
<part number="1070"><description>Description of 1070</description>
<category>0</category><parent-part>2</parent-part></part>
<part number="1032"><description>Description of 1032</description>
<category>2</category></part>
```

Standard inequality operators and other expressions are also available and help to isolate the required subset of data within a container:

```
dbxml> query 'collection("parts.dbxml")/part[@number > 100 and @number < 105]'

4 objects returned for eager expression 'collection("parts.dbxml")/part[@number > 100 and @number < 105]'

dbxml> print
<part number="101"><description>Description of 101</description>
<category>1</category></part>
<part number="102"><description>Description of 102</description>
<category>2</category></part>
<part number="103"><description>Description of 103</description>
<category>3</category></part>
<part number="104"><description>Description of 104</description>
<category>4</category></part>
```

#### Other Resources

* [Fiagro and XQuery](xref:figaro-and-xquery.md)
* [The dbxml Shell](xref:the-dbxml-shell.md)
* [Using XQuery](xref:using-xquery.md)