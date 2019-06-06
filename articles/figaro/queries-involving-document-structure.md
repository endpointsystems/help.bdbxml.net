---
uid: queries-involving-document-structure.md
---

# Queries Involving Document Structure

Notice that the parts container can contain documents with different structures. The ability to manage structured data in a flexible manner is one of the fundamental differences between XML and relational databases. In this example, a single container manages documents of two different structures sharing certain common elements. The fact that the documents partially overlap in structure allows for efficient queries and common indices. This can be used to model a union of related data. Structural queries exploit such natural unions in XML data. Here are some example structural queries.


First, select all part records containing parent-part nodes in their document structure. In English, the following XQuery would read: __from the container named parts select all part elements that also contain a parent-part element as a direct child of that element__. 

As XQuery code, it is:

```
dbxml> query 'collection("parts.dbxml")/part[parent-part]'

10000 objects returned for eager expression 'collection("parts.dbxml")/part[parent-part]'
```

To examine the query results, use the `print` command:


```
dbxml> print
<part number="0"><description>Description of 0</description>
<category>0</category><parent-part>0</parent-part></part>
<part number="10"><description>Description of 10</description>
<category>0</category><parent-part>0</parent-part></part>
...
<part number="99980"><description>Description of 99980</description>
<category>0</category><parent-part>0</parent-part></part>
<part number="99990"><description>Description of 99990</description>
<category>0</category><parent-part>0</parent-part></part>
```

To display only the `parent-part` element without displaying the rest of the document, the query changes only slightly:


```
dbxml> query 'collection("parts.dbxml")/part/parent-part'

10000 objects returned for eager expression 'collection("parts.dbxml")/part/parent-part'

dbxml> print
<parent-part>0</parent-part>
<parent-part>1</parent-part>
<parent-part>2</parent-part>
...
<parent-part>1</parent-part>
<parent-part>2</parent-part>
<parent-part>0</parent-part>
```

Alternately, to retrieve the value of the `parent-part` element, the query becomes:


```
dbxml> query 'collection("parts.dbxml")/part/parent-part/string()'

10000 objects returned for eager expression 'collection("parts.dbxml")/part/parent-part/string()'

dbxml> print
0
1
2
...
1
2
0
```

Invert the earlier example to select all documents that do not have `parent-part` elements:


```
dbxml> query 'collection("parts.dbxml")/part[not(parent-part)]'

90000 objects returned for eager expression 'collection("parts.dbxml")/part[not(parent-part)]'

dbxml> print
...
<part number="99989"><description>Description of 99989</description>
<category>9</category></part>
<part number="99991"><description>Description of 99991</description>
<category>1</category></part>
<part number="99992"><description>Description of 99992</description>
<category>2</category></part>
<part number="99993"><description>Description of 99993</description>
<category>3</category></part>
<part number="99994"><description>Description of 99994</description>
<category>4</category></part>
<part number="99995"><description>Description of 99995</description>
<category>5</category></part>
<part number="99996"><description>Description of 99996</description>
<category>6</category></part>
<part number="99997"><description>Description of 99997</description>
<category>7</category></part>
<part number="99998"><description>Description of 99998</description>
<category>8</category></part>
<part number="99999"><description>Description of 99999</description>
<category>9</category></part>
```

Structural queries are somewhat like relational joins, except that they are easier to express and manage over time. Some structural queries are even impossible or impractical to model with more traditional relational databases. This is in part due to the nature of XML as a self describing, yet flexible, data representation.


Collections of XML documents attain commonality based on the similarity in their structures just as much as the similarity in their content. Essentially, relationships are implicitly expressed within the XML structure itself. The utility of this feature becomes more apparent when you start combining structural queries with value based queries.

#### Other Resources
* [Fiagro and XQuery](xref:figaro-and-xquery.md)
* [The dbxml Shell](xref:the-dbxml-shell.md)
* [Using FLWOR](xref:using-flwor.md)