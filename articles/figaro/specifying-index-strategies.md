---
uid: specifying-index-strategies.md
---

# Specifying Index Strategies

The combined index type and syntax type is called the index strategy. To specify an index, you declare it using a string that specifies your index strategy.

This string is formatted as follows:


```
[unique]-{path type}-{node type}-{key type}-{syntax type}
```

where 

* `unique` is the actual value that you provide in this position on the string. If you provide this value, then indexed values must be unique. If you do not want indexed values to be unique, provide nothing for this position in the string. See [Uniqueness](xref:uniqueness.md) for more information.
* `{path type}` identifies the path type. Valid values are:
	* `node`
	* `edge`
	See [Path Types](xref:path-types.md)  for more information.
* `{node type}` identifies the type of node being indexed. Valid values are:
	* `element`
	* `attribute`
	* `metadata`
	If `metadata` is specified, then `{path type}` must be `node`. See [Node Types](xref:node-types.md)  for more information.
* `{key type}` identifies the sort of test that the index supports. The following key types are supported:
	* `presence`
	* `equality`
	* `substring`
	See [Key Types](xref:key-types.md) for more information.
* `{syntax type}` identifies the syntax to use for the indexed value. Specify one of the following values:
	* AnySimpleType
	* AnyUri
	* Base64Binary
	* Binary
	* Boolean
	* Date
	* DateTime
	* DaytimeDuration
	* Decimal
	* Double
	* Duration
	* Float
	* GDay
	* GMonth
	* GYear
	* GYearMonth
	* HexBinary
	* Node
	* None
	* Notation
	* QName
	* String
	* Time
	* UntypedAtomic
	* YearMonthDuration</li></ul>&nbsp;

>[!NOTE]
>If the key type is `presence`, then the syntax type should be `none`. Also, for queries that examine numerical data without specifying the cast explicitly, use `double` instead of `decimal `for the index. This is because the XQuery specification requires implicit casts of numerical data to be performed as a double.

The following are some example index strategies:

* `node-element-presence-none`

Index an element node for presence queries. That is, queries that test whether the node exists in the document.

* `unique-node-metadata-equality-string`

Index a metadata node for equality string compares. The value provided for this node must be unique within the container. This strategy is actually used by default for all documents in a container. It is used to index the document's name.

* `edge-attribute-equality-float`

Define an equality float index for an attribute's edge. This improves performance for queries that examine whether a specific element/@attribute path is equal to a float value.

Also, be aware that you can specify multiple indices at a time by providing a space-separated (or comma-separated)list of index strategies in the string. For example, you can specify two index strategies at a time using:

``` XML
node-element-presence-none edge-attribute-equality-float
```
or 
``` XML
node-element-presence-none,edge-attribute-equality-float
```


## See Also

* [Uniqueness](xref:uniqueness.md) 
* [Path Types](xref:path-types.md) 
* [Node Types](xref:node-types.md) 
* [Key Types](xref:key-types.md)