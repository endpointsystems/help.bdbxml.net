---
uid: indexer-processing-notes.md
---

# Indexer Processing Notes

As you design your indexing strategy, keep the following in mind:

* As with all indexing mechanisms, the more indices that you maintain the slower your write performance will be. Substring indices are particularly heavy relative to write performance.
* The indexer does not follow external references to document type definitions and external entities. References to external entities are removed from the character data. Pay particular attention to this when using equality and substring indices as element and attribute values (as indexed) may differ from what you expect.
* The indexer substitutes internal entity references with their replacement text.

The indexer concatenates character data mixed with child data into a single value. For example, as indexed the fragment:

``` XML
<node1>
  This is some text with some
  <inline>inline </inline> data.
</node1>
```

has two elements. `<node1>` has the value: "This is some text with some data" while `<inline>` has the value: "inline".
The indexer expands CDATA sections. For example, the fragment:


``` XML
<node1>
  Reserved XML characters are <![CDATA['<', '>', and '"']]>
</node1>
```

is indexed as if `<node1>` has the value: "Reserved XML characters are '<', '>', and '&'"@Figaro.UpdateContext
The indexer replaces namespace prefixes with the namespace URI to which they refer. For example, the class attribute in the following code fragment:


``` XML
<node1 myPrefix:class="test"
xlmns:myPrefix="http://dbxmlExamples/testPrefix />
```

is indexed as :


``` XML
<node1 http://dbxmlExamples/testPrefix:class="test"
xlmns:myPrefix="http://dbxmlExamples/testPrefix />
```

This normalization ensures that documents containing the same element types, but with different prefixes for the same namespace, are indexed as if they were identical.
