---
uid: introducing-indices.md
---

# Introducing Indices

One major advantage of modern native XML databases is their ability to index the XML documents they contain. Proper use of indices can significantly reduce the time required to execute a particular XQuery expression. The previous examples likely executed in a perceptible amount of time, because Figaro was evaluating each and every document in the container against the query. Without indices, Figaro has no choice but to review each document in turn. With indices, Figaro can find a subset of matching documents with a single, or significantly reduced, set of lookups. By carefully applying indexing strategies we can improve retrieval performance considerably.

## Introducing Indices

To examine the usefulness of our indices, we will use the `time` command with each of our queries. This will report how long it takes for each operation to complete.

>[!NOTE]
>The following query execution times are relative to the computer and operating system used by the author. Your query times will differ as they depend on many qualities of your system. However, the percentage in improvement in query execution time should be relatively similar.
Recall the first structural query:


```
time query 'collection("parts.dbxml")/part[parent-part]'
10000 objects returned for eager expression 'collection("parts.dbxml")/part[parent-part]'

Time in seconds for command 'query': 0.437096
```

Notice the query execution time. This query takes almost a half a second to execute because the query is examining each document in turn as it searches for the presence of a` parent-part` element. To improve our performance, we want to specify an index that allows Figaro to identify the subset of documents containing the `parent-part` element without actually examining each document.


Indices are specified in four parts: `path type`, `node type`, `key type`, and `uniqueness`. This query requires an index of the node elements to determine if something is present or not. Because the pattern is not expected to be unique, we do not want to turn on uniqueness. Therefore, the Figaro index type that we should use is `node-element-presence-none`.

```
dbxml> addIndex "" parent-part node-element-presence-none
Adding index type: node-element-presence-none to node: {}:parent-part

dbxml> time query 'collection("parts.dbxml")/part[parent-part]'
10000 objects returned for eager expression 'collection("parts.dbxml")/part[parent-part]'
```

Our query time improved from .4 seconds to .02 seconds. As containers grow in size or complexity, indices increase performance even more dramatically.


The previous index will also improve the performance of the value query designed to search for the value of the parent-part element. But for better results, we should index the node as a double value. (You use `double` here instead of decimal because the XQuery specification indicates that implicit numerical casts should be cast to `double`).


To do this, use a `node-element-equality-double` index.


```
dbxml> time query '
collection("parts.dbxml")/part[parent-part = 1]'
3333 objects returned for eager expression '
collection("parts.dbxml")/part[parent-part = 1]'

Time in seconds for command 'query': 0.511752

dbxml> addIndex "" parent-part node-element-equality-double

Adding index type: node-element-equality-decimal to node: {}:parent-part
dbxml> time query '
collection("parts.dbxml")/part[parent-part = 1]'
3333 objects returned for eager expression '
collection("parts.dbxml")/part[parent-part = 1]'

Time in seconds for command 'query': 0.070674
```

Additional indices will improve performance for the other value queries.


```
dbxml> time query '
collection("parts.dbxml")/part[@number > 100 and @number < 105]'
4 objects returned for eager expression
'collection("parts.dbxml")/part[@number > 100 and @number < 105]'

Time in seconds for command 'query': 5.06106
```

At over 5 seconds there is plenty of room for improvement. To improve our range query, we can provide an index for the number attribute:


```
dbxml>  addIndex "" number node-attribute-equality-double

Adding index type: node-attribute-equality-double to node: {}:number

dbxml> time query '
collection("parts.dbxml")/part[@number > 100 and @number < 105]'
4 objects returned for eager expression '
collection("parts.dbxml")/part[@number > 100 and @number < 105]'

Time in seconds for command 'query': 3.33212
```

As you can see, proper use of indices can dramatically effect query performance.



## See Also


#### Other Resources
* [Fiagro and XQuery](xref:figaro-and-xquery.md)
* [The dbxml Shell](xref:the-dbxml-shell.md)
* [Using Indices](xref:using-indices.md)